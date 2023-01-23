using System.Collections.Generic;

namespace webapiuyg.Models
{
    public interface IHizmetlerRepository
    {
        IEnumerable<Hizmetler> GetAllHizmet();
        Hizmetler GetHizmetById(int id);
        Hizmetler AddHizmet(Hizmetler hizmetler);
        Hizmetler UpdateHizmet(Hizmetler hizmetler);
        void DeleteHizmet(int? id);
    }
}
