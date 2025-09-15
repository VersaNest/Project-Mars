using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll_ProjectMars.Utilities;


namespace Reqnroll_ProjectMars.Pages
{
    public class LoginPage
    {
        By loginModal = By.CssSelector("div.ui.tiny.modal.transition.visible.active");
        By joinModal = By.XPath("//form[@class='ui large form ']");


        private readonly By emailField = By.Name("email");
        private readonly By passwordField = By.Name("password");
        private readonly By loginButton = By.XPath("//button[contains(text(),'Login')]");


        private readonly IWebDriver newDriver;
        private readonly BasePage basePage;

        IWebElement addNewButton;

        public LoginPage(IWebDriver driver)
        {
            newDriver = driver;
            basePage = new BasePage(driver);
        }


        public void SignFromMain()
        {
            IWebElement signInLink = basePage.WaitUntilVisible(By.XPath("//a[contains(text(),'Sign In')]"));
            signInLink.Click();
            Assert.IsTrue(LoginModalVisible(), "Login modal did not appear!");
        }

        public bool LoginModalVisible(int timeoutInSeconds = 20)
        {

            try
            {
                basePage.WaitUntilVisible(loginModal);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void UserLogin(string email, string password)
        {

            IWebElement emailInput = basePage.WaitUntilVisible(emailField);
            emailInput.Clear();
            if (!string.IsNullOrEmpty(email))
                emailInput.SendKeys(email);

            IWebElement passwordInput = basePage.WaitUntilVisible(passwordField);
            passwordInput.Clear();
            if (!string.IsNullOrEmpty(password))
                passwordInput.SendKeys(password);

            IWebElement loginButtonInput = basePage.WaitUntilClickable(loginButton);
            loginButtonInput.Click();

        }


        public string VerifyUser()
        {
            var hiUserLocator = By.XPath("//span[contains(@class,'item ui dropdown link') and starts-with(text(),'Hi ')]");
            IWebElement hiUser = basePage.WaitUntilVisible(hiUserLocator);
            return hiUser.Text;

        }


        public string BlankFieldErrorMessage()
        {
            By errorDisplayed = By.XPath("//div[contains(@class,'ui basic red pointing prompt label')]");
            IWebElement errorMessage = basePage.WaitUntilVisible(errorDisplayed);
            return errorMessage.Text;
        }

        public void ClickForgotPassword()
        {
            IWebElement signInLink = basePage.WaitUntilVisible(By.XPath("//a[contains(text(),'Forgot your password?')]"));
            signInLink.Click();

        }

        public void GetEmailVerificationField(string userEmail)
        {

            CloseConfirmationMessage();
            IWebElement emailField = basePage.WaitUntilClickable(By.XPath("//input[@name='email']"));
            emailField.Clear();
            emailField.SendKeys(userEmail);

        }
        public void EnterVerificationEmail(string userEmail)
        {
            GetEmailVerificationField(userEmail);

            IWebElement sendButton = basePage.WaitUntilClickable(By.XPath("//div[contains(text(),'SEND VERIFICATION EMAIL')]"));
            sendButton.Click();
        }

        public void EnterInvalidVerification(string userEmail)
        {
            GetEmailVerificationField(userEmail);
            IWebElement sendButton = basePage.WaitUntilClickable(By.XPath("//button[@id='submit-btn']"));
            sendButton.Click();
        }



        //Methods related to Join Scenarios

        public void JoinFromMain()
        {
            IWebElement joinButton = basePage.WaitUntilVisible(By.XPath("//button[contains(text(),'Join')]"));
            joinButton.Click();
            Assert.IsTrue(JoinModalVisible(), "Join modal did not appear!");
        }

        public bool JoinModalVisible(int timeoutInSeconds = 20)
        {

            try
            {
                basePage.WaitUntilVisible(joinModal);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void NewUserJoin(string firstName, string lastName, string userEmail, string userPassword)
        {
            IWebElement firstNameInput = basePage.WaitUntilVisible(By.XPath("//input[@name='firstName']"));
            firstNameInput.Clear();
            firstNameInput.SendKeys(firstName);

            IWebElement lastNameInput = basePage.WaitUntilVisible(By.XPath("//input[@name='lastName']"));
            lastNameInput.Clear();
            lastNameInput.SendKeys(lastName);

            IWebElement emailInput = basePage.WaitUntilVisible(By.XPath("//input[@name='email']"));
            emailInput.Clear();
            emailInput.SendKeys(userEmail);

            IWebElement passwordInput = basePage.WaitUntilVisible(By.XPath("//input[@name='password']"));
            passwordInput.Clear();
            passwordInput.SendKeys(userPassword);

            IWebElement confirmPasswordInput = basePage.WaitUntilVisible(By.XPath("//input[@name='confirmPassword']"));
            confirmPasswordInput.Clear();
            confirmPasswordInput.SendKeys(userPassword);

            var checkbox = newDriver.FindElement(By.Name("terms"));
            ((IJavaScriptExecutor)newDriver).ExecuteScript("arguments[0].click();", checkbox);

            IWebElement joinButton = basePage.WaitUntilVisible(By.XPath("//div[contains(text(),'Join')]"));
            joinButton.Click();
        }

        public void CloseConfirmationMessage()
        {
            By closeButton = By.CssSelector("a.ns-close");
            basePage.WaitUntilClickable(closeButton).Click();
            basePage.WaitUntilInvisible(closeButton);
        }

        //Common methods
        public string RetreiveMessage()
        {
            IWebElement displayedMessage = basePage.WaitUntilVisible(By.XPath("//div[@class='ns-box-inner']"));
            return displayedMessage.Text;
        }
    }
}
