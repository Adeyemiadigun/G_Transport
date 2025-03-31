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

let customerTrip = async () => {
  let response = await Count(
    "https://localhost:7156/api/Trip/Customer-Trips-Count"
  );
  document.querySelector("#tripCount").innerHTML = response.CustomerTrip || 0;
};


let pendingReview = async () => {
  let response = await Count(
    "https://localhost:7156/api/Trip/Customer-Trips-pending-review"
  );
  document.querySelector("#pendingReview").innerHTML =
    response.pendingReview || 0;
};
let currentCustomer;

(async () => 
{
  await customerTrip();
  await pendingReview();
})()
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

    // Ensure currentCustomer is set before updating DOM
    let customerName = document.querySelector("#name");
    if (currentCustomer) {
      customerName.innerHTML = `Welcome, ${currentCustomer.firstName}`;
    }
  } catch (error) {
    alert(error.message);
  }
};

// Call the function
postEvent();
const fetchTrips = async () => {
  const trips = await fetch(
    "https://localhost:7156/api/Trip/available?PageSize=10&CurrentPage=1",
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("userToken"),
      },
    }
  );

  return trips.json();
};
const displayTrips = async () => {
  const trip = await fetchTrips();
  let trips = document.querySelector("#trips");

  trip.data.items.$values.every((element, index) => {
    //+
    console.log(element);
    trips.innerHTML += `<div class="bg-gray-100 p-4 rounded-lg shadow">
    <h3 class="text-lg font-semibold">${element.startingLocation} to ${element.destination}</h3>
    <p class="text-gray-600">Time: ${element.departureTime}</p>
    <p class="text-gray-600">Date: ${element.departureDate}</p>
    <p class="text-gray-800 font-bold">₦${element.amount}</p>
    <button class="mt-2 bg-blue-500 text-white px-4 py-2 rounded">Book Now</button>
    </div>`;
    if (index == 2) return false;
  });
};
displayTrips();
let viewTrip = () => {
  fetch("https://localhost:7156/api/Trip/recent?PageSize=10&CurrentPage=1", {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("userToken"),
    },
  })
    .then((response) => response.json())
    .then((data) => {
      if (data.status) {
        console.log("Recent Trips:", data.data.items);
        let tBody = document.querySelector("#tbody");
        data.data.items.$values.forEach((item) => {
          tBody.innerHTML += `<tr class="border-b">
                            <td class="p-2">${item.startingLocation} to ${item.destination}</td>
                            <td class="p-2">${item.departureTime}</td>
                            <td class="p-2">${item.departureDate}</td>
                            <td class="p-2 text-green-500">${item.status}</td>
                            <td class="p-2"><a href="#" class="text-blue-500">View</a></td>
                        </tr>`;
        });
      } else {
        console.log("No recent trips found");
      }
    })
    .catch((error) => console.error("Error:", error));
};
viewTrip();

const depositAmountInput = document.getElementById("depositAmount");
const depositButton = document.getElementById("depositButton");
depositButton.addEventListener("click", async function () {
  const amount = parseFloat(depositAmountInput.value);
  if (isNaN(amount) || amount <= 0) {
    alert("Please enter a valid deposit amount.");
    return;
  }

  try {
    const response = await fetch("https://localhost:7156/api/Wallet/deposit", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("userToken"),
      },
      body: JSON.stringify({ amount }),
    });

    const data = await response.json();
    if (data.status) {
      alert("Deposit successful!");
      getEvent(); // Refresh balance
    } else {
      alert("Deposit failed. Try again.");
    }
  } catch (error) {
    console.error("Error processing deposit:", error);
    alert("An error occurred.");
  }
});
