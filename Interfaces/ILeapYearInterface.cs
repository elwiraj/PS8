using PS6.Pages.Forms;
using PS6.ViewModels;

namespace PS6.Areas.Data;

public interface ILeapYearInterface
{
    Task Save(HttpContext httpContext, YearResponse yearResponse);
    Task<ListForLeapYearResultVM> GetResults(int pageIndex, int pageSize);
}
