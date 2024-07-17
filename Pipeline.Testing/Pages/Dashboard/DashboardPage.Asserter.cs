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

        #endregion

        #region "Activity Feed"
        /// <summary>
        /// Is activity feed sort with newest
        /// </summary>
        public bool IsNewest_ActivityFeed
        {
            get
            {
                //Get list item on Grid
                var lst = CommonHelper.CastListControlsToListString(Activity_Grid.FindElements(FindType.XPath, "./section/div[2]/h2/time").ToList());
                return CommonHelper.IsEqual2ListSequence(lst, SortListDate_DESC(lst));
            }

        }

        /// <summary>
        /// Is activity feed sort with oldest
        /// </summary>
        public bool IsOldest_ActivityFeed
        {
            get
            {
                //Get list item on Grid
                var lst = CommonHelper.CastListControlsToListString(Activity_Grid.FindElements(FindType.XPath, "./section/div[2]/h2/time").ToList());
                return CommonHelper.IsEqual2ListSequence(lst, SortListDate_ASC(lst));
            }

        }

        /// <summary>
        /// Is the name sort by A->Z
        /// </summary>
        public bool IsNameAscending_ActivityFeed
        {
            // Currently, The function sort with name + time is not correct => slit it then sort and re-merge.
            get
            {
                // Get list item on Grid
                var lst_Controls = Activity_Grid.FindElements(FindType.XPath, "./section/div[2]/h2").ToList();
                // Convert list item to list string (Name + time)
                var lst_OriginalText = CommonHelper.CastListControlsToListString(lst_Controls);
                // Get list time Controls
                var lstTime_Controls = Activity_Grid.FindElements(FindType.XPath, "./section/div[2]/h2/time").ToList();
                // Convert list time controls to string(Time)
                var lstTime_OriginalText = CommonHelper.CastListControlsToListString(lstTime_Controls);

                // Get list Name only
                List<string> lst_Name = new List<string>();
                int i = 0;
                foreach (var item in lst_Controls)
                {
                    lst_Name.Add(item.Text.Replace(lstTime_OriginalText[i], string.Empty));
                    i++;
                }

                lst_Name = lst_Name.OrderBy(n => n).ToList();
                int j = 0;
                foreach (var item in lst_OriginalText)
                {
                    if (!item.StartsWith(lst_Name[j]))
                        return false;
                    j++;
                }
                return true;
            }

        }

        /// <summary>
        /// Is the name sort by Z->A .
        /// </summary>
        public bool IsNameDescending_ActivityFeed
        {
            get
            {
                // Get list item on Grid
                var lst_Controls = Activity_Grid.FindElements(FindType.XPath, "./section/div[2]/h2").ToList();
                // Convert list item to list string (Name + time)
                var lst_OriginalText = CommonHelper.CastListControlsToListString(lst_Controls);
                // Get list time Controls
                var lstTime_Controls = Activity_Grid.FindElements(FindType.XPath, "./section/div[2]/h2/time").ToList();
                // Convert list time controls to string(Time)
                var lstTime_OriginalText = CommonHelper.CastListControlsToListString(lstTime_Controls);

                // Get list Name only
                List<string> lst_Name = new List<string>();
                int i = 0;
                foreach (var item in lst_Controls)
                {
                    lst_Name.Add(item.Text.Replace(lstTime_OriginalText[i], string.Empty));
                    i++;
                }
                lst_Name = lst_Name.OrderByDescending(n => n).ToList();
                int j = 0;
                foreach (var item in lst_OriginalText)
                {
                    if (!item.StartsWith(lst_Name[j]))
                        return false;
                    j++;
                }
                return true;
            }

        }

        public string GetViewAllActivityHyperLink()
        {
            Link viewAllActivityLink = new Link(FindType.XPath, $"//*[@id='ctl00_CPH_Content_af1_hypViewAllActivity']");
            viewAllActivityLink.WaitForElementIsVisible(5);
            return viewAllActivityLink == null ? string.Empty : viewAllActivityLink.GetAttribute("href");
        }

        #endregion

        #region "Recent Houses"
        /// <summary>
        /// Is the list on recently houses added display correctly
        /// </summary>
        public bool IsListDisplayRecentlyHousesAdded
        {
            get
            {
                string query = @"SELECT distinct top 10 A1.hid AS Assets_Id , Builder_Houses.Houses_Name
                                FROM ((
                                	SELECT ActivityCenter_ActivityParams.ActivityLog_Id,                
                                	CASE WHEN ISNUMERIC(ActivityParams_Value) = 1 
                                	then CAST(ActivityParams_Value AS INTEGER) else 0 END as hid     
                                FROM dbo.ActivityCenter_ActivityParams         
                                	INNER JOIN dbo.ActivityCenter_EventParams       
                                	ON ActivityCenter_EventParams.EventParams_Id = ActivityCenter_ActivityParams.EventParams_Id   
                                WHERE ActivityCenter_EventParams.EventParams_Name = 'hid')) A1  
                                	INNER JOIN Builder_Houses  ON A1.hid = Builder_Houses.Houses_Id 
                                ORDER BY hid DESC;";
                var lst_Expected = CommonHelper.ExecuteReader<string>(query, "Houses_Name").Take(4).ToList();
                return CommonHelper.IsEqual2ListSequence(GetListHouseName_RecentHouses, lst_Expected);
            }
        }

        public bool IsHouseDetailsPageOpened(string houseName)
        {
            var houseName_Expected = FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_txtHouseName");
            if (!CurrentURL.StartsWith(BaseDashboardUrl + "/Builder/Houses/House.aspx?hid=", StringComparison.CurrentCultureIgnoreCase)
                    && CurrentURL != BaseDashboardUrl + "/Builder/Houses/House.aspx?hid=0")
                return false;
            else if (houseName_Expected != null)
                return houseName_Expected.GetAttribute("value") == houseName;
            else
                return false;
        }

        /// <summary>
        /// House details is displayed or not
        /// </summary>
        /// <param name="houseName"></param>
        /// <returns></returns>
        public bool IsHouseExpanded(string houseName)
        {
            var isExpanded = Houses_Grid.FindElement(FindType.XPath, string.Format("./div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall'] ]/section/p/a[text() = '{0}']", houseName)).GetAttribute("class");
            return isExpanded != "collapsed";
        }

        /// <summary>
        /// House details is displayed or not
        /// </summary>
        /// <param name="houseName"></param>
        /// <returns></returns>
        public bool IsHouseExpanded(int houseIndex)
        {
            var isExpanded = Houses_Grid.FindElement(FindType.XPath, string.Format("//div[following-sibling::*[@id='ctl00_CPH_Content_RecentHouses1_pnlseeall'] and position()={0} ]/section/p/a[1]", houseIndex + 1)).GetAttribute("class");
            return isExpanded != "collapsed";
        }

        #endregion

        #region "Recent Active Jobs"
        /// <summary>
        /// Is the list on recently houses added display correctly
        /// </summary>
        public bool IsListDisplayRecentlyJobsAdded
        {
            get
            {
                string query = @"select top 4 Jobs_Id,Jobs_No
                                from [dbo].[Jobs_Jobs]
                                where Jobs_IsClosed=0
                                order by Jobs_Id desc";
                var lst_Expected = CommonHelper.ExecuteReader<string>(query, "Jobs_No").ToList();
                return CommonHelper.IsEqual2ListSequence(GetListJobsName_RecentActiveJobs, lst_Expected);
            }
        }

        public bool IsJobDetailsPageOpened(string JobName)
        {
            var JobName_Expected = FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_ctl00_CPH_Content_JobFilter_litTitlePanel");

            if (!CurrentURL.StartsWith(BaseDashboardUrl + "/Jobs/Job.aspx?jid=", StringComparison.CurrentCultureIgnoreCase)
                    && CurrentURL != BaseDashboardUrl + "/Jobs/Job.aspx?jid=0")
                return false;
            else if (JobName_Expected != null)
                //return JobName_Expected.GetAttribute("text") == JobName;
                return true;
            else
                return false;
        }

        /// <summary>
        /// Job details is displayed or not
        /// </summary>
        /// <param name="JobName"></param>
        /// <returns></returns>
        public bool IsJobExpanded(string JobName)
        {
            var isExpanded = _jobs_Grid.FindElement(FindType.XPath, string.Format("./div/section/p/a[text() = '{0}']", JobName)).GetAttribute("class");
            return isExpanded != "collapsed";
        }

        /// <summary>
        /// Job details is displayed or not
        /// </summary>
        /// <param name="JobName"></param>
        /// <returns></returns>
        public bool IsJobExpanded(int JobIndex)
        {
            var isExpanded = _jobs_Grid.FindElement(FindType.XPath, string.Format("//div[position()={0} ]/section/p/a[1]", JobIndex + 1)).GetAttribute("class");
            return isExpanded != "collapsed";
        }
        #endregion

        #region "Recent Communities"
        /// <summary>
        /// Is Communities sort with newest
        /// </summary>
        public bool IsNewest_Communities
        {
            get
            {
                bool isEqual = CommonHelper.IsEqual2ListSequence(GetListCommunitiesNameOnGrid, GetListCommunitiesFromDB_Newest);
                if (!isEqual)
                    isEqual = CommonHelper.IsEqual2ListSequence(GetListCommunitiesNameOnGrid, GetListCommunitiesFromDB_Newest_Generic);
                return isEqual;
            }
        }

        /// <summary>
        /// Is Communities sort with oldest
        /// </summary>
        public bool IsOldest_Communities
        {
            get
            {
                bool isEqual = CommonHelper.IsEqual2ListSequence(GetListCommunitiesNameOnGrid, GetListCommunitiesFromDB_Oldest);
                if (!isEqual)
                    isEqual = CommonHelper.IsEqual2ListSequence(GetListCommunitiesNameOnGrid, GetListCommunitiesFromDB_Oldest_Generic);
                return isEqual;
            }
        }

        /// <summary>
        /// Is the name sort by A->Z
        /// </summary>
        public bool IsNameAscending_Communities
        {
            get
            {
                bool isEqual = CommonHelper.IsEqual2ListSequence(GetListCommunitiesNameOnGrid, GetListCommunitiesFromDB_ByNameASC);
                if (!isEqual)
                    isEqual = CommonHelper.IsEqual2ListSequence(GetListCommunitiesNameOnGrid, GetListCommunitiesFromDB_ByNameASC_Generic);
                return isEqual;
            }
        }

        /// <summary>
        /// Is the name sort by Z->A .
        /// </summary>
        public bool IsNameDescending_Communities
        {
            get
            {
                bool isEqual = CommonHelper.IsEqual2ListSequence(GetListCommunitiesNameOnGrid, GetListCommunitiesFromDB_ByNameDESC);
                if (!isEqual)
                    isEqual = CommonHelper.IsEqual2ListSequence(GetListCommunitiesNameOnGrid, GetListCommunitiesFromDB_ByNameDESC_Generic);
                return isEqual;
            }
        }

        /// <summary>
        /// Is Community Details page opened from Dashboard
        /// </summary>
        /// <param name="communityName"></param>
        /// <returns></returns>
        public bool IsCommunitiesDetailsPageOpened(string communityName)
        {
            var _Expected_Name = FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_txtCommunityName");
            var _Expected_Code = FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_txtCode");

            if (!CurrentURL.StartsWith(BaseDashboardUrl + "/Builder/Communities/community.aspx?cid=", StringComparison.CurrentCultureIgnoreCase)
                && CurrentURL != BaseDashboardUrl + "/Builder/Communities/community.aspx?cid=0")
                return false;
            else if (_Expected_Name != null && _Expected_Code != null)
            {
                if (_Expected_Code.GetAttribute("value") == "")
                    return _Expected_Name.GetAttribute("value") == communityName;
                else
                {
                    string expected = string.Format("{0} ({1})", _Expected_Name.GetAttribute("value"), _Expected_Code.GetAttribute("value"));
                    return expected == communityName;
                }
            }
            else
                return false;
        }
        #endregion

        #region "Recent Options"
        /// <summary>
        /// Is Options sort with newest
        /// </summary>
        public bool IsNewest_Options
        {
            get
            {
                return CommonHelper.IsEqual2ListSequence(GetListOptionsNameOnGrid, GetListOptionsFromDB_Newest);
            }
        }

        /// <summary>
        /// Is Options sort with oldest
        /// </summary>
        public bool IsOldest_Options
        {
            get
            {
                return CommonHelper.IsEqual2ListSequence(GetListOptionsNameOnGrid, GetListOptionsFromDB_Oldest);
            }

        }

        /// <summary>
        /// Is the name sort by A->Z
        /// </summary>
        public bool IsNameAscending_Options
        {
            get
            {
                return CommonHelper.IsEqual2ListSequence(GetListOptionsNameOnGrid, GetListOptionsFromDB_ByNameASC);
            }
        }

        /// <summary>
        /// Is the name sort by Z->A .
        /// </summary>
        public bool IsNameDescending_Options
        {
            get
            {
                return CommonHelper.IsEqual2ListSequence(GetListOptionsNameOnGrid, GetListOptionsFromDB_ByNameDESC);
            }
        }

        /// <summary>
        ///  Is Option Details page opened from Dashboard
        /// </summary>
        /// <param name="optionName"></param>
        /// <returns></returns>
        public bool IsOptionsDetailsPageOpened(string optionName)
        {
            // The product details page take time to load
            var _Expected_Name = FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_txtName", 30);
            var _Expected_Description = FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_txtDescription");

            if (!CurrentURL.StartsWith(BaseDashboardUrl + "/Builder/Options/Option.aspx?oid=", StringComparison.CurrentCultureIgnoreCase)
                    && CurrentURL != BaseDashboardUrl + "/Builder/Options/Option.aspx?oid=0")
                return false;
            else if (_Expected_Name != null && _Expected_Description != null)
            {
                if (_Expected_Description.GetAttribute("value") == "")
                    return _Expected_Name.GetAttribute("value") == optionName;
                else
                {
                    string expected = string.Format("{0} - {1}", _Expected_Name.GetAttribute("value"), _Expected_Description.GetAttribute("value"));
                    return expected == optionName;
                }
            }
            else
                return false;
        }
        #endregion

        #region "Recent Products"
        /// <summary>
        /// Is Products sort with newest
        /// </summary>
        public bool IsNewest_Products
        {
            get
            {
                return CommonHelper.IsEqual2ListSequence(GetListProductsNameOnGrid, GetListProductsFromDB_Newest);
            }
        }

        /// <summary>
        /// Is Products sort with oldest
        /// </summary>
        public bool IsOldest_Products
        {
            get
            {
                return CommonHelper.IsEqual2ListSequence(GetListProductsNameOnGrid, GetListProductsFromDB_Oldest);
            }
        }

        /// <summary>
        /// Is the name sort by A->Z
        /// </summary>
        public bool IsNameAscending_Products
        {
            get
            {
                return CommonHelper.IsEqual2ListSequence(GetListProductsNameOnGrid, GetListProductsFromDB_ByNameASC);
            }

        }

        /// <summary>
        /// Is the name sort by Z->A .
        /// </summary>
        public bool IsNameDescending_Products
        {
            get
            {
                return CommonHelper.IsEqual2ListSequence(GetListProductsNameOnGrid, GetListProductsFromDB_ByNameDESC);
            }
        }

        /// <summary>
        ///  Is Product Details page opened from Dashboard
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public bool IsProductsDetailsPageOpened(string productName)
        {
            var _Expected_Name = FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_txtName");
            var _Expected_Code = FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_txtDescription");
            
            if (!CurrentURL.StartsWith(BaseDashboardUrl + "/Products/Products/Product.aspx?pid=", StringComparison.CurrentCultureIgnoreCase)
                    && CurrentURL != BaseDashboardUrl + "/Products/Products/Product.aspx?pid=0")
                return false;
            else if (_Expected_Name != null && _Expected_Code != null)
            {
                if (_Expected_Code.GetAttribute("value") == string.Empty)
                    return _Expected_Name.GetAttribute("value") == productName;
                else
                {
                    string expected = string.Format("{0} - {1}", _Expected_Name.GetAttribute("value"), _Expected_Code.GetAttribute("value"));
                    return expected == productName;
                }
            }
            else
                return false;
        }
        #endregion

        #region All Panel
        /// <summary>
        /// Verify loading con after click any option from Sort by dropdown list
        /// </summary>
        /// <param name="item"></param>
        public void VerifyLoadingIconOnPanel(string item)
        {
            Label loadingIcon;
            switch (item)
            {
                case "Activity Feed":
                    //loadingIcon = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_af1_lp']/div[@class='raDiv']/div");
                    loadingIcon = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_af1_lpctl00_CPH_Content_af1_pnlActivityFeed'/div[1]");
                    break;
                case "Recent Houses":
                    loadingIcon = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_RecentHouses1_hpbLP']/div[@class='raDiv']/div");
                    break;
                case "Recent Active Jobs":
                    // There is no loading icon on this panel. xpath will be added after developer implement it.
                    loadingIcon = new Label(FindType.XPath, "");
                    break;
                case "Recent Communities":
                    loadingIcon = new Label(FindType.XPath, "//*[contains(@id, 'rptRecentCommsWrapper')]/div[@class='raDiv']");
                    break;
                case "Recent Options":
                    loadingIcon = new Label(FindType.XPath, "//*[contains(@id, 'rptRecentOptsWrapper')]/div[@class='raDiv']");
                    break;
                default:
                    // Recent Products
                    loadingIcon = new Label(FindType.XPath, "//*[contains(@id, 'rptRecentProductsWrapper')]/div[@class='raDiv']");
                    break;
            }

            // Verify loading icon on the panel
            if (loadingIcon.WaitForElementIsInVisible(10, false) is false)
            {
                // Loading icon doesn't display
                ExtentReportsHelper.LogFail($"<font color = 'red'>Can't find loading icon after clicking sort button on {item} panel.</font>");
            }
            else
            {
                // Loading icon display on the panel
                ExtentReportsHelper.LogPass($"<font color = 'green'><b>Loading icon display correctly on {item} panel.</b></font>");
            }
        }
        #endregion
    }

}
