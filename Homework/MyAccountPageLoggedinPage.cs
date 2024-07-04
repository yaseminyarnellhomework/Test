using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace Test
{
    public class MyAccountPageLoggedinPage 
    {


        public IWebElement DressesLink
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.CssSelector("a[href='http://automationpractice.com/index.php?id_category=8&controller=category']"));

                return retval;
            }
        }       

        public string UserFirstName
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.XPath("//*[@title='View my customer account']/span"));
                var userInfo = retval.Text;
                string[] user = userInfo.Split(' ');
                string firstname = user[0];               
                
                return firstname;
            }
        }

        public string UserLastName
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.XPath("//*[@title='View my customer account']/span"));
                var userInfo = retval.Text;
                string[] user = userInfo.Split(' ');
                string lastname = user[1];

                return lastname;
            }
        }        
        public ControlSelenium MyAccountLoggedinLandingPage
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.ClassName("page-heading"));
                return new ControlSelenium(retval);
            }
        }

        public bool PageLoaded
        {
            get
            {
                MyAccountLoggedinLandingPage.WaitForVisible();
                bool visible = MyAccountLoggedinLandingPage.Visible;
                return visible;
            }
        }
    }
}
