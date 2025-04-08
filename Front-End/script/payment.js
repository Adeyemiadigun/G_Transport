import { logout } from "./logout.js";
logout();

async function fetchPayments() {
  let response = await fetch(
    "https://localhost:7156/api/Payment/customer-payments?PageSize=10&CurrentPage=1",
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("userToken"),
      },
    }
  );

  let data = await response.json();
  console.log(data)
   if (data.data == null) {
     console.log(true);
     tripList.innerHTML = `<tr class="m-4"><td colspan="9" class="text-center font-bold text-4xl ">No payment found.</td></tr>`;
     return;
   }
  if (data.status) {
    console.log("entered here")
    let payments = data.data.items;
    let tableBody = document.getElementById("paymentTableBody");
    tableBody.innerHTML = "";

    payments.$values.forEach((payment) => {
      let row = `<tr class='border'>
                        <td class='border p-2'>${payment.refrenceNo}</td>
                        <td class='border p-2'>${payment.transaction}</td>
                        <td class='border p-2'>₦${payment.amount}</td>
                        <td class='border p-2'>${payment.status == 2 ? "Successful" : "Failed" }</td>
                        <td class='border p-2'>${new Date(
                          payment.dateCreated
                        ).toLocaleDateString()}</td>
                        <td class='border p-2'><button class='bg-blue-500 text-white px-3 py-1 rounded'  id ="downloadBtn">Download</button></td>
                    </tr>`;
      tableBody.innerHTML += row;
      console.log(payment)
      document.querySelector("#downloadBtn").addEventListener("click", ()=>
      {
        console.log(payment.dateCreated)
          downloadReceipt(payment.refrenceNo, payment.transaction,payment.amount,payment.status,payment.dateCreated);
      })
    });
  } else {
    document.getElementById(
      "paymentTableBody"
    ).innerHTML = `<tr><td colspan="6" class="text-center p-4">No payments found</td></tr>`;
  }
}

function downloadReceipt(
  referenceNo,
  transaction,
  amount,
  status,
  dateCreated
) {
  let stat ;
  if(status == 2) {
    stat = "Successful";
  } else {
    stat = "Failed";
  }
  alert("Downloading receipt for: " + referenceNo);
  const { jsPDF } = window.jspdf;
  let doc = new jsPDF();

  // Add title
  doc.setFont("helvetica", "bold");
  doc.setFontSize(18);
  doc.text("🎟 Trip Ticket", 20, 20);

  // Add ticket details
  doc.setFont("helvetica", "normal");
  doc.setFontSize(12);
  doc.text(`Ref-No: ${referenceNo}`, 20, 40);
  doc.text(`Tansaction No: ${transaction}`, 20, 50);
  doc.text(`Amount Paid: ₦${amount}`, 20, 60);
  doc.text(`status: ${stat}`, 20, 70);
  doc.text(`Date : ${dateCreated}`, 20, 90);

  // Save the PDF file
  doc.save(`Ticket_receipt.pdf`);
}

fetchPayments();
