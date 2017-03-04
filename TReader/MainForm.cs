using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TReader
{
    public partial class MainForm : Form, IMainForm
    {
        private Font Myfont;
        private string save_file;
        public MainForm()
        {
            InitializeComponent();
            tbWay.Enabled = false;
        }

      
        #region IMain интерфейс
        public string FilePath
        {
            get { return tbWay.Text; }
        }

        public string Content
        {
            get
            {
                return rtbContent.Text;
            }
            set
            {
                rtbContent.Text = value;
            }
        }

        public void SetSymbolCount(int count)
        {
            lbCountSymbol.Text = count.ToString();
        }

        public Font New_font 
        { 
            get {return Myfont;}
            set { Myfont=value;} 
        }

        public void Font_ch()
        {
            rtbContent.Font = Myfont;
            rtbContent.Refresh();
        }
        public string FileSave 
        { 
            get { return save_file;}
            set { save_file=value;} 
        }
        public event EventHandler FileOpenClick;

        public event EventHandler FileSaveClick;
        public event EventHandler FileSaveAsClick;
        public event EventHandler TransposClick;
        public event EventHandler ContentChanged;
        #endregion
        #region Проброс событий
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dl = new OpenFileDialog();
            dl.Filter = @"Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (dl.ShowDialog() == DialogResult.OK)
            {
                tbWay.Text = dl.FileName;

                if (FileOpenClick != null)
                    FileOpenClick(this, EventArgs.Empty);
            }
        }

        private void btnSavedFile_Click(object sender, EventArgs e)
        {
            if (FileSaveClick != null)
                FileSaveClick(this, EventArgs.Empty);
        }

        private void rtbContent_TextChanged(object sender, EventArgs e)
        {
             if (Myfont != null)
                    rtbContent.Font = Myfont;
            if (ContentChanged != null)             
                ContentChanged(this, EventArgs.Empty);
            
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            //открыть новое окошко
            FontDialog f = new FontDialog();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Myfont = f.Font;
                ContentChanged(this, EventArgs.Empty);
            }
        }

        private void btSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog s=new SaveFileDialog();
            s.Filter = @"Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileSave = s.FileName;
                if (FileSaveAsClick != null)
                    FileSaveAsClick(this, EventArgs.Empty);
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            Font_text form = new Font_text();
            form.ShowDialog();
        }

        private void btnTranspos_Click(object sender, EventArgs e)
        {
            if (TransposClick != null)
                TransposClick(this, EventArgs.Empty);
        }
        //private void WriteInform(string[] str)
        //{
        //    rtbContent.Clear();
        //    foreach (string s in str)
        //    {
        //        rtbContent.Text = s;
        //    }
        //}
        

      
    }
        #endregion
    
  
}
