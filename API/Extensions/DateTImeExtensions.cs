using System;

namespace API.Extensions
{   // extension method need to be static 
    public static class DateTImeExtensions
    {
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;

            // if user not have there birthday yet
            if(dob.Date > today.AddYears(-age))
            {
                 age--;
            }
            return age;


        }
    }
}