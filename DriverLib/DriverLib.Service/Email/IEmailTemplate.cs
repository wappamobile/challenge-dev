using System.Threading.Tasks;

namespace DriverLib.Service
{
    public interface IEmailTemplate
    {
        Task<string> GenerateHtmlFromTemplateAsync(string template, object model);
    }
}
