using System;
using System.Threading.Tasks;
using Nethereum.HdWallet;
using Nethereum.Web3;

namespace EthereumWallet
{
    class Program
    {
        public static readonly string SERVER = System.Environment.GetEnvironmentVariable("SERVER");
        static async Task Main(string[] args)
        {
            DotNetEnv.Env.Load("./.env");
            // var wallet = new Wallet(NBitcoin.Wordlist.English, NBitcoin.WordCount.TwentyFour);
            var wallet = new Wallet(System.Environment.GetEnvironmentVariable("WALLET_SECRET"), "");
            var account = wallet.GetAccount(0);
            var memo = string.Join(" ", wallet.Words);
            Console.WriteLine(account.Address);
            await CheckBalance(account.Address);
        }

        //check Balance
        public static async Task CheckBalance(string address)
        {
            var client = new Web3(SERVER);
            var ethBalance = await client.Eth.GetBalance.SendRequestAsync(address);
            var balance = Web3.Convert.FromWei(ethBalance.Value);
            Console.WriteLine($"Balance : {balance}");
        }
    }
}
