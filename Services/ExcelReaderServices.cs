using ExcelDataReader;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace FYPBackend.Services
{
    public class ExcelReaderServices
    {
        public List<ClusteredResult> PerformClustering(string filePath, int numClusters = 4)
        {
            var clinicTimes = ReadTotalTimeInClinic(filePath);

            if (clinicTimes.Count == 0)
                throw new Exception("No valid data found for clustering!");

            var mlContext = new MLContext();

            // Prepare Data
            var data = clinicTimes.Select(x => new PatientData { TotalTimeInClinic = (float)x }).ToList();
            var trainingData = mlContext.Data.LoadFromEnumerable(data);

            // Define K-Means Model
            var pipeline = mlContext.Clustering.Trainers.KMeans(
                featureColumnName: "Features",
                numberOfClusters: numClusters);

            // Convert Data to Feature Vector
            var features = mlContext.Transforms.Concatenate("Features", nameof(PatientData.TotalTimeInClinic));
            var model = pipeline.Fit(features.Fit(trainingData).Transform(trainingData));

            // Predict Clusters
            var transformedData = model.Transform(features.Fit(trainingData).Transform(trainingData));
            var predictions = mlContext.Data.CreateEnumerable<ClusterPrediction>(transformedData, reuseRowObject: false).ToList();

            // Return Results
            return predictions.Select((p, i) => new ClusteredResult
            {
                PatientId = i + 1,
                TotalTimeInClinic = data[i].TotalTimeInClinic,
                Cluster = p.PredictedClusterId
            }).ToList();
        }

        private List<double> ReadTotalTimeInClinic(string filePath)
        {
            var dataTable = ReadExcelFile(filePath);
            return dataTable.AsEnumerable()
                            .Where(row => row["TotalTimeInClinic"] != DBNull.Value)
                            .Select(row => Convert.ToDouble(row["TotalTimeInClinic"]))
                            .ToList();
        }

        private DataTable ReadExcelFile(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    return result.Tables[0];
                }
            }
        }
    }

    public class PatientData
    {
        [LoadColumn(0)]
        public float TotalTimeInClinic { get; set; }
    }

    public class ClusterPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId { get; set; }
    }

    public class ClusteredResult
    {
        public int PatientId { get; set; }
        public float TotalTimeInClinic { get; set; }
        public uint Cluster { get; set; }
    }
}

