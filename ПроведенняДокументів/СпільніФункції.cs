/*
 
Модуль проведення документів
 
*/

using AccountingSoftware;

namespace GeneratedCode.Документи
{
    class СпільніФункції
    {
        /// <summary>
		/// Перервати проведення документу
		/// </summary>
		/// <param name="ДокументОбєкт">Документ обєкт</param>
		/// <param name="НазваДокументу">Назва документу</param>
		/// <param name="СписокПомилок">Список помилок</param>
        public static async ValueTask ДокументНеПроводиться(DocumentObject ДокументОбєкт, string НазваДокументу, string СписокПомилок)
        {
            await Config.Kernel.MessageErrorAdd("Проведення документу", ДокументОбєкт.UnigueID.UGuid, $"Документи.{ДокументОбєкт.TypeDocument}", НазваДокументу,
                 СписокПомилок + "\n\nДокумент [ " + НазваДокументу + " ] не проводиться!");
        }
    }
}
