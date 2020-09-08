using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastracture.Entity
{
    public class Pet: BaseEntity
    {
        public enum PetType:byte
        {
            Dog=1,
            Cat=2,
            Fish=3,
            Bird=4,
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public PetType Type { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime? SoldDate { get; set; }
        public string Color { get; set; }
        public Owner Owner { get; set; }
        public Owner PreviousOwner { get; set; }
        public double price { get; set; }

    }
}
