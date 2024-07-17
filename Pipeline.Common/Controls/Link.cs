using LinqToExcel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Common.Controls
{
    public class Link : BaseControl
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Link(Row row) : base(row) { }
        
        public Link(FindType findType, string valueToFind) : base(findType, valueToFind) { }
        public Link(IWebElement element) : base(element) { }

        public Link(FindType findType, string valueToFind, int timeoutSeconds) : base(findType, valueToFind, timeoutSeconds) { }

        public void Click() => CommonHelper.click_element(this.GetWrappedControl());

        public string GetURL
        {
            get
            {
                return GetWrappedControl().GetAttribute("href");
            }
        }
    }
}