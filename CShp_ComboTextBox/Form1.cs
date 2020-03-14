using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CShp_ComboTextBox
{
    
    public partial class Form1 : Form
    {
        int codigoAluno;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregaDados();
        }
        private void CarregaDados()
        {
            cboAlunos.DataSource = DalHelper.GetAlunos();
            cboAlunos.ValueMember = "AlunoId";
            cboAlunos.DisplayMember = "Nome";
            cboAlunos.Text = "Selecione o aluno";
            LimpaFormulario();
        }
        private void LimpaFormulario()
        {
            foreach (var c in this.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = String.Empty;
                }
            }
        }

        private void CboAlunos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Aluno aluno = new Aluno();

            codigoAluno = Convert.ToInt32(((DataRowView)cboAlunos.SelectedItem)["AlunoId"]);

            aluno = DalHelper.GetAluno(codigoAluno);
            PreencheDados(aluno);
        }
        private void PreencheDados(Aluno aluno)
        {
            txtID.Text = aluno.AlunoId.ToString();
            txtNome.Text = aluno.Nome;
            txtEndereco.Text = aluno.Endereco;
            txtEmail.Text = aluno.Email;
            txtTelefone.Text = aluno.Telefone;
        }

        private void BtnIncluir_Click(object sender, EventArgs e)
        {
            if (btnIncluir.Text.Equals("Inserir"))
            {
                btnIncluir.Text = "Guardar";
                LimpaFormulario();
                txtID.Enabled = false;
                txtNome.Focus();
            }
            else if (btnIncluir.Text.Equals("Guardar"))
            {
                btnIncluir.Text = "Inserir";
                txtID.Enabled = true;
                try
                {
                    Aluno aluno = new Aluno();

                    aluno.Nome = txtNome.Text;
                    aluno.Endereco = txtEndereco.Text;
                    aluno.Email = txtEmail.Text;
                    aluno.Telefone = txtTelefone.Text;

                    DalHelper.Add(aluno);
                    CarregaDados();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
            }
             
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                Aluno aluno = new Aluno();

                aluno.AlunoId = Convert.ToInt32(txtID.Text);
                aluno.Nome = txtNome.Text;
                aluno.Endereco = txtEndereco.Text;
                aluno.Email = txtEmail.Text;
                aluno.Telefone = txtTelefone.Text;

                DalHelper.Update(aluno);
                CarregaDados();
            }
             catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                DalHelper.Delete(codigoAluno);
                CarregaDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }
    }   

}
