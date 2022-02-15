using Diadoc.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail.Model
{
    public class DocumentsFilterIris : DocumentsFilter
    {
        public string BoxIdIris { get; set; }

        public string CounteragentBoxIdIris { get; set; }

        public string FilterCategoryIris { get; set; }

        public DateTime TimestampFromIris { get; set; }

        public string contractorNameIris { get; set; }

        public Contractor contractor;

        // private static DiadocApi diadocApi;

        /// <summary>
        /// Метод для выставления диапазона даты для загрузки документов
        /// </summary>
        /// <param name="contractor"> поставщик </param>
        /// <returns></returns>

        public DocumentsFilterIris()
        {

        }

        public DocumentsFilterIris(string _boxId, string _contreagentboxId, string _categoryFilter, DateTime _date, Contractor _contractor)
        {
            BoxId = _boxId;

            CounteragentBoxId = _contreagentboxId;

            FilterCategory = _categoryFilter;

            TimestampFrom = _date;

            contractor = _contractor;
        }

        public DocumentsFilterIris(string _boxId, string _contreagentboxId, string _categoryFilter, DateTime _date)
        {
            BoxId = _boxId;

            CounteragentBoxId = _contreagentboxId;

            FilterCategory = _categoryFilter;

            TimestampFrom = _date;
        }
    }
}
