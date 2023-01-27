# TwitterStatistics
Twitter api token is located in appsettings.json file as 'BearerToken'.

This application utilizes Twitter API v2 sampled stream endpoint and processes incoming tweets to compute various statistics.

This solution contains 2 projects such as:
 1. TwitterStatistics - Console application using .NET Core 3.1
 2. TwitterUnitTests - UnitTest project
 
Application implemented using:
- Dependency injection
- Logger
- Configuration using json-file
- Error handling
- Test coverage
 
 The app displays different metrics: 
  - Total number of tweets 
  - Number of tweets containing an URL in a text message
  - Number of tweets per minute
