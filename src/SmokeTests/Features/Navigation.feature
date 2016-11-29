Feature: Navigation
	All links on the navigation bar should take me to the correct page

Scenario Outline: Link to correct page
	Given I am on the site
	When I log in
	And I click on the <link> link
	Then I should be on the <page> page

	Examples: 
	| link        | page        |
	| New         | New         |
	| Search      | Search      |
	| My Expenses | My Expenses	|