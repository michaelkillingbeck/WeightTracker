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
            RecordedWeight = String.Format("{0:F1}", recordedWeight);

            PartitionKey = "weightrecord";
            RowKey = $"{recordDate.Day.ToString("D2")}{recordDate.Month.ToString("D2")}{recordDate.Year}";
        }

        public DateTime RecordDate {get;set;}
        public String RecordedWeight {get;set;}
    }
}