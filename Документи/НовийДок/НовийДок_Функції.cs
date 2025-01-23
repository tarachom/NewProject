

/*
        НовийДок_Функції.cs
        Функції
*/

using InterfaceGtk;
using AccountingSoftware;
using GeneratedCode.Документи;

namespace NewProject
{
    static class НовийДок_Функції
    {
        public static List<Where> Відбори(string searchText)
        {
            return
            [
                
                //Назва
                new Where(НовийДок_Const.Назва, Comparison.LIKE, searchText) { FuncToField = "LOWER" },

            ];
        }

        public static async ValueTask OpenPageElement(bool IsNew, UnigueID? unigueID = null,
            Action<UnigueID?>? сallBack_LoadRecords = null)
        {
            НовийДок_Елемент page = new НовийДок_Елемент
            {
                IsNew = IsNew,
                CallBack_LoadRecords = сallBack_LoadRecords
            };

            if (IsNew)
                await page.Елемент.New();
            else if (unigueID == null || !await page.Елемент.Read(unigueID))
            {
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
                return;
            }

            NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, page.Caption, () => page);
            page.SetValue();
        }

        public static async ValueTask SetDeletionLabel(UnigueID unigueID)
        {
            НовийДок_Objest Обєкт = new НовийДок_Objest();
            if (await Обєкт.Read(unigueID))
                await Обєкт.SetDeletionLabel(!Обєкт.DeletionLabel);
            else
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
        }

        public static async ValueTask<UnigueID?> Copy(UnigueID unigueID)
        {
            НовийДок_Objest Обєкт = new НовийДок_Objest();
            if (await Обєкт.Read(unigueID))
            {
                НовийДок_Objest Новий = await Обєкт.Copy(true);
                await Новий.Save();

                await Новий.Накопичення_TablePart.Save(false); // Таблична частина "Накопичення"

                return Новий.UnigueID;
            }
            else
            {
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
                return null;
            }
        }
    }
}
