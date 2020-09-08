using PetShop.Infrastracture.Entity;

namespace PetShop.Core.DomainServices.Interfaces
{
    public interface IPetValidatorService: IBaseValidatorService
    {
        bool PetValidation(Pet pet);
    }
}
