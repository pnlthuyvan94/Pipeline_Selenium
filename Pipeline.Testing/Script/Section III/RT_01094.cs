using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Purchasing.ReleaseGroup;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class E01_RT_01094 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        ReleaseGroupData _data;

        [SetUp]
        public void GetTestData()
        {
            _data = new ReleaseGroupData()
            {
                Name = "RT-QA_Release Group",
                Description = "Release Group Test",
                SortOrder = "1",
            };
        }

        [Test]
        [Category("Section_III")]
        public void E01_Purchasing_AddReleaseGroup()
        {
            // Navigate to this URL:http://dev.bimpipeline.com/Dashboard/Purchasing/ReleaseGroups/ReleaseGroups.aspx
            ReleaseGroupPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.ReleaseGroups);

            // Delete item before creating a new one
            ReleaseGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, _data.Name);
            if (ReleaseGroupPage.Instance.IsItemInGrid("Name", _data.Name))
            {
                ReleaseGroupPage.Instance.DeleteItemInGrid("Name", _data.Name);
                ReleaseGroupPage.Instance.WaitGridLoad();
            }

            // Create a new Release Group
            string _expectedMessage = "Release Group successfully added.";
            ReleaseGroupPage.Instance.AddReleaseGroup(_data, _expectedMessage, false);

            // Close the modal
            if (ReleaseGroupPage.Instance.AddReleaseGroupModal.IsModalDisplayed())
                ReleaseGroupPage.Instance.CloseModal();

            // Insert name to filter by Equal value
            ReleaseGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, _data.Name);

            if (ReleaseGroupPage.Instance.IsItemInGrid("Name", _data.Name) is false)
                ExtentReportsHelper.LogFail($"<font color = 'red'>Release Group \"{_data.Name} \" was not display on grid.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Release Group \"{_data.Name} \" displays successfully on grid.</b></font>");


            // Create a duplicate Release Group
            _expectedMessage = "Release Group could not be added.";
            ReleaseGroupPage.Instance.AddReleaseGroup(_data, _expectedMessage, true);

            // Close the modal
            if (ReleaseGroupPage.Instance.AddReleaseGroupModal.IsModalDisplayed())
                ReleaseGroupPage.Instance.CloseModal();

        }

        [TearDown]
        public void DeleteData()
        {
            // Delete the set up data
            ReleaseGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, _data.Name);
            if (ReleaseGroupPage.Instance.IsItemInGrid("Name", _data.Name) is true)
            {
                // Create a new Release Group if it doesn't exist
                ReleaseGroupPage.Instance.DeleteReleaseGroupByName(_data.Name);
            }
        }
    }
}
