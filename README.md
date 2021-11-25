# CKO.Payments Solution

This project is a payment processing api built for Checkout

## Overview

This is built as a AWS Serverless application, this sits on top of the AWS lambda and CloudFormation resources. 
There are a few reasons why I choose to go down this route, The primary reason is that by in being serverless is can be scaled both vertically and horizontally, there is also the benefit that ir reduces the amount of DevOps required for setup and maintenance of the application.

When deciding on which type of project to go with I did initially think using AWS Lambda, the idea behind this was to use SQS/SNS and to have individual functions for each Http endpoint; 
I decided against using this approach due to the need to return a response the clients request. We could still use Lambda functions but we would not be able to use events as this would then push into a queue for processing, meaning that the client would not get a response to the initial request and instead need to be notified when it has been actioned.
In the end I landed on building a more traditional API as it will action requests as they come in, because it is still serverless it can scale to be able to keep up with high volume periods.

I decided to use AWS over Azure mainly as I had never used it before so it felt like a challenge to learn more about the services and what they can offer. Unfortuntely due to going with a traditional API instead of functions it meant that the leverage I could get from AWS was minimal, it would be the same between Azure and AWS in this instance as it could be deployed as an App Service which would also provided the scalability.

The structure of the solution is as follows

CKO.Payments
- **CKO.Payments**
--  This Project is the API, it provides the endpoints for client systems to call into and process their payments
Merchant validation also occures in this project via a JWT token, this token will be valid for 24 hours from the point of issuing, the reason why this token has a long TTL is because a merchant will likely be making multiple payment request during the day, we want to streamline this process for them as much as possible.

- **CKO.Payments.BL**
-- This project contains the business logic for the solution, in here you can find any business specific logic for processing of payments

- **CKO.Payments.DAL**
-- This project is the data access layer, it is designed to be an itermidatary between the business logic and the data later.
There is an agrument to be made that this project is redundant it could be an component of the BL.
I kept this as there will be data saving logic that in independant to any business logic, like data encryption, we 
may also have multiple BL layers in which there should only be one DAL instead of enabling every BL layer to have their own DAL logic

- **CKO.Payments.Data**
-- This project is responsible for storing and managing the DTO objects, we also contain the database migrations in this project

- **CKO.Payments.Tests**
-- This project contains unit tests for the BL layer

- **CKO.Payments.Bank**
-- This project is responsible for connecting to banks for payment processing, in our situation we only have a single bank 'Nakatomi' in this project you will find the HttpClient for sending both the processing and settlement request to this bank.
It has been writtin in this manner as it is highly likely that we will be working with multiple banks in the future and need to have each bank in an easily accessible location whilst also unifying their processes into a standard the rest of our project can work with

## How to run

If it is your first time running the solution then please run 'Update-Database' against the CKO.Payments.Data project inside of the Package Manager Console
This will create the database against the local database server on your machine, alternatively if you wish to build the database within another server, please change the 'CkoContext' found within the CKO.Payments appsettings.json file to be the connectionstring of your desired server

Once the database has been built you can run this project by using the built-in debugger in visual studio, alternatively you can use the 'dotnet run' command against the CKO.Payments project in your terminal


## Potential issues / Future changes

### JWT token

There is an inherant security flaw with the JWT token, especially would the extended TTL, by having tokens be valid for such a long period we do expose our services to anyone who has been able to retrieve a token that a client has/is using.

The way we have countered this is by request the merchant provide their secret with any payment requests, of course this only works if the attacker hasn't retrieved their secret as well, 
because of this, there are ways this can be broken as well, if the token ever did get exposed then using jwt.io would show you the contents, of which the secret is part of it. this would mean any attacker could then put requests through.
This utlimately means that the onus is on the client to ensure their token is kept safe. 

An additional way we could help keep tokens secure when the application is deployed is by enforcing all requests are https (this would be done regardless) this would ensure any request sniffing would not expose the token.
In the future I would prefer to use OAuth 2.0 and have each request validated at the time of the request, this would mean a slower execution time for the requests but the added security would make up for the miliseconds it would add.


