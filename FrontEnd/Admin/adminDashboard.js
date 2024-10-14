document.addEventListener("DOMContentLoaded", function() {
  const showdvdBtn = document.getElementById("add-dvd-btn");
  const dvdModal = document.getElementById("dvdModal");
  const closeModalBtn = document.getElementById("closeModalBtn");
  const modalTitle=document.getElementById('modal-title');
  const modalForm=document.getElementById("modal-form");
  const modal = document.getElementById('modal');
  
  const dvdIdInput = document.getElementById('dvdId');
  const submitBtn = document.getElementById("submit-btn");
  const editbtn = document.getElementById("edit-btn");

  
  const dropdownption=document.getElementById('dropdown');
  const searchinput=document.getElementById('searchtxt');
  const searchbtn=document.getElementById('search-dvd-btn');
  const dvdTableBody = document.querySelector('#dvdTable tbody');
  
  const apiUrl = 'http://localhost:3000/dvds';
  const categoriesApiUrl = 'http://localhost:3000/categories';
  const usersUrl="http://localhost:3000/users";
  const rentsUrl="http://localhost:3000/rentals";

  if (showdvdBtn && dvdModal) {
      showdvdBtn.addEventListener("click", function() {
          dvdModal.style.display = "block";
      });
  } 
  else {
      console.error("One or more elements not found");
  }

  if (closeModalBtn) {
      closeModalBtn.addEventListener("click", function() {
          dvdModal.style.display = "none";
      });
  }


 //total counts box1
 async function updateTotalUsers(usersUrl) {
    const totalUsersElement = document.getElementById("totalUsers");
    try {
        
        const response=await fetch(usersUrl);
        const users=await response.json();
        if (users) 
            {
            const totalUsers = users.length;
            totalUsersElement.textContent = totalUsers;
        } 
        else 
        {
            console.error("No users data");
        }
    } catch (error) {
        console.error("Failed to count users:", error);
    }
}

async function updateTotalDVDs(apiUrl) {
    const totalDvdsElement = document.getElementById("totalDVDs");
    try {
        const response=await fetch(apiUrl);
        const dvds = await response.json();
        if (dvds) 
            {
            const totaldvds =dvds.length;
            totalDvdsElement.textContent = totaldvds;
        } 
        else 
        {
            console.error("No dvds data");
        }
    } catch (error) {
        console.error("Failed to count dvds:", error);
    }
}

//total count of rent box3
async function updateTotalRents(rentsUrl) {
    const totalRentsElement = document.getElementById("totalrents");
    try {
        
        const response=await fetch(rentsUrl);
        const rents=await response.json();
        if (rents) 
            {
            const totalRents = rents.length;
            totalRentsElement.textContent = totalRents;
        } 
        else 
        {
            console.error("No users data");
        }
    } catch (error) {
        console.error("Failed to count users:", error);
    }
}


updateTotalUsers(usersUrl);
updateTotalDVDs(apiUrl);
updateTotalRents(rentsUrl);
fetchCategories();
    fetchDVDs();
    fetchCustomers();
    fetchNotification();
    fetchRentalHistory();


dvdTableBody.addEventListener('click', (event) => {
    if (event.target.classList.contains('delete-btn')) {
        const dvdId = event.target.getAttribute('data-id');
        const confirmDelete = confirm("Are you sure you want to delete this DVD?");

        if (confirmDelete) {
            removeDVD(dvdId);
        }

    }
});

//search button
searchbtn.addEventListener('click', () => {
    const category = dropdownption.value;
    const searchText = searchinput.value.trim();
  
    fetchDvdData().then(dvds => {
        const filteredDvds = filterDvds(dvds, category, searchText);
        filterDvdTable(filteredDvds);
    });
  });

  function fetchDvdData() {
    return fetch(apiUrl)
        .then(response => response.json());
}

fetchDvdData().then(dvds => {
    filterDvdTable(dvds);
});

function fetchCategories() {
    fetch(categoriesApiUrl)
        .then(response => response.json())
        .then(data => {
            const genreSelect = document.getElementById('genre');
            genreSelect.innerHTML = '';
            data.forEach(category => {
                const option = document.createElement('option');
                option.value = category.name;
                option.textContent = category.name;
                genreSelect.appendChild(option);
                
            });
        })
        .catch(error => console.error('Error fetching categories:', error));
  }


  function  filterDvdTable(dvds) {
    dvdTableBody.innerHTML = '';

    if (dvds.length === 0) {
        const row = document.createElement('tr');
        row.innerHTML = `<td colspan="8" class="no-data">No data found</td>`;
        dvdTableBody.appendChild(row);
        return;
    }
    dvds.forEach(dvd => {
        const row = document.createElement('tr');

        row.innerHTML = `
           <td><img src="${dvd.image}" alt="${dvd.title}" class="dvd-img"></td>
            <td>${dvd.id}</td>
             <td>${dvd.title}</td>
            <td>${dvd.director}</td>
            <td>${dvd.releaseDate}</td>
            <td>${dvd.genre}</td>
             <td>${dvd.copies}</td>
            <td>Rs.${dvd.price}</td>
        `;

        dvdTableBody.appendChild(row);
    });
}


// window.onload =() => {
//     fetchCategories();
//     fetchDVDs();
//     fetchCustomers();
//     fetchNotification();
//     fetchRentalHistory();
    
//   };


  async function fetchDVDs() {
    const response=await fetch(apiUrl);
    const dvds = await response.json();
      updateDVDTable(dvds);
  }


  //customer
async function fetchCustomers() {
    const cusresponse=await fetch(usersUrl);
    const customerdetails = await cusresponse.json();
      updateCustomerTable(customerdetails);
  }


  async function removeDVD(dvdId) {

    fetchDvdData().then(dvds => {
        const updatedDvds = dvds.filter(dvd => dvd.id !== parseInt(dvdId));
        // updateDvdData(updatedDvds);
        filterDvdTable(updatedDvds);
    });
}



function updateDVDTable(dvds) {
    const dvdTableBody = document.getElementById('dvdTable').querySelector('tbody');
    dvdTableBody.innerHTML = '';
  
    dvds.forEach(dvd => {
        const row = document.createElement('tr');
  
        const imageCell = document.createElement('td');
        const img = document.createElement('img');
        img.src = dvd.image;
        img.alt = dvd.title;
        img.style.width = '50px'; 
        img.style.height = 'auto';
        imageCell.appendChild(img);
        row.appendChild(imageCell);
      
  
        const idCell = document.createElement('td');
        idCell.textContent = dvd.id;
        row.appendChild(idCell);
  
        const titleCell = document.createElement('td');
        titleCell.textContent = dvd.title;
        row.appendChild(titleCell);
  
        const directorCell = document.createElement('td');
        directorCell.textContent = dvd.director;
        row.appendChild(directorCell);
  
        const releaseDateCell = document.createElement('td');
        releaseDateCell.textContent = dvd.releaseDate;
        row.appendChild(releaseDateCell);
  
        const genreCell = document.createElement('td');
        genreCell.textContent = dvd.genre;
        row.appendChild(genreCell);
  
        const copiesCell = document.createElement('td');
        copiesCell.textContent = dvd.copies;
        row.appendChild(copiesCell);
  
        const priceCell = document.createElement('td');
        priceCell.textContent = dvd.price;
        row.appendChild(priceCell);
  
  
        const actionCell = document.createElement('td');
        const editButton = document.createElement('button');
        editButton.innerHTML = '<ion-icon name="create-outline"></ion-icon>';
        editButton.classList.add('edit-btn');
        editButton.addEventListener('click', () => {
            // Add your edit function here
            editDVD(dvd.id);
            
        });
  
  
        const deleteButton = document.createElement('button');
        deleteButton.innerHTML = '<ion-icon name="trash-outline"></ion-icon>';
        deleteButton.classList.add('delete-btn');
        deleteButton.addEventListener('click', () => {
            // Add your delete function here
            removeDVD(dvd.id);
        });
    
        actionCell.appendChild(editButton);
        actionCell.appendChild(deleteButton);
    
        row.appendChild(actionCell);
  
        dvdTableBody.appendChild(row);
    });
  }


function updateDvdData(updatedDvds) {
    fetch(apiUrl, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updatedDvds)
    })
    .then(response => response.json())
    .then(data => {
    })
    .catch(error => {
        console.error("Error updating DVD data:", error);
    });
    }
    
    let price=80;
    // add new dvd
    async function addDVD() {
      const title = document.getElementById('title').value;
      const director = document.getElementById('director').value;
      const releaseDate = document.getElementById('releaseDate').value;
      const genre = document.getElementById('genre').value;
      const copies = document.getElementById('copies').value;
      
    
      const imageFile = document.getElementById('image').files[0];
      const imageBase64 = await getBase64(imageFile);
    
      const dvd = { id:generateId(),title, director, releaseDate, genre,copies,image:imageBase64,price};
    
      const response = await fetch(apiUrl, {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(dvd)
      });
      if (response.ok) {
          fetchDVDs();
          document.getElementById('dvdForm').reset();
      } else {
          alert('Failed to add DVD');
      }
    
    
      function getBase64(file) {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onload = () => resolve(reader.result);
            reader.onerror = error => reject(error);
            reader.readAsDataURL(file);
        });
    } 
    }
    
 
