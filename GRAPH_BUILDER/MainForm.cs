using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GRAPH_BUILDER
{
  public partial class MainForm : Form
  {
    private FormInputData frmInpData;
    private readonly int _maxZoom;
    private readonly int _minZoom;
    public MainForm()
    {
      _maxZoom = 200;
      _minZoom = 1;  //10
      InitializeComponent();
      frmInpData = new FormInputData();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      //TrackBar для R1
      trackBar1.Minimum = 1;
      trackBar1.Maximum = 10;
      //TrackBar для r2
      trackBar2.Minimum = trackBar1.Minimum;
      trackBar2.Maximum = trackBar1.Maximum;
      //TrackBar для масштаба
      trackBar3.Minimum = _minZoom;
      trackBar3.Maximum = _maxZoom;
      trackBar3.Value = 22;
      //Обновляем наши лейблы
      label4.Text = string.Format("R1: {0}", trackBar1.Value);
      label5.Text = string.Format("r2: {0}", trackBar2.Value);
      label6.Text = string.Format("Scale: {0}", trackBar3.Value);
      //Коэффициенты
      label8.Text = string.Format("A={0}, B={0}, C={0}", 0);
    }

    private void btn_BUILD_Click(object sender, EventArgs e)
    {
      pnl_GRAPH.Refresh();
      //Передаем все необходимые инпуты в функцию CreateGraph которая посроит на панельки график
      //CreateGraph(trackBar3.Value, trackBar1.Value, trackBar2.Value,3);
      CreateGraph(trackBar3.Value);
    }
    public void CreateGraph(int grids)
    {
      Graphics graph = pnl_GRAPH.CreateGraphics();
      //Это карандашы от тонкого до толстого для сетки и фигур
      Pen bold_pen = new Pen(Brushes.Black, 3);
      Pen middle_pen = new Pen(Brushes.Black, 2);
      Pen think_pen = new Pen(Brushes.Black, 1);

      //Масштаб
      int scale = pnl_GRAPH.Height / grids;

      //начало координат
      Point X0Y0 = new Point(pnl_GRAPH.Width / 2, pnl_GRAPH.Height / 2);

      //Строим ось Х
      graph.DrawLine(middle_pen, new Point(0, pnl_GRAPH.Height / 2), new Point(pnl_GRAPH.Width, pnl_GRAPH.Height / 2));
      //Строим ось Y
      graph.DrawLine(middle_pen, new Point(pnl_GRAPH.Width / 2, 0), new Point(pnl_GRAPH.Width / 2, pnl_GRAPH.Height));

      //Строим координатную сетку вдоль Х
      for (int i = 0; i <= pnl_GRAPH.Height; i++)
      {
        graph.DrawLine(think_pen, new Point(0, i * scale), new Point(pnl_GRAPH.Width, i * scale));
      }
      //Строим координатную сетку вдоль Y
      for (int i = 0; i <= pnl_GRAPH.Width; i++)
      {
        graph.DrawLine(think_pen, new Point(i * scale, 0), new Point(i * scale, pnl_GRAPH.Height));
      }
      //Масштабируем радиусы
      //radius1 = radius1 * scale;
      //radius2 = radius2 * scale;
      //radius3 = radius3 * scale;
      //double[] radius = { 1, 2, 3, 2, 1 };
      //double[] coordX = { 1.00597162791446, 2.92579330247040, 7.82842706282102, 3.82842711653416, 1 };
      //double[] coordY = { 3.68550991659417, 5.99615748776287, 5.00000000000000, 2, 1 };

      //double[] radius = { 6, 1, 2, 3 }; // 1.35 первый круг
      //double[] coordX = { 0, -1.47828526925937, -3.05767389488206, 2.94730763113741 };
      //double[] coordY = { 0, -0.448585802289327, 2.5159698132704, -0.223657567287402 };

      // ** 2222 **
      //double[] radius = { 6, 1.00095507417124, 3, 1.00095507418035 };
      //double[] coordX = { 0, -4.11995518332701, 0.726655091174848, 4.11995518332123 };
      //double[] coordY = { 0, -2.83132832213824, 0.711621702919567, 2.83132832213057 };


      //double[] radius = { 10, 1, 2, 3 };
      //double[] coordX = { 0, 0, -3, 2 };
      //double[] coordY = { 0, 6, -3, 0 };

      double[] radius = frmInpData.R;
      double[] coordX = frmInpData.CoordinateX;
      double[] coordY = frmInpData.CoordinateY;


      int[] iRadius = new int[radius.Length];
      for (int i = 0; i < radius.Length; ++i)
        iRadius[i] = Convert.ToInt32(radius[i] * scale);

      //Рисуем наши окружности
      //graph.DrawEllipse(bold_pen, new Rectangle(new Point(X0Y0.X - radius1, X0Y0.Y - radius1), new Size(2 * radius1, 2 * radius1)));
      //graph.DrawEllipse(bold_pen, new Rectangle(new Point(X0Y0.X + radius1 - radius2, X0Y0.Y - radius2), new Size(2 * radius2, 2 * radius2)));
      for (int i = 0; i < radius.Length; ++i)
        graph.DrawEllipse(bold_pen, new Rectangle(
          new Point(X0Y0.X + Convert.ToInt32(coordX[i] * scale) - iRadius[i], X0Y0.Y + Convert.ToInt32(coordY[i] * scale) - iRadius[i]),
          new Size(2 * iRadius[i], 2 * iRadius[i])));

      /*
      //Угол
      double angel = 0;
      if (radius2 >= 2* radius1)
      {
          MessageBox.Show("Касательная вырождаеться в точку!");
          return;
      }
      else if (radius1 > radius2)
      {
          angel = (((double)radius1 - (double)radius2) / (double)radius1);
      }
      else 
      {
          angel = - (((double)radius2 - (double)radius1) / (double)radius2);            
      }

      //Это будет точка касания первой окружности
      Point N = new Point();            
      N.X = X0Y0.X - Convert.ToInt16(radius1 * -(angel));
      N.Y = X0Y0.Y - Convert.ToInt16(radius1 * Math.Sqrt(1 - Math.Pow(-angel, 2)));
      //Рисуем линию(от цента до точки касания на окружности)
      graph.DrawLine(think_pen, X0Y0, N);

      //Это будет точка касания второй окружности
      Point M = new Point();
      M.X = X0Y0.X + radius1 - Convert.ToInt16(radius2 * -(angel));
      M.Y = X0Y0.Y - Convert.ToInt16(radius2 * Math.Sqrt(1 - Math.Pow(-angel, 2)));
      //Рисуем линию(от цента до точки касания на окружности)
      graph.DrawLine(think_pen, new Point(X0Y0.X + radius1, X0Y0.Y), M);

      //Считаем коеффициенты
      double A = ((double)N.Y - (double)M.Y);
      double B = ((double)M.X - (double)N.X);
      double C = ((double)N.X * (double)M.Y - (double)M.X * (double)N.Y);

      //Отображаем коэффициенты
      label8.Text = string.Format("A={0}, B={1}, C={2}", A, B, C);

      //f(x) при х=0
      int y1 = Convert.ToInt16(-C / B);
      //f(x) при х = width of panel
      int y2 = Convert.ToInt16((-A * pnl_GRAPH.Width - C) / B);

      //рисуем касательную
      graph.DrawLine(bold_pen, new Point(0, y1), new Point(pnl_GRAPH.Width, y2));
      */

    }

    private void trackBar1_Scroll(object sender, EventArgs e)
    {
      toolTip1.SetToolTip(trackBar1, trackBar1.Value.ToString());
      label4.Text = string.Format("R1: {0}", trackBar1.Value);
      label4.Update();

    }

    private void trackBar2_Scroll(object sender, EventArgs e)
    {
      toolTip1.SetToolTip(trackBar2, trackBar2.Value.ToString());
      label5.Text = string.Format("r2: {0}", trackBar2.Value);
      label5.Update();
    }

    private void trackBar3_Scroll(object sender, EventArgs e)
    {
      toolTip1.SetToolTip(trackBar3, trackBar3.Value.ToString());
      label6.Text = string.Format("Scale: {0}", trackBar3.Value);
      label6.Update();
    }

    private void menuInputData_Click(object sender, EventArgs e)
    {
      frmInpData.ShowDialog();
    }

    private void menuZoomIn_Click(object sender, EventArgs e)
    {

      trackBar3.Value = (trackBar3.Value + 5 < _maxZoom) ? trackBar3.Value + 5 : _minZoom;
    }

    private void menuZoomOut_Click(object sender, EventArgs e)
    {
      trackBar3.Value = (trackBar3.Value - 5 < _minZoom) ? _maxZoom : trackBar3.Value - 5;
    }

    private void trackBar3_ValueChanged(object sender, EventArgs e)
    {
      pnl_GRAPH.Refresh();
      CreateGraph(trackBar3.Value);
    }

    private void menuOpenFile_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
      Stream myStream = null;

      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        try
        {
          if ((myStream = openFileDialog.OpenFile()) != null)
          {
            using (myStream)
            {
              // Insert code to read the stream here.
              string[] lines = File.ReadAllLines(openFileDialog.FileName);

              int sizeArr = int.Parse(lines[1]) + 1; // +1 внешний круг
              frmInpData.R = new double[sizeArr];
              frmInpData.CoordinateX = new double[sizeArr];
              frmInpData.CoordinateY = new double[sizeArr];
              // Круг помещаем в центр координат
              frmInpData.R[0] = double.Parse(lines[0]);
              frmInpData.CoordinateX[0] = 0;
              frmInpData.CoordinateY[0] = 0;
              // Считываем остальные круги
              for (int i = 0; i < sizeArr - 1; ++i)
              {
                string[] words = lines[i + 2].Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                //sr.Write("{0} {1} {2}", r[i], x[2 * i], x[2 * i + 1]);
                frmInpData.R[i + 1] = double.Parse(words[0]);
                frmInpData.CoordinateX[i + 1] = double.Parse(words[1]);
                frmInpData.CoordinateY[i + 1] = double.Parse(words[2]);
              }
            }
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
        }
      }// openFileDialog
    }// menuOpenFile_Click

    private void menuInputDataKartashovFormat_Click(object sender, EventArgs e)
    {
      using (var frmInputDataKartashovFormat = new FormInputDataKartashovFormat())
      {
        frmInputDataKartashovFormat.ShowDialog();
        try
        {
          string[] words = frmInputDataKartashovFormat.GetData.Split(new String[] { "\n", "\t", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
          int sizeArr = (words.Length - 1) / 3; // количество кругов
          frmInpData.R = new double[sizeArr + 1]; // + Ro
          frmInpData.CoordinateX = new double[sizeArr + 1]; // + Ro
          frmInpData.CoordinateY = new double[sizeArr + 1]; // + Ro 
                                                            // Круг помещаем в центр координат
          frmInpData.R[0] = double.Parse(words[words.Length - 1]);
          frmInpData.CoordinateX[0] = 0;
          frmInpData.CoordinateY[0] = 0;
          // Считываем остальные круги
          int counter = 0;
          for (int i = 0; i < sizeArr; ++i)
          {
            frmInpData.CoordinateX[i + 1] = double.Parse(words[counter++]);
            frmInpData.CoordinateY[i + 1] = double.Parse(words[counter++]);
          }
          for (int i = 0; i < sizeArr; ++i)
            frmInpData.R[i + 1] = double.Parse(words[counter++]);
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
        }
      }
    }
  }// Class 
}
