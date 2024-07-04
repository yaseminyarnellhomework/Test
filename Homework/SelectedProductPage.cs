using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Test
{
    public class SelectedProductPage
    {
        

        public LinkSelenium<ProductAddedToCartDialog> AddToCartButton
        {
            get
            {

                //Stopwatch watch = new Stopwatch();
                IWebElement iframe = UTF.Helpers.WaitFor.Element(By.CssSelector("iframe[src*='http://automationpractice.com/index.php?id_product=4&controller=product&content_only=1']"));
                UTF.Helpers.IFrame.SwitchToFrame(iframe);

                IWebElement retval = UTF.Helpers.WaitFor.Element(By.ClassName("exclusive"));

                return new LinkSelenium<ProductAddedToCartDialog>(retval);
            }


        }
}
}
