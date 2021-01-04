using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projects.Providers.Database;

namespace Projects.Providers
{
    public class ContactsProvider : IContactsProvider
    {
        private MyContext _dbContext;

        public ContactsProvider(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ContactEntity> GetValues()
        {
            return _dbContext.Contacts;
        }

        public async Task<ContactEntity> GetAsync(long id)
        {
            return await _dbContext.Contacts.FindAsync(id);
        }

        public async Task<long> InsertAsync(ContactEntity value)
        {
            await _dbContext.AddAsync(value);
            await _dbContext.SaveChangesAsync();
            return value.Id;
        }

        public async Task ReplaceAsync(int id, ContactEntity value)
        {
            _dbContext.Update(value);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _dbContext.Contacts.FindAsync(id);
            if (entity != null)
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<long> DeleteMultipleAsync(long[] ids)
        {
            long c = 0;
            foreach (long id in ids)
            {
                c += await DeleteAsync(id) ? 1 : 0;
            }
            return c;
        }
    }
}
