
/*

Функції для динамічного відкриття списків довідників, документів, регістрів, журналів та звітів

*/

using Gtk;
using InterfaceGtk;
using GeneratedCode;

namespace NewProject
{
    class ФункціїДляДинамічногоВідкриття : InterfaceGtk.ФункціїДляДинамічногоВідкриття
    {
        public ФункціїДляДинамічногоВідкриття() : base(Config.NameSpageProgram, Config.NameSpageCodeGeneration) { }

        protected override void CreateNotebookPage(string tabName, Func<Widget>? pageWidget)
        {
            NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, tabName, pageWidget);
        }
    }
}