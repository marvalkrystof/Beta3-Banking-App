using BankingSystemMVC.Models;
using BankingSystemMVC.RoleManagement;
using BankingSystemMVC.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemMVC.Controllers
{
    public class UserController : Controller
    {
        private UnitOfWorkBank unitOfWork = new UnitOfWorkBank();


        public IActionResult ShowEmployees()
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {

                var employees = unitOfWork.EmployeeRepository.GetAll().ToList();

                return View(employees);
            }
            else 
            {
                return RedirectToAction("NoPermission","Home");
            }
        }

        public IActionResult ShowEmployeeInfo(int id)
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {

                var employee = unitOfWork.EmployeeRepository.GetByID(id);

                return PartialView(employee);
            }
            else 
            {
                return RedirectToAction("NoPermission", "Home");
            }
        }


        public IActionResult CreateEmployee()
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {

                return View();
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmployee(Employee employee)
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {
              if (ModelState.IsValid)
            {
                unitOfWork.EmployeeRepository.Insert(employee);
                unitOfWork.Save();
                return RedirectToAction("CreateEmployee");
            }
            else
            {
                return RedirectToAction("", "Home");
            }
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
            
        }

        public IActionResult UpdateEmployees()
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {
            var employees = unitOfWork.EmployeeRepository.GetAll().ToList();

            return View(employees);
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
            
        }

        public IActionResult UpdateEmployee(int id)
        {

            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {
                var employee = unitOfWork.EmployeeRepository.GetByID(id);
                return PartialView(employee);
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateEmployee(Employee employee)
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {
            if (ModelState.IsValid)
            {
                unitOfWork.EmployeeRepository.Update(employee);
                unitOfWork.Save();
                return RedirectToAction("UpdateEmployees");
            }
            else
            {
                return RedirectToAction("","Home");
            }
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
       
              
        }

        [HttpPost]
        public IActionResult DeleteEmployee(int id)
        {

            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {

            unitOfWork.EmployeeRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("ShowEmployees");
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }


        }


    

    public IActionResult ShowCustomers()
    {

            if (PermissionChecker.hasPermission(HttpContext.Session, "employee"))
            {
            var customers = unitOfWork.CustomerRepository.GetAll().ToList();

            return View(customers);
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
            
    }

    public IActionResult ShowCustomerInfo(int id)
    {
            if (PermissionChecker.hasPermission(HttpContext.Session, "employee"))
            {
            var customer = unitOfWork.CustomerRepository.GetByID(id);

             return PartialView(customer);
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }

            
    }

    public IActionResult CreateCustomer()
    {
            if (PermissionChecker.hasPermission(HttpContext.Session, "employee"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }

           
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateCustomer(Customer customer)
    {
            if (PermissionChecker.hasPermission(HttpContext.Session, "employee"))
            {
                if (ModelState.IsValid)
                    {
                        unitOfWork.CustomerRepository.Insert(customer);
                        unitOfWork.Save();
                        return RedirectToAction("CreateCustomer");
                    }
                    else
                    {
                            return RedirectToAction("", "Home");
                        }
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
        
    }

    public IActionResult UpdateCustomers()
    {
            if (PermissionChecker.hasPermission(HttpContext.Session, "employee"))
            {
             var customers = unitOfWork.CustomerRepository.GetAll().ToList();

                    return View(customers);
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
           
    }

    public IActionResult UpdateCustomer(int id)
    {
            if (PermissionChecker.hasPermission(HttpContext.Session, "employee"))
            {
            var customer = unitOfWork.CustomerRepository.GetByID(id);

                    return PartialView(customer);
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }

            
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateCustomer(Customer customer)
    {
            if (PermissionChecker.hasPermission(HttpContext.Session, "employee"))
            {
                 if (ModelState.IsValid)
                    {
                        unitOfWork.CustomerRepository.Update(customer);
                        unitOfWork.Save();
                        return RedirectToAction("UpdateCustomers");
                    }
                    else
                    {
                            return RedirectToAction("", "Home");
                        }
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
       

    }

    [HttpPost]
    public IActionResult DeleteCustomer(int id)
    {

            if (PermissionChecker.hasPermission(HttpContext.Session, "employee"))
            {
                    unitOfWork.CustomerRepository.Delete(id);
                    unitOfWork.Save();
                    return RedirectToAction("ShowCustomers");
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
            
    }



}


    
}
