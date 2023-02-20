using BankingSystemMVC.Models;
using BankingSystemMVC.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemMVC.Controllers
{
    public class UserController : Controller
    {
        private UnitOfWorkBank unitOfWork = new UnitOfWorkBank();


        public IActionResult ShowEmployees()
        {
            var employees = unitOfWork.EmployeeRepository.GetAll().ToList();

            return View(employees);
        }

        public IActionResult ShowEmployeeInfo(int id)
        {
            var employee = unitOfWork.EmployeeRepository.GetByID(id);

            return PartialView(employee);
        }

        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.EmployeeRepository.Insert(employee);
                unitOfWork.Save();
                return RedirectToAction("CreateEmployee");
            }
            else
            {
                return RedirectToAction("/Home/");
            }
        }

        public IActionResult UpdateEmployees()
        {

            var employees = unitOfWork.EmployeeRepository.GetAll().ToList();

            return View(employees);
        }

        public IActionResult UpdateEmployee(int id)
        {
            var employee = unitOfWork.EmployeeRepository.GetByID(id);

            return PartialView(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateEmployee(Employee employee)
        {

            if (ModelState.IsValid)
            {
                unitOfWork.EmployeeRepository.Update(employee);
                unitOfWork.Save();
                return RedirectToAction("UpdateEmployees");
            }
            else
            {
                return RedirectToAction("/Home/");
            }
              
        }

        [HttpPost]
        public IActionResult DeleteEmployee(int id)
        {
            unitOfWork.EmployeeRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("ShowEmployees");
        }


    

    public IActionResult ShowCustomers()
    {
        var customers = unitOfWork.CustomerRepository.GetAll().ToList();

        return View(customers);
    }

    public IActionResult ShowCustomerInfo(int id)
    {
        var customer = unitOfWork.CustomerRepository.GetByID(id);

        return PartialView(customer);
    }

    public IActionResult CreateCustomer()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateCustomer(Customer customer)
    {
        if (ModelState.IsValid)
        {
            unitOfWork.CustomerRepository.Insert(customer);
            unitOfWork.Save();
            return RedirectToAction("CreateCustomer");
        }
        else
        {
            return RedirectToAction("/Home/");
        }
    }

    public IActionResult UpdateCustomers()
    {

        var customers = unitOfWork.CustomerRepository.GetAll().ToList();

        return View(customers);
    }

    public IActionResult UpdateCustomer(int id)
    {
        var customer = unitOfWork.CustomerRepository.GetByID(id);

        return PartialView(customer);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateCustomer(Customer customer)
    {

        if (ModelState.IsValid)
        {
            unitOfWork.CustomerRepository.Update(customer);
            unitOfWork.Save();
            return RedirectToAction("UpdateCustomers");
        }
        else
        {
            return RedirectToAction("/Home/");
        }

    }

    [HttpPost]
    public IActionResult DeleteCustomer(int id)
    {
        unitOfWork.CustomerRepository.Delete(id);
        unitOfWork.Save();
        return RedirectToAction("ShowCustomers");
    }


}


    
}
