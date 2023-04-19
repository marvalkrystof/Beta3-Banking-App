using BankingSystemMVC.Models;
using BankingSystemMVC.RoleManagement;
using BankingSystemMVC.UnitOfWork;
using BankingSystemMVC.ViewModels;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Mvc;
using SharpHash.Base;
using SharpHash.Interfaces;
using System.Data;
using System.Text;
using System.Text.Json;

namespace BankingSystemMVC.Controllers
{
    public class AdminController : Controller
    {
        private UnitOfWorkBank uow = new UnitOfWorkBank();

        public IActionResult RoleManager()
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {
                List<UserAccount> userAccounts = uow.UserAccountRepository.GetAll().ToList();
                return View(userAccounts);
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }

        }

        public PartialViewResult UserRoles(int id)
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {
                List<AccountRole> accountRoleList = uow.AccountRoleRepository.Get((accountRole) => accountRole.UserAccountId == id).ToList();
                List<Role> unassignedRoles = uow.RoleRepository.GetAll().ToList();
                List<Role> assignedRoles = new List<Role>();
                foreach (var accountRole in accountRoleList)
                {
                    foreach (var role in unassignedRoles.ToList())
                    {
                        if (accountRole.RoleId == role.Id)
                        {
                            assignedRoles.Add(role);
                            unassignedRoles.Remove(role);
                        }
                    }
                }
                RoleManagementViewModel model = new RoleManagementViewModel
                {
                    userId = id,
                    assignedRoles = assignedRoles,
                    unassignedRoles = unassignedRoles
                };
                return PartialView(model);
            }
            else
            {
                return PartialView("NoPermission", "Home");
            }

        }

        [HttpPost]
        public IActionResult UserRoles(string roles)
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {

                RoleAssignmentModel model = JsonSerializer.Deserialize<RoleAssignmentModel>(roles);

                int userId = -1;
                int.TryParse(model.userId, out userId);
                if (userId == -1)
                {
                    Logger.Logger.LogCriticalFailure("int cannot be parsed");
                }


                UserAccount userAccount = uow.UserAccountRepository.GetByID(userId);


                foreach (var role in model.valuesAdded)
                {
                    int roleId = -1;
                    int.TryParse(role, out roleId);
                    if (roleId == -1)
                    {
                        Logger.Logger.LogCriticalFailure("int cannot be parsed");
                    }
                    AccountRole newRole = new AccountRole
                    {
                        RoleId = roleId,
                        UserAccountId = userAccount.Id,
                    };

                    uow.AccountRoleRepository.Insert(newRole);
                    Logger.Logger.Log("User role" + role + " assigned to user" + userAccount.Username);
                }
                foreach (var role in model.valuesRemoved)
                {
                    int roleId = -1;
                    int.TryParse(role, out roleId);
                    if (roleId == -1)
                    {
                        Logger.Logger.LogCriticalFailure("int cannot be parsed");
                    }
                    AccountRole arToDelete = uow.AccountRoleRepository.Get((accountRole) => accountRole.RoleId == roleId && accountRole.UserAccountId == userAccount.Id).FirstOrDefault();
                    uow.AccountRoleRepository.Delete(arToDelete);
                    Logger.Logger.Log("User role" + role + " unassigned from user" + userAccount.Username);
                }

                uow.Save();
                return RedirectToAction("Index", "Home");
            } 
            else 
            {
                return RedirectToAction("NoPermission", "Home");
            }
        }

        public IActionResult CreateUserAccount()
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {
                CreateUserAccountAppViewModel model = new CreateUserAccountAppViewModel
                {
                    Customers = uow.CustomerRepository.GetAll().ToList(),
                    Employees = uow.EmployeeRepository.GetAll().ToList()
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUserAccount(CreateUserAccountAppViewModel accountModel)
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {
                if (ModelState.IsValid)
                {
                    UserAccount account = accountModel.UserAccount;
                    if (account.EmployeeId == -1)
                    {
                        account.EmployeeId = null;
                    }
                    if (account.CustomerId == -1)
                    {
                        account.CustomerId = null;
                    }
                    if (account.EmployeeId == -1 && account.CustomerId == -1)
                    {
                        return RedirectToAction("", "Home");
                    }

                    IHash hash = HashFactory.Crypto.CreateMD5();
                    hash.Initialize();
                    hash.TransformString(account.Password, Encoding.UTF8);
                    string hashedPass = hash.TransformFinal().ToString();
                    account.Password = hashedPass;

                    uow.UserAccountRepository.Insert(account);
                    uow.Save();
                    return RedirectToAction("RoleManager", "Admin");
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
        public IActionResult UpdateUserAccounts()
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {

                var userAccounts = uow.UserAccountRepository.GetAll().ToList();
                return View(userAccounts);
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }

        }

        public IActionResult UpdateUserAccount(int id)
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {

                var userAccount = uow.UserAccountRepository.GetByID(id);
                

                UpdateUserAppAccountViewModel model = new UpdateUserAppAccountViewModel
                {
                    account = userAccount,
                    employees = uow.EmployeeRepository.GetAll().ToList(),
                    customers = uow.CustomerRepository.GetAll().ToList()
                };

                return PartialView(model);
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUserAccount(UpdateUserAppAccountViewModel userAccount)
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {

                if (ModelState.IsValid)
                {
                 
                    
                    var accountFromForm = userAccount.account;

                    if (accountFromForm.EmployeeId == -1)
                    {
                        accountFromForm.EmployeeId = null;
                    }
                    if (accountFromForm.CustomerId == -1)
                    {
                        accountFromForm.CustomerId = null;
                    }
                    if (accountFromForm.EmployeeId == -1 && accountFromForm.CustomerId == -1)
                    {
                        return RedirectToAction("", "Home");
                    }

                    IHash hash = HashFactory.Crypto.CreateMD5();
                    hash.Initialize();
                    hash.TransformString(userAccount.account.Password, Encoding.UTF8);
                    string hashedPass = hash.TransformFinal().ToString();

                    accountFromForm.Password = hashedPass;
                    uow.UserAccountRepository.Update(accountFromForm);
                    uow.Save();
                    return RedirectToAction("UpdateUserAccounts");
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
        public IActionResult DeleteUserAccount(int id)
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "admin"))
            {
                List<AccountRole> relations = uow.AccountRoleRepository.Get((accountRole) => accountRole.UserAccount.Id == id).ToList();
                foreach (var relation in relations)
                {
                    uow.AccountRoleRepository.Delete(relation);
                }
                uow.Save();
                uow.UserAccountRepository.Delete(id);
                uow.Save();
                return RedirectToAction("UpdateUserAccounts");
            }
            else
            {
                return RedirectToAction("NoPermission", "Home");
            }
        }

    }
}

