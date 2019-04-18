using EncryptedDialogue.EF;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EncryptedDialogue.DAL.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository()
        {

        }

        public UserRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<User>();
        }

        public override int GetId(Expression<Func<User, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate).Id;
        }
    }
}
