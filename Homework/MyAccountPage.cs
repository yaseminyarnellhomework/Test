using OpenQA.Selenium;

namespace Test
{
    public class MyAccountPage 
    {

        public IWebElement EmailAddressTextBox
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("email_create"));
                return retval;
            }
        }

        public LinkSelenium<AccountCreationForm> CreateAnAccountButton
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("SubmitCreate"));
                return new LinkSelenium<AccountCreationForm>(retval);
            }
        }
        public ControlSelenium MyAccountLandingPage
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("authentication"));
                return new ControlSelenium(retval);
            }
        }

        public bool PageLoaded
        {
            get
            {
                MyAccountLandingPage.WaitForVisible();
                bool visible = MyAccountLandingPage.Visible;
                return visible;
            }
        }
    }
}
