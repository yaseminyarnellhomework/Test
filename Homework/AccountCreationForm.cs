using OpenQA.Selenium;

namespace Test
{
    public class AccountCreationForm
    {

        public ControlSelenium GenderRadioButton
        {
            get
            {
                IWebElement parent = UTF.Helpers.WaitFor.Element(By.Id("uniform-id_gender2"));
                IWebElement retval = parent.FindElement(By.Id("id_gender2"));
                return new ControlSelenium(retval);
            }
        }

        public IWebElement FirstNameTextBox
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("customer_firstname"));
                return retval;
            }
        }

        public IWebElement LastNameTextBox
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("customer_lastname"));
                return retval;
            }
        }

        public ControlSelenium EmailTextBox
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("email"));
                return new ControlSelenium(retval);
            }
        }

        public IWebElement PasswordTextBox
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("passwd"));
                return retval;
            }
        }

        public ControlSelenium BirthDayDropdown
        {
            get
            {
                IWebElement day = UTF.Helpers.WaitFor.Element(By.Name("days"));
                IWebElement selectDay = day.FindElement(By.XPath("//*[@id='days']/option[9]"));
                return new ControlSelenium(selectDay);
            }
        }

        public ControlSelenium BirthMonthDropdown
        {
            get
            {
                IWebElement month = UTF.Helpers.WaitFor.Element(By.Name("months"));
                IWebElement selectMonth = month.FindElement(By.XPath("//*[@id='days']/option[4]"));
                return new ControlSelenium(selectMonth);
            }
        }

        public ControlSelenium BirthYearDropdown
        {
            get
            {
                IWebElement year = UTF.Helpers.WaitFor.Element(By.Name("years"));
                IWebElement selectMonth = year.FindElement(By.XPath("//*[@id='years']/option[1991]"));
                return new ControlSelenium(selectMonth);
            }
        }

        public IWebElement CompanyTextBox
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("company"));
                return retval;
            }
        }

        public IWebElement AddressTextBox
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("address1"));
                return retval;
            }
        }

        public IWebElement CityTextBox
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("city"));
                return retval;
            }
        }

        public ControlSelenium StateDropdown
        {
            get
            {
                IWebElement parent = UTF.Helpers.WaitFor.Element(By.Id("uniform-id_state"));
                IWebElement state = parent.FindElement(By.Id("id_state"));
                IWebElement selectState = state.FindElement(By.XPath("//*[@id='id_state']/option[37]"));
                return new ControlSelenium(selectState);
            }
        }

        public IWebElement ZipTextBox
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("postcode"));
                return retval;
            }
        }

        public ControlSelenium CountryDropdown
        {
            get
            {
                //IWebElement country = UTF.Helpers.WaitFor.Element(By.XPath("//*[@id='uniform-id_country']/span"));
                IWebElement parent = UTF.Helpers.WaitFor.Element(By.Id("uniform-id_country"));
                IWebElement country = parent.FindElement(By.TagName("span"));
                return new ControlSelenium(country);
            }
        }

        public IWebElement MobilePhoneTextBox
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Name("phone_mobile"));
                return retval;
            }
        }

        public IWebElement AddressAliasTextBox
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("alias"));
                return retval;
            }
        }
        public LinkSelenium<MyAccountPageLoggedinPage> RegisterButton
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("submitAccount"));
                return new LinkSelenium<MyAccountPageLoggedinPage>(retval);
            }
        }      

        public ControlSelenium AccountCreationLanding
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.ClassName("page-heading"));
                //IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("authentication"));
                return new ControlSelenium(retval);
            }
        }

        public bool PageLoaded
        {
            get
            {
                AccountCreationLanding.WaitForVisible();
                bool visible = AccountCreationLanding.Visible;
                return visible;
            }
        }
    }
}
