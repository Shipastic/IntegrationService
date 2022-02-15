using Diadoc.Api.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Interface
{
    interface IAllDepartments
    {
        Department GetDepartments(string Tag);

        string GetDepId(Department department);

        List<Diadoc.Api.Proto.Departments.Department> GetDepartmentsIris();

        string GetAddressIris(Diadoc.Api.Proto.Departments.Department department);
    }
}