// document.getElementById('edit-btn')
function showModal(action, dvd = null) {
  if(action === 'edit'){
    dvdModal.style.display="block";
    showdvdBtn.style.visibility="hidden";
      modalTitle.textContent = 'Update DVD';
      submitBtn.textContent = 'Update DVD';
      if (dvd) 
        {
        dvdIdInput.value = dvd.id;
        document.getElementById('img').value = dvd.image;
        document.getElementById('title').value = dvd.title;
        document.getElementById('director').value = dvd.director;
        document.getElementById('releaseDate').value = dvd.releaseDate;
        document.getElementById('genre').value = dvd.genre;
        document.getElementById('copies').value = dvd.copies;

        // image
            if (dvd.image)
                 {
            const imagePreview = document.createElement('img');
            imagePreview.src = dvd.image;
            imagePreview.alt = 'Current DVD Image';
            imagePreview.style.width = '50px'; 
            imagePreview.style.height = 'auto';

            const imageField = document.querySelector('.field.image');
            imageField.appendChild(imagePreview);
                }
        }

} else {
    modalTitle.textContent = 'Add New DVD';
    submitBtn.textContent = 'Add DVD';
    modalForm.reset();
    dvdIdInput.value = ''; 

    const existingPreview = document.querySelector('.field.image img');
    if (existingPreview) {
        existingPreview.remove();
    }
   
}
dvdModal.style.display = 'flex';
}

