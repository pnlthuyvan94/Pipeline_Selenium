using NUnit.Framework;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Uses.AddUses;
using Pipeline.Testing.Pages.Estimating.Uses;
using Pipeline.Testing.Based;
using Pipeline.Common.BaseClass;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B10_RT_01084 : BaseTestScript
    {
        public static bool isDuplicated;

        UsesData _use;
        
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        [SetUp]
        public void SetUp()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/Uses/Default.aspx
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);

            _use = new UsesData()
            {
                Name = "QA_USE-RT",
                Description = "'QA_USE-RT - Do not Modify",
                SortOrder = "1"
            };
        }

        [Test]
        [Category("Section_III")]
        public void B10_Estimating_AddUse()
        {
            // Delete the Use data first if it still exists from a previous test
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _use.Name);
            System.Threading.Thread.Sleep(3000);
            bool isFound = UsesPage.Instance.IsItemInGrid("Name", _use.Name);
            if (isFound)
                DeleteUses();

            // Step 2: click on "+" Add button
            UsesPage.Instance.CreateNewUse(_use);

            // Step 3. Create with existing name
            UsesPage.Instance.ClickAddToUsesModal();
            System.Threading.Thread.Sleep(3000);
            UsesPage.Instance.AddUsesModal.FillDataAndSave(_use);
            string _expectedMessage = $"Cannot create Use with duplicate name: {_use.Name}";
            string _actualMessage = UsesPage.Instance.GetLastestToastMessage();
            isDuplicated = false;
            if (_actualMessage != _expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Created duplicated Use with name { _use.Name}, description {_use.Description} and Sort Order {_use.SortOrder}.");
                isDuplicated = true;
            }
            ExtentReportsHelper.LogPass("The Use could not create with the existed number. The message is dispalyed as expected: " + _actualMessage);
            UsesPage.Instance.CloseToastMessage();

            // Step 4. Close the Adding modal
            UsesPage.Instance.AddUsesModal.CloseModal();

            // Step 5. Insert name to filter and click filter by Contain value
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _use.Name);
            System.Threading.Thread.Sleep(3000);

            if(UsesPage.Instance.IsItemInGrid("Name", _use.Name) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>New Use {_use.Name} was not display on grid.</font>");
            }
        }

        [TearDown]
        public void DeleteUses()
        {
            if (UsesPage.Instance.IsItemInGrid("Name", _use.Name))
            {
                UsesPage.Instance.DeleteUses(_use.Name);
            }
        }
    }
}
