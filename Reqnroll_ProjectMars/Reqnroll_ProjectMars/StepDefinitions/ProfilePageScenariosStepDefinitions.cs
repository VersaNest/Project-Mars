using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll_ProjectMars.Pages;

namespace Reqnroll_ProjectMars.StepDefinitions
{
    [Binding]
    public class ProfilePageScenariosStepDefinitions
    {

        private readonly LoginPage loginPage;
        private readonly ProfilePage profilePage;

        private string duplicateStatus;

        public ProfilePageScenariosStepDefinitions(IWebDriver driver)
        {

            loginPage = new LoginPage(driver);
            profilePage = new ProfilePage(driver);
        }

        [Given("I am logged in with valid credentials {string} and {string}")]
        public void GivenIAmLoggedInWithValidCredentialsAnd(string userEmail, string userPassword)
        {

            loginPage.signFromMain();
            loginPage.userLogin(userEmail, userPassword);
        }


        
        //Steps for scenarios related to language

        [Given("I am on the languages tab of the profile")]
        public void GivenIAmOnTheLanguagesTabOfTheProfile()
        {
            profilePage.clickLanguageTab();
        }



        [Given("the following languages exist in the profile:")]
        public void GivenTheFollowingLanguagesExistInTheProfile(DataTable dataTable)
        {
            
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string language = dataTable.Rows[i]["Language"];
                string level = dataTable.Rows[i]["Level"];

                profilePage.addNewLanguages(language, level);
            }
        }


        [When("I add a new language {string} with level {string}")]
        public void WhenIAddANewLanguageWithLevel(string languageName, string languageLevel)
        {
            profilePage.addNewLanguages(languageName, languageLevel);
        }



        [When("I click the update icon near the language {string} and update it to {string} with level {string}")]
        public void WhenIClickTheUpdateIconNearTheLanguageAndUpdateItToWithLevel(string oldName, string updatedName, string updatedLevel)
        {
            string updatedLanguage = profilePage.updateLanguage(oldName, updatedName, updatedLevel);
            Assert.AreEqual(updatedName, updatedLanguage, "Language not updated in the profile");
        }



        [When("I click the delete icon near the language level of {string}")]
        public void WhenIClickTheDeleteIconNearTheLanguageLevelOf(string languageName)
        {
            string status = profilePage.deleteLanguage(languageName);
            Assert.AreEqual("Success", status, "Language not found in the profile to delete");
        }


        [When("I try to add an already existing language {string} with level {string}")]
        public void WhenITryToAddAnAlreadyExistingLanguageWithLevel(string languageName, string languageLevel)
        {
            duplicateStatus = profilePage.checkDuplicateLanguage(languageName, languageLevel);
            profilePage.addNewLanguages(languageName, languageLevel);
        }


        [When("I leave (.*) or (.*) field blank while adding")]
        public void WhenILeaveLanguageNameOrFieldBlankWhileAdding(string languageName, string languageLevel)
        {
            profilePage.addNewLanguages(languageName, languageLevel);
        }




        [Then("I should see {string} displayed with the corresponding level {string} in the languages tab")]
        public void ThenIShouldSeeDisplayedWithTheCorrespondingLevelInTheLanguagesTab(string languageName, string languageLevel)
        {
            string expectedMessage = $"{languageName} has been added to your languages";
            string displayedMessage = profilePage.retreiveMessage();
            Assert.AreEqual(expectedMessage, displayedMessage, "Confirmation message does not match");

            string displayedLanguageName = profilePage.verifyDetailsDisplayed(1);
            Assert.AreEqual(languageName, displayedLanguageName, "Languages does not match");

            string displayedLanguageLevel = profilePage.verifyDetailsDisplayed(2);
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
            string displayedMessage = profilePage.retreiveMessage();
            Assert.IsTrue(displayedMessage.Equals(expectedmessage, StringComparison.OrdinalIgnoreCase), "Confirmation message does not match");
        }



        [Then("language {string} should be deleted with confirmation message")]
        public void ThenLanguageShouldBeDeletedWithConfirmationMessage(string languageName)
        {

            string expectedmessage = $"{languageName} has been deleted from your languages";
            string displayedMessage = profilePage.retreiveMessage();
            Assert.IsTrue(displayedMessage.Equals(expectedmessage, StringComparison.OrdinalIgnoreCase), "Confirmation message does not match");
        }




