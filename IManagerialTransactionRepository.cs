﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMapp
{
    internal interface IManagerialTransactionRepository
    {
        void AddTransaction(TransactionInfo transaction);
        IEnumerable<TransactionInfo> GetTransactionsByUserId(Guid userId);
        void DeleteTransaction(Guid transactionId);
    }
}
