# forFeea
  This is where we keep track with the latest changes related to the course

Session 1: 
We have discussed about automation:
  1. What is it
  2. Why is important
  3. Types of automation testing

The presentation will be sent via email at the end of this course.

Session 2:  Let's write our first UI automation tests

Scope: This session scope was to create a unit test project, install all the dependencies and write some simple tests

How to create a unit test project:
  1. Open Visual studio
  2. Click File > New > Project
  3. Search for unit test: Unit Test Project(.NET Framework)
  4. Add project Name
  5. Click OK button
  
At this moment, we have created a unit test project and should have a default test class: UnitTest1. 
The class has the following particularities: 
  1. It has [TestClass] annotation that identifies the class to be a test one. Without this annotation, the test under this class cannot     be recognized and there for cannot run the tests within it.
  2. The class contains a test method that has a [TestMethod] annotation. This help the method to be recognized as an test method and run it accordingly.
  Find more info on: https://docs.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2019
  
Let's import the needed packages in order to open a chrome browser using our test. Do a right click on the created project and 
click Manage Nuget Packages. On the opened window, select Browse tab and search for Selenium.Webdriver and install it in your project. Then search for Selenium.Webdriver.ChromeDriver and install the latest version(avoid the ones that are in betha). 

Be aware that if the local installed Chrome is not the same with the installed package, it will trigger a compatibility error when running the test.
  
Let's write some code :) 
  
REMEMBER: THE TEST DOES WHAT YOU TELL IT TO DO. Thats why, selenium manipulates the elements in DOM as a human would do. For now, we will use click and sendkeys.
  
Our first automated test case will try to login into application. For this, we will need a write code for the next steps:
  1. Open the browser
  2. Maximize the page
  3. Open the application URL
  4. Click Sign in button
  5. Fill user email
  6. Fill user password
  7. Click Sign in button 
  8. Assert that the current user label contains our email
    
Open the UnitTest1 class and let instantiate our driver to open the Chrome browser.
  
      var driver = new ChromeDriver(); //open chrome browser
      driver.Manage().Window.Maximize(); //maximize the window
      driver.Navigate().GoToUrl("OUR URL"); //access the SUT(System Under Test) url. In our case http://a.testaddressbook.com/
  
Until now, we have covered the first 3 steps of our test. Let's complete our test:
  
      driver.FindElement(By.Id("sign-in")).Click();
      Thread.Sleep(1000);
      driver.FindElement(By.Id("session_email")).SendKeys("email that was used for creating the account");
      driver.FindElement(By.Name("session[password]")).SendKeys("password used to create the account");
      driver.FindElement(By.CssSelector("input[value='Sign in']")).Click();
      Thread.Sleep(2000);
      Assert.AreEqual("asd@asd.asd", driver.FindElement(By.XPath("//span[@data-test='current-user']")).Text);
  
Clarification :)
  1. WebElement represents a DOM element. WebElements can be found by searching from the document root using a WebDriver instance. WebDriver API provides built-in methods to find the WebElements which are based on different properties like ID, Name, Class, XPath, CSS Selectors, link Text, etc.
  2. There are some ways of optimizing our selectors used to identify the elements in page.  
      a. For the CssSelector, the simplest way is to use the following format: tagname[attribute='attributeValue'].  
      b. For the XPath, the simplest way is to use the following format: //tagname[@attribute='attributeValue'].  
    Let's take for example the Sign in button:
    
    <input type="submit" name="commit" value="Sign in" class="btn btn-primary" data-test="submit" data-disable-with="Sign in">
    
    Explained: 
    
    <input(this is the tagname) 
        type(this is the attribute)="submit"(this is the value) --> The CssSelector would be: input[type='submit']
        name(this is the attribute)="commit"(this is the value) --> The XPath would be: //input[@name='commit']
        value(this is the attribute)="Sign in"(this is the value) --> The CssSelector would be: input[value='Sign in']
        class(this is the attribute)="btn btn-primary"(this is the value) --> The XPath would be: //input[@class='btn btn-primary']
        data-test(this is the attribute)="submit"(this is the value) --> The CssSelector would be: input[data-test='submit']
        data-disable-with(this is the attribute)="Sign in"(this is the value) --> The Xpath would be: //input[data-disabled-with='Sign in']
    >
    
Session 3: Let's refine/refactor our code

Scope: This session scope was to use MSTest methods to initialize/clean up our test data and to get rid of our duplicate code

One of a test case component is the prerequisite: conditions that must be met before the test case can be run.
Our code test login scenarios and we need to see what are the prerequisites.
We have identified the following steps that need to be execute before running the test:
            var driver = new ChromeDriver(); // open the browser
            driver.Manage().Window.Maximize(); // maximize the window 
            driver.Navigate().GoToUrl("http://a.testaddressbook.com/"); // access the SUT url
            driver.FindElement(By.Id("sign-in")).Click(); // click on sign in button in order to be redirected to the login page
            Thread.Sleep(1000); // THIS sleep is a bad practice AND NEEDS TO BE DELETED AND WE WILL BURN IT WITH FIRE IN THE NEAR FUTURE

