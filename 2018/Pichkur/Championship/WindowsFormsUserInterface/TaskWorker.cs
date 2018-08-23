using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsUserInterface
{
    static class TaskWorker
    {
        public static void StartNewTask(Action action)
        {
            Task task = new Task(action);
            task.Start();
        }

        public static void SetNewTextToControl(Control control, string newText)
        {
            if (control.InvokeRequired)
                control.Invoke(new Action(() =>
                {
                    control.Text = newText;
                }));
        }

        public static void AddTextToControl(Control control, string newText)
        {
            if (control.InvokeRequired)
                control.Invoke(new Action(() =>
                {
                    control.Text += newText;
                }));
        }
    }
}