function hideModal() {
  dvdModal.style.display = 'none';
}
// closeModalBtn.addEventListener('click', hideModal);



submitBtn.addEventListener('click', async () => {
  const dvdId = dvdIdInput.value;
  if (dvdId) {
      // Update DVD
      await editDVD(dvdId) ;
  } else {
      // Add new DVD
      await addDVD();
  }
  hideModal();
});

window.addEventListener('click', (event) => {
  if (event.target === dvdModal) {
      hideModal();
  }
});

  // id generate
      let dvdIdanuto=1; 
      function generateId() 
      {
        fetch(apiUrl)
            .then(response => response.json())
            .then(dvds => {
            if (dvds.length > 0) 
            {
                const lastDvd=dvds[dvds.length-1];
            let DVDIdCounter = parseInt(lastDvd.id.split('_')[1], 10);
            dvdIdanuto +=1; 
            return `dvd_${dvdIdanuto}`; 
            }
            else
            {
                dvdIdanuto=0;
                return `dvd_${dvdIdanuto}`; 
            } 
      })
    }



async function editDVD(dvdId) 
{
  const response = await fetch(`${apiUrl}/${dvdId}`);
  const dvd = await response.json();

  document.getElementById('title').value = dvd.title;
  document.getElementById('director').value = dvd.director;
  document.getElementById('releaseDate').value = dvd.releaseDate;
  document.getElementById('genre').value = dvd.genre;
  document.getElementById('copies').value = dvd.copies;
  document.getElementById('image').files[0]=dvd.image;

 const submitButton = document.getElementById('submit-btn');
    submitButton.textContent = 'Update DVD';
    submitButton.onclick = async function () {
      const updatedTitle = document.getElementById('title').value;
      const updatedDirector = document.getElementById('director').value;
      const updatedReleaseDate = document.getElementById('releaseDate').value;
      const updatedGenre = document.getElementById('genre').value;
      const updatedCopies = document.getElementById('copies').value;

      let updatedImageBase64 = dvd.image;
      const updatedImageFile = document.getElementById('image').files[0];
      if (updatedImageFile) {
          updatedImageBase64 = await getBase64(updatedImageFile);
      }

      const updatedDVD = {
          title: updatedTitle,
          director: updatedDirector,
          releaseDate: updatedReleaseDate,
          genre: updatedGenre,
          copies: updatedCopies,
          image: updatedImageBase64,
          price:80
      };

      //update the DVD
      const updateResponse = await fetch(`${apiUrl}/${dvdId}`, {
          method: 'PUT',
          headers: {
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(updatedDVD)
      });

      if (updateResponse.ok) {
          fetchDVDs();
          document.getElementById('dvdForm').reset();
          submitButton.textContent = 'Add DVD';
          submitButton.onclick = addDVD;
      } else {
          alert('Failed to update DVD');
      }
  };

}

// cateogories modal window
function openCategoryModal() {
  document.getElementById('categoryModal').style.display = 'block';
  document.getElementById('dvdModal').style.display = 'none';
}

// Close the category modal
function closeCategoryModal() {
  document.getElementById('categoryModal').style.display = 'none';
  document.getElementById('dvdModal').style.display = 'block';
}

function addCategory() {
  const newCategory = document.getElementById('newCategory').value;

  fetch(categoriesApiUrl, {
      method: 'POST',
      headers: {
          'Content-Type': 'application/json'
      },
      body: JSON.stringify({ name: newCategory })
  })
  .then(response => response.json())
  .then(() => {
      closeCategoryModal();
      fetchCategories();
      document.getElementById('newCategory').value = ''; 
      document.getElementById('dvdModal').style.display = 'block';
  })
  .catch(error => console.error('Error adding category:', error));
}


//filter

function filterDvds(dvds, category, searchText) {
    if (!Array.isArray(dvds)) {
        console.error("DVDs data is not an array or is undefined.");
        return [];
    }
    if (searchText === '') {
        return dvds;
    }

  searchText = searchText.toLowerCase();
return dvds.filter(dvd => {
      if (category === 'all') {
          return dvd.title.toLowerCase().includes(searchText) ||
                 dvd.director.toLowerCase().includes(searchText) ||
                 dvd.genre.toLowerCase().includes(searchText) 
      } else if (category === 'title') {
          return dvd.title.toLowerCase().includes(searchText);
      } else if (category === 'director') {
          return dvd.director.toLowerCase().includes(searchText);
      } else if (category === 'genre') {
          return dvd.genre.toLowerCase().includes(searchText);
      } 
      
  });
}

// -----------------------------------------Alert and nav click------------------------------------------

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
      if(index==0)
        {
          document.querySelector('.overview').style.display = 'block';
        }
        else
        {
          document.querySelector('.overview').style.display = 'none';
        }
      var tables = document.querySelectorAll(".data-table");
     
      if (tables.length > index) {
        tables[index].style.display = 'block';
       
      }
    });
  });


  function updateCustomerTable(customerdetails) {
    const customerTableBody = document.getElementById('customerTable').querySelector('tbody');
    customerTableBody.innerHTML = '';
  
    customerdetails.forEach(cus => {
        const row = document.createElement('tr');

        const idCell = document.createElement('td');
        idCell.textContent =cus.id;
        row.appendChild(idCell);
  
        const nameCell = document.createElement('td');
        nameCell.textContent = cus.name;
        row.appendChild(nameCell);
  
        const phoneCell = document.createElement('td');
        phoneCell.textContent =cus.phone;
        row.appendChild(phoneCell);
  
        const addressCell = document.createElement('td');
        addressCell.textContent = cus.address;
        row.appendChild(addressCell);
  
        const actionCell = document.createElement('td');
        const deleteButton = document.createElement('button');
        deleteButton.innerHTML = '<ion-icon name="trash-outline"></ion-icon>';
        deleteButton.classList.add('delete-btn');
        deleteButton.addEventListener('click', () => {
            // removeCustomer();
        });
    
        actionCell.appendChild(deleteButton);
        row.appendChild(actionCell);
        customerTableBody.appendChild(row);
    });
  }

