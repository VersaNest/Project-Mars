Feature: Profile Page Scenarios

This feature file contains scenarios related to the user profile pafe where a user can manage their languages and skills.

Background:
    Given I am logged in with valid credentials "test@gmail.com" and "test1234"

@ValidScenario
Scenario Outline: Add four new languages with different language levels
	When I add a new language "<Language>" with level "<Level>"
	Then I should see "<Language>" displayed with the corresponding level "<Level>" in the languages tab
	And the ADD NEW button should <Visibility> after adding language

Examples:
  | Language  | Level   | Visibility |
  | English   | Conversational  | Yes        |
  | French    | Basic   | Yes        |
  | German    | Native/Bilingual  | Yes        |
  | Spanish   | Fluent  | No         |

@ValidScenario
Scenario: User can update a language after adding multiple languages
 Given the following languages exist in the profile:
        | Language | Level             |
        | English  | Conversational    |
        | French   | Basic             |
        | German   | Native/Bilingual  |
        
  When I click the update icon near the language "French" and update it to "FrenchNew" with level "Fluent"
  Then I should see the language updated as "FrenchNew" with a confirmation message


@ValidScenario
Scenario: User able to delete any language displayed in profile
   Given the following languages exist in the profile:
        | Language | Level             |
        | English  | Conversational    |
        | French   | Basic             |
        | German   | Native/Bilingual  |
        
  When I click the delete icon near the language level of "French"
  Then language "French" should be deleted with confirmation message

  @InvalidScenario
Scenario: Error message displayed while adding an already existing language
    Given the following languages exist in the profile:
        | Language | Level             |
        | English  | Conversational    |
        | French   | Basic             |
        | German   | Native/Bilingual  |
       
    When I try to add an already existing language "French" with level "Fluent"
    Then error should be displayed on the profile avoiding addition

@InvalidScenario
Scenario Outline: Error message displayed when I leave language name or level field blank 
	Given I am on the languages tab of the profile
	When I leave <LanguageName> or <Level> field blank while adding
	Then error should be displayed to enter details <LanguageName> and <Level>

Examples:
  | LanguageName  | Level   | 
  | Spanish		  |			|
  |			      | Basic   | 
  |			      |			| 


 @DestructiveScenario
Scenario Outline: User able to add languages with symbols and long characters
	When I add a new language "<Language>" with level "<Level>"
	Then I should see "<Language>" displayed with the corresponding level "<Level>" in the languages tab
	And the ADD NEW button should <Visibility> after adding language

Examples:
  | Language  | Level   | Visibility |
  | @#$%^&*()!   | Conversational  | Yes        |
  | abcdgbahdjbsjdskjdkkkkkkkkkkkkkkkkkkkkkk    | Basic   | Yes        |
  | ggsua12343@ncjdc    | Native/Bilingual  | Yes        |
  | 漢字漢字漢字       | Fluent  | No         |



# Scenarios related to skills tab

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
  Given I am on the skills tab of the profile
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