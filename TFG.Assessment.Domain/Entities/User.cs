﻿using System.ComponentModel.DataAnnotations;

namespace TFG.Assessment.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
