using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Settings.Sage300CRE
{
    public partial class Sage300CREPage : DetailsContentPage<Sage300CREPage>
    {
        protected Button Running_btn => new Button(FindType.Id, "ctl00_CPH_Content_rbStatus_0");
        protected Button Paused_btn => new Button(FindType.Id, "ctl00_CPH_Content_rbStatus_1");
        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbSave");
        protected Button Configure_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbConfigure']");
        protected Label TitleModal_lbl => new Label(FindType.XPath, "//*[@class='card-header']/h1");
        protected DropdownList Section_ddl => new DropdownList(FindType.XPath, "//*[@id='ddlSection']");
        //Chracter
        protected DropdownList Character1_ddl => new DropdownList(FindType.XPath, "//*[@id='ddlCharacter1']");
        protected DropdownList Character2_ddl => new DropdownList(FindType.XPath, "//*[@id='ddlCharacter2']");
        protected DropdownList Character3_ddl => new DropdownList(FindType.XPath, "//*[@id='ddlCharacter3']");
        //Conection && Error Conection
        protected DropdownList Connection1_ddl => new DropdownList(FindType.XPath, "//*[@id='ddlConnection1']");
        protected Label ErrorConnection1_lbl => new Label(FindType.XPath, "//*[@id='lbl_ddlErrConnection1']");
        protected DropdownList Connection2_ddl => new DropdownList(FindType.XPath, "//*[@id='ddlConnection2']");
        protected Label ErrorConnection2_lbl => new Label(FindType.XPath, "//*[@id='lbl_ddlErrConnection2']");
        protected DropdownList Connection3_ddl => new DropdownList(FindType.XPath, "//*[@id='ddlConnection3']");
        protected Label ErrorConnection3_lbl => new Label(FindType.XPath, "//*[@id='lbl_ddlErrConnection3']");
        protected Button SaveConfigure_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveConfigure']");
        protected Textbox JobNumberMask_txt => new Textbox(FindType.XPath, "//*[@id='txtJobNumberMask']");
        protected Button HoverJobNumber_btn => new Button(FindType.XPath, "(//*[@class='card-section form modal-outer-body']//*)[3]");
        protected Label JobNumberTaskBreakDown_lbl => new Label(FindType.XPath, "//*[@class='tooltip-inner tooltip-inner-format']/b");
        protected Button CloseConfigure_btn => new Button(FindType.XPath, "//*[@class='card-header']//a[@class='close']");


    }
}
