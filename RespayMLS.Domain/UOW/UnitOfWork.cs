using RespayMLS.Core.Interface;
using RespayMLS.Data;
using RespayMLS.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespayMLS.Domain.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RespayDbContext _respayDbContext;

        public UnitOfWork(RespayDbContext respayDbContext)
        {
            _respayDbContext = respayDbContext;

            Estate = new EstateRepository(_respayDbContext);

            Area = new AreaRepository(_respayDbContext);

            City = new CityRepository(_respayDbContext);

            Street = new StreetRespository(_respayDbContext);

            State = new StateRepository(respayDbContext);

            Country = new CountryRepository(respayDbContext);
        }
        public IAreaRepository Area {get; private set;}

        public ICityRepository City {get; private set;}

        public IStreetRepository Street {get; private set;}

        public IEstateRepository Estate {get; private set;}

        public ICountryRepository Country {get; private set;}

        public IStateRepository State { get; private set; }

        public int Complete()
        {
           return _respayDbContext.SaveChanges();
        }

        
    }
}