let alertTimeout;
function logout()
{
   // alert("Logging out...");
    showAlert();
}

function showAlert() {
    document.getElementById('alertMessage').textContent = 'Logout Successfully!';
    document.getElementById('customAlert').style.display = 'block';
    document.getElementById('customAlert').classList.add('fade-in');
    document.querySelector("nav").classList.add('blur-background');
    document.getElementById('content').classList.add('blur-background');
    clearTimeout(alertTimeout);
}

function hideAlert() 
{
    document.getElementById('customAlert').style.display = 'none';
    document.querySelector("nav").classList.remove('blur-background');
    document.getElementById('content').classList.remove('blur-background');
    window.location.href = "../index.html";
}

  //notification
  const notificationIcon = document.getElementById('notificationIcon');
  const notificationModal = document.getElementById('notificationModal');
  const notificationTableBody = document.querySelector('#notificationTable tbody');
  const notificationCount = document.querySelectorAll('notification-Count');

  function fetchNotifications() {
    fetch('http://localhost:3000/adminNotification')
        .then(response => response.json())
        .then(data => {
            notificationIcon.textContent = data.length;
            notificationTableBody.innerHTML = '';
            data.forEach(notification => {
                const row = document.createElement('tr');
                
                row.innerHTML = `
                    <td>${notification.id}</td>
                    <td>${notification.user}</td>
                    <td>${notification.dvdName}</td>
                    <td>${notification.status}</td>
                    <td>${new Date(notification.date).toLocaleDateString({month:'2-digit',day:'2-digit'})}</td>
                    <td>
                    <select onchange="changeStatus(this, '${notification.id}', '${notification.userId}', '${notification.dvdId}')">
                            <option value="Accepted" ${notification.status === 'Accepted' ? 'selected' : ''}>Accepted</option>
                            <option value="Rented">Rented</option>
                        </select>
                    </td>
                    <td><button id="accept" onclick="acceptRequest('${notification.id}')"> Accept</button></td>
                    
                `;

                notificationTableBody.appendChild(row);
            });
        })
        .catch(error => console.error('Error fetching notifications:', error));
}

