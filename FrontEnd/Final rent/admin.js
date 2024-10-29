document.addEventListener('DOMContentLoaded', () => {

    const showLoginBtn = document.getElementById("login");
    const loginModal = document.getElementById("loginModal");

    const adminUrl = 'http://localhost:3000/admin';
    let currentUser = null;

    const loginform=document.getElementById("loginForm");
    document.getElementById("login").addEventListener("click",login);

    function login() {
        const adminid = document.getElementById('AdminId').value.trim();
        const adminname = document.getElementById('Adminusername').value.trim();
        const adminpassword = document.getElementById('Adminpassword').value.trim();

       
        if (adminid==="") {
            showError("AdminId", "Admin ID can't be blank");
            return;
        } 
        if (adminname==="") {
            showError("Adminusername", "Username can't be blank");
            return;
        } 
        if (adminpassword==="") {
            showError("Adminpassword", "Password can't be blank");
            return;
        }

        fetch(adminUrl)
            .then(response => response.json())
            // .then(users => {
            //     const user = users.find(u => u.id === adminid && u.username === adminname && u.password === adminpassword);
            //     console.log(users.adminid,users.adminname,users.adminpassword);
                .then(users => {
                    if(users.length>0 && users[0].name===adminname && users[0].password===adminpassword)
                        {
                            const user = users[0];
                    // const user = users.find(u =>u.id===adminid && u.username === adminname && u.password === adminpassword)
                    // {
                        // if (users) {
                    currentUser = user;
                    alert('Admin Login successful!');
                    document.getElementById('loginForm').reset();
                    
                    window.location.href = "Admin/AdminDashboard.html";
                } else {
                    alert('Invalid username or password.');
                } 
              
            });
        }
    
function showError(inputId, message) {
    const inputElement = document.getElementById(inputId);
    const errorText = inputElement.parentElement.nextElementSibling;
    errorText.textContent = message;
    errorText.style.display = "block";
}

function clearErrors() {
    const errorTexts = document.querySelectorAll(".error-txt");
    errorTexts.forEach(errorText => {
        errorText.style.display = "none";
    });
}

});