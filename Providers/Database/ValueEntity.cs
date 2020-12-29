using System.ComponentModel.DataAnnotations;

namespace Projects.Providers.Database
{
    public class ValueEntity
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
    }
}