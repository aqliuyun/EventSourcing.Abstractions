using BankTransferDemo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSourcing.Abstractions;
using Microsoft.Extensions.DependencyInjection;
namespace BankTransferDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var provider = EventSourcing.Abstractions.EventSourcing.Intergration((x) => {
                x.AddSingleton<BankService>();
                x.AddSingleton<IAggregateRootStoreProvider>(new MemoryStorageProvider());
            });
            var bank = provider.GetService<BankService>();
            var fromacc = bank.CreateAccount();
            var toacc = bank.CreateAccount();
            Console.WriteLine(bank.Display(fromacc));
            Console.WriteLine(bank.Display(toacc));
            bank.Transfer(fromacc, toacc, 10);
            Console.WriteLine(bank.Display(fromacc));
            Console.WriteLine(bank.Display(toacc));
            Console.ReadKey();
        }
    }
}
