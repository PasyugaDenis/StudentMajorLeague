using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IoT
{
    public class IoTResult
    {
        public int UserId { get; set; }

        public int CompetitionId { get; set; }

        public string Result { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("https://studentmajorleaguebeta.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            while (true)
            {
                Console.Write("Enter the user ID: ");
                var userId = int.Parse(Console.ReadLine());

                Console.Write("Enter the competition ID: ");
                var competitionId = int.Parse(Console.ReadLine());

                Console.Write("Enter the result: ");
                var result = Console.ReadLine();

                PostRequest(userId, competitionId, result).GetAwaiter().GetResult();

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private static async Task PostRequest(int userId, int competitionId, string result)
        {
            var resultObj = new IoTResult
            {
                UserId = userId,
                CompetitionId = competitionId,
                Result = result
            };

            var response = await client.PostAsJsonAsync("api/Competition/SetResult", resultObj);
            response.EnsureSuccessStatusCode();

            Console.WriteLine("Запрос выполнен...");
        }
    }
}
