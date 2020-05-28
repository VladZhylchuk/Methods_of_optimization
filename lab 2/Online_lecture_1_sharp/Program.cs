using System;
using System.Threading;



namespace Online_lecture_1_sharp
{
    class Program
    {

        static object locker = new object();

        static Program Prog = new Program();

        static void Main()
        {
            MatrixParams MP = new MatrixParams();


            Console.WriteLine("Натиснiть 1 щоб ввечти даннi вручу  ");
            Console.WriteLine("Натиснiть 2 щоб зарандомити даннi   ");
            int check = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(check);
            if (check == 1)
            {
                MP.size = Get_matrix_size();
                MP.check_input_type = true;
            }
            else
            {
                MP.size = Get_matrix_size();
                MP.randrng = 5;
            }



            Thread Thread1 = new Thread(new ParameterizedThreadStart(FirstThreadTask));
            Thread1.Start(MP);

            Thread Thread2 = new Thread(new ParameterizedThreadStart(SecondThreadTask));
            Thread2.Start(MP);

            Thread Thread3 = new Thread(new ParameterizedThreadStart(ThirdThreadTask));
            Thread3.Start(MP);
            
        }
        static void FirstThreadTask(object param)
        {

            MatrixParams p = (MatrixParams)param;
            switch(p.check_input_type)
            {

            }
            double[,] A = {{ 3, 4, 2}, {4, 4, 2}, {2, 4, 4 }};
            double[,] b = SetVectorBasMatrix(p.size);
            p.y = Mult(A, b);
            Console.WriteLine("\nFirstThreadTask");
            PrintMatrix(p.y);
            Thread.Sleep(500);
            p.C = Mult(Mult(p.Y33, p.y), p.K2);
            Console.WriteLine("\n444444444444444");
            PrintMatrix(p.C);
            Thread.Sleep(1000);
            p.result = Plus(Plus(p.A, p.B), Plus(p.C, p.D));
            Console.WriteLine("\nFirstThreadTask");
            PrintMatrix(p.result);
        }
        static void SecondThreadTask(object param)
        {
            Thread.Sleep(100);
            MatrixParams p = (MatrixParams)param;
            double[,] A1 = { { 1, 1, 2 }, { 3, 2, 4 }, { 1, 2, 1 } };
            //SetMatrixRandom(p.size, p.randrng);
            Console.WriteLine("\nA1");
            PrintMatrix(A1);
            double[,] b1 = { { 4, 0, 0 }, { 4, 0, 0 }, { 1, 0, 0 } };
            //SetVectorasMatrixRandom(p.size, p.randrng);
            Console.WriteLine("\nb1");
            PrintMatrix(b1);
            Console.WriteLine("c1");
            double[,] c1 = { { 4, 0, 0 }, { 3, 0, 0 }, { 4, 0, 0 } };
            //SetVectorasMatrixRandom(p.size, p.randrng);
            PrintMatrix(c1);

            p.y2 = Mult(A1, Plus(b1, Mult(c1,2)));
            Console.WriteLine("\nSecondThreadTask");
            PrintMatrix(p.y2);
            Console.WriteLine();
            PrintMatrix(Mult(p.y, Transpose<double>(p.y2)));
            p.D = Mult(Mult(p.y, p.y2), Transpose<double>(p.y2));
            Console.WriteLine("\n3333333333333333333");
            PrintMatrix(p.D);
            Thread.Sleep(600);
            p.B = Mult(Mult(p.Y32, p.y2), p.K1);
            Console.WriteLine("\n11111111111111111");
            PrintMatrix(p.B);
        }
        static void ThirdThreadTask(object param)
        {
            Thread.Sleep(400);
            MatrixParams p = (MatrixParams)param;
            double[,] A2 = { { 4, 4, 3 }, { 1, 3, 3 }, { 3, 3, 2 } };
            //SetMatrixRandom(p.size, p.randrng);
            Console.WriteLine("\nA2");
            PrintMatrix(A2);
            double[,] B2 = { { 3, 1, 3 }, { 3, 1, 1 }, { 1, 2, 3 } };
            //SetMatrixRandom(p.size, p.randrng);
            Console.WriteLine("\nB2");
            PrintMatrix(B2);
            double[,] C2 = SetCMatrix(p.size);
            Console.WriteLine("\nC2");
            PrintMatrix(C2);
            p.Y3 = Mult(A2, Minus(C2, B2));
            Console.WriteLine("\nThirdThreadTask");
            PrintMatrix(p.Y3);
            p.Y32 = Mult(p.Y3, p.Y3);
            p.Y33 = Mult(p.Y32, p.Y3);
            Thread.Sleep(700);
            p.A = Mult(Mult(p.Y3, p.K1), Plus(p.y, p.y2));
            Console.WriteLine("\n222222222222222222");
            PrintMatrix(p.A);
        }


