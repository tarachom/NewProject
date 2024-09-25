/*
 
Повідомлення про помилки

*/

using Gtk;
using AccountingSoftware;

namespace NewProject
{
    class СпільніФорми_ВивідПовідомленняПроПомилки : InterfaceGtk.СпільніФорми_ВивідПовідомленняПроПомилки
    {
        protected override async ValueTask<SelectRequest_Record> ПрочитатиПовідомленняПроПомилки()
        {
            return await new ФункціїДляПовідомлень().ПрочитатиПовідомленняПроПомилки();
        }

        protected override async ValueTask ОчиститиВсіПовідомлення()
        {
            await new ФункціїДляПовідомлень().ОчиститиВсіПовідомлення();
        }

        protected override Widget СтворитиВибір(UuidAndText uuidAndText)
        {
            return new CompositePointerControl { Pointer = uuidAndText, Caption = "" };
        }
    }

    class СпільніФорми_ВивідПовідомленняПроПомилки_ШвидкийВивід : InterfaceGtk.СпільніФорми_ВивідПовідомленняПроПомилки_ШвидкийВивід
    {
        protected override async ValueTask<SelectRequest_Record> ПрочитатиПовідомленняПроПомилки(UnigueID? ВідбірПоОбєкту = null, int? limit = null)
        {
            return await new ФункціїДляПовідомлень().ПрочитатиПовідомленняПроПомилки(ВідбірПоОбєкту, limit);
        }
    }
}