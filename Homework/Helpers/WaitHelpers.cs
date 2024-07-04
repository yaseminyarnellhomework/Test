using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NUnit.Framework;


namespace Test
    {
    public class WaitHelpers
        {
        protected IWebDriver driver;

        #region Constructors

        public WaitHelpers()
            {
            driver = UTF.Driver;
            }

        public WaitHelpers(IWebDriver driver)
            {
            this.driver = driver;
            }

        #endregion Constructors

        public IWebElement WaitForVisible(IWebElement element)
            {
            Stopwatch watch = new Stopwatch();
            while (!element.Displayed && watch.Elapsed.TotalSeconds < 15)
                {
                Thread.Sleep(500);
                }
            return element;
            }

        public IWebElement WaitForVisible(By locator)
            {
            return WaitForVisible(Element(locator));
            }

        public IWebElement Element(By locator, int secondsToWait = 15)
            {
            List<IWebElement> elements = driver.FindElements(locator).ToList();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (elements.Count == 0 && watch.Elapsed.TotalSeconds < secondsToWait)
                {
                Thread.Sleep(500);
                elements = driver.FindElements(locator).ToList();
                }

            if (elements.Count == 0)
                throw new NotFoundException("Element was not found after: " + secondsToWait + " seconds.");

            return elements[0];
            }

        /// <summary>
        /// Searches a heirarchy for a specific element.  Lower count in list = highest level of tree.
        /// </summary>
        /// <param name="breadCrumbs"></param>
        /// <param name="secondsToWait"></param>
        /// <returns></returns>
        public IWebElement Element(List<By> breadCrumbs, int secondsToWait = 15)
            {
            var element = Element(breadCrumbs[0]);

            if (breadCrumbs.Count > 1)
                {
                for (int i = 1; i < breadCrumbs.Count; i++)
                    {
                    element = Element(element, breadCrumbs[i]);
                    }
                }

            return element;
            }

        /// <summary>
        /// Waits for an element to exist under a existing parent element
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="locator"></param>
        /// <param name="secondsToWait"></param>
        /// <returns></returns>
        public IWebElement Element(IWebElement parent, By locator, int secondsToWait = 15)
            {
            List<IWebElement> elements = parent.FindElements(locator).ToList();
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (elements.Count == 0 && watch.Elapsed.TotalSeconds < secondsToWait)
                {
                Thread.Sleep(500);
                elements = parent.FindElements(locator).ToList();
                }

            if (elements.Count == 0)
                throw new NotFoundException("Element was not found after: " + secondsToWait + " seconds.");

            return elements[0];
            }

        public List<IWebElement> Elements(By locator, int secondsToWait = 15)
            {
            List<IWebElement> elements = UTF.Driver.FindElements(locator).ToList();
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (elements.Count == 0 && watch.Elapsed.TotalSeconds < secondsToWait)
                {
                Thread.Sleep(500);
                elements = UTF.Driver.FindElements(locator).ToList();
                }

            if (elements.Count == 0)
                throw new NotFoundException("Element was not found after: " + secondsToWait + " seconds.");

            return elements;
            }

        public List<IWebElement> Elements(IWebElement parent, By locator, int secondsToWait = 15)
            {
            List<IWebElement> elements = parent.FindElements(locator).ToList();
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (elements.Count == 0 && watch.Elapsed.TotalSeconds < secondsToWait)
                {
                Thread.Sleep(500);
                elements = parent.FindElements(locator).ToList();
                }

            if (elements.Count == 0)
                throw new NotFoundException("Element was not found after: " + secondsToWait + " seconds.");

            return elements;
            }

        public void Load(int secondsToWait = 15)
            {
           // UTF.Helpers.Javascript.WaitForAjaxToComplete(secondsToWait);
            WaitForBlockingUI();
            WaitForBlockIFrame();
            }

        private void WaitForBlockingUI(int secondsToWait = 15)
            {
            ReadOnlyCollection<IWebElement> eleBlocker = driver.FindElements(By.ClassName("blockOverlay"));
            int counter = 0;
            while (eleBlocker.Count != 0 && counter < secondsToWait)
                {
                Thread.Sleep(1000);
                eleBlocker = driver.FindElements(By.ClassName("blockOverlay"));
                counter++;
                }

            if (counter > secondsToWait)
                {
                throw new Exception("Spinner is still blocking the page.");
                }
            }

        public void WaitForBlockIFrame(int secondsToWait = 15)
            {
            List<IWebElement> loadingSnippets = driver.FindElements(By.CssSelector("iframe[class='blockUI']")).ToList();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (loadingSnippets.Count > 0 && watch.Elapsed.TotalSeconds < secondsToWait)
                {
                Thread.Sleep(1000);
                loadingSnippets = driver.FindElements(By.CssSelector("iframe[class='blockUI']")).ToList();
                }

            watch.Stop();
            watch.Reset();

            if (loadingSnippets.Count > 0)
                throw new Exception("UI is still being blocked");
            }

        public void Clickable(By locator, int millisecondsToWait = 10000)
            {
            var element = driver.FindElement(locator);
            Clickable(element, millisecondsToWait);
            }

        public void Clickable(IWebElement element, int milliseconds = 10000)
            {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (!element.Displayed && !element.Enabled && watch.Elapsed.TotalSeconds < 30)
                {
                Thread.Sleep(1000);
                }

            watch.Stop();
            watch.Reset();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(milliseconds));
           // wait.Until(WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            }

        }
    }
