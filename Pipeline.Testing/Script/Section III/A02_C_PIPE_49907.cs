using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using RestSharp.Contrib;
using System;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class A02_C_PIPE_49907 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        CommunityData _communitywith129character;
        CommunityData _communitywith128character;

        [SetUp]
        public void CreateTestData()
        {
            // Step 3: Populate all values
            _communitywith129character = new CommunityData()
            {
                Name = "R-QA Only Community Auto Test with 129 character",
                Division = "None",
                City = "Ho Chi Minh",
                Code= "R-QA Only Community Auto Test R-QA Only Community Auto Test R-QA Only Community Auto Test R-QA Only Community Auto Test With 128",
                //CityLink = "https://hcm.com",
                //Township = "Tan Binh",
                //County = "VN",
                //State = "IN",
                //Zip = "01010",
                //SchoolDistrict = "Hoang hoa tham",
                //SchoolDistrictLink = "http://hht.com",
                //Status = "Open",
                //Description = "Nothing to say v1",
                //DrivingDirections = "Nothing to say v2",
                //Slug = "R-QA-Only-Community-Auto",
            };
            _communitywith128character = new CommunityData()
            {
                Name = "R-QA Only Community Auto Test with 128 character",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "R-QA Only Community Auto Test R-QA Only Community Auto Test R-QA Only Community Auto Test R-QA Only Community Auto Test With128",
            };

        }

        [Test]
        [Category("Section_III")]
        public void A02_C_Assets_Community_ImportNotWorkingDuetoCodesOverAllowableLength()
        {
            // Step I. Create a new Community + Code larger than 128 characters
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I. Create a new Community + Code larger than 128 characters</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.1: Click to create a new community</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.GetItemOnHeader(DashboardContentItems.Add).Click();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.2: Insert code larger than 128 characters </b></font>");
            CommunityDetailPage.Instance.AddOrUpdateCommunity(_communitywith129character);
            string _expectedMessage = $"Could not create Community with name {_communitywith129character.Name}.";
            if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Could not create Community with name {_communitywith129character.Name} and recevied error message.</b></front>");
            }
            else
                ExtentReportsHelper.LogFail($"<font color='red'><b>Can create Community with name {_communitywith129character.Name} with larger than 128 characters</b></front>");
            CommunityDetailPage.Instance.CloseToastMessage();

            // Step II. Create a new Community + Code  is 128 characters
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II. Create a new Community + Code  is 128 characters</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.1: Insert code is 128 characters </b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.GetItemOnHeader(DashboardContentItems.Add).Click();
            CommunityDetailPage.Instance.AddOrUpdateCommunity(_communitywith128character);
            string _expectedCreateCommunity = $"Could not create Community with name {_communitywith128character.Name}.";
            if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedCreateCommunity)
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>Could not create Community with name {_communitywith129character.Name} and recevied error message.</b></front>");
            }
            CommunityDetailPage.Instance.CloseToastMessage();

            // Verify new Community is created with code is 128 characters
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.2: Verify new Community is created with code is 128 characters</b></font>");
            if (CommunityDetailPage.Instance.IsSaveCommunitySuccessful($"{_communitywith128character.Name} ({_communitywith128character.Code})") is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Create new Community unsuccessfully..</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Community is created sucessfully with URL: {CommunityDetailPage.Instance.CurrentURL}");
            }
            // Step III. Edit with Community + Code larger than 128 characters
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III. Edit Community + Code larger than 128 characters</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step III.1: Insert code larger than 128 characters</b></font>");
            
            //CommunityDetailPage.Instance.NavigateURL(CommunityDetailPage.Instance.CurrentURL);
            CommunityDetailPage.Instance.AddOrUpdateCommunity(_communitywith129character);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step III.2: Verify Code field larger than 128 characters</b></font>");
            string _expectedMessageEdit = $"Could not create Community with name {_communitywith129character.Name}.";
            if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessageEdit)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Could edit Community with name {_communitywith129character.Name} and recevied error message.</b></front>");
            }
            else
                ExtentReportsHelper.LogFail($"<font color='red'><b>Could not edit Community with name {_communitywith129character.Name} with larger than 128 characters</b></front>");
            CommunityDetailPage.Instance.CloseToastMessage();
            
            // Step IV. Edit with Community + Code is 128 characters
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>IV. Edit Community + Code is 128 characters</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step IV.1: Insert code is 128 characters</b></font>");

            CommunityDetailPage.Instance.NavigateURL("Builder/Communities/Community.aspx?cid=0");
            CommunityDetailPage.Instance.AddOrUpdateCommunity(_communitywith128character);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step III.2: Verify Code field is 128 characters</b></font>");
            string _expectedMessageEdit128 = $"Could not create Community with name {_communitywith128character.Name}.";
            if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessageEdit128)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Could edit Community with name {_communitywith128character.Name}.</b></front>");
            }
            else
                ExtentReportsHelper.LogFail($"<font color='red'><b>Could not edit Community with name {_communitywith128character.Name} with is 128 characters</b></front>");



        }


        [TearDown]
        public void DeleteCommunity()
        {
            // Back to community default page and delete data
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Insert name to filter and click filter by Contain value
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _communitywith128character.Name);

            bool isFound = CommunityPage.Instance.IsItemInGrid("Name", _communitywith128character.Name);
            if (isFound)
            {
                CommunityPage.Instance.DeleteItemInGrid("Name", _communitywith128character.Name);
                string successfulMess = $"Community { _communitywith128character.Name} deleted successfully!";
                string actual = CommunityPage.Instance.GetLastestToastMessage();
                if (successfulMess.Equals(actual))
                {
                    ExtentReportsHelper.LogPass("Updated Community deleted successfully!");
                    CommunityPage.Instance.CloseToastMessage();
                }
                else
                {
                    if (!CommunityPage.Instance.IsItemInGrid("Name", _communitywith128character.Name))
                        ExtentReportsHelper.LogPass("The Updated community deleted successfully!");
                    else
                        ExtentReportsHelper.LogFail($"The updated community could not be deleted. Actual message <i>{actual}</i>");
                }
            }
        }
    }
}
