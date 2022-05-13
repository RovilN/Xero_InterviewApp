using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Xero_InterviewApp.Commonlibrary;

namespace Xero_InterviewApp.Test_Scripts
{
    [TestClass]
    public class TestCase_Invoice_Login : TestBase
    {
        [TestMethod]
        [TestCategory("InvoiceTestCase")]
        [Priority(1)]
        public void TC_Invoice_001()
        {
            /// This test will login to the Application and Search for Accepted Quotes
            /// It will open the accepted Quotes and Create draft invoices. 
            /// and check the status of the invoices is set to draft       
       

            CommonMethodLibrary.Login(_uri,_username,_password);       
            //CommonMethodLibrary.SkiptheMFA();
            //CommonMethodLibrary.NavigationArea("Business", "Quotes");
            //CommonMethodLibrary.Search(TestData.Datalibrary.Data["quoteRecord"]);
            //CommonMethodLibrary.OpenGridRecord(0);
            //CommonMethodLibrary.CreateInvoice("CreateInvoice");
            //CommonMethodLibrary.Scroll_Down_Full_Page();
            //CommonMethodLibrary.SaveTheInvoice();
        }
    }
}

