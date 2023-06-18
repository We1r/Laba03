using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Laba3.SquareMatrix;

namespace Laba3
{
  class SquareMatrix : ICloneable, IComparable<SquareMatrix>, IEquatable<SquareMatrix>
  {
    private double[,] matrix;
    private int size;

    public SquareMatrix(int size)
    {
      this.size = size;
      matrix = new double[size, size];
      Random rand = new Random();
      for (int rowCount = 0; rowCount < size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < size; ++columnCount)
        {
          matrix[rowCount, columnCount] = rand.Next(20);
        }
      }
    }

    public SquareMatrix(double[,] matrix)
    {
      this.matrix = matrix;
      this.size = matrix.GetLength(0);
    }

    public SquareMatrix DeepCopy()
    {
      double[,] copiedMatrix = new double[size, size];
      Array.Copy(matrix, copiedMatrix, matrix.Length);

      return new SquareMatrix(copiedMatrix);
    }

    public static SquareMatrix operator +(SquareMatrix first, SquareMatrix second)
    {
      if (first.Size != second.Size)
        throw new MatrixException("Матрицы не соответствуют размерам.");

      double[,] result = new double[first.Size, first.Size];

      for (int rowCount = 0; rowCount < first.Size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < first.Size; ++columnCount)
        {
          result[rowCount, columnCount] = first[rowCount, columnCount] + second[rowCount, columnCount];
        }
      }

