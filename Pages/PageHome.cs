/*

Старова сторінка

*/

using Gtk;
using InterfaceGtk;
using NewProject_1_0;

namespace NewProject
{
    class PageHome : Box
    {
        public БлокДляСторінки_АктивніКористувачі АктивніКористувачі = new БлокДляСторінки_АктивніКористувачі(Config.Kernel) { WidthRequest = 500 };

        public PageHome() : base(Orientation.Vertical, 0)
        {
            {
                Box hBox = new Box(Orientation.Horizontal, 0);
                hBox.PackStart(АктивніКористувачі, false, false, 5);
                PackStart(hBox, false, false, 5);
            }

            ShowAll();
        }
    }
}