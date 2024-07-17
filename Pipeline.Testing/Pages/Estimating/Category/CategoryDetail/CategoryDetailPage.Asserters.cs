
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Category.CategoryDetail
{
    public partial class CategoryDetailPage
    {

        public bool IsDisplayDataCorrectly(CategoryData data)
        {
            if (!SubHeadTitle().Equals(data.Name))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Sub Head title: {data.Name}. Actual Sub Head title: {SubHeadTitle()}");
                return false;
            }
            if (data.Name != CategoryName_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Name: {data.Name}. Actual result: {CategoryName_txt.GetValue()}");
                return false;
            }
            if (data.Parent != Parent_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Parent: {data.Parent}. Actual result: {Parent_ddl.GetText()}");
                return false;
            }
            return true;
        }
    }
}
