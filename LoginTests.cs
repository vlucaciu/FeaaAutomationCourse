using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UnitTestProject1
{
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        public void Login_CorrectEmail_CorrectPassword()
        {
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("http://a.testaddressbook.com/");

            driver.FindElement(By.Id("sign-in")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("session_email")).SendKeys("test@test.test");
            driver.FindElement(By.Id("session_password")).SendKeys("test");
            driver.FindElement(By.Name("commit")).Click();

            var expectedResult = "test@test.test";
            var actualResults = driver.FindElement(By.CssSelector("span[data-test='current-user']")).Text;

            Assert.AreEqual(expectedResult, actualResults);

            driver.Quit();
        }

        [TestMethod]
        public void Login_IncorrectEmail_IncorrectPassword()
        {
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("http://a.testaddressbook.com/");

            driver.FindElement(By.Id("sign-in")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("session_email")).SendKeys("wrong@wrong.wrong");
            driver.FindElement(By.Id("session_password")).SendKeys("wrong");
            driver.FindElement(By.Name("commit")).Click();

            var expectedResult = "Bad email or password.";
            var actualResults = driver.FindElement(By.XPath("//div[starts-with(@class, 'alert')]")).Text;

            Assert.AreEqual(expectedResult, actualResults);

            driver.Quit();
        }

        [TestMethod]
        public void Login_CorrectEmail_IncorrectPassword()
        {
        }
    }
}