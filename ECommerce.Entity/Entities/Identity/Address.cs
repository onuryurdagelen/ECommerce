﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.Entity.Entities.Identity
{
    public class Address:IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => this.FirstName + " " + this.LastName; 
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}