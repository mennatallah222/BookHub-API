using API.Service.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace API.Service.Implementations
{
    public class EmailService : IEmailService
    {
        public async Task<string> SendEmail(string email, string message, string? reason)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    client.Authenticate("mennatallahahmed892@gmail.com", "ybungwfhjxktnkgs");
                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = message,
                        TextBody = email
                    };
                    var msg = new MimeMessage() { Body = bodyBuilder.ToMessageBody() };
                    msg.From.Add(new MailboxAddress("Mennatallah Email", "mennatallahahmed892@gmail.com"));
                    msg.To.Add(new MailboxAddress("i'm testing it", email));
                    msg.Subject = reason == null ? "Not submitted" : reason;
                    await client.SendAsync(msg);
                    await client.DisconnectAsync(true);

                }
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
