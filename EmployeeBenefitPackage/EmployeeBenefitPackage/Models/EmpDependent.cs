using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EmployeeBenefitPackage.Models
{
    public class EmpDependent
    {
        [Required(ErrorMessage = "Enter Employer Name !")]
        [Display(Name = "Employer Name:")]
        public string Name { get; set; }
        public double GrossPay { get; set; }

        [Required(ErrorMessage = "Select the Marital Status !")]
        [Display(Name = "Marital Status:")]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "Enter the No. of Children !")]
        [Display(Name = "Children:")]
        public string Children { get; set; }

        [Display(Name = "Total No. of Dependents whose Name Starts With 'A':")]
        [Required(ErrorMessage = "Enter the No. of Dependents whose Name Starts With 'A' !")]
        public string DependentNameStartsWithA { get; set; }
        public double Deduction { get; set; }

        public double NetSalary { get; set; }


    }
}