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
        private List<string> LogString;
        public TextConsole(TextBox t):base(t)
        {
            ConsoleQueue = new Queue<string>();
            LogString = new List<string>();
        }

        public void AddStringToConsole(string s)
        {
            LogString.Add(s);
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
        public override string ToString()
        {
            string aux= "";
            foreach(var str in LogString)
            {
                aux += str + "\n";
            }
            aux += DateTime.Now;
            return aux;
        }

    }
}
