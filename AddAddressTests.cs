using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UnitTestProject1.PageObjects;

namespace UnitTestProject1
{
    [TestClass]
    public class AddAddressTests
    {
        private IWebDriver driver;
        private AddAdressPage addAddressPage;

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
            var loginPage = new LoginPage(driver);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://a.testaddressbook.com/");
            loginPage.NavigateToLoginPage();
            Thread.Sleep(1000);
            loginPage.LoginApplication("test@test.test", "test");

            var homePage = new HomePage(driver);
            Thread.Sleep(1000);
            var addressesPage = homePage.NavigateToAddressesPage();
            Thread.Sleep(1000);
            addAddressPage = addressesPage.NavigateToAddAddressPage();
            Thread.Sleep(1000);
        }

        [TestMethod]
        public void Go_To_AddAddressPage()
        {
            addAddressPage.AddAddress();
            var addressDetails = new AddressDetailsPage(driver);
            var message = "Address was successfully created.";
            Assert.AreEqual(message, addressDetails.LblSuccess.Text);
        }


        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}