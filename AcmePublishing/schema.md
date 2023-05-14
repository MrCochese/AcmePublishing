classDiagram
      
class dbo_Address {
    AddressLine1
          AddressLine2
          CountryCode
          Id
          PostCode
          Region
          
}
class dbo_Customer {
    FamilyName
          GivenName
          Id
          
}
class dbo_PrintDistributor {
    CountryCode
          Id
          Name
          
}
class dbo_Publication {
    Id
          Name
          
}
class dbo_PrintDistributorPublication {
    DistributorsId
          PublicationsId
          
}
class dbo_Subscription {
    Active
          AddressId
          CustomerId
          Id
          PublicationId
          
}
class dbo_SubscriptionIssue {
    FailedToSend
          Id
          Issue
          SubscriptionId
          
}
      dbo_PrintDistributorPublication --|> dbo_PrintDistributor: Id
dbo_PrintDistributorPublication --|> dbo_Publication: Id
dbo_Subscription --|> dbo_Address: Id
dbo_Subscription --|> dbo_Customer: Id
dbo_Subscription --|> dbo_Publication: Id
dbo_SubscriptionIssue --|> dbo_Subscription: Id
