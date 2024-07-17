using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Assets.Options.Products
{
    public partial class ProductsToOptionPage
    {
        public bool IsOptionHouseQuantitiesValueShownCorrect(int row, OptionQuantitiesData data)
        {
            List<string> actualAttributesData = new List<string>();
            List<string> attributesData = new List<string>() { data.Community, data.House, data.Dependent_Condition, data.BuildingPhase, data.ProductName, data.Style,
                 data.Use, data.Parameters, data.Quantity, data.Source };
            for (int i = 1; i < attributesData.Count() + 1; i++)
            {
                Label valueAttributes = new Label(FindType.XPath, $"//table[@id = 'ctl00_CPH_Content_rgProductsToHouses_ctl00']/tbody/tr[{row}]/td[{i + 1}]");
                string valueFine = valueAttributes.GetText().Trim();
                actualAttributesData.Add(valueFine);
            }

            IEnumerable<string> differenceQuery = attributesData.Except(actualAttributesData);
            return differenceQuery.Count() > 0 ? false : true;
        }
    }
}
