using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models
{
    [Table("Book")]
    public class Book
    {

        [Key]
        public int Id { get; set; }

        //[Required]
        [Required(ErrorMessage ="Name is required")]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }
    }
}
