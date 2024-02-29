using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CachingWebAPI.Models
{
    public class Driver
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DriverNo { get; set; }
    }
}
