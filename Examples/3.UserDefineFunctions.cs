using Microsoft.Data.Analysis;
using Microsoft.Spark.Sql;
using System;


namespace HelloSpark.Examples
{
    public static class UserDefineFunctions
    {
        private static string IntToStr(int id)
        {
            return $"The id is {id}";
        }

        public static void ProcessOne()
        {
            var spark = SparkSession.Builder().GetOrCreate();
            Func<Column, Column> udfIntToString = Functions.Udf<int, string>(id => IntToStr(id));
            var dataFrame = spark.Sql("select id from range(1000)");
            dataFrame.Select(udfIntToString(dataFrame["id"])).Show();
        }


        private static int AddMount = 100;
         
        private static Int64DataFrameColumn Add100(Int64DataFrameColumn id)
        {
            return id.Add(AddMount);
        }

        public static void ProcessTwo()
        {
            var spark = SparkSession.Builder().GetOrCreate();
            var dataFrame = spark.Sql("SELECT ID FROM range(1000)");
            AddMount = 991923;

            Func<Column, Column> addUdf = DataFrameFunctions.VectorUdf<Int64DataFrameColumn, Int64DataFrameColumn>((id) => Add100(id));
            dataFrame.Select(dataFrame["ID"], addUdf(dataFrame["ID"])).Show();
        }
    }
}
