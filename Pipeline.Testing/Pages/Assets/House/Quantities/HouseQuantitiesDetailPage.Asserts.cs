using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.House.Quantities
{
    public partial class HouseQuantitiesDetailPage
    {
        public bool IsHouseQuantitiesValueShownCorrect(int row, QuantitiesDetailData data)
        {
            List<string> actualAttributesData = new List<string>();
            List<string> attributesData = new List<string>() { data.Option, data.DependentCondition, data.BuildingPhase, data.Products, data.Description, 
                data.Style, data.Use, data.Parameters, data.Quantity, data.Source };
            for (int i = 1; i < attributesData.Count() + 1; i++)
            {
                Label valueAttributes = new Label(FindType.XPath, $"//table[@id = 'ctl00_CPH_Content_rgProductsToHouses_ctl00']/tbody/tr[{row}]/td[{i + 1}]");
                string valueFine = valueAttributes.GetText().Trim();
                actualAttributesData.Add(valueFine);
            }
            
            IEnumerable<string> differenceQuery = attributesData.Except(actualAttributesData);
            return differenceQuery.Count()>0 ? false:true; 
        }
        
        public bool VerifyHouseQuantitiesIsNotDisplay()
        {
            if (NoHouseQuantitiesData_lbl.IsDisplayed())
            {
                ExtentReportsHelper.LogPass($"<font color='green'>No House Quantities Data In grid</font>");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>House Quantities Data is displayed In grid </font>");
                return false;
            }
        
        }
    }
}

