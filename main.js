document.addEventListener("DOMContentLoaded", function() {
    const showLoginBtn = document.querySelector(".login-btn");
    const showlogin_againBtn = document.querySelector(".loginbtn");
    const loginModal = document.getElementById("loginModal");

    const hideMenuBtn = loginModal.querySelector(".close-button");
    const showSignupBtn = document.getElementById("signup-btn");
    const signupModal = document.getElementById("signupModal");

    const forgotPasswordLink = document.getElementById("fotgotpwlink");
    const forgotModal = document.getElementById("forgotModal");
    const clickforgotBtn = document.getElementById("fgtpwd");
    const forgotform = document.getElementById("forgotForm");
    //



    
    if (showSignupBtn) {
        showSignupBtn.addEventListener("click", function() {
            if (loginModal) {
                loginModal.style.display = "none";
            }
    
            if (signupModal) {
                signupModal.style.display = "block";
            }
        });
    }
   if(showlogin_againBtn)
    {
        showlogin_againBtn.addEventListener("click", function() {
           
            if (signupModal) {
                signupModal.style.display = "none";
            }
    
            if (loginModal) {
                loginModal.style.display = "block";
            }
        });
    }
    if (forgotPasswordLink) {
        forgotPasswordLink.addEventListener("click", function() {
            if (loginModal) {
                loginModal.style.display = "none";
            }
    
            if (forgotModal) {
                forgotModal.style.display = "block";
            }
        });
    }

    
    showSignupBtn.addEventListener("click", function() {
        signupModal.style.display = "block";
        signupForm.reset();
    });

    // showLoginBtn.addEventListener("click", function() {
    //     loginModal.style.display = "block";
    //     loginform.reset();
    // });
    

    hideMenuBtn.addEventListener("click", function() {
        loginModal.style.display = "none";
        
    });

    clickforgotBtn.addEventListener("click",function()
    {
        loginModal.style.display = "block";
    })

    window.addEventListener("click", function(event) {
        if (event.target == loginModal) {
            loginModal.style.display = "none";
        }
        if (event.target == signupModal) {
            signupModal.style.display = "none";
        }
        if (event.target == forgotModal) {
            forgotModal.style.display = "none";
        }
    });

   

//log in
    const usersUrl = 'http://localhost:3000/users';
    const adminUrl='http://localhost:3000/admin';
    let currentUser = null;

    const loginform=document.getElementById("loginForm");
    document.getElementById("login").addEventListener("click",login);


    const profileSection = document.getElementById('profileSection');
const currentUserName = document.getElementById('currentUserName');
const profileIcon = document.getElementById('profileIcon');
const profileDropdown = document.getElementById('profileDropdown');
const logoutButton = document.getElementById('logoutButton');

    function login() {
        const username = document.getElementById('usernameLogin').value.trim();
        const password = document.getElementById('passwordLogin').value.trim();
        clearErrors();

        if (!username) {
            showError("usernameLogin", "Username can't be blank");
            return;
        }
        if (!password) {
            showError("passwordLogin", "Password can't be blank");
            return;
        }


        const loggedInUser = localStorage.getItem('loggedInUser');
        if (loggedInUser) {
            const user = JSON.parse(loggedInUser);
            showUserProfile(user.name);
        }
    

        // const response =fetch(`http://localhost:3000/users?name=${username}`);
        

        fetch(`${usersUrl}?name=${username}`)
            .then(response => response.json())
            .then(users => {
       // const user = users.find(u => u.name === username && u.password === password);
        if(users.length>0 && users[0].password===password)
        {
            const user = users[0];
            localStorage.setItem('loggedInUser', JSON.stringify(user));
            showUserProfile(user.name);
            alert('Login successful!');
            document.getElementById('loginForm').reset();
            window.location.href = "index.html";
        } else {
            alert('Invalid username or password.');
            loginform.reset();
        }
        })
        // fetch(usersUrl)
        //     .then(response => response.json())
        //     .then(users => {
        //         const user = users.find(u => u.name === username && u.password === password);

                // if (user) {
                //     currentUser = user;
                //     alert('Login successful!');
                //     document.getElementById('loginForm').reset();
                //     window.location.href = "Customer/CustomerDashboard.html";
                // } else {
                //     alert('Invalid username or password.');
                //     loginform.reset();
                // }
    //         });
    // }

// show username

function showUserProfile(username) {
    showLoginBtn.style.display = 'none';
    profileSection.style.display = 'flex';
    currentUserName.textContent = username;
}

profileIcon.addEventListener('click', function() {
    profileDropdown.style.display = profileDropdown.style.display === 'none' ? 'block' : 'none';
});

logoutButton.addEventListener('click', function() {
    localStorage.removeItem('loggedInUser');
    loginButton.style.display = 'inline-block';
    profileSection.style.display = 'none';
    profileDropdown.style.display = 'none';
    console.log("Logged out successfully.");
    window.location.href = "index.html";
});



// signup
const signupForm =document.getElementById("signupForm");
document.getElementById("signup").addEventListener("click",signup)
function signup() {
    const username = document.getElementById('usernameSignup').value.trim();
    const password = document.getElementById('passwordSignup').value.trim();
    const confirmPassword  = document.getElementById('confirmpassword').value.trim();
    const phoneno  = document.getElementById('phone').value.trim();
    const address  = document.getElementById('address').value.trim();

     clearErrors();
    if (!username) {
        showError("usernameSignup", "Name can't be blank");
        return;
    }
    if (!phoneno) {
        showError("phone", "Phone no can't be blank");
        return;
    }
    if (!address) {
        showError("address", "Address can't be blank");
        return;
    }
    if (!password) {
        showError("passwordSignup", "Password can't be blank");
        return;
    }
    if (!confirmPassword) {
        showError("confirmpassword", "Confirm Password can't be blank");
        return;
    } 
    else if (password !== confirmPassword) {
        showError("confirmpassword", "Passwords do not match");
        return;
    }

    const customerData = {
        id: generateId(),
        name: username,
        phone:phoneno,
        address:address,
        password: password,
        
    };

    fetch("http://localhost:3000/users", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(customerData)
    })
    .then(response => response.json())
    .then(data => {
        alert("Signup successful!");
        window.location.href = "index.html";
        signupForm.reset(); 
    })
    .catch(error => {
        console.error("Error:", error);
        alert("There was an error. Please try again.");
        signupForm.reset();
    });

}

//forgot password
forgotPasswordLink.addEventListener("click", function(event) 
{
    event.preventDefault();
    forgotModal.style.display = "block";
});

const closeButton = forgotModal.querySelector(".close-button");
closeButton.addEventListener("click", function()
{
    forgotModal.style.display = "none";
    loginModal.style.display="block";
});

const singupcloseButton = signupModal.querySelector(".close-button");
singupcloseButton.addEventListener("click", function()
{
    signupModal.style.display = "none";
    loginModal.style.display="block";
});


window.addEventListener("click", function(event) {
    if (event.target === forgotModal) {
        forgotModal.style.display = "block";
        
    }

    if (event.target === loginModal) {
        loginModal.style.display = "block";
    }
    if (event.target === signupModal) {
        signupModal.style.display = "block";
    }
});

document.getElementById("forgotForm").addEventListener("click", function(e) {
    e.preventDefault();
   
    const username = document.getElementById("username").value.trim();
    const newPassword = document.getElementById("forgotpassword").value.trim();
    const confirmPassword = document.getElementById("forgotcfpassword").value.trim();
    clearErrors();

    // Validate inputs
    if (username === "") {
        showError("username", "Username can't be blank");
        return;
    }
    if (newPassword === "") {
        showError("forgotpassword", "Password can't be blank");
        return;
    }
    if (confirmPassword === "") {
        showError("forgotcfpassword", "Confirmed Password can't be blank");
        return;
    }
    if (newPassword !== confirmPassword) {
        showError("forgotcfpassword", "Passwords do not match");
        return;
    }

    // Fetch user data from the JSON server
    fetch(`http://localhost:3000/users`)
        .then(response => response.json())
        .then(users => {
            if (users.length > 0) {
                // User found, update the password
                const user = users[0];
                updateUserPassword(user.id, newPassword);
            } else {
                showError("username", "Username not found");
                forgotform.reset();
            }
        })
        .catch(error => console.error('Error:', error));
});

function updateUserPassword(userId, newPassword) {
    fetch(`http://localhost:3000/users/${userId}`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ password: newPassword })
    })
    .then(response => {
        if (response.ok) {
            alert("Password updated successfully!");
            document.getElementById("forgotModal").style.display = "none";
            document.getElementById("forgotForm").reset();
        } else {
            alert("Failed to update password. Please try again.");
            forgotform.reset();
        }
    })
    .catch(error => console.error('Error:', error));
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





