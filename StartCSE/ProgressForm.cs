using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartCSE
{
    public partial class ProgressForm : Form
    {
        public string Message
        {
            set { currentStatusLbl.Text = value; }
        }
        public int ProgressValue
        {
            set { progressBar1.Value = value; }
        }
        public int ProgressMax
        {
            set { progressBar1.Maximum = value; }
        }
        
        public ProgressForm()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
        }

        public event EventHandler<EventArgs> Canceled;

        private void CancelButton_Click(object sender, EventArgs e)
        {
            EventHandler<EventArgs> ea = Canceled;
            if(ea != null)
            {
                ea(this, e);
            }
        }
    }
}
