
  const loginForm = document.getElementById("loginForm");
  const registerForm = document.getElementById("registerForm");
  const loginTab = document.getElementById("loginTab");
  const registerTab = document.getElementById("registerTab");

  // Toggle Forms
  loginTab.addEventListener("click", function () {
   toggleLogin();
  });

  registerTab.addEventListener("click", function () {
    registerForm.classList.remove("hidden");
    loginForm.classList.add("hidden");
    registerTab.classList.add("border-blue-600", "text-black");
    loginTab.classList.remove("border-blue-600", "text-black");
    loginTab.classList.add("text-gray-500");
  });

  let toggleLogin = () => 
  {
    loginForm.classList.remove("hidden");
    registerForm.classList.add("hidden");
    loginTab.classList.add("border-blue-600", "text-black");
    registerTab.classList.remove("border-blue-600", "text-black");
    registerTab.classList.add("text-gray-500");
  }
  function isValidEmail(email) {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
  }

  function showError(input, message) {
    let existingError = input.nextElementSibling;
    if (existingError && existingError.classList.contains("error-text")) {
      existingError.remove();
    }
    input.insertAdjacentHTML("afterend",`<p class="error-text text-red-500 text-sm mt-1">${message}</p>`);
  }

  function clearError(input) {
    let error = input.nextElementSibling;
    if (error && error.classList.contains("error-text")) {
      error.remove();
    }
  }

  loginForm.addEventListener("submit", async function (event) {
    event.preventDefault();
    const email = document.getElementById("email");
    const password = document.getElementById("password");
    let isValid = loginValidation(email,password);
    if (isValid) {
      let loginData = {
        email: email.value,
        password: password.value,
      }
      await login(loginData);
      let token = localStorage.getItem("userToken")
      console.log(token)
      if (token) {
        const user = jwt_decode(token);
        let userRole = user["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
        console.log(userRole)
        if (userRole === "Admin") {
          window.location.href = "/Admin/adminDashBoard.html";
        } else if (userRole === "Customer") {
          window.location.href = "/CustomerDashboard.html";
        }
        else if(userRole == "Driver")
        {
          window.location.href = "/Driver/DriverDashboard.html";
        }
      }
      loginForm.reset();
    }
  });
  let login = async (loginData) => {
  await fetch("http://localhost:5041/api", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(loginData),
  }
)
    .then(async (res) => await res.json())
    .then((response) => {
      console.log(response.token)
      localStorage.removeItem("userToken");
      localStorage.setItem("userToken",response.token);
      // localStorage.setItem("response", JSON.stringify(response.token));
      console.log(response.token);
    })
    .catch((error) => {
      alert("Error: " + error);
    });
  }

  let loginValidation = (email,password) =>
{
      
    let isValid = true;

    // if (!email.value.trim()) {
    //   showError(email, "Email is required");
    //   isValid = false;
    // } else if (!isValidEmail(email.value)) {
    //   showError(email, "Invalid email format");
    //   isValid = false;
    // } else {
    //   clearError(email);
    // }

    // if (!password.value.trim()) {
    //   showError(password, "Password is required");
    //   isValid = false;
    // } else if (password.value.length < 6) {
    //   showError(password, "Password must be at least 6 characters");
    //   isValid = false;
    // } else {
    //   clearError(password);
    // }

    return isValid;
} 
  // Register Form Validation
  registerForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let isValid = registrationValidation();
    if (isValid) {
      alert("Registration successful!");
      let firsName = document.querySelector("#firstName");
      let lastName = document.querySelector("#lastName");
      let middleName = document.querySelector("#middleName");
      let password = document.querySelector("#regPassword");
      let address = document.querySelector("#address");
      let gender = document.querySelector("#gender");
      let phoneNum = document.querySelector("#phoneNumber");
      let email = document.querySelector("#regEmail");
      let eventForm = document.querySelector("#eventForm");
      let registerData = {
        firstName: firsName.value,
        lastName: lastName.value,
        middleName: middleName.value,
        email: email.value,
        password: password.value,
        gender: parseInt(gender.value),
        address: address.value,
        phoneNumber: phoneNum.value,
      };
      console.log(registerData)
      postEvent(registerData);
      registerForm.reset();
      toggleLogin();
   }
  });
  let postEvent = (eventBody) => {
    fetch("http://localhost:5041/api/Customer/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
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

  let registrationValidation = () => 
  {
     const inputs = registerForm.querySelectorAll("input, select");
     let isValid = true;

     inputs.forEach((input) => {
       clearError(input);
       if (!input.value.trim()) {
         showError(
           input,
           `${input.previousElementSibling.innerText} is required`
         );
         isValid = false;
       }
     });

     const email = registerForm.querySelector("input[type='email']");
     if (email && !isValidEmail(email.value)) {
       showError(email, "Invalid email format");
       isValid = false;
     }

     const password = registerForm.querySelector("input[type='password']");
     if (password && password.value.length < 6) {
       showError(password, "Password must be at least 6 characters");
       isValid = false;
     }

    return isValid;
     
  }
