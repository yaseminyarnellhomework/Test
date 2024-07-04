
using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.IO;

using NUnit.Framework;

namespace Test
{
    public static class UTF
    {
        public static TestContext TestContext { get; set; }

       

        private static IWebDriver driver = null;
        public static IWebDriver Driver
        {
            get
            {
                if (driver == null)
                {
                    driver = GetDriver();
                }
                return driver;
            }
            set
            {
                driver = value;
            }
        }

        //public static EnvironmentEnum Environment
        //{
        //    get
        //    {
        //        EnvironmentEnum retval = EnvironmentEnum.Test; //default value
        //        if (TestContext.Properties["environment"] != null)
        //        {
        //            retval = ParseEnvironment(TestContext.Properties[(string)"environment"].ToString());
        //        }
        //        return retval;
        //    }
        //}

        private static EnvironmentEnum ParseEnvironment(string environment)
        {
            EnvironmentEnum retval;
            switch (environment.ToUpper())
            {
                case "PROD":
                case "PRODUCTION":
                    retval = EnvironmentEnum.Production;
                    break;
                case "TEST":
                    retval = EnvironmentEnum.Test;
                    break;
                case "TEST2":
                    retval = EnvironmentEnum.Test2;
                    break;
                case "TRAIN":
                    retval = EnvironmentEnum.Train;
                    break;
                case "DEV":
                    retval = EnvironmentEnum.Dev;
                    break;
                case "DR":
                    retval = EnvironmentEnum.Dr;
                    break;
                case "STAGE":
                    retval = EnvironmentEnum.Stage;
                    break;
                default:
                    retval = EnvironmentEnum.Test;
                    break;
            }
            return retval;
        }

        public static Launch Launch
        {
            get
            {
                return new Launch();
            }
        }

        public static HelpersBase Helpers
        {
            get
            {
                return new HelpersBase();
            }
                
        }

        private static BrowserEnum? browser = null;
        public static BrowserEnum Browser
        {
            get
            {
                if (browser == null)
                {
                    browser = GetBrowserFromRunSettings();
                }
                return browser.Value;
            }
        }
        
       
       
        #region Private Methods

        private static BrowserEnum GetBrowserFromRunSettings()
        {
            BrowserEnum retval = BrowserEnum.Chrome;
            string browserAsString = "Chrome";

            //try
            //{
            //    browserAsString = UTF.TestContext.Properties["browser"].ToString();
            //    UTF.TestContext.WriteLine("Browser = " + browserAsString);
            //}
            //catch (NullReferenceException)
            //{
            //    //Run Settings file isn't being used.  Reverting to Default
            //    UTF.TestContext.WriteLine("Using default browser: " + browserAsString);
            //}

            browserAsString = browserAsString.Replace(" ", "").ToUpper();

            switch (browserAsString)
            {
                case "CHROME":
                    retval = BrowserEnum.Chrome;
                    break;
                case "IE":
                case "INTERNETEXPLORER":
                    retval = BrowserEnum.InternetExplorer;
                    break;
                case "FF":
                case "FIREFOX":
                    retval = BrowserEnum.FireFox;
                    break;
                case "EDGE":
                    retval = BrowserEnum.Edge;
                    break;
                default:
                    throw new ApplicationException("Unsupported web driver browser requested by Config.cs");
            }
            
            return retval;
        }

        private static IWebDriver GetDriver()
        {
            IWebDriver retval;
            string workingDirectory = Directory.GetCurrentDirectory();

            switch (Browser)
            {
                case BrowserEnum.Chrome:
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("start-maximized"); // https://stackoverflow.com/a/26283818/1689770
                    chromeOptions.AddArgument("--no-sandbox"); //https://stackoverflow.com/a/50725918/1689770
                    chromeOptions.AddArgument("--disable-infobars"); //https://stackoverflow.com/a/43840128/1689770
                    chromeOptions.AddArgument("--disable-dev-shm-usage"); //https://stackoverflow.com/a/50725918/1689770
                    chromeOptions.AddArgument("--disable-browser-side-navigation"); //https://stackoverflow.com/a/49123152/1689770
                    chromeOptions.AddArgument("--disable-gpu"); //https://stackoverflow.com/questions/51959986/how-to-solve-selenium-chromedriver-timed-out-receiving-message-from-renderer-exc
                    chromeOptions.AddArguments("--incognito");
                    retval = new ChromeDriver(chromeOptions);
                    break;
                case BrowserEnum.InternetExplorer:
                    InternetExplorerDriverService ieService = InternetExplorerDriverService.CreateDefaultService(workingDirectory, "IEDriverServer.exe");
                    InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                    ieOptions.IgnoreZoomLevel = true;
                    retval = new InternetExplorerDriver(ieService, ieOptions);
                    break;
                case BrowserEnum.FireFox:
                    FirefoxOptions options = new FirefoxOptions();
                    //Commented out as NET Core does not like the encoding for Profile (encoding 437 format)
                    //options.Profile = new FirefoxProfile();
                    //options.Profile.AcceptUntrustedCertificates = true;
                    options.AcceptInsecureCertificates = true;
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(workingDirectory, "geckodriver.exe");
                    retval = new FirefoxDriver(service, options);
                    break;               
                default:
                    throw new ApplicationException("Unsupported web driver browser requested by Config.cs");
            }

            retval.Manage().Window.Maximize();
            return retval;
        }

        #endregion Private Methods
    }
}