document.addEventListener('DOMContentLoaded', fetchNotification);
notificationIcon.onclick = function() {
    fetchNotification();
}
document.querySelectorAll('.close').onclick = function() {
    notificationModal.style.display = "none";
}

function acceptRequest(id) 
{
    const acceptTime=new Date().toLocaleDateString({month:'2-digit',day:'2-digit'});
    fetch(`http://localhost:3000/adminNotification/${id}`, {
        method: 'PATCH',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ status: 'Accepted',accepttime:acceptTime })
    })
    .then(() => {
        alert('Request accepted');
        fetchNotification();
        checkCancelRequest();
    })
    .catch(error => console.error('Error accepting request:', error));
}


async function fetchNotification()
{

    const response= await fetch('http://localhost:3000/adminNotification');
    const notification =await response.json();
    
    const notificationTableBody = document.getElementById('notificationTable').querySelector('tbody');
    notificationTableBody.innerHTML = '';
  
    notification.forEach(notify => {
        const row = document.createElement('tr');

        const idCell = document.createElement('td');
        idCell.textContent = notify.id;
        row.appendChild(idCell);
  
        
        const userCell = document.createElement('td');
        userCell.textContent = notify.user;
        row.appendChild(userCell);
  
        const dvdCell = document.createElement('td');
        dvdCell.textContent = notify.dvdName;
        row.appendChild(dvdCell);
  
        const statusCell = document.createElement('td');
        statusCell.textContent = notify.status;
        row.appendChild(statusCell);
  
        const dateCell = document.createElement('td');
        dateCell.textContent = new Date(notify.date).toLocaleDateString({day:'2-digit',month:'2-digit'});
        row.appendChild(dateCell);

        const actionCell = document.createElement('td');
        const selectElement = document.createElement('select');

        const acceptedOption = document.createElement('option');
        acceptedOption.value = 'Accepted';
        acceptedOption.textContent = 'Accepted';
        if (notify.status === 'Accepted') {
            acceptedOption.selected = true;
        }
        
        const rentedOption = document.createElement('option');
        rentedOption.value = 'Rented';
        rentedOption.textContent = 'Rented';
        if(selectElement.value==='Rented')
        {
            selectElement.disabled=true;
        }
        else
        {
            selectElement.disabled=false;
        }
        selectElement.appendChild(acceptedOption);
        selectElement.appendChild(rentedOption);
        
        if(statusCell.textContent==='Rented')
        {
            statusCell.style.fontWeight='bold'
            statusCell.style.color='green'
        }
        else{
             statusCell.style.fontWeight='bold'
            statusCell.style.color='yellow'
        }
        selectElement.addEventListener('change', function() {
            changeStatus(this,notify.id, notify.userId, notify.dvdName);
        });
        
        
  
    
  
  
        // const actionCell = document.createElement('td');
        // const acceptButton = document.createElement('button');
        // selectElement.innerHTML = 'Accept';
        // selectElement.classList.add('accept-btn');
        // selectElement.addEventListener('click', () => {
           
        //    acceptRequest(notify.id)
            
        // });
      
        // actionCell.appendChild(acceptButton);       
        // row.appendChild(actionCell);

        actionCell.appendChild(selectElement);
        row.appendChild(actionCell);
  
        notificationTableBody.appendChild(row);
    });
}
 
