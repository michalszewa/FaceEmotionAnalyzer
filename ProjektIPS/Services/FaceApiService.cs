using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjektIPS.Domain.Services;
using ProjektIPS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ProjektIPS.Services
{
    public class FaceApiService : IFaceApiService
    {
        private readonly FaceApiConfigHelper _faceApiConfig;
        public FaceApiService(IOptions<FaceApiConfigHelper> faceConfig)
        {
            _faceApiConfig = faceConfig.Value;
        }
        public async Task<IEnumerable<Face>> MakeRequest(string path)
        {
            List<FaceApiViewModel.FaceInfo> faces;
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
            byte[] byteData = GetImageAsByteArray(path);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                faces = JsonConvert.DeserializeObject<List<FaceApiViewModel.FaceInfo>>(contentString);
            }
            return Map(faces);
        }

        private byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }

        private List<Face> Map(List<FaceApiViewModel.FaceInfo> source)
        {
            List<Face> output = new List<Face>();

            foreach (FaceApiViewModel.FaceInfo element in source)
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

        private string DetectEmotion(FaceApiViewModel.FaceInfo source)
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
