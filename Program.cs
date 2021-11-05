using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ImageOverlayTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<string>> imageListByFolder = new List<List<string>>();

            //Get Root Directory
            string root = Directory.GetCurrentDirectory();
            
            //For Debug
            //root = @"C:\NFT";

            //Get Directories
            string[] dir = Directory.GetDirectories(root);

            //Delete and Create Output Directory
            try
            {
                Directory.Delete(root + @"\_output", true);
            }
            catch(Exception e)
            {
                Console.WriteLine("Warning: " + e.Message);
                Console.WriteLine(@"Assuming '_output' folder does not exist.");
            }

            Directory.CreateDirectory(root + @"\_output");

            //Load Images on directories and add to list
            int maxCombo = 0;
            foreach (string d in dir)
            {
                IEnumerable<string> imagePathList = Directory.EnumerateFiles(d);

                if(imagePathList.ToList().Count() > 0)
                {
                    imageListByFolder.Add(imagePathList.ToList());

                    if(maxCombo == 0)
                    {
                        maxCombo = imagePathList.ToList().Count();
                    }
                    else
                    {
                        maxCombo *= imagePathList.ToList().Count();
                    }
                }
            }

            //Generate Combinations
            var comboList = ImageCombiner.GetAllPossibleCombos(imageListByFolder).ToList();

            //Generate Images
            int index = 0;
            foreach(var list in comboList)
            {
                Console.WriteLine("Combo " + index + " of " + maxCombo);
                index++;
                foreach(string s in list)
                {
                    Console.WriteLine(s);
                }

                Image i = ImageCombiner.CreateImage(list.ToList());
                i.Save(root + @"\_output\" + index + ".png");
            }
        }
    }
}
