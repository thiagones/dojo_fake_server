using System;

namespace Dojo.Api
{
    public class EndpointsOptions
    {
        public const string Endpoints = "Endpoints";
        public string BaseAddress { get; set; }
        public string Users { get; set; }
        public string Repos { get; set; }
    }
}