using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Threading;


namespace Test
{
    public class ControlSelenium : IControlBase
    {
        protected IWebElement element;
        protected bool scrollIntoView;
        protected bool waitForStale;
        protected IWebDriver driver = null;

        protected IWebDriver Driver
        {
            get
            {
                if (driver == null)
                {
                    driver = UTF.Driver;
                }
                return driver;
            }
        }

        #region Constructor

        public ControlSelenium(IWebElement element)
        {
            this.element = element;
        }

        public ControlSelenium(IWebElement element, bool scrollIntoView = true)
        {
            this.element = element;
            this.scrollIntoView = scrollIntoView;
        }

        public ControlSelenium(IWebElement element, bool scrollIntoView = true, bool waitForStale = false)
        {
            this.element = element;
            this.waitForStale = waitForStale;
            this.scrollIntoView = scrollIntoView;
        }

        #endregion Constructor

       

        public string GetText()
        {
            var retval = "";
            WaitForVisible();
            if (element.Text != null && element.Text != string.Empty)
            {
                retval = element.Text;
            }
            else
            {
                retval = element.GetAttribute("value");
            }
            return retval;
        }

        public bool Visible
        {
            get
            {
                return element.Displayed;
            }
        }

        public void WaitForVisible(int secondsToWait = 15, bool shouldThrow = true)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (!Visible && watch.Elapsed.TotalSeconds < secondsToWait)
            {
                Thread.Sleep(1000);
            }

            if (!Visible && shouldThrow)
            {
                throw new Exception("Element did not become visible after: " + secondsToWait + " seconds");
            }

            watch.Stop();
            watch.Reset();
        }

        public bool Enabled
        {
            get
            {
                return element.Enabled;
            }
        }

        public void WaitForEnabled(int secondsToWait = 15)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (!Enabled && watch.Elapsed.TotalSeconds < secondsToWait)
            {
                Thread.Sleep(1000);
            }

            if (!Enabled)
            {
                throw new Exception("Element did not become visible after: " + secondsToWait + " seconds");
            }
        }

        public void WaitForClickable()
        {
            WaitForVisible();
            WaitForEnabled();
        }

        protected void TryClick(IWebElement element, int currentRetries, int totalRetries)
        {
            if (currentRetries < totalRetries)
            {
                try
                {
                    element.Click();
                }
                catch (Exception)  //Some dropdowns don't disappear right away blocking the element from being clicked.  This should fix the issue.
                {
                    currentRetries++;
                    Thread.Sleep(500);
                    TryClick(element, totalRetries, currentRetries);
                }
            }
        }

        protected bool IsStale
        {
            get
            {
                bool retval = false;
                try
                {
                    retval = ExpectedConditions.StalenessOf(element)(UTF.Driver);
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    retval = true; // IE returns this error for stale elements
                }
                return retval;
            }
        }

        public object Enums { get; private set; }

        protected void WaitForStale()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (!IsStale && watch.Elapsed.TotalSeconds < 10)
            {
                Thread.Sleep(250);
            }
            if (!IsStale)
            {
                throw new Exception("Element never became stale");
            }
        }

        public virtual void Click()
        {
            //if (scrollIntoView)
            //{
            //    new JavascriptHelpers().ScrollIntoView(element);
            //}
            WaitForClickable();
            TryClick(element, 0, 3);
            if (waitForStale && (UTF.Browser != BrowserEnum.InternetExplorer))  // && UTF.Browser != Enums.BrowserEnum.Edge))
            {
                WaitForStale();
            }
            new WaitHelpers().Load();
        }
    }
}
