using Diadoc.Api.Proto.Employees;
using EDMIrisRetail.Controller;
using EDMIrisRetail.Interface;
using EDMIrisRetail.Model;
using EDMIrisRetail.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
   public class WorkWithDoc
    {
        /// <summary>
        /// Счетчик сущностей в сообщении
        /// </summary>
        int maxLabel = 1;

        /// <summary>
        /// Счетчик поставщиков
        /// </summary>
        int step = 1;

        /// <summary>
        /// Строка для комментария
        /// </summary>
        string msg = "";

        /// <summary>
        /// Строка для комментария
        /// </summary>
        string msg2 = "";

        string messageId = null;

        string documentId = null;

        /// <summary>
        /// Название документа у поставщика
        /// </summary>
        string DocumentNameContr = "";

        public int CountContractor { get; set; }
        public int CountUserIris { get; set; }
        public int CountDepartmentIris { get; set; }
        public string ListContragent { get; set; }
        public string DocforWork { get; set; }

        public string DocumentName { get; set; }

        public string ResolutionStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
}
