using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova_2ESAN_Martins.entidades
{
    public class Pessoas
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Acesso { get; set; }

        public Pessoas(string user, string pass)
        {
            user = Username;
            pass = Password;
        }

        public override string ToString()
        {
            return  "Usuario: " + Username + "\n" + "Senha: " + Password + "\n" + "Acesso: " + Acesso + "\n" + "\n";
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Pessoas objAsPessoas = obj as Pessoas;
            if (objAsPessoas == null) return false;
            else return Equals(objAsPessoas);
        }

        public bool Equals(Pessoas other)
        {
            if (other == null) return false;
            return (this.ID.Equals(other.ID));
        }
    }
}
