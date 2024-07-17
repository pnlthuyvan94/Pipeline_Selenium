using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Dashboard;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_II
{
    // Inherit the BaseTestScript
    public partial class RT_01019 : BaseTestScript
    {
        readonly string _compareMessage = @"The Actual and Expected value does not match.<br>Actual Result  :'{1}'<br>Expected Result:'{0}'.";

        bool Flag = true;
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_II);
        }

        #region"Test case"
        // The testcase run only 1 time
        [Test]
        [Category("Section_II"), Order(10)]
        public void J_NotificationMenu()
        {
            // Hover mouse to NOTIFICATION
            DashboardPage.Instance.SelectMenu(MenuItems.NOTIFICATION, true);
            // Verify Pathway list
            IList<string> expected = new List<string>(ExcelFactory.GetListByColumn(DashboardPage.Instance.TestData_RT01019, "Value"));
            IList<string> actual = DashboardPage.Instance.GetListNOTIFICATION;
            IList<string> dismiss_only = expected;
            dismiss_only.RemoveAt(1);
            Assert.That(CommonHelper.IsEqual2List(expected, actual) || CommonHelper.IsEqual2List(dismiss_only, actual), "The list items of NOTIFICATION menu is not as expected. Actual results: " + CommonHelper.CastListToString(actual));

            //********************* Verify URL ************************//////

            //If there are no notifications then only the "Dismiss All" item may be displayed, which means no further action needed 
            if (!CommonHelper.IsEqual2List(dismiss_only, actual)) {
                // Click on ViewAll from NOTIFICATION menu
                DashboardPage.Instance.SelectMenu(MenuItems.NOTIFICATION).SelectItem(NotificationMennu.ViewAll);

                string _current_URL = DashboardPage.Instance.CurrentURL;
                //The URL displayed for Notifications has the user's custom name, rather than their full username
                string _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_MANAGE_ALL_PO_URL;
                if (!_current_URL.Contains(_expected_URL))
                {
                    ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                    Flag = false;
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Notification Page URL: {_current_URL}");
                }
            }
            CommonHelper.RefreshPage();
            // Assert the testscript
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }
        #endregion

    }
}


