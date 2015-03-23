using System;
using System.Threading;
using FATBox.Ui.Controls;

namespace FATBox.Ui
{
    public class Thought : IDisposable
    {
        private ThinkingForm _thinkingForm;
        private bool _cancelled;
        private string _message;

        public Thought(string message)
        {
            _message = message;
            new Thread(() =>
            {
                System.Threading.Thread.Sleep(300);
                if (_cancelled) return;
                _thinkingForm = new ThinkingForm();
                _thinkingForm.SetMessage2(_message ?? "Thinking...");
                _thinkingForm.ShowDialog();                
            }).Start();
        }


        public void SetMessage(string text)
        {
            _message = text;
            if (_thinkingForm != null)
            {
                try
                {
                    _thinkingForm.SetMessage(text);
                }
                catch (Exception)
                {
                    // lazy - threading issues sometimes
                }
            }
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