"use strict"

// Select DOM Elements
const passwordInput = document.querySelector(".password-input");
const showPasswordBtn = document.querySelector(".show-password");

// Toggle password visibility
const togglePasswordVisibility = function (e) {
    e.preventDefault();
    if (passwordInput.type === "password") {
        passwordInput.type = "text"
    } else {
        passwordInput.type = 'password';
    }
}

showPasswordBtn.addEventListener("click", togglePasswordVisibility);

