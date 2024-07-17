using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Styles
{
    public partial class StylePage
    {
        public bool IsNotDisplayButton(string role, string styleName)
        {
            switch (role)
            {
                case "Styles - Add":
                    return !AddStyle_btn.IsDisplayed();

                case "Styles - Delete":
                    Button deleteButton = new Button(FindType.XPath, $"//tbody/tr[td[a[text()='{styleName}']]]/td/input[@title='Delete']");
                    return !deleteButton.IsDisplayed();

                case "Styles - Edit":
                    Button editButton = new Button(FindType.XPath, $"//tbody/tr[td[a[text()='{styleName}']]]/td/a/img[@alt='Edit']");
                    return !editButton.IsDisplayed();

                default:
                    ExtentReportsHelper.LogInformation($"Can't find element {role} in the UI.");
                    return true;
            }
        }
    }
}
