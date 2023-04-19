using BankingSystemMVC.Models;
using BankingSystemMVC.RoleManagement;
using BankingSystemMVC.UnitOfWork;
using BankingSystemMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace BankingSystemMVC.Controllers
{

    public class MeetingController : Controller
    {
        private UnitOfWorkBank unitOfWork = new UnitOfWorkBank();
        public IActionResult Meetings()
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {
                var meetings = unitOfWork.MeetingRepository.GetAll();
                return View(meetings);

            }
            else if(PermissionChecker.hasPermission(HttpContext.Session, "employee"))
            {
                var sessionUsername = HttpContext.Session.GetString("Username");
                var appAccount = unitOfWork.UserAccountRepository.Get((userAccount) => userAccount.Username == sessionUsername).FirstOrDefault();
                var meetings = unitOfWork.MeetingRepository.Get(((meeting) => meeting.EmployeeId == appAccount.EmployeeId));
                return View(meetings);

            }
            else if(PermissionChecker.hasPermission(HttpContext.Session, "customer")) 
            {
                var sessionUsername = HttpContext.Session.GetString("Username");
                var appAccount = unitOfWork.UserAccountRepository.Get((userAccount) => userAccount.Username == sessionUsername).FirstOrDefault();
                var meetings = unitOfWork.MeetingRepository.Get(((meeting) => meeting.CustomerId == appAccount.CustomerId));
                return View(meetings);

            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
                
            }

        public PartialViewResult MeetingInfo(int id)
        {
            var meetings = unitOfWork.MeetingRepository.GetByID(id);

            return PartialView(meetings);
        }

        public IActionResult CreateMeeting()
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {
                CreateMeetingViewModel model = new CreateMeetingViewModel
                {
                    Customers = unitOfWork.CustomerRepository.GetAll().ToList(),
                    Employees = unitOfWork.EmployeeRepository.GetAll().ToList()
                };

                return View(model);
            }
            else if(PermissionChecker.hasPermission(HttpContext.Session, "employee")) 
            {
                var sessionUsername = HttpContext.Session.GetString("Username");
                var appAccount = unitOfWork.UserAccountRepository.Get((userAccount) => userAccount.Username == sessionUsername).FirstOrDefault();
                var user = unitOfWork.EmployeeRepository.Get((employee) => employee.Id == appAccount.EmployeeId).ToList();
                CreateMeetingViewModel model = new CreateMeetingViewModel
                {
                    Customers = unitOfWork.CustomerRepository.GetAll().ToList(),
                    Employees = user
                };
                return View(model);

            }
            else if (PermissionChecker.hasPermission(HttpContext.Session, "customer")) 
            {
                var sessionUsername = HttpContext.Session.GetString("Username");
                var appAccount = unitOfWork.UserAccountRepository.Get((userAccount) => userAccount.Username == sessionUsername).FirstOrDefault();
                var user = unitOfWork.CustomerRepository.Get((customer) => customer.Id == appAccount.CustomerId).ToList();
                CreateMeetingViewModel model = new CreateMeetingViewModel
                {
                    Customers = user,
                    Employees = unitOfWork.EmployeeRepository.GetAll().ToList()
                };
                return View(model);

            }
            else 
            {
                return RedirectToAction("NoPermission","Home");
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMeeting(CreateMeetingViewModel model)
        {
            if (ModelState.IsValid)
            {
                Meeting meeting = new Meeting
                {
                    EmployeeId = model.Meeting.EmployeeId,
                    CustomerId = model.Meeting.CustomerId,
                    ShortDescription = model.Meeting.ShortDescription,
                    Text = model.Meeting.Text,
                    RequestCreatedDate = DateTime.Today,
                    MeetingDate = model.Meeting.MeetingDate
                };
                unitOfWork.MeetingRepository.Insert(meeting);
                unitOfWork.Save();
                return RedirectToAction("CreateMeeting");
            }
            else 
            {
                return RedirectToAction("", "Home");
            }

              
        }


        public IActionResult UpdateMeetings()
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {
                var employees = unitOfWork.MeetingRepository.GetAll().ToList();
                return View(employees);
            }
            else if (PermissionChecker.hasPermission(HttpContext.Session, "employee")) 
            {
                var sessionUsername = HttpContext.Session.GetString("Username");
                var appAccount = unitOfWork.UserAccountRepository.Get((userAccount) => userAccount.Username == sessionUsername).FirstOrDefault();
                var user = unitOfWork.EmployeeRepository.Get((employee) => employee.Id == appAccount.EmployeeId).ToList();
                return View(user);
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
        }
        public IActionResult UpdateMeeting(int id)
        {
            var meetings = unitOfWork.MeetingRepository.GetByID(id);
            var employees = unitOfWork.EmployeeRepository.GetAll().ToList();
            var customers = unitOfWork.CustomerRepository.GetAll().ToList();

            UpdateMeetingViewModel model = new UpdateMeetingViewModel()
            {
                meeting = meetings,
                employees = employees,
                customers = customers
            };    

            return PartialView(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateMeeting(UpdateMeetingViewModel model)
        {

            var meeting = model.meeting;
            if (ModelState.IsValid)
            {
                unitOfWork.MeetingRepository.Update(meeting);
                unitOfWork.Save();
                return RedirectToAction("UpdateMeetings");
            }
            else
            {
                return RedirectToAction("","Home");
            }

        }

        [HttpPost]
        public IActionResult DeleteMeeting(int id)
        {
            unitOfWork.MeetingRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Meetings");
        }



    }
}
