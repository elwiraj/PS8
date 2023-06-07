using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PS6.Areas.Data;
using PS6.Areas.Data.Models;
using PS6.Areas.YearDb;
using PS6.Pages.Forms;
using System.Security.Claims;

namespace PS6.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILeapYearInterface leapYearService;

        [BindProperty(SupportsGet = true)]
        public YearForm Form { get; set; }

        public YearResponse? YearResponse { get; set; }

        public IndexModel(ILeapYearInterface leapYearService)
        {
            Form = new YearForm();
            YearResponse = null;
            this.leapYearService = leapYearService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                YearResponse = new YearResponse(Form.Name, Form.Year.Value);
                SaveInSession(YearResponse);
                await SaveInDatabase(YearResponse);
            }

            return Page();
        }

        public List<YearResponse> GetResponses()
        {
            List<YearResponse>? currentList = new List<YearResponse>();
            var data = HttpContext.Session.GetString("Data");

            if (data != null)
            {
                currentList = JsonConvert.DeserializeObject<List<YearResponse>>(data);
                if (currentList == null)
                {
                    currentList = new List<YearResponse>();
                }
            }

            return currentList;
        }

        public void SaveInSession(YearResponse response)
        {
            var currentList = GetResponses();

            currentList.Add(response);
            HttpContext.Session.SetString("Data", JsonConvert.SerializeObject(currentList));
        }

        public async Task SaveInDatabase(YearResponse response)
        {
            await leapYearService.Save(HttpContext, response);
        }
    }
}