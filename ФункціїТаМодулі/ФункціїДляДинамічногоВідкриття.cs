using Gtk;
using InterfaceGtk;
using NewProject_1_0;

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