using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll_ProjectMars.Pages;
using System;

namespace Reqnroll_ProjectMars.StepDefinitions
{
    [Binding]
    public class LanguageScenariosStepDefinitions
    {
        private readonly LoginPage loginPage;
        private readonly ProfilePage profilePage;

        private string duplicateStatus;
        public LanguageScenariosStepDefinitions(IWebDriver driver) 
        {
            loginPage = new LoginPage(driver);
            profilePage = new ProfilePage(driver);
        }
        
        
        [Given("I am logged in with valid credentials {string} and {string}")]
        public void GivenIAmLoggedInWithValidCredentialsAnd(string userEmail, string userPassword)
        {
            loginPage.SignFromMain();
            loginPage.UserLogin(userEmail, userPassword);
        }

        [Given("I am on the Languages tab on the user profile")]
        public void GivenIAmOnTheLanguagesTabOnTheUserProfile()
        {
            profilePage.ClickLanguageTab();
        }


        [Given("the following languages exist in the profile:")]
        public void GivenTheFollowingLanguagesExistInTheProfile(DataTable dataTable)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string language = dataTable.Rows[i]["Language"];
                string level = dataTable.Rows[i]["Level"];

                profilePage.AddNewLanguages(language, level);
            }
        }


        [When("I add a new language {string} with level {string}")]
        public void WhenIAddANewLanguageWithLevel(string languageName, string languageLevel)
        {
            profilePage.AddNewLanguages(languageName, languageLevel);
        }


        [When("I click the update icon near the language {string} and update it to {string} with level {string}")]
        public void WhenIClickTheUpdateIconNearTheLanguageAndUpdateItToWithLevel(string oldName, string updatedName, string updatedLevel)
        {
            string updatedLanguage = profilePage.UpdateLanguage(oldName, updatedName, updatedLevel);
            Assert.AreEqual(updatedName, updatedLanguage, "Language not updated in the profile");
        }

        [When("I click the delete icon near the language level of {string}")]
        public void WhenIClickTheDeleteIconNearTheLanguageLevelOf(string languageName)
        {
            string status = profilePage.DeleteLanguage(languageName);
            Assert.AreEqual("Success", status, "Language not found in the profile to delete");
        }

        [When("I try to add an already existing language {string} with level {string}")]
        public void WhenITryToAddAnAlreadyExistingLanguageWithLevel(string languageName, string languageLevel)
        {
            duplicateStatus = profilePage.CheckDuplicateLanguage(languageName, languageLevel);
            profilePage.AddNewLanguages(languageName, languageLevel);
        }

        [When("I leave (.*) or (.*) field blank while adding")]
        public void WhenILeaveLanguageNameOrFieldBlankWhileAdding(string languageName, string languageLevel)
        {
            profilePage.AddNewLanguages(languageName, languageLevel);
        }

        [Then("I should see {string} displayed with the corresponding level {string} in the languages tab")]
        public void ThenIShouldSeeDisplayedWithTheCorrespondingLevelInTheLanguagesTab(string languageName, string languageLevel)
        {
            string expectedMessage = $"{languageName} has been added to your languages";
            string displayedMessage = profilePage.RetreiveMessage();
            Assert.AreEqual(expectedMessage, displayedMessage, "Confirmation message does not match");

            string displayedLanguageName = profilePage.VerifyDetailsDisplayed(1);
            Assert.AreEqual(languageName, displayedLanguageName, "Languages does not match");

            string displayedLanguageLevel = profilePage.VerifyDetailsDisplayed(2);
            Assert.AreEqual(languageLevel, displayedLanguageLevel, "Language levels does not match");
        }

        [Then("the ADD NEW button should (.*) after adding language")]
        public void ThenTheADDNEWButtonShouldYesAfterAddingLanguage(string visibility)
        {
            if (visibility.Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                Assert.IsTrue(profilePage.IsAddButtonVisible(), "Add New button is not present");

            }
            else if (visibility.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                Assert.IsTrue(profilePage.IsAddButtonNotVisible(), "Expected ADD NEW button to be hidden, but it was visible.");
            }
        }

        [Then("I should see the language updated as {string} with a confirmation message")]
        public void ThenIShouldSeeTheLanguageUpdatedAsWithAConfirmationMessage(string updatedName)
        {
            string expectedmessage = $"{updatedName} has been updated to your languages";
            string displayedMessage = profilePage.RetreiveMessage();
            Assert.IsTrue(displayedMessage.Equals(expectedmessage, StringComparison.OrdinalIgnoreCase), "Confirmation message does not match");
        }

        
        [Then("language {string} should be deleted with confirmation message")]
        public void ThenLanguageShouldBeDeletedWithConfirmationMessage(string languageName)
        {
            string expectedmessage = $"{languageName} has been deleted from your languages";
            string displayedMessage = profilePage.RetreiveMessage();
            Assert.IsTrue(displayedMessage.Equals(expectedmessage, StringComparison.OrdinalIgnoreCase), "Confirmation message does not match");
        }

        

        [Then("error should be displayed on the profile avoiding addition")]
        public void ThenErrorShouldBeDisplayedOnTheProfileAvoidingAddition()
        {
            string displayedMessage = profilePage.RetreiveMessage();

            if (duplicateStatus.Equals("Both duplicate", StringComparison.OrdinalIgnoreCase))
            {

                Assert.AreEqual(displayedMessage, "This language is already exist in your language list.", "Confirmation message does not match");

            }
            else if (duplicateStatus.Equals("Name duplicate", StringComparison.OrdinalIgnoreCase))
            {
                Assert.AreEqual(displayedMessage, "Duplicated data", "Confirmation message does not match");
            }
        }

       
               

        [Then("error should be displayed to enter details (.*) and (.*)")]
        public void ThenErrorShouldBeDisplayedToEnterDetailsSpanishAnd(string languageName, string languageLevel)
        {
            string displayedMessage = profilePage.RetreiveMessage();

            Assert.AreEqual(displayedMessage, "Please enter language and level", "Confirmation message does not match");
        }




    }
}
