using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.HistoryBlockRepository
{
    public interface IHistoryBlockWriteRepository
    {
        Task<HistoryBlock> AddAsync(HistoryBlock model);
    }
}
