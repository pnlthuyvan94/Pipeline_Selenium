using NUnit.Framework;
using Pipeline.Common;

namespace Pipeline.Testing {
    [SetUpFixture]
    public class SetUp : InitialSetting
    {
        [OneTimeSetUp]
        public static void OnStart()
        {
            RunBeforeAnyTests();
        }

        [OneTimeTearDown]
        public static void OnFinish()
        {
            RunAfterAnyTests();
        }
    }
}
