using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Settings.MSNAV
{
    public partial class MSNAVPage : DetailsContentPage<MSNAVPage>
    {
        protected Button Running_btn => new Button(FindType.Id, "ctl00_CPH_Content_rbStatus_0");
        protected Button Paused_btn => new Button(FindType.Id, "ctl00_CPH_Content_rbStatus_1");
        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbSave");
    }
}
