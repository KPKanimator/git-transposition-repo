using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cureos.Numerics;
/* Круги в круге
 * countCircle ~ можно задавать количество степеней в ограничении x[i]^k = r[i]^k 
 ******************************** */
namespace hs071_cs
{
  public class OptimalPoints3
  {
    public int _n;
    public int _m;
    public int _nele_jac;
    public int _nele_hess;
    public double[] _x_L;
    public double[] _x_U;
    public double[] _g_L;
    public double[] _g_U;

    public int count;
    //public double height;
    public double[] radius;
    private int _limitCount; // количество степеней в ограничении x[i]^k = r[i]^k


    public OptimalPoints3(int countCircle = 1)
    {
      _limitCount = countCircle;
    }

    public double DiametrSum(double[] radius)
    {
      var sum = 0.0;
      foreach (var rad in radius)
      {
        sum += 2 * rad;
      }
      return sum;
    }

    public void SetInputValues(int count, params double[] radius)
    {
      this.count = count; // количество кругов
                          //this.height = height;
      this.radius = radius;
      _n = radius.Length * 3 + 1;
      _x_L = new double[_n];
      double max = radius[radius.Length - 1];

      for (var i = 0; i < radius.Length; ++i)
      {
        _x_L[2 * i] = _x_L[2 * i + 1] = -DiametrSum(radius) + radius[i];
        _x_L[2 * count + i] = 0;
        //if (max < radius[i]) max = radius[i];
      }
      _x_L[_n - 1] = max;

      _x_U = new double[_n];

      for (var i = 0; i < count; ++i)
      {
        _x_U[2 * i] = _x_U[2 * i + 1] = DiametrSum(radius) - radius[i];
        _x_U[2 * count + i] = radius[radius.Length - 1]; // max Radius
      }
      _x_U[_n - 1] = DiametrSum(radius) / 2; //Ipopt.PositiveInfinity;

      _m = count + (count - 1) * count / 2 + _limitCount; // количество ограничений TODO: оптимизировать {count}

      _g_L = new double[_m];
      _g_U = new double[_m];

      for (var i = 0; i < _m - _limitCount; i++)
      {
        _g_L[i] = 0;
        _g_U[i] = Ipopt.PositiveInfinity;
      }
      // Дополнительные ограничения sum{x[i]} - sum{vx[i]} = 0
      for (var i = _m - _limitCount; i < _m; i++)
      {
        _g_L[i] = 0;
        _g_U[i] = 0;
      }
      //_g_U[_m - 1] = 0;

      _nele_jac = 4 * count + 3 * count * (count - 1) + _limitCount * count; // TODO: Добавили 1
    }
    public double[] FirstValue(double[] r)
    {
      int count = r.Length;
      double[] x = new double[count * 3 + 1];
      double l = 0;
      double d = 0.2;
      for (int i = 0; i < count; ++i)
      {
        x[2 * i] = l + r[i];
        l += 2 * r[i];
        x[2 * i + 1] = d *= -1;
        x[2 * count + i] = r[0];
      }
      x[_n - 1] = l;
      return x;
    }

    public bool eval_f(int n, double[] x, bool new_x, out double obj_value)
    {
      obj_value = x[_n - 1];

      return true;
    }

    public bool eval_grad_f(int n, double[] x, bool new_x, double[] grad_f)
    {

      for (var i = 0; i < _n - 1; i++)
      {
        grad_f[i] = 0;
      }
      grad_f[_n - 1] = 1;
      return true;
    }

    public bool eval_g(int n, double[] x, bool new_x, int m, double[] g)
    {
      for (var i = 0; i < count; ++i)
        g[i] = Math.Pow((x[_n - 1] - x[count * 2 + i]), 2) - x[2 * i] * x[2 * i] - x[2 * i + 1] * x[2 * i + 1];
      //g[i] = x[_n - 1] - radius[i] - Math.Sqrt(x[2 * i] * x[2 * i] + x[2 * i + 1] * x[2 * i + 1]);
      int kk = count;
      for (var i = 0; i < count - 1; ++i)
      {
        for (var j = i + 1; j < count; ++j)
          g[kk++] = Math.Pow((x[2 * i] - x[2 * j]), 2) + Math.Pow((x[2 * i + 1] - x[2 * j + 1]), 2) - Math.Pow((x[count * 2 + i] + x[count * 2 + j]), 2);
      }
      double sumX = 0, sumVX = 0;

      for (int k = 0; k < _limitCount; ++k)
      {
        sumX = 0;
        sumVX = 0;
        for (int i = 0; i < count; ++i)
        {
          sumX += Math.Pow(x[2 * count + i], k + 1);
          sumVX += Math.Pow(radius[i], k + 1);
        }
        g[kk++] = sumX - sumVX;
      }
      return true;
    }

