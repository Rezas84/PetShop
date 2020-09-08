using System.Collections.Generic;

namespace PetShop.Infrastracture.Entity
{
    public class DbContext
    {
        public DbContext()
        {
            if (Pets is null)
                Pets = new List<Pet>();
            if (Owners is null)
                Owners = new List<Owner>();
        }
        public static List<Pet> Pets { get; set; }
        public static List<Owner> Owners { get; set; }
    }
}
