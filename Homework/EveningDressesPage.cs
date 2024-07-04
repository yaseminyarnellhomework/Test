using OpenQA.Selenium;

namespace Test
{
    public class EveningDressesPage
    {

        public IWebElement Frame
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Name("fancybox-frame1610265699335"));
                return retval;
            }
        }

        public IWebElement PrintedDressImageButton
        {
            get
            {
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.CssSelector("a[href='http://automationpractice.com/index.php?id_product=4&controller=product']"));
                return retval;
            }
        }

        public ControlSelenium LandingPageHero
        {
            get
            {
                IWebElement parent = UTF.Helpers.WaitFor.Element(By.Id("columns"));
                IWebElement retval = parent.FindElement(By.XPath("//span[contains(text(), 'Evening Dresses')]"));
                
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
