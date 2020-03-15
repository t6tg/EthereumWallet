using System;
using System.Threading.Tasks;
using Nethereum.HdWallet;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

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
            var receiverAccount = wallet.GetAccount(1);
            // var memo = string.Join(" ", wallet.Words);
            // Console.WriteLine(receiverAccount.Address);
            // await CheckBalance(receiverAccount.Address);
            await Transfer(account, receiverAccount.Address, 1);
            Console.WriteLine($"MainAddress : {account.Address}");
            await CheckBalance(account.Address);
            Console.WriteLine($"ReceiverAddress : {receiverAccount.Address}");
            await CheckBalance(receiverAccount.Address);
        }

        //check Balance
        public static async Task CheckBalance(string address)
        {
            var client = new Web3(SERVER);
            var ethBalance = await client.Eth.GetBalance.SendRequestAsync(address);
            var balance = Web3.Convert.FromWei(ethBalance.Value);
            Console.WriteLine($"Balance : {balance}");
        }

        //Transfer
        public static async Task Transfer(Account fromAccount, string receiverAddress, decimal amount)
        {
            var client = new Web3(fromAccount, SERVER);
            var transaction = await client.Eth.GetEtherTransferService().TransferEtherAndWaitForReceiptAsync(receiverAddress, amount);
            Console.WriteLine(transaction.TransactionHash);
        }
    }
}
