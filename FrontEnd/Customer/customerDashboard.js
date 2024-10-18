document.addEventListener("DOMContentLoaded", function(e) {
  e.preventDefault();
  const showdvdBtn = document.getElementById("add-dvd-btn");
  const dvdModal = document.getElementById("dvdModal");
  const closeModalBtn = document.getElementById("closeModalBtn");
  const modalTitle=document.getElementById("modal-title");
  const modalForm=document.getElementById("modal-form");
  const modal = document.getElementById('modal');
  const notificationTable=document.getElementById('uerRequestTable');


// edit
function isLoggedIn() {
  return localStorage.getItem('loggedInUser') !== null;
}
function getCurrentUser()
{
  return JSON.parse(localStorage.getItem('loggedInUser'));
}

        const loggedInUser = localStorage.getItem('loggedInUser');
        if (!loggedInUser) {
          alert('You need to log in first.');
          window.location.href = 'movielist.html'; 
          return;
        
        }
            const activeuser = JSON.parse(localStorage.getItem('loggedInUser'));
        
  
 

 
  fetch(`https://localhost:7111/api/Customer/GET_CUSTOMER${activeuser.id}`)
      .then(response => response.json())
      .then(user => {
          document.getElementById('username').value = activeuser.name;
          document.getElementById('phone').value = activeuser.phone;
          document.getElementById('address').value = activeuser.address;
          document.getElementById('updatePassword').value = activeuser.password;
          // document.getElementById('confirmPassword').value = user.phone;
      })
      .catch(error => {
          console.error('Error fetching user data:', error);
          alert('Failed to load user data.');
      });
    
  document.getElementById('editProfileForm').addEventListener('submit', function(event) {
      event.preventDefault();
      const updatedUsername = document.getElementById('username').value;
      const updatedphone= document.getElementById('phone').value;
      const updatedAddress = document.getElementById('address').value;
      const updatedPassword = document.getElementById('updatePassword').value;
      const confirmPassword = document.getElementById('confirmPassword').value;

      if (updatedPassword && updatedPassword !== confirmPassword) {
          alert('Passwords do not match.');
          return;
      }
      const updatedUserData = {
          name: updatedUsername,
          phone: updatedphone,
          address: updatedAddress,
          ...(updatedPassword && { password: updatedPassword }) 
      };

      fetch(`https://localhost:7111/api/Customer/UPDATE_CUSTOMER${activeuser.id}`, {
          method: 'PUT',
          headers: {
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(updatedUserData)
      })
      .then(response => response.json())
      .then(data => {
          alert('Profile updated successfully!');
         
          localStorage.setItem('loggedInUser', data.username);
          
          // window.location.href = 'Customer/CustomerDashboard.html';
      })
      .catch(error => {
          console.error('Error updating profile:', error);
          alert('Failed to update profile.');
      });
  });
 
// request
fetchNotifications();
fetchRental();
function fetchNotifications() {
  fetch(`http://localhost:3000/adminNotification?user=${activeuser.name}`)
      .then(response => response.json())
      .then(data => {
        const userRequestsTableBody = document.getElementById('userRequestsTableBody');
        if(userRequestsTableBody)
        {
          userRequestsTableBody.innerHTML = '';
            
              if (data.length === 0) {
                userRequestsTableBody.innerHTML = '<tr><td colspan="5">No rent requests found.</td></tr>';
            } else {
              data.forEach(notification=>{
              const row = document.createElement('tr');
              row.innerHTML = `
                  <td>${notification.id}</td>
                  <td>${notification.user}</td>
                  <td>${notification.dvdName}</td>
                  <td>${notification.status}</td>
                  <td>${new Date(notification.date).toLocaleDateString()}</td>
                  
              `;

              userRequestsTableBody.appendChild(row);
          });
        }
      }
      })
      .catch(error => console.error('Error fetching notifications:', error));
}

// rent
function fetchRental() {
  fetch(`http://localhost:3000/rentals?customerId=${activeuser.id}`)
      .then(response => response.json())
      .then(data => {
        const userRentsTableBody = document.getElementById('userRentTableBody');
        if(userRentsTableBody)
        {
          userRentsTableBody.innerHTML = '';
            
              if (data.length === 0) {
                userRentsTableBody.innerHTML = '<tr><td colspan="5">No rent requests found.</td></tr>';
            } else {
              data.forEach(rent=>{
              const row = document.createElement('tr');
              row.innerHTML = `
                  <td>${rent.id}</td>
                  <td>${rent.customerId}</td> 
                  <td>${rent.dvdId}</td>
                  <td>${rent.rentDate}</td>
                  <td>${rent.dueDate}</td>
                  <td>${rent.returnDate}</td>
                  <td>${rent.payAction}</td>                 
              `;

              userRentsTableBody.appendChild(row);
          });
        }
      }
      })
      .catch(error => console.error('Error fetching notifications:', error));
}

// });
//document.addEventListener('DOMContentLoaded', fetchNotifications);

      const apiUrl = 'http://localhost:3000/dvds';
      // const categoriesApiUrl = 'http://localhost:3000/categories';


// window.onload =() => {
// };


// -----------------------------------------Alert and nav click------------------------------------------
let alertTimeout;
function logout() 
{
    showAlert();
}

function showAlert() {
    document.getElementById('alertMessage').textContent = 'Logout Successfully!';
    document.getElementById('customAlert').style.display = 'block';
    document.getElementById('customAlert').classList.add('fade-in');
    document.getElementById('content').classList.add('blur-background');
    clearTimeout(alertTimeout);
}

function hideAlert() 
{
    document.getElementById('customAlert').style.display = 'none';
    document.getElementById('content').classList.remove('blur-background');
    window.location.href = "../index.html";
}



document.querySelectorAll(".navList").forEach(function(element) {
    element.addEventListener('click', function() {
      
      document.querySelectorAll(".navList").forEach(function(e) {
        e.classList.remove('active');
    });

      this.classList.add('active');
      var index = Array.from(this.parentNode.children).indexOf(this);

      document.querySelectorAll(".data-table").forEach(function(table) {
        table.style.display = 'none';
      });
      // if(index==0)
      //   {
      //     document.querySelector('.overview').style.display = 'block';

      //   }
      //   else
      //   {
      //     document.querySelector('.overview').style.display = 'none';
      //   }
      var tables = document.querySelectorAll(".data-table");
     
      if (tables.length > index) {
        tables[index].style.display = 'block';
       
      }
    });
  });

});