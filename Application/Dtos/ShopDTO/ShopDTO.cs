using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ShopDTO
{
    public class ShopDTO
    {
        public int Id { get; set; }

        public string? ShopName { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        //Intro

        public string? IntroTitle { get; set; }
        public string? IntroDescription { get; set; }
        public string? IntroImage { get; set; }


    }

}
