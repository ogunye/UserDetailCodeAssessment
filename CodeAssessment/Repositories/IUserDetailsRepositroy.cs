using CodeAssessment.Models;

namespace CodeAssessment.Repositories
{
    public interface IUserDetailsRepositroy
    {
        Task<IEnumerable<UserDetail>> GetAllUserAsync();        
        Task<int> CreateUserAsync(UserDetail userDetail);
        Task<byte[]?> ExportAllToCsvAsync();
    }
}
