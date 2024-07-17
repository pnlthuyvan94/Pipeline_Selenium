using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using System;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.UserMenu.Subscription.AddSubscription
{
    public partial class AddSubscriptionModal
    {
        public string[] SelectEvent(string[] listEvent, int[] listindex)
        {

            if (listEvent.Length != 0 && Event_lt.IsItemExisted(GridFilterOperator.EqualTo, listEvent))
            {
                Event_lt.Select(listEvent);
                return listEvent;
            }
            else
            {
                List<String> getListEvent = new List<string>() { };
                string[] newlistEvent = new string[] { };
                foreach (var index in listindex)
                {
                    string listXpath = $"//*[@class='rlbGroup rlbGroupRight']//ul[@class='rlbList']/li[{index}]/span";
                    Textbox Item = new Textbox(FindType.XPath, listXpath);
                    string Itemvalue = Item.GetText().ToString();
                    getListEvent.Add(Itemvalue);
                    newlistEvent = getListEvent.ToArray();
                }
                Event_lt.Select(newlistEvent);
                return newlistEvent;
            }

        }

    public void Save()
        {
            Save_btn.Click();
            subdescription_load.WaitForElementIsInVisible(5);
        }

        public void Close()
        {
            Close_btn.Click();
            ModalTitle_lbl.WaitForElementIsInVisible(5);
        }
    }
}