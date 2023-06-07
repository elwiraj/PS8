using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PS6.Areas.Data;
using PS6.Areas.Data.Models;
using PS6.Areas.YearDb;
using PS6.ViewModels;

namespace PS6.Pages.History
{
    public class IndexModel : PageModel
    {
        private readonly ILeapYearInterface leapYearService;

        public IndexModel(ILeapYearInterface leapYearService)
        {
            this.leapYearService = leapYearService;
        }

        public ListForLeapYearResultVM ListForLeapYearResultVM { get; set; } = default!;

        public async Task OnGetAsync(int pageIndex = 0, int pageSize = 20)
        {
            ListForLeapYearResultVM = await leapYearService.GetResults(pageIndex, pageSize);
        }
    }
}
