using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;

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
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                if (nChan == 3)
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
                            dataPtr += nChan;
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
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
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

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Red(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte red;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                if (nChan == 3)
                {// image in RGB
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {

                            //PIXEL PROCESSING
                            //retrive 3 colour components
                            red = dataPtr[2];

                            //convert to gray
                            red = (byte)(255 - red);

                            //store in image
                            //dataPtr[0] = 0;
                            //dataPtr[1] = 0;
                            dataPtr[0] = red;
                            dataPtr[1] = red;
                            dataPtr[2] = red;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }
                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Green(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte green;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                if (nChan == 3)
                {// image in RGB
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {

                            //PIXEL PROCESSING
                            //retrive 3 colour components
                            green = dataPtr[1];

                            //convert to gray
                            green = (byte)(255 - green);

                            //store in image
                            dataPtr[0] = green;
                            dataPtr[1] = green;
                            dataPtr[2] = green;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }
                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Blue(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                if (nChan == 3)
                {// image in RGB
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {

                            //PIXEL PROCESSING
                            //retrive 3 colour components
                            blue = dataPtr[0];

                            //convert to gray
                            blue = (byte)(255 - blue);

                            //store in image
                            dataPtr[0] = blue;
                            dataPtr[1] = blue;
                            dataPtr[2] = blue;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }
                        //at the end of the line advance the pointer by the alignment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }


        public static void BrightContrast(Image<Bgr, byte> img, int bright, float contrast)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - nChan * width;
                int x, y;
                byte red, green, blue;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //TODO: get bright and contrast
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

                            dataPtr += nChan; //point to the next pixel
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
                byte* dataPtr_aux;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nChan * width;
                int x, y, x0, y0;

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
                            dataPtr_aux = (dataPtrC + y0 * widthstep + x0 * nChan); //point to pixel
                            //store in the image
                            (dataPtr + y * widthstep + x * nChan)[0] = dataPtr_aux[0];
                            (dataPtr + y * widthstep + x * nChan)[1] = dataPtr_aux[1];
                            (dataPtr + y * widthstep + x * nChan)[2] = dataPtr_aux[2];

                        }
                        else //out pixels will be black
                        {
                            (dataPtr + y * widthstep + x * nChan)[0] = 0;
                            (dataPtr + y * widthstep + x * nChan)[1] = 0;
                            (dataPtr + y * widthstep + x * nChan)[2] = 0;
                        }
                        //dataPtr += nChan; //point to the next pixel
                    }
                    //dataPtr += padding; //allignment padding out
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
                byte* dataPtr_aux;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nChan * width;
                int x, y, x0, y0;
                float cosine = (float)Math.Cos(angle);
                float sine = (float)Math.Sin(angle);
                float w2 = width / 2;
                float h2 = height / 2;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        //new x and y
                        x0 = (int)Math.Round((x - w2) * cosine - (h2 - y) * sine + w2);
                        y0 = (int)Math.Round(h2 - (x - w2) * sine - (h2 - y) * cosine);

                        if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height)
                        {
                            // calculate point address (x,y)
                            dataPtr_aux = (dataPtrC + y0 * widthstep + x0 * nChan); //point to pixel
                            //store in the image 
                            (dataPtr + y * widthstep + x * nChan)[0] = dataPtr_aux[0];
                            (dataPtr + y * widthstep + x * nChan)[1] = dataPtr_aux[1];
                            (dataPtr + y * widthstep + x * nChan)[2] = dataPtr_aux[2];

                        }
                        else 
                        {
                            (dataPtr + y * widthstep + x * nChan)[0] = 0;
                            (dataPtr + y * widthstep + x * nChan)[1] = 0;
                            (dataPtr + y * widthstep + x * nChan)[2] = 0;
                        }
                        //dataPtr += nChan; //point to the next pixel
                    }
                    //dataPtr += padding; //allignment padding out
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
                byte* dataPtr_aux;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nChan * width;
                int x, y, x0, y0;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        //new x and y
                        x0 = (int)Math.Round(x / scaleFactor);
                        y0 = (int)Math.Round(y / scaleFactor);

                        if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height)
                        {
                            dataPtr_aux = (dataPtrC + y0 * widthstep + x0 * nChan); //point to pixel
                            //store in the image 
                            (dataPtr + y * widthstep + x * nChan)[0] = dataPtr_aux[0];
                            (dataPtr + y * widthstep + x * nChan)[1] = dataPtr_aux[1];
                            (dataPtr + y * widthstep + x * nChan)[2] = dataPtr_aux[2];

                        }
                        else
                        {
                            (dataPtr + y * widthstep + x * nChan)[0] = 0;
                            (dataPtr + y * widthstep + x * nChan)[1] = 0;
                            (dataPtr + y * widthstep + x * nChan)[2] = 0;
                        }
                        //dataPtr += nChan; //point to the next pixel
                    }
                    //dataPtr += padding; //allignment padding out
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
                byte* dataPtr_aux;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nChan * width;
                int x, y, x0, y0;
                float w2 = (width / 2) / (scaleFactor), h2 = (height / 2) / (scaleFactor);

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        //new x and y
                        x0 = (int)Math.Round((x / scaleFactor) + centerX - w2);
                        y0 = (int)Math.Round((y / scaleFactor) + centerY - h2);

                        if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height)
                        {
                            dataPtr_aux = (dataPtrC + y0 * widthstep + x0 * nChan); //point to pixel
                            //store in the image 
                            (dataPtr + y * widthstep + x * nChan)[0] = dataPtr_aux[0];
                            (dataPtr + y * widthstep + x * nChan)[1] = dataPtr_aux[1];
                            (dataPtr + y * widthstep + x * nChan)[2] = dataPtr_aux[2];

                        }
                        else
                        {
                            (dataPtr + y * widthstep + x * nChan)[0] = 0;
                            (dataPtr + y * widthstep + x * nChan)[1] = 0;
                            (dataPtr + y * widthstep + x * nChan)[2] = 0;
                        }
                        //dataPtr += nChan; //point to the next pixel
                    }
                    //dataPtr += padding; //allignment padding out
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

                byte* dataPtr_aux = null;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nChan * width;
                int x = 0, y = 0;
                int red, green, blue;


                //for border pixels
                //1 - for particular cases: border points
                //x = y = 0
                blue = (int)Math.Round((dataPtrC[0] * 4 + (dataPtrC + widthstep)[0] * 2 + (dataPtrC + nChan)[0] * 2 + (dataPtrC + widthstep + nChan)[0]) / 9.0);
                green = (int)Math.Round((dataPtrC[1] * 4 + (dataPtrC + widthstep)[1] * 2 + (dataPtrC + nChan)[1] * 2 + (dataPtrC + widthstep + nChan)[1]) / 9.0);
                red = (int)Math.Round((dataPtrC[2] * 4 + (dataPtrC + widthstep)[2] * 2 + (dataPtrC + nChan)[2] * 2 + (dataPtrC + widthstep + nChan)[2]) / 9.0);
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
                dataPtr_aux = (dataPtrC + (height - 1) * widthstep);
                blue = (int)Math.Round((dataPtr_aux[0] * 4 + (dataPtr_aux - widthstep)[0] * 2 + (dataPtr_aux + nChan)[0] * 2 + (dataPtr_aux - widthstep - nChan)[0]) / 9.0);
                green = (int)Math.Round((dataPtr_aux[1] * 4 + (dataPtr_aux - widthstep)[1] * 2 + (dataPtr_aux + nChan)[1] * 2 + (dataPtr_aux - widthstep - nChan)[1]) / 9.0);
                red = (int)Math.Round((dataPtr_aux[2] * 4 + (dataPtr_aux - widthstep)[2] * 2 + (dataPtr_aux + nChan)[2] * 2 + (dataPtr_aux - widthstep - nChan)[2]) / 9.0);
                if (blue > 255)
                    blue = 255;
                if (green > 255)
                    green = 255;
                if (red > 255)
                    red = 255;

                (dataPtr + (height - 1) * widthstep)[0] = (byte)blue;
                (dataPtr + (height - 1) * widthstep)[1] = (byte)green;
                (dataPtr + (height - 1) * widthstep)[2] = (byte)red;

                //x = width and y = height
                dataPtr_aux = (dataPtrC + (height - 1) * widthstep + (width - 1) * nChan);
                blue = (int)Math.Round((dataPtr_aux[0] * 4 + (dataPtr_aux - widthstep)[0] * 2 + (dataPtr_aux - nChan)[0] * 2 + (dataPtr_aux - widthstep - nChan)[0]) / 9.0);
                green = (int)Math.Round((dataPtr_aux[1] * 4 + (dataPtr_aux - widthstep)[1] * 2 + (dataPtr_aux - nChan)[1] * 2 + (dataPtr_aux - widthstep - nChan)[1]) / 9.0);
                red = (int)Math.Round((dataPtr_aux[2] * 4 + (dataPtr_aux - widthstep)[2] * 2 + (dataPtr_aux - nChan)[2] * 2 + (dataPtr_aux - widthstep - nChan)[2]) / 9.0);

                if (blue > 255)
                    blue = 255;
                if (green > 255)
                    green = 255;
                if (red > 255)
                    red = 255;

                (dataPtr + (height - 1) * widthstep + (width - 1) * nChan)[0] = (byte)blue;
                (dataPtr + (height - 1) * widthstep + (width - 1) * nChan)[1] = (byte)green;
                (dataPtr + (height - 1) * widthstep + (width - 1) * nChan)[2] = (byte)red;
                //x = width and y = 0
                dataPtr_aux = (dataPtrC + (width - 1) * nChan); 
                blue = (int)Math.Round((dataPtr_aux[0] * 4 + (dataPtr_aux + widthstep)[0] * 2 + (dataPtr_aux - nChan)[0] * 2 + (dataPtr_aux + widthstep - nChan)[0]) / 9.0);
                green = (int)Math.Round((dataPtr_aux[1] * 4 + (dataPtr_aux + widthstep)[1] * 2 + (dataPtr_aux - nChan)[1] * 2 + (dataPtr_aux + widthstep - nChan)[1]) / 9.0);
                red = (int)Math.Round((dataPtr_aux[1] * 4 + (dataPtr_aux + widthstep)[2] * 2 + (dataPtr_aux - nChan)[2] * 2 + (dataPtr_aux + widthstep - nChan)[2]) / 9.0);
                if (blue > 255)
                    blue = 255;
                if (green > 255)
                    green = 255;
                if (red > 255)
                    red = 255;

                (dataPtr + (width - 1) * nChan)[0] = (byte)blue;
                (dataPtr + (width - 1) * nChan)[1] = (byte)green;
                (dataPtr + (width - 1) * nChan)[2] = (byte)red;

                //2 - for border other cases
                //line top: y = 0
                for (x = 1; x < width - 1; x++)
                {
                    dataPtr_aux = (dataPtrC + x * nChan);
                    blue = (int)Math.Round(((dataPtr_aux - nChan)[0] * 2 + dataPtr_aux[0] * 2 + (dataPtr_aux + nChan)[0] * 2 + (dataPtr_aux + widthstep - nChan)[0] + (dataPtr_aux + widthstep)[0] + (dataPtr_aux + widthstep + nChan)[0]) / 9.0);
                    green = (int)Math.Round(((dataPtr_aux - nChan)[1] * 2 + dataPtr_aux[1] * 2 + (dataPtr_aux + nChan)[1] * 2 + (dataPtr_aux + widthstep - nChan)[1] + (dataPtr_aux + widthstep)[1] + (dataPtr_aux + widthstep + nChan)[1]) / 9.0);
                    red = (int)Math.Round(((dataPtr_aux - nChan)[2] * 2 + dataPtr_aux[2] * 2 + (dataPtr_aux + nChan)[2] * 2 + (dataPtr_aux + widthstep - nChan)[2] + (dataPtr_aux + widthstep)[2] + (dataPtr_aux + widthstep + nChan)[2]) / 9.0);
                    if (blue > 255)
                        blue = 255;
                    if (green > 255)
                        green = 255;
                    if (red > 255)
                        red = 255;
                    (dataPtr + x * nChan)[0] = (byte)blue;
                    (dataPtr + x * nChan)[1] = (byte)green;
                    (dataPtr + x * nChan)[2] = (byte)red;
                }
                //bottom line: y = height
                for (x = 1; x < width - 1; x++)
                {
                    dataPtr_aux = (dataPtrC + (height - 1) * widthstep + x * nChan);
                    blue = (int)Math.Round(((dataPtr_aux - nChan)[0] * 2 + dataPtr_aux[0] * 2 + (dataPtr_aux + nChan)[0] * 2 + (dataPtr_aux - widthstep - nChan)[0] + (dataPtr_aux - widthstep)[0] + (dataPtr_aux - widthstep + nChan)[0]) / 9.0);
                    green = (int)Math.Round(((dataPtr_aux - nChan)[1] * 2 + dataPtr_aux[1] * 2 + (dataPtr_aux + nChan)[1] * 2 + (dataPtr_aux - widthstep - nChan)[1] + (dataPtr_aux - widthstep)[1] + (dataPtr_aux - widthstep + nChan)[1]) / 9.0);
                    red = (int)Math.Round(((dataPtr_aux - nChan)[2] * 2 + dataPtr_aux[2] * 2 + (dataPtr_aux + nChan)[2] * 2 + (dataPtr_aux - widthstep - nChan)[2] + (dataPtr_aux - widthstep)[2] + (dataPtr_aux - widthstep + nChan)[2]) / 9.0);
                    if (blue > 255)
                        blue = 255;
                    if (green > 255)
                        green = 255;
                    if (red > 255)
                        red = 255;

                    (dataPtr + (height - 1) * widthstep + x * nChan)[0] = (byte)blue;
                    (dataPtr + (height - 1) * widthstep + x * nChan)[1] = (byte)green;
                    (dataPtr + (height - 1) * widthstep + x * nChan)[2] = (byte)red;
                }
                //left line: x = 0
                for (y = 1; y < height - 1; y++)
                { 
                    dataPtr_aux = (dataPtrC + y * widthstep);
                    blue = (int)Math.Round(((dataPtr_aux - widthstep)[0] * 2 + dataPtr_aux[0] * 2 + (dataPtr_aux + widthstep)[0] * 2 + (dataPtr_aux - widthstep + nChan)[0] + (dataPtr_aux + nChan)[0] + (dataPtr_aux + widthstep + nChan)[0]) / 9.0);
                    green = (int)Math.Round(((dataPtr_aux - widthstep)[1] * 2 + dataPtr_aux[1] * 2 + (dataPtr_aux + widthstep)[1] * 2 + (dataPtr_aux - widthstep + nChan)[1] + (dataPtr_aux + nChan)[1] + (dataPtr_aux + widthstep + nChan)[1]) / 9.0);
                    red = (int)Math.Round(((dataPtr_aux - widthstep)[2] * 2 + dataPtr_aux[2] * 2 + (dataPtr_aux + widthstep)[2] * 2 + (dataPtr_aux - widthstep + nChan)[2] + (dataPtr_aux + nChan)[2] + (dataPtr_aux + widthstep + nChan)[2]) / 9.0);
                    if (blue > 255)
                        blue = 255;
                    if (green > 255)
                        green = 255;
                    if (red > 255)
                        red = 255;

                    (dataPtr + y * widthstep)[0] = (byte)blue;
                    (dataPtr + y * widthstep)[1] = (byte)green;
                    (dataPtr + y * widthstep)[2] = (byte)red;
                }
                //right line: x = width
                for (y = 1; y < height - 1; y++)
                {
                    dataPtr_aux = (dataPtrC + y * widthstep + (width - 1) * nChan);
                    blue = (int)Math.Round(((dataPtr_aux - widthstep)[0] * 2 + dataPtr_aux[0] * 2 + (dataPtr_aux + widthstep)[0] * 2 + (dataPtr_aux - widthstep - nChan)[0] + (dataPtr_aux - nChan)[0] + (dataPtr_aux + widthstep - nChan)[0]) / 9.0);
                    green = (int)Math.Round(((dataPtr_aux - widthstep)[1] * 2 + dataPtr_aux[1] * 2 + (dataPtr_aux + widthstep)[1] * 2 + (dataPtr_aux - widthstep - nChan)[1] + (dataPtr_aux - nChan)[1] + (dataPtr_aux + widthstep - nChan)[1]) / 9.0);
                    red = (int)Math.Round(((dataPtr_aux - widthstep)[2] * 2 + dataPtr_aux[2] * 2 + (dataPtr_aux + widthstep)[2] * 2 + (dataPtr_aux - widthstep - nChan)[2] + (dataPtr_aux - nChan)[2] + (dataPtr_aux + widthstep - nChan)[2]) / 9.0);
                    if (blue > 255)
                        blue = 255;
                    if (green > 255)
                        green = 255;
                    if (red > 255)
                        red = 255;
                    (dataPtr + y * widthstep + (width - 1) * nChan)[0] = (byte)blue;
                    (dataPtr + y * widthstep + (width - 1) * nChan)[1] = (byte)green;
                    (dataPtr + y * widthstep + (width - 1) * nChan)[2] = (byte)red;
                }
                
                //for center pixels
                for (y = 1; y < height - 1; y++)
                {
                    for (x = 1; x < width - 1; x++)
                    {
                        dataPtr_aux = (dataPtrC + y * widthstep + x * nChan);

                        blue = 0;
                        green = 0;
                        red = 0;

                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                blue += (dataPtr_aux + i * widthstep + j * nChan)[0];
                                green += (dataPtr_aux + i * widthstep + j * nChan)[1];
                                red += (dataPtr_aux + i * widthstep + j * nChan)[2];
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

                        (dataPtr + y * widthstep + x * nChan)[0] = (byte)blue;
                        (dataPtr + y * widthstep + x * nChan)[1] = (byte)green;
                        (dataPtr + y * widthstep + x * nChan)[2] = (byte)red;
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

                byte* dataPtr_aux = null;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nChan * width;
                int x, y;
                int red, green, blue;

                
                //for border pixels
                //1 - for particular cases: border points
                //x = y = 0
                blue = (int)Math.Round(((dataPtrC[0] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) + (dataPtrC + widthstep)[0] * (matrix[0, 2] + matrix[1, 2]) + (dataPtrC + nChan)[0] * (matrix[2, 1] + matrix[2, 0]) + (dataPtrC + widthstep + nChan)[0] * matrix[2, 2]) / matrixWeight) + offset);
                green = (int)Math.Round(((dataPtrC[1] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) + (dataPtrC + widthstep)[1] * (matrix[0, 2] + matrix[1, 2]) + (dataPtrC + nChan)[1] * (matrix[2, 1] + matrix[2, 0]) + (dataPtrC + widthstep + nChan)[1] * matrix[2, 2]) / matrixWeight) + offset);
                red = (int)Math.Round(((dataPtrC[2] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) + (dataPtrC + widthstep)[2] * (matrix[0, 2] + matrix[1, 2]) + (dataPtrC + nChan)[2] * (matrix[2, 1] + matrix[2, 0]) + (dataPtrC + widthstep + nChan)[2] * matrix[2, 2]) / matrixWeight) + offset);

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
                dataPtr_aux = (dataPtrC + (height - 1) * widthstep);
                blue = (int)Math.Round(((dataPtr_aux[0] * (matrix[1, 1] + matrix[0, 1] + matrix[0, 2] + matrix[1, 2]) + (dataPtr_aux - widthstep)[0] * (matrix[0, 0] + matrix[1, 0]) + (dataPtr_aux + nChan)[0] * (matrix[2, 1] + matrix[2, 2]) + (dataPtr_aux - widthstep - nChan)[0] * matrix[2, 0]) / matrixWeight) + offset);
                green = (int)Math.Round(((dataPtr_aux[1] * (matrix[1, 1] + matrix[0, 1] + matrix[0, 2] + matrix[1, 2]) + (dataPtr_aux - widthstep)[1] * (matrix[0, 0] + matrix[1, 0]) + (dataPtr_aux + nChan)[1] * (matrix[2, 1] + matrix[2, 2]) + (dataPtr_aux - widthstep - nChan)[1] * matrix[2, 0]) / matrixWeight) + offset);
                red = (int)Math.Round(((dataPtr_aux[2] * (matrix[1, 1] + matrix[0, 1] + matrix[0, 2] + matrix[1, 2]) + (dataPtr_aux - widthstep)[2] * (matrix[0, 0] + matrix[1, 0]) + (dataPtr_aux + nChan)[2] * (matrix[2, 1] + matrix[2, 2]) + (dataPtr_aux - widthstep - nChan)[2] * matrix[2, 0]) / matrixWeight) + offset);

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

                (dataPtr + (height - 1) * widthstep)[0] = (byte)blue;
                (dataPtr + (height - 1) * widthstep)[1] = (byte)green;
                (dataPtr + (height - 1) * widthstep)[2] = (byte)red;

                //x = width and y = height
                dataPtr_aux = (dataPtrC + (height - 1) * widthstep + (width - 1) * nChan);
                blue = (int)Math.Round(((dataPtr_aux[0] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]) + (dataPtr_aux - widthstep)[0] * (matrix[1, 0] + matrix[2, 0]) + (dataPtr_aux - nChan)[0] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_aux - widthstep - nChan)[0] * matrix[0, 0]) / matrixWeight) + offset);
                green = (int)Math.Round(((dataPtr_aux[1] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]) + (dataPtr_aux - widthstep)[1] * (matrix[1, 0] + matrix[2, 0]) + (dataPtr_aux - nChan)[1] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_aux - widthstep - nChan)[1] * matrix[0, 0]) / matrixWeight) + offset);
                red = (int)Math.Round(((dataPtr_aux[2] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]) + (dataPtr_aux - widthstep)[2] * (matrix[1, 0] + matrix[2, 0]) + (dataPtr_aux - nChan)[2] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_aux - widthstep - nChan)[2] * matrix[0, 0]) / matrixWeight) + offset);

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

                (dataPtr + (height - 1) * widthstep + (width - 1) * nChan)[0] = (byte)blue;
                (dataPtr + (height - 1) * widthstep + (width - 1) * nChan)[1] = (byte)green;
                (dataPtr + (height - 1) * widthstep + (width - 1) * nChan)[2] = (byte)red;

                //x = width and y = 0
                dataPtr_aux = (dataPtrC + (width - 1) * nChan);
                blue = (int)Math.Round(((dataPtr_aux[0] * (matrix[1, 0] + matrix[2, 0] + matrix[1, 1] + matrix[2, 1]) + (dataPtr_aux + widthstep)[0] * (matrix[1, 2] + matrix[2, 2]) + (dataPtr_aux - nChan)[0] * (matrix[0, 0] + matrix[1, 0]) + (dataPtr_aux + widthstep - nChan)[0] * matrix[0, 2]) / matrixWeight) + offset);
                green = (int)Math.Round(((dataPtr_aux[1] * (matrix[1, 0] + matrix[2, 0] + matrix[1, 1] + matrix[2, 1]) + (dataPtr_aux + widthstep)[1] * (matrix[1, 2] + matrix[2, 2]) + (dataPtr_aux - nChan)[1] * (matrix[0, 0] + matrix[1, 0]) + (dataPtr_aux + widthstep - nChan)[1] * matrix[0, 2]) / matrixWeight) + offset);
                red = (int)Math.Round(((dataPtr_aux[1] * (matrix[1, 0] + matrix[2, 0] + matrix[1, 1] + matrix[2, 1]) + (dataPtr_aux + widthstep)[2] * (matrix[1, 2] + matrix[2, 2]) + (dataPtr_aux - nChan)[2] * (matrix[0, 0] + matrix[1, 0]) + (dataPtr_aux + widthstep - nChan)[2] * matrix[0, 2]) / matrixWeight) + offset);

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

                (dataPtr + (width - 1) * nChan)[0] = (byte)blue;
                (dataPtr + (width - 1) * nChan)[1] = (byte)green;
                (dataPtr + (width - 1) * nChan)[2] = (byte)red;

                //2 - for border other cases
                //line top: y = 0
                for (x = 1; x < width - 1; x++)
                {
                    dataPtr_aux = (dataPtrC + x * nChan);
                    blue = (int)Math.Round((((dataPtr_aux - nChan)[0] * (matrix[0, 0] + matrix[0, 1]) + dataPtr_aux[0] * (matrix[1, 0] + matrix[1, 1]) + (dataPtr_aux + nChan)[0] * (matrix[2, 0] + matrix[2, 1]) + (dataPtr_aux + widthstep - nChan)[0] * matrix[0, 2] + (dataPtr_aux + widthstep)[0] * matrix[1, 2] + (dataPtr_aux + widthstep + nChan)[0] * matrix[2, 2]) / matrixWeight) + offset);
                    green = (int)Math.Round((((dataPtr_aux - nChan)[1] * (matrix[0, 0] + matrix[0, 1]) + dataPtr_aux[1] * (matrix[1, 0] + matrix[1, 1]) + (dataPtr_aux + nChan)[1] * (matrix[2, 0] + matrix[2, 1]) + (dataPtr_aux + widthstep - nChan)[1] * matrix[0, 2] + (dataPtr_aux + widthstep)[1] * matrix[1, 2] + (dataPtr_aux + widthstep + nChan)[1] * matrix[2, 2]) / matrixWeight) + offset);
                    red = (int)Math.Round((((dataPtr_aux - nChan)[2] * (matrix[0, 0] + matrix[0, 1]) + dataPtr_aux[2] * (matrix[1, 0] + matrix[1, 1]) + (dataPtr_aux + nChan)[2] * (matrix[2, 0] + matrix[2, 1]) + (dataPtr_aux + widthstep - nChan)[2] * matrix[0, 2] + (dataPtr_aux + widthstep)[2] * matrix[1, 2] + (dataPtr_aux + widthstep + nChan)[2] * matrix[2, 2]) / matrixWeight) + offset);

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

                    (dataPtr + x * nChan)[0] = (byte)blue;
                    (dataPtr + x * nChan)[1] = (byte)green;
                    (dataPtr + x * nChan)[2] = (byte)red;
                }
                //bottom line: y = height
                for (x = 1; x < width - 1; x++)
                {
                    dataPtr_aux = (dataPtrC + (height - 1) * widthstep + x * nChan);
                    blue = (int)Math.Round((((dataPtr_aux - nChan)[0] * (matrix[0, 1] + matrix[0, 2]) + dataPtr_aux[0] * (matrix[1, 1] + matrix[1, 2]) + (dataPtr_aux + nChan)[0] * (matrix[2, 1] + matrix[2, 2]) + (dataPtr_aux - widthstep - nChan)[0] * matrix[0, 0] + (dataPtr_aux - widthstep)[0] * matrix[1, 0] + (dataPtr_aux - widthstep + nChan)[0] * matrix[2, 0]) /matrixWeight) + offset);
                    green = (int)Math.Round((((dataPtr_aux - nChan)[1] * (matrix[0, 1] + matrix[0, 2]) + dataPtr_aux[1] * (matrix[1, 1] + matrix[1, 2]) + (dataPtr_aux + nChan)[1] * (matrix[2, 1] + matrix[2, 2]) + (dataPtr_aux - widthstep - nChan)[1] * matrix[0, 0] + (dataPtr_aux - widthstep)[1] * matrix[1, 0] + (dataPtr_aux - widthstep + nChan)[1] * matrix[2, 0]) /matrixWeight) + offset);
                    red = (int)Math.Round((((dataPtr_aux - nChan)[2] * (matrix[0, 1] + matrix[0, 2]) + dataPtr_aux[2] * (matrix[1, 1] + matrix[1, 2]) + (dataPtr_aux + nChan)[2] * (matrix[2, 1] + matrix[2, 2]) + (dataPtr_aux - widthstep - nChan)[2] * matrix[0, 0] + (dataPtr_aux - widthstep)[2] * matrix[1, 0] + (dataPtr_aux - widthstep + nChan)[2] * matrix[2, 0]) /matrixWeight) + offset);

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

                    (dataPtr + (height - 1) * widthstep + x * nChan)[0] = (byte)blue;
                    (dataPtr + (height - 1) * widthstep + x * nChan)[1] = (byte)green;
                    (dataPtr + (height - 1) * widthstep + x * nChan)[2] = (byte)red;
                }
                //left line: x = 0
                for (y = 1; y < height - 1; y++)
                {
                    dataPtr_aux = (dataPtrC + y * widthstep);
                    blue = (int)Math.Round((((dataPtr_aux - widthstep)[0] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_aux[0] * (matrix[0, 1] + matrix[1, 1]) + (dataPtr_aux + widthstep)[0] * (matrix[0, 2] + matrix[1, 2]) + (dataPtr_aux - widthstep + nChan)[0] * matrix[2, 0] + (dataPtr_aux + nChan)[0] * matrix[2, 1] + (dataPtr_aux + widthstep + nChan)[0] * matrix[2, 2]) / matrixWeight) + offset);
                    green = (int)Math.Round((((dataPtr_aux - widthstep)[1] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_aux[1] * (matrix[0, 1] + matrix[1, 1]) + (dataPtr_aux + widthstep)[1] * (matrix[0, 2] + matrix[1, 2]) + (dataPtr_aux - widthstep + nChan)[1] * matrix[2, 0] + (dataPtr_aux + nChan)[1] * matrix[2, 1] + (dataPtr_aux + widthstep + nChan)[1] * matrix[2, 2]) / matrixWeight) + offset);
                    red = (int)Math.Round((((dataPtr_aux - widthstep)[2] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_aux[2] * (matrix[0, 1] + matrix[1, 1]) + (dataPtr_aux + widthstep)[2] * (matrix[0, 2] + matrix[1, 2]) + (dataPtr_aux - widthstep + nChan)[2] * matrix[2, 0] + (dataPtr_aux + nChan)[2] * matrix[2, 1] + (dataPtr_aux + widthstep + nChan)[2] * matrix[2, 2]) / matrixWeight) + offset);

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

                    (dataPtr + y * widthstep)[0] = (byte)blue;
                    (dataPtr + y * widthstep)[1] = (byte)green;
                    (dataPtr + y * widthstep)[2] = (byte)red;
                }
                //right line: x = width
                for (y = 1; y < height - 1; y++)
                {
                    dataPtr_aux = (dataPtrC + y * widthstep + (width - 1) * nChan);
                    blue = (int)Math.Round((((dataPtr_aux - widthstep)[0] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_aux[0] * (matrix[1, 1] + matrix[2, 1]) + (dataPtr_aux + widthstep)[0] * (matrix[1, 2] + matrix[2, 2]) + (dataPtr_aux - widthstep - nChan)[0] * matrix[0, 0] + (dataPtr_aux - nChan)[0] * matrix[0, 1] + (dataPtr_aux + widthstep - nChan)[0] * matrix[0, 2]) / matrixWeight) + offset);
                    green = (int)Math.Round((((dataPtr_aux - widthstep)[1] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_aux[1] * (matrix[1, 1] + matrix[2, 1]) + (dataPtr_aux + widthstep)[1] * (matrix[1, 2] + matrix[2, 2]) + (dataPtr_aux - widthstep - nChan)[1] * matrix[0, 0] + (dataPtr_aux - nChan)[1] * matrix[0, 1] + (dataPtr_aux + widthstep - nChan)[1] * matrix[0, 2]) / matrixWeight) + offset);
                    red = (int)Math.Round((((dataPtr_aux - widthstep)[2] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_aux[2] * (matrix[1, 1] + matrix[2, 1]) + (dataPtr_aux + widthstep)[2] * (matrix[1, 2] + matrix[2, 2]) + (dataPtr_aux - widthstep - nChan)[2] * matrix[0, 0] + (dataPtr_aux - nChan)[2] * matrix[0, 1] + (dataPtr_aux + widthstep - nChan)[2] * matrix[0, 2]) / matrixWeight) + offset);

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

                    (dataPtr + y * widthstep + (width - 1) * nChan)[0] = (byte)blue;
                    (dataPtr + y * widthstep + (width - 1) * nChan)[1] = (byte)green;
                    (dataPtr + y * widthstep + (width - 1) * nChan)[2] = (byte)red;
                }

                //for center pixels
                for (y = 1; y < height - 1; y++)
                {
                    for (x = 1; x < width - 1; x++)
                    {
                        dataPtr_aux = dataPtrC + y * widthstep + x * nChan;
                        red = 0;
                        green = 0;
                        blue = 0;

                        //to sum up pixels
                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                blue +=  (int)((dataPtr_aux + j * widthstep + i * nChan)[0] * matrix[i+1, j+1]);
                                green += (int)((dataPtr_aux + j * widthstep + i * nChan)[1] * matrix[i+1, j+1]);
                                red +=   (int)((dataPtr_aux + j * widthstep + i * nChan)[2] * matrix[i+1, j+1]);
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
                        (dataPtr + y * widthstep + x * nChan)[0] = (byte)blue;
                        (dataPtr + y * widthstep + x * nChan)[1] = (byte)green;
                        (dataPtr + y * widthstep + x * nChan)[2] = (byte)red;
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

                byte* dataPtr_aux = null;

                int w = img.Width;
                int h = img.Height;
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nChan * w;
                int x, y;
                int red, green, blue, redx, greenx, bluex, redy, greeny, bluey;
                int[] arr = new int[] { 1, 2, 1 };

                //for center pixels
                for (y = 1; y < h - 1; y++)
                {
                    for (x = 1; x < w - 1; x++)
                    {
                        dataPtr_aux = dataPtrC + y * widthstep + x * nChan;
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
                                if (j != 0)
                                {
                                    bluex += (int)((dataPtr_aux + i * widthstep + j * nChan)[0] * -j * arr[i + 1]);
                                    greenx += (int)((dataPtr_aux + i * widthstep + j * nChan)[1] * -j * arr[i + 1]);
                                    redx += (int)((dataPtr_aux + i * widthstep + j * nChan)[2] * -j * arr[i + 1]);
                                }
                            }
                        }

                        //Sy = (g+2h+i)-(a+2b+c) with x+1 and x-1
                        for (int j = -1; j < 2; j++)
                        {
                            for (int i = -1; i < 2; i++)
                            {
                                if (j != 0)
                                {
                                    bluey += (int)((dataPtr_aux + j * widthstep + i * nChan)[0] * j * arr[i + 1]);
                                    greeny += (int)((dataPtr_aux + j * widthstep + i * nChan)[1] * j * arr[i + 1]);
                                    redy += (int)((dataPtr_aux + j * widthstep + i * nChan)[2] * j * arr[i + 1]);
                                }
                            }
                        }

                        blue = Math.Abs(bluex) + Math.Abs(bluey);
                        green= Math.Abs(greenx) + Math.Abs(greeny);
                        red = Math.Abs(redx) + Math.Abs(redy);

                        if (red > 255)
                            red = 255;
                        if (green > 255)
                            green = 255;
                        if (blue > 255)
                            blue = 255;
                        

                        //put in location
                        (dataPtr + y * widthstep + x * nChan)[0] = (byte)blue;
                        (dataPtr + y * widthstep + x * nChan)[1] = (byte)green;
                        (dataPtr + y * widthstep + x * nChan)[2] = (byte)red;
                    }
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

                byte* dataPtr_aux = null;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nChan * width;
                int x, y;
                int red, green, blue;

                //for right pixels: just compare to pixel down
                for (y = 0; y < height-2; y++)
                {
                    dataPtr_aux = dataPtrC + y * widthstep + (width-1) * nChan;
                    (dataPtr + y * widthstep + (width - 1) * nChan)[0] = (byte)Math.Abs(dataPtr_aux[0] - (dataPtr_aux + widthstep)[0]);
                    (dataPtr + y * widthstep + (width - 1) * nChan)[1] = (byte)Math.Abs(dataPtr_aux[1] - (dataPtr_aux + widthstep)[1]);
                    (dataPtr + y * widthstep + (width - 1) * nChan)[2] = (byte)Math.Abs(dataPtr_aux[2] - (dataPtr_aux + widthstep)[2]);
                }
                //for bottom pixels: just compare to right pixel
                for (x = 0; x < width-2; x++)
                {
                    dataPtr_aux = dataPtrC + (height - 1) * widthstep + x * nChan;
                    (dataPtr + (height - 1) * widthstep + x * nChan)[0] = (byte)Math.Abs(dataPtr_aux[0] - (dataPtr_aux + nChan)[0]);
                    (dataPtr + (height - 1) * widthstep + x * nChan)[1] = (byte)Math.Abs(dataPtr_aux[1] - (dataPtr_aux + nChan)[1]);
                    (dataPtr + (height - 1) * widthstep + x * nChan)[2] = (byte)Math.Abs(dataPtr_aux[2] - (dataPtr_aux + nChan)[2]);
                }

                //for center pixels
                for (x = 0; x < width-2; x++)
                {
                    for (y = 0; y < height-2; y++)
                    {
                        dataPtr_aux = dataPtrC + y * widthstep + x * nChan;
                        blue = Math.Abs(dataPtr_aux[0] - (dataPtr_aux + widthstep)[0]) + Math.Abs(dataPtr_aux[0] - (dataPtr_aux + nChan)[0]);
                        green = Math.Abs(dataPtr_aux[1] - (dataPtr_aux + widthstep)[1]) + Math.Abs(dataPtr_aux[1] - (dataPtr_aux + nChan)[1]);
                        red = Math.Abs(dataPtr_aux[2] - (dataPtr_aux + widthstep)[2]) + Math.Abs(dataPtr_aux[2] - (dataPtr_aux + nChan)[2]);

                        if (red > 255)
                            red = 255;
                        if (green > 255)
                            green = 255;
                        if (blue > 255)
                            blue = 255;

                        (dataPtr + y * widthstep + x * nChan)[0] = (byte)blue;
                        (dataPtr + y * widthstep + x * nChan)[1] = (byte)green;
                        (dataPtr + y * widthstep + x * nChan)[2] = (byte)red;
                    }
                }

                //for last pixel - zero
                (dataPtr + (height - 1) * widthstep + (width - 1) * nChan)[0] = (byte)0;
                (dataPtr + (height - 1) * widthstep + (width - 1) * nChan)[1] = (byte)0;
                (dataPtr + (height - 1) * widthstep + (width - 1) * nChan)[2] = (byte)0;
            }
        }

        public static void Median(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                // get pointer to the start of the pictures
                MIplImage m = img.MIplImage;
                MIplImage mc = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte* dataPtrC = (byte*)mc.imageData.ToPointer();

                byte* dataPtr_aux = null;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nChan * width;
                int x, y;
                int red, green, blue;

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
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int x, y, gray;
                int[] array = new int[256];

                for (x= 0; x < width; x++)
                {
                    for (y= 0; y < height; y++)
                    {
                        gray = (int)Math.Round(((dataPtr + y * widthstep + x * nChan)[0] + (dataPtr + y * widthstep + x * nChan)[1] + (dataPtr + y * widthstep + x * nChan)[2]) / 3.0);

                        if (gray > 255)
                            gray = 255;

                        array[gray] += 1;
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
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int x, y;
                int[,] matrix = new int[3, 256];

                for (x = 0; x < width; x++)
                {
                    for (y = 0; y < height; y++)
                    {
                        matrix[0, (int)(dataPtr + y * widthstep + x * nChan)[0]] += 1;
                        matrix[1, (int)(dataPtr + y * widthstep + x * nChan)[1]] += 1;
                        matrix[2, (int)(dataPtr + y * widthstep + x * nChan)[2]] += 1;
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
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int x, y, gray;
                int[,] matrix = new int[4, 256];

                for (x = 0; x < width; x++)
                {
                    for (y = 0; y < height; y++)
                    {
                        gray = (int)Math.Round(((dataPtr + y * widthstep + x * nChan)[0] + (dataPtr + y * widthstep + x * nChan)[1] + (dataPtr + y * widthstep + x * nChan)[2]) / 3.0);

                        if (gray > 255)
                            gray = 255;

                        matrix[0, (int)(dataPtr + y * widthstep + x * nChan)[0]] += 1;
                        matrix[1, (int)(dataPtr + y * widthstep + x * nChan)[1]] += 1;
                        matrix[2, (int)(dataPtr + y * widthstep + x * nChan)[2]] += 1;
                        matrix[3, gray] += 1;
                    }
                }
                return matrix;
            }
        }



    }
}