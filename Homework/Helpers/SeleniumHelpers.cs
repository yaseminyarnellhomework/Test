using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using Test;

namespace Test
{
    public class SeleniumHelpers
    {
        private readonly int _defaultTimeout = 30;

        /// <summary>
        /// Driver switches to the original tab
        /// </summary>
        /*protected*/
        public void SwitchTabsFirst()
        {
            UTF.Driver.SwitchTo().Window(UTF.Driver.WindowHandles.First());
            UTF.Helpers.WaitFor.Load();
        }

        /// <summary>
        /// Driver switches to the last tab
        /// </summary>
        /*protected*/
        public void SwitchTabsLast()
        {
            UTF.Driver.SwitchTo().Window(UTF.Driver.WindowHandles.Last());
            UTF.Helpers.WaitFor.Load();
        }

        /// <summary>
        /// Driver switches to the index passed tab
        /// </summary>
        /*protected*/
        public void SwitchTabs(int index)
        {
            UTF.Driver.SwitchTo().Window(UTF.Driver.WindowHandles[index]);
            UTF.Helpers.WaitFor.Load();
        }

        /// <summary>
        ///  Moves to the specified element. Can also be used for hover.
        /// </summary>
        /// <param name="element"></param>
        public void MoveToElement(IWebElement element)
        {
            Actions action = new Actions(UTF.Driver);
            action.MoveToElement(element).Perform();
        }

        public void MoveToAndClickElement(By locator)
        {
            var element = FindElement(locator, _defaultTimeout);
            MoveToElement(element);
            element.Click();
        }

        public IWebElement FindElement(By locator, int timeoutSeconds)
        {
            if (timeoutSeconds > 0)
            {
                var wait = new WebDriverWait(UTF.Driver, TimeSpan.FromSeconds(timeoutSeconds))
                {
                    Message = $"Element was not found within timeout of {timeoutSeconds}"
                };
                return wait.Until(d => d.FindElement(locator));
            }
            return UTF.Driver.FindElement(locator);
        }
    }
}
