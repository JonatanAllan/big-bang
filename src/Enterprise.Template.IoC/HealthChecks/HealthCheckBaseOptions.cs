using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Enterprise.Template.IoC.HealthChecks
{
    public static class HealthCheckBase
    { 
        public static HealthCheckOptions GetHealthCheckOptions()
        {
            return new HealthCheckOptions
            {
                ResponseWriter = async (httpContext, result) =>
                {
                    httpContext.Response.ContentType = "application/json";

                    var json = new JObject(
                        new JProperty("status", result.Status.ToString()),
                        new JProperty("results", new JObject(result.Entries.Select(pair =>
                            new JProperty(pair.Key, new JObject(
                                new JProperty("status", pair.Value.Status.ToString()),
                                new JProperty("description", pair.Value.Description),
                                new JProperty("data", pair.Value.Data.Any()
                                    ? new JObject(pair.Value.Data.Select(p => new JProperty(p.Key, p.Value)))
                                    : null)))))));
                    await httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
                }
            };
        }
    }
}
