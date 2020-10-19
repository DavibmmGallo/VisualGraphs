using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

    abstract class Console
    {
        protected TextBox Box;

        public Console(TextBox t)
        {
            Box = t;
        }

        public abstract void Clear();
        public abstract void Update();
    }
