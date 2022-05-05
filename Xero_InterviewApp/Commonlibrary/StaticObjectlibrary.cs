using System;
using System.Collections.Generic;
using System.Text;

namespace Xero_InterviewApp.TestData
{
    public class StaticObjectlibrary
    {
        
            public static Dictionary<string, string> Object = new Dictionary<string, string>()
        {
            {"username", "//input[@data-automationid='Username--input']" },
            {"password", "//input[@data-automationid='PassWord--input']" },
            {"login", "//button[@data-automationid='LoginSubmit--button']" },
            {"searchLink","//span[@id='buttonanchor-1055-btnInnerEl']" },
            {"searchInputBox","//input[@id='text-1081-inputEl']" },
            {"CreateInvoice","//span[@id='button-1032-btnInnerEl']" }
                
        };
      }
 }

