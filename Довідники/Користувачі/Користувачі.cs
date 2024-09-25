
/*     
        Користувачі.cs
        Список
*/

using Gtk;
using InterfaceGtk;
using AccountingSoftware;
using NewProject_1_0.Довідники;
using ТабличніСписки = NewProject_1_0.Довідники.ТабличніСписки;

namespace NewProject
{
    public class Користувачі : ДовідникЖурнал
    {
        public Користувачі() : base()
        {
            ТабличніСписки.Користувачі_Записи.AddColumns(TreeViewGrid);
        }

        #region Override

        protected override async ValueTask LoadRecords()
        {
            ТабличніСписки.Користувачі_Записи.SelectPointerItem = SelectPointerItem;
            ТабличніСписки.Користувачі_Записи.DirectoryPointerItem = DirectoryPointerItem;

            ТабличніСписки.Користувачі_Записи.ОчиститиВідбір(TreeViewGrid);

            await ТабличніСписки.Користувачі_Записи.LoadRecords(TreeViewGrid, OpenFolder);
        }

        protected override async ValueTask LoadRecords_OnSearch(string searchText)
        {
            ТабличніСписки.Користувачі_Записи.ОчиститиВідбір(TreeViewGrid);

            //Відбори
            ТабличніСписки.Користувачі_Записи.ДодатиВідбір(TreeViewGrid, Користувачі_Функції.Відбори(searchText));

            await ТабличніСписки.Користувачі_Записи.LoadRecords(TreeViewGrid, OpenFolder);
        }

        protected override Widget? FilterRecords(Box hBox)
        {
            return ТабличніСписки.Користувачі_Записи.CreateFilter(TreeViewGrid);
        }

        protected override async ValueTask OpenPageElement(bool IsNew, UnigueID? unigueID = null)
        {
            await Користувачі_Функції.OpenPageElement(IsNew, unigueID, CallBack_LoadRecords, null);
        }

        protected override async ValueTask SetDeletionLabel(UnigueID unigueID)
        {
            await Користувачі_Функції.SetDeletionLabel(unigueID);
        }

        protected override async ValueTask<UnigueID?> Copy(UnigueID unigueID)
        {
            return await Користувачі_Функції.Copy(unigueID);
        }

        #endregion
    }
}
