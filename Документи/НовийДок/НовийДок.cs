

/*     
        НовийДок.cs
        Список
*/

using Gtk;
using InterfaceGtk;
using AccountingSoftware;

using ТабличніСписки = NewProject_1_0.Документи.ТабличніСписки;
using NewProject_1_0.Документи;

namespace NewProject
{
    public class НовийДок : ДокументЖурнал
    {
        public НовийДок() : base()
        {
            ТабличніСписки.НовийДок_Записи.AddColumns(TreeViewGrid);
        }

        #region Override

        protected override async ValueTask LoadRecords()
        {
            ТабличніСписки.НовийДок_Записи.SelectPointerItem = SelectPointerItem;
            ТабличніСписки.НовийДок_Записи.DocumentPointerItem = DocumentPointerItem;

            ТабличніСписки.НовийДок_Записи.ДодатиВідбірПоПеріоду(TreeViewGrid, Період.Period, Період.DateStart, Період.DateStop);

            await ТабличніСписки.НовийДок_Записи.LoadRecords(TreeViewGrid);
        }

        protected override async ValueTask LoadRecords_OnSearch(string searchText)
        {
            ТабличніСписки.НовийДок_Записи.ОчиститиВідбір(TreeViewGrid);
            
            //Відбори
            ТабличніСписки.НовийДок_Записи.ДодатиВідбір(TreeViewGrid, НовийДок_Функції.Відбори(searchText));

            await ТабличніСписки.НовийДок_Записи.LoadRecords(TreeViewGrid);
        }

        protected override Widget? FilterRecords(Box hBox)
        {
            return ТабличніСписки.НовийДок_Записи.CreateFilter(TreeViewGrid);
        }

        protected override async ValueTask OpenPageElement(bool IsNew, UnigueID? unigueID = null)
        {
            await НовийДок_Функції.OpenPageElement(IsNew, unigueID, CallBack_LoadRecords);
        }

        protected override async ValueTask SetDeletionLabel(UnigueID unigueID)
        {
            await НовийДок_Функції.SetDeletionLabel(unigueID);
        }

        protected override async ValueTask<UnigueID?> Copy(UnigueID unigueID)
        {
            return await НовийДок_Функції.Copy(unigueID);
        }

        const string КлючНалаштуванняКористувача = "Документи.НовийДок";

        protected override async ValueTask BeforeSetValue()
        {
            await ФункціїНалаштуванняКористувача.ОтриматиПеріодДляЖурналу(КлючНалаштуванняКористувача + KeyForSetting, Період);
        }

        protected override async void PeriodChanged()
        {
            ФункціїНалаштуванняКористувача.ЗаписатиПеріодДляЖурналу(КлючНалаштуванняКористувача + KeyForSetting, Період.Period.ToString(), Період.DateStart, Період.DateStop);
            await LoadRecords();
        }

        protected override async ValueTask SpendTheDocument(UnigueID unigueID, bool spendDoc)
        {
            НовийДок_Objest? Обєкт = await new НовийДок_Pointer(unigueID).GetDocumentObject(true);
            if (Обєкт == null) return;

            if (spendDoc)
            {
                if (!await Обєкт.SpendTheDocument(Обєкт.ДатаДок))
                    ФункціїДляПовідомлень.ПоказатиПовідомлення(Обєкт.UnigueID);
            }
            else
                await Обєкт.ClearSpendTheDocument();
        }

        protected override void ReportSpendTheDocument(UnigueID unigueID)
        {
            СпільніФорми_РухДокументуПоРегістрах.СформуватиЗвіт(new НовийДок_Pointer(unigueID));
        }

        protected override async ValueTask ExportXML(UnigueID unigueID, string pathToFolder)
        {
            НовийДок_Pointer Вказівник = new НовийДок_Pointer(unigueID);
            await Вказівник.GetPresentation();

            await НовийДок_Export.ToXmlFile(Вказівник, System.IO.Path.Combine(pathToFolder, $"{Вказівник.Назва}.xml"));
        }

        protected override async ValueTask PrintingDoc(UnigueID unigueID)
        {
            await НовийДок_Друк.PDF(unigueID);
        }

        #endregion
    }
}
    