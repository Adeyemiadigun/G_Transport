// Function to get query parameters
function getQueryParam(param) {
  const urlParams = new URLSearchParams(window.location.search);
  return urlParams.get(param);
}

// Extract reference from URL
const reference = getQueryParam("reference");
console.log(reference)
if (!reference) {
  console.log("I entered if   " + reference)
  alert("Invalid payment reference. Please try again.");
  window.location.href = "/Customer/BookingPage.html"; // Redirect back to booking page
} else {
  console.log("I entered here    "+ reference)
  // Call backend to verify payment
  fetch(`https://localhost:7156/api/Payment/verify?reference=${reference}`)
    .then((response) => response.json())
    .then((data) => {
      if (data.status) {
        // ✅ Store payment data in localStorage
        localStorage.setItem("paymentData", JSON.stringify(data.data));

        alert("Payment successful! Redirecting to booking page...");
        window.location.href = "/Customer/BookingPage.html"; // Redirect to booking page
      } else {
        alert("Payment verification failed! Please try again.");
        window.location.href = "/Customer/BookingPage.html"; // Redirect to booking page
      }
    })
    .catch((error) => {
      console.error("Error verifying payment:", error);
      alert("Error verifying payment. Try again.");
      window.location.href = "/Customer/BookingPage.html"; // Redirect to booking page
    });
}
