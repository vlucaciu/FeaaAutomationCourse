using OpenQA.Selenium;

namespace UnitTestProject1.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver browser)
        {
            driver = browser;
        }

        //TODO: move it in a generic class
        private IWebElement BtnSignIn()
        {
            return driver.FindElement(By.Id("sign-in"));
        }

        private IWebElement TxtUsername()
        {
            return driver.FindElement(By.Id("session_email"));
        }

        private IWebElement TxtPassword()
        {
            return driver.FindElement(By.Id("session_password"));
        }

        private IWebElement BtnLogin()
        {
            return driver.FindElement(By.Name("commit"));
        }

        public void NavigateToLoginPage()
        {
            BtnSignIn().Click();
        }

        public void LoginApplication(string username, string password)
        {
            TxtUsername().SendKeys(username);
            TxtPassword().SendKeys(password);
            BtnLogin().Click();
        }

    }
}