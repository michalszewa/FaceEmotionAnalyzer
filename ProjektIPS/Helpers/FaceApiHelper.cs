using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ProjektIPS.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace ProjektIPS.Helpers
{
    public class FaceApiHelper
    {
        public static async Task<List<Face>> MakeRequest(FaceApiConfig _faceApiConfig, string imageFilePath)
        {
            List<FaceApiResponse.FaceInfo> faces;
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{_faceApiConfig.SubscriptionKey}");

            // Request parameters
            queryString["returnFaceId"] = "true";
            queryString["returnFaceLandmarks"] = "false";
            queryString["returnFaceAttributes"] = "Age,gender,smile,emotion";
            string uri = _faceApiConfig.UriBase + "?" + queryString;

            HttpResponseMessage response;
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                faces = JsonConvert.DeserializeObject<List<FaceApiResponse.FaceInfo>>(contentString);
            }

            return Map(faces);
        }

        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }

        private static List<Face> Map(List<FaceApiResponse.FaceInfo> source)
        {
            List<Face> output = new List<Face>();

            foreach(FaceApiResponse.FaceInfo element in source)
            {
                output.Add(new Face
                {
                    Top = element.faceRectangle.top,
                    Left = element.faceRectangle.left,
                    Width = element.faceRectangle.width,
                    Height = element.faceRectangle.height,
                    Gender = element.faceAttributes.gender,
                    Age = element.faceAttributes.age,
                    Emotion = DetectEmotion(element)
                });
            }
            return output;
        }

        private static string DetectEmotion(FaceApiResponse.FaceInfo source)
        {
            Dictionary<string, double> emotions = new Dictionary<string, double>
            {
                {  "Anger", source.faceAttributes.emotion.anger },
                 { "Contempt", source.faceAttributes.emotion.contempt },
                 { "Disgust", source.faceAttributes.emotion.disgust },
                 { "Fear", source.faceAttributes.emotion.fear },
                 { "Happiness", source.faceAttributes.emotion.happiness },
                 { "Neutral", source.faceAttributes.emotion.neutral },
                 { "Sadness", source.faceAttributes.emotion.sadness },
                 { "Surprise", source.faceAttributes.emotion.surprise }
            };

            var max = emotions.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

            return max;
        }
 
    }
}