// Generate a unique ID for the customer
function generateId() {
    fetch(usersUrl)
    .then(response => response.json())
    .then(users => {
        if (users.length > 0) 
        {
            const lastUser=users[users.length-1];
            let customerIdCounter = parseInt(lastUser.id.split('_')[1], 10);
            customerIdCounter += 1; 
            return `cus_${customerIdCounter}`;
        }
        else{
            customerIdCounter=0;
            return `cus_${customerIdCounter}`;
        }
        
    })
}

// admin redirect admin panel
const currentUrl = window.location.href;
if (currentUrl.includes("/admin")) {
    window.location.href = "admin.html";
}

// // load data from the json

// // const apiUrl= "http://localhost:3000/dvds";
// //     const dvdContainer = document.getElementById('dvd-container');
  
//     fetch(`${apiUrl}`)
//         .then(response => response.json())
//         .then(data => {
//             data.forEach(dvd => {
//                 const productCard = document.createElement('div');
//                 productCard.className = 'product-card';

//                 productCard.innerHTML = `
//                     <img src="${dvd.image}" alt="${dvd.title}" class="product-img">
//                     <div class="card-info">
//                         <div class="card-info-content">
//                             <p class="location-name"><li>${dvd.title}</li></p>
//                         </div>
//                         <div class="card-info-content">
//                             <div class="content">
//                                 <p>${dvd.genre}</p>
//                             </div>
//                             <div class="content">
//                                 <p>${dvd.director}</p>
//                             </div>
//                             <div class="content">
//                                 <p>${dvd.releaseDate}</p>
//                             </div>
//                         </div>
//                         <div class="price">
//                             <p>Rs.${dvd.price}</p>
//                             <a href="#" class="booking-btn">Rent Now</a>
//                         </div>
//                     </div>
//                 `;