      return new SquareMatrix(result);
    }

    public static SquareMatrix operator *(SquareMatrix first, SquareMatrix second)
    {
      if (first.Size != second.Size)
        throw new MatrixException("Матрицы не соответствуют размерам.");

      double[,] result = new double[first.Size, first.Size];

      for (int rowCount = 0; rowCount < first.Size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < first.Size; ++columnCount)
        {
          for (int k = 0; k < first.Size; k++)
          {
            result[rowCount, columnCount] += first[rowCount, k] * second[k, columnCount];
          }
        }
      }

      return new SquareMatrix(result);
    }

    public static bool operator >(SquareMatrix first, SquareMatrix second)
    {
      if (first.Size != second.Size)
        throw new MatrixException("Матрицы не соответствуют размерам.");

      double sumA = 0;
      double sumB = 0;

      for (int rowCount = 0; rowCount < first.Size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < first.Size; ++columnCount)
        {
          sumA += first[rowCount, columnCount];
          sumB += second[rowCount, columnCount];
        }
      }

      return sumA > sumB;
    }

    public static bool operator <(SquareMatrix first, SquareMatrix second)
    {
      if (first.Size != second.Size)
        throw new MatrixException("Матрицы не соответствуют размерам.");

      double sumA = 0;
      double sumB = 0;

      for (int rowCount = 0; rowCount < first.Size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < first.Size; ++columnCount)
        {
          sumA += first[rowCount, columnCount];
          sumB += second[rowCount, columnCount];
        }
      }

      return sumA < sumB;
    }

    public static bool operator >=(SquareMatrix first, SquareMatrix second)
    {
      if (first.Size != second.Size)
        throw new MatrixException("Матрицы не соответствуют размерам.");

      double sumA = 0;
      double sumB = 0;

      for (int rowCount = 0; rowCount < first.Size; rowCount++)
      {
        for (int columnCount = 0; columnCount < first.Size; ++columnCount)
        {
          sumA += first[rowCount, columnCount];
          sumB += second[rowCount, columnCount];
        }
      }

      return sumA >= sumB;
    }

    public static bool operator <=(SquareMatrix first, SquareMatrix second)
    {
      if (first.Size != second.Size)
        throw new MatrixException("Матрицы не соответствуют размерам.");

      double sumA = 0;
      double sumB = 0;

      for (int rowCount = 0; rowCount < first.Size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < first.Size; ++columnCount)
        {
          sumA += first[rowCount, columnCount];
          sumB += second[rowCount, columnCount];
        }
      }

      return sumA <= sumB;
    }

    public static bool operator ==(SquareMatrix first, SquareMatrix second)
    {
      if (first.Size != second.Size)
        throw new MatrixException("Матрицы не соответствуют размерам.");
      for (int rowCount = 0; rowCount < first.Size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < first.Size; ++columnCount)
        {
          if (first[rowCount, columnCount] != second[rowCount, columnCount])
            return false;
        }
      }

      return true;
    }

    public static bool operator !=(SquareMatrix first, SquareMatrix second)
    {
      if (first.Size != second.Size)
        throw new MatrixException("Матрицы не соответствуют размерам.");

      return !(first == second);
    }

    public static explicit operator double(SquareMatrix matrix)
    {
      double sum = 0;

      for (int rowCount = 0; rowCount < matrix.Size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < matrix.Size; ++columnCount)
        {
          sum += matrix[rowCount, columnCount];
        }
      }

      return sum;
    }

    public static implicit operator bool(SquareMatrix matrix)
    {
      for (int rowCount = 0; rowCount < matrix.Size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < matrix.Size; ++columnCount)
        {
          if (matrix[rowCount, columnCount] == 0)
            return false;
        }
      }

      return true;
    }

    public override string ToString()
    {
      string result = "";

      for (int rowCount = 0; rowCount < size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < size; ++columnCount)
        {
          result += matrix[rowCount, columnCount] + "\t";
        }
        result += "\n";
      }

      return result;
    }

    public int CompareTo(SquareMatrix other)
    {
      if (this > other)
        return 1;
      else if (this < other)
        return -1;
      else
        return 0;
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is SquareMatrix))
        return false;

      SquareMatrix other = (SquareMatrix)obj;

      if (this.Size != other.Size)
        return false;

      for (int rowCount = 0; rowCount < this.Size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < this.Size; ++columnCount)
        {
          if (this[rowCount, columnCount] != other[rowCount, columnCount])
            return false;
        }
      }

      return true;
    }

    public override int GetHashCode()
    {
      int hash = 17;
      hash = hash * 23 + size.GetHashCode();
      hash = hash * 23 + matrix.GetHashCode();
      return hash;
    }

    public double Determinant()
    {
      double result = 1;
      double[,] temp = new double[size, size];

      for (int rowCount = 0; rowCount < size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < size; ++columnCount)
        {
          temp[rowCount, columnCount] = matrix[rowCount, columnCount];
        }
      }

      for (int rowCount = 0; rowCount < size - 1; ++rowCount)
      {
        for (int columnCount = rowCount + 1; columnCount < size; ++columnCount)
        {
          double k = temp[columnCount, rowCount] / (double)temp[rowCount, rowCount];

          for (int l = 0; l < size; l++)
          {
            temp[columnCount, l] -= k * temp[rowCount, l];
          }
        }
      }

      for (int rowCount = 0; rowCount < size; ++rowCount)
      {
        result *= temp[rowCount, rowCount];
      }

      return result;
    }

    public SquareMatrix Inverse()
    {
      double det = Determinant();

      if (det == 0)
        throw new MatrixException("Матрицы не соответствуют размерам.");

      double[,] temp = new double[size, size];
      SquareMatrix result = new SquareMatrix(size);

      for (int rowCount = 0; rowCount < size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < size; ++columnCount)
        {
          temp[rowCount, columnCount] = Convert.ToDouble(matrix[rowCount, columnCount]);
        }
      }

      for (int rowCount = 0; rowCount < size; ++rowCount)
      {
        double div = temp[rowCount, rowCount];

        for (int columnCount = 0; columnCount < size; ++columnCount)
        {
          temp[rowCount, columnCount] /= div;
          result[rowCount, columnCount] /= div;
        }

        for (int columnCount = 0; columnCount < size; ++columnCount)
        {
          if (columnCount != rowCount)
          {
            double mult = temp[columnCount, rowCount];

            for (int k = 0; k < size; k++)
            {
              temp[columnCount, k] -= mult * temp[rowCount, k];
              result[columnCount, k] -= mult * result[rowCount, k];
            }
          }
        }
      }

      return result;
    }

    public int Size
    {
      get { return size; }
    }

    public double this[int rowCount, int columnCount]
    {
      get { return matrix[rowCount, columnCount]; }
      set { matrix[rowCount, columnCount] = value; }
    }

    public bool Equals(SquareMatrix other)
    {
      if (other == null)
        return false;

      if (this.Size != other.Size)
        return false;

      for (int rowCount = 0; rowCount < this.Size; ++rowCount)
      {
        for (int columnCount = 0; columnCount < this.Size; ++columnCount)
        {
          if (this[rowCount, columnCount] != other[rowCount, columnCount])
            return false;
        }
      }

      return true;
    }

    public object Clone()
    {
      return DeepCopy();
    }
  }

  public class MatrixException : Exception
  {
    public MatrixException(string message) : base(message)
    {

    }
  }


  internal class Program
  {
    static void Main(string[] args)
    {
      try
      {
        Console.WriteLine("Введите размер матрицы:");
        int size = int.Parse(Console.ReadLine());

        SquareMatrix firstMatrix = new SquareMatrix(size);

        Console.WriteLine("Введите размер второй матрицы:");
        int size2 = int.Parse(Console.ReadLine());

        SquareMatrix secondMatrix = new SquareMatrix(size2);

        Console.WriteLine("Первая матрица:\n" + firstMatrix);
        Console.WriteLine("Вторая матрица:\n" + secondMatrix);

        SquareMatrix sum = firstMatrix + secondMatrix;
        Console.WriteLine("Первая + Вторая:\n" + sum);

        SquareMatrix mult = firstMatrix * secondMatrix;
        Console.WriteLine("Первая * Вторая:\n" + mult);

        bool result = firstMatrix > secondMatrix;
        Console.WriteLine("Первая больше, чем Вторая: " + result);

        int sumOfMatrix = (int)firstMatrix;
        Console.WriteLine("Сумма Первой матрицы: " + sumOfMatrix);

        bool nonZero = firstMatrix;
        Console.WriteLine("Отстуцтвие нулей в Первой матрице: " + nonZero);

        SquareMatrix inverseMatrix = firstMatrix.Inverse();
        Console.WriteLine("Обратная матрица от Первой:\n" + inverseMatrix);

        SquareMatrix clonedMatrix = firstMatrix.DeepCopy();
        Console.WriteLine("Скопированная матрица:\n" + clonedMatrix);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

      Console.ReadLine();
    }
  }
}