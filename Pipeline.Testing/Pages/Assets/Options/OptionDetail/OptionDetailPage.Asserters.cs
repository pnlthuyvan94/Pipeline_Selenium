
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.Options.OptionDetail
{
    public partial class OptionDetailPage
    {
        /// <summary>
        /// Verify head title and id of new manufacturer 
        /// </summary>
        /// <param name="OptionName"></param>
        /// <returns></returns>
        public bool IsSaveOptionSuccessful(string OptionName)
        {
            System.Threading.Thread.Sleep(1000);
            return SubHeadTitle().Equals(OptionName) && !CurrentURL.EndsWith("oid=0");
        }

        /// <summary>
        /// Verify the Option save data correct or not
        /// </summary>
        /// <param name="OptionData"></param>
        /// <returns></returns>
        public void IsSaveOptionData(OptionData OptionData)
        {
            bool result = true;
            if (OptionData.Name != OptionName_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Option Name.<br>Expected result: {OptionData.Name}.<br>Actual result: {OptionName_txt.GetValue()}");
                result =  false;
            }
            if (OptionData.Number != OptionNumber_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Option Number.<br>Expected result: {OptionData.Number}.<br>Actual result: {OptionNumber_txt.GetValue()}");
                result = false;
            }
            if (OptionData.SquareFootage.ToString() != SquareFootage_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Square footage.<br>Expected result: {OptionData.SquareFootage}.<br>Actual result: {SquareFootage_txt.GetValue()}");
                result = false;
            }
            if (OptionData.Description != Description_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Description.<br>Expected result: {OptionData.Description}.<br>Actual result: {Description_txt.GetText()}");
                result = false;
            }
            if (OptionData.SaleDescription != SaleDescription_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Sale Description.<br>Expected result: {OptionData.SaleDescription}.<br>Actual result: {SaleDescription_txt.GetText()}");
                result = false;
            }
            if (OptionData.OptionGroup != OptionGroup_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Option Group.<br>Expected result: {OptionData.OptionGroup}.<br>Actual result: {OptionGroup_ddl.SelectedItemName}");
                result = false;
            }
            //if (OptionData.OptionRoom != OptionRoom_ddl.SelectedItemName)
            //{
            //    ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Option Room.<br>Expected result: {OptionData.OptionRoom}.<br>Actual result: {OptionRoom_ddl.SelectedItemName}");
            //    result =  false;
            //}
            if (OptionData.CostGroup != CostGroup_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Cost Group.<br>Expected result: {OptionData.CostGroup}.<br>Actual result: {CostGroup_ddl.SelectedItemName}");
                result = false;
            }
            if (OptionData.OptionType != OptionType_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Option Type.<br>Expected result: {OptionData.OptionType}.<br>Actual result: {OptionType_ddl.SelectedItemName}");
                result = false;
            }
            if (Math.Round(OptionData.Price, 2).ToString("0.00") != Price_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Price.<br>Expected result: {Math.Round(OptionData.Price, 2).ToString("0.00")}.<br>Actual result: {Price_txt.GetValue()}");
                result = false;
            }
            //if (OptionData.Types[0] != Type.Elevation.IsChecked)
            //{
            //    string state = "checked";
            //    if (!OptionData.Types[0]) state = "uncheck";

            //    ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Option Elevation Type.<br>Expected result: Elevation {state}.");
            //    return false;
            //}
            //if (OptionData.Types[1] != Type.AllowMultiples.IsChecked)
            //{
            //    var expect = "checked";
            //    if (!OptionData.Types[1]) expect = "uncheck";
            //    var actual = "checked";
            //    if (!Type.Elevation.IsChecked) actual = "uncheck";

            //    ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Option Allow Multiples Type.<br>Expected result: Allow Multiples {expect}.<br>Actual result: {actual}");
            //    return false;
            //}
            //if (OptionData.Types[2] != Type.Global.IsChecked)
            //{
            //    var expect = "checked";
            //    if (!OptionData.Types[2]) expect = "uncheck";
            //    var actual = "checked";
            //    if (!Type.Elevation.IsChecked) actual = "uncheck";

            //    ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Option Global Type.<br>Expected result: Global {expect}.<br>Actual result: {actual}");
            //    return false;
            //}
            if (result)
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(), $"<font color = 'green'><b>Option information displays correctly on the detail page!.</b></font>");
        }

        public bool IsItemDisplayedOnGrid(string columnName, params string[] valueToFinds)
        {
            bool isPassed = true;
            foreach (var name in valueToFinds)
            {
                if (!Selection_Grid.IsItemOnCurrentPage(columnName, name))
                {
                    isPassed = false;
                    ExtentReportsHelper.LogFail($"The item with name <font color='green'><b>{name}</b></font> is NOT display on the column name <font color='green'><b>{columnName}</b></font>");
                }
            }
            return isPassed;
        }
		 public bool IsOptionTypeChecked(string optionCategory)
        {   
            switch (optionCategory)
            {
                case "Elevation":
                    return CommonHelper.IsElementChecked("ctl00_CPH_Content_ckbIsElevation");
                case "AllowMultiples":
                    return CommonHelper.IsElementChecked("ctl00_CPH_Content_ckbAllowMultiples");
                case "Global":
                    return CommonHelper.IsElementChecked("ctl00_CPH_Content_ckbIsGlobal");
                default:
                    return false;
            }      
        }
    }
}