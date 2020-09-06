namespace WeightTrackerConsole
{
    using Microsoft.Azure.Cosmos.Table;
    using System;
    using System.Threading.Tasks;

    public interface ICloudTableHelper
    {
        Task<CloudTable> GetCloudTableByName(String tableName);

        Task<T> InsertOrMergeEntityAsync<T>(CloudTable table, T entity) where T : TableEntity;
    }
}