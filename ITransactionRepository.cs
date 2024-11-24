using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMapp
{
    internal interface ITransactionRepository
    {
        void AddTransaction(TransactionInfo transaction);
        void DeleteTransaction(Guid transactionId);
        IEnumerable<TransactionInfo> GetTransactionsByUserId(Guid userId);
        IEnumerable<TransactionInfo> GetFinancialTransactions(Guid userId);
        IEnumerable<TransactionInfo> GetManagerialTransactions(Guid userId);
    }

   

}
