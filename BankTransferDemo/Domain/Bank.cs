using BankTransferDemo.Domain.Entity;
using EventSourcing.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferDemo.Domain
{
    public class Bank : AggregateRoot<Guid>
    {
        public Bank()
        {
            this.UniqueID = Guid.NewGuid();
        }

        private Account account;

        public Account CreateAccount()
        {
            return account = new Account() { AccountID = 1, Balance = 1000.0m };
        }

        public decimal Credit(decimal money)
        {
            return account.Balance += money;
        }

        public decimal Debit(decimal money)
        {
            return account.Balance -= money;
        }

        public decimal GetBalace()
        {
            return account.Balance;
        }
    }
}
