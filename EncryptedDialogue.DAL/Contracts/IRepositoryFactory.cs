using System.Data.Entity;
using EncryptedDialogue.EF;

namespace EncryptedDialogue.DAL.Contracts
{
    public interface IRepositoryFactory
    {
        IRepository<User> CreateInstanceUser(DbContext context);
        IRepository<Dialogue> CreateInstanceDialogue(DbContext context);
    }
}
