using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PermutationGenerator
{
    class Globals
    {
        public static int n = 4;
        public static int n_factorial = Globals.factorial(n);
        public static List<int[]> permutations = new List<int[]>(); 

        public static void initArray(int[] a)
        {
            for (int i = 0; i < n; i++)
            {
                a[i] = i;
            }
        }

        public static void printArray(int[] a)
        {
            foreach (var e in a)
            {
                Console.Write("{0}\t", e);
            }
        }

        public static int factorial(int x)
        {
            int result = x;

            if (x == 0) return 1; 

            for (int i = x - 1; i >= 1; i--)
            {
                result *= i;
            }

            return result; 
        }
    }

    class array
    {
        int id; 
        int[] array_field = new int[Globals.n]; 
        
        public array(int id, int[] input_array)
        {
            this.id = id; 
            this.array_field = input_array; 
        }

        public array(int id)
        {
            this.id = id;
            Globals.initArray(array_field); 
        }

        public void gen_array()
        {
            swap(0, id); 
        }

        public void swap(int index1, int index2)
        {
            int temp = array_field[index1];
            array_field[index1] = array_field[index2];
            array_field[index2] = temp; 
        }

        public void permutation(int first, int last)
        {
            if(last==0)
            {
                int[] result = new int[Globals.n];
                array_field.CopyTo(result, 0);
                Globals.permutations.Add(result); 
            }
            else
            {
                for(int i=first; i<=last;i++)
                {
                    swap(i, last);
                    permutation(first, last - 1);
                    swap(i, last); 
                }
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            array[] arrays = new PermutationGenerator.array[Globals.n];
            Thread[] threads = new Thread[Globals.n];

            for (int i = 0; i < Globals.n; i++)
            {
                int local_i = i; 

                threads[local_i] = new Thread(() =>
                { 
                    arrays[local_i] = new array(local_i); 
                    arrays[local_i].gen_array();
                    arrays[local_i].permutation(1, Globals.n - 1);
                }); 
            }

            for (int i = 0; i < Globals.n; i++)
            {
                threads[i].Start(); 
            }

            for (int i = 0; i < Globals.n; i++)
            {
                threads[i].Join();
            }

            foreach (var e in Globals.permutations)
            {
                Globals.printArray(e);
                Console.WriteLine(); 
            }
        }
    }
}
