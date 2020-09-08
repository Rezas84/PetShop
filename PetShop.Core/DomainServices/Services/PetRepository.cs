using PetShop.Core.DomainServices.Interfaces;
using PetShop.Infrastracture.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.Core.DomainServices.Services
{
    public class PetRepository: IPetRepository
    {
        public PetRepository()
        {

        }
        public void Init()
        {
            var reza = new Owner {
                Address = "aaaaa",
                Email = "test@gmail.com",
                Id = 1,
                FirstName = "Reza",
                LastName = "Sarazfraz",
                PhoneNumber = "+4500000"
            };
            Create(new Pet
            {
                Color = "Black",
                Name = "SomeName",
                Owner = reza,
                Birthdate = DateTime.Now.AddMonths(-12),
                price=1200,
                Type=Pet.PetType.Dog
            });
            Create(new Pet
            {
                Color = "Gray",
                Name = "Alex",
                Owner = reza,
                Birthdate = DateTime.Now.AddMonths(-24),
                price = 1000,
                Type = Pet.PetType.Cat
            });Create(new Pet
            {
                Color = "Brown",
                Name = "Lexi",
                Owner = reza,
                Birthdate = DateTime.Now.AddMonths(-44),
                price = 8000,
                Type = Pet.PetType.Cat
            });

        }
        public bool Create(Pet model)
        {
            try
            {
                if (DbContext.Pets == null)
                    DbContext.Pets = new List<Pet>();
                var Id = 1;
                if (DbContext.Pets.Count() > 0)
                {
                    Id = Convert.ToInt32(DbContext.Pets.Max(x => x.Id) + 1);
                }
                model.Id = Id;
                DbContext.Pets.Add(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(Pet model)
        {
            try
            {
                if (DbContext.Pets == null)
                    return false;

                var oldEntity = DbContext.Pets.FirstOrDefault(x => x.Id == model.Id);
                if (oldEntity == null)
                    return false;
                if (oldEntity.Owner?.Id!=model.Owner?.Id)
                    oldEntity.PreviousOwner = oldEntity.Owner;

                oldEntity.Birthdate = model.Birthdate;
                oldEntity.Color = model.Color;
                oldEntity.Name = model.Name;
                oldEntity.Owner = model.Owner;
                oldEntity.price = model.price;
                oldEntity.SoldDate = model.SoldDate;
                oldEntity.Type = model.Type;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int Id)
        {
            try
            {
                if (DbContext.Pets == null)
                    return false;

                var oldEntity = DbContext.Pets.FirstOrDefault(x => x.Id == Id);
                if (oldEntity == null)
                    return false;
                DbContext.Pets.Remove(oldEntity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Pet GetById(int Id)
        {
            var model= DbContext.Pets?.FirstOrDefault(x=>x.Id==Id);
            if (model == null)
                throw new System.Exception("Item Not Exists!");
            return model;
        }
        public IEnumerable<Pet> GetByType(Pet.PetType petType)
        {
            var list = DbContext.Pets?.Where(x => x.Type == petType);
            if (list == null)
                throw new System.Exception("Item Not Exists!");
            return list;
        }
        public IEnumerable<Pet> GetAll()
        {
            return DbContext.Pets;
        }
        public IEnumerable<Pet> GetFiveCheapestPet()
        {
            return DbContext.Pets?.Where(x=>x.SoldDate==null).OrderBy(x=>x.price).Take(5);
        }
    }
}
