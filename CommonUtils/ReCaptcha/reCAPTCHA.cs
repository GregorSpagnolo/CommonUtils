using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CommonUtils.ReCaptcha
{
    public class ReCAPTCHA
    {
        private readonly string _secretKey;
        public ReCAPTCHA(string secretKey)
        {
            _secretKey = secretKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// /// <param name="userIP">Context.GetFeature<IHttpConnectionFeature>(),Context.GetFeature<IHttpConnectionFeature>();</param>
        /// <param name="gRecaptchaResponse">Context.Request.Form["g-recaptcha-response"]</param>
        /// <returns>returns recaptcha object check if success is true</returns>
        public async Task<ReCaptchaResponse> IsCaptchaVerified(string userIP, string gRecaptchaResponse)
        {
            var postData = string.Format("&secret={0}&remoteip={1}&response={2}",
            _secretKey,
            userIP,
            gRecaptchaResponse);

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://www.google.com/");
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "recaptcha/api/siteverify");
                    //var postDataAsBytes = Encoding.UTF8.GetBytes(postData);
                    request.Content = new StringContent(postData,
                                    Encoding.UTF8,
                                    "application/x-www-form-urlencoded");//CONTENT-TYPE header

                    HttpResponseMessage response = await client.SendAsync(request);
                    Stream receiveStream = await response.Content.ReadAsStreamAsync();
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    return JsonConvert.DeserializeObject<ReCaptchaResponse>(readStream.ReadToEnd());
                }
                catch (HttpRequestException)
                {
                    
                    return new ReCaptchaResponse { Success = false};
                }
            }
        }
    }
}

