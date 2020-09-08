using PetShop.Core.DomainServices.Interfaces;
using PetShop.Infrastracture.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainServices.Services
{
   public class PetValidatorService: BaseValidatorService, IPetValidatorService
    {
        public bool PetValidation(Pet pet)
        {
            if (pet == null
                 || string.IsNullOrWhiteSpace(pet.Name)
                 || string.IsNullOrWhiteSpace(pet.Color))
                return false;
            return true;

        }

    
    }
}
