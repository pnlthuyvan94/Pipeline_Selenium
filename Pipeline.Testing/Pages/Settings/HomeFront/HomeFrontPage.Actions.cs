using Pipeline.Common.Pages;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.HomeFront
{
    public partial class HomeFrontPage : DetailsContentPage<HomeFrontPage>
    {
        public void SetStatus(bool value)
        {
            Button selectedItem;
            Button saveBuilderMTSetting = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSave']");
            if (value is true)
                selectedItem = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rbStatus_0']");
            else
                selectedItem = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rbStatus_1']");

            if (selectedItem.IsExisted(false) is true && saveBuilderMTSetting.IsDisplayed(false) is true)
            {
                selectedItem.Click();
                saveBuilderMTSetting.Click();

                string expectedMsg = "Changes saved!";
                string actualMsg = GetLastestToastMessage();
                if (expectedMsg.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>HomeFront setting</b> saved successfully.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogInformation(null, "<font color = 'yellow'>Can't get the toast message or message content isn't same as the expectation." +
                        $"<br>The expected message: {expectedMsg}" +
                        $"<br>The actual message: {actualMsg}</br>");
                }
            }
            else
                ExtentReportsHelper.LogWarning(null, $"Can't find 'Status' or 'Save' button to update.");
        }

    }
}
