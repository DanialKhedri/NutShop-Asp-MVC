using Application.Dtos.AboutUsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces;

public interface IAboutUsService
{
    public Task<AboutUsDTO?> GetAboutUs();

    public Task EditAboutUs(AboutUsDTO aboutUsDTO);





}
