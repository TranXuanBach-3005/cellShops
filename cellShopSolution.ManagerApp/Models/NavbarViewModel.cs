using cellShopSloution.ViewModel.Dtos.Languages;

namespace cellShopSolution.ManagerApp.Models
{
    public class NavbarViewModel
    {
        public List<LanguageViewModel> Languages { get; set; }
        public string CurrenLanguageId { get; set; }
        public string ReturnUrl { get; set; }
    }
}
