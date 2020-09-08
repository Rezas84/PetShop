using PetShop.Core.DomainServices.Interfaces;
using PetShop.Infrastracture.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Core.DomainServices.Services
{
    public class PetService: IPetService
    {
        private readonly IPetValidatorService petValidationService;
        private readonly IPetRepository petRepository;
        public PetService(IPetValidatorService petValidationService, IPetRepository petRepository)
        {
            this.petValidationService = petValidationService;
            this.petRepository = petRepository;
        }

        public bool Create(Pet model)
        {
            if (petValidationService.PetValidation(model))
                return petRepository.Create(model);
            return false;
        }
        public bool Update(Pet model)
        {
            if (petValidationService.PetValidation(model))
                return petRepository.Update(model);
            return false;
        }
        public bool Delete(int Id)
        {
            return petRepository.Delete(Id);
        }
        public Pet GetById(int Id)
        {
         return petRepository.GetById(Id);
        }
        public IEnumerable<Pet> GetByType(Pet.PetType petType)
        {
            return petRepository.GetByType(petType);
        }
        public IEnumerable<Pet> GetAll()
        {
            return petRepository.GetAll();
        }
        public IEnumerable<Pet> GetFiveCheapestPet()
        {
            return petRepository.GetFiveCheapestPet();
        }
        public IEnumerable<Pet> SortAllPetsByPrice()
        {
            return petRepository.GetAll()?.OrderBy(x=>x.price);
        }
    }
}
