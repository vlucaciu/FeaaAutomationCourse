using OpenQA.Selenium;

namespace UnitTestProject1.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver browser)
        {
            driver = browser;
        }

        private IWebElement LblUserEmail => driver.FindElement(By.CssSelector("span[data-test='current-user']"));

        private IWebElement BtnAddresses => driver.FindElement(By.CssSelector("[data-test='addresses']"));

        public string UserEmailText => LblUserEmail.Text;

        public AddressesPage NavigateToAddressesPage()
        {
            BtnAddresses.Click();
            return new AddressesPage(driver);
        }
    }
}