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
        private const int MyLimit = 10;

        public TextConsole(TextBox t)
        {
            Box = t;
            ConsoleQueue = new Queue<string>();
        }

        public void AddStringToConsole(string s)
        {
            Clear();
            if (ConsoleQueue.Count() == MyLimit)
            {
                ConsoleQueue.Dequeue();
                ConsoleQueue.Enqueue(s);
            }
            else
            {
                ConsoleQueue.Enqueue(s);
            }
        }
        
        private void Clear()
        {
            Box.Text = "";
        }

        public void UpdateConsole()
        {
            foreach(string str in ConsoleQueue)
            {
               Box.Text += str + "\n";
            }
        }

    }
}
