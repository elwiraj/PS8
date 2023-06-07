using PS6.Areas.Data;
using PS6.Areas.Data.Models;
using PS6.Interfaces;
using PS6.Pages.Forms;
using PS6.ViewModels;
using System.Security.Claims;

namespace PS6.Services;

public class LeapYearService : ILeapYearInterface
{
    private readonly ILeapYearRepository leapYearRepository;

    public LeapYearService(ILeapYearRepository leapYearRepository)
    {
        this.leapYearRepository = leapYearRepository;
    }

    public async Task Save(HttpContext httpContext, YearResponse response)
    {
        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userName = httpContext.User.Identity.Name;

        var yearValidationResult = new YearValidationResult
        {
            Year = response.Year,
            Result = response.ToString(),
            TimeAdded = DateTime.Now,
            UserId = userId,
            UserLogin = userName
        };

        await leapYearRepository.AddResult(yearValidationResult);
    }

    public async Task<ListForLeapYearResultVM> GetResults(int pageIndex, int pageSize)
    {
        var leapYearResultVMList = new List<LeapYearResultVM>();
        var resultsDb = await leapYearRepository.GetResults(pageIndex, pageSize);

        foreach (YearValidationResult? result in resultsDb)
        {
            var resultVM = new LeapYearResultVM
            {
                Id = result.Id,
                Year = result.Year,
                Result = result.Result,
                TimeAdded = result.TimeAdded,
                UserId = result.UserId,
                UserLogin = result.UserLogin,
            };
            leapYearResultVMList.Add(resultVM);
        }

        var results = new ListForLeapYearResultVM {  ListLeapYearResult = leapYearResultVMList};
        return results;
    }
}
