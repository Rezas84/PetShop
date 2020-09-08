using PetShop.Infrastracture.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainServices.Interfaces
{
    public interface IPetService
    {
        bool Create(Pet model);
        bool Update(Pet model);
        bool Delete(int Id);
        Pet GetById(int Id);
        IEnumerable<Pet> GetByType(Pet.PetType petType);
        IEnumerable<Pet> GetAll();
        IEnumerable<Pet> GetFiveCheapestPet();
        IEnumerable<Pet> SortAllPetsByPrice();
    }
}
