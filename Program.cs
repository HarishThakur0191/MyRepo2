using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumProject
{
    class Program
    {
        public static IWebDriver initializeDriver() {
            IWebDriver driver = new ChromeDriver(@"C:\Users\Z003m47h\source\repos\ValueLabsTest\packages\Selenium.Chrome.WebDriver.74.0.0\driver");
            return driver;
        }

        static void Main(string[] args)
        {
            IWebDriver driver = initializeDriver();

            driver.Navigate().GoToUrl("https://www.toolsqa.com/automation-practice-table");

            driver.Manage().Window.Maximize();

            IJavaScriptExecutor j = (IJavaScriptExecutor)driver;
            j.ExecuteScript("window.scrollBy(0,500)");
            j.ExecuteScript("window.scrollBy(0,-500)");

            string structureName = "Taipei 101";

            IList<IWebElement> lstBody = driver.FindElements(By.XPath("//table//tbody//th[text()='" + structureName + "']//..//td"));

            string rank = lstBody[getIndex(driver, "Rank")].Text;

            IWebElement link = lstBody[getIndex(driver, "…")];

            Console.WriteLine("Rank of {0} is: {1}", structureName, rank);

            link.FindElement(By.LinkText("details")).Click();

            Console.WriteLine("Current URL: {0}", driver.Url);

            Assert.AreEqual("https://www.toolsqa.com/automation-practice-table#", driver.Url);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementExists(By.Id("login")));

            driver.Close();
        }

        public static int getIndex(IWebDriver driver, string headerName)
        {
            int idx = -1;
            int count = 0;
            try
            {
                IList<IWebElement> lstHeaders = driver.FindElements(By.XPath("//table//thead/tr/th"));

                foreach (IWebElement itm in lstHeaders)
                {
                    ++count;
                    if (itm.Text.Equals(headerName))
                    {
                        return count - 2;
                    }
                    else
                    {
                        Console.WriteLine("header name not available");
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Assert.Warn("IndexOutOfRangeException Exception caught: {0}", e.Message);
            }
            catch (Exception e)
            {
                Assert.Warn("Exception caught: {0}", e.Message);
            }
            return idx;
        }
    }
}
