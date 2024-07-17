using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Options;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A05_Z : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        [Test]
        [Category("Section_IV")]
        public void A05_Z_Assets_DetailPage_Options_Delete()
        {
            var optionName = "R-QA Only Option Auto";
            // Delete the Option
            // 7. Select item and click deletete icon
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionName);
            if (OptionPage.Instance.IsItemInGrid("Name", optionName))
                OptionPage.Instance.DeleteItemInGrid("Name", optionName);
            OptionPage.Instance.WaitGridLoad();
            string successfulMess = $"Option {optionName} deleted successfully!";
            string actualMsg = OptionPage.Instance.GetLastestToastMessage();
            if (successfulMess.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass("Option deleted successfully!");
                OptionPage.Instance.CloseToastMessage();
            }
            else 
            {
                ExtentReportsHelper.LogWarning($"Option could not be deleted - Possible constraint preventing deletion. Actual message <i>{actualMsg}</i>");
            }
        }
    }
}
