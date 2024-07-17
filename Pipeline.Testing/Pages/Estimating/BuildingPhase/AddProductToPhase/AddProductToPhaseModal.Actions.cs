using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.BuildingPhase.AddProductToPhase
{
    public partial class AddProductToPhaseModal
    {
        
        public void SelectProduct(string product, int index)
        {
            Button arrowProduct_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcbProductId_Arrow']");
            arrowProduct_btn.Click();
            Textbox Product_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId_Input']");
          
            Product_txt.SetText(product);

            Button Product_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']//ul/li/em[contains(text(),'{product}')]");
            Button OpenProduct_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rcbProductIdPanel']");

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId_LoadingDiv']", 3000);
            if (Product_btn.IsDisplayed() is true)
            {
                Product_btn.WaitForElementIsVisible(2);
                Product_btn.Click();
            }
            else
            {
                Button ProductRandom_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']//ul/li[{index}]");
                Product_txt.Clear();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId_LoadingDiv']", 3000);
                OpenProduct_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId_LoadingDiv']", 3000);
                ProductRandom_btn.Click();
            }
        }
        public string SelectTaxStatus(string status, int index)
        {
            return TaxStatus_ddl.SelectItemByValueOrIndex(status, index);
        }
        public AddProductToPhaseModal SetDefault(bool check)
        {
            if (check) SetDefault_chkbox.Check();
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            System.Threading.Thread.Sleep(1000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlPhase']/div[1]");
        }

        
    }
}
