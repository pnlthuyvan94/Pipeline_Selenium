using Pipeline.Common.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Costing.CostingEstimate
{
    public partial class CostingEstimatesPage
    {
        public void ClickHouseEstimates()
        {
            HouseEstimate_rbtn.Click();
            WaitHouseGridLoad();
        }               

        public CostingEstimatesPage SelectHouse(string house)
        {
            if (!string.IsNullOrEmpty(house))
                House_ddl.SelectItem(house);

            WaitHouseGridLoad();
            return this;
        }

        public CostingEstimatesPage SelectCommunity(string community)
        {
            if (!string.IsNullOrEmpty(community))
                Community_ddl.SelectItem(community);

            WaitHouseGridLoad();
            return this;
        }

        public bool IsItemInHouseGrid(string columnName, string values)
        {
            return HouseEstimates_Grid.IsItemOnCurrentPage(columnName, values);
        }
        public void WaitHouseGridLoad()
        {
            WaitingLoadingGifByXpath(_houseGridLoading);
        }

        public void ClickJobEstimates()
        {
            JobEstimate_rbtn.Click();
            WaitJobGridLoad();
        }
        public CostingEstimatesPage SelectJob(string jobNo)
        {
            if (!string.IsNullOrEmpty(jobNo))
                Job_ddl.SelectItem(jobNo);

            WaitJobGridLoad();
            return this;
        }

        public CostingEstimatesPage SelectLatestGeneratedBom()
        {
            GeneratedBOM_ddl.SelectItemByValueOrIndex("", 0);
            WaitJobGridLoad();
            return this;
        }

        public CostingEstimatesPage SelectView(string view)
        {
            if (!string.IsNullOrEmpty(view))
                ViewType_ddl.SelectItem(view);

            WaitJobGridLoad();
            return this;
        }
        public bool IsItemInJobGrid(string columnName, string values)
        {
            return JobEstimates_Grid.IsItemOnCurrentPage(columnName, values);
        }
       
        public void WaitJobGridLoad()
        {
            WaitingLoadingGifByXpath(_jobGridLoading);
        }
    }
}
