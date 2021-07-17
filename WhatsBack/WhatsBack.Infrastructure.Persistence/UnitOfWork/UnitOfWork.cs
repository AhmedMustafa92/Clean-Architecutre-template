using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WhatsBack.Application.Interfaces;
using WhatsBack.Domain.Common;
using WhatsBack.Infrastructure.Persistence.Contexts;
using WhatsBack.SharedKernal.ResourcesReader.Messages;

namespace WhatsBack.Infrastructure.Persistence.UnitOfWork
{
    internal class UnitOfWork: IUnitOfWork
    {
        private ApplicationDbContext AppDbContext { get; set; }
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;
         public readonly IMessageResourceReader _messageResourceReader;
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="ElectricityCorrespondenceContext"/></param>
        public UnitOfWork(ApplicationDbContext appContext, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser, IMessageResourceReader messageResourceReader)
        {
            AppDbContext = appContext;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
            _messageResourceReader = messageResourceReader;
        }
        public async Task<bool> Commit()
        {
            try
            {
                foreach (var entry in AppDbContext.ChangeTracker.Entries<AuditableBaseEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.Created = _dateTime.NowUtc;
                            entry.Entity.CreatedBy = _authenticatedUser.UserId;
                            entry.Entity.IsDeleted = default;
                            break;
                        case EntityState.Modified:
                            entry.Entity.LastModified = _dateTime.NowUtc;
                            entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                            break;
                        case EntityState.Deleted:
                            entry.Entity.LastModified = _dateTime.NowUtc;
                            entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                            entry.Entity.IsDeleted = true;
                            break;
                    }
                }
                return await AppDbContext.SaveChangesAsync() > default(byte);
            }
            catch (Exception exception)
            {
                AppDbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                return default;
            }
        }

        /// <summary>
        /// Commit changes and throw error if commit failed
        /// </summary>
        /// <param name="_culture">Culture to throw the exception message with.</param
        public async Task<int> Commit(string _culture)
        {
            try
            {
                foreach (var entry in AppDbContext.ChangeTracker.Entries<AuditableBaseEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.Created = _dateTime.NowUtc;
                            entry.Entity.CreatedBy = _authenticatedUser.UserId;
                            break;
                        case EntityState.Modified:
                            entry.Entity.LastModified = _dateTime.NowUtc;
                            entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                            break;
                    }
                }
                int status = await AppDbContext.SaveChangesAsync();
                if (status <= default(int))
                    throw new Exception(_messageResourceReader.CommitFailed(_culture));
                return status;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public async Task<bool> Transaction(Action action)
        {
            bool isCommitted = default;
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = null;
            try
            {
                transaction = AppDbContext.Database.BeginTransaction();
                action.Invoke();
                await transaction.CommitAsync();
            }
            catch (Exception exception)
            {
                await transaction.RollbackAsync();
            }
            return isCommitted;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }

}
