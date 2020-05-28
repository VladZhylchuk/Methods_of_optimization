using System;

namespace lab3
{
    class Program
    {
        static double[,,] C;
        static double[,] A, B;
        static int OperationCouner = 0;

        static void Main(string[] args)
        {
            int n = Get_matrix_size();

            C = new double[n, n, n + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j, 0] = 0;
                }
            }

            A = new double[n, n];
            setMatrixA(ref A, n);
            PrintMatrix(A, "\nMatrix A");
            B = new double[n, n];
            setMatrixB(ref B, n);
            PrintMatrix(B, "\nMatrix B");

            one_time_assign();
            local_recursive(0, 0, 0);

            string iteration_count_message = "\nlocal recursive Number of operations: " + OperationCouner.ToString();
            print3DMatrix(C, iteration_count_message);
        }

        static void setMatrixA(ref double[,] matrix, int size)
        {
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j == i )
                    {
                        matrix[i, j] = (i + 1) * (i + 2);
                    }
                   
                }
            }
        }

        static void setMatrixB(ref double[,] matrix, int size)
        {
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j + i == size - 1 || j + i < size)
                    {
                        matrix[i, j] = rand.Next(1, 10);
                    }
                   
                }
            }
        }

        // одна змінна
        static void one_time_assign()
        {
            int counter = 0;
            int size = A.GetLength(0);
            double[,,] C = new double[size, size, size + 1];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    C[i, j, 0] = 0;
                    for (int k = 0; k < size; k++)
                    {
                        C[i, j, k + 1] = C[i, j, k] + A[i, k] * B[k, j];
                        counter += 2;
                    }
                }
            }
            string iteration_count_message = "\nSingle assigment. Number of operations: " + counter.ToString();
            print3DMatrix(C, iteration_count_message);
        }

        // локально рексривний
        static void local_recursive(int i, int j, int k)
        {
            int size = A.GetLength(0);

            if (i < size && j < size && k < size)
            {
                if (A[i, k] != 0 && B[k, j] != 0)
                {
                    C[i, j, size] += A[i, k] * B[k, j];
                    OperationCouner += 2;
                }

                local_recursive(i, j, k + 1);

                if (k == size - 1)
                {
                    k = 0;
                    local_recursive(i, j + 1, k);

                    if (j == size - 1)
                    {
                        j = 0;
                        local_recursive(i + 1, j, k);
                    }
                }
            }
        }

        static void PrintMatrix(double[,] matrix, string iteration_count_message)
        {
            Console.WriteLine(iteration_count_message + '\n');
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(string.Format("{0} ", matrix[i, j]));
                }
                Console.WriteLine();
            }
        }

        static void print3DMatrix(double[,,] matrix, string iteration_count_message)
        {
            Console.WriteLine(iteration_count_message + '\n');
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(string.Format("{0} ", matrix[i, j, matrix.GetLength(2) - 1]));
                }
                Console.WriteLine();
            }
        }
        static int Get_matrix_size()
        {
            Console.WriteLine("Enter size of matrix");
            int matrix_size = Convert.ToInt32(Console.ReadLine());
            return matrix_size;
        }
    }
}
