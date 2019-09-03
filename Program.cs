using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UpnetixExam
{
    [TestFixture]
    public class Program
    {

        private IWebDriver driver;

        private string usedURL = "https://www.abv.bg/";
        private string userName = "g.lukov_test";
        private string passWord = "Test1234";
        private string myName = "GGGG LLLL";
        private string searchMenuId = "searchFieldInbox";
        private string searchStr = "thecrow";
        private string subjectOfMail = "test mail";
        private string mailBody = "mail recieved";

        [SetUp]
        public void Initialize()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArguments("--incognito");

            driver = new ChromeDriver(options);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test, Order(1)]
        public void VerifySuccessfullLogIn()
        {
            SeleniumMethods.LogInToABV(driver, usedURL, userName, passWord);

            /*Console.WriteLine(myName);
            Console.WriteLine(SeleniumMethods.GetUserNameFromMainPage(driver));

            Console.WriteLine(searchMenuId);
            Console.WriteLine(SeleniumMethods.SearchMenu(driver));*/

            Assert.AreEqual(myName, SeleniumMethods.GetUserNameFromMainPage(driver));
            Assert.AreEqual(searchMenuId, SeleniumMethods.SearchMenu(driver));

        }

        [Test, Order(2)]
        public void VerifyMailSubject()
        {
            SeleniumMethods.LogInToABV(driver, usedURL, userName, passWord);

            SeleniumMethods.PerformSearch(driver, searchStr);

            /*Console.WriteLine(subjectOfMail);
            Console.WriteLine(SeleniumMethods.MailSubject(driver));*/

            Assert.AreEqual(subjectOfMail, SeleniumMethods.GetMailSubject(driver));

        }

        [Test, Order(3)]
        public void VerifyMailIsRecieved()
        {
            SeleniumMethods.LogInToABV(driver, usedURL, userName, passWord);

            SeleniumMethods.PerformSearch(driver, searchStr);

            SeleniumMethods.OpenUnreadMail(driver);

            SeleniumMethods.ReplicationOfTheMail(driver, userName);

            SeleniumMethods.AddTextToMail(driver, mailBody);

            SeleniumMethods.SendMail(driver);

        }

    }
}
