using System;
using System.Threading;
using FATBox.Ui.Controls;

namespace FATBox.Ui
{
    public class Thought : IDisposable
    {
        private ThinkingForm _thinkingForm;
        private bool _cancelled;

        public Thought()
        {
            new Thread(() =>
            {
                System.Threading.Thread.Sleep(300);
                if (_cancelled) return;
                _thinkingForm = new ThinkingForm();
                _thinkingForm.ShowDialog();
            }).Start();
        }

        public void Dispose()
        {
            try
            {
                _cancelled = true;
                if (_thinkingForm != null)
                {
                    _thinkingForm.BeginInvoke(new Action(() =>
                    {
                        _thinkingForm.Close();
                    }));
                }
            }
            catch 
            {
                // dont care
            }
        }
    }
}