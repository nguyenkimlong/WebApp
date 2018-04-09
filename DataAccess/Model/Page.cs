﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccess.Model
{
    [Table("Pages")]
    public class Page : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Column(TypeName = "varchar(50)")]
        [MaxLength(256)]
        [Required]
        public string Alias { set; get; }

        public string Content { set; get; }
    }
}