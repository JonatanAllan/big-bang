using Enterprise.Configuration.Models;
using System.ComponentModel.DataAnnotations;
using Environments = Enterprise.Configuration.Enums.Environments;

namespace Enterprise.Template.IoC
{
    public class AppSettings
    {
        [Required]
        public Environments Environment;

        public List<DatabaseConnection> ConnectionStrings { get; set; }
    }
}
