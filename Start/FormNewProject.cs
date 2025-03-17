using Gtk;
using InterfaceGtk;

using GeneratedCode;
using GeneratedCode.Довідники;

namespace NewProject
{
    class FormNewProject : FormGeneral
    {
        public FormNewProject() : base(Config.Kernel) { }

        public void OpenFirstPages()
        {
            PageHome page = new PageHome();
            NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, "Стартова", () => page, false, null, null, true);
        }

        public async void SetCurrentUser()
        {
            Користувачі_Pointer ЗнайденийКористувач = await new Користувачі_Select().FindByField(Користувачі_Const.КодВСпеціальнійТаблиці, Config.Kernel.User);

            if (ЗнайденийКористувач.IsEmpty())
            {
                Користувачі_Objest НовийКористувач = new Користувачі_Objest
                {
                    КодВСпеціальнійТаблиці = Config.Kernel.User,
                    Назва = await Config.Kernel.DataBase.SpetialTableUsersGetFullName(Config.Kernel.User)
                };

                await НовийКористувач.New();
                await НовийКористувач.Save();

                Program.Користувач = НовийКористувач.GetDirectoryPointer();
            }
            else
                Program.Користувач = ЗнайденийКористувач;
        }

        protected override void Налаштування(LinkButton lb)
        {
            PageSettings page = new PageSettings();
            NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, "Налаштування", () => page);
            page.SetValue();
        }

        protected override void Сервіс(LinkButton lb)
        {
            PageService page = new PageService();
            NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, "Сервіс", () => page);
            page.SetValue();
        }

        #region Override

        protected override void ButtonMessageClicked()
        {
            ФункціїДляПовідомлень.ВідкритиПовідомлення();
        }

        protected override void ButtonFindClicked(string text)
        {
            PageFullTextSearch page = new PageFullTextSearch();
            NotebookFunction.CreateNotebookPage(Program.GeneralNotebook, "Пошук", () => page);
            page.Find(text);
        }

        protected override void ВідкритиДокументВідповідноДоВиду(string name)
        {
            new ФункціїДляДинамічногоВідкриття().ВідкритиДокументВідповідноДоВиду(name, null);
        }

        protected override void ВідкритиДовідникВідповідноДоВиду(string name)
        {
            new ФункціїДляДинамічногоВідкриття().ВідкритиДовідникВідповідноДоВиду(name, null);
        }

        protected override void ВідкритиЖурналВідповідноДоВиду(string name)
        {
            new ФункціїДляДинамічногоВідкриття().ВідкритиЖурналВідповідноДоВиду(name, null);
        }

        protected override void ВідкритиРегістрВідомостейВідповідноДоВиду(string name)
        {
            new ФункціїДляДинамічногоВідкриття().ВідкритиРегістрВідомостейВідповідноДоВиду(name, null);
        }

        protected override void ВідкритиРегістрНакопиченняВідповідноДоВиду(string name)
        {
            new ФункціїДляДинамічногоВідкриття().ВідкритиРегістрНакопиченняВідповідноДоВиду(name, null);
        }

        protected override void МенюДокументи(Box vBox)
        {
            vBox.PackStart(new Menu_Document(), false, false, 0);
        }

        protected override void МенюДовідники(Box vBox)
        {
            vBox.PackStart(new Menu_Directory(), false, false, 0);
        }

        protected override void МенюЖурнали(Box vBox)
        {
            vBox.PackStart(new Menu_Journal(), false, false, 0);
        }

        protected override void МенюЗвіти(Box vBox)
        {
            vBox.PackStart(new Menu_Report(), false, false, 0);
        }

        protected override void МенюРегістри(Box vBox)
        {
            vBox.PackStart(new Menu_Register(), false, false, 0);
        }

        #endregion
    }
}