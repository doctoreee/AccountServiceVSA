using AccountService.API.ExternalServices.Interfaces;
using System.Net;
using System.Xml.Linq;

namespace AccountService.API.ExternalServices;

public class TCKNValidateService: ITCKNValidateService
{
    public bool Validate(TCKNValidationRequest validationRequest)
    {
        var requestUrl = "https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx";
        var requestXmlbytesArray = System.Text.Encoding.UTF8.GetBytes(RequestXml(validationRequest));
        var requestContentLength = requestXmlbytesArray.Length;
        var request = BuildRequest(requestUrl, requestContentLength);
        var response = GetResponse(request, requestXmlbytesArray);

        return response;
    }

    private string RequestXml(TCKNValidationRequest request)
    {
        var requestXml = @"<?xml version=""1.0"" encoding=""utf-8""?>";
        requestXml += @"<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">";
        requestXml += @"<soap12:Body>";
        requestXml += @"<TCKimlikNoDogrula xmlns=""http://tckimlik.nvi.gov.tr/WS"">";
        requestXml += @"<TCKimlikNo>" + request.SocialSecurityNumber + "</TCKimlikNo>";
        requestXml += @"<Ad>" + request.Name + "</Ad>";
        requestXml += @"<Soyad>" + request.LastName + "</Soyad>";
        requestXml += @"<DogumYili>" + request.BirthDate.Year + "</DogumYili>";
        requestXml += @"</TCKimlikNoDogrula>";
        requestXml += @"</soap12:Body>";
        requestXml += @"</soap12:Envelope>";

        return requestXml;
    }

    private HttpWebRequest BuildRequest(string requestUrl, long contentLength)
    {
        var request = (HttpWebRequest)WebRequest.Create(requestUrl);
        request.ContentType = "application/soap+xml; charset=utf-8";
        request.Method = "POST";
        request.ContentLength = contentLength;

        return request;
    }

    private bool GetResponse(HttpWebRequest request, byte[] requestBytesArray)
    {
        using (var stream = request.GetRequestStream())
        {
            stream.Write(requestBytesArray, 0, (int)request.ContentLength);
        }

        string responseString;
        try
        {
            using (var response = request.GetResponse())
            {
                if (response == null)
                {
                    return false;
                }

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd().Trim();
                }
            }

            var responseXml = XDocument.Parse(responseString);
            var result = responseXml.Descendants().SingleOrDefault(x => x.Name.LocalName == "TCKimlikNoDogrulaResult").Value;

            return bool.Parse(result);
        }
        catch
        {
            return false;
        }
    }
}