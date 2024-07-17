using LinqToExcel;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.OptionSelection;
using Pipeline.Testing.Pages.Assets.OptionSelection.SelectionDetail;
using Pipeline.Testing.Pages.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A08_A_RT_01223 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        Row oldData;
        readonly string newSelectionName = "Update the selection Name";
        readonly string newSelectionGroup = "Carpet";
        readonly bool newCustomizable = true;
        readonly string newTitle = "New value of Title";
        readonly string[] optionList = { "CH_OP6", "CH_OP7" };

        // Pre-condition
        [SetUp]
        public void GetTestData()
        {
            oldData = ExcelFactory.GetRow(OptionSelectionPage.Instance.TestData_RT01078, 1);
            OptionSelectionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionSelections);

            OptionSelectionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldData["Option Selection Name"]);
            if (!OptionSelectionPage.Instance.IsItemInGrid("Name", oldData["Option Selection Name"]))
            {
                // Step 2: click on "+" Add button
                OptionSelectionPage.Instance.ClickAddToOptionSelectionModal();

                OptionSelectionPage.Instance.AddOptionSelectionModal
                                          .EnterOptionSelectionName(oldData["Option Selection Name"])
                                          .SelectOptionSelectionGroup(oldData["Option Selection Group"])
                                          .CheckCustomize("TRUE".Equals(oldData["Customizable"]));

                // Step 4. Select the 'Save' button on the modal;
                OptionSelectionPage.Instance.AddOptionSelectionModal.Save();

                // Verify successful save and appropriate success message.
                string _expectedMessage = "Option Selection successfully added.";
                string _actualMessage = OptionSelectionPage.Instance.AddOptionSelectionModal.GetLastestToastMessage();
                if (_expectedMessage.Equals(_actualMessage))
                {
                    ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                    OptionSelectionPage.Instance.AddOptionSelectionModal.CloseToastMessage();
                }
                else if (!string.IsNullOrEmpty(_actualMessage))
                    ExtentReportsHelper.LogFail($"The message does not as expected. \nActual results: {_actualMessage}\nExpected results: {_expectedMessage} ");

               // OptionSelectionPage.Instance.AddOptionSelectionModal.CloseModal();

                OptionSelectionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldData["Option Selection Name"]);
            }

            OptionSelectionPage.Instance.SelectItemInGridWithTextContains("Name", oldData["Option Selection Name"]);
        }

        [Test]
        [Category("Section_IV")]
        [Ignore("The Option Selection was removed from Asset menu so this test sript will be ignored.")]
        public void A08_A_Assets_DetailPage_OptionSelection_Detail()
        {
            // Update Option Selection detail
            #region "Update selection detail"
            SelectionDetailPage.Instance.UpdateSelection(newSelectionName, newSelectionGroup, newCustomizable);
            string expectedMsg = $"Successfully saved selection {newSelectionName}.";
            string actualMsg = SelectionDetailPage.Instance.GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass("Successfully update the selection.");
                SelectionDetailPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(actualMsg))
            {
                ExtentReportsHelper.LogFail($"The selection is NOT update successfully.<br>Actual result:{actualMsg}<br>Expected result:{expectedMsg}");
                SelectionDetailPage.Instance.CloseToastMessage();
            }
            #endregion

            #region "Testing 3 method of upload: Image - Link - Document"
            foreach (ResourceTypes type in Enum.GetValues(typeof(ResourceTypes)))
            {
                SelectionDetailPage.Instance.SwitchResourseType(type);
                if (type.Equals(ResourceTypes.Link))
                {
                    SelectionDetailPage.Instance.UploadLink("Current url", SelectionDetailPage.Instance.CurrentURL);
                    expectedMsg = "Selection resource saved successfully!";
                    actualMsg = SelectionDetailPage.Instance.GetLastestToastMessage();
                    if (expectedMsg.Equals(actualMsg))
                    {
                        ExtentReportsHelper.LogPass($"The selection is uploaded resource with Title <b>'Current url'</b> successfully.");
                        SelectionDetailPage.Instance.CloseToastMessage();
                    }
                    else if (!string.IsNullOrEmpty(actualMsg))
                        ExtentReportsHelper.LogFail($"The selection is NOT upload resource successfully.<br>Actual result:{actualMsg}<br>Expected result:{expectedMsg}");
                    SelectionDetailPage.Instance.CloseToastMessage();
                    SelectionDetailPage.Instance.FilterResourceOnGrid("Title", GridFilterOperator.Contains, "Current url");
                    Assert.That(SelectionDetailPage.Instance.IsItemDisplayOnResourceGrid("Title", "Current url"));

                    SelectionDetailPage.Instance.DeleteItemOnResourceGrid("Title", "Current url");
                    expectedMsg = "Selection resource successfully removed.";
                    actualMsg = SelectionDetailPage.Instance.GetLastestToastMessage();
                    if (expectedMsg.Equals(actualMsg))
                        ExtentReportsHelper.LogPass("Selection resource successfully removed.");
                    SelectionDetailPage.Instance.CloseToastMessage();
                    ClassicAssert.IsFalse(SelectionDetailPage.Instance.IsItemDisplayOnResourceGrid("Title", "Current url"));
                }
                else
                {
                    IList<string> imgList, filePaths;
                    bool validFile, isUpdated;

                    if (type.Equals(ResourceTypes.Document))
                    {
                        #region "Upload document and verify"
                        //switch to document
                        filePaths = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/DataInputFiles/Documents", "*.*",
                                                     SearchOption.TopDirectoryOnly);
                        isUpdated = false;
                        // Upload document - and only allow: txt,pdf,doc,docx
                        foreach (var item in filePaths)
                        {
                            SelectionDetailPage.Instance.SwitchResourseType(ResourceTypes.Document);
                            string extention = Path.GetExtension(item);
                            if (extention.Equals(".txt") || extention.Equals(".pdf") || extention.Equals(".doc") || extention.Equals(".docx"))
                            {
                                validFile = true;
                                expectedMsg = "Selection resource saved successfully!";
                            }
                            else
                            {
                                expectedMsg = "Document must have one of the following extensions: .txt, .pdf, .doc, .docx";
                                validFile = false;
                            }
                            // Upload document
                            SelectionDetailPage.Instance.UploadImageOrDocument(item);
                            actualMsg = SelectionDetailPage.Instance.GetLastestToastMessage();
                            if (expectedMsg.Equals(actualMsg))
                            {
                                ExtentReportsHelper.LogPass($"The selection is uploaded resource with item <font color='green'><b>'{Path.GetFileName(item)}'</b></font> successfully.");
                                SelectionDetailPage.Instance.CloseToastMessage();
                            }
                            else if (!string.IsNullOrEmpty(actualMsg))
                            {
                                ExtentReportsHelper.LogFail($"The selection with extention <font color='green'><b>{extention}</b></font> is NOT upload resource with correct logic.<br>Actual result:{actualMsg}<br>Expected result:{expectedMsg}");
                                SelectionDetailPage.Instance.CloseToastMessage();
                            }
                            if (validFile)
                                SelectionDetailPage.Instance.RefreshPage();
                            if (SelectionDetailPage.Instance.IsItemDisplayOnResourceGrid("Source", Path.GetFileName(item)))
                            {
                                if (validFile)
                                    ExtentReportsHelper.LogPass($"The selection is imported successfully with extention file <b>{extention}</b>");
                                else
                                    ExtentReportsHelper.LogFail($"The selection is imported successfully with invalid extention file <b>{extention}</b>");
                                if (!isUpdated)
                                {
                                    // edit and verify 
                                    SelectionDetailPage.Instance.UpdateItemInResoucesGridAndVerify("Title", Path.GetFileNameWithoutExtension(item), newTitle);
                                    Assert.That(SelectionDetailPage.Instance.IsItemDisplayOnResourceGrid("Title", newTitle));
                                    isUpdated = true;
                                }
                                // Delete item in grid
                                SelectionDetailPage.Instance.DeleteItemOnResourceGrid("Source", Path.GetFileName(item));
                                expectedMsg = "Selection resource successfully removed.";
                                actualMsg = SelectionDetailPage.Instance.GetLastestToastMessage();
                                if (expectedMsg.Equals(actualMsg))
                                    ExtentReportsHelper.LogPass("Selection resource successfully removed.");
                                SelectionDetailPage.Instance.CloseToastMessage();
                                ClassicAssert.IsFalse(SelectionDetailPage.Instance.IsItemDisplayOnResourceGrid("Source", Path.GetFileName(item)));
                            }
                            else
                            {
                                if (validFile)
                                    ExtentReportsHelper.LogFail($"The selection is imported unsuccessfully with invalid extention file <b>{extention}</b>");
                                else
                                    ExtentReportsHelper.LogPass($"The selection is NOT imported successfully with extention file <b>{extention}</b> as expectation");
                            }
                        }
                        #endregion

                    }
                    else
                    {
                        // Verify the upload file type
                        filePaths = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/DataInputFiles/Resources", "*.*",
                                                    SearchOption.TopDirectoryOnly);
                        imgList = filePaths.Where(p => !p.EndsWith("pdf") && !p.EndsWith("xlsx") && !p.EndsWith("mpeg")).ToList();

                        #region "Upload image and verify"

                        validFile = false;
                        isUpdated = false;
                        // Upload img and only allow jpg,png,gif
                        foreach (var item in imgList)
                        {
                            SelectionDetailPage.Instance.SwitchResourseType(ResourceTypes.Image);
                            string extention = Path.GetExtension(item);
                            if (extention.Equals(".png") || extention.Equals(".jpg") || extention.Equals(".gif"))
                            {
                                validFile = true;
                                expectedMsg = "Selection resource saved successfully!";
                            }
                            else
                            {
                                expectedMsg = "Image must have one of the following extensions: .jpg, .png, .gif";
                                validFile = false;
                            }
                            SelectionDetailPage.Instance.UploadImageOrDocument(item);
                            actualMsg = SelectionDetailPage.Instance.GetLastestToastMessage();
                            if (expectedMsg.Equals(actualMsg))
                            {
                                ExtentReportsHelper.LogPass($"The selection is uploaded resource with item <font color='green'><b>'{Path.GetFileName(item)}'</b></font> successfully.");
                                SelectionDetailPage.Instance.CloseToastMessage();
                            }
                            else if (!string.IsNullOrEmpty(actualMsg))
                            {
                                ExtentReportsHelper.LogFail($"The selection with extention <font color='green'><b>{extention}</b></font> is NOT upload resource with correct logic.<br>Actual result:{actualMsg}<br>Expected result:{expectedMsg}");
                                SelectionDetailPage.Instance.CloseToastMessage();
                            }

                            // Reload page if upload successfully
                            if (validFile)
                                SelectionDetailPage.Instance.RefreshPage();

                            if (SelectionDetailPage.Instance.IsItemDisplayOnResourceGrid("Title", Path.GetFileNameWithoutExtension(item)))
                            {
                                if (validFile)
                                    ExtentReportsHelper.LogPass($"The selection is imported successfully with extention file <b>{extention}</b>");
                                else
                                    ExtentReportsHelper.LogFail($"The selection is imported successfully with invalid extention file <b>{extention}</b>");
                                if (!isUpdated)
                                {
                                    // edit and verify 
                                    SelectionDetailPage.Instance.UpdateItemInResoucesGridAndVerify("Title", Path.GetFileNameWithoutExtension(item), newTitle);
                                    Assert.That(SelectionDetailPage.Instance.IsItemDisplayOnResourceGrid("Title", newTitle));
                                    isUpdated = true;
                                }
                                // Delete item in grid
                                SelectionDetailPage.Instance.DeleteItemOnResourceGrid("Source", Path.GetFileName(item));
                                expectedMsg = "Selection resource successfully removed.";
                                actualMsg = SelectionDetailPage.Instance.GetLastestToastMessage();
                                if (expectedMsg.Equals(actualMsg))
                                {
                                    ExtentReportsHelper.LogPass("Selection resource successfully removed.");
                                    SelectionDetailPage.Instance.CloseToastMessage();
                                }
                                ClassicAssert.IsFalse(SelectionDetailPage.Instance.IsItemDisplayOnResourceGrid("Source", Path.GetFileName(item)));
                            }
                            else
                            {
                                if (validFile)
                                    ExtentReportsHelper.LogFail($"The selection is imported unsuccessfully with invalid extention file <b>{extention}</b>");
                                else
                                    ExtentReportsHelper.LogPass($"The selection is NOT imported successfully with extention file <b>{extention}</b> as expectation");
                            }
                        }
                        #endregion
                    }
                }
            }
            #endregion


            #region "Add option(s) to this Selection
            // Add Option to Selection
            SelectionDetailPage.Instance.ClickAddToShowAddOptionModal().SelectOptionOnOptionList(optionList).InsertOptionToSelection();
            expectedMsg = $"Option Selections were successfully added.";
            actualMsg = SelectionDetailPage.Instance.GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
                ExtentReportsHelper.LogPass("Selection add Option(s) successfully.");
            SelectionDetailPage.Instance.CloseToastMessage();

            //SelectionDetailPage.Instance.CloseAddOptionToSelectionModal();

            // Verify Option is displayed
            foreach (var item in optionList)
            {
                if (SelectionDetailPage.Instance.IsItemInOptionGrid("Name", item))
                {
                    ExtentReportsHelper.LogPass($"Option with name <font color='green'><b>{item}</b></font> is added successfully.");
                    SelectionDetailPage.Instance.DeleteItemInOptionGrid("Name", item);
                    expectedMsg = $"Option Selections successfully removed.";
                    actualMsg = SelectionDetailPage.Instance.GetLastestToastMessage();
                    if (expectedMsg.Equals(actualMsg))
                        ExtentReportsHelper.LogPass("Selection add Option(s) successfully.");
                    SelectionDetailPage.Instance.CloseToastMessage();

                    if (SelectionDetailPage.Instance.IsItemInOptionGrid("Name", item))
                        ExtentReportsHelper.LogFail($"Selection is NOT removed Option {item} successfully.");
                    else
                        ExtentReportsHelper.LogPass($"Selection removed Option {item} successfully.");

                }
                else
                    ExtentReportsHelper.LogFail($"Option with name <font color='green'><b>{item}</b></font> is NOT add successfully.");
            }
            #endregion
        }

        [TearDown]
        public void DeleteSelection()
        {
            ExtentReportsHelper.LogPass(null, "**********************Clean up data*************************");

            SelectionDetailPage.Instance.UpdateSelection(oldData["Option Selection Name"], newSelectionGroup, newCustomizable);
            ExtentReportsHelper.LogInformation(SelectionDetailPage.Instance.GetLastestToastMessage());
            SelectionDetailPage.Instance.CloseToastMessage();
            OptionSelectionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionSelections);
            OptionSelectionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldData["Option Selection Name"]);

            // 5. Select the trash can to delete the new OptionSelection; 
            OptionSelectionPage.Instance.DeleteItemInGrid("Name", oldData["Option Selection Name"]);
            OptionSelectionPage.Instance.WaitGridLoad();
            if ("Selection successfully removed." == OptionSelectionPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("Selection deleted successfully.");
                OptionSelectionPage.Instance.CloseToastMessage();
            }
            else
            {
                if (OptionSelectionPage.Instance.IsItemInGrid("Name", oldData["Option Selection Name"]))
                    ExtentReportsHelper.LogFail("Selection could not be deleted.");
                else
                    ExtentReportsHelper.LogPass("Selection deleted successfully.");
            }
        }

    }
}
