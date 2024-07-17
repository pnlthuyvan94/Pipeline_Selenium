using AventStack.ExtentReports;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Pipeline.Common.Utils
{
    public class ExtentReportsHelper
    {
        private readonly static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [ThreadStatic]
        private static ExtentTest _parentTest;

        [ThreadStatic]
        private static List<ExtentTest> _listOfParentsTest;

        [ThreadStatic]
        private static ExtentTest _childTest;

        public static string last_executed_image_path = null;

        public static bool IsParentTestNull
        {
            get
            {
                log.Info("Verified parent test is null or not.");
                return _parentTest == null;
            }
        }

        /// <summary>
        /// Switching test set by Name
        /// </summary>
        /// <param name="testSetName"></param>
        public static void SwitchTestSet(string testSetName)
        {
            bool isFound = false;
            log.Info($"Switching test set with testSetName: {testSetName}.");
            if (_listOfParentsTest is null)
            {
                log.Info($"Create the list of parent test to handle.");
                _listOfParentsTest = new List<ExtentTest>();
            }
            foreach (var item in _listOfParentsTest)
            {
                if (item.Model.Name == testSetName)
                {
                    log.Info($"Set the parent test with name {testSetName}.");
                    _parentTest = item;
                    isFound = true;
                    break;
                }
            }
            if (!isFound)
            {
                log.Info($"Could not find the test set with name {testSetName} in list. So we will create the new parent test.");
                CreateParentTest(testSetName);
            }
        }

        /// <summary>
        /// Create the Parent Test
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateParentTest(string testName, string description = "")
        {
            if (_listOfParentsTest == null)
            {
                log.Info($"Create the list of parent test to handle.");
                _listOfParentsTest = new List<ExtentTest>();
            }
            log.Info($"Create Parent Test with name {testName} and description {description}.");
            _parentTest = BaseValues.ExtentReports.CreateTest(testName, description);
            log.Info($"Add the Parents Test with name {testName} and description {description} to list.");
            _listOfParentsTest.Add(_parentTest);
            return _parentTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(string testName, string description = "")
        {
            log.Info($"Create Test with name {testName} and description {description}.");
            _childTest = _parentTest.CreateNode(testName, description);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogFail(string pathImg = null, string details = "Details: ")
        {
            bool isAppTimedOut = CommonFuncs.IsApplicationTimedOut();
            if (isAppTimedOut)
            {
                return LogWarning(pathImg, $"Application timed out - Ignored error - {details}");
            } else {
                log.Info($"Log the test failed. Detail: {details}");
                if (_childTest == null)
                    return null;

                if (pathImg != null)
                    _childTest = _childTest.Fail(details, MediaEntityBuilder.CreateScreenCaptureFromPath(pathImg).Build());
                else
                    _childTest = _childTest.Fail(details);
                return _childTest;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogWarning(string pathImg = null, string details = "Details: ")
        {
            log.Info($"Log the test warning. Detail: {details}");
            if (_childTest == null)
                return null;

            if (pathImg != null)
                _childTest = _childTest.Warning(details, MediaEntityBuilder.CreateScreenCaptureFromPath(pathImg).Build());
            else
                _childTest = _childTest.Warning(details);

            bool isAppTimedOut = CommonFuncs.IsApplicationTimedOut();
            if (isAppTimedOut)
            {
                Assert.Inconclusive($"Application timed out - Marking test inconclusive.");
                return _childTest;
            }
            else
            {
                return _childTest;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogInformation(string pathImg = null, string details = "Details: ")
        {
            log.Info($"Log the test information. Detail: {details}");
            if (_childTest == null)
                return null;

            if (pathImg is null)
                _childTest = _childTest.Info(details);
            else
                _childTest = _childTest.Info(details, MediaEntityBuilder.CreateScreenCaptureFromPath(pathImg).Build());
            System.Threading.Thread.Sleep(300);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogPass(string pathImg = null, string details = "Details: ")
        {
            log.Info($"Log the test passed. Detail: {details}");
            if (_childTest == null)
                return null;

            if (pathImg is null)
                _childTest = _childTest.Pass(details);
            else
                _childTest = _childTest.Pass(details, MediaEntityBuilder.CreateScreenCaptureFromPath(pathImg).Build());
            System.Threading.Thread.Sleep(300);
            return _childTest;
        }

        /// <summary>
        /// Log fail and capture whole screen.
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogFail(string details)
        {
            bool isAppTimedOut = CommonFuncs.IsApplicationTimedOut();
            if (isAppTimedOut)
            {
                return LogWarning($"Application timed out - Ignored error - {details}");
            }
            else
            {
                log.Info($"Log the test failed and capture whole screen. Detail: {details}");
                if (_childTest == null)
                    return null;

                _childTest = _childTest.Fail(details, MediaEntityBuilder.CreateScreenCaptureFromPath(CommonHelper.CaptureScreen()).Build());
                return _childTest;
            }
        }

        /// <summary>
        /// Log warning and capture whole screen
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogWarning(string details)
        {
            log.Info($"Log the test warning and capture whole screen. Detail: {details}");
            if (_childTest == null)
                return null;

            _childTest = _childTest.Warning(details, MediaEntityBuilder.CreateScreenCaptureFromPath(CommonHelper.CaptureScreen()).Build());

            bool isAppTimedOut = CommonFuncs.IsApplicationTimedOut();
            if (isAppTimedOut)
            {
                Assert.Inconclusive($"Application timed out - Marking test inconclusive.");
                return _childTest;
            }
            else
            {
                return _childTest;
            }
        }

        /// <summary>
        /// Log information and capture whole screen
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogInformation(string details)
        {
            log.Info($"Log the test information and capture whole screen. Detail: {details}");
            if (_childTest == null)
                return null;

            _childTest = _childTest.Info(details, MediaEntityBuilder.CreateScreenCaptureFromPath(CommonHelper.CaptureScreen()).Build());
            return _childTest;
        }

        /// <summary>
        /// Log pass and capture whole screen
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogPass(string details)
        {
            log.Info($"Log the test passed and capture whole screen. Detail: {details}");
            if (_childTest == null)
                return null;

            _childTest = _childTest.Pass(details, MediaEntityBuilder.CreateScreenCaptureFromPath(CommonHelper.CaptureScreen()).Build());
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            log.Info($"Get child test.");
            return _childTest;
        }

        public static bool IsChildTestNull
        {
            get
            {
                if (_childTest is null)
                {
                    log.Info($"Verify child test is null or not. Child test is null");
                    return true;
                }
                else
                {
                    log.Info($"Verify child test is null or not. Child test is NOT null");
                    return false;
                }
            }
        }
    }
}
