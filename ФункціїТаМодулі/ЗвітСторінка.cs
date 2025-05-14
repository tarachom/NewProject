/*

Формування звітів

*/

using InterfaceGtk;
using GeneratedCode;
using AccountingSoftware;

namespace NewProject
{
    class ЗвітСторінка : InterfaceGtk.ЗвітСторінка
    {
        public ЗвітСторінка() : base(Config.Kernel) { }

        protected override void ВідкритиДокументВідповідноДоВиду(string name, UnigueID? unigueID, string keyForSetting = "", ФункціїДляДинамічногоВідкриття.TypeForm typeForm = ФункціїДляДинамічногоВідкриття.TypeForm.Journal)
        {
            new ФункціїДляДинамічногоВідкриття().ВідкритиДокументВідповідноДоВиду(name, unigueID, keyForSetting, typeForm);
        }

        protected override void ВідкритиДовідникВідповідноДоВиду(string name, UnigueID? unigueID, ФункціїДляДинамічногоВідкриття.TypeForm typeForm = ФункціїДляДинамічногоВідкриття.TypeForm.Journal)
        {
            new ФункціїДляДинамічногоВідкриття().ВідкритиДовідникВідповідноДоВиду(name, unigueID, typeForm);
        }
    }
}