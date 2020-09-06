namespace WeightTrackerConsole
{
    using System;
    using Microsoft.Azure.Cosmos.Table;

    public class CloudStorageAccountHelper : ICloudStorageAccountHelper
    {
        public CloudStorageAccount CreateFromConnectionString(String connectionString)
        {
            CloudStorageAccount storageAccount;

            try
            {
                storageAccount = CloudStorageAccount.Parse(connectionString);
            }
            catch(Exception)
            {
                Console.WriteLine("Invalid or badly formed Storage Account information provided. Could not create Storage Account.");
                throw;
            }

            return storageAccount;
        }        
    }
}