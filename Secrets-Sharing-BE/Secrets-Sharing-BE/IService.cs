using Secrets_Sharing_BE.Interfaces.Services;

namespace Secrets_Sharing_BE
{
    public interface IService
    {
        ITextDataService TextDataService { get; }
        IFileDataService FileDataService { get; }
    }
}
