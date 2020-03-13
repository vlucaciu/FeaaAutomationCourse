using OpenQA.Selenium;

namespace UnitTestProject1.PageObjects
{
    public class AddressDetailsPage
    {
        private IWebDriver driver;

        public AddressDetailsPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement LblSuccess => driver.FindElement(By.CssSelector("[data-test='notice']"));
    }
}