using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Domain.Entities;
using AdaCredit.Data;

namespace AdaCredit.UseCases
{
    public static class ProcessTransactions
    {
        public static string lastErrorDescription;
        public static bool ProcessAllTransactions()
        {
            List<Transaction> transactions = TransactionsRepository.GetAllTransactions();
            foreach(Transaction t in transactions)
            {
                if(!ProcessTransaction(t))
                {
                    TransactionsRepository.AddFailedTransaction(t,lastErrorDescription);
                } else {
                    TransactionsRepository.AddCompletedTransaction(t);
                }
            }
            return true;
        }
        public static bool ProcessTransaction(Transaction transaction)
        {
            // Processamos apenas transacoes com o banco AdaCredit
            if(transaction.originBankCode != 777 && transaction.destinationBankCode != 777)
            {
                lastErrorDescription = "Transação não inclui AdaCredit";
                return false;
            }
            // TEF apenas entre clientes do banco AdaCredit
            if(transaction.typeTransaction == "TEF" &&
                (transaction.destinationBankCode != 777 ||
                transaction.originBankCode != 777))
            {
                lastErrorDescription = "TEF com banco externo";
                return false;
            }
            // Transacoes antes do dia 01-12-2022 sao isentas
            double tarifa = 0;
            if(transaction.date >= new DateTime(2022,12,1))
            {
                tarifa = transaction.typeTransaction switch{
                    "TED" => 5,
                    "DOC" => 1 + Math.Max(transaction.value*0.01,5),
                    "TEF" => 0,
                    _ => 0
                };
            }
            double valueDebit = transaction.value + tarifa;
            if(!VerifyTransactionAccounts(transaction))
            {
                lastErrorDescription = "Código de conta não encontrada";
                return false;
            }
            if(!VerifyTransactionValue(valueDebit, transaction))
            {
                lastErrorDescription = "Saldo insuficiente";
                return false;
            }
            bool success_fromAda = true;
            if(transaction.originBankCode == 777)
            {
                success_fromAda = ProcessTransactionFromAdaCredit(valueDebit,transaction.originAccountCode);
            }
            bool success_toAda = true;
            if(transaction.destinationBankCode == 777)
            {
                success_toAda = ProcessTransactionToAdaCredit(transaction.value,transaction.destinationAccountCode);
            }
            return success_fromAda && success_toAda;
        }
        public static bool ProcessTransactionFromAdaCredit(double value, int accountNumber)
        {
            return AccountsRepository.Debit(accountNumber, value);
        }
        public static bool ProcessTransactionToAdaCredit(double value, int accountNumber)
        {
            return AccountsRepository.Credit(accountNumber, value);
        }
        public static bool VerifyTransactionAccounts(Transaction transaction)
        {
            if(transaction.originBankCode == 777 &&
                AccountsRepository.GetIndexAccountNumber(transaction.originAccountCode) == -1)
            {
                return false;
            }            
            if(transaction.destinationBankCode == 777 &&
                AccountsRepository.GetIndexAccountNumber(transaction.destinationAccountCode) == -1)
            {
                return false;
            }
            return true;
        }
        public static bool VerifyTransactionValue(double valueDebit, Transaction transaction)
        {
            if(transaction.Credit == 0 &&
                transaction.originBankCode == 777)
            {
                return AccountsRepository.GetBalanceByAccountCode(transaction.originAccountCode) >= valueDebit;
            }
            if(transaction.Credit == 1 &&
                transaction.destinationBankCode == 777)
            {
                return AccountsRepository.GetBalanceByAccountCode(transaction.destinationAccountCode) >= valueDebit;
            }
            return true;
        }
    }
}