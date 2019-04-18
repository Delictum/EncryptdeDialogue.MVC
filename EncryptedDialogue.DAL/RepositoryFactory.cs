using EncryptedDialogue.DAL.Contracts;
using EncryptedDialogue.DAL.Repositories;
using EncryptedDialogue.EF;
using System.Data.Entity;

namespace EncryptedDialogue.DAL
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<User> CreateInstanceUser(DbContext context)
        {
            return new UserRepository(context);
        }

        public IRepository<Dialogue> CreateInstanceDialogue(DbContext context)
        {
            return new DialogueRepository(context);
        }
    }
}