// function changeStatus(selectElement, notificationId, userId,dvdName) {
//     const newStatus = selectElement.value;

//     if (newStatus === 'Rented') {
//         const rentDate = new Date();
//         const dueDate = new Date(rentDate);
//         dueDate.setDate(rentDate.getDate() + 7);

//         // Create a new rental 
//         const rentalRecord = {
//             rentId: generateRentId(), 
//             customerId: userId,
//             dvdId: dvdName,
//             rentDate: rentDate.toISOString(),
//             dueDate: dueDate.toISOString(),
//             returnDate: null,
//             advance: 200,  
//             payAction: null
//         };

       
//         fetch('http://localhost:3000/rentals', {
//             method: 'POST',
//             headers: { 'Content-Type': 'application/json' },
//             body: JSON.stringify(rentalRecord)
//         })
//         .then(() => {
//             alert('DVD rented successfully');
           
//             return fetch(`http://localhost:3000/adminNotification/${notificationId}`, {
//                 method: 'PATCH',
//                 headers: { 'Content-Type': 'application/json' },
//                 body: JSON.stringify({ status: newStatus })
//             });
//         })
//         .then(() => {
//             fetchNotification(); 
//         })
//         .catch(error => console.error('Error processing rental:', error));
//     }
// }

function generateRentId() {
    fetch(rentsUrl)
    .then(response => response.json())
    .then(rent => {
        if (rent.length > 0) 
        {
            const lastRent=rent[rent.length-1];
            let rentcounter = parseInt(lastRent.id.split('_')[4], 2);
            rentcounter += 1; 
            return `rent${rentcounter}`;
        }
        else{
            rentcounter=0;
            return `rent${rentcounter}`;
        } 
    })
}


async function checkCancelRequest()
{
    const response = await fetch('http://localhost:3000/adminNotification');
    const notifications = await response.json();
    
    const currentTime = new Date();

    notifications.forEach(async (notify) => {
        if (notify.status === 'Accepted' && notify.acceptTime) {
            const acceptTime = new Date(notify.acceptTime);
            const timeDifference = Math.abs(currentTime - acceptTime); 
            const daysDifference = timeDifference / (1000 * 3600 * 24);

            if (daysDifference > 2) {
            
                await fetch(`http://localhost:3000/adminNotification/${notify.id}`, {
                    method: 'DELETE'
                });
                // await fetch(`http://localhost:3000/userNotification/${notify.id}`, {
                //     method: 'DELETE'
                // });

                alert(`Request with ID ${notify.id} deleted due to expiration.`);
            }
        }
    });
}

