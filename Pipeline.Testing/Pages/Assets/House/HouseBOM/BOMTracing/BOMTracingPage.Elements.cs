using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.House.HouseBOM.BOMTracing
{
    public partial class BOMTracingPage : DetailsContentPage<BOMTracingPage>
    {
        protected Button Mini_Btn => new Button(FindType.XPath, "//span[contains(.,'View Mode')]/span[2]");
        protected Button Normal_Btn => new Button(FindType.XPath, "//span[contains(.,'View Mode')]/span[1]");
        protected Button Simple_Btn => new Button(FindType.XPath, "//span[contains(.,'Chart Type')]/a[1]");
        protected Button Detailed_Btn => new Button(FindType.XPath, "//span[contains(.,'Chart Type')]/a[2]");
        protected Label MiniDetailedFinal_Lbl => new Label(FindType.XPath, "//img[@title= 'Final']");
        protected Label DetailedFinalBOMProduct_Lbl => new Label(FindType.XPath, "//img[@title= 'Final BOM Product']");
       
        protected Label DetailedSubFinal_Lbl => new Label(FindType.XPath, "//img[@title= 'Subfinal']");
        protected Label DetailedTotalSum_Lbl => new Label(FindType.XPath, "//img[@title= 'Total Sum']");
        protected Label MiniDetailedKeyMeasures_Lbl => new Label(FindType.XPath, "//div[@class= 'rocItem']/img[contains(@title,'Key Measure')]");
        protected Label NormalUIBomTrace => new Label(FindType.XPath, "//li[@class = 'rocNode rocRootNode class-final rocExpandedNode']");
        protected Label KeyMeasureBomTrace => new Label(FindType.XPath, "//span[contains(.,'Option Quantity')]/parent::span[@class ='Label2']");

    }
}
