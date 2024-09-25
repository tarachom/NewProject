
/*
        Користувачі_Елемент.cs
        Елемент
*/

using Gtk;
using InterfaceGtk;

using NewProject_1_0.Довідники;

namespace NewProject
{
    class Користувачі_Елемент : ДовідникЕлемент
    {
        public Користувачі_Objest Елемент { get; init; } = new Користувачі_Objest();

        #region Fields
        Entry Код = new Entry() { WidthRequest = 100 };
        Entry Назва = new Entry() { WidthRequest = 500 };
        TextView Коментар = new TextView() { WidthRequest = 500, WrapMode = WrapMode.Word };

        #endregion

        #region TabularParts

        // Таблична частина "Контакти" 
        Користувачі_ТабличнаЧастина_Контакти Контакти = new Користувачі_ТабличнаЧастина_Контакти() { HeightRequest = 300 };

        #endregion

        public Користувачі_Елемент() : base()
        {
            Елемент.UnigueIDChanged += UnigueIDChanged;
            Елемент.CaptionChanged += CaptionChanged;
        }

        protected override void CreatePack1(Box vBox)
        {
            // Код
            CreateField(vBox, "Код:", Код);

            // Назва
            CreateField(vBox, "Назва:", Назва);

            // Коментар
            CreateFieldView(vBox, "Коментар:", Коментар, 500, 200);
        }

        protected override void CreatePack2(Box vBox)
        {
            // Таблична частина "Контакти" 
            CreateTablePart(vBox, "Контакти:", Контакти);
        }

        #region Присвоєння / зчитування значень

        public override async void SetValue()
        {
            Код.Text = Елемент.Код;
            Назва.Text = Елемент.Назва;
            Коментар.Buffer.Text = Елемент.Коментар;

            // Таблична частина "Контакти"
            Контакти.ЕлементВласник = Елемент;
            await Контакти.LoadRecords();
        }

        protected override void GetValue()
        {
            Елемент.Код = Код.Text;
            Елемент.Назва = Назва.Text;
            Елемент.Коментар = Коментар.Buffer.Text;
        }

        #endregion

        protected override async ValueTask<bool> Save()
        {
            bool isSaved = false;
            try
            {
                if (await Елемент.Save())
                {
                    await Контакти.SaveRecords(); // Таблична частина "Контакти"
                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                ФункціїДляПовідомлень.ДодатиПовідомлення(Елемент.GetBasis(), Caption, ex);
            }
            return isSaved;
        }
    }
}
