using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Assets.House.Quantities.HouseOptionCopyQuantities
{
    public partial class HouseOptionCopyQuantities : DetailsContentPage<HouseOptionCopyQuantities>
    {
        
        protected Button ModalNoBtn => new Button(FindType.XPath, "//header[@class = 'card-header']//following::section/input[@value = 'No']");
        protected Button ModalXBtn => new Button(FindType.XPath, "//header[@class = 'card-header']/a[@class = 'close']");
        protected Button ModalYesBtn => new Button(FindType.XPath, "//header[@class = 'card-header']//following::section/span/input[@value = 'Yes']");
        //protected Button SelectHouseOptionYellow => new Button(FindType.XPath, "//p[contains(.,'Select Houses and Options')]/parent::div[@class = 'pnlCopyQtys']");
        public Label CopyToHouse => new Label(FindType.XPath, "//p[contains(.,'Select Houses and Options to Copy Quantities to:')]/parent::div");
        public Label CopyToOpt => new Label(FindType.XPath, "//p[text() = 'Select Option and Option(s) to Copy Quantities to:']/parent::div");
    }
}
