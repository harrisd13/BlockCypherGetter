# BlockCypher blockchain getter

## Introduction

The purpose of this project is to retrieve and store blockchain data using BlockCypher's [blockchain API](https://www.blockcypher.com/dev/bitcoin/#blockchain-api).
It is implemented on .NET 8 using microservices and Clean Architecture.

## Quick Start
1. Configure the blockchains to get and store data from in WebApi's and BlockChainWorker's `appsettings.json` `BlockCypher` section
2. Run using ``` docker compose up ```

## Services

There are two services in this project:
 - WebApi
 - BlockchainWorker

# WebApi
It can be used to access stored blockchain data per blockchain name. Data is returned ordered by creation date in descending order and
it is possible to filter by them by date range and amount of objects.

# BlockchainWorker
It can be used for retrieving blockchain data and store them in database. It respects BlockCypher's [rate limit](https://www.blockcypher.com/dev/bitcoin/#restful-resources)
which is currently no more than 3 requests per second and 100 requests per hour.

## Database
MongoDB is used to store blockchain data. It was chosen because its high performance on time-series data. It includes
an additional index created for accessing data in descending order.

## Health checks
There's a health check for mongodb, it can be accessed using /health endpoint