using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using Kurs2;
namespace Kurs2
{
    public class DataGridViewPrinter
    {
        public DataGridView dataGridView;
        public PrintDocument printDocument;
        public DataGridView dgv;
        public DataGridViewPrinter(DataGridView dgv)
        {
            this.dgv = dgv;
        }

        public void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dgv.Width, this.dgv.Height);
            this.dgv.DrawToBitmap(bm, new Rectangle(0, 0, this.dgv.Width, this.dgv.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }

        public void Print()
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
    }
}
