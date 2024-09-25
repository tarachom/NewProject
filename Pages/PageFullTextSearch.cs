/*

Повнотекстовий пошук

*/

using NewProject_1_0;

namespace NewProject
{
    class PageFullTextSearch : InterfaceGtk.PageFullTextSearch
    {
        public PageFullTextSearch() : base(Config.Kernel, Config.NameSpageProgram, Config.NameSpageCodeGeneration) { }
    }
}