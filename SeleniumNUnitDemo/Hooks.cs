using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;

namespace SeleniumNUnitDemo
{
    //Enum for browser type
    public enum BrowserType
    {
        Chrome, 
        IE, 
        Firefox
    }

    public class Hooks : Base
    {
        private BrowserType _browserType;

        [SetUp]
        public void InitializeTest()
        {
            //Get the value from NUnit-console --params 
            //e.g. nunit3-console.exe --params:Browser=Firefox \SeleniumNUnitParam.dll
            //If nothing specified, test will run in Chrome browser
            var browserType = TestContext.Parameters.Get("Browser", "Chrome");
            //Parse the browser Type, since its Enum
            _browserType = (BrowserType)Enum.Parse(typeof(BrowserType), browserType);
            //Pass it to browser
            ChooseDriverInstance(_browserType);
        }

        private void ChooseDriverInstance(BrowserType browserType)
        {
            if (browserType == BrowserType.Chrome)
                Driver = new ChromeDriver();
            else if (browserType == BrowserType.Firefox)
            {
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
                service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                service.HideCommandPromptWindow = true;
                service.SuppressInitialDiagnosticInformation = true;
                Driver = new FirefoxDriver(service);
            }
            else if (browserType == BrowserType.IE)
            {
                Driver = new InternetExplorerDriver();
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}
