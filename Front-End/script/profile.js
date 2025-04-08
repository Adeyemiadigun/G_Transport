import { logout } from "./logout.js";
logout();

document.addEventListener("DOMContentLoaded", async () => {
  const token = localStorage.getItem("userToken");

  try {
    const res = await fetch(
      "https://localhost:7156/api/Customer/profile",
      {
        method: "GET",
        headers: {
          Authorization: "Bearer " + token,
          "Content-Type": "application/json",
        },
      }
    );

    const result = await res.json();
    console.log(result);
    if (result.status) {
      console.log(result.data);
      const data = result.data;
      document.getElementById("customerId").value = data.id;
      document.getElementById("firstName").value = data.firstName;
      document.getElementById("middleName").value = data.middleName;
      document.getElementById("lastName").value = data.lastName;
      document.getElementById("email").value = data.email;
      document.getElementById("phoneNumber").value = data.phoneNumber;
      document.getElementById("gender").value = data.gender == 1? "Male" : "Female";
    } else {
      alert("Failed to load profile: " + result.message);
    }
  } catch (error) {
    alert("Error fetching profile: " + error);
  }
});

document
  .getElementById("settingsForm")
  .addEventListener("submit", async (e) => {
    e.preventDefault();

    const updatedProfile = {
      customerId: document.getElementById("customerId").value,
      firstName: document.getElementById("firstName").value,
      middleName: document.getElementById("middleName").value,
      lastName: document.getElementById("lastName").value,
    };
    try {
      const res = await fetch("http://localhost:5041/api/Profile/update", {
        method: "PUT", // adjust method if your backend uses POST
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("userToken"),
        },
        body: JSON.stringify(updatedProfile),
      });

      const result = await res.json();
      alert(result.message);
    } catch (error) {
      alert("Error updating profile: " + error);
    }
  });
