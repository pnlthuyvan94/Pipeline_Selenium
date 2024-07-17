using LBM.Testing.Based;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace LBM.Testing.Base
{
    public class TestStepManager
    {
        private string testCaseName = string.Empty;
        private List<TestStep> testSteps = new List<TestStep>();
        private int currentStep = 0;

        public TestStepManager(string testCaseName, List<TestStep> testSteps)
        {
            this.testCaseName = testCaseName;
            this.testSteps = testSteps;

            for (int stepIndex = 1; stepIndex <= testSteps.Count; stepIndex++)
            {
                testSteps[stepIndex - 1].step = stepIndex;
            }
        }

        public TestStep GetCurrentTestStep()
        {
            TestStep testStep = null;

            try
            {
                testStep = testSteps.Find(x => (x.step == currentStep));
            }
            catch (ArgumentNullException argEx)
            {
                Console.WriteLine(argEx.StackTrace);
            }

            return testStep;
        }

        public TestStep GetTestStep(int stepIndex)
        {
            TestStep testStep = null;

            try
            {
                testStep = testSteps.Find(x => (x.step == stepIndex));
            }
            catch (ArgumentNullException argEx)
            {
                Console.WriteLine(argEx.StackTrace);
            }

            return testStep;
        }

        public TestStep GetNextStep(string testMethodName)
        {
            if (currentStep == testSteps.Count)
                currentStep = 1;
            else
                currentStep++;


            TestStep testStep = GetCurrentTestStep();

            if (testStep.name == testMethodName)
                return testStep;
            else
                throw new ArgumentException("The provided function name does not match the next expected TestStep.");
        }

        public List<TestStep> GetTestSteps()
        {
            return testSteps;
        }

        public void SetCurrentTestStep(int stepIndex)
        {
            currentStep = stepIndex;
        }

        //public void RunAllTestSteps(TestListener testInstance)
        //{
        //    foreach (TestStep step in testSteps)
        //    {
        //        MethodInfo testMethod = testInstance.GetType().GetMethod(step.name);
        //        testMethod.Invoke(testInstance, null);
        //    }
        //}

        public bool TestExists(int stepIndex, string name = "", string titlePartial = "")
        {
            bool testExists = false;

            try
            {
                testExists = testSteps.Exists(_testStep => _testStep.step == stepIndex);

                if (!testExists && titlePartial != string.Empty)
                    testExists = testSteps.Exists(_testStep => _testStep.title.ToLower().Contains(titlePartial.ToLower()));
                if (!testExists && name != string.Empty)
                    testExists = testSteps.Exists(_testStep => _testStep.name == name);
            }
            catch (ArgumentNullException argEx)
            {
                Console.WriteLine(argEx.StackTrace);
            }

            return testExists;
        }

        public void AddTest(TestStep testStep = null)
        {
            bool testExists = false;

            if (!(testStep is null))
                testExists = TestExists(testStep.step);

            if (testExists)
            {
                Console.WriteLine($"Unable to add TestStep using Step index '{testStep.step}', a TestStep with this index already exists.");
                Console.WriteLine("Changing Step index and adding TestStep to end of the list of Tests.");

                testStep.step = (testSteps.Count + 1);
                testSteps.Add(testStep);
            }
            else
            {
                testSteps.Add(testStep);
            }
        }

        public void UpdateTest(TestStep testStep)
        {
            bool testExists = TestExists(testStep.step);

            if (testExists)
            {
                testSteps.RemoveAll(_testStep => _testStep.step == testStep.step);
                testSteps.Add(testStep);
            }
        }

        public void RemoveTest(TestStep testStep)
        {
            bool testExists = TestExists(testStep.step);

            if (testExists)
                testSteps.Remove(testStep);
        }

        public void RemoveTestByStep(int stepIndex)
        {
            bool testExists = TestExists(stepIndex);

            if (testExists)
                testSteps.Remove(GetTestStep(stepIndex));
        }

        public void RemoveAllTests()
        {
            testSteps.Clear();
        }

        public void RemoveMatchingTests(string name, string titlePartial = "")
        {
            try
            {
                testSteps.RemoveAll(_testStep => _testStep.name == name);

                if (titlePartial != string.Empty)
                    testSteps.RemoveAll(_testStep => _testStep.title.ToLower().Contains(titlePartial.ToLower()));

            }
            catch (ArgumentNullException argEx)
            {
                Console.WriteLine(argEx.StackTrace);
            }
        }
    }
}
