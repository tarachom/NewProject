/*

Контрол для типу даних який визначається в процесі роботи з програмою

*/

using Gtk;
using InterfaceGtk;
using AccountingSoftware;
using NewProject_1_0;

namespace NewProject
{
    class CompositePointerControl : InterfaceGtk.CompositePointerControl
    {
        public CompositePointerControl() : base(Config.Kernel, Config.NameSpageProgram, Config.NameSpageCodeGeneration) { }

        protected override async ValueTask<CompositePointerPresentation_Record> CompositePointerPresentation(UuidAndText uuidAndText)
        {
            return await Functions.CompositePointerPresentation(uuidAndText);
        }
        protected override void CreateNotebookPage(string tabName, Func<Widget>? pageWidget)
        {
            NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, tabName, pageWidget);
        }
    }
}