using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail
{
    public partial class DivisionDetailPage : DetailsContentPage<DivisionDetailPage>
    {
        protected Textbox DivisionName_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtDivisionName");

        protected Textbox DivisionAddress_txt =>
            new Textbox(FindType.Id, "ctl00_CPH_Content_txtDivisionAddress");

        protected Textbox City_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtDivisionCity");

        protected Textbox State_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtDivisionState");

        protected Textbox Zip_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtDivisionZip");

        protected Textbox Phone_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtDivisionPhone");

        protected Textbox Fax_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtDivisionFax");

        protected Textbox MainEmail_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtDivisionEmail");

        protected Textbox ServiceEmail_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtDivisionServiceEmail");

        protected Textbox Slug_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtSlug");

        protected Textbox Description_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtDivisionDescription");

        protected Button Save_btn
            => new Button(FindType.Id, "ctl00_CPH_Content_lbSaveDivision");
    }
}
