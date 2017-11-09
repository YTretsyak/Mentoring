using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParallelLibrary;

namespace ParallelLibraryTests
{
    [TestClass]
    public class MatrixManagerTests
    {
        [TestMethod]
        public void SumOfElementsTest()
        {
            // arrange  
            var matrixManager = new MatrixManager();
            var matrix = new int[2, 2] { { 1, 2 }, { 3, 4 } };

            // act 
            var sumOfElements = matrixManager.SumOfElements(matrix);

            // assert 
            Assert.AreEqual(10, sumOfElements);
        }

        [TestMethod]
        public void ShouldMultiplyMatrices()
        {
            // arrange  
            var matrixManager = new MatrixManager();
            var matixA = new int[2, 2] {{1,2},{3,4}};
            var matrixB = new int[2, 2] {{5, 6}, {7, 8}};
            var expectedResult = new int[2, 2] {{19, 22}, {43, 50}};

            // act 
            var resultMatrix = matrixManager.Multiply(matixA, matrixB);

            // assert 
            Assert.AreEqual(matrixManager.SumOfElements(resultMatrix), matrixManager.SumOfElements(expectedResult));
        }

        [TestMethod]
        public void ShouldThrowExceptionTest()
        {
            // arrange  
            var matrixManager = new MatrixManager();
            var matixA = new int[2, 3] { { 1, 2 ,7}, { 3, 4,6 } };
            var matrixB = new int[2, 3] { { 5, 6, 0 }, { 7, 8, 7 } };
            Exception exception = null;

            // act 
            try
            {
                var resultMatrix = matrixManager.Multiply(matixA, matrixB);
            }
            catch (ArgumentException e)
            {
                exception = e;
            }

            // assert 
            Assert.IsNotNull(exception);
            Assert.AreEqual("MatrixA columns number should be equals to MatrixB rows number!", exception.Message);
        }
    }
}
