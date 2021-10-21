using ImageOverlayTest.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ImageOverlayTest
{
    class Program
    {
        public bool IsDebug { get; set; }

        static void Main(string[] args)
        {
            List<ImageSet> imageSetList = new List<ImageSet>();

            //Get Root Directory
            string root = Directory.GetCurrentDirectory();

            //Get Directories
            string[] dir = Directory.GetDirectories(root);

            Console.WriteLine("Directory List:");
            foreach (string s in dir)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("----------");

            //Load Images on directories and add to list
            Console.WriteLine("Loading images per directory:");
            foreach (string d in dir)
            {
                IEnumerable<string> imagePathList = Directory.EnumerateFiles(d);
                List<ImageObject> l = new List<ImageObject>();
                int index = 0;

                Console.WriteLine("Directory: " + d);
                foreach (var imagePath in imagePathList)
                {
                    Console.WriteLine("Image " + index + ": " + imagePath);
                    l.Add(new ImageObject()
                    {
                        Index = index,
                        Image = Image.FromFile(imagePath)
                    });
                    index++;
                }

                imageSetList.Add(new ImageSet()
                {
                    Directory = d,
                    ImageList = l
                });
                Console.WriteLine("-");
            }
            Console.WriteLine("----------");
            imageSetList = imageSetList.OrderBy(i => i.Directory).ToList();

            //Combine in "output" folder
            string outputDir = root + @"\output";
            Directory.CreateDirectory(outputDir);

            foreach(ImageObject img in imageSetList[0].ImageList)
            {
                img.Process(null, "", 0, imageSetList.Count() - 1, imageSetList, outputDir);
            }
        }
    }
}
