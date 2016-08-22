using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Polynoms
{
    /// <summary>
    /// Class for calculating the canonical form of an arbitrary polyom expressio in the single variable x.
    /// </summary>
    public class PolynomExpressionCalculator
    {
        /// <summary>
        /// Calculates the canonical form of an arbitrary polynom expression in the single variable x.
        /// </summary>
        /// <param name="expression">Something like 5+10(7x^5+3x^2)(5x+2)^2</param>
        public Polynom CalculatePolynomExpression(string expression)
        {
            expression = PreparePolynomExpression(expression);

            Polynom result = IfItsASumCalculateIt(expression);
            if (result != null) return result;

            result = IfItsAProductCalculateIt(expression);
            if (result != null) return result;

            result = IfItsAnExpressionInBracketsCalculateIt(expression);
            if (result != null) return result;

            result = IfItsAPowerOfXCalculateIt(expression);
            if (result != null) return result;

            result = IfItsAnOrdinaryPowerCalculateIt(expression);
            if (result != null) return result;
          
            throw new InvalidOperationException("Not a valid polynom expression!");
        }

        private string PreparePolynomExpression(string expression)
        {
            expression = expression.Trim();
            expression = Regex.Replace(expression, "\\s+", " ");
            expression = Regex.Replace(expression, "(?<=\\d)(?=x|\\()", "*");
            expression = Regex.Replace(expression, "(?<=\\))(?=\\()", "*");
            return expression;
        }

        private Polynom IfItsASumCalculateIt(string expression)
        {
            MatchCollection mc = Regex.Matches(expression, "\\(([^()]+|\\((?<depth>)|\\)(?<-depth>))*(?(depth)(?!))\\)|\\+");

            if (mc.Any((Match m) => m.Groups[0].Value == "+"))
            {
                int start = 0;
                Polynom result = new Polynom(new int[] { 0 });

                foreach (Match m in mc)
                {
                    if (m.Groups[0].Value == "+")
                    {
                        result += CalculatePolynomExpression(expression.Substring(start, m.Index - start));
                        start = m.Index + 1;
                    }
                }

                result += CalculatePolynomExpression(expression.Substring(start));
                return result;
            }

            return null;
        }

        private Polynom IfItsAProductCalculateIt(string expression)
        {
            MatchCollection mc = Regex.Matches(expression, "\\(([^()]+|\\((?<depth>)|\\)(?<-depth>))*(?(depth)(?!))\\)|\\+|\\*");
            if (mc.Any((Match m) => m.Groups[0].Value == "*"))
            {
                int start = 0;
                Polynom result = new Polynom(new int[] { 1 });

                foreach (Match m in mc)
                {
                    if (m.Groups[0].Value == "*")
                    {
                        result *= CalculatePolynomExpression(expression.Substring(start, m.Index - start));
                        start = m.Index + 1;
                    }
                }

                result *= CalculatePolynomExpression(expression.Substring(start));
                return result;

            }
            return null;
        }

        private Polynom IfItsAnExpressionInBracketsCalculateIt(string expression)
        {
            //It's a factor
            Match m = Regex.Match(expression, "\\((?<expression>([^()]+|\\((?<depth>)|\\)(?<-depth>))*(?(depth)(?!)))\\)(?<power>\\^(?<exponent>\\d+))?");
            if (m != null && m.Groups[0].Value == expression)
            {
                Polynom p = CalculatePolynomExpression(m.Groups["expression"].Value);
                if (m.Groups["power"] != null && m.Groups["power"].Success)
                {
                    int power = int.Parse(m.Groups["exponent"].Value);
                    Polynom result = new Polynom(new int[] { 1 });

                    for (int i = 0; i < power; ++i)
                    {
                        result *= p;
                    }

                    return result;
                }

                return p;
            }
            return null;
        }

        public Polynom IfItsAPowerOfXCalculateIt(string expression)
        {
            Match m = Regex.Match(expression, "x(?<power>\\^(?<exponent>\\d+))?");
            if (m != null && m.Groups[0].Value == expression)
            {
                if (!(m.Groups["power"] != null && m.Groups["power"].Success))
                {
                    return new Polynom(new int[] { 0, 1 });
                }
                else
                {
                    int power = int.Parse(m.Groups["exponent"].Value);
                    int[] coefficients = new int[power + 1];
                    coefficients[power] = 1;
                    return new Polynom(coefficients);
                }
            }
            return null;
        }

        private Polynom IfItsAnOrdinaryPowerCalculateIt(string expression)
        {
            Match m = Regex.Match(expression, "(?<base>\\d+)(?<power>\\^(?<exponent>\\d+))?");
            if (m != null && m.Groups[0].Value == expression)
            {
                int b = int.Parse(m.Groups["base"].Value);
                if (!(m.Groups["power"] != null && m.Groups["power"].Success))
                {
                    return new Polynom(new int[] { b });
                }
                else
                {
                    int e = int.Parse(m.Groups["exponent"].Value);
                    int result = 1;
                    for (int i = 0; i < e; ++i)
                        result *= b;

                    return new Polynom(new int[] { result });
                }
            }

            return null;
        }


    }

    /// <summary>
    /// Provides extension methods that can be used on <see cref="MatchCollection"/> objects.
    /// </summary>
    public static class MatchCollectionExtensionMethods
    {
        /// <summary>
        /// Returns true if any of the matches in <paramref name="mc"/> satisfies <paramref name="p"/>.
        /// </summary>
        public static bool Any(this MatchCollection mc, Predicate<Match> p)
        {
            foreach (Match m in mc)
            {
                if (p(m)) return true;
            }
            return false;
        }
    }
}
