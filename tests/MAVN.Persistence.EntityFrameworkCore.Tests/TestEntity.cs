using System.Collections.Generic;
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

        [ForeignKey("TestEntityId")]
        public TestChildEntity Child { get; set; }

        [ForeignKey("TestEntityId")]
        public List<TestChildEntity> Children { get; set; }
    }

    public class TestChildEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long TestEntityId { get; set; }

        [ForeignKey("TestChildEntityId")]
        public List<TestGrandChildEntity> GrandChildren { get; set; }
    }

    public class TestGrandChildEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long TestChildEntityId { get; set; }
    }
}
