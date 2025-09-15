using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll_ProjectMars.Pages;
using System;

namespace Reqnroll_ProjectMars.StepDefinitions
{
    [Binding]
    public class SkillScenariosStepDefinitions
    {

        private readonly LoginPage loginPage;
        private readonly ProfilePage profilePage;

        private string duplicateStatus;
        public SkillScenariosStepDefinitions(IWebDriver driver)
        {
            loginPage = new LoginPage(driver);
            profilePage = new ProfilePage(driver);
        }


        [Given("I am on the Skills tab on the user profile")]
        public void GivenIAmOnTheSkillsTabOnTheUserProfile()
        {
            profilePage.ClickSkillsTab();
        }

        [Given("the following skills exist in the profile:")]
        public void GivenTheFollowingSkillsExistInTheProfile(DataTable dataTable)
        {
            profilePage.ClickSkillsTab();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string skill = dataTable.Rows[i]["Skill"];
                string level = dataTable.Rows[i]["Level"];

                profilePage.AddNewSkill(skill, level);
            }
        }

        [When("I add a new skill {string} with level {string}")]
        public void WhenIAddANewSkillWithLevel(string skillName, string skillLevel)
        {
            profilePage.AddNewSkill(skillName, skillLevel);
        }

        [When("I click the update icon near the skill {string} and update level to {string} with level {string}")]
        public void WhenIClickTheUpdateIconNearTheSkillAndUpdateLevelToWithLevel(string oldSkillName, string newSkillName, string newSkillLevel)
        {
            string updatedSkill = profilePage.UpdateSkill(oldSkillName, newSkillName, newSkillLevel);
            Assert.AreEqual(newSkillName, updatedSkill, "Skill not updated in the profile");
        }


        [When("I click the delete icon near the skill {string}")]
        public void WhenIClickTheDeleteIconNearTheSkill(string skillName)
        {
            string status = profilePage.DeleteSkill(skillName);
            Assert.AreEqual("Success", status, "Skill not found in the profile to delete");
        }

        [When("I try to add an already existing skill {string} with level {string}")]
        public void WhenITryToAddAnAlreadyExistingSkillWithLevel(string skillName, string skillLevel)
        {
            duplicateStatus = profilePage.CheckDuplicateSkill(skillName, skillLevel);
            profilePage.AddNewSkill(skillName, skillLevel);
        }

        [When("I leave skill details (.*) or (.*) fields blank while adding")]
        public void WhenILeaveSkillDetailsTestingOrFieldsBlankWhileAdding(string skillName, string skillLevel)
        {
            profilePage.AddNewSkill(skillName, skillLevel);
        }


        [Then("I should see {string} displayed with the corresponding level {string} in the skills tab")]
        public void ThenIShouldSeeDisplayedWithTheCorrespondingLevelInTheSkillsTab(string skillName, string skillLevel)
        {
            string expectedMessage = $"{skillName} has been added to your skills";
            string displayedMessage = profilePage.RetreiveMessage();
            Assert.AreEqual(expectedMessage, displayedMessage, "Confirmation message does not match");

            string displayedSkillName = profilePage.VerifyDetailsDisplayed(1);
            Assert.AreEqual(skillName, displayedSkillName, "Skill Name does not match");

            string displayedSkillLevel = profilePage.VerifyDetailsDisplayed(2);
            Assert.AreEqual(skillLevel, displayedSkillLevel, "Skill levels does not match");
        }

        [Then("I should see the new skill {string} with a confirmation message")]
        public void ThenIShouldSeeTheNewSkillWithAConfirmationMessage(string newSkillName)
        {
            string expectedmessage = $"{newSkillName} has been updated to your skills";
            string displayedMessage = profilePage.RetreiveMessage();
            Assert.IsTrue(displayedMessage.Equals(expectedmessage, StringComparison.OrdinalIgnoreCase), "Confirmation message does not match");
        }


        [Then("skill {string} should be deleted with confirmation message")]
        public void ThenSkillShouldBeDeletedWithConfirmationMessage(string skillName)
        {
            string expectedmessage = $"{skillName} has been deleted";
            string displayedMessage = profilePage.RetreiveMessage();
            Assert.IsTrue(displayedMessage.Equals(expectedmessage, StringComparison.OrdinalIgnoreCase), "Confirmation message does not match");
        }

        
        [Then("error should be displayed on the skill tab of profile")]
        public void ThenErrorShouldBeDisplayedOnTheSkillTabOfProfile()
        {
            string displayedMessage = profilePage.RetreiveMessage();

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
            string displayedMessage = profilePage.RetreiveMessage();

            Assert.AreEqual(displayedMessage, "Please enter skill and experience level", "Confirmation message does not match");
        }
       
    }
}
