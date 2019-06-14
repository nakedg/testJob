using System.Threading.Tasks;
using TestJob.Core.Data;

namespace TestJob.Core.Services
{
    public interface IUserService
    {
        Task<PaginationList<UserEntity>> GetUsersAsync(int page, int pageSize, SortDefenition sort, string filter);
    }
}