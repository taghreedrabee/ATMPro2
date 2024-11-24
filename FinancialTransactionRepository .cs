using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Text.Json;
using System.IO;

namespace ATMapp
{
    internal class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        string filePath = "financialTransactions.json";
        private List<TransactionInfo> _financialTransactions = new List<TransactionInfo>();

        private static readonly HashSet<string> FinancialTransactionTypes = new HashSet<string>
            {
                "Deposit",
                "Withdrawal",
                "Balance Check",
                "Transfer (Sent)",
                "Transfer (Received)",
                "Transfer (Sent - Pending)",
                "Transfer Cancelled - User Deletion",
                "Transfer Returned - Recipient Deleted",
                "Transfer (Refunded)",
                "Transfer Accepted",
                "Transfer Rejected"
            };

        public FinancialTransactionRepository()
        {
            LoadTransactions();
        }
        public void AddTransaction(TransactionInfo transaction)
        {
            if (FinancialTransactionTypes.Contains(transaction.TransactionName))
            {
                _financialTransactions.Add(transaction);
                SaveTransactions();
            }
        }

        public IEnumerable<TransactionInfo> GetTransactionsByUserId(Guid userId)
        {
            return _financialTransactions.Where(t => t.UserId == userId).ToList();
        }
        public void DeleteTransaction(Guid transactionId)
        {
            var transaction = _financialTransactions.FirstOrDefault(t => t.TransactionId == transactionId);
            if (transaction != null)
            {
                _financialTransactions.Remove(transaction);
                SaveTransactions();
            }
        }

        private void LoadTransactions()
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                _financialTransactions = JsonSerializer.Deserialize<List<TransactionInfo>>(jsonString);
            }
        }

        private void SaveTransactions()
        {
            string jsonString = JsonSerializer.Serialize(_financialTransactions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }
    }


}
