using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.Options.AddOption
{
    public partial class AddOptionModal
    {
        public bool IsDefaultValues
        {
            get
            {
                if (!string.IsNullOrEmpty(OptionName_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(OptionNumber_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(SquareFootage_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(Description_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(SaleDescription_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(Price_txt.GetText()))
                    return false;
                if (!"NONE".Equals(OptionGroup_ddl.SelectedItem.Text))
                    return false;
                if (!"NONE".Equals(OptionRoom_ddl.SelectedItem.Text))
                    return false;
                if (!"NONE".Equals(CostGroup_ddl.SelectedItem.Text))
                    return false;
                if (!"NONE".Equals(OptionType_ddl.SelectedItem.Text))
                    return false;
                if (Type.Elevation.IsChecked)
                    return false;
                if (Type.AllowMultiples.IsChecked)
                    return false;
                if (Type.Global.IsChecked)
                    return false;
                return true;
            }
        }

        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(10))
                throw new TimeoutException("The \"Create Option\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Create Option");
        }
        public bool IsOptionGroupInList(string data)
        {
            if (OptionGroup_ddl.IsItemInList(data))
            {
                ExtentReportsHelper.LogInformation($"Items {data} is displayed in list");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Items {data} is not displayed in list");
                return false;
            }
        }

    }
}
