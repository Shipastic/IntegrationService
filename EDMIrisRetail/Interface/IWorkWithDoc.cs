using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Employees;
using EDMIrisRetail.Controller;
using EDMIrisRetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Interface
{
    interface IWorkWithDoc
    {
        List<WorkWithDoc> GetConnect();

        List<WorkWithDoc>  GetListContractors(List<Contractor> contractors, List<Employee> LisiEmp, List<Diadoc.Api.Proto.Departments.Department> ListDep);

        WorkWithDoc SetCommentForDocWithParamFilter(Contractor contr, DocumentList ListDoc, WorkWithDoc workWithDoc);
    }
}
