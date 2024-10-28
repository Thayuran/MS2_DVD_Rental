document.addEventListener("DOMContentLoaded", function() {
    const showdvdBtn = document.getElementById("add-dvd-btn");
    const dvdModal = document.getElementById("dvdModal");
    const closeModalBtn = document.getElementById("closeModalBtn");
    const modalTitle=document.getElementById("modal-title");
    const modalForm=document.getElementById("modal-form");
    const modal = document.getElementById('modal');
    // window.addEventListener("click", function(event) {
    //     if (event.target == dvdModal) {
    //         dvdModal.style.display = "none";
    //     }
       
    // });
    
    
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

});

        const apiUrl = 'http://localhost:3000/dvds';
        const categoriesApiUrl = 'http://localhost:3000/categories';


// add new dvd
async function addDVD() {
    const title = document.getElementById('title').value;
    const director = document.getElementById('director').value;
    const releaseDate = document.getElementById('releaseDate').value;
    const genre = document.getElementById('genre').value;
    const copies = document.getElementById('copies').value;
    // const imageFile = document.getElementById('image').files[0];

    // const imageBase64 = await getBase64(imageFile);

    // const dvd = { title, director, releaseDate, genre,copies,image:imageBase64};
    const dvd = { title, director, releaseDate, genre,copies};

    const response = await fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(dvd)
    });
    // .then(response => response.json())
    // .then(() => {
    //     fetchDVDs();
    // })
    // .catch(error => console.error('Error adding DVD:', error));
    if (response.ok) {
        fetchDVDs();
        document.getElementById('dvdForm').reset();
    } else {
        alert('Failed to add DVD');
    }
}

async function fetchDVDs() {
    const response=await fetch(apiUrl);
        // .then(response => response.json())
        // .then(data => {
        //     updateDVDList(data);
        // })
        // .catch(error => console.error('Error fetching DVDs:', error));
        const dvds = await response.json();
        updateDVDTable(dvds);
}

// Remove a DVD by title
async function removeDVD() {
    const title = document.getElementById('title').value;

    const dvds = await fetch(apiUrl)
        .then(response => response.json());
        // .then(dvds => {
        //     const dvd = dvds.find(dvd => dvd.title === title);
        //     if (dvd) {
        //         fetch(`${apiUrl}/${dvd.id}`, {
        //             method: 'DELETE'
        //         })
        //         .then(() => {
        //             fetchDVDs();
        //         })
        //         .catch(error => console.error('Error removing DVD:', error));
        //     } else {
        //         alert('DVD not found');
        //     }
        // })
        // .catch(error => console.error('Error fetching DVDs:', error));
        const dvdToRemove = dvds.find(dvd => dvd.title === title);
        if (dvdToRemove) {
            const response = await fetch(`${dvdsEndpoint}/${dvdToRemove.id}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                fetchDVDs();
            } else {
                alert('Failed to remove DVD');
            }
        } else {
            alert('DVD not found');
        }
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
        document.getElementById('newCategory').value = '';  // Clear the input field
    })
    .catch(error => console.error('Error adding category:', error));
}

window.onload =() => {
    fetchCategories();
    fetchDVDs();
};

//  let editingRow = null;

// function openModal(title,row=null)
// {
// // modalTitle.textContent=title;
// if(row)
// {
//    document.getElementById('title').value=row.cells[0].textContent;
//    document.getElementById('director').value=row.cells[1].textContent;
//    document.getElementById('releaseDate').value=row.cells[2].textContent;
//    document.getElementById('genre').value=row.cells[3].textContent;
//    document.getElementById('genre').value=row.cells[4].textContent;
//    document.getElementById('image').value=row.cells[5].textContent;
// }
// else{
//     // modalForm.reset();
// }
//     editingRow =row;
//     // modal.style.display='block';
// }


// function addRow() 
// {
//         openModal('Add Row');
// }

// addRow();
// table view

function updateDVDTable(dvds) {
    const dvdTableBody = document.getElementById('dvdTable').querySelector('tbody');
    dvdTableBody.innerHTML = '';

    dvds.forEach(dvd => {
        const row = document.createElement('tr');

        // const imageCell = document.createElement('td');
        // const image = document.createElement('img');
        // image.src = dvd.image;
        // image.alt = dvd.title;
        // imageCell.appendChild(image);
        // row.appendChild(imageCell);

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

        dvdTableBody.appendChild(row);
    });
}
// ----------------------------------------------------------------

















// validation

// const form = document.querySelector("form");
// eField = form.querySelector(".title"),
// eInput = eField.querySelector("input"),
// pField = form.querySelector(".director"),
// pInput = pField.querySelector("input");

// form.onclick= (e)=>{
//   e.preventDefault();
//   (eInput.value == "") ? eField.classList.add("shake", "error") : checkEmail();
//   (pInput.value == "") ? pField.classList.add("shake", "error") : checkPass();

//   setTimeout(()=>{
//     eField.classList.remove("shake");
//     pField.classList.remove("shake");
//   }, 50);

// //   eInput.onkeyup = ()=>{checkEmail();} //calling checkEmail function on email input keyup
// //   pInput.onkeyup = ()=>{checkPass();} //calling checkPassword function on pass input keyup

//   function checkPass(){ 
//     if(pInput.value == ""){
//       pField.classList.add("error");
//       pField.classList.remove("valid");
//     }else{ 
//       pField.classList.remove("error");
//       pField.classList.add("valid");
//     }
//   }

//   function checkEmail(){ //checkPass function
//     if(eInput.value == ""){ //if pass is empty then add error and remove valid class
//       eField.classList.add("error");
//       eField.classList.remove("valid");
//     }else{ //if pass is empty then remove error and add valid class
//       eField.classList.remove("error");
//       eField.classList.add("valid");
//     }
//   }

//   //if eField and pField doesn't contains error class that mean user filled details properly
//   if(!eField.classList.contains("error") && !pField.classList.contains("error")){
//     window.location.href = form.getAttribute("action"); //redirecting user to the specified url which is inside action attribute of form tag
//   }
// }

