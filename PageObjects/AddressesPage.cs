using OpenQA.Selenium;

namespace UnitTestProject1.PageObjects
{
    public class AddressesPage
    {
        private IWebDriver driver;

        public AddressesPage(IWebDriver browser)
        {
            driver = browser;
        }

        private IWebElement BtnNewAddress => driver.FindElement(By.CssSelector("[data-test='create']"));


        public AddAdressPage NavigateToAddAddressPage()
        {
            BtnNewAddress.Click();
            return new AddAdressPage(driver);
        }
    }
}