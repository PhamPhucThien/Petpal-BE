using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CapstoneProject.Database.Model.Base
{
    public class BaseModel<T>
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("status", TypeName = "nvarchar(30)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public T? Status { get; set; }
        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        [Column("created_by")]
        public string? CreatedBy { get; set; }
        [Column("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
        [Column("updated_by")]
        public string? UpdatedBy { get; set; }
    }
}
