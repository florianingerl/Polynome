using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynoms.UnitTests
{
    [TestFixture]
    public class PolynomExpressionCalculatorUnitTests
    {

        [Test]
        public void CalculatePolynomExpression_PolynomIs6_ReturnsCorrectResult()
        {
            var pec = new PolynomExpressionCalculator();
            Polynom result = pec.CalculatePolynomExpression("6");

            Assert.AreEqual(new Polynom(new int[] { 6, 0 }), result);
        }

        [Test]
        public void CalculatePolynomExpression_PolynomIs2ToThePowerOf4_ReturnsCorrectResult()
        {
            var pec = new PolynomExpressionCalculator();
            Polynom result = pec.CalculatePolynomExpression("2^4");
            Assert.AreEqual(new Polynom(new int[] { 16 }), result);
        }

        [Test]
        public void CalculatePolynomExpression_PolynomIsXToThePowerOf6_ReturnsCorrectResult()
        {
            var pec = new PolynomExpressionCalculator();
            Polynom result = pec.CalculatePolynomExpression("x^6");

            Assert.AreEqual(new Polynom(new int[] { 0, 0, 0, 0, 0, 0, 1 }), result);
        }

        [Test]
        public void CalculatePolynomExpression_PolynomIsX_ReturnsCorrectResult()
        {
            var pec = new PolynomExpressionCalculator();
            Polynom result = pec.CalculatePolynomExpression("x");
            Assert.AreEqual(new Polynom(new int[] { 0, 1 }), result);
        }

        [Test]
        public void CalculatePolynomExpression_PolynomInCanonicalForm_ReturnsCorrectResult()
        {
            var pec = new PolynomExpressionCalculator();
            Polynom result = pec.CalculatePolynomExpression("2^3+4+8*x+10*x^2+102*x^0+2000*x^7+7^0");
            Assert.AreEqual(new Polynom(new int[] { 115, 8, 10, 0, 0, 0, 0, 2000 }), result);
        }

        [Test]
        public void CalculatePolynomExpression_SimpleSum_ReturnsCorrectResult()
        {
            var pec = new PolynomExpressionCalculator();
            Polynom result = pec.CalculatePolynomExpression("2^3+5*x^2");
            Assert.AreEqual(new Polynom(new int[] { 8, 0,5 }), result);
        }

        [Test]
        public void CalculatePolynomExpression_PolynomToThePowerOf2_ReturnsCorrectResult()
        {
            var pec = new PolynomExpressionCalculator();
            Polynom result = pec.CalculatePolynomExpression("(2^3+5*x^2)^2");
            Assert.AreEqual(new Polynom(new int[] { 64, 0, 80, 0, 25 }), result);
        }

        [Test]
        public void CalulatePolynomExpression_ComplexExpression_ReturnsCorrectResult()
        {
            var pec = new PolynomExpressionCalculator();
            Polynom result = pec.CalculatePolynomExpression("5+10*(7*x^5+3*x^2)*(5*x+2)^2");
            Assert.AreEqual(new Polynom(new int[] {5,0,120,600,750,280, 1400,1750 }) , result);
        }

        [Test]
        public void CalulatePolynomExpression_ComplexExpressionWithOmittedMultSigns_ReturnsCorrectResult()
        {
            var pec = new PolynomExpressionCalculator();
            Polynom result = pec.CalculatePolynomExpression("5+10(7x^5+3x^2)(5x+2)^2");
            Assert.AreEqual(new Polynom(new int[] { 5, 0, 120, 600, 750, 280, 1400, 1750 }), result);
        }
    }
}
