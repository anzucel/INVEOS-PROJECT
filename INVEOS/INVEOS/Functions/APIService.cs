using Newtonsoft.Json;
using System.Text;

namespace INVEOS.Functions
{
    public class APIService
    {
        private static int timeout = 30;
        private static string baseurl = "https://localhost:7236/";

        private static async Task<System.Net.Http.HttpResponseMessage> GetListMethod(string url)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.GetAsync(baseurl + url);
            return response;
        }

        private static async Task<System.Net.Http.HttpResponseMessage> DeleteMethod(string url, int id)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.DeleteAsync(baseurl + $"{url}/{id}");
            return response;
        }

        private static async Task<System.Net.Http.HttpResponseMessage> SetMethod(string url, string json_)
        {
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.PostAsync(baseurl + url, content);
            return response;
        }

        private static async Task<System.Net.Http.HttpResponseMessage> EditMethod(string url, int id, string json_)
        {
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.PutAsync(baseurl + $"{url}/{id}", content);
            return response;
        }

        private static async Task<System.Net.Http.HttpResponseMessage> GetByIDMethod(string url, int id)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.GetAsync(baseurl + $"{url}/{id}");
            return response;
        }


        public static async System.Threading.Tasks.Task<IEnumerable<T>> GetList<T>(string controller)
        {
            var response = await GetListMethod($"{controller}/GetList");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<T>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<T> Set<T>(T object_to_serialize, string controller)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var response = await SetMethod($"{controller}/Set", json_);//httpClient.PostAsync(baseurl + "Movies/Set", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<T> Delete<T>(int id, string controller)
        {
            //var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var response = await DeleteMethod($"{controller}/Delete", id);//httpClient.PostAsync(baseurl + "Movies/Set", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<T> GetByID<T>(int id, string controller)
        {
            var response = await GetByIDMethod($"{controller}/GetByID", id);//httpClient.PostAsync(baseurl + $"Movies/GetByID/{id}", null);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async System.Threading.Tasks.Task<T> Edit<T>(T object_to_serialize, int id, string controller)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var response = await EditMethod($"{controller}/Edit", id, json_);//httpClient.PostAsync(baseurl + "Movies/Set", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}
