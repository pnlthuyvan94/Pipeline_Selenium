using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Integrations
{
    public static class ApiConnection
    {
        public static void AddUpdateJobConfigOptions(string baseUrl, string apiMethod, object JobConfigOptions)
        {
            using (var client = new HttpClient())
            {
                var requestBody = JsonConvert.SerializeObject(
                        new { JobConfigOptions });

                client.BaseAddress = new Uri(baseUrl);

                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                try
                {
                    client.PostAsync(apiMethod, content).Wait();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public static void CreateSchedule(string baseUrl, string apiMethod, object inputs)
        {
            using (var client = new HttpClient())
            {
                string requestBody = JsonConvert.SerializeObject(inputs);

                client.BaseAddress = new Uri(baseUrl);
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                try
                {
                    client.PostAsync(apiMethod, content).Wait();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public static void CreateTaskForScheduleByJobNumberMultiple(string baseUrl, string apiMethod, List<object> inputs)
        {
            using (var client = new HttpClient())
            {
                string requestBody = JsonConvert.SerializeObject(inputs);

                client.BaseAddress = new Uri(baseUrl);
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                try
                {
                    client.PostAsync(apiMethod, content).Wait();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
