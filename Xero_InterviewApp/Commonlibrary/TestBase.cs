using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
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

        public static TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }



        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            testContextInstance = context;
            //For Test Users

            //InCase user wants to use Add the run settings file. Open the visual studio->Select the test tab and Configure run settings before using it.
                //_username = testContextInstance.Properties["username"].ToString();
                //_password = testContextInstance.Properties["password"].ToString();
                //_uri = testContextInstance.Properties["url"].ToString();
            _username = "rovil.nigam@gmail.com";
            _password = "Nashville@1987";
            _uri = "https://login.xero.com/identity/user/login";

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

            _driver.Quit();

        }
    }
}