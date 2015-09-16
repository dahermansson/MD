using System;
using System.IO;
using System.Linq;
using System.Web;

namespace MD.Models
{
    public class FileLoader
    {
        private string _url;
        private string[] _blogs;
        public string SiteRootPath { get { return System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "site_root"; } }
        
        public FileLoader()
        {
            _blogs = Directory.EnumerateDirectories(SiteRootPath, "__*").Select(t => t.Split(Path.DirectorySeparatorChar).Last()).ToArray();
        }

        public Page GetPage(string url)
        {
            _url = url;
            Page page = GetSingelPage();
            if(page == null)
            {
                page = GetMultiPage();
            }
            if(page != null)
                return page;
            return new Page("#404 file not found\n Look at another place. ");
        }

        private Page GetSingelPage()
        {
            var filePath = SiteRootPath + HttpUtility.UrlDecode(_url).Replace('/', Path.DirectorySeparatorChar);
            if (!File.Exists(filePath + ".md"))
            {
                string tempPath = string.Empty;
                //Try blogify the path
                foreach (var blog in _blogs)
                {
                    tempPath = filePath.Replace(blog.Replace("__", string.Empty), blog);
                    if (File.Exists(tempPath + ".md"))
                    {
                        filePath = tempPath;
                        break;
                    }
                }                
            }
            if (!File.Exists(filePath + ".md"))
                filePath += "index";

            string fileContent = string.Empty;
            try
            {
                fileContent = File.ReadAllText(filePath + ".md");
            }
            catch (Exception)
            {
                return null;
            }

            return new Page(fileContent);
        }

        public Page GetMultiPage()
        {
            string blog =_url.Split('/').Last();
            var directoryPath = Directory.EnumerateDirectories(SiteRootPath, "__*").SingleOrDefault(d => d.Split(Path.DirectorySeparatorChar).LastOrDefault().Replace("__", string.Empty) == blog);
            if (directoryPath == null)
                return null;
            Page page = new Page();
            page.Type = SiteType.MultiPage;
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                page.Contents.Add(new Content(Path.GetFileNameWithoutExtension(file), File.ReadAllText(file, System.Text.UTF8Encoding.UTF8), blog));
            }
            return page;
        }
    }
}
