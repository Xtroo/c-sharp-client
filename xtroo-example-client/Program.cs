using System;
using System.Net;   // Required as we are using the WebClient class, see https://msdn.microsoft.com/en-us/library/system.net.webclient(v=vs.110).aspx for more details on this class.
using Newtonsoft.Json.Linq;   // Required for parsing of JSON request, more info at https://www.newtonsoft.com/json. Downloaded via NuGet Package Manager.

namespace xtroo_example_client
{
    class Program
    {
        // The base address and API key don't need to change in this example, so I have declared them both here
        // so I can use the same variables throughout the function.
        private static string baseAddress = "https://xtroo.io/api/";
        private static string apiKey = "your_api_key";

        static void Main(string[] args)
        {
            Console.WriteLine("Fetching request, please wait.");

            // Send our query off to our GetContent function. Argument requires the URL that is to be grabbed.
            JObject result = GetContent("https://www.website-to-grab.com");

            // Print out our results to screen. Per the documentation (https://xtroo.io/documentation/1-quick-start)
            // there will be up to 5 indexes we can access from the returned object. title, text, html, images and links. 
            // In this example I'm going to print out all 5. 
            Console.WriteLine("Title: " + result["title"]);
            Console.WriteLine("Text: " + result["text"]);
            Console.WriteLine("HTML: " + result["html"]);
            // Note that both Images and Links can return an array, so you can either access the whole array by calling the parent index (i.e. result["images"])
            // Or you can access a specific entry with result["images"][arrayindex] (i.e. result["images"][2] to get the 3rd link in the array). A foreach loop
            // is also viable.
            Console.WriteLine("Images: " + result["images"]);
            Console.WriteLine("Links: " + result["links"]);

            // Since this is just a basic console app, I'll have it wait for input before it terminates.
            Console.WriteLine("Press any key to terminate");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static JObject GetContent(string urlToGrab)
        {
            // Create a new web client and give it the base address.
            WebClient client = new WebClient();
            client.BaseAddress = baseAddress;

            // API key must be given in a header, so we add this to our client.
            client.Headers.Add("api", apiKey);

            // Lastly we add the url itself.
            client.QueryString.Add("url", urlToGrab);

            // The response is in JSON so we send the DownloadString request to get it.
            string json = client.DownloadString("content");

            // We conver that returned string into a JSON object so we can reference it. 
            JObject obj = JObject.Parse(json);

            // And we return the JSON object to the caller.
            return obj;
        }
    }
}
