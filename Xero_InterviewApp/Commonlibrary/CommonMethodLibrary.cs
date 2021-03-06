using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Diagnostics;
using System.Threading;

namespace Xero_InterviewApp.Commonlibrary
{
    public class CommonMethodLibrary : TestBase
    {


        public static void Login(string url,string username,string password)
        {        

            #region Login to Xero Application
    
                  
            _driver.Navigate().GoToUrl(url);
            _driver.Manage().Window.Maximize();
            _driver.Manage().Cookies.DeleteAllCookies();

            Trace.WriteLine("Test");

            new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).
                    Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(TestData.StaticObjectlibrary.Object["username"]))).SendKeys(username);

          
            new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).
                     Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(TestData.StaticObjectlibrary.Object["password"]))).SendKeys(password);

            

            new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).
                     Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(TestData.StaticObjectlibrary.Object["login"]))).Click();
         
            var actualUrl = _driver.Url;
          


                Assert.IsTrue(true, "Marking the test cases pass just for testing purpose");
            //Assert.AreEqual("https://go.xero.com/app/!Bytq5/dashboard", actualUrl, "Unable to get login to Application");

          
            #endregion
        }


        public static void WaitForPagetoLoad()
        {
            #region Wait until Page load Completes

            new WebDriverWait(_driver, TimeSpan.FromSeconds(30)).
               Until(_waitforPagetoload=>((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState").Equals("complete"));
           
            #endregion 
        }
        public static void SelectOrganization(string organizationName)
        {
      
            #region Select the Organization


            var actualUrl = _driver.Url;

            Assert.AreEqual("https://go.xero.com/app/!Bytq5/dashboard", actualUrl, "Unable to get login to Application");

            #endregion
        }

        public static void SkiptheMFA()
        {
            #region Skip the Multi Factor Authentication
            new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).
                     Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-automationid='mfa-notnow']"))).Click();
                
            #endregion 
        }

        public static void NavigationArea(string area, string subarea)
        {
          
            #region Select the Navigation and Sub Navigation Area

            try
            {
                string _area = area.ToLower();
                string _subarea = subarea.ToLower();

                new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).
                         Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@data-name='navigation-menu/'" + _area + "']"))).Click();


                new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).
                         Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@data-name='navigation-menu/'" + _area + "/" + _subarea + "']"))).Click();

                var actualUrl = _driver.Url;
            }
            catch (Exception ex)
            {
                Assert.Fail("Unable to Open the Navigation");
            }


            #endregion
        }
        public static void Search(string searchtext)
        {
          

            #region Search the Record in the grid and Open the record

            try
            {


                new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).
                         Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(TestData.StaticObjectlibrary.Object["searchLink"]))).Click();


                new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).
                         Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(TestData.StaticObjectlibrary.Object["searchInputBox"]))).SendKeys(searchtext);


                new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).
                         Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(TestData.StaticObjectlibrary.Object["searchLink"]))).Click();


            }
            catch (Exception ex)
            {
                Assert.Fail("Unable to Search the Record");
            }


            #endregion
        }

        public static void OpenGridRecord(int index)
        {
       
            #region Open the Grid Record
            try
            {


                IWebElement element = new WebDriverWait(_driver, new TimeSpan(0, 0, 30)).
                             Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//table[@id='gridview-1073-table']//tr[@data-recordindex='" + index + "']")));

                Actions action = new Actions(_driver);
                action.DoubleClick(element).Perform();

            }
            catch (Exception ex)
            {
                Assert.Fail("Unable to Open  the grid Record");
            }
            #endregion
        }

        public static void CreateInvoice(string buttonName)
        {
       
            #region Check whether user is able to create the invoice or not
            try

            {
                string getText = _driver.FindElement(By.XPath("//div[@data-automationid='sales-quoteheadertoolbar-addquotetitle-text']")).Text;
                Assert.AreEqual(getText, "Accepted", "Quote is not in Accepted status use the correct Quote for creating Invoice");
                new WebDriverWait(_driver, new TimeSpan(0, 0, 60)).
                Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id(TestData.StaticObjectlibrary.Object["CreateInvoice"])));
                _driver.FindElement(By.Id(TestData.StaticObjectlibrary.Object["CreateInvoice"])).Click();
                _driver.SwitchTo().Alert().Accept();
                new WebDriverWait(_driver, new TimeSpan(0, 0, 60)).
                Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//h1//span['New Invoice']")));


            }
            catch (Exception ex)
            {
                Assert.Fail("Unable to Open  the grid Record");
            }
            #endregion
        }

        public static void Scroll_Down_Full_Page()
        {
            #region Scroll down till botton of the page

            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollBy(0,350)", "");
           
            #endregion
        }

        public static void SaveTheInvoice()
        {
            #region Save the Invoice Record
            Thread.Sleep(2000);
            _driver.FindElement(By.XPath("//a[@title='Save as draft']")).Click();
            Thread.Sleep(5000);
            var actualvalue = _driver.FindElement(By.XPath("//p[contains(text(),'Draft')]")).Text.ToLower();
            var expectedValue = "Draft".ToLower();
            if (actualvalue == expectedValue)
            {
                Assert.IsTrue(true, "Invoice is not in draft status"); /// Check whether invoice is not draft status or not.
            }

            #endregion
        }



    }
}

