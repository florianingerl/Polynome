using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynoms.UnitTests
{
    [TestFixture]
    public class PolynomUnitTests
    {

        [Test]
        public void OperatorPlus_TwoPolynoms_ReturnsTheCorrectResult()
        {
            Polynom result = new Polynom(new int[] { 0, 0, 1, 5 }) + new Polynom( new int[] { 7,0,0,1,1 });

            Assert.AreEqual(4, result.Degree());
            Assert.AreEqual(new Polynom(new int[] { 7, 0, 1, 6, 1 }), result);
        }

        [Test]
        public void OperatorMinus_TwoPolynoms_ReturnsTheCorrectResult()
        {
            Polynom result = new Polynom(new int[] { 0, 0, 1, 5,0 }) - new Polynom(new int[] { 7, 0, 0, 1, 1,0,0 });
            Assert.AreEqual(4, result.Degree());
            Assert.AreEqual(new Polynom(new int[] {-7,0,1,4,-1,0 }), result);   
        }

        [Test]
        public void OperatorMult_TwoPolynoms_ReturnsTheCorrectResult()
        {
            Polynom result = new Polynom(new int[] { 0, 0, 1, 5 }) * new Polynom(new int[] { 7, 0, 0, 1, 1 });

            Assert.AreEqual(7, result.Degree());
            Assert.AreEqual(new Polynom(new int[] { 0, 0, 7, 35, 0,1,6,5 }), result);
        }

        [Test]
        public void OperatorMult_OneIsANullPolynom_ReturnsNullPolynom()
        {
            Polynom result = new Polynom(new int[] { 0, 0, 1, 5 }) * new Polynom(new int[] { 0, 0, 0, 0, 0 });
            Assert.IsTrue(result.IsNullPolynom());
        }

        [Test]
        public void OperatorMult_BothAreZeroDegreePolynoms_ReturnsCorrectResult()
        {
            Polynom result = new Polynom(new int[] { 7, 0 }) * new Polynom(new int[] {5,0,0 });
            Assert.AreEqual(new Polynom(new int[] { 35, 0 }), result);
        }

        [Test]
        public void OperatorMult_OneIsAZeroDegreePolynom_ReturnsCorrectResult()
        {
            Polynom result = new Polynom(new int[] { 2, 0 }) * new Polynom(new int[] { 0, 0, 1, 5 });
            Assert.AreEqual(new Polynom(new int[] {0,0,2,10 }), result);
        }

        [Test]
        public void Degree_NullPolynom_ReturnsIntMinValue()
        {
            Polynom p = new Polynom(new int[] {0,0,0,0,0 });
            Assert.AreEqual(int.MinValue, p.Degree( ));
        }

        [Test]
        public void Degree_TwoDegreePolynom_Returns2()
        {
            Polynom p = new Polynom(new int[] { 0, 0, 4, 0 });
            Assert.AreEqual(2, p.Degree());
        }

        [Test]
        public void Degree_ZeroDegreePolynom_Returns0()
        {
            Polynom p = new Polynom(new int[] { 4, 0, 0 });
            Assert.AreEqual(0, p.Degree());
        }

        [Test]
        public void IsNullPolynom_WithNullPolynom_ReturnsTrue()
        {
            Polynom p = new Polynom(new int[] {0,0,0 });
            Assert.IsTrue(p.IsNullPolynom());
        }

        [Test]
        public void IsNullPolynom_WithNotANullPolynom_ReturnsFalse()
        {
            Polynom p = new Polynom(new int[] { 0, 0, 1, 0, 0 });
            Assert.IsFalse(p.IsNullPolynom());
        }

        [Test]
        public void ToString_WithNullPolynom_Returns0()
        {
            Polynom p = new Polynom(new int[] { 0, 0, 0 });
            Assert.AreEqual("0", p.ToString());
        }

        [Test]
        public void ToString_MoreComplexPolynom_ReturnsCorrectResult()
        {
            Polynom p = new Polynom(new int[] {0,4,5,0,6 });
            Assert.AreEqual("4x+5x^2+6x^4", p.ToString());
        }
    }
}
