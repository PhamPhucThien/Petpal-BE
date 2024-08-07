﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.DTO.Request.Pet
{
    public class PetUpdateRequest
    {
        [Required(ErrorMessage = "ID is required")]
        public string Id { get; set; }
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Type ID is required")]
        public string PetTypeId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "CreateBy is required")]
        public string UpdatedBy { get; set; }
    }
}
