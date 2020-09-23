using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_2ESAN_Martins.entidades
{
    public class Pessoas: IEquatable<Pessoas>
    {

        public int ID { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Pessoas(int Id)
        {
            ID = Id;
        }

        public override string ToString()
        {
            return "\n" + "ID: " + ID + "\n" + "  Username: " + Username
                    + "\n"
                    + "  Email: " + Email
                    + "\n"
                    + "  Password: " + Password
                    + "\n" + "\n" ;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Pessoas objAsPessoas = obj as Pessoas;
            if (objAsPessoas == null) return false;
            else return Equals(objAsPessoas);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public bool Equals(Pessoas other)
        {
            if (other == null) return false;
            return (this.ID.Equals(other.ID));
        }
    }
}
