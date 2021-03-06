﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace TCint
{
    class Program
    {
        

        static int Main(string[] args)
        {
            string source,target,format="png";
            if (args.Length == 0)
            {
                return 1;
            }
            else if (args.Length == 2)
            {
                source = args[0];
                target = args[0] + "/" + args[1];
            }
            else if (args.Length == 3)
            {
                source = args[0];
                target = args[0] + "/"+args[1];
                if(args[2]=="png" || args[2]=="jpg")
                {
                    format = args[2];
                }
                else
                {
                    return 2;
                }
            }
            else
                return 3;

            copyDir(source, target, format);

            return 0;
            
        }

        static string averageColor(Bitmap map) {
            string color="#";
            int sumRed=0, sumBlue=0, sumGreen = 0 , alpha=0;
            int width = map.Width;
            int height = map.Height;


            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    Color pixelColor = map.GetPixel(x, y);
                    if (pixelColor.A != 0) {
                        sumRed += pixelColor.R;
                        sumGreen += pixelColor.G;
                        sumBlue += pixelColor.B;
                    }
                    else
                    {
                        alpha++;
                    }

                }
            }

            int averageRed = sumRed / ((width * height)-alpha);
            int averageGreen = sumGreen / ((width * height-alpha));
            int averageBlue = sumBlue / ((width * height)-alpha);

            color += averageRed.ToString("X2") + averageGreen.ToString("X2") + averageBlue.ToString("X2");

            return color;
        }

        static void copyDir(string sourceDir, string targetDir, string format)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(file);
                if (fileName.Substring(fileName.Length - 3)== format)
                {

                    Bitmap thisBitmap = new Bitmap(file);
                    string color = averageColor(thisBitmap);

                    try
                    {
                        File.Copy(file, Path.Combine(targetDir, color + "." + format));
                    }
                    catch
                    {
                        int c = 1;
                        bool ok = false;
                        while(!ok)
                        try
                        {
                            File.Copy(file, Path.Combine(targetDir, color +"("+ c.ToString()+")" + "." + format));
                                ok = true;
                        }
                        catch
                        {
                            c++;
                        }
                    }

                    
                }                
            }
                
        }
    }

}
