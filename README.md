# IntegrationService
## Разработка WPF приложения для интеграции с АПИ Диадок
Данный проект использует технологию WPF .NET Framework 4.8 для своей реализации.
Целью данного проекта является автоматизация электронного документооборота в компании.
Для взаимодействия с контрагентами используется API Контур.Диадок.
Этапы прохождения документов от ящика Диадок-а - через подразделения компании - и обратно реализуются с использованием паттерна Машина состояний.
Происходит первичный парсинг XML-документов на соответствие юридическим стандартам и заполненность значимых полей:
```c#
 ///Выбор адреса организации отправителя
                if (xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПрод").Elements("Адрес").Elements("АдрРФ").Any())
                {
                    foreach (XElement dataElement in xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПрод").Elements("Адрес").Elements("АдрРФ"))
                    {
                        requisites.House      = dataElement.Attribute("Дом")?.Value?? "";
                        requisites.Index      = dataElement.Attribute("Индекс")?.Value?? "";
                        requisites.Apartment  = dataElement.Attribute("Кварт")?.Value?? "";
                        requisites.CodeRegion = dataElement.Attribute("КодРегион")?.Value?? "";
                        requisites.Street     = dataElement.Attribute("Улица")?.Value ?? "";
                        requisites.Corpus     = dataElement.Attribute("Корпус")?.Value ?? "";
                        requisites.City       = dataElement.Attribute("Город")?.Value ?? "";
                        //AddrCompany  - используется здесь как флаг для дальнейшей проверки
                        requisites.AddrCompany = null;
                    }
                }
                else
                    if(xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПрод").Elements("Адрес").Elements("АдрИнф").Any())
                {
                    foreach (XElement dataElement in xLDoc.Elements("Файл").Elements("Документ").Elements("СвСчФакт").Elements("СвПрод").Elements("Адрес").Elements("АдрИнф"))
                    {
                      requisites.AddrCompany = requisites.GetAddress(dataElement.Attribute("АдрТекст").Value);
                    }
                }
 ```
 
Далее запускается функция на Pl\sql для поиска нужного подразделения в компании:

```sql
begin
       select
          lower(trim(value_par))
       into
          v_email
       from
          EMPLOYEE_DESC
       where
         emp_par_id=998000000000121 and
         emp_id=v_emp_id and
         date_end is null and
         rownum=1;
   exception when others then
       raise email_not_found_error;
   end;

   for rec in (select
                    p.code_post
                from
                       iris_diadoc_doc bl,
                       XMLTable('//ТаблСчФакт/СведТов/ДопСведТов'
                               PASSING bl.xmldocument
                               COLUMNS code_post  varchar2(100) PATH '@КодТов') p
                where
                    id = p_diadoc_doc_id) loop
          begin
             --select ge.gds_id into v_gds_id from goods_ent ge, contractor c where ge.ent_id=c.ent_id and c.id=v_supp_id and lower(ge.code)=lower(trim(rec.code_post));
             v_gds_id := pck_mat_demand.get_goods_ent_gds_id_n_closed(v_supp_id, rec.code_post);
          exception when no_data_found then
                         err_flag := 1;
                         insert into iris_diadoc_log values
                         (null, p_diadoc_doc_id, sysdate, 'В документе №'||v_schf||' к заказу №'||v_dem_num||' указан код товара '||rec.code_post||' не существующий в связке с поставщиком '||v_supp_name,'Товар '||rec.code_post||' не найден.', 1);
                    when too_many_rows then
                         err_flag := 1;
                         insert into iris_diadoc_log values
                         (null, p_diadoc_doc_id, sysdate, 'В документе №'||v_schf||' к заказу №'||v_dem_num||' указан код товара '||rec.code_post||' для которого найдено более одной связки с поставщиком '||v_supp_name,'Найдено более одного товара '||rec.code_post, 1);
          end;

   end loop;
```
