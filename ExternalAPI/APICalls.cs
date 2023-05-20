using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MASHWAR.ExternalAPI
{
    public class APICalls 
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<APICalls> _logger;

        public APICalls(HttpClient httpClient, ILogger<APICalls> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

        }


        public async Task<GptResponseRoot> Gptcompletions(string  usermessage)
        {
            

            string apiUrl = "https://api.openai.com/v1/chat/completions";
            string authToken = "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";

         
            
            var message = new GptRequestRoot
            {
                model = "gpt-3.5-turbo",
                temperature = 0.7,
                messages = new List<GptMessage>
                {
                    new GptMessage
                    {
                        role = "assistant",
                        content = usermessage,
                    }
                }
            };
            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            
            var jsonRequest = JsonSerializer.Serialize(message);
            request.Content = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);
            var response = await _httpClient.SendAsync(request);
            var responsedata = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<GptResponseRoot>(responsedata);
                return result;
            }

            return null;
        }

  
        public async Task<YelpRootResposne> MapLocation(YelpParam param)
        {
            string apiUrl = "https://api.yelp.com/v3/businesses/search";
            string apiKey = "O6UgV3jvtuQsU9PrBjmBE2WYpn6b1M2L5yODeuowLh93JKHMo6KU_A_MaPDFRRJYsWG3fLg4EFcU3x9t7QaxEAA-8jVwWTdsF3GTqrtJXMu2M5QAkXQ3Fa8TCA5oZHYx";

            // Location parameters
            double latitude = param.latitude; // 37.7749;  // Example latitude
            double longitude = param.longitude; //  -122.4194;  // Example longitude
            int radius = 1000;  // Radius in meters

            // API request parameters
            string location = $"{latitude},{longitude}";
            string radiusParam = (radius / 1609).ToString();  // Convert radius from meters to miles
            string term = param.term;  // Term to search for, leave empty for all businesses

            // Create HttpClient instance
            using var httpClient = new HttpClient();

            // Set the authorization header with the API key
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            // Send the request
            string url = $"{apiUrl}?latitude={latitude}&longitude={longitude}&radius={radiusParam}&term={term}";
            var response = await httpClient.GetAsync(url);

            //var request = new HttpRequestMessage(HttpMethod.Get, url);
            //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var response = await _httpClient.SendAsync(request);
            //var responsedata = await response.Content.ReadAsStringAsync();
            //if (response.IsSuccessStatusCode)
            //{
            //    var result = JsonSerializer.Deserialize<GptResponseRoot>(responsedata);
            //    return result;
            //}

            //return null;
            //// Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<YelpRootResposne>(responseData);
                return result;
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            }

            return null;
        }
    
    
        public async Task<Root> GoogleMap(YelpParam param)
        {
            // Google Places API endpoint and API key
            string apiUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json";
            string apiKey = "AIzaSyBAdzgOoEUr5HPpLVir_sKidxrnK2USidE";

            // Location parameters
            double latitude =param.latitude;  // Example latitude
            double longitude = param.longitude;  // Example longitude
            int radius = 500;  // Radius in meters

            // API request parameters
            string location = $"{latitude},{longitude}";
            string radiusParam = radius.ToString();
            string type = "restaurant";  // Change to the desired place type

            // Create HttpClient instance
            using var httpClient = new HttpClient();

            // Send the request
            string url = $"{apiUrl}?location={location}&radius={radiusParam}&type={type}&key={apiKey}";
            var response = await httpClient.GetAsync(url);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<Root>(responseData);
                // Handle the response data as needed (e.g., parse JSON, extract reviews)
                return result;
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            }

            return null;
        }
    }



    public class YelpParam
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
        public string term { get; set; } // Term to search for, leave empty for all businesses
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Business
    {
        public string id { get; set; }
        public string alias { get; set; }
        public string name { get; set; }
        public string image_url { get; set; }
        public bool is_closed { get; set; }
        public string url { get; set; }
        public int review_count { get; set; }
        public List<Category> categories { get; set; }
        public double rating { get; set; }
        public Coordinates coordinates { get; set; }
        public List<string> transactions { get; set; }
        public string price { get; set; }
        public Location location { get; set; }
        public string phone { get; set; }
        public string display_phone { get; set; }
        public double distance { get; set; }
    }

    public class Category
    {
        public string alias { get; set; }
        public string title { get; set; }
    }

    public class Center
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
    }

    public class Coordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Location
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public List<string> display_address { get; set; }
    }

    public class Region
    {
        public Center center { get; set; }
    }

    public class YelpRootResposne
    {
        public List<Business> businesses { get; set; }
        public int total { get; set; }
        public Region region { get; set; }
    }







}
