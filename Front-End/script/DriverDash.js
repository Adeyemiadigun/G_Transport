document
  .getElementById("completeTripBtn")
  .addEventListener("click", function () {
    alert("Trip marked as completed!");
    this.classList.add("bg-gray-400");
    this.classList.remove("bg-green-500");
    this.textContent = "Completed";
    this.disabled = true;
  });

  let assigenTrip = document.querySelector("#assignedTrip");

  let fetchTrip = async () => 
  {
    let response = await fetch("/api/trip/assign");
    let data = await response.json();
    return data;
  }