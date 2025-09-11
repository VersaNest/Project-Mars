using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll_ProjectMars.Utilities;


namespace Reqnroll_ProjectMars.Pages
{
    public class LoginPage : BasePage
    {
        By loginModal = By.CssSelector("div.ui.tiny.modal.transition.visible.active");
        By joinModal = By.XPath("//form[@class='ui large form ']");


        private readonly By emailField = By.Name("email");
        private readonly By passwordField = By.Name("password");
        private readonly By loginButton = By.XPath("//button[contains(text(),'Login')]");


        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public void signFromMain()
        {
            IWebElement signInLink = WaitUntilVisible(By.XPath("//a[contains(text(),'Sign In')]"));
            signInLink.Click();
            Assert.IsTrue(loginModalVisible(), "Login modal did not appear!");
        }

        public bool loginModalVisible(int timeoutInSeconds = 20)
        {

            try
            {
                WaitUntilVisible(loginModal);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void userLogin(string email, string password)
        {

            IWebElement emailInput = WaitUntilVisible(emailField);
            emailInput.Clear();
            if (!string.IsNullOrEmpty(email))
                emailInput.SendKeys(email);

            IWebElement passwordInput = WaitUntilVisible(passwordField);
            passwordInput.Clear();
            if (!string.IsNullOrEmpty(password))
                passwordInput.SendKeys(password);

            IWebElement loginButtonInput = WaitUntilClickable(loginButton);
            loginButtonInput.Click();

        }


        public string verifyUser()
        {
            var hiUserLocator = By.XPath("//span[contains(@class,'item ui dropdown link') and starts-with(text(),'Hi ')]");
            IWebElement hiUser = WaitUntilVisible(hiUserLocator);
            return hiUser.Text;

        }


        public string blankFieldErrorMessage()
        {
            By errorDisplayed = By.XPath("//div[contains(@class,'ui basic red pointing prompt label')]");
            IWebElement errorMessage = WaitUntilVisible(errorDisplayed);
            return errorMessage.Text;
        }

        public void clickForgotPassword()
        {
            IWebElement signInLink = WaitUntilVisible(By.XPath("//a[contains(text(),'Forgot your password?')]"));
            signInLink.Click();

        }

        public void getEmailVerificationField(string userEmail)
        {

            CloseConfirmationMessage();
            IWebElement emailField = WaitUntilClickable(By.XPath("//input[@name='email']"));
            emailField.Clear();
            emailField.SendKeys(userEmail);

        }
        public void enterVerificationEmail(string userEmail)
        {
            getEmailVerificationField(userEmail);

            IWebElement sendButton = WaitUntilClickable(By.XPath("//div[contains(text(),'SEND VERIFICATION EMAIL')]"));
            sendButton.Click();
        }

        public void enterInvalidVerification(string userEmail)
        {
            getEmailVerificationField(userEmail);
            IWebElement sendButton = WaitUntilClickable(By.XPath("//button[@id='submit-btn']"));
            sendButton.Click();
        }



        //Methods related to Join Scenarios

        public void joinFromMain()
        {
            IWebElement joinButton = WaitUntilVisible(By.XPath("//button[contains(text(),'Join')]"));
            joinButton.Click();
            Assert.IsTrue(joinModalVisible(), "Join modal did not appear!");
        }

        public bool joinModalVisible(int timeoutInSeconds = 20)
        {

            try
            {
                WaitUntilVisible(joinModal);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void newUserJoin(string firstName, string lastName, string userEmail, string userPassword)
        {
            IWebElement firstNameInput = WaitUntilVisible(By.XPath("//input[@name='firstName']"));
            firstNameInput.Clear();
            firstNameInput.SendKeys(firstName);

            IWebElement lastNameInput = WaitUntilVisible(By.XPath("//input[@name='lastName']"));
            lastNameInput.Clear();
            lastNameInput.SendKeys(lastName);

            IWebElement emailInput = WaitUntilVisible(By.XPath("//input[@name='email']"));
            emailInput.Clear();
            emailInput.SendKeys(userEmail);

            IWebElement passwordInput = WaitUntilVisible(By.XPath("//input[@name='password']"));
            passwordInput.Clear();
            passwordInput.SendKeys(userPassword);

            IWebElement confirmPasswordInput = WaitUntilVisible(By.XPath("//input[@name='confirmPassword']"));
            confirmPasswordInput.Clear();
            confirmPasswordInput.SendKeys(userPassword);

            var checkbox = driver.FindElement(By.Name("terms"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", checkbox);

            IWebElement joinButton = WaitUntilVisible(By.XPath("//div[contains(text(),'Join')]"));
            joinButton.Click();
        }

        public void CloseConfirmationMessage()
        {
            By closeButton = By.CssSelector("a.ns-close");
            WaitUntilClickable(closeButton).Click();
            WaitUntilInvisible(closeButton);
        }

        //Common methods
        public string retreiveMessage()
        {
            IWebElement displayedMessage = WaitUntilVisible(By.XPath("//div[@class='ns-box-inner']"));
            return displayedMessage.Text;
        }
    }
}
