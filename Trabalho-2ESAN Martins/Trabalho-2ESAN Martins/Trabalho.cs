using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabalho_2ESAN_Martins.entidades;
using Trabalho_2ESAN_Martins.utilizaveis;

namespace Trabalho_2ESAN_Martins
{
    public partial class Trabalho : Form
    {
        List<Pessoas> pessoas = new List<Pessoas>();

        public Trabalho()
        {
            InitializeComponent();
            pessoas.Add(new Pessoas(Id)
            {
                ID = 1,
                Username = "Administrador",
                Email = "adiministrador@empresa.com.br",
                Password = "souF#*@"
            }); ;
            pessoas.Add(new Pessoas(Id)
            {
                ID = 2,
                Username = "Funcionario",
                Email = "funcionario@empresa.com.br",
                Password = "patrãoBoBã0"
            });            
            pessoas.Add(new Pessoas(Id)
            {
                ID = 3,
                Username = "Usuario",
                Email = "usuario@publico.com.br",
                Password = "preguiça"
            });
        }

        int Id;
        string user, pass, email;


        //Novo cadastro
        private void btnNovo_Click(object sender, EventArgs e)
        {
            //Habilitando campos para digitação
            txbUser.Enabled = true;
            txbEmail.Enabled = true;
            txbPass.Enabled = true;

            //Desabilitando campo 'id'  
            txbId.Enabled = false;

            //Habilitando Botões de Cancelar e Incluir 
            bntCancelar.Enabled = true;
            btnIncluir.Enabled = true;

            //Desabilitando botões Novo e Pesquisar
            btnNovo.Enabled = false;
            btnPesquisar.Enabled = false;

            int i = 1;
            foreach (Pessoas pessoas in pessoas)
            {
                if (pessoas.ID == i)
                    i++;
                else
                {
                    txbId.Text = (i).ToString();
                    return;
                }
            }
            txbId.Text = (i++).ToString();
        }

        //Pesquisar cadastro existente
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //checando se o campo foi preenchido
            if (txbId.Text == "")
            {
                MessageBox.Show("Digite o numero do ID para pesquisar");
                return;
            }
            Id = Convert.ToInt32(txbId.Text);
            if(pessoas.Contains(new Pessoas(Id)))
            {
                btnPesquisar.Enabled = false;
                foreach (Pessoas pessoas in pessoas)
                {
                    if (Id == pessoas.ID)
                    {
                        //Habilitando campos para digitação
                        txbUser.Enabled = true;
                        txbEmail.Enabled = true;
                        txbPass.Enabled = true;

                        txbId.Enabled = false;

                        //Pegando valores dos registros
                        txbUser.Text = pessoas.Username;
                        txbEmail.Text = pessoas.Email;
                        txbPass.Text = pessoas.Password;

                        //Habilitando Botões Alterar, Excluir e Cancelar
                        btnAlterar.Enabled = true;
                        btnExcluir.Enabled = true;
                        bntCancelar.Enabled = true;

                        //Desabilitando Botão Novo
                        btnNovo.Enabled = false;
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Id não existe", "Erro");
            }
        }

        //Incluir e ou salvar um cadastro
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            //checando se os campos foram preenchidos
            if (txbUser.Text == "" || txbEmail.Text == "" || txbPass.Text == "")
            {
                MessageBox.Show("Todos os campos são de preechimento obrigatorio", "Aviso", MessageBoxButtons.OK);
            }
            else
            {
                //start: Adicionando um novo registro
                pessoas.Add(new Pessoas(Id)
                {
                    ID = Id = Convert.ToInt32(txbId.Text),
                    Username = user = txbUser.Text,
                    Email = email = txbEmail.Text,
                    Password = pass = txbPass.Text
                });
                MessageBox.Show("Cadastro salvo com sucesso!");
                //end:

                //Novo registro
                if (MessageBox.Show("Deseja incluir um novo registro?", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Id++;
                    txbId.Text = Id.ToString();
                    btnNovo.Enabled = false;
                    bntCancelar.Enabled = true;
                }
                //Sair
                else
                {
                    //Limpar valor do id
                    txbId.Clear();

                    //Habilitar Botões Novo e Pesquisar
                    btnNovo.Enabled = true;
                    btnPesquisar.Enabled = true;

                    //Desabilitar Botões Incluir e Cancelar
                    btnIncluir.Enabled = false;
                    bntCancelar.Enabled = false;

                    //Habilitar Campo ID
                    txbId.Enabled = true;

                    //Desabilitar
                    txbUser.Enabled = false;
                    txbEmail.Enabled = false;
                    txbPass.Enabled = false;
                }

                //Limpando campos
                txbEmail.Clear();
                txbUser.Clear();
                txbPass.Clear();


            }
        }

        //Alterar um cadastro ja existente 
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //Pegar ID
            Id = Convert.ToInt32(txbId.Text);

            //Procurar ID
            foreach (Pessoas pessoas in pessoas)
            {
                //Reescrever valores através do ID
                if (Id == pessoas.ID)
                {
                    if (MessageBox.Show("Deseja alterar esse registro?", "ATENÇÂO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        pessoas.Username = user = txbUser.Text; 
                        pessoas.Email = email = txbEmail.Text;
                        pessoas.Password = pass = txbPass.Text; ;
                        MessageBox.Show("Cadastro alterado com sucesso, clique em 'Listar' para ver alteração.", "Registro alterado");
                        return;
                    }
                    else
                        return;
                }
            }
        }

        //Excluir um cadastro ja existente
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir esse registro", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //Remover registro de pessoas
                pessoas.Remove(new Pessoas(Id)
                {
                    ID = Id = Convert.ToInt32(txbId.Text),
                    Username = user = txbUser.Text,
                    Email = email = txbEmail.Text,
                    Password = pass = txbPass.Text
                });
            }
            else
                return;

