using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1.PageObjects
{
    public class AddAdressPage
    {
        private IWebDriver driver;

        public AddAdressPage(IWebDriver browser)
        {
            driver = browser;
        }

        private IWebElement TxtFirstName => driver.FindElement(By.Id("address_first_name"));

        private IWebElement TxtLastName => driver.FindElement(By.Id("address_last_name"));

        private IWebElement TxtAddress1 => driver.FindElement(By.Id("address_street_address"));

        private IWebElement TxtCity => driver.FindElement(By.Id("address_city"));

        private IWebElement DdlState => driver.FindElement(By.Id("address_state"));

        private IWebElement TxtZipCode => driver.FindElement(By.Id("address_zip_code"));

        private IList<IWebElement> LstCountry => driver.FindElements(By.CssSelector("input[type=radio]"));

        private IWebElement TxtBirthday => driver.FindElement(By.Id("address_birthday"));

        private IWebElement ClColor => driver.FindElement(By.Id("address_color"));

        private IWebElement BtnSave => driver.FindElement(By.Name("commit"));

        public void AddAddress()
        {
            TxtFirstName.SendKeys("test");
            TxtLastName.SendKeys("test");
            TxtAddress1.SendKeys("test");
            TxtCity.SendKeys("test");
            var selectState = new SelectElement(DdlState);
            selectState.SelectByText("Hawaii");
            TxtZipCode.SendKeys("test");
            LstCountry[1].Click();

            var js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("arguments[0].setAttribute('value', arguments[1])", ClColor, "#FF0000");
            BtnSave.Click();
            Thread.Sleep(2000);
        }

    }
}