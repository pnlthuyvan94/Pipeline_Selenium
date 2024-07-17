using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Costing.Vendor.VendorDetail
{
    public partial class VendorDetailPage : DetailsContentPage<VendorDetailPage>
    {

        protected Textbox Name_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox Code_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtCode']");

        protected DropdownList Trade_ddl
            => new DropdownList(FindType.Id, "ctl00_CPH_Content_lstBuildingTrade");

        protected Textbox Contact_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtContact");

        protected Textbox Email_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txteMail']");

        protected Textbox Address1_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddress1']");

        protected Textbox Address2_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddress2']");
        protected Textbox Address3_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddress3']");

        protected Textbox City_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtCity']");

        protected Textbox State_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtState']");

        protected Textbox Zip_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtZip']");
        protected Textbox Phone_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtPhone']");
        protected Textbox AltPhone_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtPhone2']");
        protected Textbox MobilePhone_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtMobilePhone']");
        protected Textbox Fax_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtFax']");
        protected Textbox Url_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtURL']");

        protected DropdownList TaxGroup_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlTaxGroup']");
        protected CheckBox EnablePrecision_chk
            => new CheckBox(FindType.Id, "ctl00_CPH_Content_cbPrecisionCost");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

        protected CheckBox TBD_chk => new CheckBox(FindType.Id, "ctl00_CPH_Content_chkIsTBD");
    }

}