//                 dvdContainer.appendChild(productCard);
//             });
//         })
//         .catch(error => console.error('Error loading DVD data:', error));

 
// modal
    const modal = document.getElementById('rentModal');
    const closeBtn = document.querySelector('.close-btn');
    const dvdTitleInput = document.getElementById('dvdTitle');
    const priceInput = document.getElementById('price');

    const checkinUrl="http://localhost:3000/checklogin";
    // Function to check if user is logged in
    function checkLoginStatus() {
        return fetch(`${checkinUrl}`)  
            .then(response => response.json())
            .then(data => data.isLoggedIn);
    }

    document.querySelectorAll('.booking-btn').forEach(button => {
        button.addEventListener('click', (e) => {
            e.preventDefault();

            checkLoginStatus().then(isLoggedIn => {
                if (!isLoggedIn) {
                    // window.location.href = 'login.html';
                    loginModal.style.display="block";
                    return;
                }

                const productCard = e.target.closest('.product-card');
                const dvdTitle = productCard.querySelector('.location-name li').textContent;
                const price = productCard.querySelector('.price p').textContent.replace('Rs.', '');

                dvdTitleInput.value = dvdTitle;
                priceInput.value = price;

                modal.style.display = 'flex';
            });
        });
    });

    closeBtn.addEventListener('click', () => {
        modal.style.display = 'none';
    });

    window.addEventListener('click', (e) => {
        if (e.target == modal) {
            modal.style.display = 'none';
        }
    });

    document.getElementById('rentForm').addEventListener('submit', (e) => {
        e.preventDefault();

        const dvdTitle = dvdTitleInput.value;
        const customerName = document.getElementById('customerName').value;
        const price = priceInput.value;

        // Notify admin (this part can be expanded with actual functionality)
        alert(`Request sent to admin for:\nDVD: ${dvdTitle}\nCustomer: ${customerName}\nPrice: Rs.${price}\nInitial Payment: Rs.300 credited.`);

        // Close modal after submission
        modal.style.display = 'none';
    });

// list show background remove
// const listofdvds=this.document.getElementById('movielist').addEventListener('click',(events)=>
// {
//     events.preventDefault();
//     const backimg=document.querySelector('background-image').style.display="none";

// })


// const loggedInUser = localStorage.getItem('loggedInUser');
//     if (loggedInUser) {
//         const user = JSON.parse(loggedInUser);
//         showUserProfile(user.name);
//     }




    }
});

