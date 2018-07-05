using System.Threading.Tasks;

namespace SamplesRequest.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderViewToStringAsync(string name, object model);
    }
}