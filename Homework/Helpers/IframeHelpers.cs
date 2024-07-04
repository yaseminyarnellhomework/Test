using OpenQA.Selenium;
using System.Diagnostics;
using System.Threading;

namespace Test
{
    public class IframeHelpers
    {
        protected IWebDriver driver;

        #region Constructor

        public IframeHelpers()
        {
            this.driver = UTF.Driver;
        }

        public IframeHelpers (IWebDriver driver)
        {
            this.driver = driver;
        }

        #endregion Constructor


        /// <summary>
        /// Driver switches to the specified frame
        /// </summary>
        /// <param name="iframeName"></param>
        /*protected*/
        public void SwitchToFrame(string iframeName)
        {
            driver.SwitchTo().Frame(iframeName);
            UTF.Helpers.WaitFor.Load();
        }

        /// <summary>
        /// Driver switches to the specified frame
        /// </summary>
        /// <param name="iframeName"></param>
        /*protected*/ public void SwitchToFrame(IWebElement iframeElement)
        {
            driver.SwitchTo().Frame(iframeElement);
            UTF.Helpers.WaitFor.Load();
        }

        public void WaitForIframe(By locator)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var iframes = UTF.Driver.FindElements(locator);
            while (iframes.Count == 0 && watch.Elapsed.TotalSeconds < 30)
            {
                Thread.Sleep(500);
                iframes = UTF.Driver.FindElements(locator);
            }

            watch.Stop();
            watch.Reset();
        }

        /// <summary>
        /// Driver switches to the page the frame was called from
        /// </summary>
        /*protected*/ public void SwitchToDefaultPage()
        {
            driver.SwitchTo().DefaultContent();
            UTF.Helpers.WaitFor.Load();
        }
    }
}
