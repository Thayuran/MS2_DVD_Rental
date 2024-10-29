async function fetchRentalHistory() {
    try {
        const response = await fetch('http://localhost:3000/rentals');
        const rentalHistory = await response.json();

        const rentalHistoryTableBody = document.getElementById('RentalTable').querySelector('tbody');
        rentalHistoryTableBody.innerHTML = ''; // Clear existing rows

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
            rentDateCell.textContent = new Date(record.rentDate).toLocaleDateString({month: '2-digit', day: '2-digit'});
            row.appendChild(rentDateCell);

            const dueDateCell = document.createElement('td');
            dueDateCell.textContent = new Date(record.dueDate).toLocaleDateString({month: '2-digit', day: '2-digit'});
            row.appendChild(dueDateCell);

            const returnDateCell = document.createElement('td');
            returnDateCell.textContent = record.returnDate ? new Date(record.returnDate).toLocaleDateString({month: '2-digit', day: '2-digit'}) : 'Not Returned';
            row.appendChild(returnDateCell);

            const advanceCell = document.createElement('td');
            advanceCell.textContent = record.advance;
            row.appendChild(advanceCell);

            const payActionCell = document.createElement('td');
            payActionCell.textContent = record.payAction ? record.payAction : 'Pending';
            row.appendChild(payActionCell);

            // Add the "Returned" button for the manager
            const actionCell = document.createElement('td');
            const returnButton = document.createElement('button');
            returnButton.textContent = 'Returned';
            returnButton.classList.add('return-btn');
            actionCell.appendChild(returnButton);
            row.appendChild(actionCell);

            rentalHistoryTableBody.appendChild(row);

            // Event listener for the "Returned" button to show the popup/modal
            returnButton.addEventListener('click', () => {
                showConditionModal(record);
            });
        });
    } catch (error) {
        console.error('Error fetching rental history:', error);
    }
}