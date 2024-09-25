/*

Налаштування

*/

using Gtk;
using InterfaceGtk;

using AccountingSoftware;
using NewProject_1_0;

namespace NewProject
{
    class PageSettings : Форма
    {
        public PageSettings() : base()
        {
            //Кнопки
            Box hBox = new Box(Orientation.Horizontal, 0);

            Button bSave = new Button("Зберегти");
            bSave.Clicked += OnSave;

            hBox.PackStart(bSave, false, false, 10);

            PackStart(hBox, false, false, 10);

            Paned hPaned = new Paned(Orientation.Horizontal) { BorderWidth = 5, Position = 500 };

            CreatePack1(hPaned);
            CreatePack2(hPaned);

            PackStart(hPaned, false, false, 5);

            ShowAll();
        }

        void CreatePack1(Paned hPaned)
        {
            Box vBox = new Box(Orientation.Vertical, 0);
            hPaned.Pack1(vBox, false, false);

        }

        void CreatePack2(Paned hPaned)
        {
            Box vBox = new Box(Orientation.Vertical, 0);
            hPaned.Pack2(vBox, false, false);

        }

        public void SetValue()
        {
            
        }

        void GetValue()
        {
            
        }

        void OnSave(object? sender, EventArgs args)
        {
            NotebookFunction.SensitiveNotebookPageToCode(Program.GeneralNotebook, this.Name, false);
            GetValue();
            NotebookFunction.SensitiveNotebookPageToCode(Program.GeneralNotebook, this.Name, true);
        }
    }
}