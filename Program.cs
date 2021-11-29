using System;
using HelloSpark.Examples;
using Microsoft.Spark.Sql;

namespace HelloSpark
{
    class Program
    {
        static void Main(string[] args)
        {
            var spark = SparkSession.Builder().GetOrCreate();

            // FirstScript.CreateCSVFile(spark, args);
            // FromPythonToCSharp.BasicDfExample(spark);
            UserDefineFunctions.ProcessOne();
        }
    }
}
