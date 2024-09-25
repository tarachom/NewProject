/*

1. Очистка помічених на видалення
2. Масове перепроведення документів

*/

using NewProject_1_0;

namespace NewProject
{
    class PageService : InterfaceGtk.PageService
    {
        public PageService() : base(Config.Kernel, Config.NameSpageProgram, Config.NameSpageCodeGeneration) { }

        const string КлючНалаштуванняКористувача = "PageService";

        protected override async ValueTask BeforeSetValue()
        {
            await ФункціїНалаштуванняКористувача.ОтриматиПеріодДляЖурналу(КлючНалаштуванняКористувача, Період);
        }

        protected override void PeriodChanged()
        {
            ФункціїНалаштуванняКористувача.ЗаписатиПеріодДляЖурналу(КлючНалаштуванняКористувача, Період.Period.ToString(), Період.DateStart, Період.DateStop);
        }
    }
}