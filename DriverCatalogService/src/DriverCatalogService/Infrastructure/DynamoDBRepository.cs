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
            _ddbContext.SaveAsync<Driver>(driver).Wait();
        }

        public Driver Load(string id)
        {
            return _ddbContext.LoadAsync<Driver>(id).Result;
        }

        public bool Exists(string driverFirstName, string driverLastName)
        {
            var op = _ddbContext.ScanAsync<Driver>(new[]
            {
                new ScanCondition(nameof(Driver.FirstName), ScanOperator.Equal, driverFirstName ?? string.Empty),
                new ScanCondition(nameof(Driver.LastName), ScanOperator.Equal, driverLastName ?? string.Empty)
            });

            return op.GetNextSetAsync().Result.Any();
        }

        public bool ContainsAnother(string driverId, string driverFirstName, string driverLastName)
        {
            var op = _ddbContext.ScanAsync<Driver>(new[]
            {
                new ScanCondition(nameof(Driver.FirstName), ScanOperator.Equal, driverFirstName ?? string.Empty),
                new ScanCondition(nameof(Driver.LastName), ScanOperator.Equal, driverLastName ?? string.Empty)
            });

            var driver = op.GetNextSetAsync().Result.FirstOrDefault();
            return driverId == driver?.Id;
        }
    }
}