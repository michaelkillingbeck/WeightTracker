using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace WeightTrackerConsole
{
    public class CloudTableHelper : ICloudTableHelper
    {
        private readonly CloudStorageAccount _storageAccount;

        public CloudTableHelper(CloudStorageAccount storageAccount)
        {
            _storageAccount = storageAccount;                
        }

        public async Task<CloudTable> GetCloudTableByName(String tableName)
        {
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            CloudTable table = tableClient.GetTableReference(tableName);

            if(await table.ExistsAsync() == false)
            {
                Console.WriteLine($"Could not find Table named {tableName}.");
                throw new ApplicationException($"Could not find Table named {tableName}.");
            }

            return table;
        }

        public async Task<T> InsertOrMergeEntityAsync<T>(CloudTable table, T entity) where T : TableEntity
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                TableOperation operation = TableOperation.InsertOrMerge(entity);
                TableResult result = await table.ExecuteAsync(operation);

                T returnedEntity = result.Result as T;

                return returnedEntity;
            }
            catch(Exception)
            {
                System.Console.WriteLine("Failure performing Insert/Merge operation.");
                throw;
            }
        }
    }
}