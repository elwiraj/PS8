using PS6.Areas.Data.Models;
using PS6.Areas.YearDb;
using PS6.Interfaces;

namespace PS6.Repositories;

public class LeapYearRepository : ILeapYearRepository
{
    private readonly YearDbContext _context;

    public LeapYearRepository(YearDbContext context)
    { 
        _context = context;
    }

    public async Task<IQueryable<YearValidationResult>> GetResults(int pageIndex, int pageSize)
    {
        return _context.YearValidationResult
            .Skip(pageIndex * pageSize)
            .Take(pageSize);
    }

    public async Task AddResult(YearValidationResult yearValidationResult)
    {
        await _context.YearValidationResult.AddAsync(yearValidationResult);
        await _context.SaveChangesAsync();
    }
}
