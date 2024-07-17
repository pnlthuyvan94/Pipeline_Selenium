using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Documents
{
    public partial class DocumentPage : DashboardContentPage<DocumentPage>
    {
        public IQueryable<Row> TestData_RT01207;

        public DocumentPage() : base()
        {
            // Sheet contains repository of Dashboard
            string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Assets\DocumentParams.xlsx";
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            TestData_RT01207 = ExcelHelper.GetAllRows("RT_01207");
        }

        // Xpath repository
        const string titleDocument_Id = "ctl00_CPH_Content_communityFilter_lblTitle";
        const string selectDocument_Xpath= "//*[@id='ctl00_CPH_Content_DocumentManager1_RadAsyncUpload1row0']/span/input[2]";
        const string tableDocument_Xpath = "//table[contains(@id,'_rgDocuments')]";
        const string loadingtable_Xpath = "//*[@id='ctl00_CPH_Content_DocumentManager1_LoadingPanel1ctl00_CPH_Content_DocumentManager1_pnlDocs']/div[1]";
        const string textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_DocumentManager1_RadAsyncUpload1file0']";

        protected Label TitleDocument_lbl => new Label(FindType.Id, titleDocument_Id);
        protected Button SelectDocument_btn => new Button(FindType.XPath, selectDocument_Xpath);
        protected IGrid DocumentsTable => new Grid(FindType.XPath, tableDocument_Xpath, loadingtable_Xpath);
        protected Textbox Upload_txt => new Textbox(FindType.XPath, textboxUpload_Xpath);

    }
}
