import { logout } from "./logout.js";
logout();
let postEvent = async () => {
  try {
    let response = await fetch("https://localhost:7156/api/Customer/profile", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("userToken"),
      },
    });

    let data = await response.json();
    console.log(data);

    if (!response.ok) {
      throw new Error(data.message || "Failed to fetch customer profile");
    }

    currentCustomer = data.data; // Ensure response structure matches
    alert(data.message);

    if (currentCustomer) {
      console.log(currentCustomer.firstName)
      document.getElementById("username").textContent = currentCustomer.firstName; 
    }
  } catch (error) {
    alert(error.message);
  }
};
// Call the function
postEvent();
document.getElementById("username").textContent = username;

async function loadCustomerTrips() {
  try {
    const response = await fetch(
      `https://localhost:7156/api/Trip/customer-Trips?PageSize=10&CurrentPage=1`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("userToken"),
        },
      }
    );
    const result = await response.json();

    if (!result.status || !Array.isArray(result.data)) {
      throw new Error("Invalid response from server");
    }
    console.log(response)
    const trips = result.data;
    const tableBody = document.getElementById("tripsTableBody");

    let completedCount = 0;

    trips.forEach((trip) => {
      const row = document.createElement("tr");

      row.innerHTML = `
            <td class="border p-2">${trip.destination}</td>
            <td class="border p-2">${trip.departureTime}</td>
            <td class="border p-2">${new Date(
              trip.departureDate
            ).toLocaleDateString()}</td>
            <td class="border p-2 ${
              trip.status === true
                ? "text-green-500"
                : "text-yellow-500"
            }">${trip.status}</td>
            <td class="border p-2 text-blue-500 hover:underline cursor-pointer">View</td>
          `;

      if (trip.status === true) completedCount++;
      tableBody.appendChild(row);
    });

    // Update overview stats
    document.getElementById("totalTrips").textContent = trips.length;
    document.getElementById("completedTrips").textContent = completedCount;
  } catch (err) {
    console.error("Failed to load trips:", err);
  }
}

loadCustomerTrips();

let Count = async (url) => {
  let response = await fetch(`${url}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("userToken"),
    },
  });

  return response.json();
};
let pendingReview = async () => {
  let response = await Count(
    "https://localhost:7156/api/Trip/Customer-Trips-pending-review"
  );
    
    document.getElementById("pendingReviews").textContent =
      response.pendingReview || 0;
};
pendingReview();
