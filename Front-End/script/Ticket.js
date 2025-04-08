const { jsPDF } = window.jspdf;

async function fetchTickets() {
  try {
    const response = await fetch(
      "https://localhost:7156/api/Ticket/my-ticket",
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("userToken"),
        },
      }
    );

    const data = await response.json();
    const container = document.getElementById("ticketContainer");
    container.innerHTML = "";

    if (data.status && data.data?.$values?.length > 0) {
      data.data.$values.forEach((ticket, index) => {
        const card = document.createElement("div");
        card.className =
          "bg-white p-6 rounded shadow text-center max-w-md mx-auto";
        card.innerHTML = `
                            <h2 class="text-xl font-bold mb-4">🎫 Trip Ticket</h2>
                            <p><strong>Trip:</strong> ${ticket.tripOrigin} to ${
          ticket.tripDestination
        }</p>
                            <p><strong>Date:</strong> ${new Date(
                              ticket.tripDate
                            ).toLocaleDateString()}</p>
                            <p><strong>Seat No:</strong> ${
                              ticket.seatNumber
                            }</p>
                            <p><strong>Ticket No:</strong> ${
                              ticket.ticketNumber
                            }</p>
                            <p><strong>Amount Paid:</strong> ₦${
                              ticket.amountPaid
                            }</p>
                            <p><strong>Transaction ID:</strong> ${
                              ticket.refrenceNo
                            }</p>
                            <button class="mt-4 bg-green-500 hover:bg-green-600 text-white py-2 px-4 rounded" data-index="${index}">Download Ticket</button>
                        `;
        container.appendChild(card);

        card.querySelector("button").addEventListener("click", () => {
          downloadTicket(ticket);
        });
      });
    } else {
      container.innerHTML = `<p class="text-center text-gray-500">No tickets found.</p>`;
    }
  } catch (error) {
    console.error("Error fetching tickets:", error);
  }
}

function downloadTicket(ticket) {
  const doc = new jsPDF();
  doc.setFont("helvetica", "bold");
  doc.setFontSize(18);
  doc.text("🎟 Trip Ticket", 20, 20);

  doc.setFont("helvetica", "normal");
  doc.setFontSize(12);
  doc.text(`Trip: ${ticket.tripOrigin} to ${ticket.tripDestination}`, 20, 40);
  doc.text(`Date: ${new Date(ticket.tripDate).toLocaleDateString()}`, 20, 50);
  doc.text(`Seat No: ${ticket.seatNumber}`, 20, 60);
  doc.text(`Ticket No: ${ticket.ticketNumber}`, 20, 70);
  doc.text(`Amount Paid: ₦${ticket.amountPaid}`, 20, 80);
  doc.text(`Transaction ID: ${ticket.refrenceNo}`, 20, 90);

  doc.save(`Ticket_receipt-${ticket.ticketNumber}.pdf`);
}

fetchTickets();
