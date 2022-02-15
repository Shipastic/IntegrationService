using EDMIrisRetail.Model;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail
{
    public class OracleConnectionState
    {
        private static OracleConnection oracleConnection;
        protected OracleConnectionState()
        {

        }
        private static OracleConnectionState oracleConnectionState;

        public static OracleConnectionState OraInstance()
        {
            if (oracleConnectionState == null)
                oracleConnectionState = new OracleConnectionState();

            return oracleConnectionState;
        }

        public static OracleConnection GetInstance(string TnsName= "FNP")
        {

                if (oracleConnection == null)
                    oracleConnection = new OracleConnection(ConfigurationManager.ConnectionStrings[TnsName].ConnectionString);
                else
                    if (oracleConnection != null)
                {
                    if (oracleConnection.State == ConnectionState.Open)
                    {
                        return oracleConnection;
                    }
                }

                oracleConnection = new OracleConnection(ConfigurationManager.ConnectionStrings[TnsName].ConnectionString);

                if (oracleConnection.State == ConnectionState.Broken || oracleConnection.State == ConnectionState.Closed)
                {

                    oracleConnection.Close();
                    try
                    {
                        oracleConnection.Open();
                    }
                    catch (OracleException oraex)
                    {
                        if (oraex.Number != 0)
                        {
                            // logger.Error(oe, $"Номер ошибки - {oe.Number}\n Текст ошибки: {oe.Message}\n");
                            //  logger.Info("Выполняется попытка восстановить подключение...\n");
                            OracleConnection.ClearPool(oracleConnection);
                            oracleConnection.Close();
                            oracleConnection.Open();
                            // logger.Info("Подключение восстановлено\n");
                        }
                    }
                }
                return oracleConnection;
        }

        public static void SetLog(Int64 docId, string exception, string type)
        {
            OracleConnection connection = GetInstance();
            using (
               OracleCommand cmd =
                   new OracleCommand(
                       @"INSERT INTO iris_diadoc_log (diadoc_doc_id,log_date,log,short_log,log_type)
                         VALUES ( :doc_id,:log_date,:log,:short_log,2)"))
            {
                try
                {
                    cmd.Connection = connection;
                    cmd.BindByName = true;
                    cmd.Parameters.Add("doc_id", docId);
                    cmd.Parameters.Add("log_date", DateTime.Now);
                    cmd.Parameters.Add("log", exception);
                    cmd.Parameters.Add("short_log", type);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    //logger.Error(exp);
                }
            }
        }

        //Устанавливаем значение RuApprove=2
        public static void SetRuApproveStatus2(Contractor contractor, string messageId, string entityId)
        {
            OracleConnection connection = GetInstance();

            var cmdSetStatus2 = connection.CreateCommand();

            cmdSetStatus2.CommandText = $@"UPDATE iris_diadoc_doc  SET RU_APPROVE=2  WHERE cnt_id={contractor.Id} AND entity_id ='{entityId}' AND message_id = '{messageId}' AND RU_APPROVE=1";

            using (cmdSetStatus2)
            {
                try
                {
                    cmdSetStatus2.Connection = connection;
                    cmdSetStatus2.BindByName = true;
                    cmdSetStatus2.ExecuteNonQuery();
                }
                catch (OracleException e)
                {
                    //logger.Error(e, $"Номер ошибки - {e.Number}\n Текст ошибки: {e.Message}\n");
                   // logger.Info("Выполняется попытка восстановить подключение...\n");
                    OracleConnection.ClearPool(connection);
                    connection.Close();
                    connection.Open();
                    //logger.Info("Подключение восстановлено\n");
                }
                catch (Exception exp)
                {
                   // logger.Error(exp);
                }
            }
        }

        //Устанавливаем значение RuApprove=3
        public static void SetRuApproveStatus3(Contractor contractor, string messageId, string entityId)
        {
            OracleConnection connection = GetInstance();

            var cmdSetStatus3 = connection.CreateCommand();

            cmdSetStatus3.CommandText = $@"UPDATE iris_diadoc_doc  SET RU_APPROVE=3  WHERE cnt_id={contractor.Id} AND entity_id ='{entityId}' AND message_id = '{messageId}' AND RU_APPROVE=2";

            using (cmdSetStatus3)
            {
                try
                {
                    cmdSetStatus3.Connection = connection;
                    cmdSetStatus3.BindByName = true;
                    cmdSetStatus3.ExecuteNonQuery();
                }
                catch (OracleException e)
                {
                    //logger.Error(e, $"Номер ошибки - {e.Number}\n Текст ошибки: {e.Message}\n");
                   // logger.Info("Выполняется попытка восстановить подключение...\n");
                    OracleConnection.ClearPool(connection);
                    connection.Close();
                    connection.Open();
                   // logger.Info("Подключение восстановлено\n");
                }
                catch (Exception exp)
                {
                   // logger.Error(exp);
                }
            }
        }

        /// <summary>
        /// Метод записи печатных форм в бд
        /// </summary>
        /// <param name="byteFile">массив байт из документа</param>
        /// <param name="cntId">ИД контрагента </param>
        /// <param name="entityId">ИД сущности документа </param>
        /// <param name="title">Заголовок документа </param>
        public static void WritePrintFormToDB(byte[] byteFile, Int64 cntId, string entityId, string title)
        {
            OracleConnection connection = GetInstance();

            var cmdPrintForm = connection.CreateCommand();

            cmdPrintForm.CommandText = $@"UPDATE iris_diadoc_doc  SET PRINTFORM=:byteFileP,PRINT_FILE_NAME='{title}' WHERE cnt_id={cntId} AND entity_id ='{entityId}'";

            using (cmdPrintForm)
            {
                try
                {
                    cmdPrintForm.Connection = connection;
                    cmdPrintForm.BindByName = true;
                    OracleParameter byteFileP = cmdPrintForm.CreateParameter();
                    byteFileP.OracleDbType = OracleDbType.Blob;
                    byteFileP.Direction = ParameterDirection.Input;
                    //cmd.Parameters.Add("cnt_id", cntId);
                    //cmd.Parameters.Add("entity_id", entityId);
                    //cmd.Parameters.Add("title", title);
                    cmdPrintForm.Parameters.Add("byteFileP", byteFile);
                    cmdPrintForm.ExecuteNonQuery();
                }
                catch (OracleException e)
                {
                    //logger.Error(e, $"Номер ошибки - {e.Number}\n Текст ошибки: {e.Message}\n");
                    //logger.Info("Выполняется попытка восстановить подключение...\n");
                    OracleConnection.ClearPool(oracleConnection);
                    connection.Close();
                    connection.Open();
                    //logger.Info("Подключение восстановлено\n");
                }
                catch (Exception exp)
                {
                   // logger.Error(exp);
                }
            }

        }
    }
}
