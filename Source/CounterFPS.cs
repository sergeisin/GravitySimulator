using System;

namespace GravitySimulator
{
    internal class CounterFPS
    {
        public CounterFPS(MainForm form)
        {
            this.form = form;
        }

        public void UpdateFPS()
        {
            DateTime dateTime = DateTime.Now;
            if (second == dateTime.Second)
            {
                counter++;
            }
            else
            {
                form.Text = $"FPS : {counter}";
                counter = 1;
                second = dateTime.Second;
            }
        }

        private MainForm form;
        private int second;
        private int counter;
    }
}
