using LinqToExcel;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Login
{
    public partial class LoginPage : BasePage
    {
        public LoginPage(string pathOfExcelFile) : base()
        {
            ExcelHelper = new ExcelFactory(pathOfExcelFile);

            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("Login_OR");
        }

        protected static IQueryable<Row> MetaData { get; set; }

        private Row _title { get { return ExcelFactory.GetRow(MetaData, 1); } }
        protected Label Title_lbl => new Label(_title);

        private Row _userName { get { return ExcelFactory.GetRow(MetaData, 2); } }
        protected Textbox UserName_txt => new Textbox(_userName);

        private Row _password { get { return ExcelFactory.GetRow(MetaData, 3); } }
        protected Textbox Password_txt => new Textbox(_password);

        private Row _signIn { get { return ExcelFactory.GetRow(MetaData, 4); } }
        protected Button Login_btn => new Button(_signIn);
    }
}
