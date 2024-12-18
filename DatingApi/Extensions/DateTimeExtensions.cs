namespace DatingApi.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly birthDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            int age = today.Year - birthDate.Year;

            if (today < birthDate.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
