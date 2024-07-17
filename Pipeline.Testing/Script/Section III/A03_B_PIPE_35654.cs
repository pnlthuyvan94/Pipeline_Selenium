using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Assets.Series.SeriesDetail;

namespace Pipeline.Testing.Script.Section_III
{
    class A03_B_PIPE_35654 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        SeriesData oldSeriesData;
        SeriesData SeriesData;
        SeriesData EmptyNameData;
        SeriesData newSeriesData;
        SeriesData UpdateEmptySeriesData;
        SeriesData UpdateSeriesData;

        [SetUp]
        public void GetData()
        {
            oldSeriesData = new SeriesData()
            {
                Name = "QA_RT_Serie3_Automation",
                Code = string.Empty,
                Description = "Please no not remove or modify"
            };

            SeriesData = new SeriesData()
            {
                Name = "QA_RT_Series_Automation",
                Code = "5100",
                Description = "Please do not delete this series, use for the automation purpose"
            };

            EmptyNameData = new SeriesData()
            {
                Name = string.Empty
            };


            newSeriesData = new SeriesData()
            {
                Name = "QA_RT_New_Series_Automation",
                Code = "1459",
                Description = "Please do not delete this series, use for the automation purpose"
            };

            UpdateEmptySeriesData = new SeriesData()
            {
                Name = string.Empty,
                Code = string.Empty,
                Description=string.Empty
            };

            UpdateSeriesData = new SeriesData()
            {
                Name = "QA_RT_New_Series123_Automation",
                Code = "1460",
                Description = "Please do not delete this series, use for the automation purpose"
            };

            //Prepare Series Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare to Series Page.</font>");
            // Go to the Series default page
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);

