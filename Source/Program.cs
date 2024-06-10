using System;
using System.Windows.Forms;

namespace GravitySimulator
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var t = new OpenTK.Vector2d(3, 4).LengthSquared;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
