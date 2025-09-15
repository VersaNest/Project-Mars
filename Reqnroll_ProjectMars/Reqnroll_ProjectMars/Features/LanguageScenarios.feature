Feature: LanguageScenarios

This feature file contains scenarios related to the profile page where a user can manage their languages present in their profile.


Background:
    Given I am logged in with valid credentials "test@gmail.com" and "test1234"
	And I am on the Languages tab on the user profile

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