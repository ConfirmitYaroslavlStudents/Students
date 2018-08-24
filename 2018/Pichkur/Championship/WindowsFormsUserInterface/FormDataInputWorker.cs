using System.Threading.Tasks;
using System.Windows.Forms;
using ChampionshipLibrary;

namespace WindowsFormsUserInterface
{
    class FormDataInputWorker : IDataInputWorker
    {
        private RichTextBox _messagesBox;
        private RichTextBox _teambox;
        private TextBox _textbox;
        private bool isPressEnter=false;

        public FormDataInputWorker(RichTextBox messagesBox,TextBox tbox,RichTextBox teambox)
        {
            _messagesBox = messagesBox;          
            _textbox = tbox;
            _teambox = teambox;
            _textbox.KeyDown += _textbox_KeyDown;
        }

        private void _textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                isPressEnter = true;
            }
        }

        public string InputString()
        {
            Task task = new Task(Magic);
            task.Start();
            while (!task.IsCompleted)
            {

            }

            isPressEnter = false;
            string reslt = _textbox.Text;
            TaskWorker.SetNewTextToControl(_textbox, "");
            return reslt;
        }

        public void Magic()
        {
            while (!isPressEnter)
            {

            }
        }

        public string InputTeamName()
        {
            Task task = new Task(Magic);
            task.Start();
            while (!task.IsCompleted)
            {

            }
            isPressEnter = false;
            string reslt = _textbox.Text;
            TaskWorker.SetNewTextToControl(_textbox, "");
            TaskWorker.AddTextToControl(_teambox, reslt + '\n');
            return reslt;
        }

        public string InputTeamScore()
        {
            TaskWorker.SetNewTextToControl(_messagesBox, "");
            Task task = new Task(Magic);
            task.Start();
            while (!task.IsCompleted)
            {

            }
            isPressEnter = false;
            string reslt = _textbox.Text;
            TaskWorker.SetNewTextToControl(_textbox,"");
            return reslt;
        }

        public void WriteLineMessage(string message)
        {
            TaskWorker.SetNewTextToControl(_messagesBox, message+'\n');
        }

        public void WriteMessage(string message)
        {
            TaskWorker.SetNewTextToControl(_messagesBox, message);
        }

        
    }
}
