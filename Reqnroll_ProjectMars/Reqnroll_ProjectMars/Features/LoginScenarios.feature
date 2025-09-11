Feature: LoginScenarios

This feature file contains scenarios related to the main page realated to login and join actions.

@ValidLogin
Scenario: User able to login successfully from main page
	Given I open the login modal from main page
	When I enter valid email "test@gmail.com" and password "test1234" to login
	Then Profile page of user should be displayed with first name "TestFirst"

@ValidLogin
Scenario Outline: Verify user able to access forgot passsword feature
	Given I open the login modal from main page
	When I click forgot passwork link
	Then I should be able to enter <Valid> <Email> for verification
	And confirmation message should be displayed that email sent for only <Valid> user <Email>

Examples:
  | Valid   | Email				|
  | Yes     | test@gmail.com    |  
  |	No		|abc@email.com      |   
  | No      |                   |   

@InvalidLogin
Scenario Outline: Verify error message after leaving email and password fields blank
	Given I open the login modal from main page
	When I leave the "<Email>" and "<Password>" fields blank
	Then Error message for blank "<Email>" and "<Password>" should be displayed

	Examples:
  | Email          | Password  |
  |                |           |   
  | test@gmail.com |           |   
  |                | test1234  |   
      
@InvalidLogin
Scenario Outline: Verify error message when a user enter invalid email and password for login
	Given I open the login modal from main page
	When I enter an invalid "<Email>" or "<Password>" in the fields 
	Then I should be able to enter "test@gmail.com" for verification
	And confirmation message should be displayed that email has been sent

	Examples:
  | Email           | Password  |
  | wrong@gmail.com | test1234  |   
  | wrong@gmail.com | wrong123  |   


@NewUserLogin
Scenario: User able to register new account
	Given I open the join modal from main page
	When I enter valid user details "<Email>" , "<Password>" , "<FirstName>", "<LastName>" in join form
	Then Confirmation message should be displayed for succesful account creation
	And I should be able to login successfully using the new "<Email>" and "<Password>"
	And Profile page of user should be displayed with first name "<FirstName>"

	Examples:
  | Email             | Password | FirstName | LastName |
  | johnsam@email.com | john1234 | John      | Smith    |
 


 



