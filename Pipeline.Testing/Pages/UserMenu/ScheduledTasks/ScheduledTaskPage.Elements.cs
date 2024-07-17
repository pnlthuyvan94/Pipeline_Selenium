using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.UserMenu.ScheduledTasks.AddScheduledTask;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.UserMenu.ScheduledTasks
{
    public partial class ScheduledTaskPage : DashboardContentPage<ScheduledTaskPage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\UserMenu\ScheduledTasksParams.xlsx";

        public IQueryable<Row> MetaData { get; set; }

        public IQueryable<Row> TestData_RT01109 { get; set; }

        public AddScheduledTaskModal AddScheduledTaskModal { get; private set; }

        public ScheduledTaskPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("ScheduledTask_OR");
            TestData_RT01109 = ExcelHelper.GetAllRows("RT_01109");
        }

        private Row _grid => ExcelFactory.GetRow(MetaData, 12);

        // Description - loading grid
        private Row _TaskloadingGifRow => ExcelFactory.GetRow(MetaData, 15);
        private string _TaskGridLoading => _TaskloadingGifRow[BaseConstants.ValueToFind];
        protected IGrid ScheduledTask_Grid => new Grid(_grid, _TaskGridLoading);
    }
}
