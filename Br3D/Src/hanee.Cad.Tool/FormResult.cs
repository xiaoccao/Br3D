using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace hanee.Cad.Tool
{
    public partial class FormResult : DevExpress.XtraEditors.XtraForm
    {
        public FormResult()
        {
            InitializeComponent();
        }

        private void FormResult_Load(object sender, EventArgs e)
        {
            CenterToParent();

            // 창 크기를 줄 일수 있다면 더 줄인다.
            // 최소 120
            int height = 120;

            // 글자라인수만큼 키운다.
            // 원래 크기보다 커질 수는 없다.
            height += (richTextBox1.Lines.Length - 1) * richTextBox1.Font.Height;
            if(this.Size.Height > height)
                this.Size = new Size(this.Size.Width, height);
        }

        public RichTextBox RichTextBox
        {
            get { return richTextBox1; }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}