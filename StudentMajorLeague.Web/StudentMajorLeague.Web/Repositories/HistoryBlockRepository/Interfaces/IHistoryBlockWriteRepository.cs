using StudentMajorLeague.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.HistoryBlockRepository
{
    public interface IHistoryBlockWriteRepository
    {
        Task<HistoryBlock> AddAsync(HistoryBlock model);

        Task RemoveAsync(HistoryBlock model);

        Task RemoveRangeAsync(IEnumerable<HistoryBlock> models);
    }
}
