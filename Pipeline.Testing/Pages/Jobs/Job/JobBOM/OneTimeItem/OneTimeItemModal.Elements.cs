using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.Job.JobBOM.OneTimeItem
{
    public partial class OneTimeItemModal : JobBOMPage
    {
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id='one-time-product-modal']/section/header/h1");
        
        protected Button OptionArrow_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddOption1TimeProduct_Arrow']");
        protected Button BuildingPhaseArrow_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddPhase1TimeProduct_Arrow']");
        
        protected Textbox Name_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtProductName1Time']");
        protected Textbox Description_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtProductDescription1Time']");
        protected Textbox Quantity_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtProductQuantity1Time']");
        protected Textbox UnitCost_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtProductCost1Time']");
        protected Button TaxStatusArrow_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlProductTaxStatus_Arrow']");
        protected Textbox Notes_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtProductNotes1Time']");
        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSave1Time']");
        protected Button Close_btn => new Button(FindType.XPath, "//*[@id='one-time-product-modal']/section/header/a");
        protected IGrid OneTimeProduct_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOneTimeProduct_ctl00']", "//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnOneTimeProduct']/div[1]");
    }
}
