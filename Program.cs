using Gtk;
using NewProject_1_0.Довідники;

namespace NewProject
{
    class Program
    {
        #region Global Var

        /// <summary>
        /// Основна форма
        /// </summary>
        public static FormNewProject? GeneralForm { get; set; }

        /// <summary>
        /// Основний блокнот
        /// </summary>
        public static Notebook? GeneralNotebook { get; set; }

        /// <summary>
        /// Авторизований користувач
        /// </summary>
        public static Користувачі_Pointer Користувач { get; set; } = new Користувачі_Pointer();

        #endregion

        public static void Main()
        {
            Application.Init();
            _ = new FormConfigurationSelection();
            Application.Run();
        }
    }
}