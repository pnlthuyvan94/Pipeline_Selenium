using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Settings.Estimating
{
    public partial class EstimatingPage : DetailsContentPage<EstimatingPage>
    {
        protected CheckBox ShowSubcomponent_Description_chk => new CheckBox(FindType.Id, "ctl00_CPH_Content_chkShowSubcomponentDescription");
        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbSave");
        protected CheckBox ShowCategory_ProductConversion_chk => new CheckBox(FindType.Id, "ctl00_CPH_Content_chkShowSpecSetCategory");
        protected CheckBox ShowCategory_AddOptionProduct_chk => new CheckBox(FindType.Id, "ctl00_CPH_Content_chkShowOptionProductCategory");
    }
}
