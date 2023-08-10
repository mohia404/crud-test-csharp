Feature: Customer Manager

As a an operator I wish to be able to Create, Update, Delete customers and list all customers

@create
Scenario: Operator creates valid customer
	Given the following customers
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad     | amini     | 1995/22/05    | 09158955560  | mohia1374@gmail.com | 12345678            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |
	When i try to create customer
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammadamin | amini     | 1998/22/05    | 09158955563  | mohia1377@gmail.com | 42345678            |
	Then the customers should be
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad     | amini     | 1995/22/05    | 09158955560  | mohia1374@gmail.com | 12345678            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |
		| mohammadamin | amini     | 1998/22/05    | 09158955563  | mohia1377@gmail.com | 42345678            |
@create
Scenario: Operator creates customer with invalid phone number
	Given the following customers
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad     | amini     | 1995/22/05    | 09158955560  | mohia1374@gmail.com | 12345678            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |
	When i try to create customer
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammadamin | amini     | 1998/22/05    | 0915895556   | mohia1377@gmail.com | 42345678            |
	Then i should get invalid phone number error

@create
Scenario: Operator creates customer with existing first name, last name and date of birth
	Given the following customers
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad     | amini     | 1995/22/05    | 09158955560  | mohia1374@gmail.com | 12345678            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |
	When i try to create customer
		| first_name | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad   | amini     | 1995/22/05    | 09158955560  | mohia1377@gmail.com | 52345678            |
	Then i should get customer exists error

@create
Scenario: Operator creates customer with existing email
	Given the following customers
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad     | amini     | 1995/22/05    | 09158955560  | mohia1374@gmail.com | 12345678            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |
	When i try to create customer
		| first_name | last_name     | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad   | mohammadamini | 1995/22/05    | 09158955567  | mohia1378@gmail.com | 62345678            |
	Then i should get email exists error

@delete
Scenario: Operator delete customer that exists
	Given the following customers
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad     | amini     | 1995/22/05    | 09158955560  | mohia1374@gmail.com | 12345678            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |
	When i try to delete customer with email 'mohia1374@gmail.com'
	Then the customers should be
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |

@delete
Scenario: Operator delete customer that do not exists
	Given the following customers
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad     | amini     | 1995/22/05    | 09158955560  | mohia1374@gmail.com | 12345678            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |
	When i try to delete customer with email 'mohia1377@gmail.com'
	Then i should get customer do not exist error

@read
Scenario: Operator get customers
	Given the following customers
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad     | amini     | 1995/22/05    | 09158955560  | mohia1374@gmail.com | 12345678            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |
	When i try to get customers
	Then i should get following customers
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad     | amini     | 1995/22/05    | 09158955560  | mohia1374@gmail.com | 12345678            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |

@read
Scenario: Operator get existing customer
	Given the following customers
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad     | amini     | 1995/22/05    | 09158955560  | mohia1374@gmail.com | 12345678            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |
	When i try to get customer with email 'mohia1374@gmail.com'
	Then i should get following customer
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad     | amini     | 1995/22/05    | 09158955560  | mohia1374@gmail.com | 12345678            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |

@update
Scenario: Operator update existing customer
	Given the following customers
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| mohammad     | amini     | 1995/22/05    | 09158955560  | mohia1374@gmail.com | 12345678            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |
	When i try to update following customer
		| first_name | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| hasan      | hasani    | 1995/22/05    | 09158955569  | mohia1374@gmail.com | 12345679            |
	Then the customers should be
		| first_name   | last_name | date_of_birth | phone_number | email               | bank_account_number |
		| hasan        | hasani    | 1995/22/05    | 09158955569  | mohia1374@gmail.com | 12345679            |
		| amin         | mohammadi | 1996/22/05    | 09158955561  | mohia1375@gmail.com | 22345678            |
		| mohammadamin | mohammadi | 1997/22/05    | 09158955562  | mohia1376@gmail.com | 32345678            |
