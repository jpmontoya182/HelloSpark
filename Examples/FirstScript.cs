using Microsoft.Spark.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSpark.Examples
{
    public static class FirstScript
    {
        public static void CreateCSVFile(SparkSession spark, string[] args)
        {
            var dataFrame = spark.Sql("select id, rand() as random_number from range(1000)");

            dataFrame
                .Write()
                .Format("csv")
                .Option("header", true)
                .Option("sep", "|")
                .Mode("overwrite")
                .Save(args[1]);

            foreach (var row in dataFrame.Collect())
            {
                if (row[0] as int? % 2 == 0)
                {
                    Console.WriteLine($"row : {row[0]}");
                }

            }
        }
    }
}
