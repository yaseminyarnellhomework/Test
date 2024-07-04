using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Test
{
    public class ShoppingCartPage
    {


        public string LoggedInUserName
        {
            get
            {
                IWebElement parent = UTF.Helpers.WaitFor.Element(By.ClassName("header_user_info"));
                IWebElement username = parent.FindElement(By.TagName("span"));              
                
                return username.Text;
            }
        }

        public IWebElement DeliveryParentElement()
        {
           
                IWebElement parent = UTF.Helpers.WaitFor.Element(By.Id("center_column"));
                IWebElement retval = parent.FindElement(By.XPath("//div[1]"));

                return retval;
            
        }


        public string DeliveryAddressUserName
        {
            get 
            {
                IWebElement parent = DeliveryParentElement();
                IWebElement username = parent.FindElement(By.ClassName("address_name"));               
                return username.Text;
            }
        }

        public string StreetAddress
        {
            get
            {
                IWebElement parent = DeliveryParentElement();
                IWebElement username = parent.FindElement(By.ClassName("address_address1"));
                return username.Text;
            }
        }

        public string CityStateZip
        {
            get 
            {
                IWebElement parent = DeliveryParentElement();
                IWebElement retval = parent.FindElement(By.ClassName("address_city"));
                               
                return retval.Text;
            }
        }

        public string Country
        {
            get
            {
                IWebElement parent = DeliveryParentElement();
                IWebElement retval = parent.FindElement(By.XPath("//li[6]/span"));

                return retval.Text;
            }
        }

        public string Phone
        {
            get
            {
                IWebElement parent = DeliveryParentElement();
                IWebElement retval = parent.FindElement(By.ClassName("address_phone_mobile"));                

                return retval.Text;
            }
        }

        public IWebElement BillingParentElement()
        {            
                IWebElement gparent = UTF.Helpers.WaitFor.Element(By.Id("center_column"));
                IWebElement retval = gparent.FindElement(By.XPath("//div[3]/div[2]"));                              
           
                return retval;
            
        }

        public string BillingUserName
        {
            get
            {
                IWebElement parent= BillingParentElement();
                IWebElement username = parent.FindElement(By.ClassName("address_name"));
                Console.WriteLine("Billing user name print:" + username.Text);
                return username.Text;
            }
        }

        public string BillingStreetAddress
        {
            get
            {
                IWebElement parent = BillingParentElement();
                IWebElement username = parent.FindElement(By.ClassName("address_address1"));
                return username.Text;
            }
        }

        public string BillingCityStateZip
        {
            get
            {
                IWebElement parent = BillingParentElement();
                IWebElement retval = parent.FindElement(By.ClassName("address_city"));

                return retval.Text;
            }
        }

        public string BillingCountry
        {
            get
            {
                IWebElement parent = DeliveryParentElement();
                IWebElement retval = parent.FindElement(By.XPath("//li[6]/span"));
                //IWebElement retval = UTF.Helpers.WaitFor.Element(By.XPath("//*[@id='center_column']/div[3]/div[1]/ul/li[6]/span"));

                return retval.Text;
            }
        }

        public string BillingPhone
        {
            get
            {
                IWebElement parent = DeliveryParentElement();
                IWebElement username = parent.FindElement(By.ClassName("address_phone_mobile"));

                return username.Text;
            }
        }
        public ControlSelenium LandingPageHero
        {
            get
            {
                
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("cart_title"));
                     
                return new ControlSelenium(retval);
            }
        }

        public bool PageLoaded
        {
            get
            {
                LandingPageHero.WaitForVisible();
                bool visible = LandingPageHero.Visible;
                return visible;
            }
        }
    }
}
