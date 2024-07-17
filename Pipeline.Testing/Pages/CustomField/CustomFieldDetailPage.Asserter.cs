using Pipeline.Common.BaseClass;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Pages.CustomField
{
    public partial class CustomFieldDetailPage
    {
        public bool VerifyCustomFieldIsDisplayedInPage(params CustomFieldData[] items)
        {
            string xpath = "";
            BaseControl control;
            bool isPassed = true;
            foreach (var item in items)
            {
                xpath = GetXpath(item);
                control = new SpecificControls(FindType.XPath, xpath);
                if (!control.IsExisted())
                {
                    ExtentReportsHelper.LogFail($"The Custom Field Title <font color='green'><b>{item.Title}</b></font> with type <font color='green'><b>{item.FieldType}</b></font> is not display on details page.");
                    isPassed = false;
                }
                else
                    ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(control), $"The Custom Field Title <font color='green'><b>{item.Title}</b></font> with type <font color='green'><b>{item.FieldType}</b></font> is displayed on details page as expectation.");
            }
            return isPassed;
        }

        public bool VerifyCustomFieldIsNOTDisplayedInPage(params CustomFieldData[] items)
        {
            string xpath = "";
            BaseControl control;
            bool isPassed = true;
            foreach (var item in items)
            {
                xpath = GetXpath(item);
                control = new SpecificControls(FindType.XPath, xpath);
                if (control.IsExisted())
                {
                    ExtentReportsHelper.LogFail($"The Custom Field Title <font color='green'><b>{item.Title}</b></font> with type <font color='green'><b>{item.FieldType}</b></font> is displayed on details page.");
                    isPassed = false;
                }
                else
                    ExtentReportsHelper.LogPass($"The Custom Field Title <font color='green'><b>{item.Title}</b></font> with type <font color='green'><b>{item.FieldType}</b></font> is NOT display on details page as expectation.");
            }
            return isPassed;
        }

        public bool VerifyTheTitleOnCustomFieldCorrect(string title)
        {
            if (HeaderTitle_Lbl.GetText().Equals(title))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(HeaderTitle_Lbl), $"The Custom Field page is displayed with Title <font color='green'><b>{title}</b></font>");
                return true;
            }
            ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(HeaderTitle_Lbl), $"The Custom Field page is displayed with wrong Title <font color='green'><b>{title}</b></font>");
            return false;
        }

        public bool VerifyCustomFieldIsDisplayedWithCorrectItems(params CustomFieldData[] items)
        {
            var currentItems = GetCurrentItems();
            bool isPassed = true;
            if (!currentItems.Count.Equals(items.Length))
            {
                IList<string> listActual = currentItems.Select(c => c.Title).ToList();
                IList<string> expectedList = items.Select(c => c.Title).ToList();

                ExtentReportsHelper.LogFail($"Expected list is <font color='green'><b>{items.Length}</b></font> but the actual is <font color='green'><b>{currentItems.Count}</b></font>.<br>The Actual items: {CommonHelper.CastListToString(listActual)}<br>The Expected items: {CommonHelper.CastListToString(expectedList)}.");
                return false;
            }
            foreach (var item in items)
            {
                if (!currentItems.Select(c => c.Title = item.Title).Count().Equals(1))
                {
                    ExtentReportsHelper.LogFail($"Expected item is <font color='green'><b>{item.Title}</b></font> but the actual has <font color='green'><b>{currentItems.Select(c => c.Title = item.Title).Count()}</b></font> items.");
                    isPassed = false;
                }
            }

            return isPassed;
        }

        public bool VerifyCustomItemsInList(IList<CustomFieldData> items)
        {
            // Wait until the modal display then verify it
            CommonHelper.WaitUntilElementInvisible("//h1[contains(text(), 'Add Community Custom Fields')]", 5);

            IList<string> listNames = CommonHelper.CastListControlsToListString(FindElementHelper.FindElements(FindType.XPath, listNameCustomFieldInModal_Xpah));
            IList<string> listItemFromSettingPage = new List<string>();
            foreach (var item in items)
            {
                if (item.Description == string.Empty)
                    listItemFromSettingPage.Add(item.Title + " -");
                else
                    listItemFromSettingPage.Add(item.Title + " - " + item.Description);

            }
            if (!CommonHelper.IsEqual2List(listNames, listItemFromSettingPage))
            {
                ExtentReportsHelper.LogFail($"Detail page: <font color='green'><b>{CommonHelper.CastListToString(listNames)}</b></font><br>Setting Page: <font color='green'><b>{CommonHelper.CastListToString(listItemFromSettingPage)}</b></font>");
                return false;
            }
            ExtentReportsHelper.LogPass($"The list item in modal on Detail page is displayed correctly. Items: <font color='green'><b>{CommonHelper.CastListToString(listNames)}</b></font>");
            return true;
        }
        public void VerifyHouseCustomFieldValues(string testNumber, bool testBoxCheck, string staticValue)
        {              
            if (TestNumberValue_Txb.GetAttribute("value").Trim().Equals(testNumber) && (TestCheckboxValue_Ckb.IsChecked == testBoxCheck) && StaticLabelValue_Lbl.GetText().Trim().Equals(staticValue))
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Values on the custom field are correct </font>");
               
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Values on the custom field are not correct </font>");
                
            }               
        }
       
        
    }
}
