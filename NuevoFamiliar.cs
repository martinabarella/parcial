using PrimerParcial3k22019.Modelos;
using PrimerParcial3k22019.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimerParcial3k22019.Formularios
{
    public partial class NuevoFamiliar : Form
    {
        ParentescosRepositorio parentescosRepositorio;
        FamiliaresRepositorio familiaresRepositorio;
        string dniTitular;
        public NuevoFamiliar(string dni)
        {
            InitializeComponent();
            dniTitular = dni;
            parentescosRepositorio = new ParentescosRepositorio();
            familiaresRepositorio = new FamiliaresRepositorio();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var familiar = new Familiar();
            familiar.DniTitular = dniTitular;
            familiar.DniFamiliar = txtDni.Text.Trim();
            familiar.Nombre = txtNombre.Text.Trim();
            familiar.ParentescoId = cmbParentescos.SelectedValue.ToString();
            if (!familiar.NombreValido())
            {
                MessageBox.Show("Nombre Invalido!");
                txtNombre.Text = "";
                txtNombre.Focus();
                return;
            }
            if (!familiar.DNIValido())
            {
                MessageBox.Show("DNI Invalido!");
                txtDni.Text = "";
                txtDni.Focus();
                return;
            }
            if (familiar.DniTitular == familiar.DniFamiliar)
            {
                MessageBox.Show("No puede ingresar el mismo DNI que su titular.");
                txtDni.Text = "";
                txtDni.Focus();
                return;

            }
            if (familiar.ParentescoId == null)
            {
                MessageBox.Show("Ingrese un parentesco valido!");
                cmbParentescos.Focus();
            }
            if (familiar.DniRepetido(familiar.DniFamiliar,familiar.DniTitular))
            {
                MessageBox.Show("Familiar ya existente!");
                txtDni.Text = "";
                txtDni.Focus();
                return;
            }

            if (familiaresRepositorio.Guardar(familiar))
            {
                MessageBox.Show("Se agrego Familiar con exito!");
                this.Dispose();
            }
        }

        private void NuevoFamiliar_Load(object sender, EventArgs e)
        {
            lblDni.Text = dniTitular.ToString();
            actualizarCombo();
        }

        private void actualizarCombo()
        {
            var parentescos = parentescosRepositorio.ObtenerParentescos();
            cmbParentescos.DataSource = parentescos;
            cmbParentescos.ValueMember = "Id";
            cmbParentescos.DisplayMember = "Nombre";
        }


    }
}
