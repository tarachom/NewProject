/*

Довідники

*/

using Gtk;
using InterfaceGtk;
using GeneratedCode.Довідники;

namespace NewProject
{
    class Menu_Directory : Форма
    {
        public Menu_Directory() : base()
        {
            //Список
            Box hBoxList = new Box(Orientation.Horizontal, 0);
            PackStart(hBoxList, false, false, 10);

            Box vLeft = new Box(Orientation.Vertical, 0);
            hBoxList.PackStart(vLeft, false, false, 5);

            CreateLink(vLeft, Користувачі_Const.FULLNAME, async () =>
            {
                Користувачі page = new Користувачі();
                NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, Користувачі_Const.FULLNAME, () => page);
                await page.SetValue();
            });

            ShowAll();
        }
    }
}