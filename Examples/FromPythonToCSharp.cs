using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Spark.Sql;
using Microsoft.Spark.Sql.Types;

namespace HelloSpark.Examples
{
    public static class FromPythonToCSharp
    {

        public static void BasicDfExample(SparkSession spark)
        {
            var dataFrame = spark.Read().Json("Source/people.json");
            dataFrame.Show();
            dataFrame.PrintSchema();
            // selecting a single column, by name and the printing out the resulting df
            dataFrame.Select("name").Show();
            // Select everybody, but increment the age by 1
            dataFrame.Select(dataFrame["name"], dataFrame["age"] + 1).Show();
            dataFrame.Select(dataFrame["name"], dataFrame["age"].Plus(1)).Show();
            // Select people older than 21
            dataFrame.Filter(dataFrame["age"] > 21).Show();
            // Count people by age
            dataFrame.GroupBy(dataFrame["age"]).Count().Show();
            // DataFrame is converted into an Apache Hive view, which allows the DataFrame to be queried using SQL statements
            dataFrame.CreateTempView("people"); // temp view will live as long as the SparkSession
            var sqlDataFrame = spark.Sql("SELECT * FROM people");
            // Making a DataFrame accessible to other SparkSession’s SQL context in C#
            dataFrame.CreateGlobalTempView("people");
            spark.Sql("SELECT * FROM global_temp.people").Show();
            spark.NewSession().Sql("SELECT * FROM global_temp.people").Show();




        }

    }
}
