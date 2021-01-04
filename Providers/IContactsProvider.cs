using Projects.Providers.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projects.Providers
{
    public interface IContactsProvider
    {
        Task<bool> DeleteAsync(long id);
        Task<long> DeleteMultipleAsync(long[] ids);
        Task<ContactEntity> GetAsync(long id);
        IEnumerable<ContactEntity> GetValues();
        Task<long> InsertAsync(ContactEntity value);
        Task ReplaceAsync(int id, ContactEntity value);
    }
}