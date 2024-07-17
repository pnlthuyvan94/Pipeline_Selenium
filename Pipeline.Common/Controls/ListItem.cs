using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Common.Controls
{
    public class ListItem
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IList<IWebElement> wrappedControls;
        private IList<string> wrappedControlsName;

        public IList<IWebElement> GetAllItems()
        {
            return wrappedControls;
        }

        public ListItem(IList<IWebElement> control)
        {
            this.wrappedControls = control;
        }

        public void Select(params string[] listItemName)
        {
            Select(GridFilterOperator.EqualTo, listItemName);
        }

        public void Select(GridFilterOperator filterOperator, params string[] listItemName)
        {
            IWebElement temp;
            Actions action = new Actions(BaseValues.DriverSession);

            foreach (var item in listItemName)
            {
                temp = getItemByName(item, filterOperator);
                if (temp != null)
                {
                    if (!temp.GetAttribute("class").Contains("rlbSelected"))
                    {
                        temp.Click();
                        action.KeyDown(Keys.Control).Build().Perform();
                        BasePage.JQueryLoad();
                    }
                }
                System.Threading.Thread.Sleep(667);
            }
            action.KeyUp(Keys.Control).Build().Perform();
            BasePage.JQueryLoad();
        }

        public bool IsItemExisted(GridFilterOperator filterOperator = GridFilterOperator.EqualTo, params string[] listItemName)
        {
            foreach (var item in listItemName)
            {
                if (getItemByName(item, filterOperator) == null)
                    return false;
            }
            return true;
        }

        private IWebElement getItemByName(string name, GridFilterOperator filterOperator = GridFilterOperator.EqualTo)
        {
            switch (filterOperator)
            {
                case GridFilterOperator.Contains:
                    return wrappedControls.Where(p => p.Text.Contains(name)).FirstOrDefault();
                case GridFilterOperator.StartsWith:
                    return wrappedControls.Where(p => p.Text.StartsWith(name) == true).FirstOrDefault();
                default:
                    return wrappedControls.Where(p => p.Text.Equals(name) == true).FirstOrDefault();
            }
        }

        public void SetChecked(GridFilterOperator filterOperator = GridFilterOperator.EqualTo, params string[] listItemName)
        {
            IWebElement temp;

            foreach (var item in listItemName)
            {
                temp = getItemByName(item, filterOperator);
                if (temp != null && !temp.Selected)
                    temp.FindElement(By.TagName("input")).Click();
            }
        }

        public List<string> GetSpecifiedItemsName(int numberOfItem)
        {
            return wrappedControls.Take(numberOfItem).Select(q => q.GetAttribute("textContent")).ToList();
        }

        public IList<string> GetAllItemsName()
        {
            return wrappedControls.Select(q => q.Text).ToList();
        }

    }
}