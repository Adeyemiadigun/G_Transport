// import { getEvent } from "../script/customerDashBoard.js";
// getEvent();
import { logout } from "./logout.js";
logout();
document.addEventListener("DOMContentLoaded", () => {
  let paymentData = localStorage.getItem("paymentData");

  if (paymentData) {
    paymentData = JSON.parse(paymentData);

    generateTicket(paymentData);

    // localStorage.removeItem("paymentData");
  }
  else{
    paymentData = JSON.parse(paymentData);
    unmarkTripAsBooked(paymentData.tripId);
  }
});
function unmarkTripAsBooked(tripId) {
  let button = document.querySelector(`#trip-${tripId}`);
  if (button) {
    button.disabled = false; 
    button.innerText = "Book Now";
    button.classList.remove("bg-gray-500");
    button.classList.add("bg-blue-500");
  }
}

let fetchTrip = async () => 
{
 let response = await fetch("https://localhost:7156/api/Trip/available?PageSize=10&CurrentPage=1",
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("userToken"),
      },
    }
  );
  return response.json();
}

let displayTrip = async () =>
{
  let container = document.querySelector("#availableTrips");
  let trips = await fetchTrip()
  console.log(trips)
  trips.data.items.$values.forEach((trip) => {
    container.innerHTML += `<div class="bg-white p-4 rounded-lg shadow-md transition duration-300" id="trip-${trip.id}">
                <h3 class="text-lg font-bold">${trip.startingLocation} to ${trip.destination}</h3>
                <p class="text-gray-600">Departure: ${trip.departureTime}</p>
                <p class="text-gray-600">Date: ${trip.departureDate}</p>
                <p class="text-gray-800 font-bold">₦ ${trip.amount}</p>
                     <button class="mt-3 bg-blue-500 text-white px-4 py-2 rounded book-btn" 
                        data-trip-id="${trip.id}" 
                        data-starting="${trip.startingLocation}" 
                        data-destination="${trip.destination}" 
                        data-amount="${trip.amount}">
                        Book Now
                    </button>
            </div>`;
  });
   document.querySelectorAll(".book-btn").forEach((button) => {
            button.addEventListener("click", async (event) => {
                let tripId = event.target.dataset.tripId;
                let startingLocation = event.target.dataset.starting;
                let destination = event.target.dataset.destination;
                let amount = event.target.dataset.amount;
                await bookTrip(tripId, startingLocation, destination, amount,button);
            });
})
}
displayTrip();

let markTripAsBooked = (tripId, button) =>{
  let tripCard = document.getElementById(`trip-${tripId}`);

  if (tripCard) {
    button.disabled = true;

    button.innerText = "Booked";

    button.classList.remove("bg-blue-500", "hover:bg-blue-600");
    button.classList.add("bg-gray-400", "cursor-not-allowed");
  }
}
let bookTrip = async (
  tripId,
  startingLocation,
  destination,
  amount,
  button
) => {
  let response = await fetch("https://localhost:7156/api/Booking/create", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("userToken"),
    },
    body: JSON.stringify({
      startingLocation: startingLocation,
      destination: destination,
      status: 1, 
      tripId: tripId,
    }),
  });

  let result = await response.json();
  if (result.status) {
    console.log(result)
    console.log(result.data.id)
    alert("Booking successful! Proceeding to payment...");
    markTripAsBooked(tripId, button);
    let token = localStorage.getItem("userToken");
    const user = jwt_decode(token);
    let email =
      user[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
      ];
    await payWithPaystack(email,amount,result.data.id)
  } else {
    alert("Booking failed: " + result.message);
  }
};
async function payWithPaystack(email, amount, id) {
  const response = await fetch(
    "https://localhost:7156/api/Payment/initialize",
    {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        email: email,
        amount: amount * 100,
        callbackUrl: "http://127.0.0.1:5500/Customer/callback.html",
        bookingId: id,
      }),
    }
  );

  const data = await response.json();
  if (data.status) {
    window.location.href = data.paymentUrl;
  } else {
    alert("Payment failed: " + data.message);
  }
}
let generateTicket = (bookingData) => {
  console.log(bookingData)
  let ticketHTML = `
        <div class="ticket bg-white p-4 rounded-lg shadow-md text-center">
            <h2 class="text-xl font-bold">🎟 Trip Ticket</h2>
            <p><strong>Trip:</strong> ${bookingData.startingLocation} to ${bookingData.destination}</p>
            <p><strong>Date:</strong> ${bookingData.trip.departureTime}</p>
            <p><strong>Seat No:</strong> ${bookingData.seatNo}</p>
            <p><strong>Ticket No:</strong> ${bookingData.ticketNumber}</p>
            <p><strong>Amount Paid:</strong> ₦${bookingData.amount}</p>
            <p><strong>Transaction ID:</strong> ${bookingData.transaction}</p>
            <button onclick="downloadTicket('${bookingData.ticketNumber}', '${bookingData.trip.startingLocation}', '${bookingData.trip.destination}', '${bookingData.amount}', '${bookingData.transaction}')" class="mt-3 bg-green-500 text-white px-4 py-2 rounded">Download Ticket</button>
        </div>
    `;

  document.querySelector("#ticketContainer").innerHTML = ticketHTML;
};


let downloadTicket = (
  ticketNumber,
  startingLocation,
  destination,
  amount,
  transaction
) => {
  const { jsPDF } = window.jspdf;
  let doc = new jsPDF();

  doc.setFont("helvetica", "bold");
  doc.setFontSize(18);
  doc.text("🎟 Trip Ticket", 20, 20);

  doc.setFont("helvetica", "normal");
  doc.setFontSize(12);
  doc.text(`Trip: ${startingLocation} to ${destination}`, 20, 40);
  doc.text(`Ticket No: ${ticketNumber}`, 20, 50);
  doc.text(`Amount Paid: ₦${amount}`, 20, 60);
  doc.text(`Transaction ID: ${transaction}`, 20, 70);
  doc.text("Safe travels!", 20, 90);

  doc.save(`Ticket_${ticketNumber}.pdf`);
};

