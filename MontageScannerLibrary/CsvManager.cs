using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.ComponentModel;

namespace MontageScanLib
{
    public static class CsvManager
    {
        static string filePath = "MontageScan.csv";

        public static void CreateCsvFile()
        { 
            if (File.Exists(filePath) == false)
            {
                using var writer = new StreamWriter(filePath);
                using var SaveToCsv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                SaveToCsv.Context.RegisterClassMap<MontageLieferscheinScanMap>();
                SaveToCsv.WriteHeader<MontageLieferscheinModel>();
                SaveToCsv.NextRecord();
                
            }
        
        }

        public static void WriteToCsv(MontageLieferscheinModel input)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            { HasHeaderRecord = false, };

            using var stream = File.Open(filePath, FileMode.Append);
            using var writer = new StreamWriter(stream);
            using (var SaveToCsv = new CsvWriter(writer, csvConfig))
            {
                SaveToCsv.Context.RegisterClassMap<MontageLieferscheinScanMap>();
                SaveToCsv.WriteRecord(input);
                SaveToCsv.NextRecord();
            }

        }
     
        public static string SearchForLS(string input)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<CSVReadMontageLieferscheinModel>();

            string positivOutput = string.Empty;
            string negativOutput = "Lieferschein nicht gefunden";
            foreach (var record in records)
            {
                if (record.Lieferschein == input)
                {
                    positivOutput += $"Lieferschein {record.Lieferschein} gescannt: {record.EingangsTimeStamp}\n";
                }

            }
            if (positivOutput.Length > negativOutput.Length) { return positivOutput; }
            else { return negativOutput; }
            
        }

        public static void FillListWithLastEntrys(BindingList<MontageLieferscheinModel> inputList, int amount)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<CSVReadMontageLieferscheinModel>().ToList();
            amount = Math.Min(amount, records.Count());
            int startPoint = records.Count() - amount;
            for (int i = startPoint; i < records.Count(); i++)
            {
                var artikelModel = MapToLieferscheinlModel(records[i]);
                inputList.Add(artikelModel);
            }
        }
        private static MontageLieferscheinModel MapToLieferscheinlModel(CSVReadMontageLieferscheinModel input)
        {
            MontageLieferscheinModel output = new MontageLieferscheinModel(input.Lieferschein)
            {
                Status = input.Status,
                EingangsTimeStamp = input.EingangsTimeStamp,
            };
            return output;
        }
    }


}



