using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Diagnostics;

namespace Plugin.RestClient
{
    /// <summary>
    /// RestClient implements methods for calling CRUD operations
    /// using HTTP.
    /// </summary>
    /// 
    //Thi methods used for crud operation between the WinneryApp client and WinneryApp web services
    public class RestClient<T>
    {        

        private const string WebServiceUrl = "http://wineryappwebservices.azurewebsites.net/api/";
        
        public async Task<List<T>> GetAsync(string route)
        {

            try
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var json = await httpClient.GetStringAsync(WebServiceUrl + route);

                var taskModels = JsonConvert.DeserializeObject<List<T>>(json);

                return taskModels;
            }
            catch (HttpRequestException e) //Api error
            {
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        //This method is calling http Get with the route parameter and a facebook Id (long) parameter.
        public async Task<T> GetAsync(string route,long id)
        {
            
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            try
            {
                var json = await httpClient.GetStringAsync(WebServiceUrl + route + "/" + id);

                var taskModels = JsonConvert.DeserializeObject<T>(json);

                return taskModels;
            }
            catch(Exception ex)//Something went wrong with the call
            {
                return default(T);
            }
           
        }
        //This method is calling http Post with a Generic T type and a route parameter.
        public async Task<bool> PostAsync(T t,string route)
        {
           
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            try
            {
                var result = await httpClient.PostAsync(WebServiceUrl + route, httpContent);
                return result.IsSuccessStatusCode;
            }
            catch (Exception e) //Somethimg went wrong
            {
                return false;
            }            
        }

        public async Task<bool> PutAsync(int id, T t)
        {
            //sSetWebServiceUrl();

            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PutAsync(WebServiceUrl + id, httpContent);

            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteAsync(int id, T t)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(WebServiceUrl + id);

            return response.IsSuccessStatusCode;
        }        
    }
}
