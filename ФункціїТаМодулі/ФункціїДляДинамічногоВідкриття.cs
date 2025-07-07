
/*

Функції для динамічного відкриття списків довідників, документів, регістрів, журналів та звітів

*/

using Gtk;
using InterfaceGtk3;
using GeneratedCode;

namespace NewProject
{
    class ФункціїДляДинамічногоВідкриття : InterfaceGtk3.ФункціїДляДинамічногоВідкриття
    {
        public ФункціїДляДинамічногоВідкриття() : base(Config.NameSpageProgram, Config.NameSpageCodeGeneration) { }

        protected override void CreateNotebookPage(string tabName, Func<Widget>? pageWidget)
        {
            NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, tabName, pageWidget);
        }
    }
}