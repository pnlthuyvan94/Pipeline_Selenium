using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.CommunityDetail
{
    public partial class CommunityDetailPage : DetailsContentPage<CommunityDetailPage>
    {

        // Community Detail
        protected Textbox CommunityName_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtCommunityName");

        protected DropdownList Division_ddl =>
            new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlDivisions");

        protected Textbox Code_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtCode");

        protected Textbox City_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtCity");

        protected Textbox CityLink_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtCityLink");

        protected Textbox Township_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtTownship");

        protected Textbox County_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtCounty");

        protected Textbox State_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtState");

        protected Textbox Zip_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtZip");

        protected Textbox SchoolDesstrict_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtSchoolDistrict");

        protected Textbox SchoolDesstrictLink_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtSchoolDistrictLink");

        protected DropdownList Status_ddl
            => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlStatusTypes");

        protected Textbox Description_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_radDescription_contentDiv");

        protected Textbox DrivingDescription_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_radDrivingDirections_contentDiv");

        protected Textbox Slug_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtSlug");

        protected Button Save_btn
            => new Button(FindType.Id, "ctl00_CPH_Content_lbSave");

        // Community Planner
        public Button AddLotStatus_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNewStatus']");

        protected DropdownList PlannerStatus_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlUnusedStatus']");

        protected Button AddLotStatusSub_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertStatus']");

        string selectedStatusXpath => "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rptStatusCategoriesEditPanel']/section/label";

        protected ListItem SelectedStatus_lst
          => new ListItem(FindElementHelper.FindElements(FindType.XPath, selectedStatusXpath));

        protected Textbox UploadCommunity_txt
            => new Textbox(FindType.XPath, "//*[starts-with(@id,'ctl00_CPH_Content_ruCommunityImagefile')]");

    }
}
