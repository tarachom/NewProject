/*

Заблоковані об'єкти

*/

using AccountingSoftware;
using GeneratedCode;

namespace NewProject
{
    class БлокДляСторінки_ЗаблокованіОбєкти : InterfaceGtk3.БлокДляСторінки_ЗаблокованіОбєкти
    {
        public БлокДляСторінки_ЗаблокованіОбєкти() : base(Config.Kernel) { }

        protected override async ValueTask<CompositePointerPresentation_Record> CompositePointerPresentation(UuidAndText uuidAndText)
        {
            return await Functions.CompositePointerPresentation(uuidAndText);
        }
    }
}