let fetchTrip = async () => {
  let response = await fetch(
    "https://localhost:7156/api/Trip/without-review?PageSize=10&CurrentPage=1",
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("userToken"),
      },
    }
  );
  return response.json();
};

let displayTrips = async () => {
  let tripData = await fetchTrip();
  tripData.data.data.$values.forEach((trip) =>
  {
      let option = document.querySelector("#trip-select");
      option += `<option value="${trip.id}">${trip.startingLocation} to ${trip.destination}</option>`
  })
}
await displayTrips();

document.querySelector("#review-form").addEventListener("submit", async (event) => {
    event.preventDefault();

    let reviewData = {
      rating: parseInt(document.querySelector("#rating").value),
      comment: document.querySelector("#comment").value,
      tripId: document.querySelector("#trip-select").value,
    };

    let response = await fetch("https://localhost:7156/api/reviews", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("userToken"),
      },
      body: JSON.stringify(reviewData),
    });

    if (response.ok) {
      alert("Review submitted successfully!");
      document.querySelector("#review-form").reset();
    } else {
      alert("Failed to submit review.");
    }
});



let fetchTripDetails = async (tripId) => {
  let response = await fetch(`https://localhost:7156/api/trip/${tripId}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("userToken"),
    },
  });
  return response.json();
};

// Display previous reviews and their trip details (toggleable)
let loadPreviousReviews = async () => {
  let reviews = await fetchReviews();
  let reviewContainer = document.querySelector("#previous-reviews");

  for (let review of reviews) {
    let trip = await fetchTripDetails(review.tripId);

  reviewContainer.innerHTML += `<div class= "bg-white p-4 shadow rounded-lg mb-4"> <p class="text-lg font-semibold">Rating: ${"⭐".repeat(
    review.rating
  )}</p>
            <p class="text-gray-600">"${review.comment}"</p>
            
            <button class="bg-blue-500 text-white px-3 py-1 rounded mt-2 toggle-trip-details">
                View Trip Details
            </button>
            
            <div class="trip-details hidden mt-2 p-4 border rounded bg-gray-100">
                <p><strong>From:</strong> ${trip.startingLocation}</p>
                <p><strong>To:</strong> ${trip.destination}</p>
                <p><strong>Date:</strong> ${new Date(
                  trip.departureDate
                ).toLocaleDateString()}</p>
                <p><strong>Price:</strong> $${trip.price}</p>
            </div>
        `;

  }

  // Add toggle event to each button
  document.querySelectorAll(".toggle-trip-details").forEach((button) => {
    button.addEventListener("click", function () {
      this.nextElementSibling.classList.toggle("hidden"); // Toggle visibility
    });
  });
};

// Load reviews when the page loads
document.addEventListener("DOMContentLoaded", loadPreviousReviews);