using Prova_2ESAN_Martins.entidades;
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

namespace Prova_2ESAN_Martins
{
    public partial class Sistema : Form
    {
        List<Produtos> produtos = new List<Produtos>();
        private Login Login;

        public Sistema(Login login, List<entidades.Pessoas> pessoas)
        {
            InitializeComponent();
            this.Login = login;
            produtos.Add(new Produtos()
            {
                ID = 1,
                Produto = "Leite",
                Validade = DateTime.Parse("24/09/2020"),
                Valor = (float)2.50
            });            
            produtos.Add(new Produtos()
            {
                ID = 2,
                Produto = "Café",
                Validade = DateTime.Parse("29/09/2020"),
                Valor = (float)5.10
            });            
            produtos.Add(new Produtos()
            {
                ID = 3,
                Produto = "Arroz",
                Validade = DateTime.Parse("30/09/2020"),
                Valor = (float)60.45
            });
        }

        int id;
        string prod;
        DateTime data;
        float valor;

        private void Sistema_Load(object sender, EventArgs e)
        {
            //Identificando o Usuario e o Acesso
            lblUsuario.Text = "Usuario: " + Login.pegaUser();
            lblAcesso.Text = "Acesso: " + Login.pegaValor();

            //Primeira checagem da tela
            if (Login.pegaValor() == "Total")
            {
                btnNovo.Enabled = true;
                btnPesquisar.Enabled = true;
            }
            else if (Login.pegaValor() != "Total" && Login.pegaValor() == "Parcial")
            {
                btnNovo.Enabled = true;
                btnPesquisar.Enabled = false;
            }
            else
            {
                btnNovo.Enabled = false;
                btnPesquisar.Enabled = false;
            }

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if(Login.pegaValor() == "Total" || Login.pegaValor() == "Parcial")
            {
                btnNovo.Enabled = false;
                btnPesquisar.Enabled = false;

                btnIncluir.Enabled = true;
                bntCancelar.Enabled = true;

                txbId.Enabled = false;
                txbProduto.Enabled = true;
                maskValidade.Enabled = true;
                numUDValor.Enabled = true;

                //Gerando ID automaticamente
                int i = 1;
                foreach (Produtos produtos in produtos)
                {
                    if (i != produtos.ID)
                    {
                        txbId.Text = i.ToString();
                        return;
                    }
                    i++;
                }
                txbId.Text = (i++).ToString();
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (txbId.Text == "" || txbProduto.Text == "" || maskValidade.Text == "00/00/0000" || numUDValor.Value == 0)
            {
                MessageBox.Show("Todos os campos são de preechimento obrigatorio", "Aviso", MessageBoxButtons.OK);
                return;
            }
            else
            {
                //Checagem de campo DATA em uma data valida
                try
                {
                    data = Convert.ToDateTime(maskValidade.Text);
                }
                catch(Exception)
                {
                    MessageBox.Show("Campo data Invalido");
                    return;
                }

                //start: Adicionar
                produtos.Add(new Produtos()
                {
                    ID = id = Convert.ToInt32(txbId.Text),
                    Produto = prod = txbProduto.Text,
                    Validade = data,
                    Valor = valor = (float)numUDValor.Value
                });
                MessageBox.Show("Cadastro salvo com sucesso!");
                //end: 

                //Desabilitar incluir
                btnIncluir.Enabled = false;
                
                //Desabilitar campos
                txbId.Enabled = true;
                txbProduto.Enabled = false;
                maskValidade.Enabled = false;
                numUDValor.Enabled = false;

                //Limpar Campos
                txbId.Clear();
                txbProduto.Clear();
                maskValidade.Text = "00/00/0000";
                numUDValor.ResetText();

                //checagem
                if (Login.pegaValor() == "Total")
                {
                    btnNovo.Enabled = true;
                    btnPesquisar.Enabled = true;
                }
                else if (Login.pegaValor() != "Total" && Login.pegaValor() == "Parcial")
                {
                    btnNovo.Enabled = true;
                    btnPesquisar.Enabled = false;
                }
                else
                {
                    btnNovo.Enabled = false;
                    btnPesquisar.Enabled = false;
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txbId.Text == "")
            {
                MessageBox.Show("Insira o conteudo no campo ID");
                return;
            }
            else
            {
                //Procurando Id
                id = Convert.ToInt32(txbId.Text);
                foreach (Produtos produtos in produtos)
                {
                    if(id == produtos.ID)
                    {
                        //Desabilitando botões
                        btnPesquisar.Enabled = false;
                        btnNovo.Enabled = false;

                        //Habilitando botões
                        btnAlterar.Enabled = true;
                        btnExcluir.Enabled = true;
                        bntCancelar.Enabled = true;

                        //Habilitando campos
                        txbId.Enabled = false;
                        txbProduto.Enabled = true;
                        maskValidade.Enabled = true;
                        numUDValor.Enabled = true;

                        //Transmitindo valores
                        txbProduto.Text = produtos.Produto;
                        maskValidade.Text = Convert.ToString(produtos.Validade);
                        numUDValor.Value = (decimal) produtos.Valor;

                        return;
                    }
                }
                //Não achou
                MessageBox.Show("Numero de Id não encontrado");
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //Checagem de campo DATA em uma data valida
            try
            {
                data = Convert.ToDateTime(maskValidade.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Campo data Invalido");
                return;
            }

            foreach (Produtos produtos in produtos)
            {
                //Reescrever valores através do ID
                if (id == produtos.ID)
                {
                    //Alterando um ID dentro da lista
                    if (MessageBox.Show("Deseja alterar esse registro?", "ATENÇÂO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        produtos.ID = id = Convert.ToInt32(txbId.Text);
                        produtos.Produto = prod = txbProduto.Text;
                        produtos.Validade = data;
                        produtos.Valor = valor = (float)numUDValor.Value;
                        MessageBox.Show("Cadastro alterado com sucesso, clique em 'Listar' para ver alteração.", "Registro alterado");

                        //Desabilitando Botões
                        btnAlterar.Enabled = false;
                        btnExcluir.Enabled = false;
                        
                        //Desabilitar campos
                        txbId.Enabled = true;
                        txbProduto.Enabled = false;
                        maskValidade.Enabled = false;
                        numUDValor.Enabled = false;

                        //Limpar Campos
                        txbId.Clear();
                        txbProduto.Clear();
                        maskValidade.Text = "00/00/0000";
                        numUDValor.ResetText();

                        //checagem
                        if (Login.pegaValor() == "Total")
                        {
                            btnNovo.Enabled = true;
                            btnPesquisar.Enabled = true;
                        }
                        else if (Login.pegaValor() != "Total" && Login.pegaValor() == "Parcial")
                        {
                            btnNovo.Enabled = true;
                            btnPesquisar.Enabled = false;
                        }
                        else
                        {
                            btnNovo.Enabled = false;
                            btnPesquisar.Enabled = false;
                        }
                        return;
                    }
                    else
                        return;
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir esse registro", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //Remover registro de pessoas
                produtos.Remove(new Produtos()
                {
                    ID = id = Convert.ToInt32(txbId.Text),
                    Produto = prod = txbProduto.Text,
                    Validade = data,
                    Valor = valor = (float)numUDValor.Value
                });
            }
            else
                return;

            //Desabilitando Botões
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;

            //Desabilitar campos
            txbId.Enabled = true;
            txbProduto.Enabled = false;
            maskValidade.Enabled = false;
            numUDValor.Enabled = false;

            //Limpar Campos
            txbId.Clear();
            txbProduto.Clear();
            maskValidade.Text = "00/00/0000";
            numUDValor.ResetText();

            //checagem
            if (Login.pegaValor() == "Total")
            {
                btnNovo.Enabled = true;
                btnPesquisar.Enabled = true;
            }
            else if (Login.pegaValor() != "Total" && Login.pegaValor() == "Parcial")
            {
                btnNovo.Enabled = true;
                btnPesquisar.Enabled = false;
            }
            else
            {
                btnNovo.Enabled = false;
                btnPesquisar.Enabled = false;
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            //Limpando a vizualização da lista
            richTBlistagem.Clear();

            //Escrevendo a lista
            foreach(Produtos produtos in produtos)
            {
                richTBlistagem.AppendText(produtos.ToString());
            }
        }

        private void bntCancelar_Click(object sender, EventArgs e)
        {
            //Habilitando e desabilitando Campos
            txbId.Enabled = true;
            txbProduto.Enabled = false;
            maskValidade.Enabled = false;
            numUDValor.Enabled = false;

            //Limpando Campos
            txbId.Clear();
            txbProduto.Clear();
            maskValidade.Text = "00/00/0000";
            numUDValor.ResetText();

            //Desabilitando botões
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;

            //checagem
            if (Login.pegaValor() == "Total")
            {
                btnNovo.Enabled = true;
                btnPesquisar.Enabled = true;
            }
            else if (Login.pegaValor() != "Total" && Login.pegaValor() == "Parcial")
            {
                btnNovo.Enabled = true;
                btnPesquisar.Enabled = false;
            }
            else
            {
                btnNovo.Enabled = false;
                btnPesquisar.Enabled = false;
            }
        }
    }
}
