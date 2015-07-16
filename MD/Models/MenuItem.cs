using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MD.Models
{
    public class MenuItem
    {
        public MenuItem()
        {
            Items = new List<MenuItem>();
        }
        public MenuItem(string title, bool folder = false)
        {
            Title = title;
            Folder = folder;
            Items = new List<MenuItem>();
        }

        public string Title { get; set; }
        public List<MenuItem> Items { get; set; }
        public bool Folder { get; set; }
    }
}