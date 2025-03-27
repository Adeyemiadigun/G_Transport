

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
    const trips = await response.json();
    const tripList = document.getElementById("tripList");
    tripList.innerHTML = "";
  console.log(trips)
  
 if(trips.data == null)
 {
  console.log(true)
   tripList.innerHTML = `<tr class="m-4"><td colspan="9" class="text-center font-bold text-4xl ">No trips found.</td></tr>`;
   return;
 }
    trips.data.items.$values.forEach((trip) => {
      const row = `<tr class="border-b" id="row-${trip.id}">
                        <td class="p-2">${trip.startingLocation} to ${trip.destination}</td>
                        <td class="p-2">${trip.startingLocation}</td>
                        <td class="p-2">${trip.destination}</td>
                        <td class="p-2">${trip.departureTime}</td>
                        <td class="p-2">${trip.departureDate}</td>
                        <td class="p-2">${trip.vehicle.name || "N/A"}</td>
                        <td class="p-2">${trip.driverName}</td>
                        <td class="p-2">₦${trip.amount}</td>
                        <td class="p-2">
                            <button class="text-blue-500 mr-2">Edit</button>
                            <button class="text-red-500" onclick="deleteTrip('${trip.id}')">Delete</button>
                        </td>
                    </tr>`;
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
    let tripData = {
      startingLocation: document.getElementById("startingLocation").value,
      destination: document.getElementById("destination").value,
      departureTime: document.getElementById("departureTime").value,
      departureDate:"2025-03-26T08:00:00",
      description: document.getElementById("description").value,
      amount: parseFloat(document.getElementById("amount").value),
      vehicleId: document.getElementById("vehicleId").value,
      status: true,
    };
    console.log(tripData)
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

function openModal() {
  document.getElementById("tripModal").classList.remove("hidden");
}
function closeModal() {
  document.getElementById("tripModal").classList.add("hidden");
}
