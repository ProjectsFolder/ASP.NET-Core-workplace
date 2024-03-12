using System.ComponentModel.DataAnnotations.Schema;

namespace WebTest.Models
{
    public abstract class ModelBase
    {
        [Column("id")]
        public int Id { get; set; } = 0;
    }
}
