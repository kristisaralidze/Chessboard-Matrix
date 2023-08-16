using matrixMxN;

namespace MxNmatrixTest
{
    [TestClass]
    public class ChessBoardTest
    {
        [TestMethod]
        public void CreateMatrix()
        {
            Assert.ThrowsException<Matrix.NegativeSizeException>(() => _ = new Matrix(0,0));
            Assert.ThrowsException<Matrix.NegativeSizeException>(() => _ = new Matrix(-1,-1));

            Matrix a = new(1,1);
            Assert.AreEqual(a.Length(), 1);

            Matrix b = new(2,2);
            Assert.AreEqual(b.Length(), 2);

            Matrix c = new(5,8);
            Assert.AreEqual(c.Length(), 20);

            Matrix g = new Matrix(9,5);
            Assert.AreEqual(g.Length(), 23);


            Assert.AreEqual(a.GetElement(1, 1), 0);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Assert.AreEqual(c.GetElement((i + 1), (j + 1)), 0);
                }
            }
        }


        [TestMethod]
        public void Getter()
        {
            Matrix k = new(3, 4);
            k.SetElement(1, 1, 1);
            k.SetElement(1, 3, 1);
            k.SetElement(2, 2, 1);
            k.SetElement(2, 4, 1);
            k.SetElement(3, 1, 1);
            k.SetElement(3, 3, 1);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (((i + 1) % 2 == 1 && (j + 1) % 2 == 1) || ((i + 1) % 2 == 0 && (j + 1) % 2 == 0))
                    {
                        Assert.AreEqual(k.GetElement((i + 1), (j + 1)), 1);
                    }
                    else
                    {
                        Assert.AreEqual(k.GetElement((i + 1), (j + 1)), 0);
                    }
                }
            }
            try
            {
                k.GetElement(4, 1);//i don't test setting an element on invalid index because setter was not in task
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is Matrix.InvalidIndexException);
            }
            try
            {
                k.GetElement(2, 5);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is Matrix.InvalidIndexException);
            }
        }

        [TestMethod]
        public void Add()
        {
            Matrix a = new(3,5);
            Matrix b = new(3,5);
            Matrix zero = new(3,5);
            Matrix d = new(2,7);
            Matrix c;

            a.SetElement(1, 1, 1);
            a.SetElement(1, 3, 1);
            a.SetElement(1, 5, 1);
            a.SetElement(2, 2, 1);
            a.SetElement(2, 4, 1);
            a.SetElement(3, 1, 1);
            a.SetElement(3, 3, 1);
            a.SetElement(3, 5, 1);

            b.SetElement(1, 1, 100);
            b.SetElement(1, 3, 0);
            b.SetElement(1, 5, 0);
            b.SetElement(2, 2, 0);
            b.SetElement(2, 4, 0);
            b.SetElement(3, 1, 0);
            b.SetElement(3, 3, 0);
            b.SetElement(3, 5, 0);

            c = Matrix.Add(a, b);

            Assert.AreEqual(c.GetElement(1, 1), 101);
            Assert.AreEqual(c.GetElement(2, 2), 1);
            Assert.IsTrue(a.Equals(Matrix.Add(a, zero)));
            Assert.IsTrue(a.Equals(Matrix.Add(zero, a)));
            Assert.IsTrue(Matrix.Add(a, b).Equals(Matrix.Add(b, a)));
            Assert.IsTrue(Matrix.Add((Matrix.Add(a, b)), c).Equals(Matrix.Add((Matrix.Add(b, c)), a)));

            Assert.ThrowsException<Matrix.DifferentSizeException>(() => Matrix.Add(a, d));
        }

        [TestMethod]
        public void Mul()
        {
            Matrix a = new(3,4);
            Matrix b = new(4,5);
            Matrix d = new(7,5);
            Matrix c;

            a.SetElement(1, 1, 1);
            a.SetElement(1, 3, 2);
            a.SetElement(2, 2, 3);
            a.SetElement(2, 4, 4);
            a.SetElement(3, 1, 5);
            a.SetElement(3, 3, 6);

            b.SetElement(1, 1, 1);
            b.SetElement(1, 3, 1);
            b.SetElement(1, 5, 1);
            b.SetElement(2, 2, 1);
            b.SetElement(2, 4, 1);
            b.SetElement(3, 1, 1);
            b.SetElement(3, 3, 1);
            b.SetElement(3, 5, 1);
            b.SetElement(4, 2, 1);
            b.SetElement(4, 4, 1);

            c = Matrix.Multiply(a, b);

            Assert.AreEqual(c.GetElement(1, 1), 3);
            Assert.AreEqual(c.GetElement(2, 2), 7);
            Assert.AreEqual(c.GetElement(3, 1), 11);

            

            Assert.ThrowsException<Matrix.DifferentSizeException>(() => Matrix.Multiply(a, d));

            Matrix f = new(3, 3);
            Matrix g = new(3, 3);
            Matrix t = new(3, 3);
            Matrix zero = new(3, 3);


            f.SetElement(1, 1, 2);
            f.SetElement(1, 3, 2);
            f.SetElement(2, 2, 2);
            f.SetElement(3, 1, 2);
            f.SetElement(3, 3, 2);

            g.SetElement(1, 1, 3);
            g.SetElement(1, 3, 3);
            g.SetElement(2, 2, 3);
            g.SetElement(3, 1, 3);
            g.SetElement(3, 3, 3);

            t = Matrix.Multiply(f, g);
            Assert.IsTrue(Matrix.Multiply(f, (Matrix.Multiply(g, t))).Equals(Matrix.Multiply(t, (Matrix.Multiply(f, g)))));
            Assert.IsTrue(zero.Equals(Matrix.Multiply(f, zero)));
            Assert.IsTrue(Matrix.Multiply(f, g).Equals(Matrix.Multiply(g, f)));
        }
        [TestMethod]
        public void SetMatrix()
        {
            List<int> vec = new List<int>() { 1, 2, 3, 4, 5 };
            Matrix a = new Matrix(3,3);
            Matrix b = new Matrix(3,4);

            Assert.AreEqual(a.GetElement(1, 1), 0);
            Assert.AreEqual(a.GetElement(1, 3), 0);
            Assert.AreEqual(a.GetElement(2, 2), 0);
            Assert.AreEqual(a.GetElement(3, 1), 0);
            Assert.AreEqual(a.GetElement(3, 3), 0);

            a.Set(vec);
            Assert.AreEqual(a.GetElement(1, 1), 1);
            Assert.AreEqual(a.GetElement(1, 3), 2);
            Assert.AreEqual(a.GetElement(2, 2), 3);
            Assert.AreEqual(a.GetElement(3, 1), 4);
            Assert.AreEqual(a.GetElement(3, 3), 5);

            Assert.ThrowsException<Matrix.DifferentSizeException>(() => b.Set(vec));
        }
    }
}