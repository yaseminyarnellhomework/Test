using OpenQA.Selenium;
using System.Threading;

namespace Test
{
    public class DressesPage
    {
        

        public IWebElement EveningDressesLink
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.CssSelector("a[href='http://automationpractice.com/index.php?id_category=10&controller=category']"));
             
                return retval;
            }
        }

        public ControlSelenium LandingPageHero
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("categories_block_left"));
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