//rental table view
async function fetchRentalHistory() {
    try {
        const response = await fetch('http://localhost:3000/rentals');
        const rentalHistory = await response.json();

        const rentalHistoryTableBody = document.getElementById('RentalTable').querySelector('tbody');
        rentalHistoryTableBody.innerHTML = '';

        rentalHistory.forEach(record => {
            const row = document.createElement('tr');

            const rentIdCell = document.createElement('td');
            rentIdCell.textContent = record.id;
            row.appendChild(rentIdCell);

            const customerIdCell = document.createElement('td');
            customerIdCell.textContent = record.customerId;
            row.appendChild(customerIdCell);

            const dvdIdCell = document.createElement('td');
            dvdIdCell.textContent = record.dvdId;
            row.appendChild(dvdIdCell);

            const rentDateCell = document.createElement('td');
            rentDateCell.textContent = new Date(record.rentDate).toLocaleDateString({month:'2-digit',day:'2-digit'});
            row.appendChild(rentDateCell);

            const dueDateCell = document.createElement('td');
            dueDateCell.textContent = new Date(record.dueDate).toLocaleDateString({month:'2-digit',day:'2-digit'});
            row.appendChild(dueDateCell);

            const returnDateCell = document.createElement('td');
            returnDateCell.textContent = record.returnDate ? new Date(record.returnDate).toLocaleDateString({month:'2-digit',day:'2-digit'}) : 'Not Returned';
            row.appendChild(returnDateCell);

            const advanceCell = document.createElement('td');
            advanceCell.textContent = record.advance;
            row.appendChild(advanceCell);

            const payActionCell = document.createElement('td');
            payActionCell.textContent = record.payAction ? record.payAction : 'Pending';
            row.appendChild(payActionCell);

// 
            const actionCell = document.createElement('td');
            const returnButton = document.createElement('button');
            returnButton.textContent = 'Returned';
            returnButton.classList.add('return-btn');
            actionCell.appendChild(returnButton);
            row.appendChild(actionCell);
// 
            rentalHistoryTableBody.appendChild(row);

            returnButton.addEventListener('click', () => {
                showConditionModal(record);
            });
        });
    } catch (error) {
        console.error('Error fetching rental history:', error);
    }
}

// 23.9
function showConditionModal(record) {
    const modal = document.getElementById('checkConditionModal');
    const closeModal = modal.querySelector('.close');
    const conditionForm = document.getElementById('conditionForm');

    // Open the modal
    modal.style.display = 'block';

    // Close modal on close button click
    closeModal.addEventListener('click', () => {
        modal.style.display = 'none';
    });

    // Submit form event
    conditionForm.addEventListener('submit', async function (event) {
        event.preventDefault(); // Prevent form from reloading

        // Get values from the form
        const dvdCondition = document.getElementById('dvdCondition').value;
        const returnDate = document.getElementById('returnDate').value;

        let fine = 0;
        const returnDateObj = new Date(returnDate);
        const dueDateObj = new Date(record.dueDate);

        // 
        const dvdResponse = await fetch(`${apiUrl}/?title=${record.dvdId}`);
        const dvds = await dvdResponse.json(); 
        // 
        fine += dvds[0].price;
        // Calculate fine for late return
        if (returnDateObj > dueDateObj) {
            const delayDays = Math.ceil((returnDateObj - dueDateObj) / (1000 * 60 * 60 * 24)); // Calculate delay in days
            fine += delayDays * 5; // Rs. 5 fine per day
        }

        // Fine for damaged DVD
        if (dvdCondition === 'damaged') {
            fine += 50; // Rs. 50 fine if the DVD is damaged
        }

        // Calculate final payment
        const finalPayment = record.advance - fine;
        const finalPaymentText = finalPayment > 0 ? `Rs.${finalPayment}` : 'Pending';

        // Update the record with returnDate, fines, and final payment
        await updateRentalRecord(record.id, {
            returnDate,
            fine,
            payAction: finalPaymentText,
        });

        // Close the modal
        modal.style.display = 'none';

        // Refresh the table data without refreshing the page
        fetchRentalHistory();
    });
}
// 

function checkOverdueRentals() {
    fetch('http://localhost:3000/rentals')
    .then(response => response.json())
    .then(rentals => {
        rentals.forEach(rental => {
            const today = new Date().toISOString().split('T')[0]; // current date
            if (today > rental.dueDate && !rental.returnDate) {
                // Rental is overdue, update the record
                rental.overdue = true;
                updateRental(rental);
                sendOverdueAlert(rental);
            }
        });
    })
    .catch(error => {
        console.error('Error:', error);
    });
}

function updateRental(rental) {
    fetch(`http://localhost:3000/rentals/${rental.id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(rental)
    })
    .then(response => response.json())
    .then(data => {
        console.log('Rental updated:', data);
    })
    .catch(error => {
        console.error('Error:', error);
    });
}

async function updateRentalRecord(rentalId, updatedData) {
    // const returnbtn=document.getElementById('submit-cfm');
    try {
        await fetch(`http://localhost:3000/rentals/${rentalId}`, {
            method: 'PATCH', // Use PATCH to update specific fields
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedData),
        });
        
        alert('Rental record updated successfully.');

    } catch (error) {
      console.error('Error updating rental record:', error);
    }
}


//24.9 overdue
// function checkOverdue(dueDate) {
//     const today = new Date();
//     return today > new Date(dueDate);
// }
// 

