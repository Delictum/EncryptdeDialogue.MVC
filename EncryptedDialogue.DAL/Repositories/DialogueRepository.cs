using EncryptedDialogue.EF;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EncryptedDialogue.DAL.Repositories
{
    public class DialogueRepository : BaseRepository<Dialogue>
    {
        public DialogueRepository()
        {

        }

        public DialogueRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<Dialogue>();
        }

        public override int GetId(Expression<Func<Dialogue, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate).Id;
        }
    }
}
