using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Costing.Vendor.BaseCosts.AddBaseCost
{
    public partial class AddBaseCostModal : BaseCostPage
    {
        public AddBaseCostModal() : base()
        { }

        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id='options-modal']/section/header/h1");
        protected DropdownList Products_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlProducts");
        protected DropdownList Styles_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlStyles");
        protected Textbox MaterialCost_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtNewMUnitCost");
        protected Textbox LaborCost_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtNewLUnitCost");
        protected Button AddCost_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertNewCost");
        protected Button AddAllStyles_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertAllCosts");

    }
}
