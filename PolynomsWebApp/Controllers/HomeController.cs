using PolynomsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Polynoms;
using PolynomsWebApp.ViewModels;

namespace PolynomsWebApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(PolynomViewModel ps = null)
        {
            if (ps == null || String.IsNullOrWhiteSpace(ps.PolynomExpression) )
            {
                ps = new PolynomViewModel();
            }
            else
            {
                PolynomExpressionCalculator pec = new PolynomExpressionCalculator();
                Polynom result = pec.CalculatePolynomExpression(ps.PolynomExpression);

                ps.CanonicalPolynomExpression = result.ToString();
            }
            return View(ps);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}