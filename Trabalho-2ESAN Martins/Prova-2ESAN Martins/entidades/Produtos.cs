using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_2ESAN_Martins.entidades
{
    public class Produtos
    {
        public int ID { get; set; }

        public string Produto { get; set; }

        public DateTime Validade { get; set; }

        public float Valor { get; set; }

        public override string ToString()
        {
            return "\n" + "ID: " + ID + "\n" + "  Descriçaõ do produto: " + Produto
                    + "\n"
                    + "  Data de validade: " + Validade
                    + "\n"
                    + "  Valor: " + Valor
                    + "\n" + "\n";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Produtos objAsProduto = obj as Produtos;
            if (objAsProduto == null) return false;
            else return Equals(objAsProduto);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public bool Equals(Produtos other)
        {
            if (other == null) return false;
            return (this.ID.Equals(other.ID));
        }
    }
}
