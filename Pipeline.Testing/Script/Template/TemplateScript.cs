using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Testing.Based;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Script.Template
{
    // Inherit the BaseTestScript
    //[TestFixture]
    //[Ignore("Ignore a test because this is a Template")]
    public class Template : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_I);
        }

        readonly string pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\FileNameExcel.xlsx";

        // Define the page which you test

        // Set up and initial this page
        [SetUp]
        public void SetupTest()
        {
        }


        #region"Test case"
        // The testcase run only 1 time
        //[Test]
        //[Ignore("Ignore a test because this is a Template")]
        //[Category("Template"), Order(1)]
        public void Test1()
        {

        }

        //[TestCase(1)]
        //[TestCase(2)]
        //[Ignore("Ignore a test because this is a Template")]
        //[Category("Template"), Order(1)]
        public void Test2(int sequence)
        {
            // This testcase will rune 2 time and 
            // 1st the "sequence" = 1
            // 2nd the "sequence" = 2
        }
        #endregion

        // After rune all test
        [TearDown]
        public void TearDownTest()
        {

        }
    }
}
