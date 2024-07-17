using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Settings.Saberis
{
    public partial class SaberisPage : DetailsContentPage<SaberisPage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Settings\SettingsParams.xlsx";

        public IQueryable<Row> MetaData { get; set; }
        public SaberisPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("Settings_OR");
        }

        private Row url => ExcelFactory.GetRow(MetaData, 1);
        protected Textbox RootUrl_Txt => new Textbox(url);

        private Row username => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox UserName_Txt => new Textbox(username);

        private Row password => ExcelFactory.GetRow(MetaData, 3);
        protected Textbox Password_Txt => new Textbox(password);

        private Row running => ExcelFactory.GetRow(MetaData, 4);
        protected Button Running_Btn => new Button(running);

        private Row paused => ExcelFactory.GetRow(MetaData, 5);
        protected Button Paused_Btn => new Button(paused);

        private Row save => ExcelFactory.GetRow(MetaData, 6);
        protected Button Save_Btn => new Button(save);

        // Saberis page
        private Row usernameSaber => ExcelFactory.GetRow(MetaData, 7);
        protected Textbox UserNameSaberis_Txt => new Textbox(usernameSaber);

        private Row passwordSaber => ExcelFactory.GetRow(MetaData, 8);
        protected Textbox PasswordSaberis_Txt => new Textbox(passwordSaber);
        private Row logInSaberis => ExcelFactory.GetRow(MetaData, 9);
        protected Button LogInSaberis_Btn => new Button(logInSaberis);
    }
}
