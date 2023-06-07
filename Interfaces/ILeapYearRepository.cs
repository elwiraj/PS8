using PS6.Areas.Data.Models;

namespace PS6.Interfaces;

public interface ILeapYearRepository
{
    Task<IQueryable<YearValidationResult>> GetResults(int pageIndex, int pageSize);
    Task AddResult(YearValidationResult yearValidationResult);
}
