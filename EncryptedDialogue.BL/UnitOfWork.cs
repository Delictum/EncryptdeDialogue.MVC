using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using EncryptedDialogue.BL.Models;
using EncryptedDialogue.BL.Support;
using EncryptedDialogue.DAL;
using EncryptedDialogue.DAL.Contracts;

namespace EncryptedDialogue.BL
{
    public class UnitOfWork : IDisposable
    {
        protected DbContext Context { get; }
        public IRepository<EF.User> UserRepository { get; set; }
        public IRepository<EF.Dialogue> DialogueRepository { get; set; }
        protected IDictionary<Type, ReaderWriterLockSlim> Lockers { get; }

        public UnitOfWork(DbContext context, IDictionary<Type, ReaderWriterLockSlim> lockers)
        {
            Context = context;
            InitializeRepository();
            Lockers = lockers;
        }

        private void InitializeRepository()
        {
            UserRepository = new RepositoryFactory().CreateInstanceUser(Context);
            DialogueRepository = new RepositoryFactory().CreateInstanceDialogue(Context);
        }

        public ReaderWriterLockSlim ResolveLocker(Type modelType)
        {
            return Lockers[modelType];
        }

        public IQueryable<TEntity> GetAllEntity<TEntity, TModel>(Expression<Func<TModel, bool>> searchExpression, IRepository<TEntity> repository) where TEntity : class where TModel : class
        {
            var newSearchExpression = searchExpression.Project<TModel, TEntity>();

            ResolveLocker(typeof(TModel)).EnterReadLock();
            try
            {
                return repository.Get(newSearchExpression);
            }
            finally
            {
                ResolveLocker(typeof(TModel)).ExitReadLock();
            }
        }

        public TEntity GetEntity<TEntity, TModel>(Expression<Func<TModel, bool>> searchExpression, IRepository<TEntity> repository) where TEntity : class where TModel : class
        {
            var newSearchExpression = searchExpression.Project<TModel, TEntity>();

            ResolveLocker(typeof(TModel)).EnterReadLock();
            try
            {
                return repository.Get().FirstOrDefault(newSearchExpression);
            }
            finally
            {
                ResolveLocker(typeof(TModel)).ExitReadLock();
            }
        }

        public bool TryGet<TEntity, TModel>(Expression<Func<TModel, bool>> searchExpression, IRepository<TEntity> repository) where TEntity : class where TModel : class
        {
            var newSearchExpression = searchExpression.Project<TModel, TEntity>();

            ResolveLocker(typeof(TModel)).EnterReadLock();
            try
            {
                return repository.Get().Any(newSearchExpression);
            }
            finally
            {
                ResolveLocker(typeof(TModel)).ExitReadLock();
            }
        }

        public int GetEntityId<TEntity, TModel>(Expression<Func<TModel, bool>> searchExpression, IRepository<TEntity> repository) where TEntity : class where TModel : class
        {
            var newSearchExpression = searchExpression.Project<TModel, TEntity>();

            ResolveLocker(typeof(TModel)).EnterReadLock();
            try
            {
                return repository.GetId(newSearchExpression);
            }
            finally
            {
                ResolveLocker(typeof(TModel)).ExitReadLock();
            }
        }

        public bool TryAddUser(User user, Expression<Func<User, bool>> searchExpression)
        {
            ResolveLocker(typeof(User)).EnterWriteLock();
            try
            {
                var searchedUser = GetEntity(searchExpression, UserRepository);
                if (searchedUser != null)
                {
                    return false;
                }

                UserRepository.Add(new EF.User { Login = user.Login, Password = user.Password });
                UserRepository.Save();
                return true;
            }
            finally
            {
                ResolveLocker(typeof(User)).ExitWriteLock();
            }
        }

        public bool TryAddDialogue(Dialogue dialogue)
        {
            Expression<Func<User, bool>> recipientUserSearchCriteria = x =>
                x.Login == dialogue.RecipientUser.Login;
            var recipientUserId = GetEntityId(recipientUserSearchCriteria, UserRepository);

            Expression<Func<User, bool>> senderUserSearchCriteria = x =>
                x.Login == dialogue.SenderUser.Login;
            var senderUserId = GetEntityId(senderUserSearchCriteria, UserRepository);

            DialogueRepository.Add(new EF.Dialogue
            {
                Message = dialogue.Message,
                DateTime = dialogue.DateTime,
                RecipientId = recipientUserId,
                SenderId = senderUserId
            });
            DialogueRepository.Save();
            return true;
        }

        public void TryRemove<TEntity, TModel>(Expression<Func<TModel, bool>> searchExpression, IRepository<TEntity> repository) where TEntity : class where TModel : class
        {
            ResolveLocker(typeof(TModel)).EnterWriteLock();
            try
            {
                var searchedElement = GetEntity<TEntity, TModel>(searchExpression, repository);
                if (searchedElement == null)
                {
                    return;
                }

                repository.Remove(searchedElement);
            }
            finally
            {
                ResolveLocker(typeof(TModel)).ExitWriteLock();
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
//public void TryUpdate<TEntity, TModel>(Expression<Func<TModel, bool>> searchExpression, IRepository<TEntity> repository) where TEntity : class where TModel : class
//{
//ResolveLocker(typeof(TModel)).EnterWriteLock();
//try
//{
//var searchedElement = GetEntity<TEntity, TModel>(searchExpression, repository);
//if (searchedElement == null)
//{
//return;
//}
//repositoryElement.Update(entityElement);
//_context.SaveChanges();
//}
//finally
//{
//ResolveLocker(typeof(TModel)).ExitWriteLock();
//}
//}