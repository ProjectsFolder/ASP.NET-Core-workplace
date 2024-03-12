﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebTest.Models.OrgStructure
{
    [Table("users")]
    public class User : ModelBase
    {
        [Column("login")]
        public string Login { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("email")]
        public string? Email { get; set; } = null;
    }
}
