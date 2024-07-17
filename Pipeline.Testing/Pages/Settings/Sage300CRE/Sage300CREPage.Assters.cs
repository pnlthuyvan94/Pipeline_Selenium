using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Settings.Sage300CRE
{
    public partial class Sage300CREPage
    {

        public void VerifyJobNumberMaskBreakdown(string JobNumberTask)
        {
            HoverJobNumber_btn.WaitForElementIsVisible(2);
            HoverJobNumber_btn.HoverMouse();
            if (JobNumberTaskBreakDown_lbl.GetText().Equals(JobNumberTask))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(), $"<font color='green'><b>The Job Number Mask Breakdown Modal is displayed correctly.</b><font/>");
            }
            else
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<font color='red'>The Job Number Mask Modal is displayed incorrectly.<font/>");
            }
        }

        public void VerifyJobNumberMask(string JobNumberTask)
        {
            JobNumberMask_txt.WaitForElementIsVisible(3);
            if (JobNumberMask_txt.GetValue().Equals(JobNumberTask))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The Job Number Mask is displayed correctly with Name {JobNumberMask_txt.GetText()}.</b><font/>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The Job Number Mask is displayed incorrectly with Name {JobNumberMask_txt.GetText()}.<font/>");
            }
        }
        /// <summary>
        /// Verify Error Message Is Conection Message
        /// </summary>
        /// <param name="ErrorConnection1"></param>
        /// <param name="ErrorConnection2"></param>
        /// <param name="ErrorConnection3"></param>
        public void VerifyErrorConectionIsDisplayed(string ErrorConnection1,string ErrorConnection2, string ErrorConnection3)
        {
            if(ErrorConnection1 != string.Empty)
            if (ErrorConnection1_lbl.IsDisplayed() && ErrorConnection1_lbl.GetText().Equals(ErrorConnection1) is true)
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(), $"<font color='green'><b>The Error Conection1 is displayed correctly with Text: {ErrorConnection1_lbl.GetText()}.</b><font/>");
            }
            else
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<font color='red'>The Error Conection1 is displayed incorrectly with Text: {ErrorConnection1_lbl.GetText()}.<font/>");
            }

            if (ErrorConnection2 != string.Empty)
                if ( ErrorConnection2_lbl.IsDisplayed() && ErrorConnection2_lbl.GetText().Equals(ErrorConnection2) is true)
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(), $"<font color='green'><b>The Error Conection2 is displayed correctly with Text: {ErrorConnection2_lbl.GetText()}.</b><font/>");
            }
            else
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<font color='red'>The Error Conection2 is displayed incorrectly with Text: {ErrorConnection2_lbl.GetText()}.<font/>");
            }
            if (ErrorConnection3 != string.Empty)
            if (ErrorConnection3_lbl.IsDisplayed()  && ErrorConnection3_lbl.GetText().Equals(ErrorConnection3) is true)
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(), $"<font color='green'><b>The Error Conection3 is displayed correctly with Text: {ErrorConnection3_lbl.GetText()}.</b><font/>");
            }
            else
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<font color='red'>The Error Conection3 is displayed incorrectly with Text: {ErrorConnection3_lbl.GetText()}.<font/>");
            }
        }

        /// <summary>
        /// Verify Item is Selected In Modal
        /// </summary>
        /// <param name="Sage300CREPageData"></param>
        public void VerifyJobNumberMaskPreview(Sage300CREPageData Sage300CREPageData)
        {
            if (Sage300CREPageData.Section.ToString() == Section_ddl.SelectedValue.ToString() && Sage300CREPageData.Section.ToString() =="3")
            {
                //Verify Mask Preview1
                List<string> getlist1 = new List<string>() { };
                List<string> getlist2 = new List<string>() { };
                List<string> getlist3 = new List<string>() { };
                string CombinedList1;
                string CombinedList2;
                string CombinedList3;
                for (int Previewitem = 0; Previewitem < Sage300CREPageData.Character1; Previewitem++)
                {
                    Textbox maskPreview = new Textbox(FindType.XPath, $"//*[@id='maskPreviewD1_{Previewitem}']");
                    maskPreview.GetText();
                    string Itemvalue = maskPreview.GetText().ToString();
                    getlist1.Add(Itemvalue);
                    
                }

                CombinedList1 = string.Join("", getlist1);
                if (getlist1.Count.Equals(Sage300CREPageData.Character1))
                {

                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The MaskView1 with data: {CombinedList1} is displayed.</b><font/>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The MaskView1 with data: {CombinedList1} is not displayed.</b><font/>");
                }

                //Verify Mask Preview2
                for (int Previewitem = 0; Previewitem < Sage300CREPageData.Character2; Previewitem++)
                {
                    Textbox maskPreview = new Textbox(FindType.XPath, $"//*[@id='maskPreviewD2_{Previewitem}']");
                    maskPreview.GetText();
                    string Itemvalue = maskPreview.GetText().ToString();
                    getlist2.Add(Itemvalue);


                }
                CombinedList2 = string.Join("", getlist2);
                if (getlist2.Count.Equals(Sage300CREPageData.Character2))
                {

                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The MaskView2 with data: {CombinedList2} is displayed.</b><font/>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The MaskView2 with data: {CombinedList2} is not displayed.</b><font/>");
                }

                //Verify Mask Preview3

                for (int Previewitem = 0; Previewitem < Sage300CREPageData.Character3; Previewitem++)
                {
                    Textbox maskPreview = new Textbox(FindType.XPath, $"//*[@id='maskPreviewD3_{Previewitem}']");
                    maskPreview.GetText();
                    string Itemvalue = maskPreview.GetText().ToString();
                    getlist3.Add(Itemvalue);

                }
                CombinedList3 = string.Join("", getlist3);
                if (getlist3.Count.Equals(Sage300CREPageData.Character3))
                {

                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The MaskView3 with data: {CombinedList3} is displayed.</b><font/>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The MaskView3 with data: {CombinedList3} is not displayed.</b><font/>");
                }
            }  
            else if (Sage300CREPageData.Section.ToString() == Section_ddl.SelectedValue.ToString() && Sage300CREPageData.Section.ToString() == "2")
            {
                //Verify Mask Preview1
                List<string> getlist1 = new List<string>();
                List<string> getlist2 = new List<string>();
                string CombinedList1;
                string CombinedList2;
                for (var Previewitem = 0; Previewitem < Sage300CREPageData.Character1; Previewitem++)
                {
                    Textbox maskPreview = new Textbox(FindType.XPath, $"//*[@id='maskPreviewD1_{Previewitem}']");
                    maskPreview.GetText();
                    string Itemvalue = maskPreview.GetText().ToString();
                    getlist1.Add(Itemvalue);

                }

                CombinedList1 = string.Join("", getlist1);
                if (getlist1.Count.Equals(Sage300CREPageData.Character1))
                {

                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The MaskView1 with data: {CombinedList1} is displayed.</b><font/>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The MaskView1 with data: {CombinedList1} is not displayed.</b><font/>");
                }

                //Verify Mask Preview2
                for (var Previewitem = 0; Previewitem < Sage300CREPageData.Character2; Previewitem++)
                {
                    Textbox maskPreview = new Textbox(FindType.XPath, $"//*[@id='maskPreviewD2_{Previewitem}']");
                    maskPreview.GetText();
                    string Itemvalue = maskPreview.GetText().ToString();
                    getlist2.Add(Itemvalue);


                }
                CombinedList2 = string.Join("", getlist2);
                if (getlist2.Count.Equals(Sage300CREPageData.Character2))
                {

                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The MaskView2 with data: {CombinedList2} is displayed.</b><font/>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The MaskView2 with data: {CombinedList2} is not displayed.</b><font/>");
                }
            }
            else if (Sage300CREPageData.Section.ToString() == Section_ddl.SelectedValue.ToString() && Sage300CREPageData.Section.ToString() == "1")
            {
                //Verify Mask Preview1
                List<string> getlist1 = new List<string>();
                string CombinedList1;
                for (var Previewitem = 0; Previewitem < Sage300CREPageData.Character1; Previewitem++)
                {
                    Textbox maskPreview = new Textbox(FindType.XPath, $"//*[@id='maskPreviewD1_{Previewitem}']");
                    maskPreview.GetText();
                    string Itemvalue = maskPreview.GetText().ToString();
                    getlist1.Add(Itemvalue);

                }

                CombinedList1 = string.Join("", getlist1);
                if (getlist1.Count.Equals(Sage300CREPageData.Character1))
                {

                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The MaskView1 with data: {CombinedList1} is displayed.</b><font/>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The MaskView1 with data: {CombinedList1} is not displayed.</b><font/>");
                }
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"No Section selected in Job NumberMask Preview");
            }

            }
        }
    }


