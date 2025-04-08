import { logout } from "./logout.js";
logout();

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
console.log(trip)
  trip.data.items.$values.forEach((element) => {
     let driverNames = "";

     element.drivers.$values.forEach((driver) => {
       driverNames += `<p>${driver.firstName} ${driver.lastName}</p>`;
     });
    console.log(element);
    trips.innerHTML += `<tr class="border-b" id="row-${element.id}">
                        <td class="p-2">${element.startingLocation} to ${
      element.destination
    }</td>
                        <td class="p-2">${element.startingLocation}</td>
                        <td class="p-2">${element.destination}</td>
                        <td class="p-2">${element.departureTime}</td>
                        <td class="p-2">${element.departureDate}</td>
                        <td class="p-2">${element.vehicleName || "N/A"}</td>
                        <td class="p-2">${driverNames}</td>
                        <td class="p-2">₦${element.amount}</td>
                        <td class="p-2">
                            <button class="text-blue-500 mr-2">Edit</button>
                            <button class="text-red-500" onclick="deleteTrip('${
                              element.id
                            }')">Delete</button>
                        </td>
                    </tr>`;
  });
};
displayTrips();

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


let tripCount = async () =>
{
  let response = await Count("https://localhost:7156/api/Trip/TripCount");
  document.querySelector("#tripCount").innerHTML =
  response.successfullTripCount || 0;
}


let displayFailedCount = async () => {
  let response = await Count("https://localhost:7156/api/Trip/FailedTripCount");
  document.querySelector("#failedTrip").innerHTML = response.failedTripCount || 0;
};

let customer = async () => {
  let response = await Count("https://localhost:7156/api/Trip/FailedTripCount");
  document.querySelector("#customer").innerHTML = response.failedTripCount || 0;
};


(async () => {
  await displayFailedCount();
  await customer();
  await tripCount();
})()