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
 
    public static string key;

    public static string GenerateOtp()
    {
        key = NumberGenerator.GenerateNumber();


        Timer timer = new Timer(TimerCallback, null, 10000, Timeout.Infinite);



        return key;

    }

    public static bool VerifyOtp(string otp)
    {

        if (otp == key)
            return true;
        else
            return false;

    }

    public static void TimerCallback(object state)
    {

        key = null;


    }

}




