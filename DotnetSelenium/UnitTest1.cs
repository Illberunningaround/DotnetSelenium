using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DotnetSelenium
{
    public class LoginTests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Structure", "NUnit1032:An IDisposable field/property should be Disposed in a TearDown method", Justification = "<Pending>")]
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        { //Set up Chrome driver
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://crusader.bransys.com/#/");
        }

        [Test]
        public void TestPresenceOfLoginFields()
        {
            //Check presence of username and password fields.
            Assert.IsTrue(driver.FindElement(By.XPath("//input[@id='input-204']")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//input[@id='input-207']")).Displayed);
        }

        [Test]
        public void TestInputDataValidation() 
        { 
            //Input data values into username and pasword fields
            IWebElement usernameField = driver.FindElement(By.XPath("//input[@id='input-204']"));
            usernameField.SendKeys("damjan_josifovski@yahoo.com");

            IWebElement passwordField = driver.FindElement(By.XPath("//input[@id='input-207']"));
            passwordField.SendKeys("passworddamjan");

            //Verify the entered data

            Assert.AreEqual("damjan_josifovski@yahoo.com",usernameField.GetAttribute("value"));
            Assert.AreEqual("passworddamjan", passwordField.GetAttribute("value"));

        }
        [Test]
        public void TestErrorMessage()
        {
            //Input invalid credentials
            IWebElement usernameField = driver.FindElement(By.XPath("//input[@id='input-204']"));
            usernameField.SendKeys("damjan_josifovski@yahoo.com");

            IWebElement passwordField = driver.FindElement(By.Id("//input[@id='input-207']"));
            passwordField.SendKeys("passworddamjan");

            //Submit the Form
            IWebElement logginButton = driver.FindElement(By.XPath("//button[@class='primary v-btn v-btn--is-elevated v-btn--has-bg theme--light v-size--default']//child::span"));
            logginButton.Click();

            //Check for the error message
            IWebElement errorMessage = driver.FindElement(By.XPath("//div[@class='red--text text-center col col-12']"));
            Assert.IsTrue(errorMessage.Displayed);
            Assert.AreEqual("Incorrect email/username or password", errorMessage.Text);

            [TearDown]

             void TearDown()
            {
                driver.Quit();
            }

        }
    }
}