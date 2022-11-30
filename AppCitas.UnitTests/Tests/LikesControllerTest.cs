using AppCitas.Service.DTOs;
using AppCitas.Service.Helpers;
using AppCitas.Service.Interfaces;
using AppCitas.UnitTests.Helpers;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppCitas.UnitTests.Tests
{
    public class LikesControllerTest
    {
        private string apiRoute = "api/likes";
        private readonly HttpClient _client;
        private HttpResponseMessage? httpResponse;
        private string requestUri = string.Empty;
        private string registerObject = string.Empty;
        private string loginObjetct = string.Empty;
        private HttpContent? httpContent;
        private Mock<ILikesRepository> mockLikesRepository;

        public LikesControllerTest()
        {
            _client = TestHelper.Instance.Client;
        }

        [Theory]
        [InlineData("username", "Unauthorized")]
        public async Task AddLike__FaileBeacauseIsUnauthorized(string username, string statusCode)
        {
            // Arrange
            this.requestUri = $"{apiRoute}/{username}";

            // Act
            this.httpResponse = await _client.PostAsync(this.requestUri, null);

            // Assert
            Assert.Equal(Enum.Parse<HttpStatusCode>(statusCode, true), httpResponse.StatusCode);
        }

        [Theory]
        [InlineData("NotFound", "lisa", "Pa$$w0rd")]
        public async Task AddLike__NotFoundlikedUser(string statusCode, string username, string password)
        {
            // Arrange
            this.requestUri = $"{apiRoute}/bob";

            var requestUrl = "api/account/login";
            var loginDto = new LoginDto
            {
                Username = username,
                Password = password
            };

            loginObjetct = GetLoginObject(loginDto);
            httpContent = GetHttpContent(loginObjetct);

            httpResponse = await _client.PostAsync(requestUrl, httpContent);
            var reponse = await httpResponse.Content.ReadAsStringAsync();
            var userDto = JsonSerializer.Deserialize<UserDto>(reponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userDto.Token);

            // Act
            this.httpResponse = await _client.PostAsync(this.requestUri, null);

            // Assert
            Assert.Equal(Enum.Parse<HttpStatusCode>(statusCode, true), httpResponse.StatusCode);
        }

        [Theory]
        [InlineData("OK", "lisa", "Pa$$w0rd")]
        public async Task GetUserLikes__RunSuccesfuly(string statusCode, string username, string password)
        {
            // Arrange
            this.requestUri = $"{apiRoute}";
            var likesParams = new LikesParams()
            {
                UserId = 1,
                Predicate = "test"
            };

            var requestUrl = "api/account/login";
            var loginDto = new LoginDto
            {
                Username = username,
                Password = password
            };

            loginObjetct = GetLoginObject(loginDto);
            httpContent = GetHttpContent(loginObjetct);

            httpResponse = await _client.PostAsync(requestUrl, httpContent);
            var reponse = await httpResponse.Content.ReadAsStringAsync();
            var userDto = JsonSerializer.Deserialize<UserDto>(reponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userDto.Token);


            // Act
            this.httpResponse = await _client.GetAsync(this.requestUri);

            // Assert
            Assert.Equal(Enum.Parse<HttpStatusCode>(statusCode, true), httpResponse.StatusCode);
        }

        #region PrivateMethod
        private static string GetRegisterObject(LikesParams registerDto)
        {
            var entityObject = new JObject()
            {
                { nameof(registerDto.UserId), registerDto.UserId },
                { nameof(registerDto.Predicate), registerDto.Predicate },
                { nameof(registerDto.PageNumber), registerDto.PageNumber},
                { nameof(registerDto.PageSize), registerDto.PageSize }
            };

            return entityObject.ToString();
        }

        private StringContent GetHttpContent(string objectToEncode)
        {
            return new StringContent(objectToEncode, Encoding.UTF8, "application/json");
        }

        private static string GetLoginObject(LoginDto loginDto)
        {
            var entityObject = new JObject()
            {
                { nameof(loginDto.Username), loginDto.Username },
                { nameof(loginDto.Password), loginDto.Password }
            };

            return entityObject.ToString();
        }
        #endregion
    }
}
