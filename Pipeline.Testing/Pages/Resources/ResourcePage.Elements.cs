using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.Linq;
using Pipeline.Common.Enums;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Resources
{
    public partial class ResourcePage : DetailsContentPage<ResourcePage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Resources\Params\ResourceParams.xlsx";

        public IQueryable<Row> TestData_RT_01205 { get; set; }

        readonly string updatedXpath = string.Empty;

        public ResourcePage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            TestData_RT_01205 = ExcelHelper.GetAllRows("RT_01205");
        }

        public ResourcePage(string resourceName) : base()
        {
            updatedXpath = $"//table[contains(@id,'Resources')]/tbody/tr/td/a[starts-with(text(),'{resourceName}')]/../";
        }

        const string _resourceGrid = "//table[contains(@id,'Resources')]";
        const string _resourceGridLoading = "//*[contains(@id,'ctl00_CPH_Content_LoadingPanel1ctl00')]/div[1]";

        protected Button Apply_btn => new Button(FindType.XPath, updatedXpath + "following-sibling::td/input[contains(@src,'accept')]");
        protected Textbox UpdateTitle_txt => new Textbox(FindType.XPath, updatedXpath + "preceding::td/input[contains(@id,'txtTitle')]");
        protected Textbox NewTitle_txt => new Textbox(FindType.XPath, updatedXpath + "preceding::td/span[contains(@id,'lblTitle')]");
        protected Button Add_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddResource']");
        protected DropdownList Type_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_dpType']");
        protected Textbox Title_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtTitle']");
        protected Textbox Source_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_fuResource']");
        protected Textbox Link_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtLink']");
        protected Button InsertResources_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lnkInsertResource']");
        protected Button Cancel_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lnkCancelResource']");
        protected IGrid ResourcePage_Grid => new Grid(FindType.XPath, _resourceGrid, _resourceGridLoading);
    }
}
