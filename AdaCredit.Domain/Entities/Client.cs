using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.Domain.Entities
{
    public class Client
    {
        private int cpf { get; init; }
        // Although the cpf is just init, the name can change over the years (social name cases for example)
        private string name { get; set; }
        private List<Account> accounts { get; set; }
        private bool active { get; set; }
    }
}