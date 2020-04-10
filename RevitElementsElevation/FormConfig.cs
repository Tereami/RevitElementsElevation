using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RevitElementsElevation
{
    public partial class FormConfig : Form
    {
        private Config config;
        public FormConfig(ref Config cfg)
        {
            InitializeComponent();

            config = cfg;
            foreach(string s in cfg.namePrefixes)
            {
                txtBoxNamePrefix.Text += s + ";";
            }
            txtBoxNamePrefix.Text = txtBoxNamePrefix.Text.Substring(0, txtBoxNamePrefix.Text.Length - 1);
            txtBoxLevelElevation.Text = cfg.paramBaseLevel;
            txtBoxElevationFromLevel.Text = cfg.paramElevOnLevel;

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            config.namePrefixes = txtBoxNamePrefix.Text.Split(';')
                .Where(i => !string.IsNullOrEmpty(i)).ToList();
            config.paramBaseLevel = txtBoxLevelElevation.Text;
            config.paramElevOnLevel = txtBoxElevationFromLevel.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
