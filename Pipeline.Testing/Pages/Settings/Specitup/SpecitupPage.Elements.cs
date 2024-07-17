using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Settings.Specitup
{
    public partial class SpecitupPage : DetailsContentPage<SpecitupPage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Settings\SettingsParams.xlsx";

        public IQueryable<Row> MetaData { get; set; }
        public SpecitupPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("Settings_OR");
        }

        private Row url => ExcelFactory.GetRow(MetaData, 19);
        protected Textbox RootUrl_Txt => new Textbox(url);

        private Row username => ExcelFactory.GetRow(MetaData, 20);
        protected Textbox UserName_Txt => new Textbox(username);

        private Row password => ExcelFactory.GetRow(MetaData, 21);
        protected Textbox Password_Txt => new Textbox(password);

        private Row running => ExcelFactory.GetRow(MetaData, 22);
        protected Button Running_Btn => new Button(running);

        private Row paused => ExcelFactory.GetRow(MetaData, 23);
        protected Button Paused_Btn => new Button(paused);

        private Row save => ExcelFactory.GetRow(MetaData, 24);
        protected Button Save_Btn => new Button(save);

    }
}
