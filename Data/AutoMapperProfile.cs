using AutoMapper;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AutoMapperProfile : Profile
    {
        public static int CalculateAgeCorrect(DateOnly birthDate)
        {
            var now = DateTime.UtcNow; // Use UtcNow to avoid timezone issues
            int age = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age--;

            return age;
        }
        public AutoMapperProfile()
        {
            CreateMap<DateOnly?, DateOnly>().ConvertUsing((src, dest) => src ?? dest);
            
        }
    }
}
