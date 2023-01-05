using System;
using Terminal.Gui;
using AdaCredit.Domain.Entities;
using AdaCredit.Data;
using AdaCredit.UseCases;

// Criacao Cliente com conta
var cliente1 = new Client(46080233819, "pablito");
var account1 = AccountsRepository.GetNewUnique(cliente1);
cliente1.AddAccount(account1);
ClientsRepository.Add(cliente1);
AccountsRepository.Add(account1);
// Criacao mais uma conta pro cliente
var account2 = AccountsRepository.GetNewUnique(cliente1);
cliente1.AddAccount(account2);
ClientsRepository.Update(cliente1);
AccountsRepository.Add(account2);
// Criacao Cliente com conta
var cliente2 = new Client(46080233819, "pablito");
var account3 = AccountsRepository.GetNewUnique(cliente1);
cliente1.AddAccount(account3);
ClientsRepository.Add(cliente2);
AccountsRepository.Add(account3);

// Contas no repositorio
System.Console.WriteLine(AccountsRepository.DisplayString());

// Rodar transacoes
var tr = new TransactionsRepository("caixa-20230101.csv");
ProcessTransactions.ProcessAllTransactions(tr);
tr.WriteTransactionsToFile();

// Contas no repositorio
System.Console.WriteLine(AccountsRepository.DisplayString());



