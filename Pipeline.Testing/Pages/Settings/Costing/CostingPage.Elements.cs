using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Settings.Costing
{
    public partial class CostingPage : DetailsContentPage<CostingPage>
    {

        public CostingPage() : base()
        {

        }

        protected Textbox TBDCode_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtTBDVendorCodes");
        protected Button Save_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbSave");
    }

}
