using NUnit.Framework;
using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;

namespace Pipeline.Testing.Pages.Assets.Communities.Options.AddOptionCondition
{
    public partial class AddOptionConditionGrid
    {
        public AddOptionConditionGrid SelectOptionCondition(string optionCondition)
        {
            //OptionCondition_ddl.WaitForElementIsVisible(5);
            if (!string.IsNullOrEmpty(optionCondition))
                OptionCondition_ddl.SelectItem(optionCondition, false);
            //System.Threading.Thread.Sleep(1000);
            WaitCommunityHouseOptionGridLoad();
            return this;
        }

        public AddOptionConditionGrid AddCondition()
        {
            AddCondition_btn.Click();
           // System.Threading.Thread.Sleep(5000);
             WaitCommunityHouseOptionGridLoad();
            return this;
        }

        public AddOptionConditionGrid SelectOperator(string operatorItem)
        {
            if (!string.IsNullOrEmpty(operatorItem))
                Operator_ddl.SelectItem(operatorItem, false);
            System.Threading.Thread.Sleep(1000);
            return this;
        }

        public AddOptionConditionGrid AddOperator()
        {
            AddOperator_btn.Click();
            WaitCommunityHouseOptionGridLoad();
           // System.Threading.Thread.Sleep(5000);
            return this;
        }


        public AddOptionConditionGrid EnterConditionSalePrice(string price)
        {
            if (!string.IsNullOrEmpty(price))
                ConditionPrice_txt.SetText(price);
            return this;
        }

        public void Apply()
        {
            ApplyCondition_btn.Click();
            WaitCommunityHouseOptionGridLoad();
        }

        public void Cancel()
        {
            CancelCondition_btn.Click();
            WaitCommunityHouseOptionGridLoad();
        }

        public void VerifyHyperlinkToOption(string optionName)
        {
            string xpathOption = $"//*[@id='ctl00_CPH_Content_rgHouseOptions_ctl00']/tbody/tr/td/a[starts-with(text(),'{optionName}')]";
            IWebElement option = FindElementHelper.FindElement(FindType.XPath, xpathOption);
            if (option != null)
            {
                CommonHelper.OpenLinkInNewTab(option.GetAttribute("href"));
                CommonHelper.SwitchTab(1);
                PageLoad();
                Assert.That(OptionDetailPage.Instance.IsSaveOptionSuccessful(optionName), $"Open Option: {optionName} page successfully.");
                ExtentReportsHelper.LogPass($"Open Option: {optionName} page successfully with URL: {OptionDetailPage.Instance.CurrentURL}");

                CommonHelper.CloseCurrentTab();
                CommonHelper.SwitchTab(0);
            }
        }
    }
}