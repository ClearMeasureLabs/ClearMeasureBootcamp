Feature: SubmitExpenseReport
	User should be able to submit an expense report quickly.


Scenario: Submit an expense report
	Given I am on the site
	When I log in
	And I create a new expense report
	And I submit the expense report
	Then I should be on the ExpenseReport page
