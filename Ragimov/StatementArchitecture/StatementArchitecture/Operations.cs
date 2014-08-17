using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementArchitecture
{
    public class Operations<T>
    {
        public void SendToConfirmit(T value)
        {
            //Let's pretend we are sending statement to Confirmit here
            Receiver.Receive(value.ToString());
        }
    }

    public static class Receiver
    {
        public static string Received = "";

        public static void Receive(string value)
        {
            Received += value;
        }

        public static void Clear()
        {
            Received = "";
        }
    }
}
