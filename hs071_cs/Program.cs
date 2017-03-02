using System;
using Cureos.Numerics;
using System.IO;
using System.Diagnostics;

namespace hs071_cs
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Stopwatch stopWatch = new Stopwatch();
      stopWatch.Start();
      #region Задачи разные
      /* create the IpoptProblem */
      /* allocate space for the initial point and set the values */
      /* Пример - рабочий */
      //HS071 p = new HS071();

      /* allocate space for the initial point and set the values */
      /* Задача Карташова в полосе - рабочая */
      //double[] r = { 1, 2, 3, 2, 1 };
      //var p = new CirclePolosa20011(8, 5, r);
      //double[] x = p.FirstValue(r);

      //var p = new OptimalPoints();
      //double[] x;

      /* create the IpoptProblem */
      //HS071 p = new HS071();
      #endregion

      /* Задача Карташова в круге - рабочая */
      int n = 5;
      //double[] rrr = p.findB(new double[] {2,3,5}, new double[] {-1,0,-2});
      //double[] r = { 2, 3, 2, 4, 5, 1, 2, 3, 2, 4, 2, 3, 2, 4, 5, 1, 2, 3, 2, 4 };  // , 40};//, 30, 20, 10, 10, 
      //double[] r = { 1, 1, 2, 2 };//, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5 };
      //double[] r = { 2, 3, 4, 5, 22, 4, 5, 1, 2, 43, 2, 4, 21, 3, 7, 4, 15, 1, 2, 31, 2, 49 };  // , 40};//, 30, 20, 10, 10, 
      double[] r = { 1, 2, 3, 5, 8 };
      //double[] r = new double[n];
      for (int i = 0; i < n; ++i)
        r[i] = i + 1;
      //double[] r = { 5, 5, 5, 5, 5, 5, 10 };
      //, 6, 7, 8, 9, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11, 11
      //var p = new OptimalPoints4(r, new int[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2,3,3,3,3,3,4,4,4,4,4,5,5,5,5,5,6,6,6,6,6,7,7,7,7,7,8,8,8,8,8 });
      // все круги в своих группах, т.е. фикс радиус
      //***int[] grNum = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
      //int countI = 0;
      //for (int raw = 0; raw < 40; ++raw)
      //    grNum[countI++] = raw + 1;
      //for (int raw = 0; raw < 20; ++raw)
      //  for (int column = 0; column < 2; ++column)
      //    grNum[countI++] = raw + 1;
      //grNum[n-1] = 14;

      //var p = new OptimalPoints4(r, grNum);
      var p = new OptimalPoints3(n);
      p.SetInputValues(n,r);
      /* allocate space for the initial point and set the values */
      double[] x = p.FirstValue(r);
      IpoptReturnCode status;
      using (Ipopt problem = new Ipopt(p._n, p._x_L, p._x_U, p._m, p._g_L, p._g_U, p._nele_jac, p._nele_hess, p.eval_f, p.eval_g, p.eval_grad_f, p.eval_jac_g, p.eval_h))
      {
        /* Set some options.  The following ones are only examples,
           they might not be suitable for your problem. */
        // https://www.coin-or.org/Ipopt/documentation/node41.html#opt:print_options_documentation
        problem.AddOption("tol", 1e-3);
        problem.AddOption("mu_strategy", "adaptive");
        problem.AddOption("hessian_approximation", "limited-memory");
        problem.AddOption("output_file", p.ToString() + "_" + DateTime.Now.ToShortDateString() + "_"
          + DateTime.Now.Hour + "_" + DateTime.Now.Minute + ".txt");
        problem.AddOption("print_frequency_iter", 20);
        //problem.AddOption("file_print_level", 7); // 0..12
        problem.AddOption("file_print_level", 0);
        problem.AddOption("max_iter", 100000);
        problem.AddOption("print_level", 5); // 0<= value <= 12, default value is 5
#if INTERMEDIATE
                problem.SetIntermediateCallback(p.intermediate);
#endif
        /* solve the problem */
        double obj;
        //status = problem.SolveProblem(x, out obj, null, null, null, null);
        status = problem.SolveProblem(x, out obj, null, null, null, null);
        //SaveToFile("d:\\obj.txt", obj, n);
      }

      Console.WriteLine("{0}{0}Optimization return status: {1}{0}{0}", Environment.NewLine, status);
      stopWatch.Stop();

      SaveToFile("d:\\out.txt", r, x, n);
      Console.WriteLine("{0}Press <RETURN> to exit...", Environment.NewLine);
      Console.WriteLine("RunTime: " + getElapsedTime(stopWatch));

      Console.ReadLine();
    }

    /// <summary>
    /// Форматирует резултат конвертирования времени запуска программы 
    /// </summary>
    /// <param name="Watch">объект Stopwatch</param>
    /// <returns>Строка- время чч:мм:сс.мс</returns>
    static string getElapsedTime(Stopwatch Watch)
    {
      // Get the elapsed time as a TimeSpan value.
      TimeSpan ts = Watch.Elapsed;
      // Format and display the TimeSpan value.
      return String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
    }

    static double[] GetX()
    {
      var x = new double[10];
      // R0
      x[0] = 10;
      // x
      x[1] = 0;
      x[2] = -3.0;
      x[3] = 2.0;
      // y
      x[4] = 6;
      x[5] = -3;
      x[6] = 0;
      // r
      x[7] = 1;
      x[8] = 2;
      x[9] = 3;
      //******************************************
      //for (var i = 0; i < 10; ++i)
      //  x[i] = 0.0;

      return x;
    }

    /// <summary>
    /// Записывает результаты расчётов
    /// </summary>
    /// <param name="fileName">Путь к файлу</param>
    /// <param name="r">Радиусы всех кругов</param>
    /// <param name="x">Вектор оптимизируемый</param>
    /// <param name="sizeC">Количество кругов</param>
    static void SaveToFile(string fileName, double[] r, double[] x, int sizeC)
    {
      StreamWriter sw = new StreamWriter(fileName, false);
      sw.WriteLine("{0} ", x[3 * sizeC]);
      sw.WriteLine("{0} ", sizeC);//запись размерностей масива
      for (int i = 0; i < sizeC; i++)
      {
        sw.Write("{0} {1} {2}", r[i], x[2 * i], x[2 * i + 1]);
        sw.WriteLine();
      }
      sw.Close();

      for (int i = 0; i < x.Length; ++i)
        Console.WriteLine("x[{0}]={1}", i, x[i]);
    }
    static void SaveToFile(string fileName, double[] obj, int sizeC)
    {
      using (var sw = new StreamWriter(fileName, false))
      {
        sw.WriteLine("{0} ", obj[2 * sizeC]); // Ro
        sw.WriteLine("{0} ", sizeC);          //запись размерностей масива
        for (int i = 0; i < sizeC; i++)
          sw.WriteLine("{0} {1} {2}", obj[i], obj[2 * i], obj[2 * i + 1]);
      }
      //for (int i = 0; i < x.Length; ++i)
      //Console.WriteLine("x[{0}]={1}", i, x[i]);
    }
  }
}
