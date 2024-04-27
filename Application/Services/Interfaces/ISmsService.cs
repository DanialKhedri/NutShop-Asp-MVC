using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ISmsService
    {

        public Task SendPublicSms(string PhoneNumber, string Message);

        public Task SendLookUpSms(string PhoneNumber,
     string TemplateName,
     string token,
     string? token2 = ""
     , string? token3 = "");

    }
}
