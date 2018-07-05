using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using SamplesRequest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SamplesRequest.Models.Models.DAL.Repositories;
using SamplesRequest.Models.Models.Entities;
using SamplesRequest.Models.Models.ViewModels;

namespace SamplesRequest.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHostingEnvironment _enviroment;
        private readonly IViewRenderService _viewRenderServicerenderService;
        private ResultObject resultobject;
        private readonly ISamplesRequestsRepository _requestRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly MailSettings _mailsettings;

        public EmailService(IHostingEnvironment environment, IViewRenderService viewRenderService,
            ISamplesRequestsRepository requestsRepository, IGenericRepository<User> userRepository,
            IOptions<MailSettings> mailsettings)
        {
            _enviroment = environment;
            _viewRenderServicerenderService = viewRenderService;
            _requestRepository = requestsRepository;
            resultobject = new ResultObject();
            _userRepository = userRepository;
            _mailsettings = mailsettings.Value;
        }

        public void SendMail(string to, string htmlBody, string message = "", string toName = "", string subject = "")
        {
            var mimemsg = new MimeMessage();
            var builder = new BodyBuilder();

            mimemsg.From.Add(new MailboxAddress("Xylem Mexico", "noreply@xylemninc.com"));
            mimemsg.To.Add(new MailboxAddress(toName == "" ? to : toName, to));
            mimemsg.Subject = subject == "" ? "Xylemn inc Server Message" : subject;

            builder.TextBody = message;
            builder.HtmlBody = htmlBody;

            mimemsg.Body = builder.ToMessageBody();

            sendMail(mimemsg);

        }

        private void sendMail(MimeMessage mimemsg)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 465, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(new NetworkCredential(_mailsettings.from_test_email, _mailsettings.from_test_email_pass));

                client.Send(mimemsg);
            }
        }

        public async Task<ResultObject> SendNotificationResponsibleUser(int requestId)

        {
            var request = _requestRepository.GetBy(r => r.id == requestId, includeProperties: "request_workflow_steps");

            if (_enviroment.IsDevelopment())
            {
                var viewString = await _viewRenderServicerenderService.RenderViewToStringAsync("Mail/SampleRequestNotification", request);

                SendMail(_mailsettings.test_email,
                    viewString,
                    "", _mailsettings.name_to, "you have a pending request");

            }
            else
            {
                var steps = request.request_workflow_steps;
                foreach (RequestWorkflowStep step in steps)
                {
                    var viewString = await _viewRenderServicerenderService.RenderViewToStringAsync("Mail/SampleRequestNotification", request);
                    var user = _userRepository.GetBy(u => u.uname == step.primary_responsible_id);
                    SendMail(user.email,
                        viewString,
                        "", user.name, "you have a peeding request");
                }
            }


            return resultobject;
        }
    }
}
