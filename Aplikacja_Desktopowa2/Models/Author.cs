
using Aplikacja_Desktopowa2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja_Desktopowa2.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new HashSet<Book>();
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
