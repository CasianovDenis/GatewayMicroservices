using System.Net;
using System.Text;
using Myproject.Base.Models;
using Newtonsoft.Json;

namespace Myproject.Services
{
    public class HttpService
    {
        private static readonly HttpClient client = new HttpClient();
        private static bool _isEnabledAuthorization = false;

        public HttpService()
        {
            IEnumerable<string> values;

            client.DefaultRequestHeaders.TryGetValues("Authorization", out values);
            if (values != null)
                _isEnabledAuthorization = true;
        }

        public Result<string> PostRequest<T>(T model, string url, string token = null)
        {
            var result = new Result<string>() { ResponseCode = ResponseCode.SUCCES };

            try
            {
                if (!_isEnabledAuthorization && !String.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer" + token);

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

        public Result<string> GetRequest(string url, string token)
        {
            var result = new Result<string>() { ResponseCode = ResponseCode.SUCCES };

            try
            {
                if (!_isEnabledAuthorization && !String.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var response = client.GetAsync(url);
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

        public Result<string> PutRequest<T>(T model, string url, string token = null)
        {
            var result = new Result<string>() { ResponseCode = ResponseCode.SUCCES };

            try
            {
                if (!_isEnabledAuthorization && !String.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PutAsync(url, content);

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

        public Result<string> DeleteRequest(string url, string token = null)
        {
            var result = new Result<string>() { ResponseCode = ResponseCode.SUCCES };

            try
            {
                if (!_isEnabledAuthorization && !String.IsNullOrEmpty(token))
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var response = client.DeleteAsync(url);

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
