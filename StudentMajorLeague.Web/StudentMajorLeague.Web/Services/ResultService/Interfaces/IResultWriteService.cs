using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.ResultService
{
    public interface IResultWriteService
    {
        Task<Result> AddAsync(Result model);
    }
}
