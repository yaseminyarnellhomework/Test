using OpenQA.Selenium;

namespace Test
{
    public class MainPage 
    {
        public LinkSelenium<MyAccountPage> SignInButton
        {
            get
            {                
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.ClassName("login"));
                return new LinkSelenium<MyAccountPage>(retval, true);
            }
        }
        public ControlSelenium LandingPageHero
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("block_top_menu"));
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
