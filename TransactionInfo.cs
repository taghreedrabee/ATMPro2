using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMapp
{
    internal class TransactionInfo
    {
        public Guid TransactionId { get; private set; }
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string TransactionName { get; private set; }
        public double Amount { get; private set; }
        public DateTime DateTime { get; private set; }
        public double BalanceBefore { get; private set; }
        public double BalanceAfter { get; private set; }
        public Guid? RecipientId { get; private set; }
        public bool IsComplete { get; private set; }

        public TransactionInfo(
            Guid transactionId,
            Guid userId,
            string username,
            string transactionName,
            double amount,
            DateTime dateTime,
            double balanceBefore,
            double balanceAfter,
            Guid? recipientId,
            bool isComplete)
        {
            TransactionId = transactionId;
            UserId = userId;
            Username = username;
            TransactionName = transactionName;
            Amount = amount;
            DateTime = dateTime;
            BalanceBefore = balanceBefore;
            BalanceAfter = balanceAfter;
            RecipientId = recipientId;
            IsComplete = isComplete;
        }
    }
}
