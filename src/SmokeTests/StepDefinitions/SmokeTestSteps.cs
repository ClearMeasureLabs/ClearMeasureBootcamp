using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using ClearMeasure.Bootcamp.IntegrationTests;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

namespace ClearMeasure.Bootcamp.SmokeTests.StepDefinitions
{
    [Binding]
    public class SmokeTestSteps
    {
        private static IWebDriver _driver;
        private static ICommandServer _driverService;
        private static readonly string DriversPath = SmokeTestPaths.GetDriversPath();
        private static readonly string HomePage = ConfigurationManager.AppSettings["siteUrl"];

        //Hooks
        [BeforeFeature]
        public static void Startup()
        {
            Trace.WriteLine("Loading database");
            new ZDataLoader().PopulateDatabase();
            Trace.WriteLine("Loaded database");

            var browser = ConfigurationManager.AppSettings["browser"];
            SelectBrowser(browser);
        }

        [AfterScenario()]
        public static void AfterScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                TakeScreenshot(_driver);
            }
        }

        [AfterFeature]
        public static void Cleanup()
        {
            _driverService?.Dispose();
            _driver?.Quit();
            _driver?.Dispose();
        }

        private static void SelectBrowser(string browser)
        {
            switch (browser)
            {
                case "Firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "Chrome":
                    var chromeDriverService = ChromeDriverService.CreateDefaultService(DriversPath, "chromedriver.exe");
                    chromeDriverService.Start();
                    _driver = new RemoteWebDriver(chromeDriverService.ServiceUrl, DesiredCapabilities.Chrome());
                    _driverService = chromeDriverService;
                    break;
                case "IE":
                    _driver = new InternetExplorerDriver(DriversPath);
                    break;
                case "PhantomJS":
                    var phantomJsPath = SmokeTestPaths.GetPhantomJsPath();
                    var phantomJsDriverService = PhantomJSDriverService.CreateDefaultService(phantomJsPath);
                    _driver = new PhantomJSDriver(phantomJsDriverService);
                    _driverService = phantomJsDriverService;
                    break;
                default:
                    throw new ArgumentException("Unknown browser");
            }
        }

        private static void TakeScreenshot(IWebDriver driver)
        {
            string fileNameBase = string.Format("error_{0}_{1}_{2}",
                                                FeatureContext.Current.FeatureInfo.Title.ToIdentifier(),
                                                ScenarioContext.Current.ScenarioInfo.Title.ToIdentifier(),
                                                DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "testresults");
            if (!Directory.Exists(artifactDirectory))
                Directory.CreateDirectory(artifactDirectory);

            string pageSource = driver.PageSource;
            string sourceFilePath = Path.Combine(artifactDirectory, fileNameBase + "_source.html");
            File.WriteAllText(sourceFilePath, pageSource, Encoding.UTF8);
            Console.WriteLine("Page source: {0}", new Uri(sourceFilePath));

            ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;

            if (takesScreenshot != null)
            {
                var screenshot = takesScreenshot.GetScreenshot();

                string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.png");

                screenshot.SaveAsFile(screenshotFilePath, ImageFormat.Png);

                Console.WriteLine("Screenshot: {0}", new Uri(screenshotFilePath));
            }
            
        }

        [BeforeStep]
        public void WaitForLoad()
        {
            _driver?.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        //Given
        [Given(@"I am on the site")]
        public void GivenIAmOnTheSite()
        {
            _driver.Navigate().GoToUrl(HomePage);
        }

        //When
        [When(@"I log in")]
        public void WhenILogIn()
        {
            SatisfyLoginCondition(true);
        }

        [When(@"I log out")]
        public void WhenILogout()
        {
            SatisfyLoginCondition(false);
        }



        private void SatisfyLoginCondition(bool loggedIn)
        {
            if (!_driver.Title.StartsWith("Login", StringComparison.CurrentCulture))
            {
                if (loggedIn) return;
                var logout = _driver.FindElement(By.LinkText("Logout"));
                logout.Click();
            }
            else
            {
                if (!loggedIn) return;
                var userSelect = new SelectElement(_driver.FindElement(By.Id("UserName")));
                userSelect.SelectByValue("jpalermo");
                var login = _driver.FindElement(By.XPath("//button[contains(text(), 'Log In')]"));
                login.Click();
            }
        }

        [When(@"I browse to '(.*)'")]
        public void WhenIBrowseTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        [When(@"I browse to the site")]
        public void WhenIBrowseToTheSite()
        {
            _driver.Navigate().GoToUrl(HomePage);
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string term)
        {
            var query = _driver.FindElement(By.Name("q"));
            query.SendKeys(term);
            query.Submit();
        }

        [When(@"I click on the (.*) link")]
        public void WhenIClickOn(string linkText)
        {
            var link = _driver.FindElement(By.LinkText(linkText));
            link.Click();
        }

        //Then
        [Then(@"I should be on the (.*) page")]
        public void ThenIShouldBeOnPage(string page)
        {
            var completeUrl = HomePage + SmokeTestPageUrls.PageUrls[page];
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(d => d.Url.ToLower().Contains(SmokeTestPageUrls.PageUrls[page].ToLower()));
        }

        [When]
        public void WhenICreateANewExpenseReport()
        {
            var link = _driver.FindElement(By.LinkText("New"));
            link.Click();
        }

        [When]
        public void WhenISubmitTheExpenseReport()
        {
            _driver.FindElement(By.Name("Title")).SendKeys("titleasdf");
            _driver.FindElement(By.Name("Description")).SendKeys("descriptionasd");
            _driver.FindElement(By.Name("Total")).SendKeys("123");
            
            var button = _driver.FindElement(By.XPath("//button[contains(text(), 'Submit')]"));
            button.Click();
        }
    }
}