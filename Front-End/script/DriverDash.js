import { logout } from "./logout.js";
logout();
document.addEventListener("DOMContentLoaded", () => {
  const assignedTripContainer = document.getElementById("assignedTrip");

  async function fetchAssignedTrip() {
    try {
      console.log(localStorage.getItem("userToken"));
      const res = await fetch("https://localhost:7156/api/Trip/assigned-trip", {
        method: "GET",
        headers: {
          Authorization: "Bearer " + localStorage.getItem("userToken"),
          "Content-Type": "application/json",
        },
      });
      if (!res.ok) throw new Error("Failed to fetch trip");

      const data = await res.json();
      const trip = data.data;
      console.log(trip)
      let status
      if(trip.status == 1)
        status = "Pending"
      if(trip.status == 2)
        status = "Successful"
      if(trip.status == 3)
        status = "Failed"
      assignedTripContainer.innerHTML = `
                <p><strong>From:</strong> ${trip.startingLocation}</p>
                <p><strong>To:</strong> ${trip.destination}</p>
                <p><strong>Date:</strong> ${new Date(
                  trip.departureDate
                ).toLocaleDateString()}</p>
                <p><strong>Status:</strong> ${status }</p>
                <label for="statusSelect" class="block mt-4 mb-2 font-semibold">Update Status:</label>
                <select id="statusSelect" class="p-2 border rounded w-full">
                    <option value="1">Pending</option>
                    <option value="2">Successful</option>
                    <option value="3">Failed</option>
                </select>
                <button id="updateStatusBtn" class="mt-4 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
                    Update Status
                </button>
            `;

      document
        .getElementById("updateStatusBtn")
        .addEventListener("click", async () => {
          const selectedStatus = document.getElementById("statusSelect").value;

          try {
            const statusRes = await fetch(
              `https://localhost:7156/api/Trip/Status?Id=${trip.id}&Status=${selectedStatus}`,
              {
                method: "PATCH",
                headers: {
                  "Content-Type": "application/json",
                      Authorization: "Bearer " + localStorage.getItem("userToken"),
                    
                },
              }
            );

            const statusData = await statusRes.json();

            if (!statusRes.ok) {
              alert(
                `Failed: ${statusData.message || "Could not update status"}`
              );
            } else {
              alert("Status updated successfully!");
              fetchAssignedTrip(); // Refresh assigned trip
            }
          } catch (err) {
            alert("Error updating status");
          }
        });
    } catch (error) {
      // assignedTripContainer.innerHTML = `<p class="text-red-500">Unable to load assigned trip.</p>`;
    }
  }

  fetchAssignedTrip();
});