function changeStatus(selectElement, notificationId, userId,dvdName) {
    const newStatus = selectElement.value;

    if (newStatus === 'Rented') {
        const rentDate = new Date();
        const dueDate = new Date(rentDate);
        dueDate.setDate(rentDate.getDate() + 7);

        // Create a new rental 
        const rentalRecord = {
            rentId: generateRentId(), 
            customerId: userId,
            dvdId: dvdName,
            rentDate: rentDate.toISOString(),
            dueDate: dueDate.toISOString(),
            returnDate: null,
            advance: 200,  
            payAction: null
        };

       
        fetch('http://localhost:3000/rentals', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(rentalRecord)
        })
        .then(() => {
            alert('DVD rented successfully');
           
            return fetch(`http://localhost:3000/adminNotification/${notificationId}`, {
                method: 'PATCH',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ status: newStatus })
            });
        })
        .then(() => {
            fetchNotification(); 
        })
        .catch(error => console.error('Error processing rental:', error));
    }
}





// 
// const overdue = checkOverdue(dueDate);
// if (overdue) {
//    alert('DVD is overdue!');
//     // Trigger notification
// } else {
//    alert('DVD is still within the rental period.');
// }

//notification overdue 24.9
// function sendOverdueAlert(dvdTitle, customerName) {
//     // Example structure for an overdue notification
//     const notification = {
//         message: `The DVD "${dvdTitle}" rented by ${customerName} is overdue!`,
//         date: new Date().toLocaleDateString(),
//         type: 'overdue'
//     };

//     // Store the notification in local storage or send to server
//     let notifications = JSON.parse(localStorage.getItem('managerNotifications')) || [];
//     notifications.push(notification);
//     localStorage.setItem('managerNotifications', JSON.stringify(notifications));

//     // Optionally, update UI with the new notification
//     console.log('Overdue notification sent:', notification);
// }

// Call this function when an overdue DVD is detected
// if (overdue) {
//     sendOverdueAlert('Inception', 'John Doe');
// }


// Send an overdue alert to the manager
function sendOverdueAlert(rental) {
    const notification = {
        message: `The DVD "${rental.dvdTitle}" rented by ${rental.customerName} is overdue!`,
        date: new Date().toISOString().split('T')[0],
        type: 'overdue'
    };
    
    fetch('http://localhost:3000/notifications', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(notification)
    })
    .then(response => response.json())
    .then(data => {
        console.log('Overdue notification sent:', data);
    })
    .catch(error => {
        console.error('Error:', error);
    });
}

// Run the check periodically (e.g., once a day)
setInterval(checkOverdueRentals, 24 * 60 * 60 * 1000); 

// function displayNotifications() {
//     fetch('http://localhost:3000/notifications')
//     .then(response => response.json())
//     .then(notifications => {
//         notifications.forEach(notification => {
//             console.log(`${notification.date}: ${notification.message}`);
//             // Update the notification UI here
//         });
//     })
//     .catch(error => {
//         console.error('Error:', error);
//     });
// }

// // Call this function when the manager checks notifications
// displayNotifications();


function toggleNotificationDropdown() {
    const dropdown = document.getElementById('notification-dropdown');
    dropdown.classList.toggle('show');
}


function fetchNotificationss() {
    fetch('http://localhost:3000/notifications')
        .then(response => response.json())
        .then(notifications => {
            updateNotificationUI(notifications);
        })
        .catch(error => {
            console.error('Error fetching notifications:', error);
        });
}

function updateNotificationUI(notifications) {
    const notificationDropdown = document.getElementById('notification-dropdown');
    const notificationCount = document.getElementById('notification-count');

    // Clear existing notifications in the dropdown
    notificationDropdown.innerHTML = '';

    // Update the badge count
    const unreadCount = notifications.length;
    notificationCount.textContent = unreadCount;

    // Populate the dropdown with notification items
    if (notifications.length === 0) {
        notificationDropdown.innerHTML = '<div class="notification-item">No new notifications</div>';
    } else {
        notifications.forEach(notification => {
            const notificationItem = `
                <div class="notification-item">
                    <span class="message">${notification.message}</span>
                    <span class="date">${new Date(notification.date).toLocaleDateString()}</span>
                </div>
            `;
            notificationDropdown.innerHTML += notificationItem;
        });
    }
}

fetchNotificationss();
}); 
