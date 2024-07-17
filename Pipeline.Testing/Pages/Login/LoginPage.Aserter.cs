
namespace Pipeline.Testing.Pages.Login
{
    public partial class LoginPage
    {
        // Is stay at Login Screen => return the title
        public string CurrentTilte => Title_lbl.GetText();
    }
}
