
let alertTimeout;
function logout() 
{
   // alert("Logging out...");
    showAlert();
}

function showAlert() {
    document.getElementById('alertMessage').textContent = 'Logout Successfully!';
    document.getElementById('customAlert').style.display = 'block';
    clearTimeout(alertTimeout);
}

function hideAlert() 
{
    document.getElementById('customAlert').style.display = 'none';
    window.location.href = "../index.html";
}


function formatDateTime(date) {
    const options = { 
        year: 'numeric', 
        month: 'short', 
        day: '2-digit',
        hour: '2-digit', 
        minute: '2-digit', 
        second: '2-digit', 
        hour12: true 
    };
    return date.toLocaleDateString('en-US', options);
}

function updateLastAccess() {
    const now = new Date();
    document.getElementById('lastAccessTime').textContent = formatDateTime(now);
}

// Update the "Last access" time when the page loads
updateLastAccess();




