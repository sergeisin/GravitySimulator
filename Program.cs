using System;
using System.Drawing;
using System.Windows.Forms;

namespace GravitySimulator
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            InitTest();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void InitTest()
        {
            //PhyObject[] objects = new PhyObject[]
            //{
            //        new PhyObject(1.0, new Point(0.0, 0.0), new Complex()),
            //        new PhyObject(1.0, new Point(0.0, 4.0), new Complex()),
            //        new PhyObject(1.0, new Point(3.0, 0.0), new Complex())
            //};
        }
    }
}
