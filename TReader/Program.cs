using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TEditor.BusinessLogic;

namespace TReader
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //???????????????????????????????????????????????????
            MainForm form = new MainForm();
            MassageService service = new MassageService();
            FileManager manager = new FileManager();
            Transposition trans = new Transposition();
            MainPresenter presenter = new MainPresenter(form, manager, service, trans);

            Application.Run(form);
        }
    }
}
