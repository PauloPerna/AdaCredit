using System;
using Terminal.Gui;
using AdaCredit.Domain.Entities;
using AdaCredit.Data;
using AdaCredit.UseCases;

namespace AdaCredit.UI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			System.Console.WriteLine("Bem vindo à AdaCredit!");
			if(UsersRepository.firstLogin == true)
			{
				CreateFiles.Execute();
			}
			LoginMenu.View();
			Menu.View();
		}
	}
}