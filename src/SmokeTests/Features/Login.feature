Feature: Login
	Log in page should be the first page the user sees if not logged in
	Home page should be the first page the user sees if logged in


Scenario: Arrive at login page
	Given I am on the site
	When I log out
	Then I should be on the Login page

Scenario: Arrive at home page
	Given I am on the site
	When I log in
	Then I should be on the Home page