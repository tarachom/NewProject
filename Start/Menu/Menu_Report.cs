/*

Звіти

*/

using Gtk;
using InterfaceGtk;

namespace NewProject
{
    class Menu_Report : Форма
    {
        public Menu_Report() : base()
        {
            //Список
            Box hBoxList = new Box(Orientation.Horizontal, 0);
            PackStart(hBoxList, false, false, 10);

            Box vLeft = new Box(Orientation.Vertical, 0);
            hBoxList.PackStart(vLeft, false, false, 5);
            
            ShowAll();
        }
    }
}