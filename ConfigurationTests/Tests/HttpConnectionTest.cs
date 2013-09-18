using System;
using System.Collections.Generic;
using System.Net;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class HttpConnectionTest : Test
    {
        public string UrlToTest { get; set; }
        public string ExpectedResponse { get; set; }

        public override void Run()
        {
            var httpRequest = WebRequest.Create(UrlToTest);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            int status = (int)httpResponse.StatusCode;
            AssertState.Equal(ExpectedResponse, status.ToString(), false, String.Format("The Http response was {0}. The Expected response is {1}", status, ExpectedResponse));
        }

        public override List<Test> CreateExamples()
        {
            return new List<Test>{
                new HttpConnectionTest(){UrlToTest="http://www.google.com", ExpectedResponse="200"}
            };
        }
    }
}
