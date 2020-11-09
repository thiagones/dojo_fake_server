namespace Dojo.Api.Models
{
   public class Repo
   {
      public int id { get; set; }
      public string node_id { get; set; }
      public string name { get; set; }
      public string full_name { get; set; }
      public bool @private { get; set; }
      public User owner { get; set; }
   }
}