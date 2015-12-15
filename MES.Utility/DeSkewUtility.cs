using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Diagnostics;
using System.Collections.Generic;


public class DeSkewUtility
{

    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

    }
    // Representation of a line in the image.
    public class HougLine
    {
        //' Count of points in the line.
        public int Count;
        //' Index in Matrix.
        public int Index;
        //' The line is represented as all x,y that solve y*cos(alpha)-x*sin(alpha)=d
        public double Alpha;
        public double d;

    }

    // The Bitmap
    Bitmap cBmp;

    // The range of angles to search for lines
    double cAlphaStart = -20;
    double cAlphaStep = 0.2;
    int cSteps = 40 * 5;
    // Precalculation of sin and cos.
    double[] cSinA;
    double[] cCosA;
    // Range of d
    double cDMin;
    double cDStep = 1;
    int cDCount;
    // Count of points that fit in a line.
    int[] cHMatrix;
    public int iCounter = 0;
    public Bitmap bmpNonIndexedImage;
    // calculate the skew angle of the image cBmp
    public int[] Degrees = new int[360];


    List<Coordinate> CurrentPoints = new List<Coordinate>();


    public Boolean debug = true;

    public double GetSkewAngle()
    {
        int iMaxDegree = 0;

        //HougLine[] hl;
        //int i;
        //double sum = 0;
        //int count = 0;

        //' Hough Transformation
        Calc();
        //' Top 10 of the detected lines in the image.
        //hl = GetTop(10);
        ////' Average angle of the lines
        //for (i = 0; i < 9; i++)
        //{
        //    sum += hl[i].Alpha;
        //    count += 1;
        //}




        //return sum / count;

        // get most common
        for (int iDegree = 0; iDegree < 360; iDegree++)
        {
            if (Degrees[iDegree] > Degrees[iMaxDegree])
            {
                iMaxDegree = iDegree;
            }

        }

        return iMaxDegree;
    }


    public void GetAnglesForBlock()
    {
        double dDeltaX;
        double dDeltaY;
        int iAngle;

        // run through the current points and get the angles
        for (int iCurrentPointIndex = 0; iCurrentPointIndex < CurrentPoints.Count; iCurrentPointIndex++)
        {
            for (int iComparePointIndex = iCurrentPointIndex + 1; iComparePointIndex < CurrentPoints.Count; iComparePointIndex++)
            {
                // Get the angle between the two points
                dDeltaX = CurrentPoints[iCurrentPointIndex].X - CurrentPoints[iComparePointIndex].X;
                dDeltaY = CurrentPoints[iCurrentPointIndex].Y - CurrentPoints[iComparePointIndex].Y;

                iAngle = Convert.ToInt32(Math.Atan(dDeltaY / dDeltaX) * 180 / Math.PI);

                if (iAngle < 0)
                {
                    iAngle = 360 + iAngle;
                }
                Degrees[iAngle]++;

            }

        }
    }
    //    ' Calculate the Count lines in the image with most points.
    //private HougLine[] GetTop(int Count)
    //{
    //HougLine[] hl;
    //int j;
    //HougLine tmp;
    //int AlphaIndex, dIndex;
    //hl = new HougLine[Count];
    //for (int i = 0; i < Count; i++)
    //{
    //    hl[i] = new HougLine();
    //}
    //for (int i = 0; i < cHMatrix.Length - 1; i++)
    //{
    //    if (cHMatrix[i] > hl[Count - 1].Count)
    //    {
    //        hl[Count - 1].Count = cHMatrix[i];
    //        hl[Count - 1].Index = i;
    //        j = Count - 1;
    //        while (j > 0 && hl[j].Count > hl[j - 1].Count)
    //        {
    //            tmp = hl[j];
    //            hl[j] = hl[j - 1];
    //            hl[j - 1] = tmp;
    //            j -= 1;
    //        }
    //    }
    //}
    //for (int i = 0; i < Count; i++)
    //{
    //    dIndex = hl[i].Index / cSteps;
    //    AlphaIndex = hl[i].Index - dIndex * cSteps;
    //    hl[i].Alpha = GetAlpha(AlphaIndex);
    //    hl[i].d = dIndex + cDMin;
    //}
    //return hl;
    //}
    public void New(Bitmap bmp)
    {
        cBmp = bmp;
        bmpNonIndexedImage = new Bitmap(cBmp.Width, cBmp.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

    }

    //    ' Hough Transforamtion:
    private void Calc()
    {

        CurrentPoints.Clear();
        int iXBlocks = 10;
        int iYBlocks = 10;
        int iBlockWidth = cBmp.Width / iXBlocks;
        int iBlockHeight = cBmp.Height / iYBlocks;


        int x;
        int y;

        int hMax = cBmp.Height - 1;
        int wMax = cBmp.Width;

        //Init();

        // Process the columns
        for (int iCurrentXBlock = 1; iCurrentXBlock <= iXBlocks; iCurrentXBlock++)
        {
            // Process the rows
            for (int iCurrentYBlock = 1; iCurrentYBlock <= iYBlocks; iCurrentYBlock++)
            {

                // Process the current block
                for (y = (iCurrentYBlock - 1) * iBlockHeight + 1; y < iCurrentYBlock * iBlockHeight && y < hMax; y++)
                {
                    for (x = (iCurrentXBlock - 1) * iBlockWidth + 1; x < iCurrentXBlock * iBlockWidth && x < wMax; x++)
                    {
                        if (IsBlack(x, y))
                        {
                            if (IsWhite(x, y + 1))
                            {

                                Coordinate CurrentPoint = new Coordinate();
                                CurrentPoint.X = x;
                                CurrentPoint.Y = y;

                                CurrentPoints.Add(CurrentPoint);

                                // mark x,y
                                if (debug)
                                {
                                    bmpNonIndexedImage.SetPixel(x, y + 1, System.Drawing.Color.HotPink);

                                    iCounter++;
                                }


                            }
                            else
                            {

                                bmpNonIndexedImage.SetPixel(x, y, System.Drawing.Color.Black);

                            }

                        }
                        else
                        {

                            bmpNonIndexedImage.SetPixel(x, y, System.Drawing.Color.White);

                        }

                    }

                }

                // Process the current points
                GetAnglesForBlock();


                // clear the points in this block
                CurrentPoints.Clear();

            }

        }

    }


    private bool IsBlack(int x, int y)
    {
        Color c;
        double luminance;
        c = cBmp.GetPixel(x, y);
        //luminance = (c.R * 0.299) + (c.G * 0.587) + (c.B * 0.114);
        //return luminance > 140;

        luminance = c.R + c.G + c.B;
        return luminance < 120;
    }
    private bool IsWhite(int x, int y)
    {
        Color c;
        double luminance;
        c = cBmp.GetPixel(x, y);

        //luminance = (c.R * 0.299) + (c.G * 0.587) + (c.B * 0.114);
        //return luminance > 140;

        luminance = (c.R + c.G + c.B);
        return luminance > 140;
    }

    public static Bitmap RotateImage(Bitmap bmp, double angle)
    {
        Graphics g;
        Bitmap tmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppRgb);
        tmp.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
        g = Graphics.FromImage(tmp);
        try
        {
            g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
            g.RotateTransform((float)angle);
            g.DrawImage(bmp, 0, 0);
        }
        finally
        {
            g.Dispose();
        }
        return tmp;
    }
}