
/*
    НовийДок_SpendTheDocument.cs
    Модуль проведення для документу НовийДок
*/

using NewProject_1_0.Константи;
using NewProject_1_0.Довідники;
using NewProject_1_0.Документи;
using NewProject_1_0.РегістриНакопичення;
using NewProject;

namespace NewProject_1_0.Документи
{
    class НовийДок_SpendTheDocument
    {
        public static async ValueTask<bool> Spend(НовийДок_Objest ДокументОбєкт)
        {
            try
            {
                РегНакопичення_RecordsSet регНакопичення = new РегНакопичення_RecordsSet();

                foreach (var record in ДокументОбєкт.Накопичення_TablePart.Records)
                {
                    регНакопичення.Records.Add(new()
                    {
                        Income = true,
                        Користувач = record.Користувач,
                        Сума = record.Сума
                    });
                }

                await регНакопичення.Save(ДокументОбєкт.ДатаДок, ДокументОбєкт.UnigueID.UGuid);

                return true;
            }
            catch (Exception ex)
            {
                await СпільніФункції.ДокументНеПроводиться(ДокументОбєкт, ДокументОбєкт.Назва, ex.Message);
                return false;
            }
        }

        public static async ValueTask ClearSpend(НовийДок_Objest ДокументОбєкт)
        {
            await ValueTask.FromResult(true);
        }
    }
}
