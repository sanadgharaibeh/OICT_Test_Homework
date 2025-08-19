  
**Public transportation card status and validity check.**

This project provides an API endpoint to check the status and validity of a user transport card. It returns the combined result of the card status and a validity date as a plain text response.

**Table of content**

* Requirements

* Setup

* Running the Project

* Discovering API References

* Contributors

**Requirements**

* .NET 7 SDK or later

* IDE or editor of your choice (e.g., Visual Studio)

* Internet access 

**Setup**

In order to properly setup this project, you will need to follow these steps

1- Clone the repository and navigate to root folder:

	git clone https://github.com/sanadgharaibeh/OICT_Test_Homework

2- Restore dependencies, Inside your command line editor run the following command:

	dotnet restore

3- Build the project, Inside your command line editor run the following command  :

	
	dotnet build

**Running the Project**

To start the project follow these steps.

1- Run the following command from project root folder where the .csproj file is:

	dotnet run

2- Navigate to url:

	https://localhost:7098/swagger/index.html

**Discovering [API](https://docs.google.com/document/d/1QFJ6fBUjrImk_QmxEjvnk3WB_N4zGpXI-PSYvn1kN3c/edit#bookmark=id.5utor35saw1u) References**

In order to call the Card Api, an Authorization token is needed, we recommend getting the token from the Swagger documentation first and then using it to call the Card Api. 

**Authorization API**

To get a token you must use one of two user credentials that are eligible for a token, they are sanad and tomas.

POST /api/Auth/Token

* Body:  
  * Example raw data  
    {"username": "sanad","password": "mypassword”}  
* Produces: json  
* Responses:  
  * 200 OK – Returns   
    {"token": "BELQ2HIfO0C2tyHxMKQBwA==","expires": "2025-08-18T20:53:49.8124338Z"}  
  * 401 Unauthorized – If the user is not authorized.
    
      Neplatné uživatelské jméno nebo heslo

**CARD API**

Is used to find the status and validity of any Public transportation Card. In order to make the call we need the card number and user token, the token is required  in the header, for that we can use Postman. You can import postman collection available in repository under name oict.postman\_collection.json:

GET /checkcardstatusandvalidity/{cardNumber}

* Parameters:  
  * cardNumber (string) – The user card number to check.  
* Headers:  
  * Token (string) – key is x-token and value is token value.  
* Produces: text/plain  
* Responses:  
  * 200 OK – Returns card status and validity as plain text.  
    "Aktivní v držení klienta \- 12.8.2020"  
  * 401 Unauthorized – If the request is not authorized.

  Token missing

  * 500 Server Error– server error.

  "Něco se pokazilo"

**Check API**

This API is used to health check a random service. It returns “OK” if the service is running and “Not OK” in case it is not.

Get /Check

* Produces: text  
* Responses:  
  * 200 OK – Returns   
    OK or Not OK

**Contributors**

 Sanad Gharaibeh 
sanad.gharaibeh@gmail.com

