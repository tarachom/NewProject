/*

Стартова сторінка

*/

using Gtk;
using InterfaceGtk;
using GeneratedCode;

namespace NewProject
{
    class PageHome : Форма
    {
        public БлокДляСторінки_АктивніКористувачі АктивніКористувачі = new БлокДляСторінки_АктивніКористувачі(Config.Kernel) { WidthRequest = 600, HeightRequest = 200 };

        public PageHome()
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