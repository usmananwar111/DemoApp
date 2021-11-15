User App is divided into two separate projects.
1)	DemoApp.Data

Database related activity is managed in this project

I have used datamodels and viewmodels.
In viewmodels validation is implemented and we can change the validation without needing to update database as database tables are maintained separately under datamodels.

CustomValidation is implemented to ensure we can check unique email address is used.
Front end validation is implemented using jquery
Server side validation is also used in case user manually turn off browser javascript.
I have used GUID instead of normal id as key.

Validation is implemented in single place for view and serverside validation in viewmodels instead of repeating the validation.

Password is stored in database after encoding the password.
 To make it more secure we can add salt alongside.

2)	DemoApp.Web

Web related (frontend) is managed in this project
MVC is used for web application. Used anti forgery token for security. 
I have used Nlog to store any exception.
The normal user won’t be able to see any exception but it’s stored in text file and system admin can retrieve it.
Apologies I haven’t implemented UnitTesting in the project.



