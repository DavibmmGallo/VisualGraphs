using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace VisualGraphs.Classes
{
    class TextConsole
    {
        private Queue<string> ConsoleQueue;
        private TextBox Box;

        public TextConsole(TextBox t)
        {
            Box = t;
            ConsoleQueue = new Queue<string>();
        }

        public void AddStringToConsole(string s)
        {
            if(ConsoleQueue.Count() == 5)
            {
                ConsoleQueue.Dequeue();
                ConsoleQueue.Enqueue(s);
            }
            ConsoleQueue.Enqueue(s);
        }

        public void SetTextBox(string str)
        {
            Box.Text = str;
        }

        public foo()
        {
            Debug.WriteLine("GG");
        }


    }
}
