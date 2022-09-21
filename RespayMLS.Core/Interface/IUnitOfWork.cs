using System;

namespace RespayMLS.Core.Interface
{
    public interface IUnitOfWork
    {
        IAreaRepository Area { get; }

        ICityRepository City { get; }

        IStreetRepository Street { get; }

        IEstateRepository Estate { get; }

        ICountryRepository Country { get; }

        IStateRepository State { get; }

        int Complete();
    }
}