    public bool eval_jac_g(int n, double[] x, bool new_x, int m, int nele_jac, int[] iRow, int[] jCol, double[] values)
    {
      if (values == null)
      {
        int kk = 0;
        var g = 0;
        // позиции по Х и У
        for (g = 0; g < count; ++g)
        {
          iRow[kk] = g;
          jCol[kk++] = _n - 1;
          iRow[kk] = g;
          jCol[kk++] = 2 * g;
          iRow[kk] = g;
          jCol[kk++] = 2 * g + 1;
          iRow[kk] = g;
          jCol[kk++] = count * 2 + g;
        }
        for (var i = 0; i < count - 1; ++i)
        {
          for (var j = i + 1; j < count; ++j)
          {
            iRow[kk] = g;
            jCol[kk++] = 2 * i;
            iRow[kk] = g;
            jCol[kk++] = 2 * j;
            iRow[kk] = g;
            jCol[kk++] = 2 * i + 1;
            iRow[kk] = g;
            jCol[kk++] = 2 * j + 1;
            iRow[kk] = g;
            jCol[kk++] = 2 * count + i;
            iRow[kk] = g++;
            jCol[kk++] = 2 * count + j;
          }
        }
        for (var k = 0; k < _limitCount; ++k)
        {
          for (var i = 0; i < count; ++i)
          {
            iRow[kk] = g;
            jCol[kk++] = 2 * count + i;
          }
          g++;
        }
      }
      else
      {
        int kk = 0;
        for (int i = 0; i < count; ++i)
        {
          values[kk++] = 2 * (x[_n - 1] - x[2 * count + i]); // R0'
          values[kk++] = -2 * x[2 * i]; //X'
          values[kk++] = -2 * x[2 * i + 1]; //Y' 
          values[kk++] = -2 * (x[_n - 1] - x[2 * count + i]); //r'
          //values[kk++] = 1.0;
          //values[kk++] = -x[2 * i] / Math.Sqrt(x[2 * i] * x[2 * i] + x[2 * i + 1] * x[2 * i + 1]);
          //values[kk++] = -x[2 * i + 1] / Math.Sqrt(x[2 * i] * x[2 * i] + x[2 * i + 1] * x[2 * i + 1]);
        }
        for (var i = 0; i < count - 1; ++i)
        {
          for (var j = i + 1; j < count; ++j)
          {
            values[kk++] = 2 * (x[2 * i] - x[2 * j]); //X[i]'
            values[kk++] = -2 * (x[2 * i] - x[2 * j]); //X[j]'
            values[kk++] = 2 * (x[2 * i + 1] - x[2 * j + 1]); //Y[i]'
            values[kk++] = -2 * (x[2 * i + 1] - x[2 * j + 1]); //Y[j]'
            values[kk++] = -2 * (x[2 * count + i] + x[2 * count + j]); //R[i]'
            values[kk++] = -2 * (x[2 * count + i] + x[2 * count + j]); //R[j]'
          }
        }
        for (var k = 0; k < _limitCount; ++k)
        {
          for (var i = 0; i < count; ++i)
          {
            values[kk++] = (k + 1) * Math.Pow(x[2 * count + i], k);
          }
        }
      }

      return true;
    }

    public bool eval_h(int n, double[] x, bool new_x, double obj_factor,
               int m, double[] lambda, bool new_lambda,
               int nele_hess, int[] iRow, int[] jCol,
               double[] values)
    {
      return false;
    }

    /// <summary>
    /// Решение линейных уравнений 
    /// </summary>
    /// <param name="a">отсортированный массив по возрастанию</param>
    /// <param name="c">коэффициенты</param>
    /// <returns></returns>
    public double[] findB(double[] a, double[] c)
    {
      if (a.Length != c.Length) throw new Exception();
      int arrSize = a.Length;
      double[] rez = new double[arrSize];
      int[] index = new int[arrSize]; // помещаем флаг рассмотрения. 0 - не рассматривали
      int indMinC = 0;
      for (int i = arrSize - 1; i >= 0; --i)
      {
        for (int k = 0; k < arrSize; ++k) if (index[k] == 0) { indMinC = k; break; }  // ищем первый не рассмотренный элемент
        for (int k = 0; k < arrSize && index[k] == 0; ++k)
        {
          if (c[indMinC] > c[k])
            indMinC = k;
        }
        rez[indMinC] = a[i];
        index[indMinC] = 1;
      }

      return rez;
    }
    public override string ToString()
    {
      return "OptimalPoints3";
    }

    /*        Градинт -> min (max) ????
     *******************************************************************/
    // Целевая функция - линейная !!!
    public double funF(double[] x, double[] c)
    {
      if (x.Length != c.Length) throw new Exception();
      double rez = 0;
      for (int i = 0; i < x.Length; ++i)
        rez += (x[i] * c[i]);
      return rez;
    }
    // Градиент
    public double[] gradF(double[] x, double[] c)
    {
      if (x.Length != c.Length) throw new Exception();
      int arrSize = x.Length;
      double[] rez = new double[arrSize];
      for (int i = 0; i < x.Length; ++i)
        rez[i] = c[i];
      return rez;
    }
    // Выполнение n итераций подстановки в грандиентную функцию
    public int calcFun(double[] a, double[] c, double[] x)
    {
      int iter = 0;
      double[] newX = findB(a, c);
      // TODO: что чему???
      return iter;
    }


  }
}
