using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Manufactures
{
    public partial class ManufacturerPage
    {
        public bool IsNotDisplayButton(string role, string styleName)
        {
            switch (role)
            {
                case "Styles - Add":
                    Button AddStyle_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_hypAddNew']");
                    return !AddStyle_btn.IsDisplayed();

                case "Styles - Delete":
                    Button deleteButton = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgManufacturers_ctl00']/tbody/tr/td[a[text()='{styleName}']]/../td/input[@title='Delete']");
                    return !deleteButton.IsDisplayed();

                case "Styles - Edit":
                    Button editButton = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgManufacturers_ctl00']/tbody/tr/td[a[text()='{styleName}']]/../td/a[@title='Edit']");
                    return !editButton.IsDisplayed();

                default:
                    ExtentReportsHelper.LogInformation($"Can't find element {role} in the UI.");
                    return true;
            }
        }
    }
}
