using ClinicManagementSystem.Models;
using ClinicManagementSystem.Service;
using System.Net;
using System.Net.Mail;
namespace ClinicManagementSystem.Services
{ 
    public class SmtpEmailService : IEmailService
    {
        public void SendBill(string toEmail, BillViewModel bill)
        {
            // 🔹 MUST be a real Gmail you own
            var fromEmail = "clinicmanagementsystem.project@gmail.com";

            var message = new MailMessage();
            message.From = new MailAddress(fromEmail, "Clinic Management System");

            // 🔹 THIS LINE WAS MISSING
            message.To.Add(toEmail);

            message.Subject = "Consultation Bill";
            message.Body = $@"
            <h3>Consultation Bill</h3>
            <p><b>Patient:</b> {bill.PatientName}</p>
            <p><b>Doctor:</b> {bill.DoctorName}</p>
            <p><b>Date:</b> {bill.AppointmentDate:dd-MM-yyyy}</p>
            <p><b>Total Amount:</b> ₹{bill.TotalAmount}</p>
        ";
            message.IsBodyHtml = true;

            using var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(
                    fromEmail,
                    "GOOGLEAPP_PASSWORD"   //  REAL Gmail App Password
                ),
                EnableSsl = true
            };

            smtp.Send(message);
        }

    }
}
