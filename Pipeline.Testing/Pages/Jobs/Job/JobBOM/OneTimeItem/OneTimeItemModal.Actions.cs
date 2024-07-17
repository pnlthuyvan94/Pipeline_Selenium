using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.Job.JobBOM.OneTimeItem
{
    public partial class OneTimeItemModal
    {
        public OneTimeItemModal SelectOption(string optionName)
        {
            OptionArrow_btn.Click();
            SpecificControls optionCtrl = new SpecificControls(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ddlAddOption1TimeProduct_DropDown']/div[1]/ul/li[contains(text(),'{optionName}')]");
            optionCtrl.Click();
            return this;
        }

        public OneTimeItemModal SelectBuildingPhase(string buildingPhase)
        {
            BuildingPhaseArrow_btn.Click();
            SpecificControls bpCtrl = new SpecificControls(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ddlAddPhase1TimeProduct_DropDown']/div[1]/ul/li[contains(text(),'{buildingPhase}')]");
            bpCtrl.Click();
            return this;
        }

        public OneTimeItemModal EnterName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name_txt.SetText(name);
            return this;
        }

        public OneTimeItemModal EnterDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Description_txt.SetText(description);
            return this;
        }

        public OneTimeItemModal EnterQuantity(string quantity)
        {
            if (!string.IsNullOrEmpty(quantity))
                Quantity_txt.SetText(quantity);
            return this;
        }

        public OneTimeItemModal EnterUnitCost(string unitCost)
        {
            if (!string.IsNullOrEmpty(unitCost))
                UnitCost_txt.SetText(unitCost);
            return this;
        }

        public OneTimeItemModal SelectTaxStatus(string taxStatus)
        {
            TaxStatusArrow_btn.Click();
            SpecificControls taxStatusCtrl = new SpecificControls(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ddlProductTaxStatus_DropDown']/div[1]/ul/li[contains(text(),'{taxStatus}')]");
            taxStatusCtrl.Click();
            return this;
        }

        public OneTimeItemModal EnterNotes(string notes)
        {
            if (!string.IsNullOrEmpty(notes))
                Notes_txt.SetText(notes);
            return this;
        }

        public void Save()
        {
            Save_btn.Click(false);
        }

        public void Close()
        {
            Close_btn.Click(false);
        }
        public void AddNewOneTimeProducts(JobBOMData JobBOMData)
        {

            SelectOption(JobBOMData.Option);
            ExtentReportsHelper.LogInformation(null, $"Select Option : {JobBOMData.Option}.");
            SelectBuildingPhase(JobBOMData.BuildingPhase);
            ExtentReportsHelper.LogInformation(null, $"Select BuildingPhase : {JobBOMData.BuildingPhase}.");
            EnterName(JobBOMData.Name);
            ExtentReportsHelper.LogInformation(null, $"Input Name : {JobBOMData.Name}.");
            EnterDescription(JobBOMData.Description);
            ExtentReportsHelper.LogInformation(null, $"Input Description : {JobBOMData.Description}.");
            EnterQuantity(JobBOMData.Quantity);
            ExtentReportsHelper.LogInformation(null, $"Input Quantity : {JobBOMData.Quantity}.");
            EnterUnitCost(JobBOMData.UnitCost);
            ExtentReportsHelper.LogInformation(null, $"Input Unit Cost : {JobBOMData.UnitCost}.");
            SelectTaxStatus(JobBOMData.TaxStatus);
            ExtentReportsHelper.LogInformation(null, $"Select Tax Status : {JobBOMData.TaxStatus}.");
            EnterNotes(JobBOMData.Notes);
            ExtentReportsHelper.LogInformation(null, $"Input Notes : {JobBOMData.Notes}.");
            Save();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnOneTime']/div[1]");
        }
    }
}
