using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.Linq;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.OptionType.OptionTypeDetail.AddOptionToOptionType
{
    public partial class AddOptionToOptionTypeModal : DetailsContentPage<AddOptionToOptionTypeModal>
    {
        public AddOptionToOptionTypeModal() : base()
        {

        }

        // Add Building Phase to Building Group
        protected Label AddOptionToOptionTypeTitle_lbl => new Label(FindType.XPath, "//*[@id='sg-modal']/section/header/h1");

        protected Button AddOptionToOptionType_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertSO']");

        protected Button Cancel_btn => new Button(FindType.XPath, "//*[@id='sg-modal']/section/header/a");

        protected ListItem ListOfOption_lst => new ListItem(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbSOs']/div/ul/li").ToList());
    
    }
}

