namespace HalalAssignement.Utils
{
    public static class DataDir
    {
        public static string Root => System.IO.Directory.GetCurrentDirectory() + @"/TestData/";
        public static string Points => Root + @"Points.txt";
        public static string FuncAppr => Root + @"FuncAppr1.txt";
        public static string Towns => Root + @"Towns.txt";
    }
}