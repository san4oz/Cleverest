﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Business.InterfaceDefinitions.Providers;
using Cleverest.Core.Security;

namespace Cleverest.Business.Managers
{
    public class AccountManager: BaseManager<Account, IAccountProvider>, IAccountManager
    {
        protected ITeamManager TeamManager { get; }

        public AccountManager(ITeamManager manager)
        {
            this.TeamManager = manager;
        }

        public override void Update(Account entity)
        {
            var oldAccount = Provider.Get(entity.Id);

            TryAddToTeam(entity.Id, entity.TeamId);
            

            Provider.Update(entity.Id, entityToUpdate =>
            {
                entityToUpdate.Email = entity.Email;
                entityToUpdate.Name = entity.Name;
                entityToUpdate.TeamId = entity.TeamId;
                entityToUpdate.Password = entity.Password;
                entityToUpdate.PasswordSalt = entityToUpdate.PasswordSalt;
                entityToUpdate.PhoneNumber = entity.PhoneNumber;
                entityToUpdate.SocialNetworkLink = entity.SocialNetworkLink;

                TryUpdatePassword(entityToUpdate, oldAccount);
            });
        }

        public IList<Account> GetAccountsByTeamId(string teamId)
        {
            return Provider.GetAccountsByTeamId(teamId);
        }

        public Account GetByEmail(string email)
        {
            return Provider.GetByEmail(email);
        }

        public override void Create(Account entity)
        {
            var crypto = new CryptoHelper();

            entity.PasswordSalt = crypto.GenerateSalt();
            entity.Password = crypto.GenerateHash(entity.Password, entity.PasswordSalt);

            base.Create(entity);
        }

        protected bool TryUpdatePassword(Account accountToUpdate, Account oldAccount)
        {
            if (oldAccount == null)
                return false;

            var crypto = new CryptoHelper();
            var cryptedNewPassword = crypto.GenerateHash(accountToUpdate.Password, oldAccount.PasswordSalt);

            if (string.IsNullOrEmpty(accountToUpdate.Password))
            {
                accountToUpdate.Password = oldAccount.Password;
                accountToUpdate.PasswordSalt = oldAccount.PasswordSalt;
            }
            else if(!oldAccount.Password.Equals(cryptedNewPassword))
            {
                accountToUpdate.PasswordSalt = oldAccount.PasswordSalt;
                accountToUpdate.Password = cryptedNewPassword;
            }

            return true;
        }

        protected bool TryAddToTeam(string accountId, string teamId)
        {
            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(teamId))
                return false;

            if (TeamManager.GetTeamsByAccountId(accountId).Select(x => x.Id).Contains(teamId))
                return false;
          
            Provider.CreateAccountTeamPermission(new AccountTeamPermission() { AccountId = accountId, TeamId = teamId });

            return true;
        }

        public void ProccessAccountTeamRequests(string requestId, bool isApproved)
        {
            if (string.IsNullOrEmpty(requestId))
                return;

            if (!isApproved)
            {
                Provider.DeleteAccountTeamRequest(requestId);
                return;
            }

            var request = Provider.GetAccountTeamRequest(requestId);
            if (requestId == null)
                return;

            var accountTeamPermission = new AccountTeamPermission()
            {
                AccountId = request.FromId,
                TeamId = request.TeamId
            };

            Provider.CreateAccountTeamPermission(accountTeamPermission);
            Provider.DeleteAccountTeamRequest(requestId);
        }

        public IList<AccountTeamRequest> GetRequestsByReceiverId(string receiverId)
        {
            return Provider.GetRequestsByReceiverId(receiverId);
        }

        public void CreateAccountTeamRequest(AccountTeamRequest request)
        {
            Provider.CreateAccountTeamRequest(request);
        }

        public void CreateAccountTeamPermission(AccountTeamPermission permission)
        {
            Provider.CreateAccountTeamPermission(permission);
        }
    }
}
