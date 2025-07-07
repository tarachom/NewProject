/*

Журнали

*/

using Gtk;
using InterfaceGtk3;

namespace NewProject
{
    class Menu_Journal : Форма
    {
        public Menu_Journal() : base()
        {
            //Список
            Box hBoxList = new Box(Orientation.Horizontal, 0);
            PackStart(hBoxList, false, false, 10);

            Box vLeft = new Box(Orientation.Vertical, 0);
            hBoxList.PackStart(vLeft, false, false, 5);

            CreateLink(vLeft, "Повний", async () =>
            {
                Журнал_Повний page = new Журнал_Повний();
                NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, "Повний", () => page);
                await page.SetValue();
            });

            ShowAll();
        }
    }
}