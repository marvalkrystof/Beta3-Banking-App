using BankingSystemMVC.Models;
using BankingSystemMVC.RoleManagement;
using BankingSystemMVC.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemMVC.Controllers
{
    /// <summary>
    /// Serves requests for queries about customer and employee entitites
    /// </summary>
    public class UserController : Controller
    {
        private UnitOfWorkBank unitOfWork = new UnitOfWorkBank();


        /// <summary>
        /// Returns list of employees for the user
        /// </summary>
        /// <returns>View</returns>
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

        /// <summary>
        /// Shows info about specific employee
        /// </summary>
        /// <param name="id">Id of the employee</param>
        /// <returns>View</returns>
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

        /// <summary>
        /// Form for the creation of the employees
        /// </summary>
        /// <returns>View</returns>
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

        /// <summary>
        /// Handles the post request for the employee creation and updates it in the DB. 
        /// </summary>
        /// <param name="employee">Employee data</param>
        /// <returns>View</returns>
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
        /// <summary>
        /// Returns list of the employees to update
        /// </summary>
        /// <returns>View</returns>
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

        /// <summary>
        /// Returns form for the updation of the specific employee
        /// </summary>
        /// <param name="id">Id of the employee</param>
        /// <returns>View</returns>
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
        /// <summary>
        /// Handles the post request of the employee updation
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns>Redirect</returns>
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
        /// <summary>
        /// Handles the post request for the employee deletion
        /// </summary>
        /// <param name="id">Id of the employee</param>
        /// <returns>Redirect</returns>
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


    
        /// <summary>
        /// Returns list of the customers for the user
        /// </summary>
        /// <returns>View</returns>
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
        /// <summary>
        /// Information about specific customer
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <returns>View</returns>
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
        /// <summary>
        /// Form for creation of the customer
        /// </summary>
        /// <returns>View</returns>
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

        /// <summary>
        /// Handles post request for creation of the customer
        /// </summary>
        /// <param name="customer">Customer object</param>
        /// <returns>Redirect</returns>
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

        /// <summary>
        /// Returns list of customers to update
        /// </summary>
        /// <returns>View</returns>
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
        /// <summary>
        /// Form for updation of the specific customer
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <returns>View</returns>
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
        /// <summary>
        /// Handles the post request of the customer update and updates it in the db
        /// </summary>
        /// <param name="customer">Customer object</param>
        /// <returns>Redirect</returns>
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
        /// <summary>
        /// Handles the deletion of the customer, and deletes it from the DB.
        /// </summary>
        /// <param name="id">Id of the specific customer</param>
        /// <returns>Redirect</returns>
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
