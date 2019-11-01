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
    public partial class EditarFamiliar : Form
    {
        string idFamiliar;
        Familiar familiar;
        FamiliaresRepositorio familiaresRepositorio;
        ParentescosRepositorio parentescosRepositorio;


        public EditarFamiliar(string id)
        {
            InitializeComponent();
            familiaresRepositorio = new FamiliaresRepositorio();
            parentescosRepositorio = new ParentescosRepositorio();
            familiar = familiaresRepositorio.ObtenerFamiliar(id);
        }

        private void EditarFamiliar_Load(object sender, EventArgs e)
        {
            CargarCombo();
            txtDni.Text = familiar.DniFamiliar;
            txtNombre.Text = familiar.Nombre;
            cmbParentescos.SelectedValue = familiar.ParentescoId;
            idFamiliar = familiar.Id;
            lblDni.Text = familiar.DniFamiliar;
            
        }
       
        private void CargarCombo()
        {
           
            var parentescos = parentescosRepositorio.ObtenerParentescos();
            cmbParentescos.DataSource = parentescos;
            cmbParentescos.ValueMember = "Id";
            cmbParentescos.DisplayMember = "Nombre";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var datosFami = new Familiar();
            datosFami.Nombre = txtNombre.Text;
            datosFami.DniFamiliar = txtDni.Text;
            datosFami.Id = idFamiliar;
            if (cmbParentescos.SelectedValue is null)
            {
                MessageBox.Show("El parentesco seleccionado no es valido.");
                cmbParentescos.Focus();
                return;
            }
            else
            {
                datosFami.ParentescoId = cmbParentescos.SelectedValue.ToString();
            }

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
            if (datosFami.DniFamiliar != familiar.DniFamiliar)
            {
                if (familiar.DniRepetido(familiar.DniFamiliar, familiar.DniTitular))
                {
                    MessageBox.Show("Familiar ya existente!");
                    txtDni.Text = "";
                    txtDni.Focus();
                    return;
                }
            }

            if (familiaresRepositorio.Editar(datosFami))
            {
                MessageBox.Show("La edicion ha finalizado correctamente");
                this.Dispose();
            }
        }
    }
}
