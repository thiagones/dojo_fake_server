using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dojo.Api.HttpClientServices.Interfaces;
using Dojo.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dojo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly ILogger<GitHubController> _logger;
        private readonly IGitHubService _gitHubService;

        public GitHubController(
            ILogger<GitHubController> logger,
            IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{login}")]
        public async Task<IActionResult> GetUser([FromRoute] string login)
        {
            User user;

            try
            {
                user = await _gitHubService.GetUser(login);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            if (user == null)
            {
                return NoContent();
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("{login}/repos")]
        public async Task<IActionResult> GetRepos([FromRoute] string login)
        {
            IEnumerable<Repo> repos;

            try
            {
                repos = await _gitHubService.GetRepos(login);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            if (repos == null || !repos.Any())
            {
                return NoContent();
            }

            return Ok(repos);
        }
    }
}