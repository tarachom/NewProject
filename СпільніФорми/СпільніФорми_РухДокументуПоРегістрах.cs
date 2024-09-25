
/*

Вивід рухів документу по регістрах

*/

using Gtk;
using InterfaceGtk;
using AccountingSoftware;
using NewProject_1_0;
using ТабличніСписки = NewProject_1_0.РегістриНакопичення.ТабличніСписки;

namespace NewProject
{
    public class СпільніФорми_РухДокументуПоРегістрах : InterfaceGtk.СпільніФорми_РухДокументуПоРегістрах
    {
        public static async void СформуватиЗвіт(DocumentPointer ДокументВказівник)
        {
            СпільніФорми_РухДокументуПоРегістрах page = new();
            NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, "Проводки", () => page);
            await page.Заповнити(ДокументВказівник);
        }

        protected override Widget Документ_PointerControl(DocumentPointer ДокументВказівник)
        {
            return new CompositePointerControl { Pointer = ДокументВказівник.GetBasis(), Caption = "Документ:", TypeSelectSensetive = false, ClearSensetive = false };
        }

        public async ValueTask Заповнити(DocumentPointer ДокументВказівник)
        {
            ДодатиДокументНаФорму(ДокументВказівник);

            foreach (string regAccumName in Config.Kernel.Conf.Documents[ДокументВказівник.TypeDocument].AllowRegisterAccumulation)
            {
                TreeView treeView = new TreeView();

                switch (regAccumName)
                {
                    case "РегНакопичення":
                        {
                            ДодатиБлокНаФорму("РегНакопичення", treeView);

                            ТабличніСписки.РегНакопичення_Записи.AddColumns(treeView, ["period", "owner"]);
                            ТабличніСписки.РегНакопичення_Записи.ДодатиВідбірПоДокументу(treeView, ДокументВказівник.UnigueID.UGuid);
                            await ТабличніСписки.РегНакопичення_Записи.LoadRecords(treeView, false, false);
                            
                            break;
                        }
                    default:
                        {
                            await ValueTask.FromResult(true);
                            break;
                        }
                }
            }
        }
    }
}