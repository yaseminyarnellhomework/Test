using System;
using NUnit.Framework;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using Test;
using System.Text;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace Homework
{
    class HomeWorkTests
    {

        private const string expectedEmail = "yaseminyarnel205@gmail.com";

        StringBuilder sbFirstName = new StringBuilder();
        StringBuilder sbLastName = new StringBuilder();
        StringBuilder sbAddress = new StringBuilder();
        StringBuilder sbCity = new StringBuilder();
        StringBuilder sbState = new StringBuilder();
        StringBuilder sbZip = new StringBuilder();
        StringBuilder sbCountry = new StringBuilder();
        StringBuilder sbPhone = new StringBuilder();

        IWebDriver driver = new ChromeDriver();
        
        [SetUp]
        public void Initialize()
        {
            //navigate to URL  
            // driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
            //Maximize the browser window  
            // driver.Manage().Window.Maximize();
            // Thread.Sleep(2000);
        }

        //Verify Sign in button clicked on the first page and loading the second page.
        [Test]
        public void VerifyMainPage()
        {
            MainPage landingPage = UTF.Launch.WebUrl();
            landingPage.LandingPageHero.WaitForVisible();
            string val = VerifyLoginButtonOnMainPage();
            Assert.AreEqual(string.Empty, val, "Failure occurred in the Personal Mega Menu for Savings on the Home Page");
        }

        //Input email in textbox and click "Create an account" button, which loads account creation page.
        [Test]
        public void VerifyCreateAccount()
        {
            MainPage landingPage = UTF.Launch.WebUrl();
            landingPage.LandingPageHero.WaitForVisible();
            string val = CreateAccount();
            Assert.AreEqual(string.Empty, val, "Failure occurred in email input and clicking Create an Account button");
        }

        [Test]
        public void VerifyFillingCreateAccountForm()
        {
            MainPage landingPage = UTF.Launch.WebUrl();
            landingPage.LandingPageHero.WaitForVisible();
            string val = FillTheCreateAccountForm();
            Assert.AreEqual(string.Empty, val, "Failure occurred in the Create an Account form");
        }

        //verify first name and last name matches with input-Part of easy homeowrk.
        [Test]
        public void VerifyLoggedinUserInfo()
        {
            MainPage landingPage = UTF.Launch.WebUrl();
            landingPage.LandingPageHero.WaitForVisible();
            string val = VerifyFirstAndLastName();
            Assert.AreEqual(string.Empty, val, "Failure occurred in verify first name and last name for logged in user");
        }

        //Click Dresses, Evening Dresses and verify the page shows up.
        [Test]
        public void VerifyClickEveningDresses()
        {
            MainPage landingPage = UTF.Launch.WebUrl();
            landingPage.LandingPageHero.WaitForVisible();
            string val = VerifyNavigationToEveningDresses();
            Assert.AreEqual(string.Empty, val, "Failure occurred in navigation to evening dressess shopping cart.");
        }

        //In hard homework, verify all info under Delivery Address in the shopping cart summary.
        [Test]
        public void VerifyDeliveryAddressInfo()
        {
            MainPage landingPage = UTF.Launch.WebUrl();
            landingPage.LandingPageHero.WaitForVisible();
            string val = MatchDeliveryAddressInfo();
            //Console.WriteLine("my user name:"+val);
            Assert.AreEqual(string.Empty, val, "Failure occurred in verify info under Delivery Address in the shopping cart.");
        }

        //In hard homework, verify all info under Billing Address in the shopping cart summary.
        [Test]
        public void VerifyBillingAddressInfo()
        {
            MainPage landingPage = UTF.Launch.WebUrl();
            landingPage.LandingPageHero.WaitForVisible();
            string val = MatchBillingAddressInfo();
            //Console.WriteLine("my user name:"+val);
            Assert.AreEqual(string.Empty, val, "Failure occurred in verify info under Billing Address in the shopping cart.");
        }

        public string VerifyLoginButtonOnMainPage()
        {

            string retval = string.Empty;


            MainPage m = new MainPage();
            MyAccountPage mp = m.SignInButton.Click();
            retval += mp.PageLoaded ? string.Empty : "My account page is not loaded successfully." + Environment.NewLine;

            // UTF.Driver.Navigate().GoToUrl(testUrl); // Go back to test page
            return retval;
        }
        public string CreateAccount()
        {
            string retval = string.Empty;
            VerifyLoginButtonOnMainPage();
            MyAccountPage mp = new MyAccountPage();
            mp.EmailAddressTextBox.SendKeys(expectedEmail);
            AccountCreationForm acCreation = mp.CreateAnAccountButton.Click();
            retval += acCreation.PageLoaded ? string.Empty : "CreateAccount form is not loaded successfully." + Environment.NewLine;

            // Assert.IsTrue(fvPage.PageLoaded, "The fiVISION web form page did not load.");
            // UTF.Driver.Navigate().GoToUrl(testUrl); // Go back to test page
            return retval;
        }

        public string FillTheCreateAccountForm()
        {
            CreateAccount();
            AccountCreationForm acCreation = new AccountCreationForm();
            string retval = string.Empty;
            var firstName = "Yasemin";
            var lastName = "Yarnell";
            var address = "123 Broadway st";
            var country = "United States";
            var city = "Columbus";
            var state = acCreation.StateDropdown.GetText();
            var zip = "43260";
            var phone = "1234567890";
            var actualCountry = acCreation.CountryDropdown.GetText();
            Thread.Sleep(1000);
            //ControlSelenium cs=acCreation.GenderRadioButton;
           
            sbFirstName.Append(firstName);
            sbLastName.Append(lastName);
            sbAddress.Append(address);
            sbCity.Append(city);
            sbState.Append(state);
            sbZip.Append(zip);
            sbCountry.Append(country);
            sbPhone.Append(phone);

            
            acCreation.LastNameTextBox.SendKeys(lastName);
            acCreation.FirstNameTextBox.SendKeys(firstName);
            var actualEmail = acCreation.EmailTextBox.GetText();
            retval += actualEmail == expectedEmail ? string.Empty : "Email (" + actualEmail + ") did not match expected value (" + expectedEmail + ")." + Environment.NewLine;
            acCreation.PasswordTextBox.SendKeys("test00");
            acCreation.CompanyTextBox.SendKeys("FirmA");
            acCreation.AddressTextBox.SendKeys(address);
            acCreation.CityTextBox.SendKeys(city);
            acCreation.StateDropdown.Click();
            acCreation.ZipTextBox.SendKeys(zip);
            acCreation.CountryDropdown.ToString();           
            Console.WriteLine("Print country value" + actualCountry);
            retval += actualCountry == country ? string.Empty : "Country (" + actualCountry + ") did not match expected one (" + country + ")." + Environment.NewLine;
            acCreation.MobilePhoneTextBox.SendKeys(phone);
            acCreation.AddressAliasTextBox.SendKeys("test");
            MyAccountPageLoggedinPage accountInfo = acCreation.RegisterButton.Click();
            retval += accountInfo.PageLoaded ? string.Empty : "CreateAccount form is not loaded successfully." + Environment.NewLine;
            //acCreation.BirthDayDropdown.Click();
            //acCreation.BirthMonthDropdown.Click();
            //acCreation.BirthYearDropdown.Click();

            return retval;
        }
        public string VerifyFirstAndLastName()
        {
            string retval = string.Empty;
            FillTheCreateAccountForm();
            MyAccountPageLoggedinPage mp = new MyAccountPageLoggedinPage();
            var actualFirstname = mp.UserFirstName;
            var actualLastname = mp.UserLastName;
            retval += actualFirstname == sbFirstName.ToString() ? string.Empty : "2 FirstName" + actualFirstname + Environment.NewLine;
            retval += actualLastname == sbLastName.ToString() ? string.Empty : "2 LastName:" + actualLastname + Environment.NewLine;

            return retval;
        }

        public string VerifyNavigationToEveningDresses()
        {
            string retval = string.Empty;
            FillTheCreateAccountForm();
            MyAccountPageLoggedinPage mp = new MyAccountPageLoggedinPage();
            new JavascriptHelpers().Click(mp.DressesLink);
            DressesPage dressesPage = new DressesPage();
            retval += dressesPage.PageLoaded ? string.Empty : "Dresses Page is not loaded successfully." + Environment.NewLine;
            new JavascriptHelpers().Click(dressesPage.EveningDressesLink);
            //EveningDressesPage eveningDressesPage = dressesPage.EveningDressesLink.Click();
            EveningDressesPage eveningDressesPage = new EveningDressesPage();
            retval += eveningDressesPage.PageLoaded ? string.Empty : "Evening Dresses Page is not loaded successfully." + Environment.NewLine;
            eveningDressesPage.PrintedDressImageButton.Click();
            SelectedProductPage selectedProductPage = new SelectedProductPage();
            ProductAddedToCartDialog addedtoCardPage = selectedProductPage.AddToCartButton.Click();
            retval += addedtoCardPage.PageLoaded ? string.Empty : "Product added to cart Page is not loaded successfully." + Environment.NewLine;
            ShoppingCartPage shoppingCartPage = addedtoCardPage.ProceedToCheckoutButton.Click();
            retval += shoppingCartPage.PageLoaded ? string.Empty : "Product added to cart Page is not loaded successfully." + Environment.NewLine;
            return retval;
        }

        public string MatchDeliveryAddressInfo()
        {
            string retval = string.Empty;
            VerifyNavigationToEveningDresses();
            ShoppingCartPage shoppingCart = new ShoppingCartPage();
            var loggedInUserName=shoppingCart.LoggedInUserName;
            var deliveryAddressUserName = shoppingCart.DeliveryAddressUserName;
            var deliveryAddress = shoppingCart.StreetAddress;
            var cityStateZip = shoppingCart.CityStateZip;            
            string[] array = Regex.Split(cityStateZip, ",? +");
            string city = array[0];
            string state = array[1];
            string zip = array[2];
            var country = shoppingCart.Country;
            var phone = shoppingCart.Phone;

            retval += loggedInUserName == deliveryAddressUserName ? string.Empty : "User name does not match" + Environment.NewLine;
            retval += sbAddress.ToString() == deliveryAddress ? string.Empty : "Street address does not match" + Environment.NewLine;
            retval += sbCity.ToString() == city ? string.Empty : "City does not match" + Environment.NewLine;
            retval += sbState.ToString() == state ? string.Empty : "State does not match" + Environment.NewLine;
            retval += sbZip.ToString() == zip ? string.Empty : "Zip does not match" + Environment.NewLine;
            retval += sbCountry.ToString() == country ? string.Empty : "Country does not match" + Environment.NewLine;
            retval += sbPhone.ToString() == phone ? string.Empty : "Phone does not match" + Environment.NewLine;
            return retval;
        }

        public string MatchBillingAddressInfo()
        {
            string retval = string.Empty;
            VerifyNavigationToEveningDresses();
            ShoppingCartPage shoppingCart = new ShoppingCartPage();
            var loggedInUserName = shoppingCart.LoggedInUserName;
            var billingUserName = shoppingCart.BillingUserName;
            var address = shoppingCart.BillingStreetAddress;
            var cityStateZip = shoppingCart.BillingCityStateZip;
            string[] array = Regex.Split(cityStateZip, ",? +");
            string city = array[0];
            string state = array[1];
            string zip = array[2];
            var country = shoppingCart.BillingCountry;
            var phone = shoppingCart.BillingPhone;

            retval += loggedInUserName == billingUserName ? string.Empty : "User name does not match" + Environment.NewLine;
            retval += sbAddress.ToString() == address ? string.Empty : "Street address does not match" + Environment.NewLine;
            retval += sbCity.ToString() == city ? string.Empty : "City does not match" + Environment.NewLine;
            retval += sbState.ToString() == state ? string.Empty : "State does not match" + Environment.NewLine;
            retval += sbZip.ToString() == zip ? string.Empty : "Zip does not match" + Environment.NewLine;
            retval += sbCountry.ToString() == country ? string.Empty : "Country does not match" + Environment.NewLine;
            retval += sbPhone.ToString() == phone ? string.Empty : "Phone does not match" + Environment.NewLine;
            return retval;
        }

        [TearDown]
        public void EndTest()
        {
            //close the browser  
            driver.Close();
        }
    }
}


