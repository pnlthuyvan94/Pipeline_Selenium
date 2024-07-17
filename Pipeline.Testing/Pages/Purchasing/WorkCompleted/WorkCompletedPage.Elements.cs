using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Purchasing.WorkCompleted
{
    public partial class WorkCompletedPage : DashboardContentPage<WorkCompletedPage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Purchasing\CostCodeParams.xlsx";

        public WorkCompletedPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
        }

        protected Button MarkApprovedForPayment => new Button(FindType.Id, "ctl00_CPH_Content_lbMarkPOApproved");
    }
}
