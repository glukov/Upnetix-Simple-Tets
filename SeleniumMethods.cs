using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace UpnetixExam
{
    class SeleniumMethods
    {
        public static void LogInToABV(IWebDriver driver, string usedURL, string userName, string passWord)
        {
            driver.Navigate().GoToUrl(usedURL);

            SeleniumMethods.CloseGDPRiFrame(driver);

            driver.SwitchTo().DefaultContent();

            driver.FindElement(By.Id("username")).SendKeys(userName);

            driver.FindElement(By.Id("password")).SendKeys(passWord);

            driver.FindElement(By.Id("loginBut")).Click();

        }
        
        public static string GetUserNameFromMainPage(IWebDriver driver)
        {
            IWebElement userName = driver.FindElement(By.CssSelector("#middlePagePanel .h1 .userName"));

            return userName.Text;
        }

        public static string SearchMenu(IWebDriver driver)
        {
            IWebElement searchMenu = driver.FindElement(By.CssSelector("#searchFieldInbox"));

            return searchMenu.GetAttribute("Id");
        }

        public static void PerformSearch(IWebDriver driver, string searchStr)
        {
            IWebElement searchMenu = driver.FindElement(By.CssSelector("#searchFieldInbox"));

            searchMenu.SendKeys(searchStr);

            driver.FindElement(By.CssSelector("#searchlens")).Click();

        }

        public static string GetMailSubject(IWebDriver driver)
        {
            IWebElement mailSubject = driver.FindElement(By.CssSelector("#inboxTable td:nth-child(5)"));

            return mailSubject.Text;
        }

        public static void OpenUnreadMail(IWebDriver driver)
        {
            driver.FindElement(By.CssSelector("#inboxTable td:nth-child(5)")).Click();
        }

        public static void ReplicationOfTheMail(IWebDriver driver, string userName)
        {
            driver.FindElement(By.CssSelector(".abv-letterLinksHolder>div:nth-child(1)")).Click();

            SeleniumMethods.AddCopyToTheReplication(driver, userName);
        }

        public static void AddCopyToTheReplication(IWebDriver driver, string userName)
        {
            driver.FindElement(By.CssSelector(".ccBccButtons>div:nth-child(1)")).Click();

            driver.FindElement(By.CssSelector("tr:nth-child(3)>td.clientField>div>input")).SendKeys(userName + "@abv.bg");
        }

        public static void AddTextToMail(IWebDriver driver, string mailBody)
        {
            driver.FindElement(By.CssSelector("iframe.gwt-RichTextArea")).SendKeys(mailBody);
        }

        public static void SendMail(IWebDriver driver)
        {
            driver.FindElement(By.CssSelector("div.sendMenuContent>div:nth-child(1)")).Click();
        }

        private static void CloseGDPRiFrame(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));            

            driver.SwitchTo().Frame("abv-GDPR-frame");

            wait.Until((w) => w.FindElement(By.Id("cmp-faktor-io")));

            int IsElementExist = driver.FindElements(By.Id("cmp-faktor-io")).Count;
           
            if (IsElementExist == 1)
            {
                driver.SwitchTo().Frame("cmp-faktor-io");

                wait.Until((w) => w.FindElement(By.CssSelector(".theme-wrapper .actions")));

                driver.FindElement(By.CssSelector(".theme-wrapper .actions")).Click();
            }
            else
            {
                return;
            }
        }

    }
}
