using System;
using System.Linq;

namespace VectorTask
{
    public class Vector
    {
        private double[] _components;

        public Vector(int size) : this(size, null) { }

        public Vector(double[] components) : this(components.Length, components) { }

        public Vector(Vector vector) : this(vector._components.Length, vector._components) { }

        public Vector(int size, double[] components)
        {
            if (size <= 0)
            {
                throw new ArgumentException($"Parameter value {size} is invalid. The count of components must be > 0", nameof(size));
            }

            _components = new double[size];

            if (components != null && components.Length != 0)
            {
                Array.Copy(components, _components, Math.Min(size, components.Length));
            }
        }

        public int GetSize()
        {
            return _components.Length;
        }

        public override string ToString()
        {
            return $"{{{string.Join(", ", _components)}}}";
        }

        public void Add(Vector vector)
        {
            if (vector._components.Length > _components.Length)
            {
                Array.Resize(ref _components, vector._components.Length);
            }

            for (var i = 0; i < vector._components.Length; i++)
            {
                _components[i] += vector._components[i];
            }
        }

        public void Subtract(Vector vector)
        {
            if (vector._components.Length > _components.Length)
            {
                Array.Resize(ref _components, vector._components.Length);
            }

            for (var i = 0; i < vector._components.Length; i++)
            {
                _components[i] -= vector._components[i];
            }
        }

        public void MultiplyByScalar(double number)
        {
            for (var i = 0; i < _components.Length; i++)
            {
                _components[i] *= number;
            }
        }

        public void Reverse()
        {
            MultiplyByScalar(-1);
        }

        public double GetLength()
        {
            var componentSquaresSum = _components.Sum(component => component * component);

            return Math.Sqrt(componentSquaresSum);
        }

        public double GetComponent(int index)
        {
            return _components[index];
        }

        public void SetComponent(int index, double component)
        {
            _components[index] = component;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var vector = (Vector)obj;

            if (_components.Length != vector._components.Length)
            {
                return false;
            }

            return !_components.Where((t, i) => t != vector._components[i]).Any();
        }

        public override int GetHashCode()
        {
            const int prime = 17;

            return _components.Aggregate(1, (current, e) => prime * current + e.GetHashCode());
        }

        public static Vector GetSum(Vector vector1, Vector vector2)
        {
            var result = new Vector(vector1);

            result.Add(vector2);

            return result;
        }

        public static Vector GetDifference(Vector vector1, Vector vector2)
        {
            var result = new Vector(vector1);

            result.Subtract(vector2);

            return result;
        }

        public static double GetDotProduct(Vector vector1, Vector vector2)
        {
            double result = 0;
            double minVectorSize = Math.Min(vector1._components.Length, vector2._components.Length);

            for (var i = 0; i < minVectorSize; i++)
            {
                result += vector1._components[i] * vector2._components[i];
            }

            return result;
        }
    }
}
