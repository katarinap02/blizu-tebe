using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface IHelpRequestRepository
    {
        void Create(HelpRequest helpRequest);
        void Update(HelpRequest helpRequest);
        void Delete(long helpRequestId);
        HelpRequest? GetById(long id);
        List<HelpRequest> GetAll(HelpType helpType);
        List<HelpRequest> GetMyExpired(HelpType helpType, long userId);
        List<HelpRequest> GetByCategory(HelpType helpType, HelpCategory category);
    }
}
