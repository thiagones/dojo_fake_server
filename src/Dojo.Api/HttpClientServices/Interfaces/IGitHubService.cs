using System.Collections.Generic;
using System.Threading.Tasks;
using Dojo.Api.Models;

namespace Dojo.Api.HttpClientServices.Interfaces
{
   public interface IGitHubService
   {
      Task<User> GetUser(string login);

      Task<IEnumerable<Repo>> GetRepos(string login);
       
   }
}