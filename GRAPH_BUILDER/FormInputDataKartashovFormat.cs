using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GRAPH_BUILDER
{
  public partial class FormInputDataKartashovFormat : Form
  {
    public string GetData { get; private set; }
    public FormInputDataKartashovFormat()
    {
      InitializeComponent();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      rtbData.Clear();
      Close();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      rtbData.Text = rtbData.Text.Replace(@".", @",");
      GetData = rtbData.Text;
      Close();
    }
  }
}
