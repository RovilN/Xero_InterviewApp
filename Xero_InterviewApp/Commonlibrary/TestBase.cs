using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
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

        public static TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }


        public static void TakeScreenshot(string fileName, ScreenshotImageFormat imageFormat )
        {
            ITakesScreenshot screenshotDriver = _driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Jpeg);
        }

  


        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            testContextInstance = context;
            //For Test Users
            _password = testContextInstance.Properties["password"].ToString();

            //InCase user wants to use Add the run settings file. Open the visual studio->Select the test tab and Configure run settings before using it.
            _password = GetSecret(_password);
            _username = testContextInstance.Properties["username"].ToString();
                _password = testContextInstance.Properties["password"].ToString();
                _uri = testContextInstance.Properties["url"].ToString();
             _keyVault = testContextInstance.Properties["keyVaultUri"].ToString();


            //_username = "rovil.nigam@gmail.com";
            //_password = "Nashville@1987";
            //_uri = "https://login.xero.com/identity/user/login";

        }

        private static string GetSecret(string secretName)
        {
            var client = new SecretClient(new Uri("https://xerovaulttechchallenge.vault.azure.net/"), new DefaultAzureCredential());
            KeyVaultSecret secret = client.GetSecret(secretName);

            if (secret.Value == null)
                throw new NullReferenceException("Not able to retrieve secret from Key Vault");
            return secret.Value;
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            _username = string.Empty;
            _password = string.Empty;


        }

        [TestInitialize]
        public void TestInitialize()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddExcludedArgument("enable-automation");

            options.AddArgument("incognito");
            options.AddArgument("--disable-plugins-discovery");
            // options.AddArgument("headless");           
            _driver = new ChromeDriver(options);

        }



        [TestCleanup]
        public void TestCleanup()
        {
            #region Take the Screenshot of the test before quiting the browser
            try
            {
                //Check for Outcome  of the Test//
               string status = testContextInstance.CurrentTestOutcome.ToString();
                if (status.Equals("Failed") || status.Equals("Error"))
                {
                    Console.WriteLine(url);
                    _driver.Manage().Window.Size = new Size(1920, 1080);
                    var currentDateTimeAndTest = testContextInstance.TestName + "_" + DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss");
                    string fileName = string.Format("Error_" + currentDateTimeAndTest + ".jpeg", testContextInstance.TestResultsDirectory);
                    TakeScreenshot(fileName, ScreenshotImageFormat.Jpeg);
                }
                else if (status.Equals("Passed"))
                {
                    try
                    {
                        var currentDateTimeAndTest = testContextInstance.TestName + "_" + DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss");
                        string fileName = string.Format("Success_" + currentDateTimeAndTest + ".jpeg", testContextInstance.TestResultsDirectory);
                        TakeScreenshot(fileName, ScreenshotImageFormat.Jpeg);


                       
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        var currentDateTimeAndTest = testContextInstance.TestName + "_" + DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss");
                        string fileName = string.Format("SignOutError_" + currentDateTimeAndTest + ".jpeg", testContextInstance.TestResultsDirectory);
                        TakeScreenshot(fileName, ScreenshotImageFormat.Jpeg);
                    }
              
                }
                _driver.Quit();
            }
          
            catch (Exception e)
            {
                _driver.Quit();

                throw new Exception("Not able to take screenshot, " + "Exception Message - " + e.Message + Environment.NewLine + "StackTrace - " + e.StackTrace.Trim());
            }
            #endregion

        }


    }
    }
