using PetShop.Infrastracture.Entity;
using System.Collections.Generic;

namespace PetShop.Core.DomainServices.Interfaces
{
    public interface IPetRepository
    {
        void Init();
        bool Create(Pet model);
        bool Update(Pet model);
        bool Delete(int Id);
        Pet GetById(int Id);
        IEnumerable<Pet> GetByType(Pet.PetType petType);
        IEnumerable<Pet> GetAll();
        IEnumerable<Pet> GetFiveCheapestPet();
    }
}
