using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions.Generators
{
    public static class NumberGenerator
    {

        public static string GenerateNumber()
        {

            var Generator = new Random();

            var Result = Generator.Next(0,100000).ToString("D5");

            return Result;
        }


    }
}
