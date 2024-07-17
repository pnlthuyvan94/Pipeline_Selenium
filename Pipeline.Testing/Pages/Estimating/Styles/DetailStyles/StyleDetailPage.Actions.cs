using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Styles.DetailStyles
{
    public partial class StyleDetailPage
    {
        public StyleDetailPage EnterStyleName(string name)
        {
            if (string.IsNullOrEmpty(name) is false)
                Name_txt.SetText(name);
            return this;
        }
        public string EnteManufacturer(string manufacturer)
        {
            return Manufacturer_ddl.SelectItemByValueOrIndex(manufacturer, 1);
        }


        public StyleDetailPage EnterStyleUrl(string url)
        {
            if (string.IsNullOrEmpty(url) is false)
                Url_txt.SetText(url);
            return this;
        }

        public StyleDetailPage EnterStyleDescription(string description)
        {
            if (string.IsNullOrEmpty(description) is false)
                Description_txt.SetText(description);
            return this;
        }

        public void ClickAddManufaturerButton()
        {
            AddManufacturer_btn.Click();
        }

        public void Save()
        {
            StyleSave_btn.Click();
            // Loading grid
            WaitingLoadingGifByXpath(loadingIcon);
        }

        public StyleData UpdateStyle(StyleData data)
        {
            EnterStyleName(data.Name);
            data.Manufacturer = EnteManufacturer(data.Manufacturer);
            EnterStyleUrl(data.Url)
            .EnterStyleDescription(data.Description).Save();
            string actualMessage = StyleDetailPage.Instance.GetLastestToastMessage();
            string expectedMessage = $"Style {data.Name} saved successfully!";
            if (expectedMessage == actualMessage)
            {
                ExtentReportsHelper.LogPass("Update successfully. The toast message same as expected.");
                CloseToastMessage();

            }
            else
            {
                ExtentReportsHelper.LogFail($"Toast message must be same as expected. Actual: {actualMessage}");
                CloseToastMessage();
            }

            StyleData newStyle = new StyleData(data)
            {
                Manufacturer = data.Manufacturer
            };

            return newStyle;
        }

        public StyleData CreateStyle(StyleData data)
        {
            EnterStyleName(data.Name);
            data.Manufacturer = EnteManufacturer(data.Manufacturer);
            EnterStyleUrl(data.Url).EnterStyleDescription(data.Description);
            StyleData newStyle = new StyleData(data)
            {
                Manufacturer = data.Manufacturer
            };
            return newStyle;
        }

    }
}
