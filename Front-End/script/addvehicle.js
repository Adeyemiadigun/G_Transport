
// Selecting input fields
// Error messages array
let errorMessages = [];

 let submitVehicleBtn = document.querySelector("#submitVehicleBtn");
submitVehicleBtn.addEventListener("click", (e) => {
  e.preventDefault();
  let vehicleName = document.querySelector("#vehicleName");
  let vehicleDescription = document.querySelector("#vehicleDescription");
  let vehicleCapacity = document.querySelector("#vehicleCapacity");
  let vehiclePlateNo = document.querySelector("#vehiclePlateNo");
  let validation = validateVehicleForm();
  if (!validation) {
    alert(errorMessages.join("\n"));
    errorMessages = [];
  } else {
    let vehicleData = {
      name: vehicleName.value,
      description: vehicleDescription.value,
      capacity: parseInt(vehicleCapacity.value), // Convert to integer
      plateNo: vehiclePlateNo.value,
    };

    // Send data to API
    registerVehicle(vehicleData);
  }
});

// Function to send the request to the API
let registerVehicle = (vehicleData) => {
  console.log(vehicleData)
  fetch("https://localhost:7156/api/Vehicle/create", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("userToken"),
    },
    body: JSON.stringify(vehicleData),
  })
    .then((res) => res.json())
    .then((response) => {
      console.log(response);
      alert(response.message);
      viewVehicle();
    })
    .catch((error) => {
      alert("Error: " + error);
    });
};

// Form validation function
let validateVehicleForm = () => {
  let isValid = true;

  if (vehicleName.value.trim() === "") {
    isValid = false;
    errorMessages.push("Vehicle name cannot be empty.");
  }

  if (vehicleDescription.value.trim() === "") {
    isValid = false;
    errorMessages.push("Vehicle description cannot be empty.");
  }

  if (
    !vehicleCapacity.value ||
    isNaN(vehicleCapacity.value) ||
    parseInt(vehicleCapacity.value) <= 0
  ) {
    isValid = false;
    errorMessages.push(
      "Vehicle capacity must be a valid number greater than zero."
    );
  }

  if (vehiclePlateNo.value.trim() === "") {
    isValid = false;
    errorMessages.push("Vehicle plate number cannot be empty.");
  }

  return isValid;
};

      //  public Guid Id { get; set; }
      //   public string Name { get; set; }
      //   public string Description { get; set; }
      //   public int Capacity { get; set; }
      //   public string PlateNo { get; set; }
      //   public int DriverNo { get; set; }
      //   public Guid DriverId { get; set; }
  
let viewVehicle = () => {
      fetch(
        "https://localhost:7156/api/Vehicle/get-all?PageSize=10&CurrentPage=1",
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + localStorage.getItem("userToken"),
          },
        }
      )
        .then((response) => response.json())
        .then((data) => {
          if (data.status) {
            console.log("Recent Trips:", data.data.items);
            let tBody = document.querySelector("#tbody");
            data.data.items.$values.forEach((item) => {
              tBody.innerHTML += `<tr class="border-b">
                            <td class="p-2">${item.name}</td>
                            <td class="p-2">${item.description}</td>
                            <td class="p-2">${item.capacity}</td>
                            <td class="p-2">${item.plateNo}</td>
                            <td class="p-2">${item.driverNo}</td>
                            <td class="p-2">
                                <button class="text-blue-500 mr-2">Edit</button>
                                <button class="text-red-500">Delete</button>
                            </td>
                        </tr>`;
            });
          } else {
            console.log("No recent trips found");
          }
        })
        .catch((error) => console.error("Error:", error));
      }
    viewVehicle();

      const vehicleModal = document.getElementById("vehicleModal");
      const closeVehicleModal = document.getElementById("closeVehicleModal");
      const openVehicleModal = document.getElementById("openVehicleModal"); // Button that opens modal

      // Show Modal
      openVehicleModal.addEventListener("click", function () {
        vehicleModal.classList.remove("hidden");
      });

      // Hide Modal
      closeVehicleModal.addEventListener("click", function () {
        vehicleModal.classList.add("hidden");
      });

      // Close modal on background click
      vehicleModal.addEventListener("click", function (event) {
        if (event.target === vehicleModal) {
          vehicleModal.classList.add("hidden");
        }
      });