using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Pipeline.Common.Utils
{
    public static class CommonHelper
    {
        static SqlConnection con;
        static SqlDataReader dr;
        static SqlCommand sqlCmd;

        private static log4net.ILog Log;
        private static string style;

        private static ref IWebDriver _driver => ref BaseValues.DriverSession;

        public static string get_cased_name(string original_name) => (original_name.Substring(0, 1) + original_name.Substring(1).ToLower());

        internal static void InitLog()
        {
            if (Log == null)
                Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// How to call: MeasureTime(() => MethodName(param1, param2,...))
        /// </summary>
        /// <param name="action"></param>
        public static double MeasureTime(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            var result = stopwatch.Elapsed.TotalSeconds;
            return Math.Round(result, 2);
        }

        public static double MeasureLoadTime(Action action)
        {
            action();
            var loadTime = _driver.ExecuteJavaScript<object>("return window.performance.timing.domComplete - window.performance.timing.navigationStart;");
            return Convert.ToDouble(loadTime) / 1000;
        }

        public static void FitViewportToContentByJavascript() => BaseValues.DriverSession.ExecuteJavaScript($"document.querySelector('html').setAttribute('style', 'min-width: {BaseValues.BrowserWidth}px !important; overflow-x: auto;');");

        /// <summary>
        /// Query data from sql server
        /// </summary>
        /// <param name="command"></param>
        public static IEnumerable<T> ExecuteReader<T>(string query, int columnIndex)
        {
            try
            {
                return DBConnect<T>(query, columnIndex);
            }

            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
                return Enumerable.Empty<T>();
            }
        }

        /// <summary>
        /// Query data from sql server
        /// </summary>
        /// <param name="command"></param>
        public static IEnumerable<T> ExecuteReader<T>(string query, string columnIndex)
        {
            try
            {
                return DBConnect<T>(query, columnIndex);
            }

            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
                return Enumerable.Empty<T>();
            }

        }

        private static IEnumerable<T> DBConnect<T>(string cmd, int columnIndex)
        {
            ConnectionStringSettings connection = System.Configuration.ConfigurationManager.ConnectionStrings["MainConnectionString"];
            string connectionString = connection.ConnectionString;

            con = new SqlConnection(connectionString);
            con.Open();
            sqlCmd = new SqlCommand(cmd, con);

            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                yield return (T)dr[columnIndex];
            }
            con.Close();
        }

        private static IEnumerable<T> DBConnect<T>(string cmd, string columnIndex)
        {
            ConnectionStringSettings connection = System.Configuration.ConfigurationManager.ConnectionStrings["MainConnectionString"];
            string connectionString = connection.ConnectionString;

            con = new SqlConnection(connectionString);
            con.Open();
            sqlCmd = new SqlCommand(cmd, con);

            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                yield return (T)dr[columnIndex];
            }
            con.Close();
        }
        /// <summary>
        /// Create list
        /// </summary>
        /// <typeparam name="Cast list elements to list controls which define on this framework"></typeparam>
        /// <param name="List Elements"></param>
        /// <returns></returns>
        public static IList<T> CreateList<T>(IList<IWebElement> elements) where T : BaseControl
        {
            var result = new List<T>();
            result.AddRange(elements.Select(e => (T)Activator.CreateInstance(typeof(T), e)));
            return result;
        }

        /// <summary>
        /// Compare 2 list string without sequence
        /// </summary>
        /// <param name="list string 1"></param>
        /// <param name="list string 2"></param>
        /// <returns></returns>
        public static bool IsEqual2List(IList<string> list1, IList<string> list2)
        {
            bool isEqual = list1.Count == list2.Count && !list1.Except(list2).Any();
            return isEqual;
        }

        public static bool IsEqual2ListSequence(IList<string> list1, IList<string> list2)
        {
            if (list1.Count == list2.Count)
                return list1.SequenceEqual(list2);
            return false;
        }

        /// <summary>
        /// Cast list controls to list string
        /// </summary>
        /// <param name="listControls"></param>
        /// <returns>List text of elements</returns>
        public static IList<string> CastListControlsToListString(IList<IWebElement> listItem)
        {
            IList<string> list = new List<string>();
            foreach (var item in listItem)
                list.Add(item.Text);
            return list;
        }

        /// <summary>
        /// Cast list controls to list string by attribute (if the attribute =nu)
        /// </summary>
        /// <param name="listControls"></param>
        /// <returns>List text of elements</returns>
        public static IList<string> CastListControlsToListString(IList<IWebElement> listItem, string attribute = null)
        {
            if (string.IsNullOrEmpty(attribute))
                return CastListControlsToListString(listItem);

            IList<string> list = new List<string>();
            foreach (var item in listItem)
                list.Add(item.GetAttribute(attribute).Replace("\r\n", "\n"));
            return list;
        }

        /// <summary>
        /// Click Element by position
        /// </summary>
        /// <param name="element"></param>
        public static void CoordinateClick(IWebElement element)
        {
            Actions action = new Actions(BaseValues.DriverSession);
            action.MoveToElement(element).Click().Build().Perform();
        }

        /// <summary>
        /// Click on the element by javascript
        /// </summary>
        /// <param name="control"></param>
        public static void JavaScriptClick(IWebElement control)
        {
            string text_val = "<NULL_ELEMENT>";
            try
            {
                _driver.ExecuteJavaScript("arguments[0].click();", control);

                if (control != null)
                    text_val = (control.Text == "") ? control.GetAttribute("value") : control.Text;

                ExtentReportsHelper.LogInformation(CaptureScreen(), $"Element <font color ='green'><b>{text_val}</b></font> was clicked by javascript");
            }
            catch (Exception ex)
            {
                if (control != null)
                    text_val = (control.Text == "") ? control.GetAttribute("value") : control.Text;

                ExtentReportsHelper.LogWarning(CaptureScreen(), $"Element <font color ='red'><b>{text_val}</b></font> could not be clicked after capturing the screenshot");
                throw new InvalidElementStateException($"Element could not be clicked by Javascript {text_val}, it may not exist on the DOM for the provided identifier");
            }
        }

        /// <summary>
        /// Cast list string to string
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public static string CastListToString(IList<string> itemList)
        {
            return String.Join(";", itemList);
        }

        /// <summary>
        /// Cast string value to IList<string> separate with ";"
        /// </summary>
        public static IList<string> CastValueToList(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            var paths = value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            return paths.ToList();
        }

        public static string[] CastValueToArray(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            var paths = value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            return paths;
        }

        /// <summary>
        /// Emulate a device with the provided screen size.
        /// </summary>
        public static void EmulateDevice(int screen_width = 0, int screen_height = 0)
        {
            if (BaseValues.DriverSession == null)
                return;

            if (screen_width == 0 || screen_height == 0)
            {
                screen_width = BaseValues.BrowserWidth;
                screen_height = BaseValues.BrowserHeight;
            }

            ChromeDriver driver_executor = (ChromeDriver)BaseValues.DriverSession;

            //Get the current page's content size from the page layout metrics so we can build a Viewport object of the cssContentSize data
            Dictionary<string, object> layout_metrics = (Dictionary<string, object>)driver_executor.ExecuteCdpCommand("Page.getLayoutMetrics", new Dictionary<string, object>());
            Dictionary<string, object> css_content_size = (Dictionary<string, object>)layout_metrics["cssContentSize"];
            double css_width = Convert.ToDouble(css_content_size["width"]);
            double css_height = Convert.ToDouble(css_content_size["height"]);

            //Width & height must be based on cssContentSize in order to capture the page contents properly
            Dictionary<string, object> device_metrics = new Dictionary<string, object> {
                { "width", css_width },
                { "height", css_height },
                { "deviceScaleFactor", 0 },
                { "mobile", true },
                { "screenWidth", screen_width },
                { "screenHeight", screen_height }
            };

            //Clear device metrics before we set it with our new metrics, just incase
            driver_executor.ExecuteCdpCommand("Emulation.clearDeviceMetricsOverride", new Dictionary<string, object>());

            //Set new metrics
            driver_executor.ExecuteCdpCommand("Emulation.setDeviceMetricsOverride", device_metrics);
        }

        public static void CreateScreenshotFolder()
        {
            bool exists = Directory.Exists($"{BaseValues.PathReportFile}ScreenShots");
            if (!exists)
                Directory.CreateDirectory($"{BaseValues.PathReportFile}ScreenShots");
        }

        /// <summary>
        /// Only use if necessary, or to debug element position and dimension values being returned by Selenium. 
        /// If possible, use Selenium's Location and Size properties on the WebElement instead.
        /// </summary>
        /// <param name="web_element"></param>
        /// <returns></returns>
        public static Rectangle GetElementRectDirect(IWebElement web_element)
        {
            if (web_element == null)
                throw new ArgumentNullException("web_element");

            //Get the element's visible area by using getBoundingClientRect and parsing getComputedStyle
            var result = BaseValues.DriverSession.ExecuteJavaScript<IReadOnlyCollection<object>>(
                "var element = arguments[0];" +
                "var element_bounds = element.getBoundingClientRect();" +
                "return [(element_bounds.left + window.scrollX), (element_bounds.top + window.scrollY), element_bounds.width, element_bounds.height];",
            web_element);

            int[] pts = Array.ConvertAll(result.ToArray(), Convert.ToInt32);
            return new Rectangle(pts[0], pts[1], pts[2], pts[3]);
        }

        /// <summary>
        /// Sets the web application's header navigation menu to an absolute position to prevent element overlap. 
        /// Useful if you need to take cropped screenshots or if you need to interact with an element.
        /// </summary>
        public static void pin_navigation_headers()
        {
            try
            {
                _driver.ExecuteJavaScript("document.querySelector('header[role=\"banner\"]').style.position = 'absolute';");
                _driver.ExecuteJavaScript("document.querySelector('section.subhead').style.position = 'absolute';");
            }
            catch (Exception ex)
            {
                //string msg = $"Exception in navigation header pinning, one of the header elements does not exist or does not have a style attribute - Stack: {ex.StackTrace}";
                //Log.Info(msg);
                //Console.WriteLine(msg);
            }
        }

        /// <summary>
        /// Resets the web application's header navigation menu back to the original fixed position.
        /// </summary>
        public static void reset_navigation_headers()
        {
            try
            {
                _driver.ExecuteJavaScript("document.querySelector('header[role=\"banner\"]').style.position = 'fixed';");
                _driver.ExecuteJavaScript("document.querySelector('section.subhead').style.position = 'fixed';");
            }
            catch (Exception ex)
            {
                //string msg = $"Exception in reset of navigation header, one element does not exist or does not have a style attribute - Stack: {ex.StackTrace}";
                //Log.Info(msg);
                //Console.WriteLine(msg);
            }
        }

        public static string get_viewport_meta_content() => _driver.ExecuteJavaScript<string>("return document.querySelector('html > head > [name*=\"viewport\"]').content;");

        public static void set_viewport_meta_content(string content) => _driver.ExecuteJavaScript($"document.querySelector('html > head > [name*=\"viewport\"]').content = '{content}';");

        /// <summary>
        /// DEPRECATED - Use #force_display_block instead
        /// </summary>
        /// <param name="css_selector"></param>
        public static void force_hover_menu(string css_selector)
        {
            string script =
                $"var nav_ele = document.querySelector('{css_selector}').parentElement; " + //nav[role=\"navigation\"] ul li [title=\"Purchasing\"]
                "nav_ele.querySelector('.sub-nav').style.display = 'block'; ";

            _driver.ExecuteJavaScript(script);
        }

        public static void force_hover_submenu(string css_parent_menu_selector, string submenu_text)
        {
            string script =
                 $"var nav_ele = document.querySelector('{css_parent_menu_selector}').parentElement; " + //nav[role=\"navigation\"] ul li [title=\"Purchasing\"]
                 "return nav_ele; ";

            IWebElement nav_ele = _driver.ExecuteJavaScript<IWebElement>(script);
            if (nav_ele == null)
                return;

            IWebElement submenu = CommonHelper.find_element_weak(nav_ele, By.XPath($"./ul/li/a[contains(text(), {submenu_text})]/../ul"));
            if (submenu == null)
                return;

            CommonHelper.force_visible(submenu, true);
        }

        public static void force_display_block(IWebElement element, bool is_menu_or_submenu)
        {
            try
            {
                if (is_menu_or_submenu)
                    _driver.ExecuteJavaScript("arguments[0].parentElement.parentElement.style.display = 'block';", element);
                else
                    _driver.ExecuteJavaScript("arguments[0].style.display = 'block';", element);
            }
            catch (Exception ex)
            {
                string msg = $"Skipping setting of 'block' to element's display style, error encountered while executing javascript - Stack: {ex.StackTrace}";
                Log.Info(msg);
                Console.WriteLine(msg);
            }
        }

        public static void force_visible(IWebElement element, bool is_menu_or_submenu)
        {
            try
            {
                if (is_menu_or_submenu)
                    _driver.ExecuteJavaScript("arguments[0].parentElement.parentElement.style.visibility = 'visible';", element);
                else
                    _driver.ExecuteJavaScript("arguments[0].style.visibility = 'visible';", element);
            }
            catch (Exception ex)
            {
                string msg = $"Skipping setting of 'visible' to element's visibility style, error encountered while executing javascript - Stack: {ex.StackTrace}";
                Log.Info(msg);
                Console.WriteLine(msg);
            }
        }

        /// <summary>
        /// Use DevTools protocol available in Selenium 4+ to capture a dynamic screenshot directly from the canvas, of a single element or the entire page content. 
		/// Thanks to the use of devtools protocol for direct capturing of the canvas surface, the host's screen size or window size will not constrain the resulting image. 
        /// If an element is provided then the screenshot will only capture the element and margins, the optional margins define how many extra pixels to capture around the element. 
        /// Otherwise, if no web element is provided then the screenshot will capture the entire page's content size based on the CSS values returned from device metrics command.
        /// </summary>
        /// <param name="element_control"></param>
		/// <param name="margin_x"></param>
		/// <param name="margin_y"></param>
        /// <returns>Screenshot</returns>
        private static Screenshot capture_screen_internal(IWebElement element_control = null, int margin_x = 16, int margin_y = 16)
        {
            Screenshot screenshot_data = null;
            Dictionary<string, object> screenshot_viewport;
            Dictionary<string, object> screenshot_params;

            ChromeDriver driver_executor = (ChromeDriver)_driver;
            if (driver_executor == null)
                return screenshot_data;

            try
            {
                //Set application's nav header to absolute to prevent it from overlapping any elements
                CommonHelper.pin_navigation_headers();

                if (element_control != null)
                {
                    //Ensure that the element reference or state hasn't changed (can probably remove this step)
                    if (!CommonHelper.is_valid(element_control))
                        throw new InvalidElementStateException("Element is no longer enabled or the element state has changed");

                    //Prepare element for a screenshot by setting the capture target and highlighting it
                    IWebElement capture_target = element_control;
                    CommonHelper.HighLightElement(capture_target);

                    //Use the second parent element as the capture target if the provided element is a child of a dropdown list
                    IWebElement parent_element = CommonHelper.find_element_weak(element_control, By.XPath("./../../../ul[contains(@class, 'dropdown')]"));
                    if (parent_element != null)
                    {
                        CommonHelper.MoveToCenterOfElement(parent_element, true);

                        //Ensure the parent element is actually valid before making it the capture target
                        if (CommonHelper.is_valid(parent_element))
                            capture_target = parent_element;
                    }

                    //Build the screenshot clipping area and add it to the params
                    screenshot_viewport = new Dictionary<string, object> {
                        { "x", (capture_target.Location.X - margin_x) },
                        { "y", (capture_target.Location.Y - margin_y) },
                        { "width", Math.Min((capture_target.Size.Width + (margin_x * 2)), 3200) },
                        { "height", Math.Min((capture_target.Size.Height + (margin_y * 2)), 3200) },
                        { "scale", 1 } //Default scale factor
                    };
                }
                else
                {
                    //Get layout metrics for use in building accurate screenshot dimensions of full page content
                    Dictionary<string, object> layout_metrics = (Dictionary<string, object>)driver_executor.ExecuteCdpCommand("Page.getLayoutMetrics", new Dictionary<string, object>());
                    Dictionary<string, object> css_content_size = (Dictionary<string, object>)layout_metrics["cssContentSize"];
                    double css_width = Convert.ToDouble(css_content_size["width"]);
                    double css_height = Convert.ToDouble(css_content_size["height"]);

                    //Build the fullpage screenshot viewport area and add it to the params
                    screenshot_viewport = new Dictionary<string, object> {
                        { "x", 0 },
                        { "y", 0 },
                        { "width", Math.Min(css_width, 6400d) },
                        { "height", Math.Min(css_height, 6400d) },
                        { "scale", 1 } //Default scale factor
                    }; //Some scrollable page content can be extremely large, so make sure to clamp screenshot dimensions to a valid size (in our case, we set 6400x6400 pixels as the largest accepted capture size)
                }

                //Finally set the screenshot clipping to the viewport we created
                screenshot_params = new Dictionary<string, object> {
                    { "captureBeyondViewport", true },
                    { "fromSurface", true },
                    { "clip", screenshot_viewport }
                };

                //Execute the CDP command with our custom settings using the provided Page.captureScreenshot method and get the response data
                Dictionary<string, object> capture_response = (Dictionary<string, object>)driver_executor.ExecuteCdpCommand("Page.captureScreenshot", screenshot_params);
                screenshot_data = new Screenshot(capture_response["data"].ToString());

                //Reset the navigation header back to normal
                CommonHelper.reset_navigation_headers();
            }
            catch (Exception unk_exception)
            {
                string msg = $"Encountered exception while capturing a screenshot; Element argument is null or is full page screenshot: {(element_control == null)} - Exception: {unk_exception.StackTrace}";
                Console.WriteLine(msg);
                Log.Info(msg);
            }
            finally
            {
                if (element_control != null) CommonHelper.RemoveHighLightElement(element_control);
            }

            return screenshot_data;
        }

        /// <summary>
        /// Capture screen and highlight the control, if does not have the control, the system will capture current screen
        /// </summary>
        /// <param name="iControl"></param>
        /// <returns></returns>
        public static string CaptureScreen(IWebElement iControl = null)
        {
            ExtentReportsHelper.last_executed_image_path = $"./ScreenShots/{DateTime.Now.ToString("hh-mm-ssff")}.png";

            Screenshot capture_result = CommonHelper.capture_screen_internal(iControl);

            //Save the Screenshot to the filesystem
            if (capture_result != null)
            {
                try
                {
                    CommonHelper.CreateScreenshotFolder();
                    capture_result.SaveAsFile(BaseValues.PathReportFile + ExtentReportsHelper.last_executed_image_path, ScreenshotImageFormat.Png);

                    if (BaseValues.SaveImageAsBase64)
                        return capture_result.AsBase64EncodedString;
                    else
                        return ExtentReportsHelper.last_executed_image_path;
                }
                catch (Exception io_ex)
                {
                    string msg = $"Encountered exception while saving Screenshot, it may be too large or we are lacking permissions to the file/folder. {io_ex.StackTrace}";
                    Console.WriteLine(msg);
                    Log.Info(msg);
                }
            }
            return null;
        }

        public static string CaptureScreen(BaseControl iControl) => CommonHelper.CaptureScreen(iControl.GetWrappedControl());

        internal static void HighLightElement(IWebElement iControl = null)
        {
            try
            {
                if (iControl == null)
                    throw new InvalidElementStateException("Element reference was null or it's state unexpectedly changed");

                //CommonHelper.MoveToCenterOfElement(iControl, true);
                style = iControl.GetAttribute("style");
                Log.Debug($"Trying to view and highlight the element");
                IJavaScriptExecutor js = BaseValues.DriverSession as IJavaScriptExecutor;
                js.ExecuteScript($"arguments[0].style='border: 2px solid red;background: yellow; {style}'", iControl);
            }
            catch (Exception e)
            {
                Log.Info($"Could not apply highlight style to element, skipping");// " + e.StackTrace);
                Console.WriteLine($"Could not apply highlight style to element");// - Exception: " + e.StackTrace);
            }
        }

        public static void HighLightElement(BaseControl iControl = null) => HighLightElement(iControl.GetWrappedControl());


        internal static void RemoveHighLightElement(IWebElement control = null)
        {
            //if (style == string.Empty)
            //    return;

            try
            {
                if (control == null)
                    throw new InvalidElementStateException("Element reference was null or it's state unexpectedly changed");

                Log.Debug($"Trying to remove highlight the element");
                IJavaScriptExecutor js = BaseValues.DriverSession as IJavaScriptExecutor;
                js.ExecuteScript($"arguments[0].style='{style}'", control);
            }
            catch (Exception e)
            {
                Log.Info($"Could not remove highlight style from element, resetting style string");// - Exception: " + e.StackTrace);
                Console.WriteLine($"Could not remove highlight style from element, resetting style string");// + e.StackTrace);
            }
            finally
            {
                style = string.Empty;
            }
        }

        public static void RemoveHighLightElement(BaseControl iControl = null) => RemoveHighLightElement(iControl.GetWrappedControl());

        /// <summary>
        /// Switch to the other Tab via index, start with 0
        /// </summary>
        /// <param name="index"></param>
        public static void SwitchTab(int index)
        {
            BaseValues.DriverSession.SwitchTo().Window(BaseValues.DriverSession.WindowHandles[index]);
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
        }

        public static void FocusContent()
        {
            BaseValues.DriverSession.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Switch to last Tab
        /// </summary>
        /// <param name="index"></param>
        public static void SwitchLastestTab()
        {
            BaseValues.DriverSession.SwitchTo().Window(BaseValues.DriverSession.WindowHandles.Last());
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
        }

        public static void RefreshPage()
        {
            BaseValues.DriverSession.Navigate().Refresh();
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void OpenURL(string url)
        {
            BaseValues.DriverSession.Url = url;
            BasePage.JQueryLoad();
            CommonHelper.FitViewportToContentByJavascript();
            System.Threading.Thread.Sleep(667);
        }

        /// <summary>
        /// Close current Tab
        /// </summary>
        public static void CloseCurrentTab()
        {
            //BaseValues.DriverSession.Close();
            //JavaScriptExecutor("window.close()");
            BaseValues.DriverSession.Close();
        }

        /// <summary>
        /// Close all tab exclude the existing one
        /// </summary>
        public static void CloseAllTabsExcludeCurrentOne()
        {
            // Get the current tab
            string originalHandle = BaseValues.DriverSession.CurrentWindowHandle;

            foreach (string handle in BaseValues.DriverSession.WindowHandles)
            {
                // Delete the remaining tab
                if (!handle.Equals(originalHandle))
                {
                    BaseValues.DriverSession.SwitchTo().Window(handle);
                    BaseValues.DriverSession.Close();
                }
            }

            BaseValues.DriverSession.SwitchTo().Window(originalHandle);
        }

        /// <summary>
        /// Execute the javascript for this controls
        /// </summary>
        public static void JavaScriptExecutor(string script)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
            executor.ExecuteScript(script);
        }

        /// <summary>
        /// Open the link on new Tab
        /// </summary>
        /// <param name="linkHref"></param>
        public static void OpenLinkInNewTab(string linkHref)
        {
            JavaScriptExecutor(string.Format("window.open('{0}')", linkHref));
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(100);
        }


        /// <summary>
        /// Scroll into view
        /// </summary>
        /// <param name="control"></param>
        public static void MoveToCenterOfElement(IWebElement control, bool force_move = false)
        {
            if (CommonHelper.is_valid(control) || force_move)
            {
                //New way, reuse the extension method provided in Selenium support library
                try
                {
                    BaseValues.DriverSession.ExecuteJavaScript("arguments[0].scrollIntoView({behavior: 'auto', block: 'center', inline: 'center'});", control);
                    System.Threading.Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not scroll to element, the element reference is null");
                    Log.Warn("Could not scroll to element, the element reference is null");
                }
            }
        }

        /// <summary>
        /// Scroll element into view; Deprecated, this will execute MoveToCenterOfElement now
        /// </summary>
        /// <param name="control"></param>
        public static void MoveToElement(IWebElement control, bool force_move = false) => CommonHelper.MoveToCenterOfElement(control, force_move);

        /// <summary>
        /// Scroll element into view; Performs some additional checks on provided control then executes MoveToCenterOfElement on control's web element
        /// </summary>
        /// <param name="control"></param>
        public static void MoveToElement(BaseControl control, bool force_move = false) => CommonHelper.MoveToCenterOfElement(control.GetWrappedControl(), force_move);


        /// <summary>
        /// Scroll element into view without capturing a screenshot; Redirects to MoveToCenterOfElement now
        /// </summary>
        /// <param name="control"></param>
        public static void MoveToElementWithoutCapture(BaseControl control) => CommonHelper.MoveToCenterOfElement(control.GetWrappedControl(), true);

        public static void MoveToElementWithoutCaptureAndCenter(IWebElement control) => CommonHelper.MoveToCenterOfElement(control, true);

        public static void ScrollToEndOfPage()
        {
            CommonHelper.JavaScriptExecutor("window.scrollTo(0, document.body.scrollHeight);");
        }

        public static void ScrollToBeginOfPage()
        {
            CommonHelper.JavaScriptExecutor("window.scrollTo(0, 0);");
        }

        public static void ScrollToPosition(double x, double y)
        {
            CommonHelper.JavaScriptExecutor($"window.scrollTo({x}, {y});");
        }
        public static void ScrollLeftToOffSetWidth(string element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
            IWebElement scrollArea = BaseValues.DriverSession.FindElement(By.XPath(element));
            executor.ExecuteScript("arguments[0].scrollLeft = arguments[0].offsetWidth", scrollArea);
        }

        public static bool IsElementChecked(string eleId)
        {
            IWebElement optionEle = BaseValues.DriverSession.FindElement(By.Id(eleId));
            MoveToElement(optionEle);
            return (optionEle.Selected) ? true : false;
        }

        public static void SwitchToAnotherOne(string AnotherOneName)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
            IWebElement toElement = BaseValues.DriverSession.FindElement(By.XPath($"//ul[@class = 'dropdown-menu subhead-lateral']//li/a[contains(.,'{AnotherOneName}')]"));
            executor.ExecuteScript("return arguments[0].click();", toElement);
        }
        public static string ConvertImgToBase64String(string imgPath)
        {
            if (imgPath is null)
                return string.Empty;
            else
                using (Image image = Image.FromFile(imgPath))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        // Convert byte[] to Base64 String
                        string base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
        }

        public static IWebElement VisibilityOfAllElementsLocatedBy(int second, string xpath)
        {
            WebDriverWait wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(second));
            IWebElement element = null;
            try
            {
                element = wait.Until(
                       SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(xpath))).FirstOrDefault();
            }
            catch (WebDriverTimeoutException e)
            {
                Console.WriteLine(e.Message);
            }
            return element;
        }

        public static bool WaitUntilElementVisible(int timeoutSeconds, string xpath, bool isCaptured = true)
        {
            WebDriverWait wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(timeoutSeconds));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            try
            {
                IWebElement element;
                element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                MoveToElement(element);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CaptureScreen(element), $"The element <font color='green'><b>{element.Text}</b></font> is <font color='green'><b>visible</b></font> on screen");
                return element.Displayed;
            }
            catch (WebDriverTimeoutException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool WaitUntilElementInvisible(string ValueToFind, int timeout = 5, bool isCaptured = true)
        {
            WebDriverWait wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            try
            {
                bool hide = false;
                hide = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath(ValueToFind)));
                if (isCaptured)
                    ExtentReportsHelper.LogInformation($"Waiting for the element with Xpath : { ValueToFind}] hide after { timeout} seconds");
                return hide;
            }
            catch (WebDriverTimeoutException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static string GetCurrentDriverURL
        {
            get
            {
                return BaseValues.DriverSession.Url;
            }
        }

        public static string PrettyXml(string xmlContent)
        {
            try
            {
                XDocument doc = XDocument.Parse(xmlContent);
                return doc.ToString();
            }
            catch (Exception)
            {
                return xmlContent;
            }
        }

        public static void SwitchFrame(int indexFrame)
        {
            BaseValues.DriverSession.SwitchTo().Frame(indexFrame);
        }

        public static void SwitchFrame(string frameName)
        {
            BaseValues.DriverSession.SwitchTo().Frame(frameName);
        }

        public static void SwitchToDefaultContent()
        {
            BaseValues.DriverSession.SwitchTo().DefaultContent();
        }

        public static void SwitchFrameAction(string frameName, Action action)
        {
            SwitchFrame(frameName);
            action();
            SwitchToDefaultContent();
        }

        public static void JavaScriptClick(BaseControl control)
        {
            try
            {
                Log.Info($"Trying to Click by javascript.");
                IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
                control.CaptureAndLog($"Element <font color = 'blue'><b>{control.GetText()}</b></font> has been clicked after capturing the screenshot");
                executor.ExecuteScript("arguments[0].click();", control);
                BasePage.JQueryLoad();
                System.Threading.Thread.Sleep(667);
            }
            catch (StaleElementReferenceException e)
            {
                Log.Error($"The element is out of date. " + e.Message);
                throw new StaleElementReferenceException("The element is out of date.");
            }
            catch (ElementNotInteractableException e)
            {
                Log.Error($"The element does not interactable. " + e.Message);
                throw new ElementNotInteractableException("The element does not interactable(hidden or disable or something ...)");
            }
            catch (NoSuchElementException e)
            {
                Log.Error($"The element does not exist on DOM. " + e.Message);
                throw new NoSuchElementException("The element could not be found.");
            }
        }

        /// <summary>
        /// Waiting grid loading after draging and droping
        /// </summary>
        /// <param name="element1"></param>
        /// <param name="element2"></param>
        /// <param name="action"></param>
        public static void DragAndDrop(BaseControl fromElement, BaseControl toElement, Action action)
        {
            DragAndDrop(fromElement.GetWrappedControl(), toElement.GetWrappedControl(), action);
        }

        /// <summary>
        /// Waiting grid loading after draging and droping
        /// </summary>
        /// <param name="element1"></param>
        /// <param name="element2"></param>
        /// <param name="action"></param>
        public static void DragAndDrop(IWebElement fromElement, IWebElement toElement, Action action)
        {
            Log.Info("Drag and Drop element");
            HighLightElement(fromElement);
            MoveToElement(fromElement, true);
            HighLightElement(toElement);
            ExtentReportsHelper.LogInformation(CaptureScreen(), "Before Drag and Drop 2 items");
            var builder = new Actions(BaseValues.DriverSession);
            var dragAndDrop = builder.ClickAndHold(fromElement).MoveToElement(toElement).Release(toElement).Build();
            dragAndDrop.Perform();
            action();
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
            ExtentReportsHelper.LogInformation(CaptureScreen(), "After Drag and Drop 2 items");
        }

        public static bool Compare2List<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }

        /// <summary>
        /// Click on the position on the element
        /// </summary>
        /// <param name="control"></param>
        /// <param name="margin"></param>
        /// <param name="percentValue"></param>
        /// <param name="isCaptured"></param>
        public static void ClickAtPosition(IWebElement control, Margin margin, int percentValue, bool isCaptured = true)
        {

            Size size = control.Size;
            int x = size.Width;
            int y = size.Height;
            int expectedValue;

            switch (margin)
            {
                case Margin.Top:
                    x /= 2;
                    expectedValue = y * percentValue / 100;
                    if (expectedValue < y && y - expectedValue > 0)
                        y = expectedValue;
                    break;
                case Margin.Left:
                    y /= 2;
                    expectedValue = x * percentValue / 100;
                    if (expectedValue < x && x - expectedValue > 0)
                        x = expectedValue;
                    break;
                case Margin.Bottom:
                    x /= 2;
                    expectedValue = y * percentValue / 100;
                    if (expectedValue < y && y - expectedValue > 0)
                        y -= expectedValue;
                    break;
                default:
                    y /= 2;
                    expectedValue = x * percentValue / 100;
                    if (expectedValue < x && x - expectedValue > 0)
                        x -= expectedValue;
                    break;
            }
            try
            {
                string text = control.Text == "" ? control.GetAttribute("value") : control.Text;
                Actions builder = new Actions(BaseValues.DriverSession);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CaptureScreen(control), $"The page was clicked on the element <font color='green'><b><i>'{text}'</i></b></font> at position <i>X:{x} | Y:{y}</i>.");

                builder.MoveToElement(control, x, y).Click().Build().Perform();
                BasePage.JQueryLoad();
                System.Threading.Thread.Sleep(333);
            }
            catch (Exception e)
            {
                Log.Warn(e.Message);
            }
        }

        public static void ClickAtPosition(BaseControl control, Margin margin, int percentValue, bool isCaptured = true)
        {
            ClickAtPosition(control.GetWrappedControl(), margin, percentValue, isCaptured);
        }

        public static void click_element(IWebElement element, bool isCaptured = true)
        {
            try
            {
                if (!CommonHelper.is_valid(element) && element == null)
                {
                    Log.Info($"Web element is null, disabled, or not visible");
                    throw new InvalidElementStateException("Web element is null, disabled, or not visible");
                }

                //Clear any existing input to prevent conflicting actions
                //((IActionExecutor)_driver).ResetInputState();
                //System.Threading.Thread.Sleep(50);

                //Scroll page to the center of the element
                CommonHelper.MoveToCenterOfElement(element, true);

                //string text = element.Text == "" ? element.GetAttribute("value") : element.Text;
                if (isCaptured)
                    //ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Element will be clicked after capturing the screenshot");
                    ExtentReportsHelper.LogInformation(null, $"Element will be clicked after capturing the screenshot");

                try
                {
                    //Build and execute the input action for clicking the element
                    IAction click_element = new Actions(_driver).MoveToElement(element).Click().Build();
                    click_element.Perform();
                }
                catch
                {
                    string error = $"Exception from click action, attempting to click by Javascript instead...";
                    try
                    {
                        //CommonHelper.ClickAtPosition(element, Margin.Right, 10, false);
                        _driver.ExecuteJavaScript("arguments[0].focus();", element);
                        _driver.ExecuteJavaScript("arguments[0].click();", element);
                    }
                    catch
                    {
                        throw new InvalidElementStateException("Web element could not be clicked by Javascript or by Selenium - Ensure the correct element is provided or you are on the correct page");
                    }
                }

                //Verify or wait for submissions to complete just incase
                //BasePage.JQueryLoad();
                System.Threading.Thread.Sleep(100);
            }
            catch (Exception any_ex)
            {
                string error = $"Encountered exception while attempting to click web element of a control - Exception = [{any_ex.Message}: {any_ex.StackTrace}]"; //, Exception #2: [ {js_ex.StackTrace} ];";
                Console.WriteLine(error);
                Log.Warn(error);
                //throw any_ex;
            }
        }

        /// <summary>
        /// Expects DriverSession to exist, so it may throw a NullReferenceException
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool is_stale(IWebElement element) => ExpectedConditions.StalenessOf(element)(BaseValues.DriverSession);

        /// <summary>
        /// Only use if the element not being found is a valid case, as it will not throw exceptions
        /// </summary>
        /// <param name="element"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        public static IWebElement find_element_weak(IWebElement element, By by)
        {
            if (element == null)
                return null;

            var wait = new DefaultWait<IWebDriver>(BaseValues.DriverSession)
            {
                Timeout = TimeSpan.FromSeconds(1.5),
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            try
            {
                return wait.Until(d => {
                    try { return element.FindElement(by); } catch { return null; }
                });
            }
            catch (WebDriverTimeoutException) { return null; }
            catch (TimeoutException) { return null; }
        }

        /// <summary>
        /// Verify an element is enabled and displayed
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool is_valid(IWebElement element)
        {
            try
            {
                var wait = new DefaultWait<IWebDriver>(BaseValues.DriverSession)
                {
                    Timeout = TimeSpan.FromSeconds(0.5),
                    PollingInterval = TimeSpan.FromMilliseconds(100)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
                var result = wait.Until(ExpectedConditions.ElementToBeClickable(element));
                return (result != null);
            }
            catch (Exception)
            {
                Log.Info("Element is not valid - null, disabled, or invisible");
                Console.Write("Element is not valid - null, disabled, or invisible");
                return false;
            }
        }

        /// <summary>
        /// Get the full file path to a file inside the automation app's working directory. 
        /// The provided file name should include the file extension and the file should already exist in the working directory.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFullFilePath(string fileName)
        {
            return BaseValues.PathReportFile + fileName;
        }

        /// <summary>
        /// Get default download file path
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFullDownLoadFilePath(string fileName)
        {
            return BaseValues.PathReportFile + "Download\\" + fileName;
        }

        /// <summary>
        /// Get default baseline file path
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFullBaseLineFilePath(string fileName)
        {
            return GetBaseLinePath() + "\\" + fileName;
        }

        /// <summary>
        /// Get default baseline folder path
        /// </summary>
        /// <returns></returns>
        public static string GetBaseLinePath()
        {
            return BaseValues.BaselineFilesDir;
        }

        /// <summary>
        /// Back to last page
        /// </summary>
        public static void NavigateBack()
        {
            try
            {
                BaseValues.DriverSession.Navigate().Back();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error during navigating to previous browser page - Exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Open a local file
        /// </summary>
        /// <param name="html_file"></param>
        public static void OpenLocalFile(string html_file)
        {
            try
            {
                BaseValues.DriverSession.Url = "file:///" + html_file;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to open local file - Exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Get Export file name
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="dataInput"></param>
        /// <returns></returns>
        public static string GetExportFileName(string exportType, params string[] dataInput)
        {
            // House BOM - export file nam
            if (exportType.ToLower().Equals(ExportType.House_BOM.ToString().ToLower()))
                return $"Pipeline_HouseBom_{dataInput[0]}_{dataInput[1]}_FilteredTo_ALL";
            // Job BOM  - export file name
            else if (exportType.ToLower().Equals(ExportType.Job_BOM.ToString().ToLower()))
                return $"Pipeline_Bom_{dataInput[0]}";
            // Community House BOM  - export file name
            else if (exportType.ToLower().Equals(ExportType.Community_HouseBOM.ToString().ToLower()))
                return $"Pipeline_HouseBom_{dataInput[0]}_FilteredTo_ALL";
            // House Quantities - export file name
            else if (exportType.ToLower().Equals(ExportType.DefaultHouseQuantities.ToString().ToLower()))
                return $"Pipeline_Quantities_{dataInput[0]}";
            else if (exportType.ToLower().Equals(ExportType.SpecificCommunityHouseQuantities.ToString().ToLower()))
                return $"Pipeline_Quantities_{dataInput[0]}_{dataInput[1]}";
            else if (exportType.ToLower().Equals(ExportType.Worksheets.ToString().ToLower()))
                return $"Pipeline_Worksheets_{dataInput[0]}";
            // Others
            else
                return $"Pipeline_{exportType}";
        }
        public static bool WaitUntilAlertAppears(IWebDriver driver, TimeSpan timeout)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout);
                wait.Until(ExpectedConditions.AlertIsPresent());
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                // Alert did not appear within the specified timeout
                return false;
            }
        }

        public static void ScrollToElement(string elementID)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
            IWebElement scrollArea = BaseValues.DriverSession.FindElement(By.Id(elementID));
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", scrollArea);
        }

        public static bool IsElementDisabled(string elementID, string attributeName, string AttributeValueToMatch)
        {
            IWebElement elementUI = BaseValues.DriverSession.FindElement(By.Id(elementID));
            if (elementUI.GetAttribute(attributeName).Trim().Contains(AttributeValueToMatch) == true)
            {
                ExtentReportsHelper.LogPass("<font color='green'>Element is disabled</font>");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Element is not disabled</font>");
                return false;
            }
        }
        public static string ReadTextFromFile(string fileDir)
        {
            if (string.IsNullOrEmpty(fileDir))
            {
                throw new ArgumentException("File directory cannot be null or empty", nameof(fileDir));
            }
            try
            {
                string fileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + fileDir;
                string contentOfFile = File.ReadAllText(fileLocation);
                return contentOfFile.Trim('\'', '"').Trim();
            }
            catch (UnauthorizedAccessException)
            {
                return "Cannot access the file!" + nameof(fileDir);
            }
            catch (FileLoadException)
            {
                return "File Load Exception occurred";
            }
        }
        public static string GetTextParagraph(string elementId)
        {
            try
            {
                if (elementId == null)
                {
                    return "Element not found!";
                }
                IWebElement element = BaseValues.DriverSession.FindElement(By.Id(elementId));
                IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
                string rawContent = (string)executor.ExecuteScript("return arguments[0].textContent;", element);
                return rawContent.Trim('\'', '"');
            }
            catch (NoSuchElementException)
            {
                throw new Exception($"Element with ID {elementId} not found.");
            }
        }
        public static bool CompareContentElementAndFile(string elementId, string fileDir)
        {
            string extractedTextFromFile = ReadTextFromFile(fileDir);
            string extractedTextFromElement = GetTextParagraph(elementId);
            return extractedTextFromFile.Equals(extractedTextFromElement);
        }
    }
}