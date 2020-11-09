using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Dojo.Api.HttpClientServices.Interfaces;
using Dojo.Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dojo.Api.HttpClientServices
{

   public class GitHubService : IGitHubService
   {
      private readonly HttpClient _httpClient;
      private readonly EndpointsOptions _endpointsOptions;
      private readonly ILogger<GitHubService> _logger;

      public GitHubService(HttpClient httpClient, IOptions<EndpointsOptions> endpointsOptions, ILogger<GitHubService> logger)
      {
         _logger = logger;
         _endpointsOptions = endpointsOptions.Value;

         httpClient.BaseAddress = new Uri(endpointsOptions.Value.BaseAddress);
         httpClient.DefaultRequestHeaders.Add("User-Agent", "DojoStub");

         _httpClient = httpClient;

         _logger.LogInformation(_endpointsOptions.BaseAddress);
         _logger.LogInformation(_endpointsOptions.Users);
         _logger.LogInformation(_endpointsOptions.Repos);
      }

      public async Task<IEnumerable<Repo>> GetRepos(string login)
      {
         _logger.LogInformation($"/users/{login}/repos");
         var response = await _httpClient.GetAsync(string.Format(_endpointsOptions.Repos, login));

         if (!response.IsSuccessStatusCode)
         {
            return null;
         }

         IEnumerable<Repo> result;
         using(var stream = await response.Content.ReadAsStreamAsync())
         {
            result = await JsonSerializer.DeserializeAsync<IEnumerable<Repo>>(stream);
         }
         return result;
      }

      public async Task<User> GetUser(string login)
      {
          _logger.LogInformation($"/users/{login}");
         var response = await _httpClient.GetAsync(string.Format(_endpointsOptions.Users, login));

         if (!response.IsSuccessStatusCode)
         {
            return null;
         }

         User result;
         using(var stream = await response.Content.ReadAsStreamAsync())
         {
            result = await JsonSerializer.DeserializeAsync<User>(stream);
         }

         return result;
      }
   }
}