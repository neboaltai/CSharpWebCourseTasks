using VectorTask;

namespace UnitTests.Tests
{
    public class VectorTests
    {
        private readonly Vector _vector1;
        private readonly Vector _vector2;
        private const double Number = -1.0;

        public VectorTests()
        {
            _vector1 = new Vector(new[] { 2.5, 3.0 });
            _vector2 = new Vector(new[] { 1.5, -2.0, 4.0 });
        }

        [Fact]
        public void TestAdd()
        {
            var expected = new Vector(new[] { 4.0, 1.0, 4.0 });

            var actual = Add(_vector1, _vector2);
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestMultiplyByScalar()
        {
            var expected = new Vector(new[] { -1.5, 2.0, -4.0 });

            var actual = MultiplyByScalar(_vector2, Number);
            
            Assert.Equal(expected, actual);
        }

        private static Vector Add(Vector vector1, Vector vector2)
        {
            vector1.Add(vector2);

            return vector1;
        }

        private static Vector MultiplyByScalar(Vector vector2, double number)
        {
            vector2.MultiplyByScalar(number);

            return vector2;
        }
    }
}