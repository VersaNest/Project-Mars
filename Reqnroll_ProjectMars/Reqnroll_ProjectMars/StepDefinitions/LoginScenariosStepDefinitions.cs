using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll_ProjectMars.Pages;

namespace Reqnroll_ProjectMars.StepDefinitions
{
    [Binding]
    public class LoginScenariosStepDefinitions
    {
        private readonly IWebDriver newDriver;
        private readonly LoginPage loginPage;
        List<Cookie> cookies;

        public LoginScenariosStepDefinitions(IWebDriver driver)
        {
            newDriver = driver;
            loginPage = new LoginPage(newDriver);
        }


        [Given("I open the login modal from main page")]
        public void GivenIOpenTheLoginModalFromMainPage()
        {
            loginPage.SignFromMain();
        }



        [When("I enter valid email {string} and password {string} to login")]
        public void WhenIEnterValidEmailAndPasswordToLogin(string userEmail, string userPassword)
        {
            loginPage.UserLogin(userEmail, userPassword);
        }


        [When("I leave the {string} and {string} fields blank")]
        public void WhenILeaveTheAndFieldsBlank(string email, string password)
        {
            loginPage.UserLogin(email, password);
        }



        [When("I click forgot passwork link")]
        public void WhenIClickForgotPassworkLink()
        {
            loginPage.ClickForgotPassword();

        }




        [When("I enter an invalid {string} or {string} in the fields")]
        public void WhenIEnterAnInvalidOrInTheFields(string userEmail, string userPassword)
        {
            loginPage.UserLogin(userEmail, userPassword);
        }

        [Then("I should be able to enter {string} for verification")]
        public void ThenIShouldBeAbleToEnterForVerification(string validEmail)
        {
            loginPage.EnterInvalidVerification(validEmail);
        }


        [Then("confirmation message should be displayed that email has been sent")]
        public void ThenConfirmationMessageShouldBeDisplayedThatEmailHasBeenSent()
        {
            string displayedMessage = loginPage.RetreiveMessage();
            Assert.AreEqual("Email Verification Sent", displayedMessage, "Confirmation message for verification email not correct");

        }



        [Then("Profile page of user should be displayed with first name {string}")]
        public void ThenProfilePageOfUserShouldBeDisplayedWithFirstName(string firstName)
        {
            string loggedUser = loginPage.VerifyUser();
            Assert.That(loggedUser.Contains(firstName), "Correct user not logged in");

        }




        [Then("Error message for blank {string} and {string} should be displayed")]
        public void ThenErrorMessageForBlankAndShouldBeDisplayed(string email, string password)
        {
            string displayedError = loginPage.BlankFieldErrorMessage();

            if (string.IsNullOrWhiteSpace(email))
            {

                Assert.AreEqual("Please enter a valid email address", displayedError, "Email error message does not match");
            }
            else if (string.IsNullOrWhiteSpace(password))
            {

                Assert.AreEqual("Password must be at least 6 characters", displayedError, "Password error message does not match");
            }
            else
            {
                string displayedEmailError = loginPage.BlankFieldErrorMessage();
                Assert.AreEqual("Please enter a valid email address", displayedEmailError, "Email error message does not match");
                string displayedPasswordError = loginPage.BlankFieldErrorMessage();
                Assert.AreEqual("Password must be at least 6 characters", displayedPasswordError, "Password error message does not match");
            }
        }



        [Then("Confirmation message should be displayed that email sent")]
        public void ThenConfirmationMessageShouldBeDisplayedThatEmailSent()
        {
            string displayedMessage = loginPage.RetreiveMessage();
            Assert.AreEqual("Please check your email to reset your password", displayedMessage, "Confirmation message for verification email not correct");


        }

        [Then("I should be able to enter (.*) (.*) for verification")]
        public void ThenIShouldBeAbleToEnterYesTestGmail_ComForVerification(string valid, string userEmail)
        {
            loginPage.EnterVerificationEmail(userEmail);
        }

        [Then("confirmation message should be displayed that email sent for only (.*) user (.*)")]
        public void ThenConfirmationMessageShouldBeDisplayedThatEmailSentForOnlyYesUserTestGmail_Com(string valid, string userEmail)
        {
            string displayedMessage = loginPage.RetreiveMessage();

            if (valid.Equals("Yes") && !string.IsNullOrWhiteSpace(userEmail))
            {
                Assert.AreEqual("Please check your email to reset your password", displayedMessage, "Confirmation message for verification email not correct");
            }

            else if (valid.Equals("No") && !string.IsNullOrWhiteSpace(userEmail))
            {
                Assert.AreEqual("Unable to reset password. Invalid email or token has expired.", displayedMessage, "Displayed error message not correct");
            }
            else if (valid.Equals("No") && string.IsNullOrWhiteSpace(userEmail))
            {
                Assert.AreEqual("Field cannot be empty", displayedMessage, "Displayed error message for verification email not correct");
            }

        }



        // Test related to Join Scenarios

        [Given("I open the join modal from main page")]
        public void GivenIOpenTheJoinModalFromMainPage()
        {
            loginPage.JoinFromMain();
        }


        [When("I enter valid user details {string} , {string} , {string}, {string} in join form")]
        public void WhenIEnterValidUserDetailsInJoinForm(string newUserEmail, string newUserPassword, string newUserFirstName, string newUserLastName)
        {
            loginPage.NewUserJoin(newUserFirstName, newUserLastName, newUserEmail, newUserPassword);
        }

        [Then("I should be able to login successfully using the new {string} and {string}")]
        public void ThenIShouldBeAbleToLoginSuccessfullyUsingTheNewEmailAndPassword(string userEmail, string userPassword)
        {
            loginPage.SignFromMain();
            loginPage.UserLogin(userEmail, userPassword);

        }


        [Then("Confirmation message should be displayed for succesful account creation")]
        public void ThenConfirmationMessageShouldBeDisplayedForSuccesfulAccountCreation()
        {
            string displayedMessage = loginPage.RetreiveMessage();
            Assert.AreEqual("Registration successful, Please verify your email!", displayedMessage, "Confirmation message for new user join not correct");
            loginPage.CloseConfirmationMessage();
        }



    }
}
