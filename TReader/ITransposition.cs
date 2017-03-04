using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TReader
{
   public interface ITransposition
    {
       void Generate(int k, int n, int[] arr);
       void Call(int count, int[] arr,out string transport);
    }


    public class Transposition: ITransposition
    {
        public static List<int[]> ans = new List<int[]>();
        public void Generate(int k, int n, int[] arr)
        {
            if (k == n)
            {
                ans.Add((int[])arr.Clone());
            }
            else
            {
                for (int j = k; j < arr.Length; j++)
                {
                    Swap(arr, k, j);
                    Generate(k + 1, n, arr);
                    Swap(arr, k, j);
                }
            }
        }
        public static void Swap(int[] arr, int i, int j)//поменять местами i-ый и j-ый элемент
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        public void Call(int count,int[] arr,out string transport)
        {
            Generate(0, count, arr);
            transport = "";
            //transport = new string[ans.Count];
            for (int i = 0; i < ans.Count; i++)
            {
               // StringBuilder temp = new StringBuilder();
                for (int j = 0; j < count; j++)
                {
                    //     temp.Append(ans[i][j]);
                    transport = transport + " " + ans[i][j];
                }
                //transport[i] = temp.ToString();
                transport = transport + '\n';
            }

        }

    }






} 