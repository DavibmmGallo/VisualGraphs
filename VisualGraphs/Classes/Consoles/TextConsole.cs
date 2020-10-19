using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace VisualGraphs.Classes
{
    class TextConsole: Console
    {
        private Queue<string> ConsoleQueue;
        private const int MyLimit = 10;

        public TextConsole(TextBox t):base(t)
        {
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
        
        public override void Clear()
        {
            Box.Text = "";
        }

        public override void Update()
        {
            foreach(string str in ConsoleQueue)
            {
               Box.Text += str + "\n";
            }
        }

    }
}
