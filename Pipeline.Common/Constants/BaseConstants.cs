namespace Pipeline.Common.Constants
{
    public class BaseConstants
    {
        //----------------------------Configuration-----------------------------
        public const string ApplicationProtocol = "ApplicationProtocol";
        public const string ApplicationDomain = "ApplicationDomain";
        public const string ApplicationRoot = "ApplicationRoot";
        public const string BrowserTarget = "BrowserTarget";
        public const string MaximizeBrowser = "MaximizeBrowser";
        public const string BrowserWidth = "BrowserWidth";
        public const string BrowserHeight = "BrowserHeight";
        public const string UserName = "UserName";
        public const string Password = "Password";
        public const string IsAutoLogin = "IsAutoLogin";
        public const string PageloadTimeouts = "PageloadTimeouts";
        public const string SaveImageAsBase64 = "SaveImageAsBase64";
        public const string IsCaptureEverything = "CaptureEachStep";
        public const string BaselineFilesDir = "BaselineFilesDir";

        
        //------------------------------Finding Elements-----------------------------

        public const string FindType = "Field Find Type";
        public const string ValueToFind = "Value To Find";


        //------------------------------Message Controls-----------------------------
        public const string LeftClickMessage = "The page was clicked with the left mouse.";
        public const string LeftCheckMessage = "The page was checked with the left mouse checkbox.";
        public const string LeftUnCheckMessage = "The page was unchecked with the left mouse checkbox.";
        public const string SelectItemMessage = "The page was selected item <font color='green'><b>{0}</b></font> with the left mouse dropdown list.";
        public const string SendkeysMessage = "The text <font color='green'><b>'{0}'</b></font> was entered in the text editor.";
        public const string ClearValueMessage = "The text was cleared in the text editor.";
        public const string HoverMouseMessage = "The page was hovered on the element.";
        public const string JavaScriptMessage = "The page was execute the script via javascript.";
    }
}
