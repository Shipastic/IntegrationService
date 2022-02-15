using EDMIrisRetail.Interface;
using EDMIrisRetail.Model;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diadoc.Api.Proto;

namespace EDMIrisRetail.Controller
{
    public class ContractorController: IAllContractors
    {
        public List<Contractor> GetContractors()
        {
            OracleConnection oracleConnect = OracleConnectionState.GetInstance();

           // OracleConnection connection = new OracleConnection();

            DataSet setContractor = new DataSet();

            OracleCommand cmdGetContractor = oracleConnect.CreateCommand();

            cmdGetContractor.CommandText = @"  SELECT c.id                                             ID,
                                                                   c.short_name                                     SHORT_NAME,
                                                                   c.inn                                            INN,
                                                                   get_ent_param_value_fast(c.ent_id, 'КПП')        KPP,
                                                                   get_ent_param_value_fast(c.ent_id, 'BOX_ID')     BoxID,
                                                                   nvl((select max(last_index) FROM iris_diadoc_doc dd WHERE dd.cnt_id=c.id),0) IndexKey,
                                                                   get_ent_param_value_fast(c.ent_id, 'DIADOC_START') dateStart
                                                            FROM contractor c
                                                            WHERE get_ent_param_value_fast(c.ent_id, 'ДИАДОК') = 1
                                                            ORDER BY 2";

            using (cmdGetContractor)
            {
                cmdGetContractor.Connection = oracleConnect;

                cmdGetContractor.BindByName = true;

                cmdGetContractor.ExecuteNonQuery();

                List<Contractor> contractors = new List<Contractor>();

                using (OracleDataAdapter da = new OracleDataAdapter(cmdGetContractor))
                {
                    da.Fill(setContractor);

                    foreach (DataRow dataRow in setContractor.Tables[0].Rows)
                    {
                        Contractor contractor = new Contractor
                        {
                            BoxId = dataRow["BoxID"].ToString(),
                            Id = long.Parse(dataRow["ID"].ToString()),
                            INN = dataRow["INN"].ToString(),
                            KPP = dataRow["KPP"].ToString(),
                            Name = dataRow["SHORT_NAME"].ToString(),
                            LastIndexKey = dataRow["IndexKey"].ToString(),
                            dateStart = DateTime.ParseExact(dataRow["dateStart"].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture)
                        };
                        contractors.Add(contractor);
                    }

                    return contractors;
                }
            }
        }

        /// <summary>
        /// Метод для получения всех контрагентов
        /// </summary>
        /// <returns></returns>
        public List<Counteragent> GetCounteragentLists()
        {
            CounteragentList counteragent = new CounteragentList();

            CounteragentList counteragentFirst = new CounteragentList();

            var ListContr = new List<Counteragent>();

            var CurOrgId = EDMClass.apiDiadoc.GetOrganizationByBoxId(Constants.DefaultFromBoxId);

            var orgId = CurOrgId.OrgId;


            counteragentFirst = EDMClass.apiDiadoc.GetCounteragents(EDMClass.authTokenByLogin, orgId, "Active", null);

            ListContr.AddRange(counteragentFirst.Counteragents.OrderBy(n => n.IndexKey));

            string lastIndexKey = counteragentFirst.Counteragents.Select(i => i.IndexKey).LastOrDefault();

            int countIndex = Int32.Parse(lastIndexKey);

            for (var page = 1; ; page++)
            {
                
                counteragent = EDMClass.apiDiadoc.GetCounteragents(EDMClass.authTokenByLogin, orgId, "Active", $"{countIndex}", null, pageSize:100);

                ListContr.AddRange(counteragent.Counteragents.OrderBy(n => n.IndexKey));

                if (counteragent.Counteragents.Count == 0 || ListContr.Count >= counteragentFirst.TotalCount)
                {
                    break;
                }
                countIndex += 100;
            }

            return ListContr;
        }
    }
}
