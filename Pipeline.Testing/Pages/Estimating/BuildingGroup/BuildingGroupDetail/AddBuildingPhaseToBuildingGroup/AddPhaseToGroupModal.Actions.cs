using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.BuildingGroup.BuildingGroupDetail.AddBuildingPhaseToBuildingGroup
{
    public partial class AddPhaseToGroupModal
    {
        public AddPhaseToGroupModal AddPhaseToGroup(int numberOfBuildingPhase)
        {
            List<string> buildingGroupList = ListOfPhase_lst.GetSpecifiedItemsName(numberOfBuildingPhase);

            CheckBox buildingGroup;
            foreach (var phaseName in buildingGroupList)
            {
                buildingGroup = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rlbBuildingPhase']/div/ul/li/label/input[../span[contains(text(), '{phaseName}')]]");
                if (buildingGroup.IsNull())
                {
                    ExtentReportsHelper.LogFail($"The building group with name <font color='green'><b>{phaseName}</b></font> is not displayed on option list.");
                }
                else
                {
                    buildingGroup.Check();
                }
            }
            Save();

            return this;
        }


        private void Save()
        {
            AddPhaseToGroup_btn.Click();
        }

        public void CloseModal()
        {
            Cancel_btn.Click();
        }

        public List<string> GetSpecifiedItemInPhaseList(int numberOfBuildingPhase)
        {
            return ListOfPhase_lst.GetSpecifiedItemsName(numberOfBuildingPhase);
        }
    }
}