        static private bool IsEven(int a) { return (a & 1) == 0; }
        static int Get_matrix_size()
        {
            Console.WriteLine("Enter size of matrix");
            int matrix_size = Convert.ToInt32(Console.ReadLine());
            return matrix_size;
        }
        static double[,] SetMatrixRandom(int msize, int randrange)
        {
            //об'єкт для генерації випадкових чисел
            Random rnd = new Random();
            double[,] RandomedMatrix = new double[msize, msize];

            for (int mrow = 0; mrow < msize; mrow++)
            {
                for (int mcol = 0; mcol < msize; mcol++)
                {
                    RandomedMatrix[mrow, mcol] = rnd.Next(1, randrange);
                }
            }

            return RandomedMatrix;
        }
        static void PrintMatrix(object M)
        {
            double[,] Matrix = (double[,])M;
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(0); j++)
                {
                    Console.Write(Matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        static double[,] SetMatrixKeyBoard(int msize)
        {
            double[,] Matrix = new double[msize, msize];

            for (int mrow = 0; mrow < msize; mrow++)
            {
                for (int mcol = 0; mcol < msize; mcol++)
                {
                    Console.WriteLine("Enter " + mrow + mcol + " element : ");
                    Matrix[mrow, mcol] = Convert.ToInt32(Console.ReadLine());
                }
            }
            return Matrix;
        }
        static void test()
        {
            int j = 1;
            switch (j)
            {
                case 1:
                    Console.WriteLine(1);
                    break;
                case 2:
                    Console.WriteLine(2);
                    break;

            }

        }
        static double[,] SetVectorBasMatrix(int vsize)
        {
            double[,] vectorb = new double[vsize, vsize];
            for (int i = 0; i < vsize; i++)
            {
                for (int j = 0; j < vsize; j++)
                {
                    switch (j)
                    {
                        case 0:
                            bool check = IsEven(i);
                            switch (check)
                            {
                                case true:
                                    vectorb[i, j] = Math.Round(1f / (i * i + 2 + i), 2);
                                    break;
                                case false:
                                    vectorb[i, j] = Math.Round(1f / (i + 1), 2);
                                    break;
                            }
                            break;
                        default:
                            vectorb[i, j] = 0;
                            break;
                    }
                }

            }

            return vectorb;
        }

        static double[,] Mult(double[,] MatrixA, double[,] MatrixB)
        {
            double[,] Multiplied_Matrix = new double[MatrixA.GetLength(0), MatrixA.GetLength(0)];
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixA.GetLength(0); j++)
                {
                    Multiplied_Matrix[i, j] = 0;
                    for (int k = 0; k < MatrixA.GetLength(0); k++)
                    {
                        Multiplied_Matrix[i, j] += MatrixA[i, k] * MatrixB[k, j];
                    }
                }
            }
            return Multiplied_Matrix;
        }
        static T[,] Transpose<T>(T[,] Matrix)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    T tmp = Matrix[i, j];
                    Matrix[i, j] = Matrix[j, i];
                    Matrix[j, i] = tmp;
                }
            }
            return Matrix;
        }
        static double[,] SetCMatrix(int msize)
        {
            double[,] Matrix = new double[msize, msize];
            for (int i = 0; i < msize; i++)
            {
                for (int j = 0; j < msize; j++)
                {
                    Matrix[i, j] = Math.Round(1f / ((i + 1) + (j + 1)), 2);
                }
            }
            return Matrix;
        }
        static double[,] Minus(double[,] MatrixA, double[,] MatrixB)
        {
            double[,] Result_Matrix = new double[MatrixA.GetLength(0), MatrixA.GetLength(0)];
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixA.GetLength(0); j++)
                {
                    Result_Matrix[i, j] = MatrixA[i, j] - MatrixB[i, j];
                }
            }
            return Result_Matrix;
        }
        static double[,] SetVectorasMatrixRandom(int vsize, int randrange)
        {
            Random rnd = new Random();
            double[,] vectorb = new double[vsize, vsize];
            for (int i = 0; i < vsize; i++)
            {
                for (int j = 0; j < vsize; j++)
                {
                    switch (j)
                    {
                        case 0:
                            vectorb[i, j] = rnd.Next(1, randrange);
                            break;
                        default:
                            vectorb[i, j] = 0;
                            break;
                    }
                }

            }

            return vectorb;
        }
        static double[,] Plus(double[,] MatrixA, double[,] MatrixB)
        {
            double[,] Result_Matrix = new double[MatrixA.GetLength(0), MatrixA.GetLength(0)];
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixA.GetLength(0); j++)
                {
                    Result_Matrix[i, j] = MatrixA[i, j] + MatrixB[i, j];
                }
            }
            return Result_Matrix;
        }
        static double[,] Mult(double[,] MatrixA, double number)
        {
            double[,] Multiplied_Matrix = new double[MatrixA.GetLength(0), MatrixA.GetLength(0)];
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixA.GetLength(0); j++)
                {
                        Multiplied_Matrix[i, j] = MatrixA[i, j] * number;
                }
            }
            return Multiplied_Matrix;
        }
    }
    class MatrixParams
    {
        public int size;
        public int randrng;
        public double[,] y;
        public double[,] y2;
        public double[,] Y3;
        public double K1 = 0.003;
        public double K2 = 0.1;
        public double[,] yt;
        public double[,] A;
        public double[,] B;
        public double[,] C;
        public double[,] D;
        public double[,] Y32;
        public double[,] Y33;
        public double[,] result;
        public bool check_input_type;
    }



}
