using PrimerParcial3k22019.Formularios;
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

namespace PrimerParcial3k22019
{
    public partial class CargaDeFamiliares : Form
    {
        FamiliaresRepositorio _familiaresRepositorio;
        TitularesRepositorio titularesRepositorio;
        
        
        

        public CargaDeFamiliares()
        {
            _familiaresRepositorio = new FamiliaresRepositorio();
            titularesRepositorio = new TitularesRepositorio();
            InitializeComponent();            
        }

        private void CargaDeFamiliares_Load(object sender, EventArgs e)
        {
            ActualizarComboDni();
        }
        private void ActualizarComboDni()
        {
            var titulares = titularesRepositorio.ObtenerTitular();
            cmbDni.ValueMember = "DniTitular";
            cmbDni.DisplayMember = "DniTitular";
            cmbDni.DataSource = titulares;


            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            foreach (DataRow row in titulares.Rows)
            {
                collection.Add(Convert.ToString(row["DniTitular"]));
            }

            cmbDni.AutoCompleteCustomSource = collection;
            cmbDni.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbDni.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void ActualizarFamiliares()
        {
            var familiares = _familiaresRepositorio.ObtenerFamiliaresTitular(cmbDni.SelectedValue.ToString());
            dataGridView1.DataSource = familiares;
            this.dataGridView1.Columns["Id"].Visible = false;
            this.dataGridView1.Columns["DniTitular"].Visible = false;
            this.dataGridView1.Columns["Dni"].Visible = true;
            this.dataGridView1.Columns["NombreApellido"].Visible = true;
            this.dataGridView1.Columns["ParentescoId"].Visible = false;
            this.dataGridView1.Columns["TipoParentesco"].Visible = true;
            //parentesco, dni, nombre
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            NuevoFamiliar ventana = new NuevoFamiliar(cmbDni.SelectedValue.ToString());
            ventana.ShowDialog();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (cmbDni.SelectedValue is null)
            {
                MessageBox.Show("Numero de dni no existe!");
                cmbDni.Focus();
                return;
            }
            var index = cmbDni.SelectedValue.ToString();
            Titular titular = titularesRepositorio.ObtenerTitular(index);
            txtNombre.Text = titular.nombre.ToString();
            ActualizarFamiliares();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (cmbDni.SelectedValue is null)
            {
                MessageBox.Show("Debe seleccionar un dni valido!");
                cmbDni.Focus();
                return;
            }
            ActualizarFamiliares();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            var seleccionados = dataGridView1.SelectedRows;
            if (seleccionados.Count == 0 || seleccionados.Count > 1)
            {
                MessageBox.Show("Deberia seleccionar una fila");
                return;
            }
            foreach (DataGridViewRow fila in seleccionados)
            {
                
                var id = fila.Cells[0].Value;
                var nombre = fila.Cells[3].Value;
                var parentesco = fila.Cells[5].Value;

                var confirmacion = MessageBox.Show($"¿Esta seguro/a de eliminar a {parentesco}, {nombre} ?",
                    "Confirmar operacion",
                    MessageBoxButtons.YesNo);

                if (confirmacion.Equals(DialogResult.No))
                    return;

                if (_familiaresRepositorio.Eliminar(id.ToString()))
                {
                    MessageBox.Show($"Usted Elimino a {parentesco}, {nombre}.");
                    ActualizarFamiliares();
                }

            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var seleccionados = dataGridView1.SelectedRows;
            if (seleccionados.Count == 0 || seleccionados.Count > 1)
            {
                MessageBox.Show("Deberia seleccionar una fila");
                return;

            }
            foreach (DataGridViewRow fila in seleccionados)
            {
                var id = fila.Cells[0].Value;
                var ventana = new EditarFamiliar(id.ToString());
                ventana.ShowDialog();
                
            }
        }
    }
}
