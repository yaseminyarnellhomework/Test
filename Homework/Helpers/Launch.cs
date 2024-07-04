using OpenQA.Selenium;

using System;
using System.Configuration;


namespace Test
{
    public class Launch
    {
        private AppSettingsReader appSettings = new AppSettingsReader();
        public enum Language
        {
            EN,
            ES
        }

        public MainPage WebUrl(Language lang = Language.EN) 
        {
            //string web = "WebUrl" + lang.ToString();
            //string baseURL = (string)appSettings.GetValue(UTF.Environment.ToString() + web + "Url", typeof(String));
            //UTF.TestContext.WriteLine("Test = " + web);
            //UTF.TestContext.WriteLine("URL = " + baseURL);
            UTF.Driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
            UTF.Helpers.WaitFor.Load();
            return new MainPage();
        }


    }
}
