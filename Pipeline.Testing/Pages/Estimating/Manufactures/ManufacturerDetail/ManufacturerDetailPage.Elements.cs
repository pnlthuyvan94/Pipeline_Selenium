using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Manufactures.ManufacturerDetail
{
    public partial class ManufacturerDetailPage : DetailsContentPage<ManufacturerDetailPage>
    {
        protected Textbox Name_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox Url_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtUrl']");

        protected Textbox Description_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDescription']");

        protected Button ManufacturerSave_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

        protected Button ManufacturerAdd_btn
           => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypNew']");
    }

}
