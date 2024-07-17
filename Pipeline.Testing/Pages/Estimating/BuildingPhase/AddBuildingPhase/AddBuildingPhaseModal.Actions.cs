using OpenQA.Selenium;

namespace Pipeline.Testing.Pages.Estimating.BuildingPhase.AddBuildingPhase
{
    public partial class AddBuildingPhaseModal
    {
        public AddBuildingPhaseModal EnterPhaseCode(string code)
        {
            PhaseCode_txt.SetText(code);
            return this;
        }

        public AddBuildingPhaseModal EnterPhaseName(string name)
        {
            PhaseName_txt.SetText(name);
            return this;
        }

        public AddBuildingPhaseModal EnterAbbName(string abbName)
        {
            PhaseAbbName_txt.SetText(abbName);
            return this;
        }

        public AddBuildingPhaseModal EnterDescription(string description)
        {
            PhaseDescription_txt.SetText(description);
            return this;
        }

        public string SelectGroup(string group)
        {
            return PhaseBuildingGroup_ddl.SelectItemByValueOrIndex(group, 1);
        }

        public string SelectType(string type)
        {
            return PhaseType_ddl.SelectItemByValueOrIndex(type, 1);
        }

        public string SelectParent(string parent)
        {
            return PhaseParents_ddl.SelectItemByValueOrIndex(parent, 1);
        }

        public AddBuildingPhaseModal EnterPercentBilled(string percent)
        {
            PhasePercentBill_txt.SetText(percent);
            return this;
        }

        public AddBuildingPhaseModal IsTaxable(bool check)
        {
            if (check) PhaseTaxable_chk.Check();
            return this;
        }

        public string SelectTaskForPayment(string activity)
        {
            return SchedulingTaskForPayment_ddl.SelectItemByValueOrIndex(activity, 1);
        }

        public string SelectTaskForPO(string activity)
        {
            return TaskForPODisplay_ddl.SelectItemByValueOrIndex(activity, 1);
        }

        public AddBuildingPhaseModal ClickTaxableYes()
        {
            TaxableYes_rbtn.Click();
            return this;
        }

        public AddBuildingPhaseModal ClickTaxableNo()
        {
            TaxableNo_rbtn.Click();
            return this;
        }
        public void Save()
        {
            PhaseSave_btn.Click();
            BuildingPhase_Grid.WaitGridLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlNewBuildingPhase']");
        }

        public void CloseModal()
        {
            PhaseClose_btn.Click();
            System.Threading.Thread.Sleep(1000);
        }
    }
}
