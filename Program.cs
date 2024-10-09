namespace Elemendid_vormis_TARpv23
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Установим визуальный стиль и совместимость рендеринга
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запускаем форму StartVorm
            Application.Run(new StartVorm());
        }
    }
}
