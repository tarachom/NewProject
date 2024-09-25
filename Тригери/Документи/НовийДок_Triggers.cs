
/*
    НовийДок_Triggers.cs
    Тригери для документу НовийДок
*/

using NewProject_1_0.Константи;

namespace NewProject_1_0.Документи
{
    class НовийДок_Triggers
    {
        public static async ValueTask New(НовийДок_Objest ДокументОбєкт)
        {
            ДокументОбєкт.НомерДок = (++НумераціяДокументів.НовийДок_Const).ToString("D8");
            ДокументОбєкт.ДатаДок = DateTime.Now;
            await ValueTask.FromResult(true);
        }

        public static async ValueTask Copying(НовийДок_Objest ДокументОбєкт, НовийДок_Objest Основа)
        {
            ДокументОбєкт.Назва += " - Копія";
            await ValueTask.FromResult(true);
        }

        public static async ValueTask BeforeSave(НовийДок_Objest ДокументОбєкт)
        {
            ДокументОбєкт.Назва = $"{НовийДок_Const.FULLNAME} №{ДокументОбєкт.НомерДок} від {ДокументОбєкт.ДатаДок.ToShortDateString()}";
            await ValueTask.FromResult(true);
        }

        public static async ValueTask AfterSave(НовийДок_Objest ДокументОбєкт)
        {
            await ValueTask.FromResult(true);
        }

        public static async ValueTask SetDeletionLabel(НовийДок_Objest ДокументОбєкт, bool label)
        {
            await ValueTask.FromResult(true);
        }

        public static async ValueTask BeforeDelete(НовийДок_Objest ДокументОбєкт)
        {
            await ValueTask.FromResult(true);
        }
    }
}
