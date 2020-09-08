using PetShop.Core.DomainServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PetShop.Core.DomainServices.Services
{
   public class BaseValidatorService: IBaseValidatorService
    {
        public bool StringLenght(string data, int minLenght, int maxLenght)
        {
            if (data == null)
                return false;

            if (minLenght < 0)
                throw new Exception("minLenght is not valid");

            if (maxLenght < 0)
                throw new Exception("maxLenght is not valid");

            if (data.Length < minLenght || data.Length > maxLenght) 
                return false;
            return true;
        }

        public bool ValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase);
        }
    }
}
