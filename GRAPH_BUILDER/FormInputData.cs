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
  public partial class FormInputData : Form
  {
    public double[] R { get; set; }
    public double[] CoordinateX { get; set; }
    public double[] CoordinateY { get; set; }

    public FormInputData()
    {
      InitializeComponent();
      CoordinateX = new double[] { 0 };
      CoordinateY = new double[] { 0 };
      R = new double[] { 0 };
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      tbRadiuses.Text = tbRadiuses.Text.Replace(@".", @",");
      tbX.Text = tbX.Text.Replace(@".", @",");
      tbY.Text = tbY.Text.Replace(@".", @",");
      String[] LinesR = tbRadiuses.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
      String[] LinesX = tbX.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
      String[] LinesY = tbY.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
      int cicleCount = LinesR.Length + 1; // большой круг
      R = new double[cicleCount];
      CoordinateX = new double[cicleCount];
      CoordinateY = new double[cicleCount];
      try
      {
        if (!((LinesR.Length == LinesY.Length) && (LinesR.Length == LinesY.Length))) throw new EvaluateException();

        R[0] = double.Parse(tbCoordinateR0.Text);
        CoordinateX[0] = double.Parse(tbCoordinateRX.Text);
        CoordinateY[0] = double.Parse(tbCoordinateRY.Text);
        int count = 0;
        for (int i = 1; i < cicleCount; ++i)
        {
          R[i] = double.Parse(LinesR[count]);
          CoordinateX[i] = double.Parse(LinesX[count]);
          CoordinateY[i] = double.Parse(LinesY[count]);
          count++;
        }
        Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnDefault_Click(object sender, EventArgs e)
    {
      R = new double[] { 8, 1, 2, 3 };
      CoordinateX = new double[] { 0, -0.735172529228688, -3.21523819955971, 3.14270838933609 };
      CoordinateY = new double[] { 0, 7.18308198792879, -3.23638521171619, -2.09246646911878 };

      tbRadiuses.Text = "";
      tbX.Text = "";
      tbY.Text = "";
      tbCoordinateR0.Text = R[0].ToString();
      tbCoordinateRX.Text = CoordinateX[0].ToString();
      tbCoordinateRY.Text = CoordinateY[0].ToString();
      int coordinateCount = R.Length;
      for (int i = 1; i < coordinateCount; ++i)
      {
        tbRadiuses.Text += R[i].ToString();
        tbX.Text += CoordinateX[i].ToString();
        tbY.Text += CoordinateY[i].ToString();
        tbRadiuses.Text += Environment.NewLine;
        tbX.Text += Environment.NewLine;
        tbY.Text += Environment.NewLine;
      }
    }
  }
}
