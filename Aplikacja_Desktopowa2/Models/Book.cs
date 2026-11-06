
using Aplikacja_Desktopowa2.Models;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja_Desktopowa2.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public int PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public Book()
        {
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
