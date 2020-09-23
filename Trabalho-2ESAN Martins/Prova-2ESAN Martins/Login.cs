using Prova_2ESAN_Martins.entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prova_2ESAN_Martins
{
    public partial class Login : Form
    {
        public List<Pessoas> pessoas = new List<Pessoas>();

        public Login()
        {
            InitializeComponent();
            pessoas.Add(new Pessoas(user,pass)
            {
                ID = 1,
                Username = "Adm",
                Password = "Adm",
                Acesso = "Total"
            });
            pessoas.Add(new Pessoas(user,pass)
            {
                ID = 2,
                Username = "Func",
                Password = "Func",
                Acesso = "Parcial"
            });
            pessoas.Add(new Pessoas(user,pass)
            {
                ID = 3,
                Username = "User",
                Password = "User",
                Acesso = "Nenhum"
            });
        }

        //start: Iniciando novo Form
        Thread NovoForm;
        void iniciar()
        {
            this.Close();
            NovoForm = new Thread(Aplicativo);
            NovoForm.SetApartmentState(ApartmentState.STA);
            NovoForm.Start();
        }

        private void Aplicativo()
        {
            Application.Run(new Sistema(this, pessoas));
        }
        //end:

        string user, pass, acess;
        private void btnLogar_Click(object sender, EventArgs e)
        {
            if (txbUser.Text == "" || txbPass.Text == "")
            {
                MessageBox.Show("Preencha todos os campos para fazer o login");
                return;
            }
            else
            {
                //Checando se Usuario e senha estão corretos
                user = txbUser.Text;
                pass = txbPass.Text;
                foreach (Pessoas pessoas in pessoas)
                {
                    if (pessoas.Username == user && pessoas.Password == pass)
                    {
                        acess = pessoas.Acesso;
                        iniciar();
                        return;
                    }
                }
                MessageBox.Show("Usuario ou senha incorreta");
            }
        }

        //start: Pegando Usuario e Tipo de Acesso
        public string pegaUser()
        {
            return user;
        }
        public string pegaValor()
        {
            return acess;
        }
        //end:

        private void btnRegistros_Click(object sender, EventArgs e)
        {
            //Limpando lista
            richTBlistagem.Clear();

            //Escrevendo usuarios da lista
            foreach (Pessoas pessoas in pessoas)
            {
                richTBlistagem.AppendText(pessoas.ToString());
            }
        }

        private void Mostrar_senha(object sender, EventArgs e)
        {
            //Mostrar senha
            if (ckbMostrarPassword.Checked == true)
            {
                txbPass.PasswordChar = '\0';
            }
            //Ocultar
            else
            {
                txbPass.PasswordChar = '*';
            }
        }
    }
}
