# CKO.Payments Solution

This project is a payment processing api built for Checkout

## Overview

This is built as a AWS Serverless application, this sits on top of the AWS lambda and CloudFormation resources. 
There are a few reasons why I choose to go down this route, The primary reason is that by in being serverless is can be scaled both vertically and horizontally, there is also the benefit that it reduces the amount of DevOps required for setup and maintenance of the application.

When deciding on which type of project to go with I did initially think using AWS Lambda, the idea behind this was to use SQS/SNS and to have individual functions for each Http endpoint; 
I decided against using this approach due to the need to return a response the clients request. We could still use Lambda functions but we would not be able to use events as this would then push into a queue for processing, meaning that the client would not get a response to the initial request and instead need to be notified when it has been actioned.
In the end I landed on building a more traditional API as it will action requests as they come in, because it is still serverless it can scale to be able to keep up with high volume periods.

I decided to use AWS over a more familiar option is mainly because I have never used it before so it felt like it would be more of a challenge to learn more about the services and what they can offer. Unfortuntely due to going with AWS Serverless with .NET Core Web API instead of functions it meant that the opportunity to experience what AWS has to offer/challenges it would pose was not there, it would be the same between Azure and AWS in this instance as it could be deployed as an App Service which would also provide the scalability AWS Serverless does.

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

## Design assumptions

### Customer

When putting through a transaction there must be a customer linked to it, the main reason for this is that there must be a billing address linked to the trasaction, at first I was thinking that we could check for existing Customers by email and instead of having duplicate customer records we would link each customer to each transaction as it came in, but after considering what a transaction is I decided against this. When i thought about the transaction, each transaction is unique, the only characteristic of the transaction that needs to reference back to a single item is the Merchant, this is because a Merchant can have multiple transactions, whereas a customer is just a component of the transaction. by taking this approach it does have the negative impact that we will likely end up with duplicate customer records in our database, which could end up being something that needs to be changed later, but for now we should assume that each transaction and it's components are individual only linking back to the merchant who made the transaction.

#### Issue with this idea

If we look at this from the PayPal perspective, then every merchant can put a transaction through but every customer can see their transactions, if we ever needed to implement a system where a customer could review their transactions then we would find ourselves in a position where we would need to coalate the data together, rather than it all being linked to a single customer record.

In our system at the moment there is no way for a customer to interact with their data so this isn't an issue for now, but depending on the roadmap this could end up being an issue.

## Potential issues / Future changes

### JWT token

There is an inherant security flaw with the JWT token, especially would the extended TTL, by having tokens be valid for such a long period we do expose our services to anyone who has been able to retrieve a token that a client has/is using.

The way we have countered this is by request the merchant provide their secret with any payment requests, of course this only works if the attacker hasn't retrieved their secret as well, 
because of this, there are ways this can be broken as well, if the token ever did get exposed then using jwt.io would show you the contents, of which the secret is part of it. this would mean any attacker could then put requests through.
This utlimately means that the onus is on the client to ensure their token is kept safe. 

An additional way we could help keep tokens secure when the application is deployed is by enforcing all requests are https (this would be done regardless) this would ensure any request sniffing would not expose the token.
In the future I would prefer to use OAuth 2.0 and have each request validated at the time of the request, this would mean a slower execution time for the requests but the added security would make up for the miliseconds it would add.

### Request Validation

At the moment I have implemented simple validation that checks that the supplied model is in a format we expect/can use. when there is an error we sned a message back to the client saying there is an error, a future implementation would be to provided a more details response so that the client can see what failed validation and why. 

This would have the preceived benefit that it would make the API easier to code against as it would help the client to establish a good working model. It would also allow the client to easily dianoigse any potential issues they may get in the future either through their own system, or any potential changes we may release that would change the expected model.

### Logging 

For this execise I did not implement logging in the way I think would be beneficial moving forward, to start with I would implement a basic log system that would output to a service like DataDog, as well as using an exception tracking solution like Sentry, whilst you could use DataDog for both, it is not the best way to use it as it does not capture stacktraces and other key elements that would be needed to diagnosing issues. It does however provide a snapshot of the events that occured up to the point of failure.

### Data Encryption

Future change I would make is to save all data pretaining to card details or customer addresses as encrypted values, this will provided additional security encase of a data leak or security breach. When returning the data to the user to view I am masking all sensitive data so that only the minimum needed to verify a transaction can be seen. because of the masking it will always leave the last three characters unmasked, this is so that the user can have some data to do a comparison with, for example, the end of a card number or post code.

There are issues with this approach in that if the data provided is three characters or less then it would not be masked.

