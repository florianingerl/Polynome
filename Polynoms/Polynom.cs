using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynoms
{
    /// <summary>
    /// Represents a polynom in the single variable x.
    /// </summary>
    public class Polynom
    {
        private int[] coefficients;

        /// <summary>
        /// Creates a new instance of a Polynom with the specified coefficients.
        /// </summary>
        /// <param name="coefficients"><paramref name="coefficients"/>[i] is the coefficient belonging to x^i</param>
        public Polynom(int[] coefficients)
        {
            if (coefficients == null) throw new InvalidOperationException("Coefficients mustn't be null!");
            this.coefficients = coefficients;
        }

        /// <summary>
        /// Returns true if all coefficients are 0, and false otherwise
        /// </summary>
        /// <returns>true if all coefficients are 0, and false otherwise</returns>
        public bool IsNullPolynom()
        {
            
            if (coefficients.All(x => x == 0))
                return true;
            return false; 
        }

        /// <summary>
        /// Returns the degree of the polynom
        /// </summary>
        /// <returns>If <see cref="IsNullPolynom"/> returns true, this method returns int.MinValue (not -infinity).
        /// Otherwise it returns the degree of the polynom.</returns>
        public int Degree()
        {
            
            if (IsNullPolynom())
                return int.MinValue;

            int degree = -1;
            for(int i=0; i < coefficients.Length; ++i)
            {
                if(coefficients[i] != 0)
                {
                    degree = i;
                }
            }

            return degree;
        }

        /// <summary>
        /// Multiplies two polynoms
        /// </summary>
        public static Polynom operator*(Polynom p1, Polynom p2)
        {
            if (p1.IsNullPolynom() || p2.IsNullPolynom()) return new Polynom(new int[] { 0 });

            int degree;
            if (p1.Degree() == 0 && p2.Degree() == 0) degree = 0;
            else if (p1.Degree() == 0) degree = p2.Degree();
            else if (p2.Degree() == 0) degree = p1.Degree();
            else degree = p1.Degree() * p2.Degree() + 1;

            int[] coefficients = new int[degree + 1];

            for(int k=0; k <= degree; ++k)
            {
                for(int i=Math.Max(0, k - p2.Degree()); i <= Math.Min(k, p1.Degree() ); ++i )
                {
                    coefficients[k] += p1.coefficients[i] * p2.coefficients[k - i];
                }
            }

            return new Polynom(coefficients);
        }

        /// <summary>
        /// Adds two polynoms
        /// </summary>
        public static Polynom operator+(Polynom p1, Polynom p2)
        {
            int degree = Math.Max(p1.Degree(), p2.Degree());
            int[] coefficients = new int[degree + 1];

            for(int i=0; i <= degree; ++i)
            {
                if( i < p1.coefficients.Length)
                {
                    coefficients[i] += p1.coefficients[i];
                }
                if( i < p2.coefficients.Length )
                {
                    coefficients[i] += p2.coefficients[i];
                }
            }

 
            return new Polynom(coefficients);
        }

        /// <summary>
        /// Substracts two polynoms
        /// </summary>
        /// <param name="p2">The polynom that is substracted from <paramref name="p1"/></param>
        public static Polynom operator-(Polynom p1, Polynom p2)
        {
            int degree = Math.Max(p1.Degree(), p2.Degree());
            int[] coefficients = new int[degree + 1];

            for (int i = 0; i <= degree; ++i)
            {
                if (i < p1.coefficients.Length)
                {
                    coefficients[i] += p1.coefficients[i];
                }
                if (i < p2.coefficients.Length)
                {
                    coefficients[i] -= p2.coefficients[i];
                }
            }


            return new Polynom(coefficients);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Polynom))
                return false;
            Polynom p = (Polynom)obj;

            if (p.Degree() != Degree())
                return false;

            for(int i=0; i <= Degree(); ++i)
            {
                if( coefficients[i] != p.coefficients[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            if (IsNullPolynom()) return "0";
            StringBuilder sb = new StringBuilder();

            if (coefficients[0] != 0)
                sb.Append(coefficients[0].ToString());
            if (Degree() > 0 && coefficients[1] != 0)
            {
                if (sb.Length != 0) sb.Append("+");
                sb.Append(coefficients[1].ToString()).Append("x");
            }

            for(int i=2; i <= Degree(); ++i)
            {
                if(coefficients[i]!=0)
                {
                    if (sb.Length != 0) sb.Append("+");
                    sb.Append(coefficients[i].ToString()).Append("x^").Append(i.ToString());
                }
            }


            return sb.ToString();
        }
    }
}
