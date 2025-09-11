Feature: SkillScenarios

This feature file contains scenarios related to the profile page where a user can manage their skills present in their profile.

Background:
    Given I am logged in with valid credentials "test@gmail.com" and "test1234"
	And I am on the Skills tab on the user profile

@ValidScenario
Scenario Outline: Add new skills with different skill levels
	When I add a new skill "<Skill>" with level "<Level>"
	Then I should see "<Skill>" displayed with the corresponding level "<Level>" in the skills tab
	

Examples:
  | Skill	      | Level        |
  | Programming   | Beginner     |
  | Automation    | Intermediate |
  | Administration| Expert       |
 


@ValidScenario
Scenario: User able to update skill name and level of any skills displayed in profile
 Given the following skills exist in the profile:
  | Skill	      | Level        |
  | Programming   |Beginner     |
  | Automation    |Intermediate |
  | Administration|Expert       |
        
  When I click the update icon near the skill "Automation" and update level to "UpdatedAutomation" with level "Expert"
  Then I should see the new skill "UpdatedAutomation" with a confirmation message


@ValidScenario
Scenario: User able to delete any skill displayed in profile
 Given the following skills exist in the profile:
  | Skill	      | Level        |
  | Programming   |Beginner     |
  | Automation    |Intermediate |
  | Administration|Expert       |
        
  When I click the delete icon near the skill "Automation"
  Then skill "Automation" should be deleted with confirmation message
  
 
@InvalidScenario
Scenario: Error message displayed while adding already existing skill
 Given the following skills exist in the profile:
  | Skill	      | Level        |
  | Programming   |Beginner     |
  | Automation    |Intermediate |
  | Administration|Expert       |
       
    When I try to add an already existing skill "Programming" with level "Expert"
    Then error should be displayed on the skill tab of profile

@InvalidScenario
Scenario Outline: Error message displayed when I leave skill name or level field blank 
  When I leave skill details <SkillName> or <Level> fields blank while adding
  Then error should be displayed to enter skill details <SkillName> and <Level>

Examples:
  | SkillName	| Level   | 
  | Testing		|		  |
  |			    | Expert  | 
  |			    |		  | 

@DestructiveScenario
Scenario Outline: User able to add skills with symbols and long characters
	When I add a new skill "<Skill>" with level "<Level>"
	Then I should see "<Skill>" displayed with the corresponding level "<Level>" in the skills tab
	

Examples:
  | Skill	      | Level        |
  | abcdgbahdjbsjd1111111jdkkkkkkkkkkkkkkkkkkkkkk   | Beginner     |
  | #$%^&!*@#@#    | Intermediate |
  | 漢字漢字漢字漢字漢字漢字| Expert       |
