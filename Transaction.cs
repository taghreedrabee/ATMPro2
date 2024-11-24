using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMapp
{
    public enum TransactionType
    {
        Managerial,
        Financial
    }

    internal class Transaction : ITransactionRepository
    {
        private readonly IFinancialTransactionRepository _financialRepository;
        private readonly IManagerialTransactionRepository _managerialRepository;

        public Transaction()
        {
            _financialRepository = new FinancialTransactionRepository();
            _managerialRepository = new ManagerialTransactionRepository();
        }

        public void AddTransaction(TransactionInfo transaction)
        {
            
            _financialRepository.AddTransaction(transaction);
            _managerialRepository.AddTransaction(transaction);
        }

        public IEnumerable<TransactionInfo> GetTransactionsByUserId(Guid userId)
        {
            
            var financialTransactions = _financialRepository.GetTransactionsByUserId(userId);
            var managerialTransactions = _managerialRepository.GetTransactionsByUserId(userId);
            return financialTransactions.Concat(managerialTransactions).OrderByDescending(t => t.DateTime);
        }

        public IEnumerable<TransactionInfo> GetFinancialTransactions(Guid userId)
        {
            return _financialRepository.GetTransactionsByUserId(userId);
        }

        public IEnumerable<TransactionInfo> GetManagerialTransactions(Guid userId)
        {
            return _managerialRepository.GetTransactionsByUserId(userId);
        }

        public void DeleteTransaction(Guid transactionId)
        {
            _financialRepository.DeleteTransaction(transactionId);
            _managerialRepository.DeleteTransaction(transactionId);
        }
    }
}

