using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_2ESAN_Martins.utilizaveis
{
    public class ValidarEmail
    {
        public static bool Validar(string Email)
        {
            string Modelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (System.Text.RegularExpressions.Regex.IsMatch(Email,Modelo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
