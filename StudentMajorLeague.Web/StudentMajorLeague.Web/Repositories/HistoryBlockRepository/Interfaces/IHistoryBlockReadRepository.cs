using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.HistoryBlockRepository
{
    public interface IHistoryBlockReadRepository
    {
        Task<HistoryBlock> GetByIdAsync(int id);
    }
}
