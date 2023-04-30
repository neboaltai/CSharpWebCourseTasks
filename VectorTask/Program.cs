using System;

namespace VectorTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector vector1 = new Vector(new Vector(5));

            double temp = vector1.GetComponent(0);

            temp += 4.5;
            vector1.SetComponent(0, temp);

            Vector vector2 = new Vector(new double[] { -3, 5, 8 });

            Console.WriteLine("Заданы:");
            Console.WriteLine($"- {vector1.GetSize()}-мерный вектор v1 = {vector1}");
            Console.WriteLine($"- {vector2.GetSize()}-мерный вектор v2 = {vector2}");

            Console.WriteLine("Вычислить и найти длину вектора: -(6 * (v1 + v2) - v2)");
            Console.WriteLine();

            Console.WriteLine("Решение:");
            Console.Write("1. Прибавление вектора v2 к v1: v1 + v2 = ");
            vector1.Add(vector2);
            Console.WriteLine(vector1);

            Console.Write("2. Умножение вектора(1) на скалярную величину: 6 * (v1 + v2) = ");
            vector1.MultiplyByScalar(6);
            Console.WriteLine(vector1);

            Console.Write("3. Вычитание вектора v2 из вектора(2): 6 * (v1 + v2) - v2 = ");
            vector1.Subtract(vector2);
            Console.WriteLine(vector1);

            Console.Write("4. Умножение вектора(3) на -1 (разворот вектора): -(6 * (v1 + v2) - v2) = ");
            vector1.Reverse();
            Console.WriteLine(vector1);

            Console.WriteLine($"5. Длина вектора: {vector1.GetLength():f2}");

            Vector resultVector = new Vector(5, new double[] { -12, -25, -40 });

            Console.WriteLine();
            Console.Write($"Сравнение с ответом: {resultVector} - {vector1.Equals(resultVector)}, ");
            Console.WriteLine(vector1.GetHashCode() == resultVector.GetHashCode());

            Console.WriteLine();
            Console.Write("Вектор v3 = v1 + v2 = ");
            Vector vector3 = Vector.GetSum(resultVector, vector2);
            Console.WriteLine(vector3);

            Console.Write("Вектор v4 = v1 - v2 = ");
            Vector vector4 = Vector.GetDifference(resultVector, vector2);
            Console.WriteLine(vector4);

            Console.Write("Длина вектора v5: v1 * v2 = ");
            double vector5Length = Vector.GetDotProduct(resultVector, vector2);
            Console.WriteLine(vector5Length);
        }
    }
}
