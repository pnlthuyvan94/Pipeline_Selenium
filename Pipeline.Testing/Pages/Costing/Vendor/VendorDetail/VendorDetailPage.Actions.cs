
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Costing.Vendor.VendorDetail
{
    public partial class VendorDetailPage
    {
        public VendorDetailPage EnterVendorName(string name)
        {
            Name_txt.SetText(name);
            return this;
        }

        public VendorDetailPage EnterVendorCode(string code)
        {
            Code_txt.SetText(code);
            return this;
        }
        public VendorDetailPage SelectTrade(string trade)
        {
            if(!string.IsNullOrEmpty(trade))
                Trade_ddl.SelectItemByValueOrIndex(trade, 1);
            return this;
        }
        public VendorDetailPage EnterContact(string contact)
        {
            Contact_txt.SetText(contact);
            return this;
        }
        public VendorDetailPage EnterEmail(string email)
        {
            Email_txt.SetText(email);
            return this;
        }

        public VendorDetailPage EnterAddress1(string address)
        {
            Address1_txt.SetText(address);
            return this;
        }
        public VendorDetailPage EnterAddress2(string address2)
        {
            Address2_txt.SetText(address2);
            return this;
        }
        public VendorDetailPage EnterAddress3(string address3)
        {
            Address3_txt.SetText(address3);
            return this;
        }

        public VendorDetailPage EnterCity(string city)
        {
            City_txt.SetText(city);
            return this;
        }

        public VendorDetailPage EnterState(string state)
        {
            State_txt.SetText(state);
            return this;
        }

        public VendorDetailPage EnterZip(string zip)
        {
            Zip_txt.SetText(zip);
            return this;
        }

        public VendorDetailPage EnterPhone(string phone)
        {
            Phone_txt.SetText(phone);
            return this;
        }

        public VendorDetailPage EnterAltPhone(string altPhone)
        {
            AltPhone_txt.SetText(altPhone);
            return this;
        }
        public VendorDetailPage EnterMobilePhone(string mobilePhone)
        {
            MobilePhone_txt.SetText(mobilePhone);
            return this;
        }
        public VendorDetailPage EnterFax(string fax)
        {
            Fax_txt.SetText(fax);
            return this;
        }
        public VendorDetailPage EnterUrl(string url)
        {
            Url_txt.SetText(url);
            return this;
        }
        public VendorDetailPage SelectTaxGroup(string taxGroup)
        {
            if(!string.IsNullOrEmpty(taxGroup))
                TaxGroup_ddl.SelectItemByValueOrIndex(taxGroup, 1);
            return this;
        }
        public VendorDetailPage SetPrecisionCheckbox(bool check)
        {
            if (check)
                EnablePrecision_chk.Check();
            else
                EnablePrecision_chk.UnCheck();
            return this;
        }
        public void CreateOrUpdateAVendor(VendorData data)
        {
            EnterVendorName(data.Name)
                .EnterVendorCode(data.Code)
                .SelectTrade(data.Trade)
                .EnterEmail(data.Email)
                .EnterAddress1(data.Address1)
                .EnterAddress2(data.Address2)
                .EnterAddress3(data.Address3)
                .EnterCity(data.City)
                .EnterState(data.State)
                .EnterZip(data.Zip)
                .EnterPhone(data.Phone)
                .EnterAltPhone(data.AltPhone)
                .EnterMobilePhone(data.MobilePhone)
                .EnterFax(data.Fax)
                .EnterUrl(data.Url)
                .SelectTaxGroup(data.TaxGroup)
                .SetPrecisionCheckbox(data.EnablePrecision)
                .Save();
            Label VendorTitle_Lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lblTitle' and text()='{data.Name}']");
            VendorTitle_Lbl.WaitUntilExist(10);
        }

        public VendorDetailPage SetTBD(bool isTbd)
        {
            if (isTbd)
                TBD_chk.Check();
            else
                TBD_chk.UnCheck();
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            PageLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveContinue']/div[1]");
        }

    }
}
