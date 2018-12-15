using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WerewolfClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Mainmanu mMainForm = new Mainmanu();
            MainForm_16 mGame = new MainForm_16();
            Endgame end = new Endgame(mMainForm);
            mGame.Visible = false;
            mMainForm.Visible = false;
            Login_WF mLogin = new Login_WF(mMainForm);
            WerewolfController mControler =  WerewolfController.GetInstance();
            WerewolfModel mModel = new WerewolfModel();
            mMainForm.setLogin(mLogin);
            mMainForm.setGame(mGame);
            mGame.setend(end);

            // View -> Controller
            mMainForm.setController(mControler);
            mLogin.setController(mControler);
            mGame.setController(mControler);

            // Controler -> Model
            mControler.AddModel(mModel);

            // Model -> View
            mModel.AttachObserver(mLogin);
            mModel.AttachObserver(mMainForm);
            mModel.AttachObserver(mGame);

            Application.Run(mLogin);
        }
    }
}
