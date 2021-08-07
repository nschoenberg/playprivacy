using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using PlayPrivacy.Data;

namespace PlayPrivacy.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PrivacyDbContext _dbContext;

        public IndexModel(ILogger<IndexModel> logger, PrivacyDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            var counter = _dbContext.Counter.FirstOrDefault();

            if (counter == null)
            {
                counter = new Counter();
                _dbContext.Add(counter);
                _dbContext.SaveChanges();
            }
        }

        public async Task<IActionResult> OnPostVoteYesAsync()
        {
            var counter = _dbContext.Counter.First();
            counter.Yes++;
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostVoteNoAsync()
        {
            var counter = _dbContext.Counter.First();
            counter.No++;
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public IActionResult OnPostShowResult()
        {
            var counter = _dbContext.Counter.First();
            var total = counter.Yes + counter.No;

            Message = $"{counter.Yes} / {total}";

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostResetAsync()
        {
            var counter = _dbContext.Counter.First();
            counter.No = 0;
            counter.Yes = 0;
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
