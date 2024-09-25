
/*
        НовийДок_Елемент.cs
        Елемент
*/

using Gtk;
using InterfaceGtk;
using AccountingSoftware;

using NewProject_1_0;
using NewProject_1_0.Константи;
using NewProject_1_0.Довідники;
using NewProject_1_0.Документи;
using NewProject_1_0.Перелічення;

namespace NewProject
{
    class НовийДок_Елемент : ДокументЕлемент
    {
        public НовийДок_Objest Елемент { get; init; } = new НовийДок_Objest();

        #region Fields
        Entry НомерДок = new Entry() { WidthRequest = 100 };
        DateTimeControl ДатаДок = new DateTimeControl();
        Entry Коментар = new Entry() { WidthRequest = 500 };

        #endregion

        #region TabularParts

        // Таблична частина "Накопичення" 
        НовийДок_ТабличнаЧастина_Накопичення Накопичення = new НовийДок_ТабличнаЧастина_Накопичення();

        #endregion

        public НовийДок_Елемент()
        {
            Елемент.UnigueIDChanged += UnigueIDChanged;
            Елемент.CaptionChanged += CaptionChanged;

            CreateDocName(НовийДок_Const.FULLNAME, НомерДок, ДатаДок);

            CreateField(HBoxComment, "Коментар:", Коментар);

            // Таблична частина "Накопичення" 
            NotebookTablePart.InsertPage(Накопичення, new Label("Накопичення"), 0);

            NotebookTablePart.CurrentPage = 0;

        }

        protected override void CreateContainer1(Box vBox)
        {

        }

        protected override void CreateContainer2(Box vBox)
        {

        }

        protected override void CreateContainer3(Box vBox)
        {

        }

        protected override void CreateContainer4(Box vBox)
        {

        }

        #region Присвоєння / зчитування значень

        public override async void SetValue()
        {
            НомерДок.Text = Елемент.НомерДок;
            ДатаДок.Value = Елемент.ДатаДок;
            Коментар.Text = Елемент.Коментар;

            // Таблична частина "Накопичення" 
            Накопичення.ЕлементВласник = Елемент;
            await Накопичення.LoadRecords();

        }

        protected override void GetValue()
        {
            Елемент.НомерДок = НомерДок.Text;
            Елемент.ДатаДок = ДатаДок.Value;
            Елемент.Коментар = Коментар.Text;

        }

        #endregion

        protected override async ValueTask<bool> Save()
        {
            bool isSaved = false;
            try
            {
                if (await Елемент.Save())
                {
                    await Накопичення.SaveRecords(); // Таблична частина "Накопичення"

                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                ФункціїДляПовідомлень.ДодатиПовідомлення(Елемент.GetBasis(), Caption, ex);
            }
            return isSaved;
        }

        protected override async ValueTask<bool> SpendTheDocument(bool spendDoc)
        {
            if (spendDoc)
            {
                bool isSpend = await Елемент.SpendTheDocument(Елемент.ДатаДок);
                if (!isSpend) ФункціїДляПовідомлень.ПоказатиПовідомлення(Елемент.UnigueID);
                return isSpend;
            }
            else
            {
                await Елемент.ClearSpendTheDocument();
                return true;
            }
        }

        protected override void ReportSpendTheDocument(UnigueID unigueID)
        {
            СпільніФорми_РухДокументуПоРегістрах.СформуватиЗвіт(new НовийДок_Pointer(unigueID));
        }
    }
}
