using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System;

namespace ImageOverlayTest
{
    public static class ImageCombiner
    {
        public static IEnumerable<IEnumerable<string>> GetAllPossibleCombos(IEnumerable<IEnumerable<string>> strings)
        {
            IEnumerable<IEnumerable<string>> combos = new string[][] { new string[0] };

            foreach (var inner in strings)
                combos = from c in combos
                         from i in inner
                         select c.Append(i);

            return combos;
        }

        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource item)
        {
            foreach (TSource element in source)
                yield return element;

            yield return item;
        }

        public static Image CreateImage(List<string> imagePathList)
        {
            if(imagePathList.Count() > 0)
            {
                List<Image> imageList = new List<Image>();
                foreach(string s in imagePathList)
                {
                    imageList.Add(Image.FromFile(s));
                }

                Image generatedImage = (Bitmap) imageList[0].Clone();
                int imageCount = 0;

                foreach(Image i in imageList)
                {
                    using (Graphics g = Graphics.FromImage(generatedImage))
                    {
                        g.DrawImage(i, 0, 0);
                        g.Save();
                        g.Dispose();
                        imageCount++;
                    };

                    if(imageCount >= 5)
                    {
                        GC.Collect();
                    }
                }
                return generatedImage;
            }
            else
            {
                return null;
            }
        }
    }
}
