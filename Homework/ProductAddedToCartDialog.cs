using OpenQA.Selenium;

namespace Test
{
    public class ProductAddedToCartDialog
    {


        public LinkSelenium<ShoppingCartPage> ProceedToCheckoutButton
        {
            get
            {
                IWebElement parent = UTF.Helpers.WaitFor.Element(By.ClassName("button-container"));
                IWebElement retval = parent.FindElement(By.TagName("a"));
                return new LinkSelenium<ShoppingCartPage>(retval);
            }
        }


        public ControlSelenium LandingPageHero
        {
            get
            {
                
                IWebElement retval = UTF.Helpers.WaitFor.Element(By.Id("layer_cart"));
                     
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
