using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace PolicyDetailsPdfGenerator.PersonPolicyService
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(byte[] streamResult, string FileExtension)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("dkhan895@gmail.com"));
            email.To.Add(MailboxAddress.Parse("dkhan895@gmail.com"));
            email.Subject = "Policy pdf";
            email.Body = new TextPart(TextFormat.Html) { Text = streamResult.ToString() };
            //email.Attachments = new MemoryStream(streamResult);
            var builder = new BodyBuilder();
            //var attachments = new List<MimeEntity>
            //{
            //    MimeEntity.Load(new ContentType("application", "pdf"), new MemoryStream(streamResult))
            //};
            
            builder.Attachments.Add("PrudentialPolicy.pdf", streamResult, new ContentType("application", FileExtension));
            //foreach (var attachment in attachments)
            //{
            //    builder.Attachments.Add(attachment);
            //}

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("dkhan895@gmail.com", "uxlnuozyheyzxrs");
            smtp.Send(email);
            smtp.Disconnect(true);

            return true;
        }
    }
}
