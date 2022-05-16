using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Xero_InterviewApp.Commonlibrary
{

    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    public class TestBase
    {

        //User Name and Password
        public static string _username;
        public static string _password;
        public static IWebDriver _driver;
        public static string url;
        //Initialization
        public static string _uri;
        //Test Environment
        static public string _keyVault;

        // Create TestContext Instance Property
        public static TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }


        public static void TakeScreenshot(string fileName, ScreenshotImageFormat imageFormat)
        {
            #region  Selenium Code for Taking Screenshot
            ITakesScreenshot screenshotDriver = _driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
            #endregion
        }




        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            #region Intialize the variables from RunSettings File
            testContextInstance = context;
            _keyVault = testContextInstance.Properties["keyVaultUri"].ToString();
            //For Test Users          
            //InCase user wants to use Add the run settings file. Open the visual studio->Select the test tab and Configure run settings before using it.
            _username = testContextInstance.Properties["username"].ToString();
            _password = testContextInstance.Properties["password"].ToString();
            _password = GetSecret(_password);
            _uri = testContextInstance.Properties["url"].ToString();
            #endregion


        }

        private static string GetSecret(string secretName)
        {
            #region Get Secret from Azure Keyvault
            var client = new SecretClient(new Uri(_keyVault), new DefaultAzureCredential());
            KeyVaultSecret secret = client.GetSecret(secretName);

            if (secret.Value == null)
                throw new NullReferenceException("Not able to retrieve secret from Key Vault");
            return secret.Value;
            #endregion
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            #region Cleanup the variable for entire assembly
            _username = string.Empty;
            _password = string.Empty;
            #endregion


        }

        [TestInitialize]
        public void TestInitialize()
        {
            #region If Test needed to be executed as headless and through Remote Webdriver
            // options.AddArgument("headless");     
            //_driver = new RemoteWebDriver(new Uri("http://127.0.0.1:4444/wd/hub"), options);
            #endregion

            #region Intialize the Chrome Driver Version
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("incognito");
            _driver = new ChromeDriver(options);
            #endregion

        }



        [TestCleanup]
        public void TestCleanup()
        {

            try
            {
                #region Check for Test Outcome
                string status = testContextInstance.CurrentTestOutcome.ToString();
                #endregion

                #region Take Page Screenshot
                if (status.Equals("Failed") || status.Equals("Error"))
                {
                    Console.WriteLine(url);
                    _driver.Manage().Window.Size = new Size(1920, 1080);
                    var currentDateTimeAndTest = testContextInstance.TestName + "_" + DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss");
                    string fileName = string.Format("Error_" + currentDateTimeAndTest + ".PNG", testContextInstance.TestResultsDirectory);
                    TakeScreenshot(fileName, ScreenshotImageFormat.Png);
                    testContextInstance.AddResultFile(fileName);
                }
                else if (status.Equals("Passed"))
                {
                    try
                    {
                        var currentDateTimeAndTest = testContextInstance.TestName + "_" + DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss");
                        string fileName = string.Format("Success_" + currentDateTimeAndTest + ".PNG", testContextInstance.TestResultsDirectory);
                        TakeScreenshot(fileName, ScreenshotImageFormat.Png);
                        testContextInstance.AddResultFile(fileName);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        var currentDateTimeAndTest = testContextInstance.TestName + "_" + DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss");
                        string fileName = string.Format("SignOutError_" + currentDateTimeAndTest + ".PNG", testContextInstance.TestResultsDirectory);
                        TakeScreenshot(fileName, ScreenshotImageFormat.Png);
                        testContextInstance.AddResultFile(fileName);
                    }

                }
                #endregion

                #region Quit the Driver and Close all browser instances

                _driver.Quit();

                #endregion
            }

            catch (Exception e)
            {
                #region Close the browser instance and throw an exception in case unable to take screenshot
                _driver.Quit();
                #endregion
                throw new Exception("Not able to take screenshot, " + "Exception Message - " + e.Message + Environment.NewLine + "StackTrace - " + e.StackTrace.Trim());

            }


        }


    }
}
