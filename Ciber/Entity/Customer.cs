using System.ComponentModel.DataAnnotations;

namespace Ciber.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool KeepLoggedIn { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}