            // Filter the created series 
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldSeriesData.Name);

            // Verify the item is display in list
            if (!SeriesPage.Instance.IsItemInGrid("Name", oldSeriesData.Name))
            {
                // Create new series to test
                SeriesPage.Instance.ClickAddToSeriesModal();

                Assert.That(SeriesPage.Instance.AddSeriesModal.IsModalDisplayed(), "Add Series modal is not displayed.");

                SeriesPage.Instance.AddSeriesModal
                                         .EnterSeriesName(oldSeriesData.Name)
                                         .EnterSeriesCode(oldSeriesData.Code)
                                         .EnterSeriesDescription(oldSeriesData.Description);


                // Select the 'Save' button on the modal;
                SeriesPage.Instance.AddSeriesModal.Save();

                // Verify successful save and appropriate success message.
                string _expectedMessage = "Series " + oldSeriesData.Name + " created successfully!";
                string _actualMessage = SeriesPage.Instance.AddSeriesModal.GetLastestToastMessage();
                if (_expectedMessage.Equals(_actualMessage))
                {
                    ExtentReportsHelper.LogPass("<font color ='green'><b>The message is dispalyed as expected. Actual results: " + _actualMessage);
                    SeriesPage.Instance.CloseToastMessage();
                }
                else
                    ExtentReportsHelper.LogFail($"The message does not as expected. \nActual results: {_actualMessage}\nExpected results: {_expectedMessage} ");

                // Close modal
                //SeriesPage.Instance.AddSeriesModal.CloseModal();
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Serires with name {oldSeriesData.Name} is displayed in grid.</font>");
            }
        }

        [Test]
        [Category("Section_III")]
        public void A03_B_Assets_Series_The_exactly_error_message_should_display_when_create_and_edit_series()
        {
            //Step 1: Verify the error message show when add Series with blank value
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1: Verify the error message show when add Series with blank value.</font>");           
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);
            
            SeriesPage.Instance.ClickAddToSeriesModal();
            Assert.That(SeriesPage.Instance.AddSeriesModal.IsModalDisplayed(), "Add Series modal is not displayed.");

            SeriesPage.Instance.AddSeriesModal
                                      .EnterSeriesName(string.Empty);

            SeriesPage.Instance.AddSeriesModal.Save();

            // Verify successful save and appropriate success message.
            string _expectedErrorMessage = "**Name is required";
            string _actualErrorMessage = SeriesPage.Instance.AddSeriesModal.IsModalNameIsDisplayed();
            if (_expectedErrorMessage.Equals(_actualErrorMessage))
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>The label message is dispalyed as expected.</b></font> Actual results: " + _actualErrorMessage);
            }
            else
                ExtentReportsHelper.LogWarning($"The message does not as expected. \nActual results: {_actualErrorMessage}\nExpected results: {_expectedErrorMessage} ");
            

            // Close modal if it's open
            SeriesPage.Instance.AddSeriesModal.CloseModal();

            CommonHelper.RefreshPage();

            //Step 2: Verify the error message show when add Series with Name already existed in the system
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2: Verify the error message show when add Series with Name already existed in the system.</font>");
            SeriesPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, oldSeriesData.Code);
            if(SeriesPage.Instance.IsItemInGrid("Code", oldSeriesData.Code) is true)
            {
                SeriesPage.Instance.ClickAddToSeriesModal(); 

                //Populate all values
                SeriesPage.Instance.AddSeriesModal
                                          .EnterSeriesName(oldSeriesData.Name)
                                          .EnterSeriesCode(oldSeriesData.Code)
                                          .EnterSeriesDescription(oldSeriesData.Description);

                 _actualErrorMessage = SeriesPage.Instance.AddSeriesModal.Save();

                // Verify successful save and appropriate success message.
                _expectedErrorMessage = $"Failed to create {oldSeriesData.Name}. The Series Name must be unique.";
                if (_expectedErrorMessage.Equals(_actualErrorMessage))
                {
                    ExtentReportsHelper.LogPass("<font color ='green'><b>The message is dispalyed as expected.</b></font> Actual results: " + _actualErrorMessage);
                }
                else
                    ExtentReportsHelper.LogWarning($"The message does not as expected. \nActual results: {_actualErrorMessage}\nExpected results: {_expectedErrorMessage} ");


                // Close modal if it's open
                SeriesPage.Instance.AddSeriesModal.CloseModal();

                CommonHelper.RefreshPage();
            }


            //Step 3: Verify the toast message show when add Series successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3: Verify the toast message show when add Series successfully.</font>");
            SeriesPage.Instance.ClickAddToSeriesModal();

            SeriesPage.Instance.AddSeriesModal
                                      .EnterSeriesName(SeriesData.Name)
                                      .EnterSeriesCode(SeriesData.Code)
                                      .EnterSeriesDescription(SeriesData.Description);

            string _actualMessage = SeriesPage.Instance.AddSeriesModal.Save();

            // Verify successful save and appropriate success message.
            string _expectedMessage = "Series " + SeriesData.Name + " created successfully!";
            if (_expectedMessage.Equals(_actualMessage))
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>The message is dispalyed as expected.</b></font> Actual results: " + _actualMessage);

            }
            else
                ExtentReportsHelper.LogWarning($"The message does not as expected. \nActual results: {_actualMessage}\nExpected results: {_expectedMessage} ");

            // Verify the modal is displayed with default value ()
            if (!SeriesPage.Instance.AddSeriesModal.IsDefaultValues)
                ExtentReportsHelper.LogWarning("The modal of Add Series is not displayed with default values.");

            // Close modal if it's open
            SeriesPage.Instance.AddSeriesModal.CloseModal();

            CommonHelper.RefreshPage();

            //Step 4: Verify the error message show when edit Series with blank value
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4: Verify the error message show when edit Series with blank value.</font>");
            SeriesPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, SeriesData.Code);
            if (SeriesPage.Instance.IsItemInGrid("Code", SeriesData.Code) is true)
            {
                //Go to detail Series
                SeriesPage.Instance.SelectItemAndOpenDetailPage("Name", SeriesData.Name);

                SeriesDetailPage.Instance.UpdateSeries(EmptyNameData);

                _actualErrorMessage = SeriesDetailPage.Instance.GetLastestToastMessage();
                // Verify successful save and appropriate success message.
                _expectedErrorMessage = $"Series name cannot be empty.";
                if (_expectedErrorMessage.Equals(_actualErrorMessage))
                {
                    ExtentReportsHelper.LogPass("<font color ='green'><b>The message is dispalyed as expected.</b></font> Actual results: " + _actualErrorMessage);
                }
                else
                    ExtentReportsHelper.LogWarning($"The message does not as expected. \nActual results: {_actualErrorMessage}\nExpected results: {_expectedErrorMessage} ");

                CommonHelper.RefreshPage();
            }


            //Step 5: Verify the error message show when edit Series with Name already existed in the system
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 5: Verify the error message show when edit Series with Name already existed in the system.</font>");
            SeriesDetailPage.Instance.UpdateSeries(oldSeriesData);
            _actualErrorMessage = SeriesDetailPage.Instance.GetLastestToastMessage();

            // Verify successful save and appropriate success message.
            _expectedErrorMessage = $"Failed to update {SeriesData.Name}. The Series Name must be unique.";
            if (_expectedErrorMessage.Equals(_actualErrorMessage))
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>The message is dispalyed as expected.</b></font> Actual results: " + _actualErrorMessage);
            }
            else
                ExtentReportsHelper.LogWarning($"The message does not as expected. \nActual results: {_actualErrorMessage}\nExpected results: {_expectedErrorMessage} ");

            CommonHelper.RefreshPage();

            //Step 6: Verify the toast message show when edit Series successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 6: Verify the toast message show when edit Series successfully.</font>");
            SeriesDetailPage.Instance.UpdateSeries(newSeriesData);
            // Verify the success msg
            string expectedMsg = $"Series {newSeriesData.Name} saved successfully!";
            if (expectedMsg.Equals(SeriesDetailPage.Instance.GetLastestToastMessage()))
            {
                ExtentReportsHelper.LogPass($"The Series '<font color='green'><b>{newSeriesData.Name}</b></font>' is updated successfully.");
                SeriesDetailPage.Instance.CloseToastMessage();
            }

            // Go back to Series default page
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);

            // Filter and find the updated series
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newSeriesData.Name);

            // Verify the updated series is displayed on grid
            if (!SeriesPage.Instance.IsItemInGrid("Name", newSeriesData.Name))
            {
                // The item does not exist in list
                ExtentReportsHelper.LogFail($"The Series with name <font color='green'><b>{newSeriesData.Name}</b></font> is not exist in list.");
            }
            ExtentReportsHelper.LogPass($"The Series with name '<font color='green'><b>{newSeriesData.Name}</b></font>' is updated to name '<font color='green'><b>{newSeriesData.Name}</b></font>' successfully.");

            //Step 7: Verify the error message show when edit Series with blank value
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 7: Verify the error message show when edit Series with blank value.</font>");
            SeriesPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, newSeriesData.Code);
            if (SeriesPage.Instance.IsItemInGrid("Code", newSeriesData.Code) is true)
            {
                SeriesPage.Instance.ClickEditItemInGrid("Code", newSeriesData.Code);
                SeriesPage.Instance.UpdateSeries(UpdateEmptySeriesData);
                _actualErrorMessage = SeriesPage.Instance.GetLastestToastMessage();
                // Verify successful save and appropriate success message.
                _expectedErrorMessage = $"Series name cannot be empty.";
                if (_expectedErrorMessage.Equals(_actualErrorMessage))
                {
                    ExtentReportsHelper.LogPass("<font color ='green'><b>The message is dispalyed as expected.</b></font> Actual results: " + _actualErrorMessage);
                }
                else
                    ExtentReportsHelper.LogWarning($"The message does not as expected. \nActual results: {_actualErrorMessage}\nExpected results: {_expectedErrorMessage} ");

                CommonHelper.RefreshPage();
            }



            //Step 8: Verify the error message show when edit Series with Name already existed in the system
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 8: Verify the error message show when edit Series with Name already existed in the system.</font>");
            SeriesPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, newSeriesData.Code);
            if (SeriesPage.Instance.IsItemInGrid("Code", newSeriesData.Code) is true)
            {
                SeriesPage.Instance.ClickEditItemInGrid("Code", newSeriesData.Code);
                SeriesPage.Instance.UpdateSeries(oldSeriesData);

                // Verify successful save and appropriate success message.
                _actualErrorMessage = SeriesPage.Instance.GetLastestToastMessage();
                _expectedErrorMessage = $"Failed to update {oldSeriesData.Name}. The Series Name must be unique.";
                if (_expectedErrorMessage.Equals(_actualErrorMessage))
                {
                    ExtentReportsHelper.LogPass("<font color ='green'><b>The message is dispalyed as expected.</b></font> Actual results: " + _actualErrorMessage);
                }
                else
                    ExtentReportsHelper.LogWarning($"The message does not as expected. \nActual results: {_actualErrorMessage}\nExpected results: {_expectedErrorMessage} ");
                
                CommonHelper.RefreshPage();
            }



            //Step 9: Verify the toast message show when edit Series successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 9: Verify the toast message show when edit Series successfully.</font>");
            SeriesPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, newSeriesData.Code);
            if (SeriesPage.Instance.IsItemInGrid("Code", newSeriesData.Code) is true)
            {
                SeriesPage.Instance.ClickEditItemInGrid("Code", newSeriesData.Code);
                SeriesPage.Instance.UpdateSeries(UpdateSeriesData);

                // Verify successful save and appropriate success message.
                _actualMessage = SeriesPage.Instance.GetLastestToastMessage();
                _expectedMessage = $"Series {UpdateSeriesData.Name} saved successfully!";
                if (_expectedMessage.Equals(_actualMessage))
                {
                    ExtentReportsHelper.LogPass("<font color ='green'><b>The message is dispalyed as expected.</b></font> Actual results: " + _actualMessage);
                }
                else
                    ExtentReportsHelper.LogWarning($"The message does not as expected. \nActual results: {_actualMessage}\nExpected results: {_expectedMessage} ");
                
                CommonHelper.RefreshPage();
            }

            //Step 10: Verify the delete function work correctly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 10: Verify the delete function work correctly.</font>");
            SeriesPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, UpdateSeriesData.Code);
            if (SeriesPage.Instance.IsItemInGrid("Code", UpdateSeriesData.Code) is true)
            {
                SeriesPage.Instance.DeleteItemInGrid("Code", UpdateSeriesData.Code);
                _actualMessage = SeriesPage.Instance.GetLastestToastMessage();
                _expectedMessage = $"Series {UpdateSeriesData.Name} deleted successfully!";
                if (_expectedMessage.Equals(_actualMessage))
                {
                    ExtentReportsHelper.LogPass("<font color ='green'><b>The message is dispalyed as expected.</b></font> Actual results: " + _actualMessage);
                }
                else
                    ExtentReportsHelper.LogWarning($"The message does not as expected. \nActual results: {_actualMessage}\nExpected results: {_expectedMessage} ");
                
                CommonHelper.RefreshPage();
            }

            //Step 11: Verify user cannot create the series when click on the “x” button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 11: Verify user cannot create the series when click on the “x” button.</font>");
            //Click on “+”
            SeriesPage.Instance.ClickAddToSeriesModal();
            
            // Close modal if it's open
            SeriesPage.Instance.AddSeriesModal.CloseModal();

            //Step 12: Verify user cannot update the series when click on the “x” button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 12: Verify user cannot update the series when click on the “x” button.</font>");
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, oldSeriesData.Name);
            if (SeriesPage.Instance.IsItemInGrid("Name", oldSeriesData.Name) is true)
            {
                SeriesPage.Instance.ClickEditItemInGrid("Name", oldSeriesData.Name);
                SeriesPage.Instance.CancelUpdateSeries();

                // Verify the series is displayed on grid 
                if (!SeriesPage.Instance.IsItemInGrid("Name", oldSeriesData.Name))
                {
                    // The item does not exist in list
                    ExtentReportsHelper.LogFail($"The Series with name <font color='green'><b>{oldSeriesData.Name}</b></font> is not exist in list.");
                }
                ExtentReportsHelper.LogPass($"The Series with name '<font color='green'><b>{oldSeriesData.Name} is displayed in grid</b></font>");

            }

        }
    }

}
