using CapstoneProject.Database.Model.Base;
using CapstoneProject.Database.Model.Meta;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Database.Model
{
    [Table("CareCenter")]
    public class CareCenter : BaseModel<BaseStatus>
    {
        [Column("partner_id")]
        public Guid? PartnerId { get; set; }
        [Column("manager_id")]
        public Guid? ManagerId { get; set; }
        [Column("carecenter_name")]
        public string? CareCenterName { get; set; }
        [Column("list_images")]
        public string? ListImages { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("lattitude")]
        public string? Lattitude { get; set; }
        [Column("longtitude")]
        public string? Longtitude { get; set; }
        [Column("averate_rating")]
        public float AverageRating { get; set; }

        [ForeignKey("PartnerId")]
        public User? Partner { get; set; }
        [ForeignKey("ManagerId")]
        public User? Manager { get; set; }
    }
}
