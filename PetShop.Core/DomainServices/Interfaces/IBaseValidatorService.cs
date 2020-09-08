using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainServices.Interfaces
{
    public interface IBaseValidatorService
    {
        bool StringLenght(string data, int minLenght, int maxLenght);
        bool ValidEmail(string email);
    }
}
