namespace WeightTrackerConsole
{
    using Microsoft.Azure.Cosmos.Table;
    using System;

    public interface ICloudStorageAccountHelper
    {
        CloudStorageAccount CreateFromConnectionString(String accountConnectionDetails);
    }
}