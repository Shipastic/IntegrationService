using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Employees;
using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Interface
{
    public interface IAllUserIrisList
    {
        /// <summary>
        /// Метод получения списка сотрудников
        /// </summary>
        /// <returns></returns>
        List<UserIris> UserIrises(List<Employee> empl, List<Diadoc.Api.Proto.Departments.Department> departments);

        /// <summary>
        /// Метод для получения сотрудника по емайлу из бд
        /// </summary>
        /// <param name="email"> емайл </param>
        /// <returns></returns>
        UserV2 GetEmployeeIrises(List<Employee> empl, string email);

        List<Employee> GetuserIris(List<Employee> empl);

        string GetId(ParcedDocument parcedDocument, List<Employee> employees);

        /// <summary>
        /// Метод получения Тега подразделения для сотрудника
        /// </summary>
        /// <returns></returns>
        string GetUserIrisesTag(Employee emp, List<Employee> employee, List<Diadoc.Api.Proto.Departments.Department> departments);

        /// <summary>
        /// Метод получения userId по выбранному сотруднику
        /// </summary>
        /// <param name="userV2"> сотрудник </param>
        /// <returns></returns>
        string GetuserId(UserV2 v2);
    }
}
