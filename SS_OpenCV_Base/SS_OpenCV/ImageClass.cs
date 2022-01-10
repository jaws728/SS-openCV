using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;
using System.IO;
using System.Linq;

namespace SS_OpenCV
{
    class ImageClass
    {

        /// <summary>
        /// Image Negative using EmguCV library
        /// Slower method
        /// </summary>
        /// <param name="img">Image</param>
        public static void Negative_old(Image<Bgr, byte> img)
        {
            int x, y;

            Bgr aux;
            for (y = 0; y < img.Height; y++)
            {
                for (x = 0; x < img.Width; x++)
                {
                    //direct access: slower
                    aux = img[y, x];
                    img[y, x] = new Bgr(255 - aux.Blue, 255 - aux.Green, 255 - aux.Red);
                }
            }
        }

        public static void Negative(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * width; // alinhament bytes (padding)
                int x, y;

                if (nC == 3)
                {// image in RGB
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {

                            //PIXEL PROCESSING
                            //retrive 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            //convert to gray
                            blue = (byte)(255 - blue);
                            green = (byte)(255 - green);
                            red = (byte)(255 - red);

                            //store in image
                            dataPtr[0] = blue;
                            dataPtr[1] = green;
                            dataPtr[2] = red;

                            // advance the pointer to the next pixel
                            dataPtr += nC;
                        }
                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        /// <summary>
        /// Convert to gray
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * width; // alinhament bytes (padding)
                int x, y;

                if (nC == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrive 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            dataPtr += nC;
                        }
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * width; // alinhament bytes (padding)
                int x, y;

                if (nC == 3)
                {// image in RGB
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            dataPtr[0] = dataPtr[2];
                            dataPtr[1] = dataPtr[2];
                            dataPtr += nC;
                        }
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void GreenCnel(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * width; // alinhament bytes (padding)
                int x, y;

                if (nC == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            dataPtr[0] = dataPtr[1];
                            dataPtr[2] = dataPtr[1];
                            dataPtr += nC;
                        }
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void BlueChannel (Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nC == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            dataPtr[1] = dataPtr[0];
                            dataPtr[2] = dataPtr[0];
                            dataPtr += nC;
                        }
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int padding = m.widthStep - nC * width;
                int x, y;
                byte red, green, blue;

                if (nC == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //pixel(x,y) = contrast * pixel_old(x,y) + bright
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            blue = (byte)Math.Round(contrast * (int)blue + bright);
                            green = (byte)Math.Round(contrast * (int)green + bright);
                            red = (byte)Math.Round(contrast * (int)red + bright);

                            dataPtr[0] = blue;
                            dataPtr[1] = green;
                            dataPtr[2] = red;

                            dataPtr += nC; //point to the next pixel
                        }
                        dataPtr += padding; //allignment padding out
                    }
                }
            }
        }


        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();
                byte* ptr1;
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int padding = wS - nC * width;
                int x, y, x0, y0;

