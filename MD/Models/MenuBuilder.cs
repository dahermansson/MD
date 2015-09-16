using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace MD.Models
{
    public class MenuBuilder
    {
        public MenuItem GetMenu()
        {
            MenuItem menu = new MenuItem();
            AddMenuItemsInFolder(menu, "site_root");
            return menu;
        }

        private void AddMenuItemsInFolder(MenuItem item, string folder)
        {
            foreach (var i in Directory.GetFiles(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + folder))
            {
                if (Path.GetFileNameWithoutExtension(i).ToLower() == "index")
                    continue;
                item.Items.Add(new MenuItem(Path.GetFileNameWithoutExtension(i)));
            }
            foreach (var d in Directory.GetDirectories(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + folder))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(d);
                bool blog = dirInfo.Name.StartsWith("__");
                if (dirInfo.Name.EndsWith("__")) //Ignorera bildmappar
                    continue;
                string dirPath = Path.Combine(folder,dirInfo.Name);
                item.Items.Add(new MenuItem(dirInfo.Name.TrimStart('_'), !blog));
                if(!blog)
                    AddMenuItemsInFolder(item.Items.Last(), dirPath);
            }
        }
    }
}