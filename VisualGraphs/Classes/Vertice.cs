using System;
using System.Collections.Generic;
using System.Text;

namespace VisualGraphs.Classes
{
    class Vertice
    {
        public int _id { get; set; }
        public string Label { get; set; }
        
        public Vertice(string label, int id)
        {
            _id = id;
            Label = label;
        }

        public override string ToString()
        {
            return  "Vertice: " + Label;
        }
    }
}
