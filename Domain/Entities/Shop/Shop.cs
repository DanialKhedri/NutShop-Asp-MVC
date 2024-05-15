using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Shop
{
    public class Shop
    {
        public int Id { get; set; }

        public string? ShopName { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        //Intro

        public string? IntroTitle { get; set; }
        public string? IntroDescription { get; set; }
        public string? IntroImage { get; set; }

        //

        public string? WhyNutsTitle { get; set; }
        public string? WhyNutsDescription { get; set; }

        public string? WhyUsTitle { get; set; }
        public string? WhyUsDescription { get; set; }

        public string? WhyUsTitle2 { get; set; }
        public string? WhyUsDescription2 { get; set; }


    }
}
