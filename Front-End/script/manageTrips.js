const fetchMembers = async () => {
  try {
    const response = await fetch(
      "https://localhost:7156/api/Driver/available",
      {
        method: "GET",
        headers: {
          Authorization: "Bearer " + localStorage.getItem("userToken"),
          "Content-Type": "application/json",
        },
      }
    );
    if (!response.ok) {
      throw new Error(`Error: ${response.status} - ${response.statusText}`);
    }
    return await response.json();
  } catch (error) {
    console.error("Failed to fetch drivers:", error);
    return { data: { $values: [] } }; // Return empty data structure to prevent crashes
  }
};
const displayMember = async () => {
  const drivers = await fetchMembers();
  const driverList = document.querySelector("#driverList");

  // Clear previous checkboxes
  if (drivers.data.$values.length === 0) {
    driverList.innerHTML = "No available drivers.";
    return;
  }

  driverList.innerHTML = ""; // Reset container

  drivers.data.$values.forEach((driver) => {
    driverList.innerHTML += `
            <div class="flex items-center space-x-2 mb-1">
                <input type="checkbox" value="${driver.id}" name="driverIds" class="driverCheckbox" onchange="limitDriverSelection()">
                <label>${driver.firstName} ${driver.lastName}</label>
            </div>
        `;
  });
};
displayMember();

let limitDriverSelection = () => {
  const selected = document.querySelectorAll(".driverCheckbox:checked");
  const warning = document.getElementById("driverLimitWarning");

  if (selected.length > 3) {
    // Uncheck the last one that triggered it
    selected[selected.length - 1].checked = false;
    warning.classList.remove("hidden");
  } else {
    warning.classList.add("hidden");
  }
};
async function loadVehicles() {
  try {
    const response = await fetch(
      "https://localhost:7156/api/Vehicle/get-all?PageSize=10&CurrentPage=1",
      {
        method: "GET",
        headers: {
          Authorization: "Bearer " + localStorage.getItem("userToken"),
          "Content-Type": "application/json",
        },
      }
    );
    const data = await response.json();
    let vehicleSelect = document.getElementById("vehicleId");

    data.data.items.$values.forEach((vehicle) => {
      let option = document.createElement("option");
      option.value = vehicle.id;
      option.textContent = vehicle.name + " (" + vehicle.plateNo + ")";
      vehicleSelect.appendChild(option);
    });
  } catch (error) {
    console.error("Error fetching vehicles:", error);
  }
}
loadVehicles();

async function loadTrips() {
  try {
    const response = await fetch(
      "https://localhost:7156/api/Trip/all?PageSize=10&CurrentPage=1",
      {
        method: "GET",
        headers: {
          Authorization: "Bearer " + localStorage.getItem("userToken"),
          "Content-Type": "application/json",
        },
      }
    );

    if (!response.ok) {
      throw new Error(`Error: ${response.status} - ${response.statusText}`);
    }

    const trips = await response.json();
    const tripList = document.getElementById("tripList");
    tripList.innerHTML = "";
    console.log(trips);

    if (
      !trips.data ||
      !trips.data.items ||
      trips.data.items.$values.length === 0
    ) {
      tripList.innerHTML = `<tr class="m-4"><td colspan="9" class="text-center font-bold text-4xl">No trips found.</td></tr>`;
      return;
    }

    trips.data.items.$values.forEach((trip) => {
      let driverNames = "";
      if (trip.drivers && trip.drivers.$values.length > 0) {
        trip.drivers.$values.forEach((driver) => {
          driverNames += `<p>${driver.firstName} ${driver.lastName}</p>`;
        });
      } else {
        driverNames = "N/A";
      }

      const row = `
        <tr class="border-b" id="row-${trip.id}">
          <td class="p-2">${trip.startingLocation} to ${trip.destination}</td>
          <td class="p-2">${trip.startingLocation}</td>
          <td class="p-2">${trip.destination}</td>
          <td class="p-2">${trip.departureTime}</td>
          <td class="p-2">${trip.departureDate}</td>
          <td class="p-2">${trip.vehicle?.name || "N/A"}</td>
          <td class="p-2">${driverNames}</td>
          <td class="p-2">₦${trip.amount}</td>
          <td class="p-2">
            <button class="text-blue-500 mr-2">Edit</button>
            <button class="text-red-500" onclick="deleteTrip('${
              trip.id
            }')">Delete</button>
          </td>
        </tr>
      `;

      tripList.innerHTML += row;
    });
  } catch (error) {
    console.error("Error loading trips:", error);
  }
}

loadTrips();


async function deleteTrip(tripId) {
  if (!confirm("Are you sure you want to delete this trip?")) return;

  try {
    let response = await fetch(
      `https://localhost:7156/api/Trip/delete/${tripId}`,
      {
        method: "DELETE",
        headers: {
          Authorization: "Bearer " + localStorage.getItem("userToken"),
        },
      }
    );

    if (response.ok) {
      alert("Trip deleted successfully!");
      // Remove the deleted trip from the UI
      document.getElementById(`row-${tripId}`).remove();
    } else {
      let errorMessage = await response.text();
      alert("Failed to delete trip: " + errorMessage);
    }
  } catch (error) {
    console.error("Error deleting trip:", error);
    alert("An error occurred while deleting the trip.");
  }
}

document
  .getElementById("addTripForm")
  .addEventListener("submit", async function (event) {
    event.preventDefault();

    // Collecting form data
    let tripData = {
      startingLocation: document.getElementById("startingLocation").value,
      destination: document.getElementById("destination").value,
      departureTime: document.getElementById("departureTime").value,
      departureDate:
        document.getElementById("departureDate").value +
        "T" +
        document.getElementById("departureTime").value, // Combine date and time
      description: document.getElementById("description").value,
      amount: parseFloat(document.getElementById("amount").value),
      vehicleId: document.getElementById("vehicleId").value,
      status: true,
      driverIds: Array.from(
        document.querySelectorAll(".driverCheckbox:checked")
      ).map((checkbox) => checkbox.value), // Collect selected driver IDs
    };

    // Ensure up to 3 drivers are selected
    if (tripData.driverIds.length > 3) {
      document.getElementById("driverLimitWarning").classList.remove("hidden");
      return;
    } else {
      document.getElementById("driverLimitWarning").classList.add("hidden");
    }

    console.log(tripData);

    try {
      let response = await fetch("https://localhost:7156/api/Trip/create", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("userToken"),
        },
        body: JSON.stringify(tripData),
      });

      let result = await response.json();
      alert(result.message);

      if (result.status) {
        closeModal();
        await loadTrips();
      }
    } catch (error) {
      alert("Error adding trip: " + error);
    }
  });

// Function to limit driver selection to 3

function openModal() {
  document.getElementById("tripModal").classList.remove("hidden");
}
function closeModal() {
  document.getElementById("tripModal").classList.add("hidden");
}


