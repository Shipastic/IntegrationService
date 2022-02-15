using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Employees;
using EDMIrisRetail.Interface;
using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Controller
{
    public class UserIrisController : IAllUserIrisList
    {

        List<UserIris> irises = new List<UserIris>();

        public string GetCurrTag { get; set; }

        public List<UserIris> UserIrises (List<Employee> empl, List<Diadoc.Api.Proto.Departments.Department> departments)
        {
            for (var page = 1; ; page++)
            {
                var empList = EDMClass.apiDiadoc.GetEmployees(EDMClass.authTokenByLogin, Constants.DefaultFromBoxId, page, count: 20);

                empl.AddRange(empList.Employees);

                if (empList.Employees.Count == 0 || empl.Count >= empList.TotalCount)
                {
                    break;
                }
            }

            foreach (Employee item in empl)
            {
                UserIris iris = new UserIris
                {
                    DepartmentTagIris = GetUserIrisesTag(item, empl, departments),

                    FirstNameIris = item.User.FullName?.FirstName ?? "Нет данных",

                    LastNameIris = item.User.FullName?.LastName ?? "Нет данных",

                    MiddleNameIris = item.User.FullName?.MiddleName ?? "Нет данных",

                    LoginIris = item.User?.Login ?? "Нет данных",

                    PositionIris = item?.Position ?? "Нет данных"

                };

                irises.Add(iris);
            }
            return irises;
        }

        public UserV2 GetEmployeeIrises(List<Employee> empl, string email)
        {
            UserV2 emp = empl.Where(u => u.User.Login.ToLower() == email.ToLower())
                                                .Select(u => u.User)
                                                .FirstOrDefault();
            return emp;
        }

        /// <summary>
        /// Метод для получения ИД пользователя по статусу и ру из бд
        /// </summary>
        /// <param name="parcedDocument"></param>
        /// <param name="employees"></param>
        /// <returns></returns>
        public string GetId(ParcedDocument parcedDocument, List<Employee> employees)
        {
            string userId;

            switch (parcedDocument.status)
            {
                case 1 when parcedDocument.isArrove == 0:
                    {
                        userId = employees.Where(o => o.User.Login.ToLower() == parcedDocument.email.ToLower()).Select(o => o.User.UserId).FirstOrDefault();
                        break;
                    }

                case 6 when parcedDocument.isArrove == 1:
                    {
                        userId = employees.Where(o => o.User.Login.ToLower() == "informer-iris@iris-retail.ru").Select(o => o.User.UserId).FirstOrDefault();
                        break;
                    }

                default:
                    {
                        userId = employees.Where(o => o.User.Login.ToLower() == parcedDocument.email.ToLower()).Select(o => o.User.UserId).FirstOrDefault();
                        break;
                    }
            }

            return userId;
        }

        public string GetuserId(UserV2 v2)
        {
            var usID = v2.UserId;

            return usID;
        }

        public List<Employee> GetuserIris(List<Employee> empl)
        {
            for (var page = 1; ; page++)
            {
                var empList = EDMClass.apiDiadoc.GetEmployees(EDMClass.authTokenByLogin, Constants.DefaultFromBoxId, page, count: 20);

                empl.AddRange(empList.Employees);

                if (empList.Employees.Count == 0 || empl.Count >= empList.TotalCount)
                {
                    break;
                }
            }
            return empl;
        }

        public string GetUserIrisesTag(Employee emp, List<Employee> employee, List<Diadoc.Api.Proto.Departments.Department> departments)
        {
            var depTag = from d in departments
                         join e in employee on
                         d.Id equals e.Permissions.UserDepartmentId
                         where e.User.UserId.Equals(emp.User.UserId)
                         select d.Abbreviation;

            var departTagAll = depTag.FirstOrDefault();

            GetCurrTag = departTagAll;

            return departTagAll;
        }
    }
}
