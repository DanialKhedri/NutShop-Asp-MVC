using Application.Extensions.Generators;
using OtpSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiry.Base32;

namespace Application.Extensions.OtpSharp;

public static class OtpManger
{

    public static string GenerateOtp()
    {
        string StringKey = NumberGenerator.GenerateNumber();

        byte[] Keybytes = Encoding.ASCII.GetBytes(StringKey);

        //string someString = Encoding.ASCII.GetString(bytes);


        var totp = new Totp(Keybytes);
        return totp.ComputeTotp();
    }

    public static bool VerifyOtp(string otp)
    {
        string StringKey = NumberGenerator.GenerateNumber();

        byte[] Keybytes = Encoding.ASCII.GetBytes(StringKey);

        //string someString = Encoding.ASCII.GetString(bytes);


        var totp = new Totp(Keybytes);

        return totp.VerifyTotp(otp, out long timeStepMatched);
    }


}



}
