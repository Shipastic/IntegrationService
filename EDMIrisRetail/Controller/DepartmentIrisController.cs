using Diadoc.Api.Proto;
using EDMIrisRetail.Interface;
using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Controller
{
    public class DepartmentIrisController : IAllDepartments
    {

        List<DepartmentIris> departmentIrises = new List<DepartmentIris>();
        public string GetAddressIris(Diadoc.Api.Proto.Departments.Department department)
        {
            string currAddress = $"{department.Address?.RussianAddress.City ?? "Нет данных"}, {department.Address?.RussianAddress.Street ?? "Нет данных"}, {department.Address?.RussianAddress.Building ?? "Нет данных"}, {department.Address?.RussianAddress.Region ?? "Нет данных"}";
            return currAddress;
        }

        /// <summary>
        /// Метод получения департамента по тегу
        /// </summary>
        /// <param name="Tag"></param>
        /// <returns></returns>
        public Department GetDepartments(string Tag)
        {
            List<Department> departments = new List<Department>();

            Department department = departments.Where(d => d.Abbreviation == Tag)
                                                      .FirstOrDefault();
            return department;
        }

        /// <summary>
        /// Метод получения списка подразделений
        /// </summary>
        /// <returns></returns>
        public List<Diadoc.Api.Proto.Departments.Department> GetDepartmentsIris()
        {
            var departments = new List<Diadoc.Api.Proto.Departments.Department>();

            for (var page = 1; ; page++)
            {
                var depList = EDMClass.apiDiadoc.GetDepartments(EDMClass.authTokenByLogin, Constants.DefaultFromBoxId, page, count: 10);

                departments.AddRange(depList.Departments);

                if (depList.Departments.Count == 0 || departments.Count >= depList.TotalCount)
                {
                    break;
                }
            }
            return departments;
        }

        /// <summary>
        /// Метод для получения ИД подразделения
        /// </summary>
        /// <param name="department"> Подразделение </param>
        /// <returns></returns>
        public string GetDepId(Department department)
        {
            string depId = department.DepartmentId;

            return depId;
        }

        public List<DepartmentIris> GetDeptsIris()
        {
            var departments = new List<Diadoc.Api.Proto.Departments.Department>();

            for (var page = 1; ; page++)
            {
                var depList = EDMClass.apiDiadoc.GetDepartments(EDMClass.authTokenByLogin, Constants.DefaultFromBoxId, page, count: 10);

                departments.AddRange(depList.Departments);

                if (depList.Departments.Count == 0 || departments.Count >= depList.TotalCount)
                {
                    break;
                }
            }

            foreach (var item in departments)
            {
                DepartmentIris departmentIris = new DepartmentIris
                {
                    AddressIris = GetAddressIris(item),

                    AbbreviationIris = item?.Abbreviation ?? "Нет данных",

                    KppIris = item?.Kpp ?? "Нет данных",

                    ParentDepartmentIdIris = item?.ParentDepartmentId ?? "Нет данных",

                    DepartNameIris = item?.Name ?? "Нет данных"

                };

                departmentIrises.Add(departmentIris);
            }
            return departmentIrises;
        }
    }
}
