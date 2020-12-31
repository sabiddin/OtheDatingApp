using System;

namespace DatingApp.Api.Extensions
{
    public static class DateTimExtensions
    {
        public static int CalculateAge(this DateTime dob){
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if(dob.Date > today.AddYears(-age)) age--;
            return age;
        }
        
    }
}