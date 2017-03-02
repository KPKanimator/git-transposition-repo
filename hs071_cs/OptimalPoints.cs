using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cureos.Numerics;

namespace hs071_cs
{
    public class OptimalPoints
    {
        public int _n;
        public int _m;
        public int _nele_jac;
        public int _nele_hess;
        public double[] _x_L;
        public double[] _x_U;
        public double[] _g_L;
        public double[] _g_U;

        public OptimalPoints()
        {
            _n = 5;
            _x_L = new double[] { 3, 3, 2, 2, 0 };
            _x_U = new double[] { 20, 5, 20, 6, Ipopt.PositiveInfinity };
            _m = 3;
            _g_L = new double[] { 0, 0, 0 };
            _g_U = new double[] { Ipopt.PositiveInfinity, Ipopt.PositiveInfinity, Ipopt.PositiveInfinity };
            _nele_jac = 8;
        }

        public bool eval_f(int n, double[] x, bool new_x, out double obj_value)
        {
            obj_value = x[4];

            return true;
        }

        public bool eval_grad_f(int n, double[] x, bool new_x, double[] grad_f)
        {
            grad_f[0] = 0;
            grad_f[1] = 0;
            grad_f[2] = 0;
            grad_f[3] = 0;
            grad_f[4] = 1.0;
            return true;
        }

        public bool eval_g(int n, double[] x, bool new_x, int m, double[] g)
        {
            g[0] = x[4] - x[0] - 3;
            g[1] = x[4] - x[2] - 2;
            g[2] = (x[0] - x[2]) * (x[0] - x[2]) + (x[1] - x[3]) * (x[1] - x[3]) - 25;

            return true;
        }

        public bool eval_jac_g(int n, double[] x, bool new_x, int m, int nele_jac, int[] iRow, int[] jCol, double[] values)
        {
            if (values == null)
            {
                iRow[0] = 0;
                jCol[0] = 0;
                iRow[1] = 0;
                jCol[1] = 4;
                iRow[2] = 1;
                jCol[2] = 2;
                iRow[3] = 1;
                jCol[3] = 4;
                iRow[4] = 2;
                jCol[4] = 0;
                iRow[5] = 2;
                jCol[5] = 2;
                iRow[6] = 2;
                jCol[6] = 1;
                iRow[7] = 2;
                jCol[7] = 3;
            }
            else
            {
                values[0] = -1.0;                   // | x1 | y1 | x2 | y2 | z |
                values[1] = 1.0;                   // 0| -1 |              | 1 |
                values[2] = -1.0;                   // 1| 
                values[3] = 1.0;
                values[4] = 2 * (x[0] - x[2]);
                values[5] = -2 * (x[0] - x[2]);
                values[6] = 2 * (x[1] - x[3]);
                values[7] = -2 * (x[1] - x[3]);
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
    }
}
