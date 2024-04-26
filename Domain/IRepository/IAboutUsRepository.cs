using Domain.Entities.AboutUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IAboutUsRepository
    {
        public Task<AboutUs?> GetAboutUs();

    }
}
