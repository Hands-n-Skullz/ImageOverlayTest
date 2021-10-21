using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageOverlayTest.Model
{
    public class ImageObject
    {
        public Image Image { get; set; }
        public int Index { get; set; }

        public void Process(Image image, string name, int listIndex, int maxListIndex, List<ImageSet> l, string output)
        {
            Image nextImage;

            //Check if receiving image is null, if yesm then just send the image of this object, if not, overlay.
            if (image == null)
            {
                nextImage = this.Image;
            }
            else
            {
                nextImage = new Bitmap(this.Image.Width, this.Image.Height);
                using (Graphics g = Graphics.FromImage(nextImage))
                {
                    g.DrawImage(image, 0, 0);
                    g.DrawImage(this.Image, 0, 0);
                    g.Save();
                };
            }

            //Check if this is the last ImageSet on the list
            int newIndex = listIndex + 1;
            if (newIndex <= maxListIndex)
            {
                foreach(ImageObject i in l[newIndex].ImageList)
                {
                    i.Process(nextImage, name + this.Index, newIndex, maxListIndex, l, output);
                }
            }
            else
            {
                nextImage.Save(output + @"\" + name + this.Index + ".png", ImageFormat.Png);
            }
        }
    }
}
