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

  trip.data.items.$values.foreach(element => {
    console.log(element);
    trips.innerHTML += `<tr class="border-b" id="row-${trip.id}">
                        <td class="p-2">${trip.startingLocation} to ${
      trip.destination
    }</td>
                        <td class="p-2">${trip.startingLocation}</td>
                        <td class="p-2">${trip.destination}</td>
                        <td class="p-2">${trip.departureTime}</td>
                        <td class="p-2">${trip.departureDate}</td>
                        <td class="p-2">${trip.vehicle.name || "N/A"}</td>
                        <td class="p-2">${trip.driverName}</td>
                        <td class="p-2">₦${trip.amount}</td>
                        <td class="p-2">
                            <button class="text-blue-500 mr-2">Edit</button>
                            <button class="text-red-500" onclick="deleteTrip('${
                              trip.id
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