using System;
using System.IO;
using System.Net;

namespace IoT
{
    class Program
    {
        private static readonly string url = @"http://studentmajorleague/api/Competition/SetResult";
        private static int userId;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter the user ID: ");
                var userId = Int32.Parse(Console.ReadLine());
                
                Console.Write("Enter the result: ");
                var result = Console.ReadLine();

                PostRequest(userId, result);

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private static void PostRequest(int userId, string result)
        {
            var request = WebRequest.Create(url);

            request.Method = "POST";

            var data = $"userId={userId}&result={result}";

            var byteArray = System.Text.Encoding.UTF8.GetBytes(data);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            var response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }

            response.Close();

            Console.WriteLine("Запрос выполнен...");
        }
    }
}
