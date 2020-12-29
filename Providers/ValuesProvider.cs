using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projects.Providers.Database;

namespace Projects.Providers
{
    public class ValuesProvider : IValuesProvider
    {
        private MyContext _dbContext;

        public ValuesProvider(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<string> GetValues()
        {
            return _dbContext.Values.Select(x => x.Value);
        }

        public async Task<string> GetAsync(int id)
        {
            var entity = await _dbContext.Values.FindAsync(id);
            return entity?.Value;
        }

        public async Task<int> InsertAsync(string value)
        {
            var entity = new ValueEntity() {Value = value};
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task ReplaceAsync(int id, string value)
        {
            var entity = new ValueEntity() {Id = id, Value = value};
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.Values.FindAsync(id);
            if (entity != null)
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> DeleteMultipleAsync(int[] ids)
        {
            int c = 0;
            foreach (int id in ids)
            {
                c += await DeleteAsync(id) ? 1 : 0;
            }
            return c;
        }
    }
}
