using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cureos.Numerics;

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


        public OptimalPoints3()
        {
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

        public void SetInputValues( int count, params double[] radius)
        {
            this.count = count; // количество кругов
            //this.height = height;
            this.radius = radius;
            _n = radius.Length * 2 + 1;
            _x_L = new double[_n];
            double max = radius[0];

            for (var i = 0; i < radius.Length; ++i)
            {
                _x_L[2 * i] = _x_L[2 * i + 1] = -DiametrSum(radius) + radius[i];
                if (max < radius[i]) max = radius[i];
            }
            _x_L[_n - 1] = max;

            _x_U = new double[_n];

            for (var i = 0; i < radius.Length; ++i )
            {
                _x_U[2 * i] = _x_U[2 * i + 1] = DiametrSum(radius) - radius[i];
            }
            _x_U[_n - 1] = DiametrSum(radius);//Ipopt.PositiveInfinity;

            _m = count +  (count - 1) * count / 2; // количество ограничений

            _g_L = new double[_m];
            _g_U = new double[_m];

            for (var i = 0; i < _m; i++)
            {
                _g_L[i] = 0;
                _g_U[i] = Ipopt.PositiveInfinity;
            }

            _nele_jac = 3*count + 2*count*(count-1);
        }
        public double[] FirstValue(double[] r)
        {
            int count = r.Length;
            double[] x = new double[count * 2 + 1];
            double l = 0;
            double d = 0.2;
            for (int i = 0; i < count; ++i)
            {
                x[2*i] = l+r[i];
                l+=2*r[i];
                x[2 * i + 1] = d*=-1;
            }
            x[count * 2] = l;
            return x;
        }
  
        public bool eval_f(int n, double[] x, bool new_x, out double obj_value)
        {
            obj_value = x[_n-1];

            return true;
        }

        public bool eval_grad_f(int n, double[] x, bool new_x, double[] grad_f)
        {
          
            for (var i = 0; i < _n-1; i++)
            {
              grad_f[i] = 0;
            }
            grad_f[_n-1] = 1;
            return true;
        }

        public bool eval_g(int n, double[] x, bool new_x, int m, double[] g)
        {
            for (var i = 0; i < count ; ++i)
                g[i] = (x[_n - 1] - radius[i]) * (x[_n - 1] - radius[i]) - x[2 * i] * x[2 * i] - x[2 * i + 1] * x[2 * i + 1];
                //g[i] = x[_n - 1] - radius[i] - Math.Sqrt(x[2 * i] * x[2 * i] + x[2 * i + 1] * x[2 * i + 1]);
            int kk = count;
            for (var i = 0; i < count-1 ; ++i)
              for (var j = i+1; j < count ; ++j)
                 g[kk++] = (x[2*i] - x[2*j]) * (x[2*i] - x[2*j]) + (x[2*i+1] - x[2*j+1]) * (x[2*i+1] - x[2*j+1]) - (radius[i]+radius[j]) * (radius[i]+radius[j]);

            return true;
        }

        public bool eval_jac_g(int n, double[] x, bool new_x, int m, int nele_jac, int[] iRow, int[] jCol, double[] values)
        {
            if (values == null)
            {
                int kk = 0;
                var g = 0;
                for ( g = 0; g < count; ++g)
                {
                    iRow[kk] = g;
                    jCol[kk++] = _n-1;
                    iRow[kk] = g;
                    jCol[kk++] = 2*g;
                    iRow[kk] = g;
                    jCol[kk++] = 2 * g + 1;
                }
                for (var i = 0; i < count - 1; ++i)
                    for (var j = i + 1; j < count; ++j)
                    {
                        iRow[kk] = g;
                        jCol[kk++] = 2*i;
                        iRow[kk] = g;
                        jCol[kk++] = 2*j;
                        iRow[kk] = g;
                        jCol[kk++] = 2*i+1;
                        iRow[kk] = g++;
                        jCol[kk++] = 2*j+1;
                    }
        
            }
            else
            {
                int kk = 0;
                for (int i = 0; i < count;++i )
                {
                    values[kk++] = 2 * (x[_n - 1] - radius[i]);
                    values[kk++] = -2 * x[2 * i];
                    values[kk++] = -2 * x[2 * i + 1];
                    //values[kk++] = 1.0;
                    //values[kk++] = -x[2 * i] / Math.Sqrt(x[2 * i] * x[2 * i] + x[2 * i + 1] * x[2 * i + 1]);
                    //values[kk++] = -x[2 * i + 1] / Math.Sqrt(x[2 * i] * x[2 * i] + x[2 * i + 1] * x[2 * i + 1]);
                }

                for (var i = 0; i < count - 1; ++i)
                    for (var j = i + 1; j < count; ++j)
                    {
                        values[kk++] = 2 * (x[2*i] - x[2*j]);
                        values[kk++] = -2 * (x[2*i] - x[2*j]);
                        values[kk++] = 2 * (x[2*i+1] - x[2*j+1]);
                        values[kk++] = -2 * (x[2*i+1] - x[2*j+1]);
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
    public override string ToString()
    {
      return "OptimalPoints3";
    }
  }
}
