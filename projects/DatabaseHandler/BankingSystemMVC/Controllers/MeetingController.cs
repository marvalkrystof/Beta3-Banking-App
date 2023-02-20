using BankingSystemMVC.Models;
using BankingSystemMVC.UnitOfWork;
using BankingSystemMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemMVC.Controllers
{

    public class MeetingController : Controller
    {
        private UnitOfWorkBank unitOfWork = new UnitOfWorkBank();
        public IActionResult Meetings()
        {

            var meetings = unitOfWork.MeetingRepository.GetAll();
            return View(meetings);
        }

        public PartialViewResult MeetingInfo(int id)
        {
            var meetings = unitOfWork.MeetingRepository.GetByID(id);

            return PartialView(meetings);
        }

        public IActionResult CreateMeeting()
        {
            CreateMeetingViewModel model = new CreateMeetingViewModel
            {
                Customers = unitOfWork.CustomerRepository.GetAll().ToList(),
                Employees = unitOfWork.EmployeeRepository.GetAll().ToList()
            };

            return View(model);
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
                return RedirectToAction("/Home/");
            }

              
        }

    }
}
