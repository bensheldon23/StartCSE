using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace StartCSE
{
    class GeneralFunctions
    {
        public void UpdateVersion()
        {

        }
        public void PasteSiteInfo(DataGridView dgv)
        {
            string[] clipboardRows;
            string[] clipboardValues;
            DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();

            clipboardRows = Clipboard.GetText().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            string[,] clipboardCells = new string[clipboardRows.Length, 2];
            for (int i = 0; i < clipboardRows.Length - 1; i++)
            {
                clipboardValues = clipboardRows[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                if (clipboardValues.Length != 2)
                {
                    MessageBox.Show("Error - There must be 2 columns in each Clipboard Row");
                    return;
                }
                clipboardCells[i, 0] = clipboardValues[0];
                clipboardCells[i, 1] = clipboardValues[1];
            }
            for (int i = 0; i < clipboardRows.Length - 1; i++)
            {

                row.Cells[0].Value = clipboardCells[i, 0];
                row.Cells[1].Value = clipboardCells[i, 1];
                dgv.Rows.Add(row.Cells[0].Value, row.Cells[1].Value);
            }
        }

        public void ClearSiteInfo(DataGridView dgv)
        {
            do
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    try
                    {
                        dgv.Rows.Remove(row);
                    }
                    catch (Exception) { }
                }
            } while (dgv.Rows.Count > 1);
        }

    }
}