Also, after the test has finished running, we need to clean up the operations that we made in our test in order to not impact further test. Remember, each test is independent and should not influence the result of other tests. In our test, the clean up would mean to close the browser.
            driver.Quit();

MSTest provides a way to declare methods to be called by the test runner before or after running a test.

            [TestInitialize]
            public void TestInitialize()
            {
            }

            [TestCleanup]
            public void TestCleanup()
            {
            }

The method decorated by [TestInitialize] is called before running each test of the class. The method decorated by [TestCleanup] is called after running each test of the class.

First, we need to remove the init/clean up steps and to move it the according method. At this point, our tests should look like this: 

namespace UnitTestProject1
{
    [TestClass]
    public class LoginTests
    {
        //declare IWebDriver instance variable
        //use it outside of any methods so that we can use it in various methods
        private IWebDriver driver;

        [TestInitialize]
        public void SetUp()
        {
            //initialize the needed driver. In our case is ChromeDriver
            driver = new ChromeDriver();
            loginPage = new LoginPage(driver);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://a.testaddressbook.com/");
            driver.FindElement(By.Id("sign-in")).Click();
            Thread.Sleep(1000);
        }

        [TestMethod]
        public void Login_CorrectEmail_CorrectPassword()
        {
            driver.FindElement(By.Id("session_email")).SendKeys("test@test.test");
            driver.FindElement(By.Id("session_password")).SendKeys("test");
            driver.FindElement(By.Name("commit")).Click();

            var expectedResult = "test@test.test";
            var actualResults = driver.FindElement(By.CssSelector("span[data-test='current-user']")).Text;

            Assert.AreEqual(expectedResult, actualResults);
        }

        [TestMethod]
        public void Login_IncorrectEmail_IncorrectPassword()
        {
            driver.FindElement(By.Id("session_email")).SendKeys("wrong@wrong.wrong");
            driver.FindElement(By.Id("session_password")).SendKeys("wrong");
            driver.FindElement(By.Name("commit")).Click();

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

ONE OF THE COMMON MISTAKE IS TO DECLARE THE IWebDriver INSTANCE VARIABLE WITHIN THE TEST INIT:

        private IWebDriver driver;

        [TestInitialize]
        public void SetUp()
        {
            var driver = new ChromeDriver();
          //this is a method local variable and cannot be used in other methods outside init. The tests will throw null refrence since it doesn't know about the ChromeDriver instance
        }

Our code starts to look cleaner :)

But wait, there is more work to do. Let's say that the login page layout needs to be changed. Our test will fail after this changes.
We have only two tests and will be easy to fix it. But imagine that we have 25 login tests. Is not so funny to update all the tests.

A better approach to script maintenance is to create a separate class file which would find web elements, fill them or verify them. This class can be reused in all the scripts using that element. In future, if there is a change in the web element, we need to make the change in just 1 class file and not 10 different scripts.
This approach is called Page Object Model(POM). It helps make the code more readable, maintainable, and reusable.

Page Object model is an object design pattern in Selenium, where web pages are represented as classes, and the various elements on the page are defined as variables on the class. All possible user interactions can then be implemented as methods on the class.

Let's create the the page object that contains the elements for the login page: LoginPage.cs
Right click on the project > Add > Folder and name it PageObjects
Right click on the PageObjects folder > Add > New Item... > Add a class with name: LoginPage.cs
We need to add the objects that we use in our script in this class: email input, password input, sign in button and create a method to login the user.
There are multiple ways to declare the page objects:

First: create a method to return an IWebelement
      private IWebelement TxtEmail()
      {
        return driver.FindElement(By.Id("session_email"));
      }

Second: create an IWebElement property 
      private IWebElement TxtEmail => driver.FindElement(By.Id("session_email"));

Third: Extract the By separately 
      By email = By.Id("session_email")
      private IWebElement TxtEmail => driver.FindElement(email);

Our login page will look like this in the end:
      public class LoginPage
      {
          //declare the driver
          private IWebDriver driver;

          //instantiate the driver
          public LoginPage(IWebDriver browser)
          {
              driver = browser;
          }

          //create our elements
          private IWebElement Username()
          {
            return driver.FindElement(By.Id("session_email"));
          }

          private IWebElement Password()
          {
              return driver.FindElement(By.Id("session_password"));
          }

          private IWebElement LoginClick()
          {
              return driver.FindElement(By.Name("commit"));
          }

          public void LoginApplication(string username, string password)
          {
              Username.SendKeys(username);
              Password.SendKeys(password);
              LoginClick.Click();
          }
      }

At this point, we need to update our tests:

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
            driver.FindElement(By.Id("sign-in")).Click();
            Thread.Sleep(1000);
        }

        [TestMethod]
        public void Login_CorrectEmail_CorrectPassword()
        {
            loginPage.LoginApplication("test@test.test", "test");

            var expectedResult = "test@test.test";
            var actualResults = driver.FindElement(By.CssSelector("span[data-test='current-user']")).Text;

            Assert.AreEqual(expectedResult, actualResults);
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

We still have work to do. But chill out and see you at the next seminar.