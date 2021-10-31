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
                    // acesso directo : mais lento 
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
                // obter apontador do inicio da imagem
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
                            // calcula endereço do pixel no ponto (x,y)
                            dataPtr_aux = (dataPtrC + y0 * widthstep + x0 * nChan); //point to pixel
                            //store in the image
                            (dataPtr + y * widthstep + x * nChan)[0] = dataPtr_aux[0];
                            (dataPtr + y * widthstep + x * nChan)[1] = dataPtr_aux[1];
                            (dataPtr + y * widthstep + x * nChan)[2] = dataPtr_aux[2];

                        }
                        else //pixeis de fora ficam a preto
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
                // obter apontador do inicio da imagem
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
                            // calcula endereço do pixel no ponto (x,y)
                            dataPtr_aux = (dataPtrC + y0 * widthstep + x0 * nChan); //point to pixel
                            //store in the image 
                            (dataPtr + y * widthstep + x * nChan)[0] = dataPtr_aux[0];
                            (dataPtr + y * widthstep + x * nChan)[1] = dataPtr_aux[1];
                            (dataPtr + y * widthstep + x * nChan)[2] = dataPtr_aux[2];

                        }
                        else //pixeis de fora ficam a preto
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
                // obter apontador do inicio da imagem
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
                            // calcula endereço do pixel no ponto (x,y)
                            dataPtr_aux = (dataPtrC + y0 * widthstep + x0 * nChan); //point to pixel
                            //store in the image 
                            (dataPtr + y * widthstep + x * nChan)[0] = dataPtr_aux[0];
                            (dataPtr + y * widthstep + x * nChan)[1] = dataPtr_aux[1];
                            (dataPtr + y * widthstep + x * nChan)[2] = dataPtr_aux[2];

                        }
                        else //pixeis de fora ficam a preto
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
                // obter apontador do inicio da imagem
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
                            // calcula endereço do pixel no ponto (x,y)
                            dataPtr_aux = (dataPtrC + y0 * widthstep + x0 * nChan); //point to pixel
                            //store in the image 
                            (dataPtr + y * widthstep + x * nChan)[0] = dataPtr_aux[0];
                            (dataPtr + y * widthstep + x * nChan)[1] = dataPtr_aux[1];
                            (dataPtr + y * widthstep + x * nChan)[2] = dataPtr_aux[2];

                        }
                        else //pixeis de fora ficam a preto
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
                // obter apontador do inicio da imagem
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
                dataPtr[0] = (byte)Math.Round((dataPtrC[0] * 4 + (dataPtrC[0] + widthstep) * 2 + (dataPtrC[0] + nChan) * 2 + (dataPtrC[0] + widthstep + nChan)) / 9.0);
                dataPtr[1] = (byte)Math.Round((dataPtrC[1] * 4 + (dataPtrC[1] + widthstep) * 2 + (dataPtrC[1] + nChan) * 2 + (dataPtrC[1] + widthstep + nChan)) / 9.0);
                dataPtr[2] = (byte)Math.Round((dataPtrC[2] * 4 + (dataPtrC[2] + widthstep) * 2 + (dataPtrC[2] + nChan) * 2 + (dataPtrC[2] + widthstep + nChan)) / 9.0);
                //x = 0 and y = height
                dataPtr_aux = (dataPtrC + height * widthstep);
                (dataPtr + height * widthstep)[0] = (byte)Math.Round((dataPtr_aux[0] * 4 + (dataPtr_aux[0] - widthstep) * 2 + (dataPtr_aux[0] + nChan) * 2 + (dataPtr_aux[0] - widthstep - nChan)) / 9.0);
                (dataPtr + height * widthstep)[1] = (byte)Math.Round((dataPtr_aux[1] * 4 + (dataPtr_aux[1] - widthstep) * 2 + (dataPtr_aux[1] + nChan) * 2 + (dataPtr_aux[1] - widthstep - nChan)) / 9.0);
                (dataPtr + height * widthstep)[2] = (byte)Math.Round((dataPtr_aux[2] * 4 + (dataPtr_aux[2] - widthstep) * 2 + (dataPtr_aux[2] + nChan) * 2 + (dataPtr_aux[2] - widthstep - nChan)) / 9.0);
                //x = width and y = height
                dataPtr_aux = (dataPtrC + height * widthstep + width * nChan);
                (dataPtr + height * widthstep + width * nChan)[0] = (byte)Math.Round((dataPtr_aux[0] * 4 + (dataPtr_aux[0] - widthstep) * 2 + (dataPtr_aux[0] - nChan) * 2 + (dataPtr_aux[0] - widthstep - nChan)) / 9.0);
                (dataPtr + height * widthstep + width * nChan)[1] = (byte)Math.Round((dataPtr_aux[1] * 4 + (dataPtr_aux[1] - widthstep) * 2 + (dataPtr_aux[1] - nChan) * 2 + (dataPtr_aux[1] - widthstep - nChan)) / 9.0);
                (dataPtr + height * widthstep + width * nChan)[2] = (byte)Math.Round((dataPtr_aux[2] * 4 + (dataPtr_aux[2] - widthstep) * 2 + (dataPtr_aux[2] - nChan) * 2 + (dataPtr_aux[2] - widthstep - nChan)) / 9.0);
                //x = width and y = 0
                dataPtr_aux = (dataPtrC + width * nChan); 
                (dataPtr + width * nChan)[0] = (byte)Math.Round((dataPtr_aux[0] * 4 + (dataPtr_aux[0] + widthstep) * 2 + (dataPtr_aux[0] - nChan) * 2 + (dataPtr_aux[0] + widthstep - nChan)) / 9.0);
                (dataPtr + width * nChan)[1] = (byte)Math.Round((dataPtr_aux[1] * 4 + (dataPtr_aux[1] + widthstep) * 2 + (dataPtr_aux[1] - nChan) * 2 + (dataPtr_aux[1] + widthstep - nChan)) / 9.0);
                (dataPtr + width * nChan)[2] = (byte)Math.Round((dataPtr_aux[1] * 4 + (dataPtr_aux[1] + widthstep) * 2 + (dataPtr_aux[1] - nChan) * 2 + (dataPtr_aux[1] + widthstep - nChan)) / 9.0);
                
                //2 - for border other cases
                //line top: y = 0
                for (x = 1; x < width - 1; x++)
                {
                    dataPtr_aux = (dataPtrC + x * nChan);
                    (dataPtr + x * nChan)[0] = (byte)Math.Round(((dataPtr_aux[0] - nChan) * 2 + dataPtr_aux[0] * 2 + (dataPtr_aux[0] + nChan) * 2 + (dataPtr_aux[0] + widthstep - nChan) + (dataPtr_aux[0] + widthstep) + (dataPtr_aux[0] + widthstep + nChan)) / 9.0);
                    (dataPtr + x * nChan)[1] = (byte)Math.Round(((dataPtr_aux[1] - nChan) * 2 + dataPtr_aux[1] * 2 + (dataPtr_aux[1] + nChan) * 2 + (dataPtr_aux[1] + widthstep - nChan) + (dataPtr_aux[1] + widthstep) + (dataPtr_aux[1] + widthstep + nChan)) / 9.0);
                    (dataPtr + x * nChan)[2] = (byte)Math.Round(((dataPtr_aux[2] - nChan) * 2 + dataPtr_aux[2] * 2 + (dataPtr_aux[2] + nChan) * 2 + (dataPtr_aux[2] + widthstep - nChan) + (dataPtr_aux[2] + widthstep) + (dataPtr_aux[2] + widthstep + nChan)) / 9.0);
                }
                //bottom line: y = height
                for (x = 1; x < width - 1; x++)
                {
                    dataPtr_aux = (dataPtrC + height * widthstep + x * nChan);
                    (dataPtr + height * widthstep + x * nChan)[0] = (byte)Math.Round(((dataPtr_aux[0] - nChan) * 2 + dataPtr_aux[0] * 2 + (dataPtr_aux[0] + nChan) * 2 + (dataPtr_aux[0] - widthstep - nChan) + (dataPtr_aux[0] - widthstep) + (dataPtr_aux[0] - widthstep + nChan)) / 9.0);
                    (dataPtr + height * widthstep + x * nChan)[1] = (byte)Math.Round(((dataPtr_aux[1] - nChan) * 2 + dataPtr_aux[1] * 2 + (dataPtr_aux[1] + nChan) * 2 + (dataPtr_aux[1] - widthstep - nChan) + (dataPtr_aux[1] - widthstep) + (dataPtr_aux[1] - widthstep + nChan)) / 9.0);
                    (dataPtr + height * widthstep + x * nChan)[2] = (byte)Math.Round(((dataPtr_aux[2] - nChan) * 2 + dataPtr_aux[2] * 2 + (dataPtr_aux[2] + nChan) * 2 + (dataPtr_aux[2] - widthstep - nChan) + (dataPtr_aux[2] - widthstep) + (dataPtr_aux[2] - widthstep + nChan)) / 9.0);
                }
                //left line: x = 0
                /*
                for (y = 1; y < height - 1; y++)
                { 
                    dataPtr_aux = (dataPtrC + y * widthstep);
                    (dataPtr + y * widthstep)[0] = (byte)Math.Round(((dataPtr_aux[0] - widthstep) * 2 + dataPtr_aux[0] * 2 + (dataPtr_aux[0] + widthstep) * 2 + (dataPtr_aux[0] - widthstep + nChan) + (dataPtr_aux[0] + nChan) + (dataPtr_aux[0] + widthstep + nChan)) / 9.0);
                    (dataPtr + y * widthstep)[1] = (byte)Math.Round(((dataPtr_aux[1] - widthstep) * 2 + dataPtr_aux[1] * 2 + (dataPtr_aux[1] + widthstep) * 2 + (dataPtr_aux[1] - widthstep + nChan) + (dataPtr_aux[1] + nChan) + (dataPtr_aux[1] + widthstep + nChan)) / 9.0);
                    (dataPtr + y * widthstep)[2] = (byte)Math.Round(((dataPtr_aux[2] - widthstep) * 2 + dataPtr_aux[2] * 2 + (dataPtr_aux[2] + widthstep) * 2 + (dataPtr_aux[2] - widthstep + nChan) + (dataPtr_aux[2] + nChan) + (dataPtr_aux[2] + widthstep + nChan)) / 9.0);
                }
                //right line: x = width
                for (y = 1; y < height - 1; y++)
                {
                    dataPtr_aux = (dataPtrC + y * widthstep + width * nChan);
                    (dataPtr + y * widthstep + width * nChan)[0] = (byte)Math.Round(((dataPtr_aux[0] - widthstep) * 2 + dataPtr_aux[0] * 2 + (dataPtr_aux[0] + widthstep) * 2 + (dataPtr_aux[0] - widthstep - nChan) + (dataPtr_aux[0] - nChan) + (dataPtr_aux[0] + widthstep - nChan)) / 9.0);
                    (dataPtr + y * widthstep + width * nChan)[1] = (byte)Math.Round(((dataPtr_aux[1] - widthstep) * 2 + dataPtr_aux[1] * 2 + (dataPtr_aux[1] + widthstep) * 2 + (dataPtr_aux[1] - widthstep - nChan) + (dataPtr_aux[1] - nChan) + (dataPtr_aux[1] + widthstep - nChan)) / 9.0);
                    (dataPtr + y * widthstep + width * nChan)[2] = (byte)Math.Round(((dataPtr_aux[2] - widthstep) * 2 + dataPtr_aux[2] * 2 + (dataPtr_aux[2] + widthstep) * 2 + (dataPtr_aux[2] - widthstep - nChan) + (dataPtr_aux[2] - nChan) + (dataPtr_aux[2] + widthstep - nChan)) / 9.0);
                }
                */
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
                // obter apontador do inicio da imagems
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
            }
        }


    }
}