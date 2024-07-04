using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Test
{
    public class JavascriptHelpers
    {
        protected IWebDriver driver;

        #region Constructor

        public JavascriptHelpers()
        {
            driver = UTF.Driver;
        }


        public JavascriptHelpers(IWebDriver driver)
        {
            this.driver = driver;
        }

        #endregion Constructor

        public IAlert WaitForAlert()
        {
            IAlert retval = null;
            int timeout = 10;
            bool foundAlert = false;

            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (!foundAlert && watch.Elapsed.TotalSeconds < timeout)
            {
                try
                {
                    retval = driver.SwitchTo().Alert();
                    foundAlert = true;
                }
                catch (Exception)
                {
                    Thread.Sleep(500);
                }
            }

            watch.Stop();
            watch.Reset();

            return retval;
        }

        public bool IsAlertPresent()
        {
            bool retval = false;

            try
            {
                driver.SwitchTo().Alert();
                retval = true;
            }
            catch (Exception)
            {
                retval = false;
            }
            return retval;
        }

        public void Click(IWebElement element)
        {
            IJavaScriptExecutor javaScriptExecutor = driver as IJavaScriptExecutor;
            javaScriptExecutor.ExecuteScript("arguments[0].click();", element);
            WaitHelpers waitFor = new WaitHelpers();
            waitFor.Load();
        }


        /// <summary>
        /// Waits for ajax calls to finish and spinner to disappear
        /// 
        /// Note:  This should be in a separate class
        /// </summary>
        /// <param name="secondsToWait">Amount of time to wait for ajax and spinner to finish</param>
        public void WaitForAjaxToComplete(int secondsToWait = 15)
        {
            bool isAjaxFinished = false;
            bool isLoaderHidden = false;
            bool isComplete = false;

            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (!isComplete && watch.Elapsed.TotalSeconds < secondsToWait)
            {
                try
                {
                    bool jQueryDefined = (bool)((IJavaScriptExecutor)driver).
                            ExecuteScript("return window.jQuery != undefined");
                    if (!jQueryDefined)
                    {
                        isAjaxFinished = true;
                        isLoaderHidden = true;
                    }
                    else
                    {
                        isAjaxFinished = (bool)((IJavaScriptExecutor)driver).
                            ExecuteScript("return jQuery.active == 0");
                        isLoaderHidden = (bool)((IJavaScriptExecutor)driver).
                            ExecuteScript("return $('.spinner').is(':visible') == false");
                    }
                    if (isAjaxFinished && isLoaderHidden)
                    {
                        isComplete = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    if (e.Message.Contains("javascript error: $ is not a function"))
                        break;
                    Thread.Sleep(100);
                }
            }
            watch.Stop();
            watch.Reset();
        }

        /// <summary>
        /// Waits for spinner to finish
        /// 
        /// </summary>
        /// <param name="secondsToWait"></param>
        public void WaitForSpinner(int secondsToWait = 10)
        {
            bool isLoaderHidden = false;

            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (!isLoaderHidden && watch.Elapsed.TotalSeconds < secondsToWait)
            {
                try
                {
                    isLoaderHidden = (bool)((IJavaScriptExecutor)driver).
                        ExecuteScript("return $('.spinner').is(':visible') == false");
                }
                catch (Exception)
                {
                    Thread.Sleep(100);
                }

            }
        }

        public string GetAlertText()
        {
            WaitForAlert();
            return driver.SwitchTo().Alert().Text;
        }

        public void AcceptAlert()
        {
            AcceptAlert(false);
        }

        public void AcceptAlert(bool closeIframe)
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WaitForAjaxToComplete();  //If in an iframe we need to wait for the Iframe to finish processing

            if (closeIframe)
            {
                WaitForAjaxToComplete();
            }
        }

        public void DismissAlert()
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.Dismiss();
            WaitForAjaxToComplete();
        }

        /// <summary>
        /// After the scroll, the element is aligned to the top or the bottom of the browser based on the bool passed in
        /// </summary>
        /// <param name="element">Element to be aligned</param>
        /// <param name="alignTop">True = align to the top; false = align to the bottom</param>
        public void ScrollIntoView(IWebElement element, ElementAlignmentEnum alignment = ElementAlignmentEnum.Center)
        {
            IJavaScriptExecutor javaScriptExecutor = driver as IJavaScriptExecutor;

            switch (alignment)
            {
                case ElementAlignmentEnum.Center:
                    if (UTF.Browser == BrowserEnum.Edge || UTF.Browser == BrowserEnum.InternetExplorer)
                    {
                        //Trying this out for IE and Edge since it is just a calculation and doesn't seem to require any special code to be implemented in the browser
                        String scrollElementIntoMiddle = "var viewPortHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);"
                                                       + "var elementTop = arguments[0].getBoundingClientRect().top;"
                                                       + "window.scrollBy(0, elementTop-(viewPortHeight/2));";
                        javaScriptExecutor.ExecuteScript(scrollElementIntoMiddle, element);
                    }
                    else
                    {
                        //At the time this was written, the 'scrollIntoView' with the 'block' parameter was not supported on IE and unknown on Edge.  This case may need to be changed for those browsers.
                        javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView({block: \"center\"});", element);
                    }
                    break;
                case ElementAlignmentEnum.Bottom:
                    javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView(false);", element);
                    break;
                case ElementAlignmentEnum.Top:
                    javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                    break;
            }
        }

        public void FocusElement(IWebElement element)
        {
            IJavaScriptExecutor javaScriptExecutor = driver as IJavaScriptExecutor;
            javaScriptExecutor.ExecuteScript("arguments[0].focus();", element);
        }

        public void Hover(IWebElement element)
        {
            IJavaScriptExecutor javaScriptExecutor = driver as IJavaScriptExecutor;
            javaScriptExecutor.ExecuteScript("arguments[0].fireEvent('onmouseover');", element);
        }

        public void MouseHoverJScript(IWebElement HoverElement)
        {
            IJavaScriptExecutor javaScriptExecutor = driver as IJavaScriptExecutor;

            String mouseOverScript = "if(document.createEvent){var evObj = document.createEvent('MouseEvents');evObj.initEvent('mouseover', true, false); arguments[0].dispatchEvent(evObj);} else if (document.createEventObject) { arguments[0].fireEvent('onmouseover'); }";
            javaScriptExecutor.ExecuteScript(mouseOverScript, HoverElement);

        }

        //public void GetAllAttributes(IWebElement element)
        //{
        //    IJavaScriptExecutor javaScriptExecutor = UTF.MobileDriver as IJavaScriptExecutor;
        //    Dictionary<string, object> attributes = javaScriptExecutor.ExecuteScript("var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;", element) as Dictionary<string, object>;
        //}

        public bool ImageLoaded(IWebElement element)
        {
            Object result = ((IJavaScriptExecutor)driver).ExecuteScript(
               "return arguments[0].complete && " +
               "typeof arguments[0].naturalWidth != \"undefined\" && " +
               "arguments[0].naturalWidth > 0", element);

            return (Boolean)result;
        }
    }
}