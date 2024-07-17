using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Pages.Dashboard
{
    public partial class DashboardPage
    {
        #region "Dashboard Overview"
        // Using only for Dashboard Overview
        private bool IsListDisplayed(string itemName)
        {
            var list = FindElementHelper.FindElement(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[1]/section/div/ul/li/a[./text()[contains(translate(.,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz') ,'" + itemName.ToLower() + "')]]", 1);
            if (list != null && list.Displayed)
                return true;
            return false;
        }

        /// <summary>
        /// Click Job on Quick-start section
        /// </summary>
        public JobSegment ClickJobToShowList()
        {
            JobSegment _jobs = new JobSegment();

            if (!IsListDisplayed(JOBS.GetText().Trim()))
            {
                JOBS.Click();
            }
            return _jobs;

        }

        /// <summary>
        /// Click House on Quick-start section
        /// </summary>
        public HouseSegment ClickHouseToShowList()
        {
            HouseSegment _house = new HouseSegment();

            if (!IsListDisplayed(HOUSES.GetText().Trim()))
                HOUSES.Click();
            return _house;
        }

        /// <summary>
        /// Click Community on Quick-start section
        /// </summary>
        public CommunitySegment ClickCommunityToShowList()
        {
            CommunitySegment _commu = new CommunitySegment();
            if (!IsListDisplayed(COMMUNITIES.GetText().Trim()))
                COMMUNITIES.Click();
            return _commu;
        }

        /// <summary>
        /// Click Option on Quick-start section
        /// </summary>
        public OptionSegment ClickOptionToShowList()
        {
            OptionSegment _option = new OptionSegment();
            if (!IsListDisplayed(OPTIONS.GetText().Trim()))
                OPTIONS.Click();
            return _option;
        }

        /// <summary>
        /// Click Product on Quick-start section
        /// </summary>
        public ProductSegment ClickProductToShowList()
        {
            ProductSegment _product = new ProductSegment();
            if (!IsListDisplayed(PRODUCTS.GetText().Trim()))
                PRODUCTS.Click();
            return _product;
        }

        /// <summary>
        /// Get list Jobs on quick-start section
        /// </summary>
        /// <returns>Get list quick-start default of Jobs</returns>
        public IList<string> GetListJobs
        {
            get
            {
                List<string> lst = new List<string>();
                if (IsListDisplayed("Jobs"))
                {
                    var listjob = ClickJobToShowList().GetListItem();
                    return CommonHelper.CastListControlsToListString(listjob);
                }
                return lst;
            }
        }

        /// <summary>
        /// Get list Products on quick-start section
        /// </summary>
        /// <returns>Get list quick-start default of Products</returns>
        public IList<string> GetListProducts
        {
            get
            {
                List<string> lst = new List<string>();
                if (IsListDisplayed("Product"))
                {
                    var list = ClickProductToShowList().GetListItem();
                    return CommonHelper.CastListControlsToListString(list);
                }
                return lst;
            }
        }

        /// <summary>
        /// Get list Options on quick-start section
        /// </summary>
        /// <returns>Get list quick-start default of Options</returns>
        public IList<string> GetListOptions
        {
            get
            {
                List<string> lst = new List<string>();
                if (IsListDisplayed("Option"))
                {
                    var list = ClickOptionToShowList().GetListItem();
                    return CommonHelper.CastListControlsToListString(list);
                }
                return lst;
            }
        }

        /// <summary>
        /// Get list Communities on quick-start section
        /// </summary>
        /// <returns>Get list quick-start default of Communities</returns>
        public IList<string> GetListCommunities
        {
            get
            {
                List<string> lst = new List<string>();
                if (IsListDisplayed("Community"))
                {
                    var list = ClickCommunityToShowList().GetListItem();
                    return CommonHelper.CastListControlsToListString(list);
                }
                return lst;
            }
        }

        /// <summary>
        /// Get list Houses on quick-start section
        /// </summary>
        /// <returns>Get list quick-start default of Houses</returns>
        public IList<string> GetListHouses
        {
            get
            {
                IList<string> lst = new List<string>();
                if (IsListDisplayed("House"))
                {
                    var list = ClickHouseToShowList().GetListItem();
                    return CommonHelper.CastListControlsToListString(list);
                }
                return lst;
            }
        }
        #endregion

        #region "MENU Functions"
        /// <summary>
        /// Get list ASSETS on Toolbar Menu
        /// </summary>
        /// <returns>List item of ASSETS Menu</returns>
        public IList<string> GetListASSETS
        {
            get
            {
                return SelectMenu(Common.Enums.MenuEnums.MenuItems.ASSETS).GetListNameOnMenu();
            }
        }

        /// <summary>
        /// Get list Products MENU
        /// </summary>
        /// <returns>List of Products menu</returns>
        public IList<string> GetListPRODUCTS
        {
            get
            {
                return SelectMenu(Common.Enums.MenuEnums.MenuItems.ESTIMATING).GetListNameOnMenu();
            }
        }

        /// <summary>
        /// Get list item from JOBS menu
        /// </summary>
        public IList<string> GetListJOBS
        {
            get
            {
                return SelectMenu(Common.Enums.MenuEnums.MenuItems.JOBS).GetListNameOnMenu();
            }
        }

        /// <summary>
        /// Get list item from COSTING menu
        /// </summary>
        public IList<string> GetListCOSTING
        {
            get
            {
                return SelectMenu(Common.Enums.MenuEnums.MenuItems.COSTING).GetListNameOnMenu();
            }
        }

        /// <summary>
        /// Get list item from PURCHASING menu
        /// </summary>
        public IList<string> GetListPURCHASING
        {
            get
            {
                return SelectMenu(Common.Enums.MenuEnums.MenuItems.PURCHASING).GetListNameOnMenu();
            }
        }

        /// <summary>
        ///  Get list item from PATHWAY menu
        /// </summary>
        public IList<string> GetListPATHWAY
        {
            get
            {
                return SelectMenu(Common.Enums.MenuEnums.MenuItems.PATHWAY).GetListNameOnMenu();
            }
        }

        /// <summary>
        ///  Get list item from SALES menu
        /// </summary>
        public IList<string> GetListSALES
        {
            get
            {
                return SelectMenu(Common.Enums.MenuEnums.MenuItems.SALES).GetListNameOnMenu();
            }
        }

        /// <summary>
        /// Get list item from SALES PRICING menu
        /// </summary>
        public IList<string> GetListSALESPRICING
        {
            get
            {
                return SelectMenu(Common.Enums.MenuEnums.MenuItems.SALESPRICING).GetListNameOnMenu();
            }
        }

        /// <summary>
        ///  Get list item from REPORTS menu
        /// </summary>
        public IList<string> GetListREPORTS
        {
            get
            {
                return SelectMenu(Common.Enums.MenuEnums.MenuItems.REPORTS).GetListNameOnMenu();
            }
        }

        /// <summary>
        ///  Get list item from NOTIFICATION menu
        /// </summary>
        public IList<string> GetListNOTIFICATION
        {
            get
            {
                return SelectMenu(Common.Enums.MenuEnums.MenuItems.NOTIFICATION, true).GetListNameOnMenu();
            }
        }

        /// <summary>
        ///  Get list item from PROFILE menu
        /// </summary>
        public IList<string> GetListPROFILE
        {
            get
            {
                return SelectMenu(Common.Enums.MenuEnums.MenuItems.PROFILE).GetListNameOnMenu();
            }
        }

        #endregion

        #region "Activity Feed"
        public void WaitingActivityLoad()
        {
            try
            {
                while (_loadingGif_Activity != null && 
                    _loadingGif_Activity.GetWrappedControl() != null && 
                    _loadingGif_Activity.GetWrappedControl().Displayed)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (StaleElementReferenceException)
            {
                System.Console.WriteLine("Ignore the Stale exception for this case.");
            }
            catch (NullReferenceException)
            {
                System.Console.WriteLine("Ignore the No Such Element exception for this case.");
            }
        }

        /// <summary>
        /// Get list menu after click Sort button on Activity Feed
        /// </summary>
        public IList<string> GetListMenu_SortActivity
        {
            get
            {
                if (Activity_Menu.IsDisplayed())
                {
                    return Activity_Menu.GetListItemName;
                }
                return new List<string>();
            }
        }

        private IList<string> SortListDate_ASC(IList<string> ts)
        {
            // convert list string date to datetime
            List<DateTime> dateTimes_lst = new List<DateTime>();
            foreach (var item in ts)
            {
                DateTime.TryParse(item, out DateTime temp);
                dateTimes_lst.Add(temp);
            }
            dateTimes_lst = dateTimes_lst.OrderBy(i => i.TimeOfDay)
                                               .OrderBy(i => i.Date)
                                               .ToList();
            return dateTimes_lst.Select(x => x.ToString()).ToList();
        }

        private IList<string> SortListDate_DESC(IList<string> ts)
        {
            // convert list string date to datetime
            List<DateTime> dateTimes_lst = new List<DateTime>();
            foreach (var item in ts)
            {
                DateTime.TryParse(item, out DateTime temp);
                dateTimes_lst.Add(temp);
            }

            List<string> dateTimes_lstString = new List<string>();
            foreach (var item in dateTimes_lst)
            {
                dateTimes_lstString.Add(item.ToString("dd.MM.yyyy hh:mm:ss tt"));
            }

            dateTimes_lst = dateTimes_lst.OrderByDescending(i => i.TimeOfDay)
                                         .OrderByDescending(i => i.Date)
                                         .ToList();
            return dateTimes_lst.Select(x => x.ToString()).ToList();
        }
        #endregion

        #region "Recent Houses"
        /// <summary>
        /// Get list item on Sort menu recent house
        /// </summary>
        public IList<string> GetListMenu_SortRecentHouses
        {
            get
            {
                if (SortHouses_Menu.IsDisplayed())
                {
                    return SortHouses_Menu.GetListItemName;
                }
                return new List<string>();
            }
        }

        /// <summary>
        /// Get newest list house added
        /// </summary>
        public IList<string> GetListNewestAddedHouses
        {
            get
            {
                string query = @"SELECT top 4[Houses_Id],[Houses_PlanNo],[Houses_Name]
                        FROM[pipeline_dev].[dbo].[Builder_Houses]
                        order by[Houses_Id] desc";
                return CommonHelper.ExecuteReader<string>(query, "Houses_Name").ToList();
            }
        }

        /// <summary>
        /// Wating the Grid Recent Houses reload
        /// </summary>
        public void WaitingRecentHousesLoad()
        {
            try
            {
                while (_loadingGif_Houses != null && _loadingGif_Houses.IsDisplayed())
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (StaleElementReferenceException)
            {
                System.Console.WriteLine("Ignore the Stale exception for this case.");
            }
            catch (NullReferenceException)
            {
                System.Console.WriteLine("Ignore the No Such Element exception for this case.");
            }
        }

        /// <summary>
        /// Get the Houses URL by index, start from 0.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetHousesURL(int index)
        {
            //return Houses_Grid.FindElement(FindType.XPath, string.Format("./div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall'] and position()={0}]/section/p/a[2]", index + 1)).GetAttribute("href");
            return Houses_Grid.FindElement(FindType.XPath, string.Format("//div[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall']//preceding-sibling::div/div[@id='house_{0}']/../section/p/a[2]", index)).GetAttribute("href");
        }

        /// <summary>
        /// Get the URL of Houses from Recent Houses Grid with name
        /// </summary>
        /// <param name="houseName"></param>
        /// <returns></returns>
        public string GetHousesURL(string houseName)
        {
            return Houses_Grid.FindElement(FindType.XPath, string.Format("./div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlHouses'] ]/section/p/a[text() = '{0}']/../a[2]", houseName)).GetAttribute("href");
        }

        /// <summary>
        /// Get list Houses on Recent Add
        /// </summary>
        public IList<string> GetListHouseName_RecentHouses
        {
            get
            {
                //return CommonHelper.CastListControlsToListString(Houses_Grid.FindElements(FindType.XPath, "./div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall'] ]/section/p/a[1]").ToList());
                return CommonHelper.CastListControlsToListString(Houses_Grid.FindElements(FindType.XPath, "//div[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall']//preceding-sibling::div/section/p/a[1]").ToList());
            }
        }

        /// <summary>
        /// click on House to display more information
        /// Click on House to display more information
        /// </summary>
        /// <param name="houseName"></param>
        public void ClickingCaratToExpand_House(string houseName)
        {

            // Check the node if it expanded ignore
            var status = Houses_Grid.FindElement(FindType.XPath, string.Format("./div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall'] ]/section/p/a[text() = '{0}']", houseName));
            if (status == null)
            {
                throw new NoSuchElementException("Please recheck the control, the element is not found");
            }
            else if (status.GetAttribute("class") == string.Empty)
            {
                // Do notthing, this node already expanded
            }
            else
            {
                SpecificControls carrat = new SpecificControls(Houses_Grid.FindElement(FindType.XPath, string.Format("./div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall'] ]/section/p/a[text() = '{0}']", houseName)));
                carrat.Click();
            }
        }

        /// <summary>
        /// Click on house by index to display more information
        /// </summary>
        /// <param name="houseIndex"></param>
        public void ClickingCaratToExpand_House(int houseIndex)
        {

            // Check the node if it expanded ignore
            //var status = Houses_Grid.FindElement(FindType.XPath, string.Format("./div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall'] and position()={0}]/section/p/a[1]", houseIndex + 1));
            var status = Houses_Grid.FindElement(FindType.XPath, string.Format("//div[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall']//preceding-sibling::div/div[@id='house_{0}']/../section/p/a[1]", houseIndex));
            if (status == null)
            {
                throw new NoSuchElementException("Please recheck the control, the element is not found");
            }
            else if (status.GetAttribute("class") == string.Empty)
            {
                // Do notthing, this node already expanded
            }
            else
            {
                //SpecificControls carrat = new SpecificControls(Houses_Grid.FindElement(FindType.XPath, string.Format("./div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall'] and position()={0}]/section/p/a[1]", houseIndex + 1)));
                SpecificControls carrat = new SpecificControls(Houses_Grid.FindElement(FindType.XPath, string.Format("//div[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall']//preceding-sibling::div/div[@id='house_{0}']/../section/p/a[1]", houseIndex)));
                carrat.Click();
            }
        }

        /// <summary>
        /// Click on House to collapse more information
        /// </summary>
        /// <param name="houseName"></param>
        public void ClickingCaratToCollapse_House(string houseName)
        {
            // Check the node if it expanded ignore
            var status = Houses_Grid.FindElement(FindType.XPath, string.Format("./div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall'] ]/section/p/a[text() = '{0}']", houseName));
            if (status == null)
            {
                throw new NoSuchElementException("Please recheck the control, the element is not found");
            }
            else if (status.GetAttribute("class") == "collapsed")
            {
                // Do notthing, this node already collapsed
            }
            else
            {
                SpecificControls carrat = new SpecificControls(Houses_Grid.FindElement(FindType.XPath, string.Format("./div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall'] ]/section/p/a[text() = '{0}']", houseName)));
                carrat.Click();
            }
        }


        /// <summary>
        /// Click on House to collapse more information by index. Index start from 0
        /// </summary>
        /// <param name="houseIndex"></param>
        public void ClickingCaratToCollapse_House(int houseIndex)
        {
            // Check the node if it expanded ignore
            var status = Houses_Grid.FindElement(FindType.XPath, string.Format("./div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall'] and position()={0}]/section/p/a[1]", houseIndex + 1));
            if (status == null)
            {
                throw new NoSuchElementException("Please recheck the control, the element is not found");
            }
            else if (status.GetAttribute("class") == "collapsed")
            {
                // Do notthing, this node already collapsed
            }
            else
            {
                SpecificControls carrat = new SpecificControls(Houses_Grid.FindElement(FindType.XPath, string.Format("./div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall'] and position()={0}]/section/p/a[1]", houseIndex + 1)));
                carrat.Click();
            }
        }
        #endregion

        #region "Recent Active Jobs"
        /// <summary>
        /// Get list menu items of recent added Jobs
        /// </summary>
        public IList<string> GetListMenu_SortRecentJobs
        {
            get
            {
                if (SortJobs_Menu.IsDisplayed())
                {
                    return SortJobs_Menu.GetListItemName;
                }
                return new List<string>();
            }
        }

        /// <summary>
        /// Get list newest job
        /// </summary>
        public IList<string> GetListNewestAddedJobs
        {
            get
            {
                string query = @"SELECT TOP (4) [Jobs_Id],[Jobs_No],[Jobs_IsClosed]
                                FROM [pipeline_dev].[dbo].[Jobs_Jobs]
                                where [Jobs_IsClosed]=0
                                order by [Jobs_Id] desc";
                return CommonHelper.ExecuteReader<string>(query, "Jobs_No").ToList();
            }
        }

        /// <summary>
        /// Wating the Grid Recent Houses reload
        /// </summary>
        public void WaitingRecentJobsLoad()
        {
            try
            {
                while (_loadingGif_Jobs != null && _loadingGif_Jobs.IsDisplayed())
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (StaleElementReferenceException)
            {
                System.Console.WriteLine("Ignore the Stale exception for this case.");
            }
            catch (NullReferenceException)
            {
                System.Console.WriteLine("Ignore the No Such Element exception for this case.");
            }
        }

        /// <summary>
        /// Get the Houses URL by index, start from 0.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetJobURL(int index)
        {
            return _jobs_Grid.FindElement(FindType.XPath, string.Format("//div[following-sibling::*[@id='ctl00_CPH_Content_RecentActiveJobs1_pnlseeall'] and position()={0}]/section/p/a[2]", index + 1)).GetAttribute("href");
        }

        /// <summary>
        /// Get the URL of Houses from Recent Houses Grid with name
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public string GetJobURL(string jobName)
        {
            return _jobs_Grid.FindElement(FindType.XPath, string.Format("./div/section/p/a[text() = '{0}']/../a[2]", jobName)).GetAttribute("href");
        }

        /// <summary>
        /// Get list Houses on Recent Add
        /// </summary>
        public IList<string> GetListJobsName_RecentActiveJobs
        {
            get
            {
                return CommonHelper.CastListControlsToListString(_jobs_Grid.FindElements(FindType.XPath, "//div[following-sibling::*[@id='ctl00_CPH_Content_RecentActiveJobs1_pnlseeall']]/section/p/a[1]").ToList());
            }
        }

        /// <summary>
        /// click on House to display more information
        /// Click on House to display more information
        /// </summary>
        /// <param name="jobName"></param>
        public void ClickingCaratToExpand_Job(string jobName)
        {

            // Check the node if it expanded ignore
            var status = _jobs_Grid.FindElement(FindType.XPath, string.Format("./div/section/p/a[text() = '{0}']", jobName));
            if (status == null)
            {
                throw new NoSuchElementException("Please recheck the control, the element is not found");
            }
            else if (status.GetAttribute("class") == string.Empty)
            {
                // Do notthing, this node already expanded
            }
            else
            {
                SpecificControls carrat = new SpecificControls(_jobs_Grid.FindElement(FindType.XPath, string.Format("//div/section/p/a[text() = '{0}']", jobName)));
                carrat.Click();
            }
        }

        /// <summary>
        /// Click on house by index to display more information
        /// </summary>
        /// <param name="jobIndex"></param>
        public void ClickingCaratToExpand_Job(int jobIndex)
        {

            // Check the node if it expanded ignore
            var status = _jobs_Grid.FindElement(FindType.XPath, string.Format("//div[position()={0}]/section/p/a[1]", jobIndex + 1));
            if (status == null)
            {
                throw new NoSuchElementException("Please recheck the control, the element is not found");
            }
            else if (status.GetAttribute("class") == string.Empty)
            {
                // Do notthing, this node already expanded
            }
            else
            {
                SpecificControls carrat = new SpecificControls(_jobs_Grid.FindElement(FindType.XPath, string.Format("//div[position()={0}]/section/p/a[1]", jobIndex + 1)));
                carrat.Click();
            }
        }

        /// <summary>
        /// Click on House to collapse more information
        /// </summary>
        /// <param name="jobName"></param>
        public void ClickingCaratToCollapse_Job(string jobName)
        {
            // Check the node if it expanded ignore
            var status = _jobs_Grid.FindElement(FindType.XPath, string.Format("./div/section/p/a[text() = '{0}']", jobName));
            if (status == null)
            {
                throw new NoSuchElementException("Please recheck the control, the element is not found");
            }
            else if (status.GetAttribute("class") == "collapsed")
            {
                // Do notthing, this node already collapsed
            }
            else
            {
                SpecificControls carrat = new SpecificControls(_jobs_Grid.FindElement(FindType.XPath, string.Format("./div/section/p/a[text() = '{0}']", jobName)));
                carrat.Click();
            }
        }

        /// <summary>
        /// Click on House to collapse more information by index. Index start from 0
        /// </summary>
        /// <param name="jobIndex"></param>
        public void ClickingCaratToCollapse_Job(int jobIndex)
        {
            // Check the node if it expanded ignore
            var status = _jobs_Grid.FindElement(FindType.XPath, string.Format("./div[ position()={0}]/section/p/a[1]", jobIndex + 1));
            if (status == null)
            {
                throw new NoSuchElementException("Please recheck the control, the element is not found");
            }
            else if (status.GetAttribute("class") == "collapsed")
            {
                // Do notthing, this node already collapsed
            }
            else
            {
                SpecificControls carrat = new SpecificControls(_jobs_Grid.FindElement(FindType.XPath, string.Format("./div[position()={0}]/section/p/a[1]", jobIndex + 1)));
                carrat.Click();
            }
        }
        #endregion

        public static string userID = "F3811AD1-0C11-4C3F-BA41-300CFDC64E99";

        #region "Recent Communities"
        public void WaitingCommunitiesLoad()
        {
            try
            {
                while (_loadingGif_Communities != null && _loadingGif_Communities.IsDisplayed())
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (StaleElementReferenceException)
            {
                System.Console.WriteLine("Ignore the Stale exception for this case. The loading gif was hidden.");
            }
            catch (NullReferenceException)
            {
                System.Console.WriteLine("The loading gif was hidden.");
            }
        }

        /// <summary>
        /// Get the Communities URL by index, start from 0.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>URL</returns>
        public string GetCommunityURL(int index)
        {
            return _communities_Grid.FindElement(FindType.XPath, string.Format("./section[position()={0} ]/h2/a", index + 1)).GetAttribute("href");
        }

        /// <summary>
        /// Get the URL of Communities
        /// </summary>
        /// <param name="CommunityName"></param>
        /// <returns></returns>
        public string GetCommunityURL(string communityName)
        {
            return _communities_Grid.FindElement(FindType.XPath, string.Format("./section/h2/a[text() = '{0}']", communityName)).GetAttribute("href");
        }

        /// <summary>
        /// Get list menu after click Sort button on Communities
        /// </summary>
        public IList<string> GetListMenu_SortCommunities
        {
            get
            {
                if (SortCommunities_Menu.IsDisplayed())
                {
                    return SortCommunities_Menu.GetListItemName;
                }
                return new List<string>();
            }
        }

        /// <summary>
        /// Get list name Communities 
        /// </summary>
        public IList<IWebElement> GetListCommunitiesOnGrid
        {
            get
            {
                return _communities_Grid.FindElements(FindType.XPath, "./section/h2/a[1]").ToList();
            }
        }

        /// <summary>
        /// Get list name Communities 
        /// </summary>
        public IList<string> GetListCommunitiesNameOnGrid
        {
            get
            {
                return CommonHelper.CastListControlsToListString(GetListCommunitiesOnGrid, "text");
            }
        }

        /// <summary>
        /// Get List Communities From DB  By Name ASC 
        /// </summary>
        public IList<string> GetListCommunitiesFromDB_ByNameASC
        {
            get
            {
                //NOTE: Add this line to 'WHERE' clause of query if user is NOT able to see CG_Visions/hidden communities
                //      "AND (Builder_CommunitiesInDivision.CompanyDivisions_Id != 2 OR Builder_CompanyDivisions.CompanyDivisions_Name != 'CG Visions')"
                string query = $@"select top 6 Builder_Communities.Communities_Id ,Communities_Name +(case when (Communities_Code!='') then ' ('+Communities_Code +')' when (Communities_Code is null or Communities_Code='') then '' end) as Communities_Names
                                 FROM Builder_Communities
                                 LEFT OUTER JOIN Builder_CommunityStatusTypes ON Builder_Communities.CommunityStatusTypes_Id = Builder_CommunityStatusTypes.CommunityStatusTypes_Id
                                 LEFT OUTER JOIN Builder_CommunitiesInDivision ON Builder_Communities.Communities_Id = Builder_CommunitiesInDivision.Communities_Id
                                 LEFT OUTER JOIN Builder_CompanyDivisions ON Builder_CommunitiesInDivision.CompanyDivisions_Id = Builder_CompanyDivisions.CompanyDivisions_Id
                                 WHERE (Builder_CommunitiesInDivision.CompanyDivisions_Id IS NULL OR Builder_CommunitiesInDivision.CompanyDivisions_Id IN (SELECT DivisionId FROM Builder.GetDivisionFilterForUserId('{userID}')))
                                 order by Communities_Name";
                return CommonHelper.ExecuteReader<string>(query, "Communities_Names").ToList();
            }
        }

        public IList<string> GetListCommunitiesFromDB_ByNameASC_Generic
        {
            get
            {
                //NOTE: Add this line to 'WHERE' clause of query if user is NOT able to see CG_Visions/hidden communities
                //      "AND (Builder_CommunitiesInDivision.CompanyDivisions_Id != 2 OR Builder_CompanyDivisions.CompanyDivisions_Name != 'CG Visions')"
                string query = $@"select top 6 Builder_Communities.Communities_Id ,Communities_Name +(case when (Communities_Code!='') then ' ('+Communities_Code +')' when (Communities_Code is null or Communities_Code='') then '' end) as Communities_Names
                                 FROM Builder_Communities
                                 LEFT OUTER JOIN Builder_CommunityStatusTypes ON Builder_Communities.CommunityStatusTypes_Id = Builder_CommunityStatusTypes.CommunityStatusTypes_Id
                                 LEFT OUTER JOIN Builder_CommunitiesInDivision ON Builder_Communities.Communities_Id = Builder_CommunitiesInDivision.Communities_Id
                                 LEFT OUTER JOIN Builder_CompanyDivisions ON Builder_CommunitiesInDivision.CompanyDivisions_Id = Builder_CompanyDivisions.CompanyDivisions_Id
                                 WHERE (Builder_CommunitiesInDivision.CompanyDivisions_Id IS NULL)
                                 order by Communities_Name";
                return CommonHelper.ExecuteReader<string>(query, "Communities_Names").ToList();
            }
        }

        /// <summary>
        /// Get List Communities From DB  By Name DESC 
        /// </summary>
        public IList<string> GetListCommunitiesFromDB_ByNameDESC
        {
            get
            {
                //NOTE: Add this line to 'WHERE' clause of query if user is NOT able to see CG_Visions/hidden communities
                //      "AND (Builder_CommunitiesInDivision.CompanyDivisions_Id != 2 OR Builder_CompanyDivisions.CompanyDivisions_Name != 'CG Visions')"
                string query = $@"select top 6 Builder_Communities.Communities_Id ,Communities_Name + (case when (Communities_Code!='') then ' ('+Communities_Code +')' when (Communities_Code is null or Communities_Code='') then '' end) as Communities_Names
                                 FROM Builder_Communities
                                 LEFT OUTER JOIN Builder_CommunityStatusTypes ON Builder_Communities.CommunityStatusTypes_Id = Builder_CommunityStatusTypes.CommunityStatusTypes_Id
                                 LEFT OUTER JOIN Builder_CommunitiesInDivision ON Builder_Communities.Communities_Id = Builder_CommunitiesInDivision.Communities_Id
                                 LEFT OUTER JOIN Builder_CompanyDivisions ON Builder_CommunitiesInDivision.CompanyDivisions_Id = Builder_CompanyDivisions.CompanyDivisions_Id
								 WHERE (Builder_CommunitiesInDivision.CompanyDivisions_Id IS NULL OR Builder_CommunitiesInDivision.CompanyDivisions_Id IN (SELECT DivisionId FROM Builder.GetDivisionFilterForUserId('{userID}')))
                                 order by Communities_Name DESC";
                return CommonHelper.ExecuteReader<string>(query, "Communities_Names").ToList();
            }
        }

        public IList<string> GetListCommunitiesFromDB_ByNameDESC_Generic
        {
            get
            {
                //NOTE: Add this line to 'WHERE' clause of query if user is NOT able to see CG_Visions/hidden communities
                //      "AND (Builder_CommunitiesInDivision.CompanyDivisions_Id != 2 OR Builder_CompanyDivisions.CompanyDivisions_Name != 'CG Visions')"
                string query = $@"select top 6 Builder_Communities.Communities_Id ,Communities_Name + (case when (Communities_Code!='') then ' ('+Communities_Code +')' when (Communities_Code is null or Communities_Code='') then '' end) as Communities_Names
                                 FROM Builder_Communities
                                 LEFT OUTER JOIN Builder_CommunityStatusTypes ON Builder_Communities.CommunityStatusTypes_Id = Builder_CommunityStatusTypes.CommunityStatusTypes_Id
                                 LEFT OUTER JOIN Builder_CommunitiesInDivision ON Builder_Communities.Communities_Id = Builder_CommunitiesInDivision.Communities_Id
                                 LEFT OUTER JOIN Builder_CompanyDivisions ON Builder_CommunitiesInDivision.CompanyDivisions_Id = Builder_CompanyDivisions.CompanyDivisions_Id
								 WHERE (Builder_CommunitiesInDivision.CompanyDivisions_Id IS NULL)
                                 order by Communities_Name DESC";
                return CommonHelper.ExecuteReader<string>(query, "Communities_Names").ToList();
            }
        }

        /// <summary>
        /// Get List Communities From DB  By Date Create DESC 
        /// </summary>
        public IList<string> GetListCommunitiesFromDB_Newest
        {
            get
            {
                //NOTE: Add this line to 'WHERE' clause of query if user is NOT able to see CG_Visions/hidden communities
                //      "AND (Builder_CommunitiesInDivision.CompanyDivisions_Id != 2 OR Builder_CompanyDivisions.CompanyDivisions_Name != 'CG Visions')"
                string query = $@"select top 6 Builder_Communities.Communities_Id ,Communities_Name + (case when (Communities_Code!='') then ' ('+Communities_Code +')' when (Communities_Code is null or Communities_Code='') then '' end) as Communities_Names
                                 FROM Builder_Communities
                                 LEFT OUTER JOIN Builder_CommunityStatusTypes ON Builder_Communities.CommunityStatusTypes_Id = Builder_CommunityStatusTypes.CommunityStatusTypes_Id
                                 LEFT OUTER JOIN Builder_CommunitiesInDivision ON Builder_Communities.Communities_Id = Builder_CommunitiesInDivision.Communities_Id
                                 LEFT OUTER JOIN Builder_CompanyDivisions ON Builder_CommunitiesInDivision.CompanyDivisions_Id = Builder_CompanyDivisions.CompanyDivisions_Id
                                 WHERE (Builder_CommunitiesInDivision.CompanyDivisions_Id IS NULL OR Builder_CommunitiesInDivision.CompanyDivisions_Id IN (SELECT DivisionId FROM Builder.GetDivisionFilterForUserId('{userID}')))
                                 order by Builder_Communities.Communities_Id desc";
                return CommonHelper.ExecuteReader<string>(query, "Communities_Names").ToList();
            }
        }

        public IList<string> GetListCommunitiesFromDB_Newest_Generic
        {
            get
            {
                string query = $@"select top 6 Builder_Communities.Communities_Id ,Communities_Name + (case when (Communities_Code!='') then ' ('+Communities_Code +')' when (Communities_Code is null or Communities_Code='') then '' end) as Communities_Names
                                 FROM Builder_Communities
                                 LEFT OUTER JOIN Builder_CommunityStatusTypes ON Builder_Communities.CommunityStatusTypes_Id = Builder_CommunityStatusTypes.CommunityStatusTypes_Id
                                 LEFT OUTER JOIN Builder_CommunitiesInDivision ON Builder_Communities.Communities_Id = Builder_CommunitiesInDivision.Communities_Id
                                 LEFT OUTER JOIN Builder_CompanyDivisions ON Builder_CommunitiesInDivision.CompanyDivisions_Id = Builder_CompanyDivisions.CompanyDivisions_Id
                                 WHERE (Builder_CommunitiesInDivision.CompanyDivisions_Id IS NULL)
                                 order by Builder_Communities.Communities_Id desc";
                return CommonHelper.ExecuteReader<string>(query, "Communities_Names").ToList();
            }
        }

        /// <summary>
        /// Get List Communities From DB  By Date Create ASC 
        /// </summary>
        public IList<string> GetListCommunitiesFromDB_Oldest
        {
            get
            {
                //NOTE: Add this line to 'WHERE' clause of query if user is NOT able to see CG_Visions/hidden communities
                //      "AND (Builder_CommunitiesInDivision.CompanyDivisions_Id != 2 OR Builder_CompanyDivisions.CompanyDivisions_Name != 'CG Visions')"
                string query = $@"select top 6 Builder_Communities.Communities_Id ,Communities_Name + (case when (Communities_Code!='') then ' ('+Communities_Code +')' when (Communities_Code is null or Communities_Code='') then '' end) as Communities_Names
                                 FROM Builder_Communities
                                 LEFT OUTER JOIN Builder_CommunityStatusTypes ON Builder_Communities.CommunityStatusTypes_Id = Builder_CommunityStatusTypes.CommunityStatusTypes_Id
                                 LEFT OUTER JOIN Builder_CommunitiesInDivision ON Builder_Communities.Communities_Id = Builder_CommunitiesInDivision.Communities_Id
                                 LEFT OUTER JOIN Builder_CompanyDivisions ON Builder_CommunitiesInDivision.CompanyDivisions_Id = Builder_CompanyDivisions.CompanyDivisions_Id
                                 WHERE (Builder_CommunitiesInDivision.CompanyDivisions_Id IS NULL OR Builder_CommunitiesInDivision.CompanyDivisions_Id IN (SELECT DivisionId FROM Builder.GetDivisionFilterForUserId('{userID}')))
                                 order by Builder_Communities.Communities_Id";
                return CommonHelper.ExecuteReader<string>(query, "Communities_Names").ToList();
            }
        }

        public IList<string> GetListCommunitiesFromDB_Oldest_Generic
        {
            get
            {
                //Generic list of communities w
                string query = $@"select top 6 Builder_Communities.Communities_Id ,Communities_Name + (case when (Communities_Code!='') then ' ('+Communities_Code +')' when (Communities_Code is null or Communities_Code='') then '' end) as Communities_Names
                                 FROM Builder_Communities
                                 LEFT OUTER JOIN Builder_CommunityStatusTypes ON Builder_Communities.CommunityStatusTypes_Id = Builder_CommunityStatusTypes.CommunityStatusTypes_Id
                                 LEFT OUTER JOIN Builder_CommunitiesInDivision ON Builder_Communities.Communities_Id = Builder_CommunitiesInDivision.Communities_Id
                                 LEFT OUTER JOIN Builder_CompanyDivisions ON Builder_CommunitiesInDivision.CompanyDivisions_Id = Builder_CompanyDivisions.CompanyDivisions_Id
                                 WHERE (Builder_CommunitiesInDivision.CompanyDivisions_Id IS NULL)
                                 order by Builder_Communities.Communities_Id";
                return CommonHelper.ExecuteReader<string>(query, "Communities_Names").ToList();
            }
        }

        #endregion

        #region "Recent Options"
        public void WaitingOptionsLoad()
        {
            try
            {
                while (_loadingGif_Options != null && _loadingGif_Options.IsDisplayed())
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (StaleElementReferenceException)
            {
                System.Console.WriteLine("Ignore the Stale exception for this case.");
            }
            catch (NullReferenceException)
            {
                System.Console.WriteLine("Ignore the No Such Element exception for this case.");
            }
        }

        /// <summary>
        /// Get list menu after click Sort button on Options
        /// </summary>
        public IList<string> GetListMenu_SortOptions
        {
            get
            {
                if (SortOptions_Menu.IsDisplayed())
                {
                    return SortOptions_Menu.GetListItemName;
                }
                return new List<string>();
            }
        }

        /// <summary>
        /// Get the Options URL by index, start from 0.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>URL</returns>
        public string GetOptionURL(int index)
        {
            return _options_Grid.FindElement(FindType.XPath, string.Format("./section[position()={0} ]/h2/a", index + 1)).GetAttribute("href");
        }

        /// <summary>
        /// Get the URL of Options
        /// </summary>
        /// <param name="OptionName"></param>
        /// <returns></returns>
        public string GetOptionsURL(string optionName)
        {
            return _options_Grid.FindElement(FindType.XPath, string.Format("./section/h2/a[text() = '{0}']", optionName)).GetAttribute("href");
        }

        /// <summary>
        /// Get list Options
        /// </summary>
        public IList<IWebElement> GetListOptionsOnGrid
        {
            get
            {
                return _options_Grid.FindElements(FindType.XPath, "./section/h2/a[1]").ToList();
            }
        }

        /// <summary>
        /// Get list Options Name
        /// </summary>
        public IList<string> GetListOptionsNameOnGrid
        {
            get
            {
                return CommonHelper.CastListControlsToListString(GetListOptionsOnGrid, "text");
            }
        }

        /// <summary>
        /// Get List Options From DB  By Name ASC 
        /// </summary>
        public IList<string> GetListOptionsFromDB_ByNameASC
        {
            get
            {
                string query = @" select top 6 Options_Id,cast( Options_Name + (case when(Options_Description!='') then(' - ' + Options_Description) else Options_Description end) as nvarchar(1000))as Options_Names
                            from[dbo].[Builder_Options]
                            order by Options_Name asc";
                return CommonHelper.ExecuteReader<string>(query, "Options_Names").ToList();
            }
        }

        /// <summary>
        /// Get List Options From DB  By Name DESC 
        /// </summary>
        public IList<string> GetListOptionsFromDB_ByNameDESC
        {
            get
            {
                string query = @" select top 6 Options_Id,cast( Options_Name + (case when(Options_Description!='') then(' - ' + Options_Description) else Options_Description end) as nvarchar(1000))as Options_Names
                            from[dbo].[Builder_Options]
                            order by Options_Name desc";
                return CommonHelper.ExecuteReader<string>(query, "Options_Names").ToList();
            }
        }

        /// <summary>
        /// Get List Options From DB  By Date Create DESC 
        /// </summary>
        public IList<string> GetListOptionsFromDB_Newest
        {
            get
            {
                string query = @" select top 6 Options_Id,cast( Options_Name + (case when(Options_Description!='') then(' - ' + Options_Description) else Options_Description end) as nvarchar(1000))as Options_Name
                            from[dbo].[Builder_Options]
                            order by Options_Id desc";
                return CommonHelper.ExecuteReader<string>(query, "Options_Name").ToList();
            }
        }

        /// <summary>
        /// Get List Options From DB  By Date Create ASC 
        /// </summary>
        public IList<string> GetListOptionsFromDB_Oldest
        {
            get
            {
                string query = @"select top 6 Options_Id,cast( Options_Name + (case when(Options_Description!='') then(' - ' + Options_Description) else Options_Description end) as nvarchar(1000))as Options_Name
                            from[dbo].[Builder_Options]
                            order by Options_Id asc";
                return CommonHelper.ExecuteReader<string>(query, "Options_Name").ToList();
            }
        }
        #endregion

        #region "Recent Products"
        public void WaitingProductsLoad()
        {
            try
            {
                while (LoadingGif_Products != null && LoadingGif_Products.IsDisplayed())
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (StaleElementReferenceException)
            {
                System.Console.WriteLine("Ignore the Stale exception for this case.");
            }
            catch (NullReferenceException)
            {
                System.Console.WriteLine("Ignore the No Such Element exception for this case.");
            }
        }

        /// <summary>
        /// Get list menu after click Sort button on Products
        /// </summary>
        public IList<string> GetListMenu_SortProducts
        {
            get
            {
                if (SortProducts_Menu.IsDisplayed())
                {
                    return SortProducts_Menu.GetListItemName;
                }
                return new List<string>();
            }
        }

        /// <summary>
        /// Get the Products URL by index, start from 0.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>URL</returns>
        public string GetProductURL(int index)
        {
            return Products_Grid.FindElement(FindType.XPath, string.Format("./section[position()={0} ]/h2/a", index + 1)).GetAttribute("href");
        }

        /// <summary>
        /// Get the URL of Products
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public string GetProductURL(string productName)
        {
            return Products_Grid.FindElement(FindType.XPath, string.Format("./section/h2/a[text() = '{0}']", productName)).GetAttribute("href");
        }

        /// <summary>
        /// Get list Products
        /// </summary>
        public IList<IWebElement> GetListProductsOnGrid
        {
            get
            {
                return Products_Grid.FindElements(FindType.XPath, "./section/h2/a[1]").ToList();
            }
        }

        /// <summary>
        /// Get list Products Name
        /// </summary>
        public IList<string> GetListProductsNameOnGrid
        {
            get
            {
                return CommonHelper.CastListControlsToListString(GetListProductsOnGrid, "text");
            }
        }

        /// <summary>
        /// Get List Products From DB  By Name ASC 
        /// </summary>
        public IList<string> GetListProductsFromDB_ByNameASC
        {
            get
            {
                string query = @"select top 6 Products_Id, cast( Products_Name + (case when(Products_Description IS NOT NULL and Products_Description!='') then(' - ' + Products_Description) else '' end) as nvarchar(1000)) as Products_Names
                from[dbo].[Products_Products]
                order by Products_Names";
                return CommonHelper.ExecuteReader<string>(query, "Products_Names").ToList();
            }
        }

        /// <summary>
        /// Get List Products From DB  By Name DESC 
        /// </summary>
        public IList<string> GetListProductsFromDB_ByNameDESC
        {
            get
            {
                string query = @"select top 6 Products_Id,cast( Products_Name + (case when(Products_Description IS NOT NULL and Products_Description!='') then(' - ' + Products_Description) else '' end) as nvarchar(1000)) as Products_Names
                from[dbo].[Products_Products]
                order by Products_Name desc";
                return CommonHelper.ExecuteReader<string>(query, "Products_Names").ToList();
            }
        }

        /// <summary>
        /// Get List Products From DB  By Date Create DESC 
        /// </summary>
        public IList<string> GetListProductsFromDB_Newest
        {
            get
            {
                string query = @"select top 6 Products_Id,cast( Products_Name + (case when(Products_Description IS NOT NULL and Products_Description!='') then(' - ' + Products_Description) else '' end) as nvarchar(1000)) as Products_Name
                from[dbo].[Products_Products]
                order by Products_Id desc";
                return CommonHelper.ExecuteReader<string>(query, "Products_Name").ToList();
            }
        }

        /// <summary>
        /// Get List Products From DB  By Date Create ASC 
        /// </summary>
        public IList<string> GetListProductsFromDB_Oldest
        {
            get
            {
                string query = @"select top 6 Products_Id,cast( Products_Name + (case when(Products_Description IS NOT NULL and Products_Description!='') then(' - ' + Products_Description) else '' end) as nvarchar(1000)) as Products_Name
                from[dbo].[Products_Products]
                order by Products_Id";
                return CommonHelper.ExecuteReader<string>(query, "Products_Name").ToList();
            }
        }
        #endregion
    }

    #region "Dashboard Overview"

    public class ProductSegment
    {
        /// <summary>
        /// Click on View Product
        /// </summary>
        public void ViewProducts()
        {
            SpecificControls _view = new SpecificControls(GetListItem()[0]);
            _view.Click();
        }

        /// <summary>
        /// Click on Create New Product
        /// </summary>
        public void CreateNewProduct()
        {
            SpecificControls _create = new SpecificControls(GetListItem()[1]);
            _create.Click();
        }

        /// <summary>
        /// Get list item of Product
        /// </summary>
        /// <returns>List item</returns>
        public IList<IWebElement> GetListItem()
        {
            return FindElementHelper.Instance().FindElements(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[1]/section/div/ul/li/a[contains(text(),'Product')]");
        }
    }

    public class OptionSegment
    {
        public void ViewOptions()
        {
            SpecificControls _view = new SpecificControls(GetListItem()[0]);
            _view.Click();
        }

        public void CreateNewOption()
        {
            SpecificControls _create = new SpecificControls(GetListItem()[1]);
            _create.Click();
        }

        /// <summary>
        /// Get list item of Option
        /// </summary>
        /// <returns>List item</returns>
        public IList<IWebElement> GetListItem()
        {
            return FindElementHelper.Instance().FindElements(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[1]/section/div/ul/li/a[contains(text(),'Option')]");
        }
    }

    public class CommunitySegment
    {
        public void ViewCommunities()
        {
            SpecificControls _view = new SpecificControls(GetListItem()[0]);
            _view.Click();
        }

        public void CreateNewCommunity()
        {
            SpecificControls _create = new SpecificControls(GetListItem()[1]);
            _create.Click();
        }

        /// <summary>
        /// Get list item of Community
        /// </summary>
        /// <returns>List item</returns>
        public IList<IWebElement> GetListItem()
        {
            return FindElementHelper.Instance().FindElements(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[1]/section[3]/div/ul/li/a");
        }
    }

    public class HouseSegment
    {
        public void ViewHouses()
        {
            SpecificControls _view = new SpecificControls(GetListItem()[0]);
            _view.Click();
        }

        public void CreateNewHouse()
        {
            SpecificControls _create = new SpecificControls(GetListItem()[1]);
            _create.Click();
        }

        /// <summary>
        /// Get list item of House
        /// </summary>
        /// <returns>List item</returns>
        public IList<IWebElement> GetListItem()
        {
            return FindElementHelper.Instance().FindElements(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[1]/section/div/ul/li/a[contains(text(),'House')]");
        }
    }

    public class JobSegment
    {
        public void ViewJobs()
        {
            SpecificControls _view = new SpecificControls(GetListItem()[0]);
            _view.Click();
        }

        public void CreateNewJob()
        {
            SpecificControls _create = new SpecificControls(GetListItem()[1]);
            _create.Click();
        }

        /// <summary>
        /// Get list item of Job
        /// </summary>
        /// <returns>List item</returns>
        public IList<IWebElement> GetListItem()
        {
            return FindElementHelper.Instance().FindElements(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[1]/section/div/ul/li/a[contains(text(),'Job')]");
        }
    }
    #endregion
}