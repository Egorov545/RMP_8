namespace RMP_8
{
    public static class StudentData
    {
        public static string LastName { get; set; } = "";
        public static string FirstName { get; set; } = "";
        public static string MiddleName { get; set; } = "";
        public static string Gender { get; set; } = "";
        public static DateTime BirthDate { get; set; } = DateTime.Now;
        public static bool NeedsDormitory { get; set; } = false;
        public static bool IsMonitor { get; set; } = false;
        public static string MathGrade { get; set; } = "";
        public static string RussianGrade { get; set; } = "";
        public static string PhotoPath { get; set; } = "";    
    }
}