using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMIrisRetail
{
	internal static class Constants
	{
		// URL веб-сервиса Диадок
		internal const string DefaultApiUrl = "https://diadoc-api.kontur.ru";

		// Идентификатор клиента, он же ключ разработчика
		internal const string DefaultClientId = "__";

		// Логин для авторизации на сервере Диадок
		internal const string DefaultLogin = "__";

		// Пароль для авторизации на сервере Диадок
		internal const string DefaultPassword = "__";

		// Подставляем сюда идентификатор своего ящика (отправителя), из которого будем отправлять документ.
		// Допустимы форматы как в GUID (12345675-1234-1234-1234-123456789012),
		// так и в формате вида 12345675123412341234123456789012@diadoc.ru
		internal const string DefaultFromBoxId = "__@diadoc.ru";

		// Подставляем сюда идентификатор ящика-получателя, в который будем отправлять документ.
		internal const string DefaultToBoxId = "<Идентификатор получателя>";

		// Подставляем сюда путь до публичной части сертификата, которым будет подписан документ (пример: C:\public.cer)
		// Важно, чтобы приватная часть ключа была установлена в машину, на которой будет выполняться этот код
		internal const string CertificatePath = @"<Путь до сертификата>";

		internal const string msgCommentStatus_1_1 = "Необходимо проверить поступление и согласовать документ\n\n";

		internal const string msgCommentStatus_1_2 = "Документ содержит ошибки. Необходимо обработать";

		internal const string textEnd = "\n============  Процесс завершен ============";

		internal const string textBegin = "============  Процесс начал работу ============  \n\n";

		internal const string docNotExist = "Документов для обработки нет";

		internal const string statusIAS = "Документ проверен в IAS\n\n";

		internal const string listContragents = "Проверка документов по контрагенту: ";

		internal const string ContrExist = "Контрагентов на проверку:";

		internal const string pathFileLog = @"__.txt";

		internal const string DirectprintForm = @"__PrintForm";

		internal const string DirectOtherDep = "Отправка в другое подразделение";

		internal const string DirectApprove = "Отправка на согласование";

		internal const string Reconnect = "Выполняется попытка восстановить подключение...\n";

		internal const string ReconnSuccess = "Подключение восстановлено\n";

		internal const string CanselRequest = "Отмена запроса на согласование на сотрудника";

		internal const string logBegin = "Начало записи лога работы сервиса...\n";

		internal const string loadDictionary = "\r\n Загрузка справочников: ";

		internal const string loadDepart = "\r\n Загрузка подразделений: ";

		internal const string loadEmployee = "\r\n Загрузка сотрудников: ";

		internal const string RecomilePackage = "Произошла перекомпиляция пакета iris_diadoc_pck, Производится перезапуск службы...";

		internal const string filterCategoryDocumentAIIIn = "Any.InboundNotFinished";

		internal const string filterCategoryDocumentAII = "Any.Inbound";

		internal const string filterCategoryDocumentAIIOut = "Any.Outbound";

		internal const string filterCatDocumentWithRecSigned = "Any.InboundWithRecipientSignature";

		internal const string filterWithSenderSigned = "Any.InboundWithSenderSignature";

		internal const string filterCatDocum = "Any.InboundWaitingForResolution";

		internal const string filterSF = "InvoiceRevision.Inbound";

		internal const string filterSFCorrection = "InvoiceCorrection.Inbound";	

		internal const string docCapitalize = "Документ оприходован";

		internal const string autoAproveDoc = "Документ прошел автосогласование\n";

		internal const string newdocForDB = "Новый документ добавлен в бд\n\n";

		internal const string olddocForDB = "Данный документ уже существует в базе\n\n";


		internal static string LogIn = "";

		internal static string PassWord = "";

		internal static string tokenQNTSOFT = "__";
	}
}