                if (nC == 3)
                {  // image in RGB
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //new x and y
                            x0 = x - dx;
                            y0 = y - dy;

                            if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height)
                            {
                                // calculate the address of the pixel (x,y)
                                ptr1 = (dataPtrC + y0 * wS + x0 * nC); //point to pixel
                                //store in the image
                                (dataPtr + y * wS + x * nC)[0] = ptr1[0];
                                (dataPtr + y * wS + x * nC)[1] = ptr1[1];
                                (dataPtr + y * wS + x * nC)[2] = ptr1[2];

                            }
                            else //out pixels will be black
                            {
                                (dataPtr + y * wS + x * nC)[0] = 0;
                                (dataPtr + y * wS + x * nC)[1] = 0;
                                (dataPtr + y * wS + x * nC)[2] = 0;
                            }
                            //dataPtr += nC; //point to the next pixel
                        }
                        //dataPtr += padding; //allignment padding out
                    }
                }
            }
        }

        
        public static void Rotation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();
                byte* ptr1;
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int padding = wS - nC * width;
                int x, y, x0, y0;
                double cosine = (double)Math.Cos((double)angle);
                double sine = (double)Math.Sin((double)angle);
                float w2 = (float)(width / 2.0);
                float h2 = (float)(height / 2.0);

                if (nC == 3)
                {  // image in RGB
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //new x and y
                            x0 = (int)Math.Round((x - w2) * cosine - (h2 - y) * sine + w2);
                            y0 = (int)Math.Round(h2 - (x - w2) * sine - (h2 - y) * cosine);

                            if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height) //if the pixels goes out of the image
                            {
                                // calculate point address (x,y)
                                ptr1 = (dataPtrC + y0 * wS + x0 * nC); //point to pixel
                                //store in the image 
                                (dataPtr + y * wS + x * nC)[0] = (byte)(ptr1)[0];
                                (dataPtr + y * wS + x * nC)[1] = (byte)(ptr1)[1];
                                (dataPtr + y * wS + x * nC)[2] = (byte)(ptr1)[2];

                            }
                            else
                            {
                                (dataPtr + y * wS + x * nC)[0] = 0;
                                (dataPtr + y * wS + x * nC)[1] = 0;
                                (dataPtr + y * wS + x * nC)[2] = 0;
                            }
                        }
                    }
                }
            }
        }


        public static void Rotation_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();
                //byte* dataPtrC;
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int padding = wS - nC * width;
                int x, y;
                double b1, b2, b3, g1, g2, g3, r1, r2, r3;
                byte* ptr1, ptr2, ptr3, ptr4;
                double x0, y0, x_decimal, y_decimal;
                double cosine = (double)Math.Cos((double)angle);
                double sine = (double)Math.Sin((double)angle);
                float w2 = (float)(width / 2.0);
                float h2 = (float)(height / 2.0);

                if (nC == 3)
                {  // image in RGB
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            x0 = (double)((x - w2) * cosine - (h2 - y) * sine + w2);
                            y0 = (double)(h2 - (x - w2) * sine - (h2 - y) * cosine);

                            if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height) //if the pixels goes out of the image
                            {
                                //BILINEAR METHOD
                                //get the nearest 4 pixels
                                ptr1 = (dataPtrC + (int)Math.Floor(y0) * wS + (int)Math.Floor(x0) * nC);
                                ptr2 = (dataPtrC + (int)Math.Floor(y0) * wS + (int)Math.Ceiling(x0) * nC);
                                ptr3 = (dataPtrC + (int)Math.Ceiling(y0) * wS + (int)Math.Floor(x0) * nC);
                                ptr4 = (dataPtrC + (int)Math.Ceiling(y0) * wS + (int)Math.Ceiling(x0) * nC);
                                //retrieve decimal part of the x0, y0
                                x_decimal = x0 % 1;
                                y_decimal = y0 % 1;

                                //get pixel values and calculate x values
                                b1 = (double)(ptr1[0] + x_decimal * (ptr2[0] - ptr1[0]));
                                g1 = (double)(ptr1[1] + x_decimal * (ptr2[1] - ptr1[1]));
                                r1 = (double)(ptr1[2] + x_decimal * (ptr2[2] - ptr1[2]));

                                b2 = (double)(ptr3[0] + x_decimal * (ptr4[0] - ptr3[0]));
                                g2 = (double)(ptr3[1] + x_decimal * (ptr4[1] - ptr3[1]));
                                r2 = (double)(ptr3[2] + x_decimal * (ptr4[2] - ptr3[2]));

                                b3 = (double)(b1 + y_decimal * (b2 - b1));
                                g3 = (double)(g1 + y_decimal * (g2 - g1));
                                r3 = (double)(r1 + y_decimal * (r2 - r1));

                                //store in the image 
                                (dataPtr + y * wS + x * nC)[0] = (byte)(Math.Round(b3));
                                (dataPtr + y * wS + x * nC)[1] = (byte)(Math.Round(g3));
                                (dataPtr + y * wS + x * nC)[2] = (byte)(Math.Round(r3));

                            }
                            else
                            {
                                (dataPtr + y * wS + x * nC)[0] = 0;
                                (dataPtr + y * wS + x * nC)[1] = 0;
                                (dataPtr + y * wS + x * nC)[2] = 0;
                            }
                        }
                    }
                }
            }
        }

        public static void Scale(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();
                byte* ptr1;
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int padding = wS - nC * width;
                int x, y, x0, y0;

                if (nC == 3)
                {  // image in RGB
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //new x and y
                            x0 = (int)Math.Round(x / scaleFactor);
                            y0 = (int)Math.Round(y / scaleFactor);

                            if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height)
                            {
                                ptr1 = (dataPtrC + y0 * wS + x0 * nC); //point to pixel
                                //store in the image 
                                (dataPtr + y * wS + x * nC)[0] = ptr1[0];
                                (dataPtr + y * wS + x * nC)[1] = ptr1[1];
                                (dataPtr + y * wS + x * nC)[2] = ptr1[2];

                            }
                            else
                            {
                                (dataPtr + y * wS + x * nC)[0] = 0;
                                (dataPtr + y * wS + x * nC)[1] = 0;
                                (dataPtr + y * wS + x * nC)[2] = 0;
                            }
                        }
                    }
                }
            }
        }


        public static void Scale_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int padding = wS - nC * width;
                int x, y;
                double b1, b2, b3, g1, g2, g3, r1, r2, r3;
                byte* ptr1, ptr2, ptr3, ptr4;
                double x0, y0, x_decimal, y_decimal;

                if (nC == 3)
                {  // image in RGB
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //new x and y
                            x0 = (double)(x / scaleFactor);
                            y0 = (double)(y / scaleFactor);

                            if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height)
                            {
                                //BILINEAR METHOD
                                //get the nearest 4 pixels
                                ptr1 = (dataPtrC + (int)Math.Floor(y0) * wS + (int)Math.Floor(x0) * nC);
                                ptr2 = (dataPtrC + (int)Math.Floor(y0) * wS + (int)Math.Ceiling(x0) * nC);
                                ptr3 = (dataPtrC + (int)Math.Ceiling(y0) * wS + (int)Math.Floor(x0) * nC);
                                ptr4 = (dataPtrC + (int)Math.Ceiling(y0) * wS + (int)Math.Ceiling(x0) * nC);
                                //retrieve decimal part of the x0, y0
                                x_decimal = x0 % 1;
                                y_decimal = y0 % 1;

                                //get pixel values and calculate x values
                                b1 = (double)(ptr1[0] + x_decimal * (ptr2[0] - ptr1[0]));
                                g1 = (double)(ptr1[1] + x_decimal * (ptr2[1] - ptr1[1]));
                                r1 = (double)(ptr1[2] + x_decimal * (ptr2[2] - ptr1[2]));

                                b2 = (double)(ptr3[0] + x_decimal * (ptr4[0] - ptr3[0]));
                                g2 = (double)(ptr3[1] + x_decimal * (ptr4[1] - ptr3[1]));
                                r2 = (double)(ptr3[2] + x_decimal * (ptr4[2] - ptr3[2]));

                                b3 = (double)(b1 + y_decimal * (b2 - b1));
                                g3 = (double)(g1 + y_decimal * (g2 - g1));
                                r3 = (double)(r1 + y_decimal * (r2 - r1));

                                (dataPtr + y * wS + x * nC)[0] = (byte)b3;
                                (dataPtr + y * wS + x * nC)[1] = (byte)g3;
                                (dataPtr + y * wS + x * nC)[2] = (byte)r3;

                            }
                            else
                            {
                                (dataPtr + y * wS + x * nC)[0] = 0;
                                (dataPtr + y * wS + x * nC)[1] = 0;
                                (dataPtr + y * wS + x * nC)[2] = 0;
                            }
                        }
                    }
                }
            }
        }

        public static void Scale_point_xy(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int centerX, int centerY)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();
                byte* ptr1;
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int padding = wS - nC * width;
                int x, y, x0, y0;
                float w2 = (width / 2) / (scaleFactor), h2 = (height / 2) / (scaleFactor);

                if (nC == 3)
                {  // image in RGB
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //new x and y
                            x0 = (int)Math.Round((x / scaleFactor) + centerX - w2);
                            y0 = (int)Math.Round((y / scaleFactor) + centerY - h2);

                            if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height)
                            {
                                ptr1 = (dataPtrC + y0 * wS + x0 * nC); //point to pixel
                                //store in the image 
                                (dataPtr + y * wS + x * nC)[0] = ptr1[0];
                                (dataPtr + y * wS + x * nC)[1] = ptr1[1];
                                (dataPtr + y * wS + x * nC)[2] = ptr1[2];

                            }
                            else
                            {
                                (dataPtr + y * wS + x * nC)[0] = 0;
                                (dataPtr + y * wS + x * nC)[1] = 0;
                                (dataPtr + y * wS + x * nC)[2] = 0;
                            }
                            //dataPtr += nC; //point to the next pixel
                        }
                        //dataPtr += padding; //allignment padding out
                    }
                }
            }
        }

        public static void Scale_point_xy_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int centerX, int centerY)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int padding = wS - nC * width;
                int x, y;
                double b1, b2, b3, g1, g2, g3, r1, r2, r3;
                byte* ptr1, ptr2, ptr3, ptr4;
                double x0, y0, x_decimal, y_decimal;
                float w2 = (width / 2) / (scaleFactor), h2 = (height / 2) / (scaleFactor);

                if (nC == 3)
                {  // image in RGB
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //new x and y
                            x0 = (int)Math.Round((x / scaleFactor) + centerX - w2);
                            y0 = (int)Math.Round((y / scaleFactor) + centerY - h2);

                            if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height)
                            {
                                //BILINEAR METHOD
                                //get the nearest 4 pixels
                                ptr1 = (dataPtrC + (int)Math.Floor(y0) * wS + (int)Math.Floor(x0) * nC);
                                ptr2 = (dataPtrC + (int)Math.Floor(y0) * wS + (int)Math.Ceiling(x0) * nC);
                                ptr3 = (dataPtrC + (int)Math.Ceiling(y0) * wS + (int)Math.Floor(x0) * nC);
                                ptr4 = (dataPtrC + (int)Math.Ceiling(y0) * wS + (int)Math.Ceiling(x0) * nC);
                                //retrieve decimal part of the x0, y0
                                x_decimal = x0 % 1;
                                y_decimal = y0 % 1;

                                //get pixel values and calculate x values
                                b1 = (double)(ptr1[0] + x_decimal * (ptr2[0] - ptr1[0]));
                                g1 = (double)(ptr1[1] + x_decimal * (ptr2[1] - ptr1[1]));
                                r1 = (double)(ptr1[2] + x_decimal * (ptr2[2] - ptr1[2]));

                                b2 = (double)(ptr3[0] + x_decimal * (ptr4[0] - ptr3[0]));
                                g2 = (double)(ptr3[1] + x_decimal * (ptr4[1] - ptr3[1]));
                                r2 = (double)(ptr3[2] + x_decimal * (ptr4[2] - ptr3[2]));

                                b3 = (double)(b1 + y_decimal * (b2 - b1));
                                g3 = (double)(g1 + y_decimal * (g2 - g1));
                                r3 = (double)(r1 + y_decimal * (r2 - r1));

                                (dataPtr + y * wS + x * nC)[0] = (byte)b3;
                                (dataPtr + y * wS + x * nC)[1] = (byte)g3;
                                (dataPtr + y * wS + x * nC)[2] = (byte)r3;

                            }
                            else
                            {
                                (dataPtr + y * wS + x * nC)[0] = 0;
                                (dataPtr + y * wS + x * nC)[1] = 0;
                                (dataPtr + y * wS + x * nC)[2] = 0;
                            }
                        }
                    }
                }
            }
        }


        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();

                byte* ptr1 = null;

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int padding = wS - nC * width;
                int x = 0, y = 0;
                int red, green, blue;

                if (nC == 3)
                {  // image in RGB

                   //for border pixels
                   //1 - for particular cases: border points
                   //x = y = 0
                    blue = (int)Math.Round((dataPtrC[0] * 4 + (dataPtrC + wS)[0] * 2 + (dataPtrC + nC)[0] * 2 + (dataPtrC + wS + nC)[0]) / 9.0);
                    green = (int)Math.Round((dataPtrC[1] * 4 + (dataPtrC + wS)[1] * 2 + (dataPtrC + nC)[1] * 2 + (dataPtrC + wS + nC)[1]) / 9.0);
                    red = (int)Math.Round((dataPtrC[2] * 4 + (dataPtrC + wS)[2] * 2 + (dataPtrC + nC)[2] * 2 + (dataPtrC + wS + nC)[2]) / 9.0);
                    if (blue > 255)
                        blue = 255;
                    if (green > 255)
                        green = 255;
                    if (red > 255)
                        red = 255;
                    dataPtr[0] = (byte)blue;
                    dataPtr[1] = (byte)green;
                    dataPtr[2] = (byte)red;

                    //x = 0 and y = height
                    ptr1 = (dataPtrC + (height - 1) * wS);
                    blue = (int)Math.Round((ptr1[0] * 4 + (ptr1 - wS)[0] * 2 + (ptr1 + nC)[0] * 2 + (ptr1 - wS + nC)[0]) / 9.0);
                    green = (int)Math.Round((ptr1[1] * 4 + (ptr1 - wS)[1] * 2 + (ptr1 + nC)[1] * 2 + (ptr1 - wS + nC)[1]) / 9.0);
                    red = (int)Math.Round((ptr1[2] * 4 + (ptr1 - wS)[2] * 2 + (ptr1 + nC)[2] * 2 + (ptr1 - wS + nC)[2]) / 9.0);
                    if (blue > 255)
                        blue = 255;
                    if (green > 255)
                        green = 255;
                    if (red > 255)
                        red = 255;
                    (dataPtr + (height - 1) * wS)[0] = (byte)blue;
                    (dataPtr + (height - 1) * wS)[1] = (byte)green;
                    (dataPtr + (height - 1) * wS)[2] = (byte)red;

                    //x = width and y = height
                    ptr1 = (dataPtrC + (height - 1) * wS + (width - 1) * nC);
                    blue = (int)Math.Round((ptr1[0] * 4 + (ptr1 - wS)[0] * 2 + (ptr1 - nC)[0] * 2 + (ptr1 - wS - nC)[0]) / 9.0);
                    green = (int)Math.Round((ptr1[1] * 4 + (ptr1 - wS)[1] * 2 + (ptr1 - nC)[1] * 2 + (ptr1 - wS - nC)[1]) / 9.0);
                    red = (int)Math.Round((ptr1[2] * 4 + (ptr1 - wS)[2] * 2 + (ptr1 - nC)[2] * 2 + (ptr1 - wS - nC)[2]) / 9.0);
                    if (blue > 255)
                        blue = 255;
                    if (green > 255)
                        green = 255;
                    if (red > 255)
                        red = 255;
                    (dataPtr + (height - 1) * wS + (width - 1) * nC)[0] = (byte)blue;
                    (dataPtr + (height - 1) * wS + (width - 1) * nC)[1] = (byte)green;
                    (dataPtr + (height - 1) * wS + (width - 1) * nC)[2] = (byte)red;

                    //x = width and y = 0
                    ptr1 = (dataPtrC + (width - 1) * nC);
                    blue = (int)Math.Round((ptr1[0] * 4 + (ptr1 + wS)[0] * 2 + (ptr1 - nC)[0] * 2 + (ptr1 + wS - nC)[0]) / 9.0);
                    green = (int)Math.Round((ptr1[1] * 4 + (ptr1 + wS)[1] * 2 + (ptr1 - nC)[1] * 2 + (ptr1 + wS - nC)[1]) / 9.0);
                    red = (int)Math.Round((ptr1[1] * 4 + (ptr1 + wS)[2] * 2 + (ptr1 - nC)[2] * 2 + (ptr1 + wS - nC)[2]) / 9.0);
                    if (blue > 255)
                        blue = 255;
                    if (green > 255)
                        green = 255;
                    if (red > 255)
                        red = 255;
                    (dataPtr + (width - 1) * nC)[0] = (byte)blue;
                    (dataPtr + (width - 1) * nC)[1] = (byte)green;
                    (dataPtr + (width - 1) * nC)[2] = (byte)red;

                    //2 - for border other cases
                    //line top: y = 0
                    for (x = 1; x < width - 1; x++)
                    {
                        ptr1 = (dataPtrC + x * nC);
                        blue = (int)Math.Round(((ptr1 - nC)[0] * 2 + ptr1[0] * 2 + (ptr1 + nC)[0] * 2 + (ptr1 + wS - nC)[0] + (ptr1 + wS)[0] + (ptr1 + wS + nC)[0]) / 9.0);
                        green = (int)Math.Round(((ptr1 - nC)[1] * 2 + ptr1[1] * 2 + (ptr1 + nC)[1] * 2 + (ptr1 + wS - nC)[1] + (ptr1 + wS)[1] + (ptr1 + wS + nC)[1]) / 9.0);
                        red = (int)Math.Round(((ptr1 - nC)[2] * 2 + ptr1[2] * 2 + (ptr1 + nC)[2] * 2 + (ptr1 + wS - nC)[2] + (ptr1 + wS)[2] + (ptr1 + wS + nC)[2]) / 9.0);
                        if (blue > 255)
                            blue = 255;
                        if (green > 255)
                            green = 255;
                        if (red > 255)
                            red = 255;
                        (dataPtr + x * nC)[0] = (byte)blue;
                        (dataPtr + x * nC)[1] = (byte)green;
                        (dataPtr + x * nC)[2] = (byte)red;
                    }
                    //bottom line: y = height
                    for (x = 1; x < width - 1; x++)
                    {
                        ptr1 = (dataPtrC + (height - 1) * wS + x * nC);
                        blue = (int)Math.Round(((ptr1 - nC)[0] * 2 + ptr1[0] * 2 + (ptr1 + nC)[0] * 2 + (ptr1 - wS - nC)[0] + (ptr1 - wS)[0] + (ptr1 - wS + nC)[0]) / 9.0);
                        green = (int)Math.Round(((ptr1 - nC)[1] * 2 + ptr1[1] * 2 + (ptr1 + nC)[1] * 2 + (ptr1 - wS - nC)[1] + (ptr1 - wS)[1] + (ptr1 - wS + nC)[1]) / 9.0);
                        red = (int)Math.Round(((ptr1 - nC)[2] * 2 + ptr1[2] * 2 + (ptr1 + nC)[2] * 2 + (ptr1 - wS - nC)[2] + (ptr1 - wS)[2] + (ptr1 - wS + nC)[2]) / 9.0);
                        if (blue > 255)
                            blue = 255;
                        if (green > 255)
                            green = 255;
                        if (red > 255)
                            red = 255;

                        (dataPtr + (height - 1) * wS + x * nC)[0] = (byte)blue;
                        (dataPtr + (height - 1) * wS + x * nC)[1] = (byte)green;
                        (dataPtr + (height - 1) * wS + x * nC)[2] = (byte)red;
                    }
                    //left line: x = 0
                    for (y = 1; y < height - 1; y++)
                    {
                        ptr1 = (dataPtrC + y * wS);
                        blue = (int)Math.Round(((ptr1 - wS)[0] * 2 + ptr1[0] * 2 + (ptr1 + wS)[0] * 2 + (ptr1 - wS + nC)[0] + (ptr1 + nC)[0] + (ptr1 + wS + nC)[0]) / 9.0);
                        green = (int)Math.Round(((ptr1 - wS)[1] * 2 + ptr1[1] * 2 + (ptr1 + wS)[1] * 2 + (ptr1 - wS + nC)[1] + (ptr1 + nC)[1] + (ptr1 + wS + nC)[1]) / 9.0);
                        red = (int)Math.Round(((ptr1 - wS)[2] * 2 + ptr1[2] * 2 + (ptr1 + wS)[2] * 2 + (ptr1 - wS + nC)[2] + (ptr1 + nC)[2] + (ptr1 + wS + nC)[2]) / 9.0);
                        if (blue > 255)
                            blue = 255;
                        if (green > 255)
                            green = 255;
                        if (red > 255)
                            red = 255;

                        (dataPtr + y * wS)[0] = (byte)blue;
                        (dataPtr + y * wS)[1] = (byte)green;
                        (dataPtr + y * wS)[2] = (byte)red;
                    }
                    //right line: x = width
                    for (y = 1; y < height - 1; y++)
                    {
                        ptr1 = (dataPtrC + y * wS + (width - 1) * nC);
                        blue = (int)Math.Round(((ptr1 - wS)[0] * 2 + ptr1[0] * 2 + (ptr1 + wS)[0] * 2 + (ptr1 - wS - nC)[0] + (ptr1 - nC)[0] + (ptr1 + wS - nC)[0]) / 9.0);
                        green = (int)Math.Round(((ptr1 - wS)[1] * 2 + ptr1[1] * 2 + (ptr1 + wS)[1] * 2 + (ptr1 - wS - nC)[1] + (ptr1 - nC)[1] + (ptr1 + wS - nC)[1]) / 9.0);
                        red = (int)Math.Round(((ptr1 - wS)[2] * 2 + ptr1[2] * 2 + (ptr1 + wS)[2] * 2 + (ptr1 - wS - nC)[2] + (ptr1 - nC)[2] + (ptr1 + wS - nC)[2]) / 9.0);
                        if (blue > 255)
                            blue = 255;
                        if (green > 255)
                            green = 255;
                        if (red > 255)
                            red = 255;
                        (dataPtr + y * wS + (width - 1) * nC)[0] = (byte)blue;
                        (dataPtr + y * wS + (width - 1) * nC)[1] = (byte)green;
                        (dataPtr + y * wS + (width - 1) * nC)[2] = (byte)red;
                    }

                    //for center pixels
                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 1; x < width - 1; x++)
                        {
                            ptr1 = (dataPtrC + y * wS + x * nC);

                            blue = 0;
                            green = 0;
                            red = 0;

                            for (int i = -1; i < 2; i++)
                            {
                                for (int j = -1; j < 2; j++)
                                {
                                    blue += (ptr1 + i * wS + j * nC)[0];
                                    green += (ptr1 + i * wS + j * nC)[1];
                                    red += (ptr1 + i * wS + j * nC)[2];
                                }
                            }

                            blue = (int)Math.Round(blue / 9.0);
                            green = (int)Math.Round(green / 9.0);
                            red = (int)Math.Round(red / 9.0);

                            if (blue > 255)
                                blue = 255;
                            if (green > 255)
                                green = 255;
                            if (red > 255)
                                red = 255;

                            (dataPtr + y * wS + x * nC)[0] = (byte)blue;
                            (dataPtr + y * wS + x * nC)[1] = (byte)green;
                            (dataPtr + y * wS + x * nC)[2] = (byte)red;
                        }
                    }
                }
            }
        }

        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight, float offset)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();

                byte* ptr1 = null;

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int padding = wS - nC * width;
                int x, y;
                int red, green, blue;

                if (nC == 3)
                {  // image in RGB
                   //for border pixels
                   //1 - for particular cases: border points
                   //x = y = 0
                    blue = (int)Math.Round(((dataPtrC[0] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) + (dataPtrC + wS)[0] * (matrix[0, 2] + matrix[1, 2]) + (dataPtrC + nC)[0] * (matrix[2, 1] + matrix[2, 0]) + (dataPtrC + wS + nC)[0] * matrix[2, 2]) / matrixWeight) + offset);
                    green = (int)Math.Round(((dataPtrC[1] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) + (dataPtrC + wS)[1] * (matrix[0, 2] + matrix[1, 2]) + (dataPtrC + nC)[1] * (matrix[2, 1] + matrix[2, 0]) + (dataPtrC + wS + nC)[1] * matrix[2, 2]) / matrixWeight) + offset);
                    red = (int)Math.Round(((dataPtrC[2] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) + (dataPtrC + wS)[2] * (matrix[0, 2] + matrix[1, 2]) + (dataPtrC + nC)[2] * (matrix[2, 1] + matrix[2, 0]) + (dataPtrC + wS + nC)[2] * matrix[2, 2]) / matrixWeight) + offset);

                    //limit the values
                    if (red < 0)
                        red = 0;
                    if (red > 255)
                        red = 255;
                    if (green < 0)
                        green = 0;
                    if (green > 255)
                        green = 255;
                    if (blue < 0)
                        blue = 0;
                    if (blue > 255)
                        blue = 255;

                    dataPtr[0] = (byte)blue;
                    dataPtr[1] = (byte)green;
                    dataPtr[2] = (byte)red;

                    //x = 0 and y = height
                    ptr1 = (dataPtrC + (height - 1) * wS);
                    blue = (int)Math.Round(((ptr1[0] * (matrix[1, 1] + matrix[0, 1] + matrix[0, 2] + matrix[1, 2]) + (ptr1 - wS)[0] * (matrix[0, 0] + matrix[1, 0]) + (ptr1 + nC)[0] * (matrix[2, 1] + matrix[2, 2]) + (ptr1 - wS + nC)[0] * matrix[2, 0]) / matrixWeight) + offset);
                    green = (int)Math.Round(((ptr1[1] * (matrix[1, 1] + matrix[0, 1] + matrix[0, 2] + matrix[1, 2]) + (ptr1 - wS)[1] * (matrix[0, 0] + matrix[1, 0]) + (ptr1 + nC)[1] * (matrix[2, 1] + matrix[2, 2]) + (ptr1 - wS + nC)[1] * matrix[2, 0]) / matrixWeight) + offset);
                    red = (int)Math.Round(((ptr1[2] * (matrix[1, 1] + matrix[0, 1] + matrix[0, 2] + matrix[1, 2]) + (ptr1 - wS)[2] * (matrix[0, 0] + matrix[1, 0]) + (ptr1 + nC)[2] * (matrix[2, 1] + matrix[2, 2]) + (ptr1 - wS + nC)[2] * matrix[2, 0]) / matrixWeight) + offset);

                    //limit the values
                    if (red < 0)
                        red = 0;
                    if (red > 255)
                        red = 255;
                    if (green < 0)
                        green = 0;
                    if (green > 255)
                        green = 255;
                    if (blue < 0)
                        blue = 0;
                    if (blue > 255)
                        blue = 255;

                    (dataPtr + (height - 1) * wS)[0] = (byte)blue;
                    (dataPtr + (height - 1) * wS)[1] = (byte)green;
                    (dataPtr + (height - 1) * wS)[2] = (byte)red;

                    //x = width and y = height
                    ptr1 = (dataPtrC + (height - 1) * wS + (width - 1) * nC);
                    blue = (int)Math.Round(((ptr1[0] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]) + (ptr1 - wS)[0] * (matrix[1, 0] + matrix[2, 0]) + (ptr1 - nC)[0] * (matrix[0, 1] + matrix[0, 2]) + (ptr1 - wS - nC)[0] * matrix[0, 0]) / matrixWeight) + offset);
                    green = (int)Math.Round(((ptr1[1] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]) + (ptr1 - wS)[1] * (matrix[1, 0] + matrix[2, 0]) + (ptr1 - nC)[1] * (matrix[0, 1] + matrix[0, 2]) + (ptr1 - wS - nC)[1] * matrix[0, 0]) / matrixWeight) + offset);
                    red = (int)Math.Round(((ptr1[2] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]) + (ptr1 - wS)[2] * (matrix[1, 0] + matrix[2, 0]) + (ptr1 - nC)[2] * (matrix[0, 1] + matrix[0, 2]) + (ptr1 - wS - nC)[2] * matrix[0, 0]) / matrixWeight) + offset);

                    //limit the values
                    if (red < 0)
                        red = 0;
                    if (red > 255)
                        red = 255;
                    if (green < 0)
                        green = 0;
                    if (green > 255)
                        green = 255;
                    if (blue < 0)
                        blue = 0;
                    if (blue > 255)
                        blue = 255;

                    (dataPtr + (height - 1) * wS + (width - 1) * nC)[0] = (byte)blue;
                    (dataPtr + (height - 1) * wS + (width - 1) * nC)[1] = (byte)green;
                    (dataPtr + (height - 1) * wS + (width - 1) * nC)[2] = (byte)red;

                    //x = width and y = 0
                    ptr1 = (dataPtrC + (width - 1) * nC);
                    blue = (int)Math.Round(((ptr1[0] * (matrix[1, 0] + matrix[2, 0] + matrix[1, 1] + matrix[2, 1]) + (ptr1 + wS)[0] * (matrix[1, 2] + matrix[2, 2]) + (ptr1 - nC)[0] * (matrix[0, 0] + matrix[1, 0]) + (ptr1 + wS - nC)[0] * matrix[0, 2]) / matrixWeight) + offset);
                    green = (int)Math.Round(((ptr1[1] * (matrix[1, 0] + matrix[2, 0] + matrix[1, 1] + matrix[2, 1]) + (ptr1 + wS)[1] * (matrix[1, 2] + matrix[2, 2]) + (ptr1 - nC)[1] * (matrix[0, 0] + matrix[1, 0]) + (ptr1 + wS - nC)[1] * matrix[0, 2]) / matrixWeight) + offset);
                    red = (int)Math.Round(((ptr1[1] * (matrix[1, 0] + matrix[2, 0] + matrix[1, 1] + matrix[2, 1]) + (ptr1 + wS)[2] * (matrix[1, 2] + matrix[2, 2]) + (ptr1 - nC)[2] * (matrix[0, 0] + matrix[1, 0]) + (ptr1 + wS - nC)[2] * matrix[0, 2]) / matrixWeight) + offset);

                    //limit the values
                    if (red < 0)
                        red = 0;
                    if (red > 255)
                        red = 255;
                    if (green < 0)
                        green = 0;
                    if (green > 255)
                        green = 255;
                    if (blue < 0)
                        blue = 0;
                    if (blue > 255)
                        blue = 255;

                    (dataPtr + (width - 1) * nC)[0] = (byte)blue;
                    (dataPtr + (width - 1) * nC)[1] = (byte)green;
                    (dataPtr + (width - 1) * nC)[2] = (byte)red;

                    //2 - for border other cases
                    //line top: y = 0
                    for (x = 1; x < width - 1; x++)
                    {
                        ptr1 = (dataPtrC + x * nC);
                        blue = (int)Math.Round((((ptr1 - nC)[0] * (matrix[0, 0] + matrix[0, 1]) + ptr1[0] * (matrix[1, 0] + matrix[1, 1]) + (ptr1 + nC)[0] * (matrix[2, 0] + matrix[2, 1]) + (ptr1 + wS - nC)[0] * matrix[0, 2] + (ptr1 + wS)[0] * matrix[1, 2] + (ptr1 + wS + nC)[0] * matrix[2, 2]) / matrixWeight) + offset);
                        green = (int)Math.Round((((ptr1 - nC)[1] * (matrix[0, 0] + matrix[0, 1]) + ptr1[1] * (matrix[1, 0] + matrix[1, 1]) + (ptr1 + nC)[1] * (matrix[2, 0] + matrix[2, 1]) + (ptr1 + wS - nC)[1] * matrix[0, 2] + (ptr1 + wS)[1] * matrix[1, 2] + (ptr1 + wS + nC)[1] * matrix[2, 2]) / matrixWeight) + offset);
                        red = (int)Math.Round((((ptr1 - nC)[2] * (matrix[0, 0] + matrix[0, 1]) + ptr1[2] * (matrix[1, 0] + matrix[1, 1]) + (ptr1 + nC)[2] * (matrix[2, 0] + matrix[2, 1]) + (ptr1 + wS - nC)[2] * matrix[0, 2] + (ptr1 + wS)[2] * matrix[1, 2] + (ptr1 + wS + nC)[2] * matrix[2, 2]) / matrixWeight) + offset);

                        //limit the values
                        if (red < 0)
                            red = 0;
                        if (red > 255)
                            red = 255;
                        if (green < 0)
                            green = 0;
                        if (green > 255)
                            green = 255;
                        if (blue < 0)
                            blue = 0;
                        if (blue > 255)
                            blue = 255;

                        (dataPtr + x * nC)[0] = (byte)blue;
                        (dataPtr + x * nC)[1] = (byte)green;
                        (dataPtr + x * nC)[2] = (byte)red;
                    }
                    //bottom line: y = height
                    for (x = 1; x < width - 1; x++)
                    {
                        ptr1 = (dataPtrC + (height - 1) * wS + x * nC);
                        blue = (int)Math.Round((((ptr1 - nC)[0] * (matrix[0, 1] + matrix[0, 2]) + ptr1[0] * (matrix[1, 1] + matrix[1, 2]) + (ptr1 + nC)[0] * (matrix[2, 1] + matrix[2, 2]) + (ptr1 - wS - nC)[0] * matrix[0, 0] + (ptr1 - wS)[0] * matrix[1, 0] + (ptr1 - wS + nC)[0] * matrix[2, 0]) / matrixWeight) + offset);
                        green = (int)Math.Round((((ptr1 - nC)[1] * (matrix[0, 1] + matrix[0, 2]) + ptr1[1] * (matrix[1, 1] + matrix[1, 2]) + (ptr1 + nC)[1] * (matrix[2, 1] + matrix[2, 2]) + (ptr1 - wS - nC)[1] * matrix[0, 0] + (ptr1 - wS)[1] * matrix[1, 0] + (ptr1 - wS + nC)[1] * matrix[2, 0]) / matrixWeight) + offset);
                        red = (int)Math.Round((((ptr1 - nC)[2] * (matrix[0, 1] + matrix[0, 2]) + ptr1[2] * (matrix[1, 1] + matrix[1, 2]) + (ptr1 + nC)[2] * (matrix[2, 1] + matrix[2, 2]) + (ptr1 - wS - nC)[2] * matrix[0, 0] + (ptr1 - wS)[2] * matrix[1, 0] + (ptr1 - wS + nC)[2] * matrix[2, 0]) / matrixWeight) + offset);

                        //limit the values
                        if (red < 0)
                            red = 0;
                        if (red > 255)
                            red = 255;
                        if (green < 0)
                            green = 0;
                        if (green > 255)
                            green = 255;
                        if (blue < 0)
                            blue = 0;
                        if (blue > 255)
                            blue = 255;

                        (dataPtr + (height - 1) * wS + x * nC)[0] = (byte)blue;
                        (dataPtr + (height - 1) * wS + x * nC)[1] = (byte)green;
                        (dataPtr + (height - 1) * wS + x * nC)[2] = (byte)red;
                    }
                    //left line: x = 0
                    for (y = 1; y < height - 1; y++)
                    {
                        ptr1 = (dataPtrC + y * wS);
                        blue = (int)Math.Round((((ptr1 - wS)[0] * (matrix[0, 0] + matrix[1, 0]) + ptr1[0] * (matrix[0, 1] + matrix[1, 1]) + (ptr1 + wS)[0] * (matrix[0, 2] + matrix[1, 2]) + (ptr1 - wS + nC)[0] * matrix[2, 0] + (ptr1 + nC)[0] * matrix[2, 1] + (ptr1 + wS + nC)[0] * matrix[2, 2]) / matrixWeight) + offset);
                        green = (int)Math.Round((((ptr1 - wS)[1] * (matrix[0, 0] + matrix[1, 0]) + ptr1[1] * (matrix[0, 1] + matrix[1, 1]) + (ptr1 + wS)[1] * (matrix[0, 2] + matrix[1, 2]) + (ptr1 - wS + nC)[1] * matrix[2, 0] + (ptr1 + nC)[1] * matrix[2, 1] + (ptr1 + wS + nC)[1] * matrix[2, 2]) / matrixWeight) + offset);
                        red = (int)Math.Round((((ptr1 - wS)[2] * (matrix[0, 0] + matrix[1, 0]) + ptr1[2] * (matrix[0, 1] + matrix[1, 1]) + (ptr1 + wS)[2] * (matrix[0, 2] + matrix[1, 2]) + (ptr1 - wS + nC)[2] * matrix[2, 0] + (ptr1 + nC)[2] * matrix[2, 1] + (ptr1 + wS + nC)[2] * matrix[2, 2]) / matrixWeight) + offset);

                        //limit the values
                        if (red < 0)
                            red = 0;
                        if (red > 255)
                            red = 255;
                        if (green < 0)
                            green = 0;
                        if (green > 255)
                            green = 255;
                        if (blue < 0)
                            blue = 0;
                        if (blue > 255)
                            blue = 255;

                        (dataPtr + y * wS)[0] = (byte)blue;
                        (dataPtr + y * wS)[1] = (byte)green;
                        (dataPtr + y * wS)[2] = (byte)red;
                    }
                    //right line: x = width
                    for (y = 1; y < height - 1; y++)
                    {
                        ptr1 = (dataPtrC + y * wS + (width - 1) * nC);
                        blue = (int)Math.Round((((ptr1 - wS)[0] * (matrix[1, 0] + matrix[2, 0]) + ptr1[0] * (matrix[1, 1] + matrix[2, 1]) + (ptr1 + wS)[0] * (matrix[1, 2] + matrix[2, 2]) + (ptr1 - wS - nC)[0] * matrix[0, 0] + (ptr1 - nC)[0] * matrix[0, 1] + (ptr1 + wS - nC)[0] * matrix[0, 2]) / matrixWeight) + offset);
                        green = (int)Math.Round((((ptr1 - wS)[1] * (matrix[1, 0] + matrix[2, 0]) + ptr1[1] * (matrix[1, 1] + matrix[2, 1]) + (ptr1 + wS)[1] * (matrix[1, 2] + matrix[2, 2]) + (ptr1 - wS - nC)[1] * matrix[0, 0] + (ptr1 - nC)[1] * matrix[0, 1] + (ptr1 + wS - nC)[1] * matrix[0, 2]) / matrixWeight) + offset);
                        red = (int)Math.Round((((ptr1 - wS)[2] * (matrix[1, 0] + matrix[2, 0]) + ptr1[2] * (matrix[1, 1] + matrix[2, 1]) + (ptr1 + wS)[2] * (matrix[1, 2] + matrix[2, 2]) + (ptr1 - wS - nC)[2] * matrix[0, 0] + (ptr1 - nC)[2] * matrix[0, 1] + (ptr1 + wS - nC)[2] * matrix[0, 2]) / matrixWeight) + offset);

                        //limit the values
                        if (red < 0)
                            red = 0;
                        if (red > 255)
                            red = 255;
                        if (green < 0)
                            green = 0;
                        if (green > 255)
                            green = 255;
                        if (blue < 0)
                            blue = 0;
                        if (blue > 255)
                            blue = 255;

                        (dataPtr + y * wS + (width - 1) * nC)[0] = (byte)blue;
                        (dataPtr + y * wS + (width - 1) * nC)[1] = (byte)green;
                        (dataPtr + y * wS + (width - 1) * nC)[2] = (byte)red;
                    }

                    //for center pixels
                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 1; x < width - 1; x++)
                        {
                            ptr1 = dataPtrC + y * wS + x * nC;
                            red = 0;
                            green = 0;
                            blue = 0;

                            //to sum up pixels
                            for (int i = -1; i < 2; i++)
                            {
                                for (int j = -1; j < 2; j++)
                                {
                                    blue += (int)((ptr1 + j * wS + i * nC)[0] * matrix[i + 1, j + 1]);
                                    green += (int)((ptr1 + j * wS + i * nC)[1] * matrix[i + 1, j + 1]);
                                    red += (int)((ptr1 + j * wS + i * nC)[2] * matrix[i + 1, j + 1]);
                                }
                            }

                            //divide by total weight
                            blue = (int)Math.Round((blue / matrixWeight) + offset);
                            green = (int)Math.Round((green / matrixWeight) + offset);
                            red = (int)Math.Round((red / matrixWeight) + offset);

                            //limit the values
                            if (red < 0)
                                red = 0;
                            if (red > 255)
                                red = 255;
                            if (green < 0)
                                green = 0;
                            if (green > 255)
                                green = 255;
                            if (blue < 0)
                                blue = 0;
                            if (blue > 255)
                                blue = 255;

                            //put in location
                            (dataPtr + y * wS + x * nC)[0] = (byte)blue;
                            (dataPtr + y * wS + x * nC)[1] = (byte)green;
                            (dataPtr + y * wS + x * nC)[2] = (byte)red;
                        }
                    }
                }
            }
        }
        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();

                byte* ptr1 = null;

                int w = img.Width;
                int h = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int padding = wS - nC * w;
                int x, y;
                int red, green, blue, redx, greenx, bluex, redy, greeny, bluey;
                int[] arr = new int[3] { 1, 2, 1 };

                if (nC == 3)
                {  // image in RGB
                   //for center pixels
                    for (y = 1; y < h - 1; y++)
                    {
                        for (x = 1; x < w - 1; x++)
                        {
                            ptr1 = dataPtrC + y * wS + x * nC;
                            redx = 0;
                            greenx = 0;
                            bluex = 0;
                            bluey = 0;
                            greeny = 0;
                            redy = 0;

                            //Sx = (a+2d+g)-(c+2f+i) with y+1 and y-1
                            for (int j = -1; j < 2; j++)
                            {
                                for (int i = -1; i < 2; i++)
                                {
                                    if (j == 0)
                                        continue;
                                    bluex += (int)((ptr1 + i * wS + j * nC)[0] * -j * arr[i + 1]);
                                    greenx += (int)((ptr1 + i * wS + j * nC)[1] * -j * arr[i + 1]);
                                    redx += (int)((ptr1 + i * wS + j * nC)[2] * -j * arr[i + 1]);
                                }
                            }

                            //Sy = (g+2h+i)-(a+2b+c) with x+1 and x-1
                            for (int j = -1; j < 2; j++)
                            {
                                for (int i = -1; i < 2; i++)
                                {
                                    if (j == 0)
                                        continue;
                                    bluey += (int)((ptr1 + j * wS + i * nC)[0] * j * arr[i + 1]);
                                    greeny += (int)((ptr1 + j * wS + i * nC)[1] * j * arr[i + 1]);
                                    redy += (int)((ptr1 + j * wS + i * nC)[2] * j * arr[i + 1]);
                                }
                            }

                            blue = Math.Abs(bluex) + Math.Abs(bluey);
                            green = Math.Abs(greenx) + Math.Abs(greeny);
                            red = Math.Abs(redx) + Math.Abs(redy);

                            if (red > 255)
                                red = 255;
                            if (green > 255)
                                green = 255;
                            if (blue > 255)
                                blue = 255;

                            //put in location
                            (dataPtr + y * wS + x * nC)[0] = (byte)blue;
                            (dataPtr + y * wS + x * nC)[1] = (byte)green;
                            (dataPtr + y * wS + x * nC)[2] = (byte)red;
                        }
                    }

                    //METHOD: DUPLICATE borders
                    //top side
                    for(x = 1; x < w - 1; x++)
                    {
                        ptr1 = dataPtrC + x * nC;

                        //Sx = (a+2d+g)-(c+2f+i) with y+1 and y-1
                        bluex = (int)(3*((ptr1 - nC)[0] - (ptr1 + nC)[0]) + (ptr1 + wS - nC)[0] - (ptr1 + wS + nC)[0]);
                        greenx = (int)(3*((ptr1 - nC)[1] - (ptr1 + nC)[1]) + (ptr1 + wS - nC)[1] - (ptr1 + wS + nC)[1]);
                        redx = (int)(3*((ptr1 - nC)[2] - (ptr1 + nC)[2]) + (ptr1 + wS - nC)[2] - (ptr1 + wS + nC)[2]);

                        //Sy = (g+2h+i)-(a+2b+c) with x+1 and x-1
                        bluey = (int)(2*((ptr1 + wS)[0] - ptr1[0]) + (ptr1 + wS - nC)[0] + (ptr1 + wS + nC)[0] - (ptr1 - nC)[0] - (ptr1 + nC)[0]);
                        greeny = (int)(2*((ptr1 + wS)[1] - ptr1[1]) + (ptr1 + wS - nC)[1] + (ptr1 + wS + nC)[1] - (ptr1 - nC)[1] - (ptr1 + nC)[1]);
                        redy = (int)(2*((ptr1 + wS)[2] - ptr1[2]) + (ptr1 + wS - nC)[2] + (ptr1 + wS + nC)[2] - (ptr1 - nC)[2] - (ptr1 + nC)[2]);

                        blue = Math.Abs(bluex) + Math.Abs(bluey);
                        green = Math.Abs(greenx) + Math.Abs(greeny);
                        red = Math.Abs(redx) + Math.Abs(redy);
                        if (red > 255)
                            red = 255;
                        if (green > 255)
                            green = 255;
                        if (blue > 255)
                            blue = 255;
                        (dataPtr + x * nC)[0] = (byte)blue;
                        (dataPtr + x * nC)[1] = (byte)green;
                        (dataPtr + x * nC)[2] = (byte)red;
                    }
                    //bottom side
                    for (x = 1; x < w - 1; x++)
                    {
                        ptr1 = dataPtrC + (h - 1) * wS + x * nC;

                        //Sx = (a+2d+g)-(c+2f+i) with y+1 and y-1
                        bluex = (int)(3 * ((ptr1 - nC)[0] - (ptr1 + nC)[0]) + (ptr1 - wS - nC)[0] - (ptr1 - wS + nC)[0]);
                        greenx = (int)(3 * ((ptr1 - nC)[1] - (ptr1 + nC)[1]) + (ptr1 - wS - nC)[1] - (ptr1 - wS + nC)[1]);
                        redx = (int)(3 * ((ptr1 - nC)[2] - (ptr1 + nC)[2]) + (ptr1 - wS - nC)[2] - (ptr1 - wS + nC)[2]);

                        //Sy = (g+2h+i)-(a+2b+c) with x+1 and x-1
                        bluey = (int)(2 * (ptr1[0] - (ptr1 - wS)[0]) + (ptr1 - nC)[0] + (ptr1 + nC)[0] - (ptr1 - wS - nC)[0] - (ptr1 - wS + nC)[0]);
                        greeny = (int)(2 * (ptr1[1] - (ptr1 - wS)[1]) + (ptr1 - nC)[1] + (ptr1 + nC)[1] - (ptr1 - wS - nC)[1] - (ptr1 - wS + nC)[1]);
                        redy = (int)(2 * (ptr1[2] - (ptr1 - wS)[2]) + (ptr1 - nC)[2] + (ptr1 + nC)[2] - (ptr1 - wS - nC)[2] - (ptr1 - wS + nC)[2]);

                        blue = Math.Abs(bluex) + Math.Abs(bluey);
                        green = Math.Abs(greenx) + Math.Abs(greeny);
                        red = Math.Abs(redx) + Math.Abs(redy);
                        if (red > 255)
                            red = 255;
                        if (green > 255)
                            green = 255;
                        if (blue > 255)
                            blue = 255;
                        (dataPtr + (h - 1) * wS + x * nC)[0] = (byte)blue;
                        (dataPtr + (h - 1) * wS + x * nC)[1] = (byte)green;
                        (dataPtr + (h - 1) * wS + x * nC)[2] = (byte)red;
                    }
                    //left side
                    for (y = 1; y < h - 1; y++)
                    {
                        ptr1 = dataPtrC + y * wS;

                        //Sx = (a+2d+g)-(c+2f+i) with y+1 and y-1
                        bluex = (int)(2 * (ptr1[0] - (ptr1 + nC)[0]) + (ptr1 - wS)[0] + (ptr1 + wS)[0] - (ptr1 - wS + nC)[0] - (ptr1 + wS + nC)[0]);
                        greenx = (int)(2 * (ptr1[1] - (ptr1 + nC)[1]) + (ptr1 - wS)[1] + (ptr1 + wS)[1] - (ptr1 - wS + nC)[1] - (ptr1 + wS + nC)[1]);
                        redx = (int)(2 * (ptr1[2] - (ptr1 + nC)[2]) + (ptr1 - wS)[2] + (ptr1 + wS)[2] - (ptr1 - wS + nC)[2] - (ptr1 + wS + nC)[2]);

                        //Sy = (g+2h+i)-(a+2b+c) with x+1 and x-1
                        bluey = (int)(3 * ((ptr1 + wS)[0] - (ptr1 - wS)[0]) + (ptr1 + wS + nC)[0] - (ptr1 - wS + nC)[0]);
                        greeny = (int)(3 * ((ptr1 + wS)[1] - (ptr1 - wS)[1]) + (ptr1 + wS + nC)[1] - (ptr1 - wS + nC)[1]);
                        redy = (int)(3 * ((ptr1 + wS)[2] - (ptr1 - wS)[2]) + (ptr1 + wS + nC)[2] - (ptr1 - wS + nC)[2]);

                        blue = Math.Abs(bluex) + Math.Abs(bluey);
                        green = Math.Abs(greenx) + Math.Abs(greeny);
                        red = Math.Abs(redx) + Math.Abs(redy);
                        if (red > 255)
                            red = 255;
                        if (green > 255)
                            green = 255;
                        if (blue > 255)
                            blue = 255;
                        (dataPtr + y * wS)[0] = (byte)blue;
                        (dataPtr + y * wS)[1] = (byte)green;
                        (dataPtr + y * wS)[2] = (byte)red;
                    }
                    //right side
                    for (y = 1; y < h - 1; y++)
                    {
                        ptr1 = dataPtrC + y * wS + (w - 1) * nC;

                        //Sx = (a+2d+g)-(c+2f+i) with y+1 and y-1
                        bluex = (int)(2 * ((ptr1 - nC)[0] - ptr1[0]) + (ptr1 - wS - nC)[0] + (ptr1 + wS - nC)[0] - (ptr1 - wS)[0] - (ptr1 + wS)[0]);
                        greenx = (int)(2 * ((ptr1 - nC)[1] - ptr1[1]) + (ptr1 - wS - nC)[1] + (ptr1 + wS - nC)[1] - (ptr1 - wS)[1] - (ptr1 + wS)[1]);
                        redx = (int)(2 * ((ptr1 - nC)[2] - ptr1[2]) + (ptr1 - wS - nC)[2] + (ptr1 + wS - nC)[2] - (ptr1 - wS)[2] - (ptr1 + wS)[2]);

                        //Sy = (g+2h+i)-(a+2b+c) with x+1 and x-1
                        bluey = (int)(3 * ((ptr1 + wS)[0] - (ptr1 - wS)[0]) + (ptr1 + wS - nC)[0] - (ptr1 - wS - nC)[0]);
                        greeny = (int)(3 * ((ptr1 + wS)[1] - (ptr1 - wS)[1]) + (ptr1 + wS - nC)[1] - (ptr1 - wS - nC)[1]);
                        redy = (int)(3 * ((ptr1 + wS)[2] - (ptr1 - wS)[2]) + (ptr1 + wS - nC)[2] - (ptr1 - wS - nC)[2]);

                        blue = Math.Abs(bluex) + Math.Abs(bluey);
                        green = Math.Abs(greenx) + Math.Abs(greeny);
                        red = Math.Abs(redx) + Math.Abs(redy);
                        if (red > 255)
                            red = 255;
                        if (green > 255)
                            green = 255;
                        if (blue > 255)
                            blue = 255;
                        (dataPtr + y * wS + (w - 1) * nC)[0] = (byte)blue;
                        (dataPtr + y * wS + (w - 1) * nC)[1] = (byte)green;
                        (dataPtr + y * wS + (w - 1) * nC)[2] = (byte)red;
                    }

                    //FOR 4 CORNER
                    //x = y = 0
                    blue = (int)(Math.Abs(3*(dataPtrC[0] - (dataPtrC + nC)[0]))+Math.Abs(3*((dataPtrC + wS)[0] - dataPtrC[0]) + (dataPtrC + wS + nC)[0] - (dataPtrC + nC)[0]));
                    green = (int)(Math.Abs(3*(dataPtrC[1] - (dataPtrC + nC)[1]))+Math.Abs(3*((dataPtrC + wS)[1] - dataPtrC[1]) + (dataPtrC + wS + nC)[1] - (dataPtrC + nC)[1]));
                    red = (int)(Math.Abs(3*(dataPtrC[2] - (dataPtrC + nC)[2]))+Math.Abs(3*((dataPtrC + wS)[2] - dataPtrC[2]) + (dataPtrC + wS + nC)[2] - (dataPtrC + nC)[2]));
                    if (red > 255)
                        red = 255;
                    if (green > 255)
                        green = 255;
                    if (blue > 255)
                        blue = 255;
                    (dataPtr)[0] = (byte)blue;
                    (dataPtr)[1] = (byte)green;
                    (dataPtr)[2] = (byte)red;

                    //x = width, y = 0
                    ptr1 = dataPtrC + (w - 1) * nC; 
                    blue = (int)(Math.Abs(3*((ptr1 - nC)[0] - ptr1[0]) + (ptr1 + wS - nC)[0] - (ptr1 + wS)[0])+Math.Abs(3*((ptr1 + wS)[0] - ptr1[0]) + (ptr1 + wS - nC)[0] - (ptr1 - nC)[0]));
                    green = (int)(Math.Abs(3*((ptr1 - nC)[1] - ptr1[1]) + (ptr1 + wS - nC)[1] - (ptr1 + wS)[1])+Math.Abs(3*((ptr1 + wS)[1] - ptr1[1]) + (ptr1 + wS - nC)[1] - (ptr1 - nC)[1]));
                    red = (int)(Math.Abs(3*((ptr1 - nC)[2] - ptr1[2]) + (ptr1 + wS - nC)[2] - (ptr1 + wS)[2])+Math.Abs(3*((ptr1 + wS)[2] - ptr1[2]) + (ptr1 + wS - nC)[2] - (ptr1 - nC)[2]));
                    if (red > 255)
                        red = 255;
                    if (green > 255)
                        green = 255;
                    if (blue > 255)
                        blue = 255;
                    (dataPtr + (w - 1) * nC)[0] = (byte)blue;
                    (dataPtr + (w - 1) * nC)[1] = (byte)green;
                    (dataPtr + (w - 1) * nC)[2] = (byte)red;

                    //x = 0, y = height
                    ptr1 = dataPtrC + (h - 1) * wS;
                    blue = (int)(Math.Abs(3 * (ptr1[0] - (ptr1 + nC)[0]) + (ptr1 - wS)[0] - (ptr1 - wS + nC)[0]) + Math.Abs(3 * (ptr1[0] - (ptr1 - wS)[0]) + (ptr1 + nC)[0] - (ptr1 - wS + nC)[0]));
                    green = (int)(Math.Abs(3 * (ptr1[1] - (ptr1 + nC)[1]) + (ptr1 - wS)[1] - (ptr1 - wS + nC)[1]) + Math.Abs(3 * (ptr1[1] - (ptr1 - wS)[1]) + (ptr1 + nC)[1] - (ptr1 - wS + nC)[1]));
                    red = (int)(Math.Abs(3 * (ptr1[2] - (ptr1 + nC)[2]) + (ptr1 - wS)[2] - (ptr1 - wS + nC)[2]) + Math.Abs(3 * (ptr1[2] - (ptr1 - wS)[2]) + (ptr1 + nC)[2] - (ptr1 - wS + nC)[2]));
                    if (red > 255)
                        red = 255;
                    if (green > 255)
                        green = 255;
                    if (blue > 255)
                        blue = 255;
                    (dataPtr + (h - 1) * wS)[0] = (byte)blue;
                    (dataPtr + (h - 1) * wS)[1] = (byte)green;
                    (dataPtr + (h - 1) * wS)[2] = (byte)red;

                    //x = width, y = height
                    ptr1 = dataPtrC + (h - 1) * wS + (w - 1) * nC;
                    blue = (int)(Math.Abs(3 * ((ptr1 - nC)[0] - ptr1[0]) + (ptr1 - wS - nC)[0] - (ptr1 - wS)[0]) + Math.Abs(3 * (ptr1[0] - (ptr1 - wS)[0]) + (ptr1 - nC)[0] - (ptr1 - wS - nC)[0]));
                    green = (int)(Math.Abs(3 * ((ptr1 - nC)[1] - ptr1[1]) + (ptr1 - wS - nC)[1] - (ptr1 - wS)[1]) + Math.Abs(3 * (ptr1[1] - (ptr1 - wS)[1]) + (ptr1 - nC)[1] - (ptr1 - wS - nC)[1]));
                    red = (int)(Math.Abs(3 * ((ptr1 - nC)[2] - ptr1[2]) + (ptr1 - wS - nC)[2] - (ptr1 - wS)[2]) + Math.Abs(3 * (ptr1[2] - (ptr1 - wS)[2]) + (ptr1 - nC)[2] - (ptr1 - wS - nC)[2]));
                    if (red > 255)
                        red = 255;
                    if (green > 255)
                        green = 255;
                    if (blue > 255)
                        blue = 255;
                    (dataPtr + (h - 1) * wS + (w - 1) * nC)[0] = (byte)blue;
                    (dataPtr + (h - 1) * wS + (w - 1) * nC)[1] = (byte)green;
                    (dataPtr + (h - 1) * wS + (w - 1) * nC)[2] = (byte)red;

                }
            }
        }

        public static void Diferentiation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();

                byte* ptr1 = null;

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int padding = wS - nC * width;
                int x, y;
                int red, green, blue;

                if (nC == 3)
                {  // image in RGB
                   //for right pixels: just compare to pixel down
                    for (y = 0; y < height; y++)
                    {
                        ptr1 = dataPtrC + y * wS + (width - 1) * nC;
                        (dataPtr + y * wS + (width - 1) * nC)[0] = (byte)Math.Abs(ptr1[0] - (ptr1 + wS)[0]);
                        (dataPtr + y * wS + (width - 1) * nC)[1] = (byte)Math.Abs(ptr1[1] - (ptr1 + wS)[1]);
                        (dataPtr + y * wS + (width - 1) * nC)[2] = (byte)Math.Abs(ptr1[2] - (ptr1 + wS)[2]);
                    }
                    //for bottom pixels: just compare to right pixel
                    for (x = 0; x < width; x++)
                    {
                        ptr1 = dataPtrC + (height - 1) * wS + x * nC;
                        (dataPtr + (height - 1) * wS + x * nC)[0] = (byte)Math.Abs(ptr1[0] - (ptr1 + nC)[0]);
                        (dataPtr + (height - 1) * wS + x * nC)[1] = (byte)Math.Abs(ptr1[1] - (ptr1 + nC)[1]);
                        (dataPtr + (height - 1) * wS + x * nC)[2] = (byte)Math.Abs(ptr1[2] - (ptr1 + nC)[2]);
                    }

                    //for center pixels
                    for (x = 0; x < width - 1; x++)
                    {
                        for (y = 0; y < height - 1; y++)
                        {
                            ptr1 = dataPtrC + y * wS + x * nC;
                            blue = Math.Abs(ptr1[0] - (ptr1 + wS)[0]) + Math.Abs(ptr1[0] - (ptr1 + nC)[0]);
                            green = Math.Abs(ptr1[1] - (ptr1 + wS)[1]) + Math.Abs(ptr1[1] - (ptr1 + nC)[1]);
                            red = Math.Abs(ptr1[2] - (ptr1 + wS)[2]) + Math.Abs(ptr1[2] - (ptr1 + nC)[2]);

                            if (red > 255)
                                red = 255;
                            if (green > 255)
                                green = 255;
                            if (blue > 255)
                                blue = 255;

                            (dataPtr + y * wS + x * nC)[0] = (byte)blue;
                            (dataPtr + y * wS + x * nC)[1] = (byte)green;
                            (dataPtr + y * wS + x * nC)[2] = (byte)red;
                        }
                    }

                    //for last pixel - zero
                    (dataPtr + (height - 1) * wS + (width - 1) * nC)[0] = (byte)0;
                    (dataPtr + (height - 1) * wS + (width - 1) * nC)[1] = (byte)0;
                    (dataPtr + (height - 1) * wS + (width - 1) * nC)[2] = (byte)0;
                }
            }
        }


        public static void Roberts(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();

                byte* ptr1 = null;

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int padding = wS - nC * width;
                int x, y;
                int red, green, blue;

                if (nC == 3)
                {  // image in RGB
                    
                   //duplicate margins
                   //for right pixels: just compare to pixel down
                    for (y = 0; y < height; y++)
                    {
                        ptr1 = dataPtrC + y * wS + (width - 1) * nC;
                        (dataPtr + y * wS + (width - 1) * nC)[0] = (byte)Math.Abs(2*(ptr1[0] - (ptr1 + wS)[0]));
                        (dataPtr + y * wS + (width - 1) * nC)[1] = (byte)Math.Abs(2*(ptr1[1] - (ptr1 + wS)[1]));
                        (dataPtr + y * wS + (width - 1) * nC)[2] = (byte)Math.Abs(2*(ptr1[2] - (ptr1 + wS)[2]));
                    }
                    //for bottom pixels: just compare to right pixel
                    for (x = 0; x < width; x++)
                    {
                        ptr1 = dataPtrC + (height - 1) * wS + x * nC;
                        (dataPtr + (height - 1) * wS + x * nC)[0] = (byte)Math.Abs(2*(ptr1[0] - (ptr1 + nC)[0]));
                        (dataPtr + (height - 1) * wS + x * nC)[1] = (byte)Math.Abs(2*(ptr1[1] - (ptr1 + nC)[1]));
                        (dataPtr + (height - 1) * wS + x * nC)[2] = (byte)Math.Abs(2*(ptr1[2] - (ptr1 + nC)[2]));
                    }
                    
                    //for center pixels
                    for (x = 0; x < width - 1; x++)
                    {
                        for (y = 0; y < height - 1; y++)
                        {
                            ptr1 = dataPtrC + y * wS + x * nC;
                            blue = Math.Abs(ptr1[0] - (ptr1 + wS + nC)[0]) + Math.Abs((ptr1 + wS)[0] - (ptr1 + nC)[0]);
                            green = Math.Abs(ptr1[1] - (ptr1 + wS + nC)[1]) + Math.Abs((ptr1 + wS)[1] - (ptr1 + nC)[1]);
                            red = Math.Abs(ptr1[2] - (ptr1 + wS + nC)[2]) + Math.Abs((ptr1 + wS)[2] - (ptr1 + nC)[2]);

                            if (red > 255)
                                red = 255;
                            if (green > 255)
                                green = 255;
                            if (blue > 255)
                                blue = 255;

                            (dataPtr + y * wS + x * nC)[0] = (byte)blue;
                            (dataPtr + y * wS + x * nC)[1] = (byte)green;
                            (dataPtr + y * wS + x * nC)[2] = (byte)red;
                        }
                    }

                    (dataPtr + (height - 1) * wS + (width - 1) * nC)[0] = (byte)0;
                    (dataPtr + (height - 1) * wS + (width - 1) * nC)[1] = (byte)0;
                    (dataPtr + (height - 1) * wS + (width - 1) * nC)[2] = (byte)0;
                    
                }
            }
        }

        public static void Median(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                /*
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();

                byte* ptr1 = null;

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.wS;
                int padding = wS - nC * width;
                int x, y;
                int red, green, blue;
                */
                //calculate the median pixel: img.SmoothMedian(3).CopyTo(img);
                imgCopy.SmoothMedian(3).CopyTo(img);
                
                //bonus: implemetation by us
            }
        }

        public static int[] Histogram_Gray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int x, y, gray;
                int[] array = new int[256];

                if (nC == 3)
                {  // image in RGB
                    for (x = 0; x < width; x++)
                    {
                        for (y = 0; y < height; y++)
                        {
                            gray = (int)Math.Round(((dataPtr + y * wS + x * nC)[0] + (dataPtr + y * wS + x * nC)[1] + (dataPtr + y * wS + x * nC)[2]) / 3.0);

                            if (gray > 255)
                                gray = 255;   

                            array[gray] += 1;
                        }
                    }
                }
                return array;
            }
        }

        public static int[,] Histogram_RGB(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int x, y;
                int[,] matrix = new int[3, 256];
                if (nC == 3)
                {  // image in RGB
                    for (x = 0; x < width; x++)
                    {
                        for (y = 0; y < height; y++)
                        {
                            matrix[0, (int)(dataPtr + y * wS + x * nC)[0]] += 1;
                            matrix[1, (int)(dataPtr + y * wS + x * nC)[1]] += 1;
                            matrix[2, (int)(dataPtr + y * wS + x * nC)[2]] += 1;
                        }
                    }
                }
                return matrix;
            }
        }

        public static int[,] Histogram_All(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int x, y, gray;
                int[,] matrix = new int[4, 256];

                if (nC == 3)
                {  // image in RGB
                    for (x = 0; x < width; x++)
                    {
                        for (y = 0; y < height; y++)
                        {
                            gray = (int)Math.Round(((dataPtr + y * wS + x * nC)[0] + (dataPtr + y * wS + x * nC)[1] + (dataPtr + y * wS + x * nC)[2]) / 3.0);
                            if (gray > 255)
                                gray = 255;

                            matrix[0, gray] += 1;
                            matrix[1, (int)(dataPtr + y * wS + x * nC)[0]] += 1;
                            matrix[2, (int)(dataPtr + y * wS + x * nC)[1]] += 1;
                            matrix[3, (int)(dataPtr + y * wS + x * nC)[2]] += 1;
                        }
                    }
                }
                return matrix;
            }
        }

        public static void ConvertToBW(Emgu.CV.Image<Bgr, byte> img, int threshold)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;  
                int x, y, gray;

                if (nC == 3)
                {  // image in RGB
                    for (x = 0; x < width; x++)
                    {
                        for (y = 0; y < height; y++)
                        {
                            gray = (int)Math.Round(((dataPtr + y * wS + x * nC)[0] + (dataPtr + y * wS + x * nC)[1] + (dataPtr + y * wS + x * nC)[2]) / 3.0);
                            if (gray > threshold)
                            {
                                (dataPtr + y * wS + x * nC)[0] = (byte)255;
                                (dataPtr + y * wS + x * nC)[1] = (byte)255;
                                (dataPtr + y * wS + x * nC)[2] = (byte)255;

                            }
                            else
                            {
                                (dataPtr + y * wS + x * nC)[0] = (byte)0;
                                (dataPtr + y * wS + x * nC)[1] = (byte)0;
                                (dataPtr + y * wS + x * nC)[2] = (byte)0;
                            }
                        }
                    }
                }
            }
        }

        public static int otsu(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;

                int[] hist = Histogram_Gray(img);
                float[] prob = new float[256];
                float area = width * height;
                int max = 0, variance, threshold = 0;
                float q1 = 0;
                float q2 = 0;
                float u1 = 0;
                float u2 = 0;

                if (nC == 3)
                {
                    //1 - get max value for variance = q1.q2.(u1-u2)^2
                    for (int i = 0; i < 256; i++)
                        prob[i] = hist[i] / area;

                    for (int i = 1; i < 256; i++) //sum all for u2, then just do inverse of u1
                        u2 += i * prob[i];

                    //for (int i = 0; i < 256; i++) //sum all for q2, then can just inverse of q1
                    //    q2 += prob[i];
                    q1 = prob[0];
                    q2 = 1 - q1;
                    //u1 = prob[0];
                    for (int i = 1; i < 256; i++)
                    {
                        q1 += prob[i];
                        q2 -= prob[i];

                        u1 += i * prob[i];
                        u2 -= i * prob[i];


                        variance = (int)Math.Round(q1 * q2 * Math.Pow((u1 / q1 - u2 / q2), 2.0));

                        if (variance > max)
                        {
                            threshold = i;
                            max = variance;
                        }
                    }
                }
                return threshold;
            }
        }

        public static void ConvertToBW_Otsu(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                /*
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int wS = m.widthStep;
                
                int[] hist = Histogram_Gray(img);
                float[] prob = new float[256];
                float area = width * height;
                int x, y, max = 0, variance, threshold = 0;
                float q1 = 0;
                float q2 = 0;
                float u1 = 0;
                float u2 = 0;

                if (nC == 3)
                {
                    //1 - get max value for variance = q1.q2.(u1-u2)^2
                    for (int i = 0; i < 256; i++)
                        prob[i] = hist[i] / area;

                    for (int i = 1; i < 256; i++) //sum all for u2, then just do inverse of u1
                        u2 += i * prob[i];

                    //for (int i = 0; i < 256; i++) //sum all for q2, then can just inverse of q1
                    //    q2 += prob[i];
                    q1 = prob[0];
                    q2 = 1 - q1;
                    //u1 = prob[0];
                    for (int i = 1; i < 256; i++)
                    {
                        q1 += prob[i];
                        q2 -= prob[i];

                        u1 += i * prob[i];
                        u2 -= i * prob[i];

                        
                        variance = (int)Math.Round(q1 * q2 * Math.Pow((u1 / q1 - u2 / q2), 2.0));

                        if (variance > max)
                        {
                            threshold = i;
                            max = variance;
                        }
                    }
                    */
                //2 - Convert now to B&W with threshold as value separator
                /*int gray;
                for (x = 0; x < width; x++)
                {
                    for (y = 0; y < height; y++)
                    {
                        gray = (int)Math.Round(((dataPtr + y * wS + x * nC)[0] + (dataPtr + y * wS + x * nC)[1] + (dataPtr + y * wS + x * nC)[2]) / 3.0);
                        if (gray > threshold)
                        {
                            (dataPtr + y * wS + x * nC)[0] = (byte)255;
                            (dataPtr + y * wS + x * nC)[1] = (byte)255;
                            (dataPtr + y * wS + x * nC)[2] = (byte)255;

                        }
                        else
                        {
                            (dataPtr + y * wS + x * nC)[0] = (byte)0;
                            (dataPtr + y * wS + x * nC)[1] = (byte)0;
                            (dataPtr + y * wS + x * nC)[2] = (byte)0;
                        }
                    }
                }
                }*/

                int threshold = otsu(img);
                ConvertToBW(img, threshold);
            }
        }

        public static void Projections(Image<Bgr, byte> img, int[] projX, int[] projY, Rectangle r, int color)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                //int w = img.Width;
                //int h = img.Height;
                int w = r.Width + r.X;
                int h = r.Height + r.Y;
                int nC = m.nChannels;
                int wS = m.widthStep;
                int x, y;
                int i = 0, j = 0;

                //Get horizontal and vertical projections
                for (x = r.X; x < w; x++)
                {
                    for (y = r.Y; y < h; y++)
                    {
                        if ((int)(dataPtr + y * wS + x * nC)[0] == color) //black(0) or white(255)
                        {
                            projX[i] += 1;
                            projY[j] += 1;
                        }
                        j++;
                    }
                    i++;
                    j = 0;
                }
            }
        }


        public static void getLocLetters(int[] projX, int[] projY, Rectangle[] r, int level, string cat)
        {
            unsafe
            {
                int[] xLoc1 = new int[16];
                int[] xLoc = new int[12];
                int[] xLocL = new int[8];
                int[] xLocR = new int[8];
                int[] yLoc = new int[2];
                int prev = projX[0];
                int i = 0; //, j = 0;
                int xPas = projX.Max() / 8;//8
                //int yPas = 5; //projY.Max() / 5;//5

                //for x positions
                /*
                for (int x = 0; x < projX.Length; x++)
                {
                    if (projX[x] >= xPas && prev < xPas && i != 8)
                        xLocL[i++] = x;
                    if (projX[x] <= xPas && prev > xPas && j != 8)
                        xLocR[j++] = x;
                    prev = projX[x];
                }
                */

                /*
                if (xLocL[xLocL.Length - 1] == 0)
                {
                    for (i = xLocL.Length - 1; i > 1; i--)
                    {
                        xLocL[i] = xLocL[i - 1];
                    }
                    xLocL[0] = 0;
                }
                */
                if (level == 2)
                {
                    xLoc1[i++] = 0;
                    //xLoc1[xLoc1.Length - 1] = projX[projX.Length - 1];
                }
                for (int x = 0; x < projX.Length; x++)
                {
                    if (i == 16)
                        break;
                    if ((projX[x] >= xPas && prev < xPas) || (projX[x] < xPas && prev >= xPas))
                    {
                        if (i != 0)
                        {
                            if (xLoc1[i - 1] + 5 < x)
                                xLoc1[i++] = x;
                        }else
                            xLoc1[i++] = x;
                    }
                    prev = projX[x];
                }
                

                int j = 0;
                //remove dots
                if (cat.Equals("A"))
                {
                    for (i = 0; i < xLoc1.Length; i++)
                    {
                        if (i != 4 && i != 5 && i != 10 && i != 11)
                        {
                            xLoc[j++] = xLoc1[i];
                        }
                    }
                } else
                {
                    /*
                    for (i = 0; i < xLoc1.Length; i++)
                    {
                        xLoc[j++] = xLoc1[i];
                    }
                    */
                    Array.Copy(xLoc1, xLoc, xLoc.Length);
                }
                if (level == 2)
                {
                    xLoc[xLoc.Length - 1] = projX.Length - 1;
                }

                //for y positions
                if (level == 1)
                {
                    /*
                    i = 0;
                    prev = -1;
                    for (int y = 0; y < projY.Length; y++)
                    {
                        if (i == 2)
                            break;

                        if ((projY[y] > yPas && prev < yPas) || (projY[y] < yPas && prev > yPas)) //transfer from no pixel to pixels or vise versa
                        {
                            //save the x value to end/begin of the letter
                            yLoc[i++] = y;
                        }
                        prev = projY[y];
                    }
                    */
                    yLoc[0] = (int)(projY.Length * .08);//10;
                    yLoc[1] = (int)(projY.Length * .96);
                    r[0] = new Rectangle(xLoc[0], yLoc[0], xLoc[11] - xLoc[0], yLoc[1] - yLoc[0]);
                    for (i = 0; i < 12; i += 2)
                    {
                        r[i / 2 + 1] = new Rectangle(xLoc[i] - 2, yLoc[0], xLoc[i + 1] - xLoc[i] + 4, yLoc[1] - yLoc[0]);
                    }
                } else
                {
                    yLoc[0] = r[0].Y;
                    yLoc[1] = r[0].Height + r[0].Y;
                    for (i = 0; i < 12; i += 2)
                    {
                        r[i / 2 + 1] = new Rectangle(xLoc[i] + r[0].X - 2, yLoc[0], xLoc[i + 1] - xLoc[i] + 4, yLoc[1] - yLoc[0]);
                    }
                }
            }
        }

        public static string getLetter(Image<Bgr, byte> img, Rectangle rect, int state)
        {
            unsafe
            {
                string path = "..\\..\\..\\BD\\";
                string[] files = Directory.GetFiles(path, "*.bmp", SearchOption.AllDirectories);
                int match = 0, bestMatch = 0;
                string str = null;
               
                byte* dataPtr = (byte*)img.MIplImage.imageData.ToPointer();
                int width = rect.Width;
                int height = rect.Height;
                int nC = img.MIplImage.nChannels;
                int wS = img.MIplImage.widthStep;

                foreach (string file in files)
                {
                    if (state == 1)
                    {
                        if (!Path.GetFileNameWithoutExtension(file).All(char.IsDigit))
                            continue;
                    }
                    if (state == 2)
                    {
                        if (Path.GetFileNameWithoutExtension(file).All(char.IsDigit))
                            continue;
                    }

                    Image<Bgr, byte> data = new Image<Bgr, byte>(file);
                    data = data.Resize(width, height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, false);
                    ConvertToBW_Otsu(data);
                    byte* ptr1 = (byte*)data.MIplImage.imageData.ToPointer();
                    int nC1 = data.MIplImage.nChannels;
                    int wS1 = data.MIplImage.widthStep;

                    //Comparison
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            int p1 = (int)(dataPtr + (y + rect.Y) * wS + (x + rect.X) * nC)[0]; //image - plate
                            int p2 = (int)(ptr1 + y * wS1 + x * nC1)[0];  //data base
                            if (p1 == p2)
                            {
                                match++;
                            }
                        }
                    }

                    //Store more simular one
                    if (match > bestMatch)
                    {
                        bestMatch = match;
                        str = Path.GetFileNameWithoutExtension(file);
                    }
                    
                    match = 0;
                }

                return str;
            }
        }


        public static void Level1 (Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, Rectangle[] rect, string[] str, string cat)
        {
            unsafe
            {
                //Convert to Black and White
                Median(img, imgCopy);
                ConvertToBW_Otsu(img);

                int[] projX = new int[img.Width];
                int[] projY = new int[img.Height];
                //Put all arrays with zero value
                //Array.Clear(projX, 0, projX.Length);
                //Array.Clear(projY, 0, projY.Length);

                //1. Get horizontal and vertical projections
                rect[0] = new Rectangle(0, 0, img.Width, img.Height);
                Projections(img, projX, projY, rect[0], 0); //black
                //Array.Copy(projX, proj, projX.Length);

                //2. Get the letters position - rectangles
                getLocLetters(projX, projY, rect, 1, cat);

                //3. Compare to Data Base
                for (int i = 0; i < str.Length; i++) //0 1 2 3 4 5
                {
                    str[i] = getLetter(img, rect[i + 1], 0);
                }

            }
        }


        public static Rectangle getPlateLoc(Image<Bgr, byte> img)
        {
            unsafe 
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int w = m.width;
                int h = m.height;
                int nC = m.nChannels;
                int wS = m.widthStep;

                int h0 = (int)(.2 * img.Height);
                int h1 = (int)(.9 * img.Height);
                int w0 = (int)(.15 * img.Width); //left limit
                int w1 = (int)(.85 * img.Width); //right limit
                int[] projX = new int[w1 - w0];
                int[] projY = new int[h1 - h0]; //limit the plate location as it in center

                //if image has more black pixels -> white count for projections - or vice versa
                int black = 0, white = 0;
                for(int i = w0; i < w1; i++)
                {
                    for (int j = h0; j < h1; j++)
                    {
                        if ((dataPtr + i * nC + j * wS)[1] != 0)
                            white++;
                        else
                            black++;
                    }
                }
                if (black > white)
                    Projections(img, projX, projY, new Rectangle(w0, h0, (int)(w1 - w0), (int)(h1 - h0)), 255); //white
                if (black < white)
                    Projections(img, projX, projY, new Rectangle(w0, h0, (int)(w1 - w0), (int)(h1 - h0)), 0); //black

                //fillRect(img, new Rectangle(w0, h0, (int)(w1 - w0), (int)(h1 - h0)));

                // for print histogram
                //Array.Copy(projY, proj, projY.Length);

                //Vertical proj
                int filterNum = projY.Max() / 2;
                int prev = filterNum;
                //int maxIndex = Array.IndexOf(projY, projY.Max());
                List<int> listY = new List<int>();
                
                for (int i = 0; i < projY.Length; i++)
                {
                    if ((projY[i] >= filterNum && prev < filterNum) || (projY[i] <= filterNum && prev > filterNum))
                    {
                        listY.Add(i + h0);
                    }
                    prev = projY[i];
                }
                int[] y = new int[2];
                if (listY.Count < 3) //case just two point detected
                {
                    y[0] = listY[0];
                    y[1] = listY[1];
                } else if (listY.Count > 9) //more points - take first 3 out
                {
                    y[0] = listY[3];
                    y[1] = listY[6];
                } else //take first out
                {
                    y[0] = listY[1];
                    if (listY.Count > 9)
                        y[1] = listY[8];
                    y[1] = listY[2];
                }
                //fillRect(img, new Rectangle(w0, y[0], (int)(w1 - w0), y[1] - y[0]));

                //Horizontal proj - need to get new proj based on Y recognition
                prev = -1;
                Array.Clear(projX, 0, projX.Length);
                Array.Clear(projY, 0, projY.Length);
                Projections(img, projX, projY, new Rectangle(w0, y[0], (int)(w1 - w0), y[1] - y[0]), 0); //black

                // for print histogram
                //Array.Copy(projX, proj, projX.Length);

                filterNum = (int)(.15 * projX.Max());
                //maxIndex = Array.IndexOf(projX, projX.Max());
                List<int> listX = new List<int>();
                List<int> maxInx = new List<int>();
                int max = projX.Max() - 10;

                for (int i = 0; i < projX.Length; i++)
                {
                    if ((projX[i] >= filterNum && prev < filterNum) || (projX[i] <= filterNum && prev > filterNum))
                    {
                        listX.Add(i + w0);
                    }
                    if ((projX[i] >= max && prev < max) || (projX[i] < max && prev >= max))
                    {
                        maxInx.Add(i + w0);
                    }
                    prev = projX[i];
                }
                int[] x = new int[2];
                for (int i = 0; i < listX.Count; i++)
                {
                    for (int j = 0; j < maxInx.Count; j++)
                    {
                        if (listX[i] <= maxInx[j] + 5 && listX[i] > maxInx[j] - 5)
                        {
                            listX.RemoveAt(i);
                        }
                    }
                }
                x[0] = listX[0];
                x[1] = listX[listX.Count - 1];
                //fillRect(img, new Rectangle(x[0], y[0], x[1] - x[0], y[1] - y[0]));
                return new Rectangle(x[0], y[0], x[1] - x[0], y[1] - y[0]);
            }
        }

        public static void Level2(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, Rectangle[] rect, string[] str, string cat)
        {
            unsafe
            {
                //Make Copy of the image
                Image<Bgr, byte> img1 = img.Copy();

                //Locate plate: sobel + otsu
                Sobel(img1, imgCopy);
                for (int i = 0; i < 4; i++)
                    Median(img1, imgCopy);
                 ConvertToBW(img1, 150);

                //Analyse projections in order to recognize the location of the plate
                rect[0] = getPlateLoc(img1);

                //Convert to Black and White
                Median(img, imgCopy);
                ConvertToBW_Otsu(img);

                int[] projX = new int[rect[0].Width];
                int[] projY = new int[rect[0].Height];
                
                //Get horizontal and vertical projections
                Projections(img, projX, projY, rect[0], 0);
                //Array.Copy(projX, proj, projX.Length);

                //Get the letters position - rectangles
                getLocLetters(projX, projY, rect, 2, cat);

                //Compare to Data Base
                List<int> error = new List<int>();
                int digits = 0;
                for (int i = 0; i < str.Length; i++) //0 1 2 3 4 5
                {
                    str[i] = getLetter(img, rect[i + 1], 0);
                    if (str[i].All(char.IsDigit))
                        digits++;
                    
                    if (i % 2 != 0) //odd
                    {
                        if (str[i].All(char.IsDigit) != str[i - 1].All(char.IsDigit))
                        {
                            error.Add(i);
                        }
                    }
                    
                }
                if (error.Count > 0) //exist error
                {
                    switch (digits) //knows that the error maybe the letters
                    {
                        case 1: //letters letter/number letters - number
                            if (!str[error[0]].All(char.IsDigit))
                                str[error[0]] = getLetter(img, rect[error[0] + 1], 1); //number
                            else
                                str[error[0] - 1] = getLetter(img, rect[error[0]], 1); //number
                            break;

                        case 2: //letters letter/number letter/number - number letter
                            if (!str[error[0]].All(char.IsDigit))
                                str[error[0]] = getLetter(img, rect[error[0] + 1], 1); //number
                            else
                                str[error[0] - 1] = getLetter(img, rect[error[0]], 1); //number

                            if (str[error[1]].All(char.IsDigit))
                                str[error[1]] = getLetter(img, rect[error[1] + 1], 2); //letter
                            else
                                str[error[1] - 1] = getLetter(img, rect[error[1]], 2); //letter
                            break;

                        case 3: //numbers number/letter letters - number
                            if (!str[error[0]].All(char.IsDigit))
                                str[error[0]] = getLetter(img, rect[error[0] + 1], 1); //number
                            else
                                str[error[0] - 1] = getLetter(img, rect[error[0]], 1); //number
                            break;

                        case 4: //numbers number/letter number/letter - letter number
                            if (str[error[0]].All(char.IsDigit))
                                str[error[0]] = getLetter(img, rect[error[0] + 1], 2); //letter
                            else
                                str[error[0] - 1] = getLetter(img, rect[error[0]], 2);

                            if (!str[error[1]].All(char.IsDigit))
                                str[error[1]] = getLetter(img, rect[error[1] + 1], 1); //number
                            else
                                str[error[1] - 1] = getLetter(img, rect[error[1]], 1); 
                            break;

                        case 5: //numbers numbers number/letter - letter
                            if (str[error[0]].All(char.IsDigit))
                                str[error[0]] = getLetter(img, rect[error[0] + 1], 2); //letter
                            else
                                str[error[0] - 1] = getLetter(img, rect[error[0]], 2); //letter
                            break;
                    }
                }
                /*
                */
            }
        }

        public static void fillRect(Emgu.CV.Image<Bgr, byte> img, Rectangle rect)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int wS = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                dataPtr += rect.X * nC + rect.Y * wS;

                for (y = 0; y < rect.Height; y++)
                {
                    for (x = 0; x < rect.Width; x++)
                    {
                        if ((x == 0 || x == rect.Width - 1 || x == 1 || x == rect.Width - 2) || (y == 0 || y == rect.Height - 1 || y == 1 || y == rect.Height - 2))
                        {
                            dataPtr[0] = (byte)0;
                            dataPtr[1] = (byte)0;
                            dataPtr[2] = (byte)255;
                        }

                        dataPtr += nC;
                    }
                    dataPtr += wS - rect.Width * nC;
                }
            }
        }




        /// <summary>
        /// License plate recognition
        /// </summary>
        /// <param name="img"></param>
        /// <param name="imgCopy"></param>
        /// <param name="difficultyLevel">Difficulty level 1-4</param>
        /// <param name="Type">LP type (A or B)</param>
        /// <param name="LP_Location"></param>
        /// <param name="LP_Chr1"></param>
        /// <param name="LP_Chr2"></param>
        /// <param name="LP_Chr3"></param>
        /// <param name="LP_Chr4"></param>
        /// <param name="LP_Chr5"></param>
        /// <param name="LP_Chr6"></param>
        /// <param name="LP_C1"></param>
        /// <param name="LP_C2"></param>
        /// <param name="LP_C3"></param>
        /// <param name="LP_C4"></param>
        /// <param name="LP_C5"></param>
        /// <param name="LP_C6"></param>
        public static void LP_Recognition(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy,
            int difficultyLevel,
            string LPType,
            out Rectangle LP_Location,
            out Rectangle LP_Chr1,
            out Rectangle LP_Chr2,
            out Rectangle LP_Chr3,
            out Rectangle LP_Chr4,
            out Rectangle LP_Chr5,
            out Rectangle LP_Chr6,
            out string LP_C1,
            out string LP_C2,
            out string LP_C3,
            out string LP_C4,
            out string LP_C5,
            out string LP_C6
        )
        {
            LP_Location = new Rectangle(220, 190, 200, 40);
            LP_Chr1 = new Rectangle(340, 190, 30, 40);
            LP_Chr2 = new Rectangle(360, 190, 30, 40);
            LP_Chr3 = new Rectangle(380, 190, 30, 40);
            LP_Chr4 = new Rectangle(400, 190, 30, 40);
            LP_Chr5 = new Rectangle(420, 190, 30, 40);
            LP_Chr6 = new Rectangle(440, 190, 30, 40);

            LP_C1 = "1";
            LP_C2 = "2";
            LP_C3 = "3";
            LP_C4 = "4";
            LP_C5 = "5";
            LP_C6 = "6";

            Rectangle[] r = new Rectangle[7];
            string[] str = new string[6];

            switch (difficultyLevel)
            {
                case 1:
                    Level1(img, imgCopy, r, str, LPType);
                    break;  
                case 2:
                    Level2(img, imgCopy, r, str, LPType);
                    break;
            }

            LP_Location = r[0];
            LP_Chr1 = r[1];
            LP_Chr2 = r[2];
            LP_Chr3 = r[3];
            LP_Chr4 = r[4];
            LP_Chr5 = r[5];
            LP_Chr6 = r[6];
            
            /*
            fillRect(img, r[0]);
            fillRect(img, r[1]);
            fillRect(img, r[2]);
            fillRect(img, r[3]);
            fillRect(img, r[4]);
            fillRect(img, r[5]);
            fillRect(img, r[6]);
            */

            LP_C1 = str[0];
            LP_C2 = str[1];
            LP_C3 = str[2];
            LP_C4 = str[3];
            LP_C5 = str[4];
            LP_C6 = str[5];
        }



    }
}