using Application.Extensions.KaveNegar;
using Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.implements;

public class SmsService : ISmsService
{

    #region Ctor

    private readonly KaveNegarInfoModel _kaveNegarInfoModel;

    public SmsService(IOptions<KaveNegarInfoModel> kavenegarmodel)
    {
        _kaveNegarInfoModel = kavenegarmodel.Value;
    }

    #endregion



    #region Properties

    #endregion

    //Send  Public Sms

    public async Task SendPublicSms(string PhoneNumber, string Message)
    {
        try
        {

            var api = new Kavenegar.KavenegarApi(_kaveNegarInfoModel.ApiKey);

            var result = api.Send(_kaveNegarInfoModel.Sender, PhoneNumber, Message);

        }

        catch (Kavenegar.Exceptions.ApiException ex)
        {
            throw new Exception(ex.Message);
        }

        catch (Kavenegar.Exceptions.HttpException ex)
        {
            throw new Exception(ex.Message);
        }



    }

    //Send LookUp


    public async Task SendLookUpSms(string PhoneNumber,
        string TemplateName,
        string token, 
        string? token2 = ""
        , string? token3 = "")

    {
        try
        {

            var api = new Kavenegar.KavenegarApi(_kaveNegarInfoModel.ApiKey);

            var result = api.VerifyLookup(PhoneNumber,token,TemplateName);

        }

        catch (Kavenegar.Exceptions.ApiException ex)
        {
            throw new Exception(ex.Message);
        }

        catch (Kavenegar.Exceptions.HttpException ex)
        {
            throw new Exception(ex.Message);
        }

    }


}
