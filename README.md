# Frends.Amazon.SQS

FRENDS Amazon Task for SQS

 [![Actions Status](https://github.com/FrendsPlatform/Frends.AmazonSQS/workflows/PackAndPushAfterMerge/badge.svg)](https://github.com/FrendsPlatform/Frends.AmazonSQS/actions) ![MyGet](https://img.shields.io/myget/FrendsPlatform/v/Frends.AmazonSQS) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 

- [Installing](#installing)
- [Tasks](#tasks)
     - [GetBasicAWSCredentials](#GetBasicAWSCredentials)
     - [ReceiveMessage](#ReceiveMessage)
     - [SendMessage](#SendMessage)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing

You can install the task via FRENDS UI Task View or you can find the NuGet package from the following NuGet feed
https://www.myget.org/F/frends-community/api/v3/index.json and in Gallery view in MyGet https://www.myget.org/feed/frends-community/package/nuget/Frends.Community.AWS.SQS

# Building

Clone a copy of the repo

`git clone https://github.com/CommunityHiQ/Frends.Community.AWS.SQS.git`

Rebuild the project

`dotnet build`

Run Tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log

| Version | Changes |
| ------- | ------- |
| 1.0.0   | First multiplatform version | 
