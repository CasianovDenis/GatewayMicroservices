using System.Net;
using System.Text;
using Myproject.Base.Models;
using Newtonsoft.Json;

namespace Myproject.Services
{
    public class HttpService
    {
        private static readonly HttpClient client = new HttpClient();

        public Result<string> PostRequest<T>(T model, string url)
        {
            var result = new Result<string>() { ResponseCode = ResponseCode.SUCCES };

            try
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content);

                result.ReturnObject = response.Result.Content.ReadAsStringAsync().Result;

                if (response.Result.StatusCode != HttpStatusCode.OK || String.IsNullOrEmpty(result.ReturnObject))
                    throw new Exception(response.Result.Content.ToString());
            }
            catch (Exception ex)
            {
                result.ResponseCode = ResponseCode.TECHNICAL_EXCEPTION;
                result.ResultMessage = ex.Message;
            }

            return result;
        }

    }


}
