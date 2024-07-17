using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Series.SeriesDetail
{
    public partial class SeriesDetailPage
    {
        /// <summary>
        /// Verify the House in list or not
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="houseName"></param>
        /// <returns></returns>
        public bool VerifyHouseInList(string columnName, string houseName)
        {
            return House_Grid.IsItemOnCurrentPage(columnName, houseName);
        }

        /// <summary>
        /// Verify the Series updated successfully
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool VerifyTheSeriesUpdateSuccessfully(SeriesData data)
        {
            bool isPassed = true;
            if (!Title_Txt.GetValue().Equals(data.Name))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail($"The Title does not save correctly. Expected: <font color='green'><b>{data.Name}</b></font>.<br>Actual: <font color='green'><b>{Title_Txt.GetValue()}</b></font>.<br>");
            }
            if (!Code_Txt.GetValue().Equals(data.Code))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail($"The Code does not save correctly. Expected: <font color='green'><b>{data.Code}</b></font>.<br>Actual: <font color='green'><b>{Code_Txt.GetValue()}</b></font>.<br>");
            }
            if (!Description_Txt.GetValue().Equals(data.Description))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail($"The Description does not save correctly. Expected: <font color='green'><b>{data.Description}</b></font>.<br>Actual: <font color='green'><b>{Description_Txt.GetValue()}</b></font>.<br>");
            }
            return isPassed;
        }

        public bool VerifySeriesDetailPageDisplay(string SeriesName)
        {
            string title = SubHeadTitle();
            // Get the subhead title and verify with the SeriesName
            if (title.Equals(SeriesName) && !CurrentURL.EndsWith("cid=0"))
                return true;
            return false;
        }
    }
}
