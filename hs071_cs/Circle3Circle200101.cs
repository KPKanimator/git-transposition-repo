using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cureos.Numerics;
using System.Runtime.InteropServices;

namespace hs071_cs
{
  class Circle3Circle200101
  {
    public int _n; // размерность Х
    public int _m; // количетсво ограничений
    public int _nele_jac;
    public int _nele_hess;
    public double[] _x_L;
    public double[] _x_U;
    public double[] _g_L;
    public double[] _g_U;

    public Circle3Circle200101()
    {
      /* set the number of variables and allocate space for the bounds */
      /* set the values for the variable bounds */
      _n = 10;
      #region Инициализация граничных условий для X
      _x_L = new double[_n];
      _x_U = new double[_n];
      // TODO: Область допустимых Х
      // 0 < r < sum{vr[i]}
      _x_L[0] = 0;
      _x_U[0] = 10;

      for (int i = 1; i < 7; ++i)
      {
        _x_L[i] = -10;
        _x_U[i] = 10;
      }
      //for (int i = 7; i < _n; ++i)
      //{
      //  _x_L[i] = 1;
      //  _x_U[i] = 3;
      //}
      _x_L[7] = 1;
      _x_U[7] = 1;
      _x_L[8] = 2;
      _x_U[8] = 2;
      _x_L[9] = 3;
      _x_U[9] = 3;
      #endregion

      /* установить количество ограничений и выделить пространство для границ */
      /* set the number of constraints and allocate space for the bounds */
      _m = 6; // 6+8

      /* set the values of the constraint bounds */
      // g[i]<=0
      _g_L = new double[_m];
      for (int i = 0; i < _m; ++i)
      {
        _g_L[i] = Ipopt.NegativeInfinity;
      }
      //_g_L[12] = 0; // только g[13] = 0

      _g_U = new double[_m];
      for (var i = 0; i < _m; ++i)
        _g_U[i] = 0;

      /* Number of nonzeros in the Jacobian of the constraints */
      _nele_jac = 30;// 45;

      /* Number of nonzeros in the Hessian of the Lagrangian (lower or
         upper triangual part only) */
      _nele_hess = 0;
    }

    [AllowReversePInvokeCalls]
    public bool eval_f(int n, double[] x, bool new_x, out double obj_value)
    {
      obj_value = x[0];

      return true;
    }

    [AllowReversePInvokeCalls]
    public bool eval_grad_f(int n, double[] x, bool new_x, double[] grad_f)
    {
      grad_f[0] = 1;
      for (int i = 1; i < _n; ++i)
        grad_f[i] = 0;

      return true;
    }

    [AllowReversePInvokeCalls]
    public bool eval_g(int n, double[] x, bool new_x, int m, double[] g)
    {
      const int C = 3; // количество кругов
      var counter = 0; //сквозная нумерация ограничений

      var vr = new double[] { 1.0, 2.0, 3.0 }; // радиусы кругов
      for (var i = 0; i < C; ++i)
      {
        //  x[i]^2+y[i]^2-(r0-r[i])^2 <=0 
        g[counter++] = Math.Pow(x[1 + i], 2) + Math.Pow(x[1 + C + i], 2) - Math.Pow((x[0] - x[1 + 2 * C + i]), 2);
      }
      // (r[i]+r[j])^2-(x[i]-x[j])^2-(y[i]-y[j])^2<=0; i=1..C, j=i+1..C
      for (var i = 0; i < C - 1; ++i)
        for (var j = i + 1; j < C; ++j)
        {
          g[counter] = Math.Pow((x[1 + 2 * C + i] + x[1 + 2 * C + j]), 2); //r
          g[counter] -= Math.Pow((x[1 + i] - x[1 + j]), 2); //x
          g[counter++] -= Math.Pow((x[1 + C + i] - x[1 + C + j]), 2); //y
        }
        /*
      //  vr[1]-r[i] <=0 
      for (var i = 0; i < C; ++i)
        g[counter++] = vr[1] - x[1 + 2 * C + i];

      //  vr[1]+vr[2]-r[i]-r[j] <= 0
      for (var i = 0; i < C - 1; ++i)
        for (var j = i + 1; j < C; ++j)
          g[counter++] = vr[1] + vr[2] - (x[1 + 2 * C + i] + x[1 + 2 * C + j]);

      //  r[1]+r[2]+r[3] - (vr[1]+vr[2]+vr[3]) = 0
      double sumR = 0, sumVR = 0;
      for (var i = 0; i < C; ++i)
      {
        sumR += x[1 + 2 * C + i];
        sumVR += vr[i];
      }
      g[counter++] = sumR - sumVR;

      // eps = min {vr[i]} / 10 = 1/10
      // sum { r[i]^2 } - sum { vr[i]^2 } - eps <= 0
      const double eps = 0.1;
      sumR = 0;
      sumVR = 0;
      for (var i = 0; i < C; ++i)
      {
        sumR += Math.Pow(x[1 + 2 * C + i], 2);
        sumVR += vr[i] * vr[i];
      }
      g[counter++] = -sumR + sumVR - eps;
      */
      return true;
    }

