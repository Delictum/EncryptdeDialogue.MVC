using EncryptedDialogue.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EncryptedDialogue.BL
{
    public sealed class Unity
    {
        private Dictionary<Type, ReaderWriterLockSlim> _lockers;
        private readonly DbContext _dbContext;

        private void InitializeLockers()
        {
            _lockers = new Dictionary<Type, ReaderWriterLockSlim>
            {
                {
                    typeof(Models.User), new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion)
                },
                {
                    typeof(Models.Dialogue), new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion)
                }
            };
        }

        public Unity()
        {
            var contextFactory = new EncryptedDialogContextFactory();
            _dbContext = contextFactory.CreateInstance();
            InitializeLockers();
        }

        public void SendMessage(Models.Dialogue dialogue)
        {

            Task.Factory.StartNew(() =>
            {
                var unitOfWork = new UnitOfWork(_dbContext, _lockers);
                unitOfWork.TryAddDialogue(dialogue);
            });
        }

        public bool CheckLogIn(string login, string password)
        {
            Expression<Func<Models.User, bool>> userSearchCriteria = x =>
                x.Login == login && x.Password == password;

            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            return unitOfWork.TryGet(userSearchCriteria, unitOfWork.UserRepository);
        }

        public ICollection<string> GetAllLoginUsers()
        {
            Expression<Func<Models.User, bool>> userSearchCriteria = x =>
                x.Login != null;
            ICollection<string> users = new List<string>();

            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            var listUsers = unitOfWork.GetAllEntity(userSearchCriteria, unitOfWork.UserRepository);
            foreach (var user in listUsers)
            {
                users.Add(user.Login);
            }

            return users;
        }

        public ICollection<Tuple<string, string, DateTime>> GetMessages(string recipient, string sender)
        {
            Expression<Func<Models.Dialogue, bool>> dialogueSearchCriteria = x =>
                (x.RecipientUser.Login == recipient && x.SenderUser.Login == sender) || (x.RecipientUser.Login == sender && x.SenderUser.Login == recipient);
            ICollection<Tuple<string, string, DateTime>> messages = new List<Tuple<string, string, DateTime>>();

            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            var listMessages = unitOfWork.GetAllEntity(dialogueSearchCriteria, unitOfWork.DialogueRepository);
            foreach (var message in listMessages)
            {
                messages.Add(new Tuple<string, string, DateTime>(message.SenderUser.Login, message.Message, message.DateTime));
            }

            return messages;
        }

        public void SendMessage(string recipient, string sender, string message)
        {
            var dialogue = new Models.Dialogue
            {
                DateTime = DateTime.Now, Message = message,
                RecipientUser = new Models.User { Login = recipient },
                SenderUser = new Models.User { Login = sender}
            };
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            unitOfWork.TryAddDialogue(dialogue);
        }

        ~Unity()
        {
            _dbContext.Dispose();
        }
    }
}


//public string GetClientName()
//{
//    Expression<Func<Models.Client, bool>> clientSearchCriteria = x => x.FirstName == "Slava";
//    using (var context = _contextFactory.CreateInstance())
//    {
//        var clientUnitOfWork = new UnitOfWorks.ClientUnitOfWork(context, _repositoryFactory,
//            ResolveLocker(typeof(Models.Client)));

//        var client = clientUnitOfWork.TryGet(clientSearchCriteria, true);
//        return client != null ? client.LastName : string.Empty;
//    }
//}

//public void AddClient()
//{
//    var newClient = new Models.Client { FirstName = "Ivan", LastName = "Novich" };
//    Expression<Func<Models.Client, bool>> clientSearchCriteria = x => x.FirstName == "Ivan" && x.LastName == "Novich";
//    using (var context = _contextFactory.CreateInstance())
//    {
//        var clientUnitOfWork = new UnitOfWorks.ClientUnitOfWork(context, _repositoryFactory,
//            ResolveLocker(typeof(Models.Client)));

//        clientUnitOfWork.TryAdd(newClient, clientSearchCriteria, true);
//    }
//}

//public void UpdateClient()
//{
//    Expression<Func<Models.Client, bool>> clientSearchCriteria = x => x.FirstName == "Ivan" && x.LastName == "Novich";
//    using (var context = _contextFactory.CreateInstance())
//    {
//        var clientUnitOfWork = new UnitOfWorks.ClientUnitOfWork(context, _repositoryFactory,
//            ResolveLocker(typeof(Models.Client)));

//        var client = clientUnitOfWork.TryEntityGet(clientSearchCriteria, true);
//        client.FirstName = "Slava";
//        clientUnitOfWork.TryUpdate(client, clientSearchCriteria, true);
//    }
//}

//public void RemoveClient()
//{
//    Expression<Func<Models.Client, bool>> clientSearchCriteria = x => x.FirstName == "Slava" && x.LastName == "Novich";
//    using (var context = _contextFactory.CreateInstance())
//    {
//        var clientUnitOfWork = new UnitOfWorks.ClientUnitOfWork(context, _repositoryFactory,
//            ResolveLocker(typeof(Models.Client)));

//        var client = clientUnitOfWork.TryEntityGet(clientSearchCriteria, true);
//        clientUnitOfWork.TryRemove(client, clientSearchCriteria, true);
//    }
//}