using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAVN.Persistence.EntityFrameworkCore.Tests
{
    public class TestEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string StrParam { get; set; }

        public int IntParam { get; set; }
    }
}
