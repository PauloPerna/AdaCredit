using System;
using Terminal.Gui;
using AdaCredit.Domain.Entities;
using AdaCredit.Data;

// var user = new User(46080233818,"pablo","senha2");
// UsersRepository.Add(user);
// System.Console.WriteLine(UsersRepository.WriteUsersToFile());
// System.Console.WriteLine(user);

var cliente = new Client(46080233819, "pablito");
var account = AccountsRepository.GetNewUnique(cliente);
cliente.AddAccount(account);
ClientsRepository.Add(cliente);
AccountsRepository.Add(account);
ClientsRepository.WriteClientsToFile();
AccountsRepository.WriteAccountsToFile();
System.Console.WriteLine(ClientsRepository.WriteClientsToFile());



