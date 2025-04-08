export let logout = () => {
  document.querySelector("#Logout").addEventListener("click", () => {
    localStorage.removeItem("userToken");
    window.location.href = "/Auth/Auth.html";
  });
};
logout();
