using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.UserMenu.Subscription;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class I01_RT_01105 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        SubscriptionData _subcription;
        [SetUp]
        public void GetData()
        {
            _subcription = new SubscriptionData()
            {
                Event1= "Added_Communityqwe",
                Event2= "Added_BaseCostqwe",
                Event3= "Added_BidCostqwe"
            };
        }
        [Test]
        [Category("Section_III")]
        public void I01_UserMenu_AddSubscription()
        {
            // 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Subscriptions.aspx
            SubscriptionPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Subscriptions);

            SubscriptionPage.Instance.OpenSubcriptionModal();
            //Assert.That(SubscriptionPage.Instance.AddSubscriptionModal.IsModalDisplayed(), "Choose Event to Subscribe to modal isn't displayed");
            if (SubscriptionPage.Instance.AddSubscriptionModal.IsModalDisplayed())
            {
                ExtentReportsHelper.LogPass("Choose Event to Subscribe to modal is displayed");
            }
            else
            {
                ExtentReportsHelper.LogFail("Choose Event to Subscribe to modal isn't displayed");
            }

            // 2: Populate all values - Click 'Save' Button
            string[] listItem = { _subcription.Event1, _subcription.Event2, _subcription.Event3};
            int[] listItemIndex = {1, 2, 3};
            string[] getlistItem = SubscriptionPage.Instance.AddSubscriptionModal.SelectEvent(listItem, listItemIndex);
            SubscriptionPage.Instance.AddSubscriptionModal.Save();

            // 3: Verify message
            string _expectedMessage = "Event(s) subscribed to successfully";
            string _actualDuplicateMessage = SubscriptionPage.Instance.GetLastestToastMessage();

            if (_actualDuplicateMessage == _expectedMessage)
            {
                foreach (var item in getlistItem)
                {
                        ExtentReportsHelper.LogPass($"Insert event with name {item} to subscribe successfully");
                        SubscriptionPage.Instance.CloseToastMessage();
                }
            }
            else 
            {
                ExtentReportsHelper.LogFail($"Don't display any message. Actual message {_actualDuplicateMessage}");
                ExtentReportsHelper.LogFail("Fail to insert event to subscribe.");
                //Assert.Fail("Fail to insert event to subscribe.");
            }

            // 4: Close Add Event to subcribe modal
            //SubscriptionPage.Instance.AddSubscriptionModal.Close();

            foreach (var item in getlistItem)
            {
                // 5: Verify the new event create successfully
                //SubscriptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, item);
                bool isFound = SubscriptionPage.Instance.IsItemInGrid("Name", item);
                System.Threading.Thread.Sleep(1000);
                //Assert.That(isFound);
                if (isFound)
                {
                    ExtentReportsHelper.LogPass(string.Format("New event \"{0}\" was display on grid.", item));
                }
                else
                {
                    ExtentReportsHelper.LogFail(string.Format("New event \"{0}\" was not display on grid.", item));
                }
                // 7: Delete Event
                DeleteEvent(item);
            }
        }

        public void DeleteEvent(string eventName)
        {
            // Select OK to confirm; verify successful delete and appropriate success message.
            SubscriptionPage.Instance.DeleteItemInGrid("Name", eventName);
            SubscriptionPage.Instance.GetLastestToastMessage();
            SubscriptionPage.Instance.RefreshPage();
            System.Threading.Thread.Sleep(1000);

            // Filter again to find out that deleted item is removed
            bool isFoundDeletedItem = SubscriptionPage.Instance.IsItemInGrid("Name", eventName);
            if (isFoundDeletedItem)
            {
                ExtentReportsHelper.LogFail("Event could not be deleted!");
                ExtentReportsHelper.LogFail($"The selected event: \"{eventName}\" was not deleted on grid.");
                //Assert.Fail($"The selected event: \"{eventName}\" was not deleted on grid.");
            }
            else
            {
                ExtentReportsHelper.LogPass("Event deleted successfully!");
            }

        }
    }
}
