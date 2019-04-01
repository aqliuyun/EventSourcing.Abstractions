using EventSourcing.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferDemo.Domain
{
    public class BankService : DomainService
    {
        public BankService(IAggregateRootRepository _repo) : base(_repo)
        {
        }

        public string CreateAccount()
        {
            var root = this.repo.CreateOne<Bank>();
            root.CreateAccount();
            return root.ID();
        }

        public void Transfer(string from, string to, decimal money)
        {
            var frommember = this.repo.Find<Bank>(from);
            frommember.Debit(money);
            var tomember = this.repo.Find<Bank>(to);
            tomember.Credit(money);
        }

        public decimal Display(string id)
        {
            var member = this.repo.Find<Bank>(id);
            return member.GetBalace();
        }
    }
}