        [Then("error should be displayed on the profile avoiding addition")]
        public void ThenErrorShouldBeDisplayedOnTheProfileAvoidingAddition()
        {

            string displayedMessage = profilePage.retreiveMessage();

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
        public void ThenErrorShouldBeDisplayedToEnterDetailsLanguageNameAnd(string languageName, string languageLevel)
        {
            string displayedMessage = profilePage.retreiveMessage();

            Assert.AreEqual(displayedMessage, "Please enter language and level", "Confirmation message does not match");
        }



        //Steps related to scenarios related to skills



        [Given("I am on the skills tab of the profile")]
        public void GivenIAmOnTheSkillsTabOfTheProfile()
        {
            profilePage.clickSkillsTab();

        }

        [Given("the following skills exist in the profile:")]
        public void GivenTheFollowingSkillsExistInTheProfile(DataTable dataTable)
        {
            profilePage.clickSkillsTab();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string skill = dataTable.Rows[i]["Skill"];
                string level = dataTable.Rows[i]["Level"];

                profilePage.addNewSkill(skill, level);
            }
        }



        [When("I add a new skill {string} with level {string}")]
        public void WhenIAddANewSkillWithLevel(string skillName, string skillLevel)
        {
            profilePage.addNewSkill(skillName, skillLevel);
        }

        [When("I click the update icon near the skill {string} and update level to {string} with level {string}")]
        public void WhenIClickTheUpdateIconNearTheSkillAndUpdateLevelToWithLevel(string oldSkillName, string newSkillName, string newSkillLevel)
        {
            string updatedSkill = profilePage.updateSkill(oldSkillName, newSkillName, newSkillLevel);
            Assert.AreEqual(newSkillName, updatedSkill, "Skill not updated in the profile");
        }


        [When("I click the delete icon near the skill {string}")]
        public void WhenIClickTheDeleteIconNearTheSkill(string skillName)
        {
            string status = profilePage.deleteSkill(skillName);
            Assert.AreEqual("Success", status, "Skill not found in the profile to delete");
        }



        [When("I try to add an already existing skill {string} with level {string}")]
        public void WhenITryToAddAnAlreadyExistingSkillWithLevel(string skillName, string skillLevel)
        {


            duplicateStatus = profilePage.checkDuplicateSkill(skillName, skillLevel);
            profilePage.addNewSkill(skillName, skillLevel);
        }


        [When("I leave skill details (.*) or (.*) fields blank while adding")]
        public void WhenILeaveSkillDetailsTestingOrFieldsBlankWhileAdding(string skillName, string skillLevel)
        {
            profilePage.addNewSkill(skillName, skillLevel);
        }



        [Then("I should see {string} displayed with the corresponding level {string} in the skills tab")]
        public void ThenIShouldSeeDisplayedWithTheCorrespondingLevelInTheSkillsTab(string skillName, string skillLevel)
        {
            string expectedMessage = $"{skillName} has been added to your skills";
            string displayedMessage = profilePage.retreiveMessage();
            Assert.AreEqual(expectedMessage, displayedMessage, "Confirmation message does not match");

            string displayedSkillName = profilePage.verifyDetailsDisplayed(1);
            Assert.AreEqual(skillName, displayedSkillName, "Skill Name does not match");

            string displayedSkillLevel = profilePage.verifyDetailsDisplayed(2);
            Assert.AreEqual(skillLevel, displayedSkillLevel, "Skill levels does not match");
        }

        [Then("I should see the new skill {string} with a confirmation message")]
        public void ThenIShouldSeeTheNewSkillWithAConfirmationMessage(string newSkillName)
        {
            string expectedmessage = $"{newSkillName} has been updated to your skills";
            string displayedMessage = profilePage.retreiveMessage();
            Assert.IsTrue(displayedMessage.Equals(expectedmessage, StringComparison.OrdinalIgnoreCase), "Confirmation message does not match");
        }


        [Then("skill {string} should be deleted with confirmation message")]
        public void ThenSkillShouldBeDeletedWithConfirmationMessage(string skillName)
        {

            string expectedmessage = $"{skillName} has been deleted";
            string displayedMessage = profilePage.retreiveMessage();
            Assert.IsTrue(displayedMessage.Equals(expectedmessage, StringComparison.OrdinalIgnoreCase), "Confirmation message does not match");
        }


        [Then("error should be displayed on the skill tab of profile")]
        public void ThenErrorShouldBeDisplayedOnTheSkillTabOfProfile()
        {
            string displayedMessage = profilePage.retreiveMessage();

            if (duplicateStatus.Equals("Both duplicate", StringComparison.OrdinalIgnoreCase))
            {

                Assert.AreEqual(displayedMessage, "This skill is already exist in your skill list.", "Confirmation message does not match");

            }
            else if (duplicateStatus.Equals("Name duplicate", StringComparison.OrdinalIgnoreCase))
            {
                Assert.AreEqual(displayedMessage, "Duplicated data", "Confirmation message does not match");
            }
        }


        [Then("error should be displayed to enter skill details (.*) and (.*)")]
        public void ThenErrorShouldBeDisplayedToEnterSkillDetailsTestingAnd(string skillName, string skillLevel)
        {
            string displayedMessage = profilePage.retreiveMessage();

            Assert.AreEqual(displayedMessage, "Please enter skill and experience level", "Confirmation message does not match");
        }
    }
}
