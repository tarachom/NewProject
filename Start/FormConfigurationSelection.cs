
using AccountingSoftware;
using InterfaceGtkLib;

using GeneratedCode;

namespace NewProject
{
    /// <summary>
    /// Переоприділення форми вибору бази
    /// </summary>
    class FormConfigurationSelection : InterfaceGtk3.FormConfigurationSelection
    {
        public FormConfigurationSelection() : base(Config.Kernel, Configurator.Program.Kernel, TypeForm.WorkingProgram) { }

        public override async ValueTask<bool> OpenProgram(ConfigurationParam? openConfigurationParam)
        {
            //Запуск фонових задач
            Config.StartBackgroundTask();

            Program.GeneralForm = new FormNewProject() { OpenConfigurationParam = openConfigurationParam };
            Program.GeneralNotebook = Program.GeneralForm.Notebook;
            Program.GeneralForm.Show();

            //Вивід інформації в нижній StatusBar
            Program.GeneralForm.SetStatusBar();

            //Присвоєння користувача
            Program.GeneralForm.SetCurrentUser();

            //Відкрити перші сторінки
            Program.GeneralForm.OpenFirstPages();

            return await ValueTask.FromResult(true);
        }

        public override async ValueTask<bool> OpenConfigurator(ConfigurationParam? openConfigurationParam)
        {
            Configurator.FormConfigurator сonfigurator = new() { OpenConfigurationParam = openConfigurationParam };
            сonfigurator.Show();

            сonfigurator.SetValue();
            сonfigurator.LoadTreeAsync();

            return await ValueTask.FromResult(true);
        }
    }
}