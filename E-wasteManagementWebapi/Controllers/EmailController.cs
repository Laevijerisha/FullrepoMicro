using E_wasteManagementWebapi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using E_wasteManagementWebapi.Model;
using E_wasteManagementWebapi.DTO;

namespace E_wasteManagementWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _adcontext;
        public EmailController(IEmailService adcontext)
        {
            _adcontext = adcontext;
        }


        [HttpPost("SendingEmail")]
        public async Task<IActionResult> Add([FromForm] CenterDTO centerDetails)
        {
            Center center = new Center()
            {
                CenterId = centerDetails.CenterId,
                Email = centerDetails.Email,
                Password = centerDetails.Password,
                personalEmail = centerDetails.personalEmail,

            };

            await SendEmailToCenterAsync(centerDetails);
            return CreatedAtAction(nameof(Add), center);

        }


        private async Task SendEmailToCenterAsync(CenterDTO centerDetails)
        {
            try
            {

                var centerEmail = centerDetails.personalEmail; // Replace with actual admin email address
                var subject = $"Your Center Email and Password";
                var body = $"Your Email id is: {centerDetails.Email} Password: {centerDetails.Password}";

                await _adcontext.SendEmailAsync(centerEmail, subject, body);
               
            }
            catch (Exception ex)
            {
              
                return;
            }
        }
    }
}

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}


public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        using (var client = new SmtpClient("smtp-mail.outlook.com"))
        {
            client.Port = 587;
            client.Credentials = new NetworkCredential("laevijerisha05@gmail.com", "2801@Freeda");
            client.EnableSsl = true;

            var message = new MailMessage
            {
                From = new MailAddress("laevijerisha05@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };
            message.To.Add(toEmail);

            await client.SendMailAsync(message);
        }
    }
}




