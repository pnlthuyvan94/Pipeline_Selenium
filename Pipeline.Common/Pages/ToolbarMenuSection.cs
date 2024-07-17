using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Common.Pages
{
    public class ToolbarMenuSection : BasePage
    {
        protected ToolbarMenuSection() : base() { }

        /// <summary>
        /// Select item on Menubar, if isClick is true, the system will capture this step
        /// </summary>
        /// <param name="Item Name"></param>
        /// <param name="is Click/Hover"></param>
        /// <returns></returns>
        public Menu SelectMenu(MenuItems item, bool isClick = false)
        {
            CommonHelper.ScrollToBeginOfPage();

            if (BaseValues.DriverSession.Url.ToLower().Contains("login"))
            {
                CommonFuncs.LoginToPipeline(ConfigurationManager.GetValue(BaseConstants.UserName), ConfigurationManager.GetValue(BaseConstants.Password));
            }

            switch (item)
            {
                case MenuItems.NOTIFICATION:
                    {
                        Menu _menu = new Menu(item)
                        {
                            Header = new SpecificControls(FindType.XPath, "//*[@id='aspnetForm']/header/div[contains(@class,'dropdown notification')]/a[@id='ctl00_Notifications1_lbnotificationslink']")
                        };

                        if (_menu.Header is null)
                        {
                            throw new NoSuchElementException(string.Format("Could not find menu with name - {0} - on your application.", item.ToString("g")));
                        }
                        if (isClick)
                        {
                            _menu.Header.Click();
                            Button firstItem = new Button(FindType.XPath, "//a[contains(@href,'Notifications.aspx?u')] | //li[contains(@id,'liMessage')]");
                            firstItem.WaitForElementIsVisible(8);

                            return _menu;
                        }

                        _menu.Header.HoverMouseWithoutCapture();

                        return _menu;
                    }
                case MenuItems.PROFILE:
                    {
                        Menu _menu = new Menu(item)
                        {
                            Header = new SpecificControls(FindType.XPath, "//*[@id='aspnetForm']/header/div[contains(@class,'dropdown user-menu')]")
                        };
                        if (_menu.Header is null)
                            throw new NoSuchElementException(string.Format("Could not find menu with name - {0} - on your application.", item.ToString("g")));

                        if (isClick)
                        {
                            _menu.Header.Click();

                            return _menu;
                        }
                        else
                        {
                            _menu.Header.HoverMouseWithoutCapture();
                        }

                        return _menu;
                    }
                case MenuItems.SALESPRICING:
                    {
                        Menu _menu = new Menu(item)
                        {
                            Header = new SpecificControls(FindType.XPath, "//header/nav[@role='navigation']/ul/li/a[translate(@title,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz') = translate('Sales Pricing','ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')]")
                        };

                        if (_menu.Header is null)
                        {
                            throw new NoSuchElementException(string.Format("Could not find menu with name - {0} - on your application.", item.ToString("g")));
                        }
                        if (isClick)
                        {
                            _menu.Header.Click();

                            return _menu;
                        }

                        _menu.Header.HoverMouseWithoutCapture();

                        return _menu;
                }
                case MenuItems.PURCHASING: {
                    Menu _menu = new Menu(item) {
                        Header = new SpecificControls(FindType.XPath, "//header/nav[@role='navigation']/ul/li/a[translate(@title,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz') = translate('Purchasing','ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')]")
                    };

                    if (_menu.Header is null) {
                        throw new NoSuchElementException(string.Format("Could not find menu with name - {0} - on your application.", item.ToString("g")));
                    }
                    if (isClick) {
                        CommonHelper.force_display_block(_menu.Header.GetWrappedControl(), true);
                        CommonHelper.force_visible(_menu.Header.GetWrappedControl(), true);
                        _menu.Header.Click();

                        return _menu;
                    }

                    _menu.Header.HoverMouseWithoutCapture();
                    CommonHelper.force_display_block(_menu.Header.GetWrappedControl(), true);
                    CommonHelper.force_visible(_menu.Header.GetWrappedControl(), true);

                    return _menu;
                }
                default:
                    {
                        Menu _menu = new Menu(item)
                        {
                            Header = new SpecificControls(
                                    FindType.XPath,
                                    @"//header/nav[@role='navigation']/ul/li/a[translate(@title,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz') = translate('"
                                    + item.ToString("g")
                                    + "','ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')]")
                        };

                        if (_menu.Header.IsNull())
                        {
                            throw new NoSuchElementException(string.Format("Could not find menu with name - {0} - on your application.", item.ToString("g")));
                        }
                        if (isClick)
                        {
                            CommonHelper.force_display_block(_menu.Header.GetWrappedControl(), true);
                            CommonHelper.force_visible(_menu.Header.GetWrappedControl(), true);
                            _menu.Header.Click();

                            return _menu;
                        }

                        _menu.Header.HoverMouseWithoutCapture();
                        CommonHelper.force_display_block(_menu.Header.GetWrappedControl(), true);
                        CommonHelper.force_visible(_menu.Header.GetWrappedControl(), true);

                        return _menu;
                    }
            }
        }

    }

    public class Menu
    {
        internal Menu(MenuItems menu)
        {
            Item = menu;
        }
        protected MenuItems Item { get; }
        internal SpecificControls Header { get; set; }

        /// <summary>
        /// Click item on menu after hover mouse to header menu
        /// </summary>
        /// <param name="item Name" which you want to click (Using MenuEnums/__Menu)></param>
        /// <param name="is Click / Hover only"></param>
        public void SelectItem(string itemOnMenu, bool isClick = true, bool isOpenNewTab = false)
        {
            CommonHelper.ScrollToBeginOfPage();

            switch (Item)
            {
                case MenuItems.NOTIFICATION:
                    {
                        DropdownMenu notification = new DropdownMenu(FindType.Id, "ctl00_Notifications1_lbnotificationslink");
                        if (isClick)
                        {
                            IWebElement item = CommonHelper.find_element_weak(notification.GetWrappedControl(), By.XPath(string.Format("./../ul/*/a[text()='{0}']", itemOnMenu)));
                            //var item = notification.FindElement(FindType.XPath, string.Format("./../ul/*/a[text()='{0}']", itemOnMenu));

                            if (isOpenNewTab)
                                CommonHelper.OpenLinkInNewTab(item.GetAttribute("href"));
                            else
                                CommonHelper.click_element(item, false);
                        }
                    }
                    break;

                case MenuItems.PROFILE:
                    {
                        DropdownMenu profile = new DropdownMenu(FindType.Id, "user-menu");

                        if (isClick)
                            profile.SelectItem(itemOnMenu, isOpenNewTab);
                        break;
                    }
                default:
                    {

                        // If the item which you want to list is display on menu list
                        // Click on the sub item
                        var _xpathItem = @"./../ul/li/a[translate(normalize-space(text()),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')=translate('"
                                        + itemOnMenu
                                        + "','ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')]";
                        // If the sub item is not exist on the menu
                        IWebElement _itemNeedToClick = null;
                        try
                        {
                            _itemNeedToClick = CommonHelper.find_element_weak(Header.GetWrappedControl(), By.XPath(_xpathItem));
                            //_itemNeedToClick = Header.FindElement(FindType.XPath, _xpathItem, 2);
                        }
                        catch (NoSuchElementException)
                        {
                            // Only cactch No Such Element Exception here
                            Console.WriteLine("We already catch this exception on next statement.");
                        }

                        if (isClick && _itemNeedToClick != null)
                        {
                            Link itemNeedToClick = new Link(_itemNeedToClick);

                            // Redirect to new page or open new tab
                            if (isOpenNewTab)
                                CommonHelper.OpenLinkInNewTab(itemNeedToClick.GetAttribute("href"));
                            else {
                                CommonHelper.force_display_block(itemNeedToClick.GetWrappedControl(), true);
                                CommonHelper.force_visible(itemNeedToClick.GetWrappedControl(), true);
                                itemNeedToClick.Click();
                            }
                        }
                        else
                        {
                            // The item on the sub menu => hover the item on list => show submenu => click item
                            // 1. Find father item which have sub item which name = itemNeedToClick
                            var _xpathFatherSubItem = @"./../ul/li[./ul/li/a/text()[contains(translate(.,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz') ,translate('"
                                                      + itemOnMenu
                                                      + "','ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))]]/a";
                            Link fatherItem;
                            if (itemOnMenu == ReportsMenu.OptionCost)
                            {
                                fatherItem = new Link(Header.FindElements(FindType.XPath, _xpathFatherSubItem).LastOrDefault());
                            }
                            else
                                fatherItem = new Link(Header.FindElement(FindType.XPath, _xpathFatherSubItem));

                            if (fatherItem.GetWrappedControl() is null)
                                throw new NoSuchElementException(string.Format("Could not find item with name - {0} - on your Menu.", itemOnMenu));

                            //CommonHelper.MoveToElementWithoutCaptureAndCenter(fatherItem.GetWrappedControl());

                            // 2. Hover to show list child items
                            fatherItem.HoverMouse();
                            CommonHelper.force_display_block(fatherItem.GetWrappedControl(), true);
                            CommonHelper.force_visible(fatherItem.GetWrappedControl(), true);

                            // 3. Find exactly item what need to Click
                            var _xpathSubItem = @"./../ul/li/ul/li/a[./text()[contains(translate(.,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz') ,translate('"
                                                + itemOnMenu
                                                + "','ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))]]";

                            // 4. Click/Hover item
                            Link item;
                            if (itemOnMenu == ReportsMenu.OptionCost)
                            {
                                item = new Link(Header.FindElements(FindType.XPath, _xpathSubItem)[1]);
                            }
                            else
                            {
                                item = new Link(Header.FindElement(FindType.XPath, _xpathSubItem));
                            }

                            if (item.GetWrappedControl() is null)
                                throw new NoSuchElementException(string.Format("Could not find item with name - {0} - on your Menu.", itemOnMenu));

                            if (!isClick)
                            {
                                item.HoverMouse();
                                CommonHelper.force_display_block(item.GetWrappedControl(), true);
                                CommonHelper.force_visible(item.GetWrappedControl(), true);
                            } else {
                                //CommonHelper.MoveToElementWithoutCaptureAndCenter(item.GetWrappedControl());

                                // Redirect to new page or open new tab
                                if (isOpenNewTab)
                                    CommonHelper.OpenLinkInNewTab(item.GetAttribute("href"));
                                else {
                                    CommonHelper.force_display_block(item.GetWrappedControl(), true);
                                    CommonHelper.force_visible(item.GetWrappedControl(), true);
                                    item.Click();
                                }
                            }
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// This function can use for the hover item purpose 
        /// </summary>
        /// <param name="which menu you want to get list"></param>
        /// <returns>List item on Menu</returns>
        public IList<IWebElement> GetListItemsOnMenu(string itemOnMenu = null)
        {
            if (itemOnMenu == null)
            {
                #region "Find menu and hover => TODO: review this method"
                // Find the item
                //var _itemXpath = @"./ul/li/a[translate(@title,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz') = translate('"
                //                 + Item.ToString("g")
                //                 + "','ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')]/span";

                //Link menu = null;
                //try
                //{
                //    menu = new Link(Header.FindElement(By.XPath(_itemXpath)));
                //}
                //catch (NoSuchElementException)
                //{
                //    // Only cactch No Such Element Exception here
                //    throw new NoSuchElementException(string.Format("Could not find item with name - {0} - on your Menu.", itemOnMenu));
                //}

                // Hover mouse to Menu if the sub item is hidden if this menu is displayed => do not need hover again
                //menu.HoverMouse();
                #endregion

                // FindChildByXPath only return the 1st result
                var _listItems = Header.FindElements(FindType.XPath, "./../ul/li/a").ToList();
                return _listItems;
            }
            // If the item need to find childs is a child level 1
            else
            {
                // Find the sub item 1 on menu items
                var _subItem1Xpath = @"./../ul/li/a[translate(normalize-space(text()),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')=translate('"
                                    + itemOnMenu
                                    + "','ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')]";

                Link _subItem1 = null;
                try
                {
                    _subItem1 = new Link(Header.FindElement(FindType.XPath, _subItem1Xpath));
                }
                catch (NoSuchElementException)
                {
                    // Only cactch No Such Element Exception here
                    throw new NoSuchElementException(string.Format("Could not find item with name - {0} - on your Menu.", itemOnMenu));
                }

                CommonHelper.MoveToElementWithoutCaptureAndCenter(_subItem1.GetWrappedControl());

                // Hover mouse to subItem 1
                _subItem1.HoverMouse();
                CommonHelper.force_display_block(_subItem1.GetWrappedControl(), true);
                CommonHelper.force_visible(_subItem1.GetWrappedControl(), true);

                System.Threading.Thread.Sleep(333);

                // Return list items
                var _xpathListItems = @"./../ul/li[./a[translate(normalize-space(text()),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')=translate('"
                                      + itemOnMenu
                                      + "','ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')]]/ul/li/a";

                var _listItems = Header.FindElements(FindType.XPath, _xpathListItems).ToList();

                return _listItems;
            }

        }

        /// <summary>
        /// Get list item Name on Menu. "item Name" which you want to click (Using MenuEnums/__Menu)
        /// </summary>
        /// <param name="item Name" which you want to click (Using MenuEnums/__Menu)></param>
        /// <returns></returns>
        public IList<string> GetListNameOnMenu(string itemOnMenu = null)
        {
            CommonHelper.ScrollToBeginOfPage();

            switch (Item)
            {
                case MenuItems.NOTIFICATION:
                    {
                        DropdownMenu notification = new DropdownMenu(FindType.Id, "ctl00_Notifications1_lbnotificationslink");
                        var listNotiItem = notification.FindElements(FindType.XPath, "./../ul/*/a");
                        int timeout = 0;
                        while (listNotiItem.Count() <= 1 && timeout < 6)
                        {
                            System.Threading.Thread.Sleep(667);
                            listNotiItem = notification.FindElements(FindType.XPath, "./../ul/*/a", 4);
                            timeout++;
                        }
                        CommonHelper.VisibilityOfAllElementsLocatedBy(5, "//*[@id='ctl00_Notifications1_rptNotifications_ctl00_hypViewAll']");
                        var listString = CommonHelper.CastListControlsToListString(listNotiItem);
                        return listString;
                    }
                case MenuItems.PROFILE:
                    {
                        DropdownMenu profile = new DropdownMenu(FindType.Id, "user-menu");
                        return profile.GetListItemName;
                    }
                default:
                    {
                        var a = GetListItemsOnMenu(itemOnMenu);
                        List<string> listName = new List<string>();
                        foreach (var item in a)
                            listName.Add(item.Text.Trim());
                        return listName;
                    }
            }
        }
    }

}
