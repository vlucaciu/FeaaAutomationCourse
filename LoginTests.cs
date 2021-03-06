﻿using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UnitTestProject1.PageObjects;

namespace UnitTestProject1
{
    [TestClass]
    public class LoginTests
    {
        private IWebDriver driver;
        private LoginPage loginPage;


        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver();
            loginPage = new LoginPage(driver);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://a.testaddressbook.com/");
            loginPage.NavigateToLoginPage();
            Thread.Sleep(1000);
        }

        [TestMethod]
        public void Login_CorrectEmail_CorrectPassword()
        {
            loginPage.LoginApplication("test@test.test", "test");

            var expectedResult = "test@test.test";
            var homePage = new HomePage(driver);

            Assert.AreEqual(expectedResult, homePage.UserEmailText);
        }

        [TestMethod]
        public void Login_IncorrectEmail_IncorrectPassword()
        {
            loginPage.LoginApplication("weor@hdsh.asdhg", "asd");

            var expectedResult = "Bad email or password.";
            var actualResults = driver.FindElement(By.XPath("//div[starts-with(@class, 'alert')]")).Text;

            Assert.AreEqual(expectedResult, actualResults);
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}