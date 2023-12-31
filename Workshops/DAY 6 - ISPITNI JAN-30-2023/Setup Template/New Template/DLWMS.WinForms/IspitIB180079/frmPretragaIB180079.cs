﻿using DLWMS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLWMS.WinForms.IspitIB180079
{
    public partial class frmPretragaIB180079 : Form
    {
        DLWMSDbContext db = new DLWMSDbContext();
        List<Student> studenti;
        public frmPretragaIB180079()
        {
            InitializeComponent();
        }

        private void frmPretragaIB180079_Load(object sender, EventArgs e)
        {

            dgvStudenti.AutoGenerateColumns = false;
            cbSpol.DataSource = db.Spolovi.ToList();
            UcitajSve();
        }

        private void UcitajSve()
        {
            studenti = db.Studenti.Include("Spol").ToList();

            if (studenti != null)
            {



                dgvStudenti.DataSource = null;
                dgvStudenti.DataSource = studenti;
            }
        }
        private void UcitajStudente()
        {

            var datumOd = dtpDatumOd.Value;
            var datumDo = dtpDatumDo.Value;
            var spol = cbSpol.SelectedItem.ToString();

            studenti = db.Studenti.Include("Spol").Where(x => (x.DatumRodjenja >= datumOd && x.DatumRodjenja <= datumDo) && (x.Spol.Naziv == spol))
                .ToList();

            if (studenti != null)
            {



                dgvStudenti.DataSource = null;
                dgvStudenti.DataSource = studenti;
            }
            //if(studenti.Count() == 0)
            //{
            //    MessageBox.Show($"U bazi podataka ne postoji evidencija o studentima {spol} spola rođenih u periodu od {datumOd} – {datumDo}. godine.");
            //}
        }

        private void cbSpol_SelectedIndexChanged(object sender, EventArgs e)
        {
            UcitajStudente();
        }


        private void dtpDatumOd_ValueChanged(object sender, EventArgs e)
        {
            UcitajStudente();

        }

        private void dtpDatumDo_ValueChanged(object sender, EventArgs e)
        {
            UcitajStudente();

        }

        private void dgvStudenti_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var student = studenti[e.RowIndex];

            if (e.ColumnIndex == 5)
            {
                frmUvjerenjaIB180079 frmUvjerenja = new frmUvjerenjaIB180079(student);
                frmUvjerenja.ShowDialog();
            }
            else
            {
                frmStudentInfoIB180079 frmInfo = new frmStudentInfoIB180079(student);
                frmInfo.ShowDialog();
            }

        }
    }
}
