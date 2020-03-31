using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDashboard
{
    public static class MachineLearning
    {
        private const string Exporturi = "Comert international - exporturi FOB";
        private const string Importuri = "Comert international - importuri CIF";
        private const string Sold = "Comert international - sold FOB/CIF";
        private const string EfectivulSalariatilor = "Forta de munca - efectiv salariati";
        private const string CastigulSalarialMediuBrut = "Forta de munca - salariu mediu brut";
        private const string CastigulSalarialMediuNet = "Forta de munca - salariu mediu net";
        private const string NumarulSomerilor = "Forta de munca - numar someri";
        private const string InnoptariInStructurileDePrimireTuristica = "Turism - innoptari";
        private const string SosiriInStructurileDePrimireTuristica = "Turism - numar turisti";
        private const string NascutiVii = "Populatie - nascuti vii";
        private const string Decedati = "Populatie - decedati";
        private const string SporNatural = "Populatie - spor natural";
        private const string Casatorii = "Populatie - casatorii";
        private const string Divorturi = "Populatie - divorturi";
        private const string DecedatiSubUnAn = "Populatie - decedati sub 1 an";
        private const string AutorizatiiDeConstruire = "Autorizatii de construire pentru cladiri rezidentiale";

        //protected Dictionary<string, Type> ChapterList = new Dictionary<string, Type>()
        //    {
        //        { Exporturi, typeof(ExportFob) },
        //        { Importuri, typeof(ImportCif) },
        //        { Sold, typeof(SoldFobCif) },
        //        { EfectivulSalariatilor, typeof(NumberOfEmployees) },
        //        { CastigulSalarialMediuBrut, typeof(AverageGrossSalary) },
        //        { CastigulSalarialMediuNet, typeof(AverageNetSalary) },
        //        { NumarulSomerilor, typeof(Unemployed) },
        //        { InnoptariInStructurileDePrimireTuristica, typeof(NumberOfNights) },
        //        { SosiriInStructurileDePrimireTuristica, typeof(NumberOfTourists) },
        //        { NascutiVii, typeof(BornAlive) },
        //        { Decedati, typeof(Deceased) },
        //        { SporNatural, typeof(NaturalGrowth) },
        //        { Casatorii, typeof(Marriages) },
        //        { Divorturi, typeof(Divorces) },
        //        { DecedatiSubUnAn, typeof(DeceasedUnderOneYearOld) },
        //        { AutorizatiiDeConstruire, typeof(BuildingPermits) },
        //    };

        public static async Task Start()
        {
            var mlContext = new MLContext();

            List<float> values = new List<float>();
            
            for(int year = 2014; year <= 2019; year++)
            {
                for(int month = 1; month <= 12; month++)
                {
                    values.Add((await CountyStandingsProvider.GetData(typeof(ExportFob), year, month)).Sum(x => x.Value));
                }
            }
            // { (await CountyStandingsProvider.GetData(typeof(ExportFob), 2019, 1)).Sum(x => x.Value) }; // TODO: Add more data, in fact, all data that you have

            // Get history
            var productHistory = values.Select(x => new MlData { Value = x });
            var productDataView = mlContext.Data.LoadFromEnumerable(productHistory);

            // Create and add the forecast estimator to the pipeline.
            IEstimator<ITransformer> forecastEstimator = mlContext.Forecasting.ForecastBySsa(
                outputColumnName: nameof(ProductUnitTimeSeriesPrediction.ForecastedProductUnits),
                inputColumnName: nameof(ProductData.units), // This is the column being forecasted.
                windowSize: 12, // Window size is set to the time period represented in the product data cycle; our product cycle is based on 12 months, so this is set to a factor of 12, e.g. 3.
                seriesLength: productHistory.Count(), // This parameter specifies the number of data points that are used when performing a forecast.
                trainSize: productHistory.Count(), // This parameter specifies the total number of data points in the input time series, starting from the beginning.
                horizon: 1, // Indicates the number of values to forecast; 1 indicates that the next month of product units will be forecasted.
                confidenceLevel: 0.95f, // Indicates the likelihood the real observed value will fall within the specified interval bounds.
                confidenceLowerBoundColumn: nameof(ProductUnitTimeSeriesPrediction.ConfidenceLowerBound), //This is the name of the column that will be used to store the lower interval bound for each forecasted value.
                confidenceUpperBoundColumn: nameof(ProductUnitTimeSeriesPrediction.ConfidenceUpperBound)); //This is the name of the column that will be used to store the upper interval bound for each forecasted value.

            // Train the forecasting model for the specified product's data series.
            ITransformer forecastTransformer = forecastEstimator.Fit(productDataView);

            // Create the forecast engine used for creating predictions.
            TimeSeriesPredictionEngine<ProductData, ProductUnitTimeSeriesPrediction> forecastEngine = forecastTransformer.CreateTimeSeriesEngine<ProductData, ProductUnitTimeSeriesPrediction>(mlContext);

            // Predict.
            var nextMonthUnitDemandEstimation = forecastEngine.Predict();

            //return nextMonthUnitDemandEstimation.ForecastedProductUnits.First();
        }
    }

    public class MlData
    {
        public float Value { get; set; }
    }

    public class Prediction
    {
        [VectorType]
        public double[] Output { get; set; }
    }

    public class ProductUnitTimeSeriesPrediction
    {
        public float[] ForecastedProductUnits { get; set; }

        public float[] ConfidenceLowerBound { get; set; }

        public float[] ConfidenceUpperBound { get; set; }
    }

    public class ProductData
    {
        // The index of column in LoadColumn(int index) should be matched with the position of columns in the underlying data file.
        // The next column is used by the Regression algorithm as the Label (e.g. the value that is being predicted by the Regression model).
        [LoadColumn(0)]
        public float next;

        [LoadColumn(1)]
        public float productId;

        [LoadColumn(2)]
        public float year;

        [LoadColumn(3)]
        public float month;

        [LoadColumn(4)]
        public float units;

        [LoadColumn(5)]
        public float avg;

        [LoadColumn(6)]
        public float count;

        [LoadColumn(7)]
        public float max;

        [LoadColumn(8)]
        public float min;

        [LoadColumn(9)]
        public float prev;

        public override string ToString()
        {
            return $"ProductData [ productId: {productId}, year: {year}, month: {month:00}, next: {next:0000}, units: {units:0000}, avg: {avg:000}, count: {count:00}, max: {max:000}, min: {min}, prev: {prev:0000} ]";
        }
    }
}