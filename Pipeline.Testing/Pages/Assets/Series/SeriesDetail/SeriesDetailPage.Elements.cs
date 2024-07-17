using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.Series.SeriesDetail
{
    public partial class SeriesDetailPage : DetailsContentPage<SeriesDetailPage>
    {

        // name
        protected Textbox Title_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtTitle");

        // Code
        protected Textbox Code_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtCode");

        // Description
        protected Textbox Description_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtDescription");

        // Save button
        protected Button Save_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbSaveContinue");

        // Add House button
        protected Button AddHouse_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddHouse");

        // House Table
        protected IGrid House_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgHouses_ctl00","");

        // Add house modal title
        protected Label AddHouseModalTitle_Lbl => new Label(FindType.XPath, "//*[@id='houses-modal']/section/header/h1");

        // List house (we will using the other control like ListItem)
        protected ListItem House_Lst => new ListItem(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbHouses']/div/ul/li/span").ToList());

        // Add
        protected Button AddHouseInModal_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertHouse");

        // "Close modal" button
        protected Button CloseModal_Btn => new Button(FindType.XPath, "//*[@id='houses - modal']/section/header/a");

    }
}
