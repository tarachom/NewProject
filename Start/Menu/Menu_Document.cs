/*

Документи

*/

using Gtk;
using InterfaceGtk;
using NewProject_1_0.Документи;

namespace NewProject
{
    class Menu_Document : Форма
    {
        public Menu_Document() : base()
        {
            //Список
            Box hBoxList = new Box(Orientation.Horizontal, 0);
            PackStart(hBoxList, false, false, 10);

            Box vLeft = new Box(Orientation.Vertical, 0);
            hBoxList.PackStart(vLeft, false, false, 5);

            CreateLink(vLeft, НовийДок_Const.FULLNAME, async () =>
            {
                НовийДок page = new НовийДок();
                NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, НовийДок_Const.FULLNAME, () => page);
                await page.SetValue();
            });

            ShowAll();
        }
    }
}