    [AllowReversePInvokeCalls]
    public bool eval_jac_g(int n, double[] x, bool new_x, int m, int nele_jac, int[] iRow, int[] jCol, double[] values)
    {
      if (values == null)
      {
        /* set the structure of the jacobian */
        /* this particular jacobian is dense */
        #region номера строк и столбцов с не нулевыми значениями
        iRow[0] = 0;
        jCol[0] = 0;
        iRow[1] = 0;
        jCol[1] = 1;
        iRow[2] = 0;
        jCol[2] = 4;
        iRow[3] = 0;
        jCol[3] = 7;

        iRow[4] = 1;
        jCol[4] = 0;
        iRow[5] = 1;
        jCol[5] = 2;
        iRow[6] = 1;
        jCol[6] = 5;
        iRow[7] = 1;
        jCol[7] = 8;

        iRow[8] = 2;
        jCol[8] = 0;
        iRow[9] = 2;
        jCol[9] = 3;
        iRow[10] = 2;
        jCol[10] = 6;
        iRow[11] = 2;
        jCol[11] = 9;

        iRow[12] = 3;
        jCol[12] = 1;
        iRow[13] = 3;
        jCol[13] = 2;

        iRow[14] = 3;
        jCol[14] = 4;

        iRow[15] = 3;
        jCol[15] = 5;

        iRow[16] = 3;
        jCol[16] = 7;

        iRow[17] = 3;
        jCol[17] = 8;

        iRow[18] = 4;
        jCol[18] = 2;

        iRow[19] = 4;
        jCol[19] = 3;

        iRow[20] = 4;
        jCol[20] = 5;

        iRow[21] = 4;
        jCol[21] = 6;

        iRow[22] = 4;
        jCol[22] = 8;

        iRow[23] = 4;
        jCol[23] = 9;

        iRow[24] = 5;
        jCol[24] = 1;

        iRow[25] = 5;
        jCol[25] = 3;

        iRow[26] = 5;
        jCol[26] = 4;

        iRow[27] = 5;
        jCol[27] = 6;

        iRow[28] = 5;
        jCol[28] = 7;

        iRow[29] = 5;
        jCol[29] = 9;
        /*
        iRow[30] = 6;
        jCol[30] = 7;
        
        iRow[31] = 7;
        jCol[31] = 8;

        iRow[32] = 8;
        jCol[32] = 9;

        iRow[33] = 9;
        jCol[33] = 7;

        iRow[34] = 9;
        jCol[34] = 8;

        iRow[35] = 10;
        jCol[35] = 7;

        iRow[36] = 10;
        jCol[36] = 9;

        iRow[37] = 11;
        jCol[37] = 8;

        iRow[38] = 11;
        jCol[38] = 9;

        iRow[39] = 12;
        jCol[39] = 7;

        iRow[40] = 12;
        jCol[40] = 8;

        iRow[41] = 12;
        jCol[41] = 9;

        iRow[42] = 13;
        jCol[42] = 7;

        iRow[43] = 13;
        jCol[43] = 8;

        iRow[44] = 13;
        jCol[44] = 9;
        */
        #endregion
      }
      else
      {
        /* return the values of the jacobian of the constraints */

        #region для образца
        //values[0] = x[1] * x[2] * x[3]; /* 0,0 */
        //values[1] = x[0] * x[2] * x[3]; /* 0,1 */
        //values[2] = x[0] * x[1] * x[3]; /* 0,2 */
        //values[3] = x[0] * x[1] * x[2]; /* 0,3 */

        //values[4] = 2 * x[0];         /* 1,0 */
        //values[5] = 2 * x[1];         /* 1,1 */
        //values[6] = 2 * x[2];         /* 1,2 */
        //values[7] = 2 * x[3];         /* 1,3 */

        #endregion

        #region Значения Якобиана
        values[0] = -2 * x[0] + 2 * x[6 + 1];         /* 0,0 */
        values[1] = 2 * x[1];                      /* 0,1 */
        values[2] = 2 * x[3 + 1];                    /* 0,4 */
        values[3] = -x[6 + 1] * x[6 + 1] * 2 + 2 * x[0];         /* 0,7 */

        values[4] = -2 * x[0] + 2 * x[6 + 2];         /* 1,0 */
        values[5] = 2 * x[2];         /* 1,2 */
        values[6] = 2 * x[3 + 2];         /* 1,5 */
        values[7] = -x[6 + 2] * x[6 + 2] * 2 + 2 * x[0];         /* 1,8 */

        values[8] = -2 * x[0] + 2 * x[6 + 3];         /* 2,0 */
        values[9] = 2 * x[3];         /* 2,3 */
        values[10] = 2 * x[3 + 3];         /* 2,6 */
        values[11] = -x[6 + 3] * x[6 + 3] * 2 + 2 * x[0];         /* 2,9 */

        values[12] = -2 * x[1] + 2 * x[2];         /* 3,1 */
        values[13] = -2 * x[2] + 2 * x[1];         /* 3,2 */
        values[14] = -2 * x[3 + 1] + 2 * x[3 + 2];     /* 3,4 */
        values[15] = -2 * x[3 + 2] + 2 * x[3 + 1];     /* 3,5 */
        values[16] = 2 * x[6 + 1] + 2 * x[6 + 2];     /* 3,7 */
        values[17] = 2 * x[6 + 2] + 2 * x[6 + 1];     /* 3,8 */

        values[18] = -2 * x[2] + 2 * x[3];         /* 4,2 */
        values[19] = -2 * x[3] + 2 * x[2];         /* 4,3 */
        values[20] = -2 * x[3 + 2] + 2 * x[3 + 3];     /* 4,5 */
        values[21] = -2 * x[3 + 3] + 2 * x[3 + 2];     /* 4,6 */
        values[22] = 2 * x[6 + 2] + 2 * x[6 + 3];      /* 4,8 */
        values[23] = 2 * x[6 + 3] + 2 * x[6 + 2];      /* 4,9 */

        values[24] = -2 * x[1] + 2 * x[3];         /* 5,1 */
        values[25] = -2 * x[3] + 2 * x[1];         /* 5,3 */
        values[26] = -2 * x[3 + 1] + 2 * x[3 + 3];     /* 5,4 */
        values[27] = -2 * x[3 + 3] + 2 * x[3 + 1];     /* 5,6 */
        values[28] = 2 * x[6 + 1] + 2 * x[6 + 3];      /* 5,7 */
        values[29] = 2 * x[6 + 3] + 2 * x[6 + 1];      /* 5,9 */
        
        //values[30] = -1;         /* 6,7 */
        //values[31] = -1;         /* 7,8 */
        //values[32] = -1;         /* 8,9 */
        //values[33] = -1;         /* 9,7 */
        //values[34] = -1;         /* 9,8 */
        //values[35] = -1;         /* 10,7 */
        //values[36] = -1;         /* 10,9 */
        //values[37] = -1;         /* 11,8 */
        //values[38] = -1;         /* 11,9 */
        //values[39] = 1;          /* 12,7 */
        //values[40] = 1;          /* 12,8 */
        //values[41] = 1;          /* 12,9 */
        //values[42] = -2;          /* 13,7 */
        //values[43] = -2;          /* 13,8 */
        //values[44] = -2;          /* 13,9 */
        #endregion
      }

      return true;
    }

    [AllowReversePInvokeCalls]
    public bool eval_h(int n, double[] x, bool new_x, double obj_factor,
                int m, double[] lambda, bool new_lambda,
                int nele_hess, int[] iRow, int[] jCol,
                double[] values)
    {
      return false;
    }

#if INTERMEDIATE
        public bool intermediate(IpoptAlgorithmMode alg_mod, int iter_count, double obj_value, double inf_pr, double inf_du,
            double mu, double d_norm, double regularization_size, double alpha_du, double alpha_pr, int ls_trials)
        {
            Console.WriteLine("Intermediate callback method at iteration {0} in {1} with d_norm {2}",
                iter_count, alg_mod, d_norm);
            return iter_count < 5;
        }
#endif
    public override string ToString()
    {
      return "Circle3Circle200101";
    }

  }
}



