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

        protected override void ВідкритиДокументВідповідноДоВиду(string name, UnigueID? unigueID, string keyForSetting = "")
        {
            new ФункціїДляДинамічногоВідкриття().ВідкритиДокументВідповідноДоВиду(name, unigueID);
        }

        protected override void ВідкритиДовідникВідповідноДоВиду(string name, UnigueID? unigueID)
        {
            new ФункціїДляДинамічногоВідкриття().ВідкритиДовідникВідповідноДоВиду(name, unigueID);
        }
    }
}