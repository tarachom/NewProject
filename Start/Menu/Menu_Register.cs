/*

Регістри відомостей

*/

using Gtk;
using InterfaceGtk;
using NewProject_1_0.РегістриНакопичення;

namespace NewProject
{
    class Menu_Register : Форма
    {
        public Menu_Register() : base()
        {
            //Список
            Box hBoxList = new Box(Orientation.Horizontal, 0);
            PackStart(hBoxList, false, false, 10);

            Box vLeft = new Box(Orientation.Vertical, 0);
            hBoxList.PackStart(vLeft, false, false, 5);

            CreateLink(vLeft, РегНакопичення_Const.FULLNAME, async () =>
            {
                РегНакопичення page = new РегНакопичення();
                NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, РегНакопичення_Const.FULLNAME, () => page);
                await page.SetValue();
            });

            ShowAll();
        }
    }
}