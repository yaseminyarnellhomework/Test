using OpenQA.Selenium;
using System;

namespace Test
{
    public class LinkSelenium<T> : ControlSelenium, ILinkBase<T>
    {
        #region Constructor

        public LinkSelenium(IWebElement element):base (element)
        {
            this.element = element;
        }

        public LinkSelenium(IWebElement element, bool scrollIntoView = true) :base(element,scrollIntoView)
        {
            this.scrollIntoView = scrollIntoView;
        }

        public LinkSelenium(IWebElement element, bool scrollIntoView = true, bool waitForStale = false) : base(element, scrollIntoView, waitForStale)
        {
            this.element = element;
            this.waitForStale = waitForStale;
            this.scrollIntoView = scrollIntoView;
        }

       #endregion Constructor

        public virtual new T Click()
        {
            base.Click();
            return Activator.CreateInstance<T>();
        }
    }
}
