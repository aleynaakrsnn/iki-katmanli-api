using System.Collections.Generic;

namespace webapiuyg.Models
{
    public interface IMusterilerRepository
    {
        IEnumerable<Musteriler> GetAllMusteriler();
        Musteriler GetMusteriById(int id);
        Musteriler AddMusteri(Musteriler musteriler);
        Musteriler UpdateMusteri(Musteriler musteriler);
        void DeleteMusteri(int? id);
    }
}
