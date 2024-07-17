using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A01_C_RT_01197 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private DivisionData _division;
        private string[] CommunityItems;

        [SetUp]
        public void CreateTestData()
        {
            _division = new DivisionData()
            {
                Name = "R-QA Only Division Auto - Community",
                Address = "3990 IN 38",
                City = "Lafayette",
                State = "IN",
                Zip = "47905",
                Description = "Create a new Division by automation",
            };

            // Get option name on row 4 and 5 to assign to option group
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Filter community that has no division and get it
            CommunityPage.Instance.FilterItemInGrid("Division", GridFilterOperator.IsEmpty, string.Empty);

            CommunityItems = new string[] { CommunityPage.Instance.GetCommunityNameByIndexAndColumn("Name", 3),
                CommunityPage.Instance.GetCommunityNameByIndexAndColumn("Name", 4),
            CommunityPage.Instance.GetCommunityNameByIndexAndColumn("Name", 5)};

            // Clear filter
            CommunityPage.Instance.FilterItemInGrid("Division", GridFilterOperator.NoFilter, string.Empty);
        }

        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        public void A01_C_Assets_DetailPage_Division_Community()
        {
            // Step 1: navigate to this URL: http://beta.bimpipeline.com/Dashboard/Builder/Divisions/Default.aspx
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_DIVISION_URL);

            // Step 2: Filter and select updated Division
            // Step 2: Filter and verify Division on the grid
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.Contains, _division.Name);
            if (DivisionPage.Instance.IsItemInGrid("Division", _division.Name) is false)
            {
                // if the division doesn't exist then create a new one
                DivisionPage.Instance.CreateDivision(_division);
            }
            else
            {
                // Select filter item to open detail page
                DivisionPage.Instance.SelectItemInGrid("Division", _division.Name);
            }

            // Verify the updated Division in header
            if (DivisionDetailPage.Instance.IsSaveDivisionSuccessful(_division.Name) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Division detail page is displayed sucessfully with URL: {DivisionDetailPage.Instance.CurrentURL}</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>The updated Division displays unsuccessfully.</font>");

            // Step 3: Open Communities navigation
            DivisionCommunityPage.Instance.LeftMenuNavigation("Communities");
            if (DivisionCommunityPage.Instance.IsDivisionPageDisplayed is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The Community in Division page isn't displayed or the title is incorrect." +
                    $"<br>Expected Title: Communities in Division</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The Community in Division page is displayed sucessfully.</b></font>");

            // Step 4: Click Add Community to Division button
            DivisionCommunityPage.Instance.OpenDivisionCommunityModal();

            // Step 5: Select community to add to current division
            DivisionCommunityPage.Instance.DivisionCommunityModal.SelectDivisionCommunity(CommunityItems).Save();

            // Verify message
            string savedMess = $"Successfully added {CommunityItems.Length} communities!";
            string actualSavedMess = DivisionCommunityPage.Instance.GetLastestToastMessage();
            if (savedMess == actualSavedMess)
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Add communities to division successfully!</b></font>");
                DivisionPage.Instance.CloseToastMessage();
            }

            // Step 6: Verify  new items which is added to the grid view
            DivisionCommunityPage.Instance.LeftMenuNavigation("Communities");
            System.Threading.Thread.Sleep(2000);

            foreach (string item in CommunityItems)
            {
                // Insert name to filter and click filter by Contain value
                DivisionCommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, item);
                if (DivisionCommunityPage.Instance.IsItemInGrid("Name", item) is false)
                    ExtentReportsHelper.LogFail($"<font color='red'>The community \"{item} \" was not display on grid.</font>");
            }

            // Clear filter
            DivisionCommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            // Remove community from current division
            RemoveCommunityFromDivision(CommunityItems, _division.Name);

        }

        private void RemoveCommunityFromDivision(string[] CommunityItems, string divisonName)
        {
            // Clear filter
            //DivisionCommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, "");
            //System.Threading.Thread.Sleep(3000);
            // Delete
            foreach (string item in CommunityItems)
            {
                // Delete that community
                DivisionCommunityPage.Instance.DeleteItemInGrid("Name", item);

                string deletedExpectedMess = $"Community {item} deleted from Division {divisonName}";
                if (deletedExpectedMess == DivisionCommunityPage.Instance.GetLastestToastMessage(30))
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Community {item} deleted from division {divisonName} successfully!</b></font>");
                    DivisionPage.Instance.CloseToastMessage();
                }
                else
                {
                    if (!DivisionCommunityPage.Instance.IsItemInGrid("Name", item))
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Community {item} deleted from division {divisonName} successfully!</b></font>");
                    else
                        ExtentReportsHelper.LogFail($"<font color='red'>Community {item} can't delete from division {divisonName}!</font>");
                }
            }
        }

        #endregion

        [TearDown]
        public void DeleteData()
        {
            // Step 7: Back to Division page and delete item
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Back to Division page and delete it.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_DIVISION_URL);
            DivisionPage.Instance.DeleteDivision(_division);
        }
    }
}
