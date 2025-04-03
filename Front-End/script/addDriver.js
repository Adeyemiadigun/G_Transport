let firsName = document.querySelector("#firstName");
let lastName = document.querySelector("#lastName");
let middleName = document.querySelector("#middleName");
let password = document.querySelector("#password");
let address = document.querySelector("#address");
let gend= document.querySelector("#gender");
let phoneNum = document.querySelector("#phoneNumber");
let email = document.querySelector("#email");
let submitBtn = document.querySelector("#submitBtn");
let errorMessages = [];

// Form validation
submitBtn.addEventListener("click", (e) => {
  e.preventDefault();
  let validation = formValidation();
  if (!validation) {
    alert(errorMessages.join("\n"));
    errorMessages = [];	
  } 
  else
  {
    console.log(gend.value)
    console.log(gend)
    let formData = {
      firstName: firsName.value,
      lastName: lastName.value,
      middleName: middleName.value,
      gender: Number(gend.value),
      email: email.value,
      password: password.value,
      address: address.value,
      phoneNumber: phoneNum.value,
    };
    postEvent(formData);
    console.log(formData);
  }
});
let postEvent = (eventBody) => {
  fetch("http://localhost:5041/api/Driver/create", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer " + localStorage.getItem("userToken"),
    },
    body: JSON.stringify(eventBody),
  })
    .then((res) => res.json())
    .then((response) => {
      console.log(response);
      alert(response.message);
    })
    .catch((error) => {
      alert(error);
    });
};
let formValidation = () =>
{
  let isValid = true;
  console.log("entered here")
  let nameRegex = /^[A-Za-z]+$/;
  let phoneRegex = /^(?:\+234|234|0)[789][01]\d{8}$/;

  let emailRegex = /^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/;
  let passwordRegex =
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

  if (!firstName.value.match(nameRegex)) {
    isValid = false;
    errorMessages.push("First name must contain only letters.");
  }

  if (!lastName.value.match(nameRegex)) {
    isValid = false;
    errorMessages.push("Last name must contain only letters.");
  }

  if (middleName.value && !middleName.value.match(nameRegex)) {
    isValid = false;
    errorMessages.push("Middle name must contain only letters.");
  }

  if (!phoneNum.value.match(phoneRegex)) {
    isValid = false;
    errorMessages.push(
      "Phone number must be in the format: +234 ."
    );
  }

  if (!email.value.match(emailRegex)) {
    isValid = false;
    errorMessages.push("Invalid email format.");
  }

  if (!password.value.match(passwordRegex)) {
    isValid = false;
    errorMessages.push(
      "Password must be at least 8 characters, include uppercase, lowercase, a number, and a special character."
    );
  }

  if (address.value.trim() === "") {
    isValid = false;
    errorMessages.push("Address field cannot be empty.");
  }

  // if (!gender.value.match(genderRegex)) {
  //   isValid = false;
  //   errorMessages.push("Select a valid gender: Male, Female, or Other.");
  // }
  return isValid;
}
let viewDriver = () => {
//  console.log(jwt_decode(localStorage.getItem("userToken")));
fetch("https://localhost:7156/api/Driver/all?PageSize=10&CurrentPage=1", {
  method: "GET",
  headers: {
    "Content-Type": "application/json",
    Authorization: `Bearer ${localStorage.getItem("userToken")}`,
  },
})
  .then((response) => response.json())
  .then((data) => {
    console.log(data);
     if (data.data == null) {
       console.log(true);
       tripList.innerHTML = `<tr class="m-4"><td colspan="9" class="text-center font-bold text-4xl ">No driver found.</td></tr>`;
       return;
     }
    if (data.status) {
      console.log("data.data:", data.data);
    console.log("data.data.items:", data.data.items);
      let tBody = document.querySelector("#tbody");
      data.data.items.$values.forEach((item) => {
        tBody.innerHTML += `<tr class="border-b">
        <td class="p-2">${item.firstName} ${item.lastName}</td>
        <td class="p-2">${item.email}</td>
        <td class="p-2">${item.gender==1 ? "Male" :"Female" }</td>
        <td class="p-2">${item.driverNo}</td>
        <td class="p-2">${item.phoneNumber}</td>
        <td class="p-2">${item.address || "No record"}</td>
        <td class="p-2">
        <button class="text-blue-500 mr-2">Edit</button>
        <button class="text-red-500">Delete</button>
        </td>
        </tr>`;
      });
    } else {
      console.log("No recent driver found");
    }
  })
  .catch((error) => console.error("Error:", error));
}
viewDriver();

function openModal() {
  document.getElementById("driverModal").classList.remove("hidden");
}
function closeModal() {
  document.getElementById("driverModal").classList.add("hidden");
}