            //Limpando Todos os valores
            txbId.Clear();
            txbEmail.Clear();
            txbUser.Clear();
            txbPass.Clear();

            //Desabilitando campos username,email e password
            txbUser.Enabled = false;
            txbEmail.Enabled = false;
            txbPass.Enabled = false;

            //Habilitando campo Id
            txbId.Enabled = true;

            //Desabilitando Botões Alterar, Excluir e Cancelar
            bntCancelar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;

            //Habilitando Botões Novo e Pesquisar
            btnNovo.Enabled = true;
            btnPesquisar.Enabled = true;
        }

        //listar todos os cadastros existentes
        private void btnListar_Click(object sender, EventArgs e)
        {
            richTBlistagem.Clear();
            //Odenando lista
            pessoas = pessoas.OrderBy(x => x.ID).ToList();
            //Escrevendo Valores
            pessoas.ForEach(pessoas =>
            {
                richTBlistagem.AppendText(pessoas.ToString());
            });
        }

        //Deixar que apenas que numeros sejam digitados no campo id
        private void Validar_id(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar))
            {
                    e.Handled = true;
            }
        }

        //Mostrar senha
        private void Mostrar_senha(object sender, EventArgs e)
        {
            if(ckbMostrarPassword.Checked == true)
            {
                txbPass.PasswordChar = '\0';
            }
            else
            {
                txbPass.PasswordChar = '*';
            }
        }

        //Verificar se o campo email foi digitado corretamente
        private void Verificar_email(object sender, EventArgs e)
        {
            if (txbEmail.Text == "")
                return;
            if(ValidarEmail.Validar(txbEmail.Text) == false)
            {
                MessageBox.Show("Formato do Email incorreto", "Aviso");
            }
        }

        //Botão Cancelar
        private void bntCancelar_Click(object sender, EventArgs e)
        {
            //ativando botões
            btnNovo.Enabled = true;
            btnPesquisar.Enabled = true;

            //desativando botões
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            bntCancelar.Enabled = false;

            //reiniciando campos
            txbId.Clear();
            txbId.Enabled = true;

            txbUser.Clear();
            txbUser.Enabled = false;

            txbEmail.Clear();
            txbEmail.Enabled = false;

            txbPass.Clear();
            txbPass.Enabled = false;

        }

        //Botão para sair
        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo encerrar a aplicação?", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Application.Exit();
        }
    }
}
