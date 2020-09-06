namespace WeightTrackerConsole
{
    using Microsoft.Azure.Cosmos.Table;
    using System;
    
    public class WeightRecord : TableEntity
    {        
        public WeightRecord()
        {
        }

        public WeightRecord(DateTime recordDate, Decimal recordedWeight)
        {
            RecordDate = recordDate;
            RecordedWeight = recordedWeight;

            PartitionKey = "weightrecord";
            RowKey = $"{recordDate.Year}{recordDate.Month.ToString("D2")}{recordDate.Day.ToString("D2")}";
        }

        public DateTime RecordDate {get;set;}
        public Decimal RecordedWeight {get;set;}
    }
}