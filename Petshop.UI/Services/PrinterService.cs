using PetShop.Core.AplicationServices.Interfaces;
using PetShop.Core.DomainServices.Interfaces;
using PetShop.Infrastracture.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.UI.Services
{
    public class PrinterService : IPrinterService
    {
        private readonly IPetService petService;
        private readonly IPetValidatorService petValidatorService;
        public PrinterService(IPetService petService, IPetValidatorService petValidatorService)
        {
            this.petService = petService;
            this.petValidatorService = petValidatorService;
        }

        public void Run()
        {
            var read = "";

            while (read != "0")
            {
                Console.WriteLine("choose one of the options");
                Console.WriteLine("1 list all pets");
                Console.WriteLine("2 add new pet");
                Console.WriteLine("3 remove pet");
                Console.WriteLine("4 update pet");
                Console.WriteLine("5 get pets by type");
                Console.WriteLine("6 sort pets by price");
                Console.WriteLine("7 show Five cheapest pets");
                Console.WriteLine("0 exit");
                Console.WriteLine("-------------------------------------------------");

                read = Console.ReadLine();

                switch (read)
                {
                    default:
                        Console.WriteLine("option not found try again \n"); ;
                        break;
                    case "1":
                        ListAllPets();
                        break;
                    case "2":
                        AddNewPet();
                        break;
                    case "3":
                        DeletePet();
                        break;
                    case "4":
                        UpdatePet();
                        break;
                    case "5":
                        GetPetsByType();
                        break;
                    case "6":
                        SortPetsByPrice();
                        break;
                    case "7":
                        ShowFiveCheapestPets();
                        break;
                    case "0": return;
                }
            }
        }

        private void AddNewPet()
        {
            var pet = new Pet();
            var read = "";
            bool isValid = false;

            #region [-Name-]
            while (!isValid)
            {
                Console.WriteLine("Please Enter Pet Name?");
                read = Console.ReadLine();
                if (petValidatorService.StringLenght(read, 3, 50))
                {
                    isValid = true;
                    pet.Name = read;
                }
                else
                {
                    Console.WriteLine("Ops!Input is not valid try again\r\n");
                }
            }
            #endregion

            #region [-Color-]
            isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Please Enter Pet Color?");
                read = Console.ReadLine();
                if (petValidatorService.StringLenght(read, 3, 50))
                {
                    isValid = true;
                    pet.Color = read;
                }
                else
                {
                    Console.WriteLine("Input is not valid try again\r\n");
                }
            }

            #endregion

            #region [-Birthdate-]
            isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Please Enter Pet Birthdate format(yyyy-MM-dd)?");
                read = Console.ReadLine();
                if (DateTime.TryParse(read, out DateTime birthdate))
                {
                    isValid = true;
                    pet.Birthdate = birthdate;
                }
                else
                {
                    Console.WriteLine("Input is not valid try again\r\n");
                }
            }
            #endregion

            #region [-Sold Date-]
            isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Please Enter Pet Sold Date format(yyyy-MM-dd)?\r\nyou can enter 0 for null");
                read = Console.ReadLine();
                if (read == "0")
                {
                    isValid = true;
                    break;
                }

                if (DateTime.TryParse(read, out DateTime soldDate))
                {
                    isValid = true;
                    pet.SoldDate = soldDate;
                }
                else
                {
                    Console.WriteLine("Input is not valid try again\r\n");
                }
            }
            #endregion

            #region [-Price-]

            isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Please Enter Pet Price");
                read = Console.ReadLine();
                if (long.TryParse(read, out long price))
                {
                    isValid = true;
                    pet.price = price;
                }
                else
                {
                    Console.WriteLine("Input is not valid try again\r\n");
                }
            }
            #endregion

            #region [-Type-]
            isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Please Enter Pet Type");
                foreach (var item in (Pet.PetType[])Enum.GetValues(typeof(Pet.PetType)))
                {
                    Console.WriteLine($"{(byte)item}) {item}");
                }
                byte.TryParse(Console.ReadLine(), out byte typeID);
                if (typeID < 1 || typeID > 4)
                {
                    Console.WriteLine("Please Select Correct Option");
                    continue;
                }
                isValid = true;
                pet.Type = (Pet.PetType)typeID;

            }

            #endregion

            #region [-CreateOwner-]
            pet.Owner = CreateOwner();
            #endregion

            #region [-PreviousOwner-]
            isValid = false;
            while (!isValid)
            {
                Console.WriteLine("This pet has previous owner? y or n");
                read = Console.ReadLine();
                read = read.ToLower();
                if (read == "y" || read == "n")
                {
                    isValid = true;
                    switch (read)
                    {
                        case "y":
                            pet.PreviousOwner = CreateOwner();
                            break;
                        case "n":
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Input is not valid try again\r\n");
                }


            } 
            #endregion

            var createPetResult = petService.Create(pet);

            Console.WriteLine($"Hurra pet was added {createPetResult}");

        }

        private void UpdatePet()
        {
            var list = petService.GetAll();
            var pet = new Pet();
            var read = "";
            bool isValid = false;

            #region [-Get Id-]
            while (!isValid)
            {
                ListAllPets();
                Console.WriteLine("\r\nPlease Enter Pet ID ?");
                read = Console.ReadLine();
                if (int.TryParse(read, out int id))
                {
                    if (list.Any(x => x.Id == id))
                    {
                        pet = list.FirstOrDefault(x => x.Id == id);
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Ops Pet Not Found\r\n");
                    }

                }
                else
                {
                    Console.WriteLine("Input is not valid try again\r\n");
                }
            }
            #endregion

            #region [-Name-]

            Console.Clear();
            Console.WriteLine($"Pet Name : {pet.Name}");
            if (AskEditThis())
            {
                Console.Clear();
                isValid = false;
                while (!isValid)
                {
                    Console.WriteLine("Please Enter Pet Name?");
                    read = Console.ReadLine();
                    if (petValidatorService.StringLenght(read, 3, 50))
                    {
                        isValid = true;
                        pet.Name = read;
                    }
                    else
                    {
                        Console.WriteLine("Input is not valid try again\r\n");
                    }
                }

            }

            #endregion

            #region [-Color-]
            Console.Clear();
            Console.WriteLine($"Pet Color : {pet.Color}");
            if (AskEditThis())
            {
                Console.Clear();
                isValid = false;
                while (!isValid)
                {
                    Console.WriteLine("Please Enter Pet Color?");
                    read = Console.ReadLine();
                    if (petValidatorService.StringLenght(read, 3, 50))
                    {
                        isValid = true;
                        pet.Color = read;
                    }
                    else
                    {
                        Console.WriteLine("Input is not valid try again\r\n");
                    }
                }
            }

            #endregion

            #region [-Birthdate-]
            Console.Clear();
            Console.WriteLine($"Pet Birthdate : {pet.Birthdate.ToString("yyyy-MMM-dd")}");
            if (AskEditThis())
            {
                Console.Clear();
                isValid = false;
                while (!isValid)
                {
                    Console.WriteLine("Please Enter Pet Birthdate format(yyyy-MM-dd)?");
                    read = Console.ReadLine();
                    if (DateTime.TryParse(read, out DateTime birthdate))
                    {
                        isValid = true;
                        pet.Birthdate = birthdate;
                    }
                    else
                    {
                        Console.WriteLine("Input is not valid try again\r\n");
                    }
                }
            }

            #endregion

            #region [-Sold Date-]
            Console.Clear();
            if (pet.SoldDate != null)
            {
                Console.WriteLine($"Pet Sold Date : {((DateTime)pet.SoldDate).ToString("yyyy-MMM-dd")}");
            }
            else
            {
                Console.WriteLine($"Pet Sold Date : null");
            }
            if (AskEditThis())
            {
                Console.Clear();
                isValid = false;
                while (!isValid)
                {
                    Console.WriteLine("Please Enter the Pet Sold Date format(yyyy-MM-dd)?\r\nyou can enter 0 for null");
                    read = Console.ReadLine();
                    if (read == "0")
                    {
                        isValid = true;
                        break;
                    }

                    if (DateTime.TryParse(read, out DateTime soldDate))
                    {
                        isValid = true;
                        pet.SoldDate = soldDate;
                    }
                    else
                    {
                        Console.WriteLine("Input is not valid try again\r\n");
                    }
                }
            }

            #endregion

            #region [-Price-]
            Console.Clear();
            Console.WriteLine($"Pet Price : {pet.price}");
            if (AskEditThis())
            {
                Console.Clear();
                isValid = false;
                while (!isValid)
                {
                    Console.WriteLine("Please Enter Pet Price");
                    read = Console.ReadLine();
                    if (long.TryParse(read, out long price))
                    {
                        isValid = true;
                        pet.price = price;
                    }
                    else
                    {
                        Console.WriteLine("Input is not valid try again\r\n");
                    }
                }

            }

            #endregion

            #region [-Type-]
            Console.Clear();
            Console.WriteLine($"Pet Type : {pet.Type}");
            if (AskEditThis())
            {
                Console.Clear();
                isValid = false;
                while (!isValid)
                {
                    Console.WriteLine("Please Enter Pet Type");
                    foreach (var item in (Pet.PetType[])Enum.GetValues(typeof(Pet.PetType)))
                    {
                        Console.WriteLine($"{(byte)item}) {item}");
                    }
                    byte.TryParse(Console.ReadLine(), out byte typeID);
                    if (typeID < 1 || typeID > 4)
                    {
                        Console.WriteLine("Please Select Correct Option");
                        continue;
                    }
                    isValid = true;
                    pet.Type = (Pet.PetType)typeID;

                }
            }
            #endregion

            #region [-Owner-]
            Console.Clear();
            Console.WriteLine($"Pet Owner is  : {pet.Owner.FirstName} {pet.Owner.LastName}");
            if (AskEditThis())
            {
                Console.Clear();
                pet.Owner = CreateOwner();
            } 
            #endregion

            Console.Clear();
            var createPetResult = petService.Update(pet);

            Console.WriteLine($"pet  Update {createPetResult}");

        }
        private Owner CreateOwner()
        {
            var isValid = false;
            var read = "";
            var owner = new Owner();
            while (!isValid)
            {
                Console.WriteLine("Please Enter Owner FirstName");
                read = Console.ReadLine();
                if (petValidatorService.StringLenght(read, 3, 50))
                {
                    isValid = true;
                    owner.FirstName = read;
                }
                else
                {
                    Console.WriteLine("Input is not valid try again\r\n");
                }

            }


            isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Please Enter Owner LastName");
                read = Console.ReadLine();
                if (petValidatorService.StringLenght(read, 3, 50))
                {
                    isValid = true;
                    owner.LastName = read;
                }
                else
                {
                    Console.WriteLine("Input is not valid try again\r\n");
                }

            }


            isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Please Enter Owner Address");
                read = Console.ReadLine();
                if (petValidatorService.StringLenght(read, 15, 200))
                {
                    isValid = true;
                    owner.Address = read;
                }
                else
                {
                    Console.WriteLine("Input is not valid try again\r\n");
                }

            }


            isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Please Enter Owner Email");
                read = Console.ReadLine();
                if (petValidatorService.ValidEmail(read))
                {
                    isValid = true;
                    owner.Email = read;
                }
                else
                {
                    Console.WriteLine("Input is not valid try again\r\n");
                }

            }


            isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Please Enter Owner PhoneNumber");
                read = Console.ReadLine();
                if (read.All(x => char.IsDigit(x)))
                {
                    isValid = true;
                    owner.PhoneNumber = read;
                }
                else
                {
                    Console.WriteLine("Input is not valid try again\r\n");
                }

            }
            return owner;

        }

        private bool AskEditThis()
        {
            var read = "";
            var isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Do you want edit this?\r\ny or n");
                read = Console.ReadLine();
                read = read.ToLower();
                if (read == "y" || read == "n")
                {
                    isValid = true;
                    switch (read)
                    {
                        case "y":
                            return true;
                        case "n":
                            return false;
                    }
                }
                else
                {
                    Console.WriteLine("Input is not valid try again\r\n");
                }
            }
            return false;
        }
        private void ListAllPets()
        {
            var list = petService.GetAll();
            foreach (var item in list)
            {
                Console.WriteLine($"id:{item.Id} name:{item.Name} ownername:{item.Owner.FirstName} Color:{item.Color} price: {item.price} Birthdate:{item.Birthdate.ToString("yyyy/MMM/dd")}");
            }
            Console.WriteLine();

        }

        private void GetPetsByType()
        {
            Console.WriteLine("Select one of the below Pet type");
            foreach (var item in (Pet.PetType[])Enum.GetValues(typeof(Pet.PetType)))
            {
                Console.WriteLine($"{(byte)item}) {item}");
            }
            byte.TryParse(Console.ReadLine(), out byte typeID);
            var result = petService.GetByType((Pet.PetType)typeID);
            foreach (var item in result)
            {
                Console.WriteLine($"id:{item.Id} name:{item.Name} price: {item.price.ToString("N0")}");

            }

            Console.WriteLine();
        }

        private void SortPetsByPrice()
        {
            var list = petService.SortAllPetsByPrice();
            foreach (var item in list)
            {
                Console.WriteLine($"id:{item.Id} name:{item.Name} price: {item.price}");
            }
            Console.WriteLine();
        }

        private void DeletePet()
        {
            Console.WriteLine("Please Enter Pet ID");
            int.TryParse(Console.ReadLine(), out int id);
            var result = petService.Delete(id);
            Console.WriteLine($"Delete result is {result} ");
            Console.WriteLine();
        }

        private void ShowFiveCheapestPets()
        {
            var list = petService.GetFiveCheapestPet();
            foreach (var item in list)
            {
                Console.WriteLine($"id:{item.Id} name:{item.Name} price: {item.price}");
            }
            Console.WriteLine();
        }
    }
}

