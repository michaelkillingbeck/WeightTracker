namespace WeightTrackerConsole
{
    using Microsoft.Azure.Cosmos.Table;
    using System;
    using System.Threading.Tasks;

    public interface ICloudTableHelper
    {
        Task<CloudTable> GetCloudTableByName(String tableName);
        Task<T> GetEntity<T>(CloudTable table, String partitionKey, String rowKey) where T : TableEntity;
        Task<T> InsertOrMergeEntityAsync<T>(CloudTable table, T entity) where T : TableEntity;
    }
}