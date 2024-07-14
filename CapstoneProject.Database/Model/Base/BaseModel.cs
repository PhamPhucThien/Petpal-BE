using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model.Base
{
    public class BaseModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("status")]
        public string? Status { get; set; }
        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        [Column("created_by")]
        public DateTimeOffset CreatedBy { get; set; }
        [Column("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
        [Column("updated_by")]
        public DateTimeOffset? UpdatedBy { get; set; }
    }
}
