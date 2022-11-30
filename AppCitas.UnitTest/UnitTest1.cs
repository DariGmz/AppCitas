using AppCitas.Service.Controllers;
using AppCitas.Service.Entities;
using AppCitas.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit.Sdk;

namespace AppCitas.UnitTest
{
    public class UnitTest1
    {
        private string apiRoute = "api/likes";
        private readonly HttpClient httpClient;
        private HttpResponseMessage? HttpResponse;
        private string requestUri = string.Empty;
        private string registerObject = string.Empty;
        private HttpContent? httpContent;

        public UnitTest1()
        {
            _client = Test
        }
    }
}