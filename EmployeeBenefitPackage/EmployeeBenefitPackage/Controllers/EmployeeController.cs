using System;
using EmployeeBenefitPackage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefitPackage.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(EmpDependent emp)
        {
            if (emp.Name != null && emp.MaritalStatus != null)
            {
                emp.GrossPay = 2000;

                var BenftPerYr = 83.3; //$1000/year(12 months) for each employee

                var TenPercDisc = 0.1;  //$10% discount for an employee or dependent whose name starts with 'A'


                //benefits deduction $1000/year.

                if (emp.MaritalStatus == "Single")
                {
                    if (emp.Name.StartsWith("A"))
                    {
                        emp.Deduction = BenftPerYr - TenPercDisc;
                    }
                    else
                    {
                        emp.Deduction = BenftPerYr;
                    }

                    emp.NetSalary = emp.GrossPay - emp.Deduction;

                    HttpContext.Session.SetString("Name", emp.Name);
                    HttpContext.Session.SetString("Deduction", emp.Deduction.ToString());
                    HttpContext.Session.SetString("NetSalary", emp.NetSalary.ToString());
                    return RedirectToAction("EmpSalaryInfo", "Employee");

                }
                else if (emp.MaritalStatus == "Married" && emp.Children != null && emp.DependentNameStartsWithA != null)
                {
                    var DepdntCstPerYr = 41.6; //$500/year(12 months) for each employee & dependent

                    if (emp.Name.StartsWith("A") && Int32.Parse(emp.DependentNameStartsWithA) != 0)
                    {
                        if (emp.Children == "N/A")
                        {
                            emp.Deduction = BenftPerYr + DepdntCstPerYr - ((Int32.Parse(emp.DependentNameStartsWithA) + 1) * TenPercDisc);
                        }
                        else
                        {
                            //dependent(children & spouse) deduction $500/year.
                            emp.Deduction = BenftPerYr + (DepdntCstPerYr * (Int32.Parse(emp.Children) + 1)) - ((Int32.Parse(emp.DependentNameStartsWithA) + 1) * TenPercDisc);
                        }
                    }
                    else if (emp.Name.StartsWith("A") && Int32.Parse(emp.DependentNameStartsWithA) == 0)
                    {
                        if (emp.Children == "N/A")
                        {
                            emp.Deduction = BenftPerYr + DepdntCstPerYr - TenPercDisc;
                        }
                        else
                        {
                            //dependent(children & spouse) deduction $500/year.
                            emp.Deduction = BenftPerYr + (DepdntCstPerYr * (Int32.Parse(emp.Children) + 1)) - TenPercDisc;
                        }

                    }
                    else if (!emp.Name.StartsWith("A") && Int32.Parse(emp.DependentNameStartsWithA) != 0)
                    {
                        if (emp.Children == "N/A")
                        {
                            emp.Deduction = BenftPerYr + DepdntCstPerYr - (Int32.Parse(emp.DependentNameStartsWithA) * TenPercDisc);
                        }
                        else
                        {
                            //dependent(children & spouse) deduction $500/year.
                            emp.Deduction = BenftPerYr + (DepdntCstPerYr * (Int32.Parse(emp.Children) + 1)) - (Int32.Parse(emp.DependentNameStartsWithA) * TenPercDisc);
                        }

                    }
                    else
                    {
                        if (emp.Children == "N/A")
                        {
                            emp.Deduction = BenftPerYr + DepdntCstPerYr;
                        }
                        else
                        {
                            //dependent(children & spouse) deduction $500/year.
                            emp.Deduction = BenftPerYr + (DepdntCstPerYr * (Int32.Parse(emp.Children) + 1));
                        }
                    }
                    emp.NetSalary = emp.GrossPay - emp.Deduction;

                    HttpContext.Session.SetString("Name", emp.Name);
                    HttpContext.Session.SetString("Deduction", emp.Deduction.ToString());
                    HttpContext.Session.SetString("NetSalary", emp.NetSalary.ToString());
                    return RedirectToAction("EmpSalaryInfo", "Employee");
                }
                else
                {
                    return View();
                }

            }
            else
            {
                return View();
            }
        }

        public IActionResult EmpSalaryInfo()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Deduction = HttpContext.Session.GetString("Deduction");
            ViewBag.NetSalary = HttpContext.Session.GetString("NetSalary");

            return View();
        }
    }
}