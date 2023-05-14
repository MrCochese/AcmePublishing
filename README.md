# AcmePublishing

Naive implementation of Print-Distribution runner job as a dotnet 7 console app. This would be executed monthly - a likely strategy for this would be a CRON job, or a timed job in a managed container service like K8s, ECS, Azure Containers etc... 

## Configuration

Set ConnectionStrings:SqlServer as an environment variable or user secret.

## Assumptions and simplifications

I'm assuming for the purposes of this exercise that each publication has one new issue every month, and that they all go out on the same day, and we will reference each as "mm/yyyy". Another simplification is the assumption that each Print-Distributor already has access to the issue file for printing, and is able to retrieve it via an API or CMS, otherwise we would have to load the publication files from storage here and post them to the Print-Distributors once per issue. I've also assumed that the Print-Distributor APIs may fail for transient or address validation reasons, and that the number of subscriptions and publications is not yet enormous (order of 10^3) and manageable in one go.

## Design

All active subscriptions are loaded and the relevent API for the subscription is provided using a static factory (DistributorApiFactory, implementation faked). Then a call is made for each subscription to print and send the publication issue, and then we save a record against the subscription to record that the issue was sent. The API wrapper implementation would likely run API specific a recovery policy, but we want to capture cases of absolute failure to send, in which case we log it and mark the SubscriptionIssue as having failed.  

## Database Schema

See AcmePublishing/schema.md and AcmePublishing/schema.svg

## Development Process

I designed the entities code-first in EF Core and generated the database schema in a docker-hosted SQL Server using DataBase.EnsureCreated() for rapid development. Normally, I would use EF Core data migrations or another method to generate the schema rather than generating it from scratch on app execution.

## Improvements

Likely we would actually want to represent issues in the system with a publication date and publish file, and publish the issues on that day rather than all at once. To make the system more scaleable to large number of subscriptions, we would introduce batching, first by splitting by publication issue, then by distributor. At the moment we're tracking when a subscription fails to send, so for resilience we want a way to manually automatically retry later, either when address data has been fixed or the PrintDistributor API has recovered from an outage. If we run this application again on the same day, it should only send issues out to subscriptions that have not yet been sent already.

The SubscriptionProcess class would also benefit hugely from unit tests, using InMemory hosting on the data context.

In addition all the API calls are made in serial, which could take a while, so we can speed this up considerably by running the process on multiple threads to allow for simultaneous API calls.