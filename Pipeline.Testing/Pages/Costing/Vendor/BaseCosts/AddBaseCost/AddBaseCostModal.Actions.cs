using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Costing.Vendor.BaseCosts.AddBaseCost
{
    public partial class AddBaseCostModal
    {
        public AddBaseCostModal SelectProduct(string product)
        {
            Products_ddl.SelectItem(product);
            return this;
        }

        public AddBaseCostModal SelectStyle(string style)
        {
            Styles_ddl.SelectItem(style);
            return this;
        }

        public AddBaseCostModal EnterMaterialCost(string cost)
        {
            MaterialCost_txt.SetText(cost);
            return this;
        }

        public AddBaseCostModal EnterLaborCost(string cost)
        {
            LaborCost_txt.SetText(cost);
            return this;
        }

        public AddBaseCostModal AddCost()
        {
            AddCost_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbInsertNewCost']/div[1]");
            return this;
        }

        public AddBaseCostModal AddAllStyles()
        {
            AddAllStyles_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbInsertAllCosts']/div[1]");
            return this;
        }
    }
}
