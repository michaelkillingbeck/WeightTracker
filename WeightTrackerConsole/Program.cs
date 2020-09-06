namespace WeightTrackerConsole
{
    using System;
    using Microsoft.Azure.Cosmos.Table;

    class Program
    {
        static void Main(String[] args)
        {
            AppSettings appSettings = AppSettings.LoadAppSettings();
            ICloudStorageAccountHelper storageAccountHelper = new CloudStorageAccountHelper();

            CloudStorageAccount storageAccount = storageAccountHelper.CreateFromConnectionString(appSettings.ConnectionString);
            ICloudTableHelper tableHelper = new CloudTableHelper(storageAccount);

            CloudTable table = tableHelper.GetCloudTableByName(appSettings.TableName).Result;

            DateTime recordDate = new DateTime(2020, 06, 01);

            while(recordDate <= DateTime.Now)
            {
                String rowKey = $"{recordDate.Day.ToString("D2")}{recordDate.Month.ToString("D2")}{recordDate.Year}";
                WeightRecord existingRecord = tableHelper.GetEntity<WeightRecord>(table, "weightrecord", rowKey).Result;

                if(existingRecord == null)
                {
                    Console.WriteLine($"Enter the weight to record for {recordDate}...");
                    Decimal weightToRecord = 0;
                    String rawInput = Console.ReadLine();
                    Boolean success = Decimal.TryParse(rawInput, out weightToRecord);

                    if(success)
                    {
                        WeightRecord newWeightToRecord = new WeightRecord(recordDate, weightToRecord);

                        WeightRecord result = tableHelper.InsertOrMergeEntityAsync(table, newWeightToRecord).Result;

                        Console.WriteLine("Result:");
                        Console.WriteLine($"Date: {result.RecordDate}, Weight: {result.RecordedWeight}");
                    }
                }
                else
                {
                    Console.WriteLine($"Existing record found for {recordDate}...");
                    Console.WriteLine($"Date: {existingRecord.RecordDate.ToLocalTime()}, Weight: {existingRecord.RecordedWeight}");
                    Console.WriteLine("Enter a new weight to edit the record; or press Enter to leave the record unmodified...");

                    Decimal weightToRecord = 0;
                    String rawInput = Console.ReadLine();
                    Boolean success = Decimal.TryParse(rawInput, out weightToRecord);

                    if(success)
                    {
                        WeightRecord newWeightToRecord = new WeightRecord(recordDate, weightToRecord);

                        WeightRecord result = tableHelper.InsertOrMergeEntityAsync(table, newWeightToRecord).Result;

                        Console.WriteLine("Result:");
                        Console.WriteLine($"Date: {result.RecordDate}, Weight: {result.RecordedWeight}");

                        recordDate = recordDate.AddDays(1);
                    }
                }

                recordDate = recordDate.AddDays(1);
            }

            Console.WriteLine("Finshed.");
            Console.ReadKey(true);
        }
    }
}
