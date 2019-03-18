using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DriverCatalogService.Models;
using Microsoft.Extensions.Configuration;

namespace DriverCatalogService.Infrastructure
{
    public class DynamoDBRepository : IRepository
    {
        private readonly string _targetTableName;
        private DynamoDBContext _ddbContext;
        private RegionEndpoint _region;

        public DynamoDBRepository(IConfiguration configuration)
        {
            _targetTableName = configuration["Repository:TableName"];
            _region = RegionEndpoint.GetBySystemName(configuration["AWS:Region"]);

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(Driver)] = new Amazon.Util.TypeMapping(typeof(Driver), _targetTableName);

            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
            _ddbContext = new DynamoDBContext(new AmazonDynamoDBClient(_region), config);
        }

        public async Task SetupTable()
        {
            using (var client = new AmazonDynamoDBClient(_region))
            {
                CreateTableRequest request = new CreateTableRequest
                {
                    TableName = _targetTableName,
                    ProvisionedThroughput = new ProvisionedThroughput
                    {
                        ReadCapacityUnits = 2,
                        WriteCapacityUnits = 2
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            KeyType = KeyType.HASH,
                            AttributeName = nameof(Driver.Id)
                        }
                    },
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition
                        {
                            AttributeName = nameof(Driver.Id),
                            AttributeType = ScalarAttributeType.S
                        }
                    }
                };

                await client.CreateTableAsync(request);

                var describeRequest = new DescribeTableRequest {TableName = _targetTableName};
                DescribeTableResponse response = null;
                do
                {
                    Thread.Sleep(1000);
                    response = await client.DescribeTableAsync(describeRequest);
                } while (response.Table.TableStatus != TableStatus.ACTIVE);
            }
        }

        public void DropTable()
        {
            using (var client = new AmazonDynamoDBClient(_region))
            {
                client.DeleteTableAsync(_targetTableName).Wait();
            }
        }

        public void Save(Driver driver)
        {
            var table = _ddbContext.GetTargetTable<Driver>();

            var doc = _ddbContext.ToDocument(driver);
            if (driver.ModifiedAt == null)
            {
                doc[nameof(Driver.ModifiedAt)] = DynamoDBNull.Null;
                table.PutItemAsync(doc).Wait();
            }
            else
            {
                table.UpdateItemAsync(doc).Wait();
            }
        }

        public Driver Load(string driverId)
        {
            var table = _ddbContext.GetTargetTable<Driver>();
            var doc = table.GetItemAsync(driverId).Result;
            if (doc != null)
            {
                return new Driver
                {
                    Id = doc[nameof(Driver.Id)],
                    FirstName = doc[nameof(Driver.FirstName)],
                    LastName = doc[nameof(Driver.LastName)],
                    CreatedAt = DateTime.Parse(doc[nameof(Driver.CreatedAt)]),
                    ModifiedAt = !Equals(doc[nameof(Driver.ModifiedAt)], DynamoDBNull.Null)? DateTime.Parse(doc[nameof(Driver.ModifiedAt)]) : (DateTime?) null
                };
            }

            return null;
        }

        public bool Exists(string driverFirstName, string driverLastName)
        {
            var table = _ddbContext.GetTargetTable<Driver>();
            var filter = new ScanFilter();
            filter.AddCondition(nameof(Driver.FirstName), ScanOperator.Equal, driverFirstName);
            filter.AddCondition(nameof(Driver.LastName), ScanOperator.Equal, driverLastName);

            var search = table.Scan(filter);
            return search.GetNextSetAsync().Result.Any();
        }

        public bool ContainsAnother(string driverId, string driverFirstName, string driverLastName)
        {
            var table = _ddbContext.GetTargetTable<Driver>();
            var filter = new ScanFilter();
            filter.AddCondition(nameof(Driver.FirstName), ScanOperator.Equal, driverFirstName);
            filter.AddCondition(nameof(Driver.LastName), ScanOperator.Equal, driverLastName);
            filter.AddCondition(nameof(Driver.Id), ScanOperator.NotEqual, driverId);

            var search = table.Scan(filter);
            return search.GetNextSetAsync().Result.Any();
        }

        public void Delete(string driverId)
        {
            _ddbContext.DeleteAsync<Driver>(driverId).Wait();
        }
    }
}