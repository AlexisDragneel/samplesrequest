using System.Threading.Tasks;
using SamplesRequest.Models.Models.ViewModels;

namespace SamplesRequest.Services
{
    public interface IEmailService
    {
        void SendMail(string to, string htmlBody, string message = "", string toName = "", string subject = "");
        Task<ResultObject> SendNotificationResponsibleUser(int requestId);
    }
}