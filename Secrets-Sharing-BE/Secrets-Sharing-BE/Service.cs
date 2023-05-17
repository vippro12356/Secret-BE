using Secrets_Sharing_BE.Interfaces.Services;
using Secrets_Sharing_BE.Services;

namespace Secrets_Sharing_BE
{
    public class Service : IService
    {
        public ITextDataService TextDataService { get; set; }
        public IFileDataService FileDataService { get; set; }
        public Service(ITextDataService textDataService
            ,IFileDataService fileDataService)
        {
            TextDataService=textDataService;
            FileDataService=fileDataService;
        }
    }
}
