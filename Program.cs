using System;
using Nethereum.HdWallet;

namespace EthereumWallet
{
    class Program
    {
        static void Main(string[] args)
        {
            DotNetEnv.Env.Load("./.env");
            // var wallet = new Wallet(NBitcoin.Wordlist.English, NBitcoin.WordCount.TwentyFour);
            var wallet = new Wallet(System.Environment.GetEnvironmentVariable("WALLET_SECRET"), "");
            var account = wallet.GetAccount(0);
            var memo = string.Join(" ", wallet.Words);
            Console.WriteLine(account.Address);
        }

        //check Balance
        public static void CheckBalance(string address)
        {

        }
    }
}
