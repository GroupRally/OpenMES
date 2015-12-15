using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Collections.Specialized;
using System.Collections;

/// <summary>
/// Barcode imaging class by Berend Engelbrecht (b.engelbrecht@gmail.com).
/// See http://www.codeproject.com/KB/graphics/BarcodeImaging3.aspx
/// 
/// Parts of this class are derived from an earlier code project by James Fitch (qlipoth).
/// Used and published with permission of the original author.
/// 
/// Licensed under The Code Project Open License (CPOL). 
/// See http://www.codeproject.com/info/cpol10.aspx
/// </summary>
public class BarcodeImaging
{
  #region Public types (used in public function parameters)
  /// <summary>
  /// Used to specify what barcode type(s) to detect.
  /// </summary>
  public enum BarcodeType
  {
    /// <summary>Not specified</summary>
    None = 0,
    /// <summary>Code39</summary>
    Code39 = 1,
    /// <summary>EAN/UPC</summary>
    EAN = 2,
    /// <summary>Code128</summary>
    Code128 = 4,
    /// <summary>Use BarcodeType.All for all supported types</summary>
    All = Code39 | EAN | Code128

    // Note: Extend this enum with new types numbered as 8, 16, 32 ... ,
    //       so that we can use bitwise logic: All = Code39 | EAN | <your favorite type here> | ...
  }

  /// <summary>
  /// Used to specify whether to scan a page in vertical direction,
  /// horizontally, or both.
  /// </summary>
  public enum ScanDirection
  {
    /// <summary>Scan top-to-bottom</summary>
    Vertical = 1,
    /// <summary>Scan left-to-right</summary>
    Horizontal = 2
  }
  #endregion

  #region Private constants and types
  private struct BarcodeZone
  {
    public int Start;
    public int End;
  }

  /// <summary>
  /// Structure used to return the processed data from an image
  /// </summary>
  private class histogramResult
  {
    /// <summary>Averaged image brightness values over one scanned band</summary>
    public byte[] histogram;
    /// <summary>Minimum brightness (darkest)</summary>
    public byte min; // 
    /// <summary>Maximum brightness (lightest)</summary>
    public byte max;

    public byte threshold;     // threshold brightness to detect change from "light" to "dark" color
    public float lightnarrowbarwidth; // narrow bar width for light bars
    public float darknarrowbarwidth;  // narrow bar width for dark bars
    public float lightwiderbarwidth;  // width of most common wider bar for light bars
    public float darkwiderbarwidth;   // width of most common wider bar for dark bars

    public BarcodeZone[] zones; // list of zones on the current band that might contain barcode data
  }

  // General
  private const int GAPFACTOR = 48;        // width of quiet zone compared to narrow bar
  private const int MINNARROWBARCOUNT = 4; // minimum occurence of a narrow bar width

  // Code39
  private const string STARPATTERN = "nwnnwnwnn"; // the pattern representing a star in Code39
  private const float WIDEFACTOR = 2.0f; // minimum width of wide bar compared to narrow bar
  private const int MINPATTERNLENGTH = 10; // length of one barcode digit + gap

  // Code128
  private const int CODE128START = 103;    // Startcodes have index >= 103
  private const int CODE128STOP = 106; // Stopcode has index 106
  private const int CODE128C = 99;  // Switch to code page C
  private const int CODE128B = 100; // Switch to code page B
  private const int CODE128A = 101; // Switch to code page A


  #endregion

  #region Private member variables
  private static BarcodeType m_FullScanBarcodeTypes = BarcodeType.All;
  private static bool m_bUseBarcodeZones = true;
  #endregion

  #region Public properties used in configuration

  /// <summary>
  /// Barcode types to be detected in FullScanPage. Set this to a specific
  /// subset of types if you do not need "All" to be detected by default.
  /// </summary>
  public static BarcodeType FullScanBarcodeTypes
  {
    get { return m_FullScanBarcodeTypes; }
    set { m_FullScanBarcodeTypes = value; }
  }

  /// <summary>
  /// Set UseBarcodeZones to false if you do not need this feature.
  /// Barcode regions improve detection of multiple barcodes on one scan line,
  /// but have a significant performance impact.
  /// </summary>
  public static bool UseBarcodeZones
  {
    get { return m_bUseBarcodeZones; }
    set { m_bUseBarcodeZones = value; }
  }
  #endregion

  #region Public methods
  /// <summary>
  /// FullScanPage does a full scan of the active frame in the passed bitmap. This function
  /// will scan both vertically and horizontally. 
  /// </summary>
  /// <remarks>
  /// By default FullScanPage will attempt to detect barcodes of all supported types. Assign 
  /// a subset to FullScanBarcodeTypes if your application does not need this.
  /// 
  /// Use ScanPage instead of FullScanPage if you want to scan in one direction only, 
  /// or only for specific barcode types.
  /// 
  /// For a multi-page tiff only one page is scanned. By default, the first page is used, but
  /// you can scan other pages by calling bmp.SelectActiveFrame(FrameDimension.Page, pagenumber)
  /// before calling FullScanPage.
  /// </remarks>
  /// <param name="CodesRead">Will contain detected barcode strings when the function returns</param>
  /// <param name="bmp">Input bitmap</param>
  /// <param name="numscans">Number of passes that must be made over the page. 
  /// 50 - 100 usually gives a good result.</param>
  public static void FullScanPage(ref System.Collections.ArrayList CodesRead, Bitmap bmp, int numscans)
  {
    ScanPage(ref CodesRead, bmp, numscans, ScanDirection.Vertical, FullScanBarcodeTypes);
    ScanPage(ref CodesRead, bmp, numscans, ScanDirection.Horizontal, FullScanBarcodeTypes);
  }

  /// <summary>
  /// Scans the active frame in the passed bitmap for barcodes.
  /// </summary>
  /// <param name="CodesRead">Will contain detected barcode strings when the function returns</param>
  /// <param name="bmp">Input bitmap</param>
  /// <param name="numscans">Number of passes that must be made over the page. 
  /// 50 - 100 usually gives a good result.</param>
  /// <param name="direction">Scan direction</param>
  /// <param name="types">Barcode types. Pass BarcodeType.All, or you can specify a list of types,
  /// e.g., BarcodeType.Code39 | BarcodeType.EAN</param>
  public static void ScanPage(ref System.Collections.ArrayList CodesRead, Bitmap bmp, int numscans, ScanDirection direction, BarcodeType types)
  {
    int iHeight, iWidth;
    if (direction == ScanDirection.Horizontal)
    {
      iHeight = bmp.Width;
      iWidth = bmp.Height;
    }
    else
    {
      iHeight = bmp.Height;
      iWidth = bmp.Width;
    }
    if (numscans > iHeight) numscans = iHeight; // fix for doing full scan on small images
    for (int i = 0; i < numscans; i++)
    {
      int y1 = (i * iHeight) / numscans;
      int y2 = ((i + 1) * iHeight) / numscans;
      string sCodesRead = ReadBarcodes(bmp, y1, y2, direction, types);

      if ((sCodesRead != null) && (sCodesRead.Length > 0))
      {
        string[] asCodes = sCodesRead.Split('|');
        foreach (string sCode in asCodes)
        {
          if (!CodesRead.Contains(sCode))
            CodesRead.Add(sCode);
        }
      }
    }
  }

  /// <summary>
  /// Scans one band in the passed bitmap for barcodes. 
  /// </summary>
  /// <param name="bmp">Input bitmap</param>
  /// <param name="start">Start coordinate</param>
  /// <param name="end">End coordinate</param>
  /// <param name="direction">
  /// ScanDirection.Vertical: a horizontal band across the page will be examined 
  /// and start,end should be valid y-coordinates.
  /// ScanDirection.Horizontal: a vertical band across the page will be examined 
  /// and start,end should be valid x-coordinates.
  /// </param>
  /// <param name="types">Barcode types to be found</param>
  /// <returns>Pipe-separated list of barcodes, empty string if none were detected</returns>
  public static string ReadBarcodes(Bitmap bmp, int start, int end, ScanDirection direction, BarcodeType types)
  {
    string sBarCodes = "|"; // will hold return values

    // To find a horizontal barcode, find the vertical histogram to find individual barcodes, 
    // then get the vertical histogram to decode each
    histogramResult vertHist = verticalHistogram(bmp, start, end, direction);

    // Get the light/dark bar patterns.
    // GetBarPatterns returns the bar pattern in 2 formats: 
    //
    //   sbCode39Pattern: for Code39 (only distinguishes narrow bars "n" and wide bars "w")
    //   sbEANPattern: for EAN (distinguishes bar widths 1, 2, 3, 4 and L/G-code)
    //
    StringBuilder sbCode39Pattern;
    StringBuilder sbEANPattern;
    GetBarPatterns(ref vertHist, out sbCode39Pattern, out sbEANPattern);

    // We now have a barcode in terms of narrow & wide bars... Parse it!
    if ((sbCode39Pattern.Length > 0) || (sbEANPattern.Length > 0))
    {
      for (int iPass = 0; iPass < 2; iPass++)
      {
        if ((types & BarcodeType.Code39) != BarcodeType.None) // if caller wanted Code39
        {
          string sCode39 = ParseCode39(sbCode39Pattern);
          if (sCode39.Length > 0)
            sBarCodes += sCode39 + "|";
        }
        if ((types & BarcodeType.EAN) != BarcodeType.None) // if caller wanted EAN
        {
          string sEAN = ParseEAN(sbEANPattern);
          if (sEAN.Length > 0)
            sBarCodes += sEAN + "|";
        }
        if ((types & BarcodeType.Code128) != BarcodeType.None) // if caller wanted Code128
        {
          // Note: Code128 uses same bar width measurement data as EAN
          string sCode128 = ParseCode128(sbEANPattern);
          if (sCode128.Length > 0)
            sBarCodes += sCode128 + "|";
        }

        // Reverse the bar pattern arrays to read again in the mirror direction
        if (iPass == 0)
        {
          sbCode39Pattern = ReversePattern(sbCode39Pattern);
          sbEANPattern = ReversePattern(sbEANPattern);
        }
      }
    }

    // Return pipe-separated list of found barcodes, if any
    if (sBarCodes.Length > 2)
      return sBarCodes.Substring(1, sBarCodes.Length - 2);
    return string.Empty;
  }


  public static Bitmap RotateImage(Image image, float angle)
  {
      // center of the image
      float rotateAtX = image.Width / 2;
      float rotateAtY = image.Height / 2;
      bool bNoClip = true;
      return RotateImage(image, rotateAtX, rotateAtY, angle, bNoClip);
  }

  public static Bitmap RotateImage(Image image, float angle, bool bNoClip)
  {
      // center of the image
      float rotateAtX = image.Width / 2;
      float rotateAtY = image.Height / 2;
      return RotateImage(image, rotateAtX, rotateAtY, angle, bNoClip);
  }

  public static Bitmap RotateImage(Image image, float rotateAtX, float rotateAtY, float angle, bool bNoClip)
  {
      int W, H, X, Y;
      if (bNoClip)
      {
          double dW = (double)image.Width;
          double dH = (double)image.Height;

          double degrees = Math.Abs(angle);
          if (degrees <= 90)
          {
              double radians = 0.0174532925 * degrees;
              double dSin = Math.Sin(radians);
              double dCos = Math.Cos(radians);
              W = (int)(dH * dSin + dW * dCos);
              H = (int)(dW * dSin + dH * dCos);
              X = (W - image.Width) / 2;
              Y = (H - image.Height) / 2;
          }
          else
          {
              degrees -= 90;
              double radians = 0.0174532925 * degrees;
              double dSin = Math.Sin(radians);
              double dCos = Math.Cos(radians);
              W = (int)(dW * dSin + dH * dCos);
              H = (int)(dH * dSin + dW * dCos);
              X = (W - image.Width) / 2;
              Y = (H - image.Height) / 2;
          }
      }
      else
      {
          W = image.Width;
          H = image.Height;
          X = 0;
          Y = 0;
      }

      //create a new empty bitmap to hold rotated image
      Bitmap bmpRet = new Bitmap(W, H);
      bmpRet.SetResolution(image.HorizontalResolution, image.VerticalResolution);

      //make a graphics object from the empty bitmap
      Graphics g = Graphics.FromImage(bmpRet);

      //Put the rotation point in the "center" of the image
      g.TranslateTransform(rotateAtX + X, rotateAtY + Y);

      //rotate the image
      g.RotateTransform(angle);

      //move the image back
      g.TranslateTransform(-rotateAtX - X, -rotateAtY - Y);

      //draw passed in image onto graphics object
      g.DrawImage(image, new PointF(0 + X, 0 + Y));

      return bmpRet;
  }
  #endregion

  #region Private functions

  #region General
  /// <summary>
  /// Scans for patterns of bars and returns them encoded as strings in the passed
  /// string builder parameters.
  /// </summary>
  /// <param name="hist">Input data containing picture information for the scan line</param>
  /// <param name="sbCode39Pattern">Returns string containing "w" for wide bars and "n" for narrow bars</param>
  /// <param name="sbEANPattern">Returns string with numbers designating relative bar widths compared to 
  /// narrowest bar: "1" to "4" are valid widths that can be present in an EAN barcode</param>
  /// <remarks>In both output strings, "|"-characters will be inserted to indicate gaps 
  /// in the input data.</remarks>
  private static void GetBarPatterns(ref histogramResult hist, out StringBuilder sbCode39Pattern, out StringBuilder sbEANPattern)
  {
    // Initialize return data
    sbCode39Pattern = new StringBuilder();
    sbEANPattern = new StringBuilder();

    if (hist.zones != null) // if barcode zones were found along the scan line
    {
      for (int iZone = 0; iZone < hist.zones.Length; iZone++)
      {
        // Recalculate bar width distribution if more than one zone is present, it could differ per zone
        if (hist.zones.Length > 1)
          GetBarWidthDistribution(ref hist, hist.zones[iZone].Start, hist.zones[iZone].End);

        // Check the calculated narrow bar widths. If they are very different, the pattern is
        // unlikely to be a bar code
        if (ValidBars(ref hist))
        {
          // add gap separator to output patterns
          sbCode39Pattern.Append("|");
          sbEANPattern.Append("|");

          // Variables needed to check for
          int iBarStart = 0;
          bool bDarkBar = (hist.histogram[0] <= hist.threshold);

          // Find the narrow and wide bars
          for (int i = 1; i < hist.histogram.Length; ++i)
          {
            bool bDark = (hist.histogram[i] <= hist.threshold);
            if (bDark != bDarkBar)
            {
              int iBarWidth = i - iBarStart;
              float fNarrowBarWidth = bDarkBar ? hist.darknarrowbarwidth : hist.lightnarrowbarwidth;
              float fWiderBarWidth = bDarkBar ? hist.darkwiderbarwidth : hist.lightwiderbarwidth;
              if (IsWideBar(iBarWidth, fNarrowBarWidth, fWiderBarWidth))
              {
                // The bar was wider than the narrow bar width, it's a wide bar or a gap
                if (iBarWidth > GAPFACTOR * fNarrowBarWidth)
                {
                  sbCode39Pattern.Append("|");
                  sbEANPattern.Append("|");
                }
                else
                {
                  sbCode39Pattern.Append("w");
                  AppendEAN(sbEANPattern, iBarWidth, fNarrowBarWidth);
                }
              }
              else
              {
                // The bar is a narrow bar
                sbCode39Pattern.Append("n");
                AppendEAN(sbEANPattern, iBarWidth, fNarrowBarWidth);
              }
              bDarkBar = bDark;
              iBarStart = i;
            }
          }
        }
      }
    }
  }

  /// <summary>
  /// Returns true if the bar appears to be "wide".
  /// </summary>
  /// <param name="iBarWidth">measured bar width in pixels</param>
  /// <param name="fNarrowBarWidth">average narrow bar width</param>
  /// <param name="fWiderBarWidth">average width of next wider bar</param>
  /// <returns></returns>
  private static bool IsWideBar(int iBarWidth, float fNarrowBarWidth, float fWiderBarWidth)
  {
    if (fNarrowBarWidth < 4.0)
      return (iBarWidth > WIDEFACTOR * fNarrowBarWidth);
    return (iBarWidth >= fWiderBarWidth) || ((fWiderBarWidth - iBarWidth) < (iBarWidth - fNarrowBarWidth));
  }

  /// <summary>
  /// Checks if dark and light narrow bar widths are in agreement.
  /// </summary>
  /// <param name="hist">barcode data</param>
  /// <returns>true if barcode data is valid</returns>
  private static bool ValidBars(ref histogramResult hist)
  {
    float fCompNarrowBarWidths = hist.lightnarrowbarwidth / hist.darknarrowbarwidth;
    float fCompWiderBarWidths = hist.lightwiderbarwidth / hist.darkwiderbarwidth;
    return ((fCompNarrowBarWidths >= 0.5) && (fCompNarrowBarWidths <= 2.0)
         && (fCompWiderBarWidths >= 0.5) && (fCompWiderBarWidths <= 2.0)
         && (hist.darkwiderbarwidth / hist.darknarrowbarwidth >= 1.5)
         && (hist.lightwiderbarwidth / hist.lightnarrowbarwidth >= 1.5));
  }

  /// <summary>
  /// Used by ReadBarcodes to reverse a bar pattern. 
  /// </summary>
  /// <param name="sbPattern">String builder containing a bar pattern string</param>
  /// <returns>String builder containing the reverse of the input string</returns>
  private static StringBuilder ReversePattern(StringBuilder sbPattern)
  {
    if (sbPattern.Length > 0)
    {
      char[] acPattern = sbPattern.ToString().ToCharArray();
      Array.Reverse(acPattern);
      sbPattern = new StringBuilder(acPattern.Length);
      sbPattern.Append(acPattern);
    }
    return sbPattern;
  }

  /// <summary>
  /// Vertical histogram of an image
  /// </summary>
  /// <param name="bmp">Bitmap</param>
  /// <param name="start">Start coordinate of band to be scanned</param>
  /// <param name="end">End coordinate of band to be scanned</param>
  /// <param name="direction">
  /// ScanDirection.Vertical: start and end denote y-coordinates.
  /// ScanDirection.Horizontal: start and end denote x-coordinates.
  /// </param>
  /// <returns>histogramResult, containing average brightness values across the scan line</returns>
  private static histogramResult verticalHistogram(Bitmap bmp, int start, int end, ScanDirection direction)
  {
    // convert the pixel format of the bitmap to something that we can handle
    PixelFormat pf = CheckSupportedPixelFormat(bmp.PixelFormat);
    BitmapData bmData;
    int xMax, yMax;

    if (direction == ScanDirection.Horizontal)
    {
      bmData = bmp.LockBits(new Rectangle(start, 0, end - start, bmp.Height), ImageLockMode.ReadOnly, pf);
      xMax = bmData.Height;
      yMax = end - start;
    }
    else
    {
      bmData = bmp.LockBits(new Rectangle(0, start, bmp.Width, end - start), ImageLockMode.ReadOnly, pf);
      xMax = bmp.Width;
      yMax = bmData.Height;
    }

    // Create the return value
    byte[] histResult = new byte[xMax + 2]; // add 2 to simulate light-colored background pixels at sart and end of scanline
    ushort[] vertSum = new ushort[xMax];

    unsafe
    {
      byte* p = (byte*)(void*)bmData.Scan0;
      int stride = bmData.Stride;    // stride is offset between horizontal lines in p 

      for (int y = 0; y < yMax; ++y)
      {
        // Add up all the pixel values vertically
        for (int x = 0; x < xMax; ++x)
        {
          if (direction == ScanDirection.Horizontal)
            vertSum[x] += getpixelbrightness(p, pf, stride, y, x);
          else
            vertSum[x] += getpixelbrightness(p, pf, stride, x, y);
        }
      }
    }
    bmp.UnlockBits(bmData);

    // Now get the average of the row by dividing the pixel by num pixels
    int iDivider = end - start;
    if (pf != PixelFormat.Format1bppIndexed)
      iDivider *= 3;

    byte maxValue = byte.MinValue; // Start the max value at zero
    byte minValue = byte.MaxValue; // Start the min value at the absolute maximum

    for (int i = 1; i <= xMax; i++) // note: intentionally skips first pixel in histResult
    {
      histResult[i] = (byte)(vertSum[i - 1] / iDivider);
      //Save the max value for later
      if (histResult[i] > maxValue) maxValue = histResult[i];
      // Save the min value for later
      if (histResult[i] < minValue) minValue = histResult[i];
    }

    // Set first and last pixel to "white", i.e., maximum intensity
    histResult[0] = maxValue;
    histResult[xMax + 1] = maxValue;

    histogramResult retVal = new histogramResult();
    retVal.histogram = histResult;
    retVal.max = maxValue;
    retVal.min = minValue;

    // Now we have the brightness distribution along the scan band, try to find the distribution of bar widths.
    retVal.threshold = (byte)(minValue + ((maxValue - minValue) >> 1));
    GetBarWidthDistribution(ref retVal, 0, retVal.histogram.Length);

    // Now that we know the narrow bar width, lets look for barcode zones.
    // The image could have more than one barcode in the same band, with 
    // different bar widths.
    FindBarcodeZones(ref retVal);
    return retVal;
  }

  /// <summary>
  /// Gets the bar width distribution and calculates narrow bar width over the specified
  /// range of the histogramResult. A histogramResult could have multiple ranges, separated 
  /// by quiet zones.
  /// </summary>
  /// <param name="hist">histogramResult data</param>
  /// <param name="iStart">start coordinate to be considered</param>
  /// <param name="iEnd">end coordinate + 1</param>
  private static void GetBarWidthDistribution(ref histogramResult hist, int iStart, int iEnd)
  {
    HybridDictionary hdLightBars = new HybridDictionary();
    HybridDictionary hdDarkBars = new HybridDictionary();
    bool bDarkBar = (hist.histogram[iStart] <= hist.threshold);
    int iBarStart = 0;
    for (int i = iStart + 1; i < iEnd; i++)
    {
      bool bDark = (hist.histogram[i] <= hist.threshold);
      if (bDark != bDarkBar)
      {
        int iBarWidth = i - iBarStart;
        if (bDarkBar)
        {
          if (!hdDarkBars.Contains(iBarWidth))
            hdDarkBars.Add(iBarWidth, 1);
          else
            hdDarkBars[iBarWidth] = (int)hdDarkBars[iBarWidth] + 1;
        }
        else
        {
          if (!hdLightBars.Contains(iBarWidth))
            hdLightBars.Add(iBarWidth, 1);
          else
            hdLightBars[iBarWidth] = (int)hdLightBars[iBarWidth] + 1;
        }
        bDarkBar = bDark;
        iBarStart = i;
      }
    }

    // Now get the most common bar widths
    CalcNarrowBarWidth(hdLightBars, out hist.lightnarrowbarwidth, out hist.lightwiderbarwidth);
    CalcNarrowBarWidth(hdDarkBars, out hist.darknarrowbarwidth, out hist.darkwiderbarwidth);
  }

  private static void CalcNarrowBarWidth(HybridDictionary hdBarWidths, out float fNarrowBarWidth, out float fWiderBarWidth)
  {
    fNarrowBarWidth = 1.0f;
    fWiderBarWidth = 2.0f;
    if (hdBarWidths.Count > 1) // we expect at least two different bar widths in supported barcodes
    {
      int[] aiWidths = new int[hdBarWidths.Count];
      int[] aiCounts = new int[hdBarWidths.Count];
      int i = 0;
      foreach (int iKey in hdBarWidths.Keys)
      {
        aiWidths[i] = iKey;
        aiCounts[i] = (int)hdBarWidths[iKey];
        i++;
      }
      Array.Sort(aiWidths, aiCounts);

      // walk from lowest to highest width. The narrowest bar should occur at least 4 times
      fNarrowBarWidth = aiWidths[0];
      fWiderBarWidth = WIDEFACTOR * fNarrowBarWidth;
      for (i = 0; i < aiCounts.Length; i++)
      {
        if (aiCounts[i] >= MINNARROWBARCOUNT)
        {
          fNarrowBarWidth = aiWidths[i];
          if (fNarrowBarWidth < 3)
            fWiderBarWidth = WIDEFACTOR * fNarrowBarWidth;
          else
          {
            // if the width is not singular, look for the most common width in the neighbourhood
            float fCount;
            FindPeakWidth(i, ref aiWidths, ref aiCounts, out fNarrowBarWidth, out fCount);
            fWiderBarWidth = WIDEFACTOR * fNarrowBarWidth;

            if (fNarrowBarWidth >= 6)
            {
              // ... and for the next wider common bar width if the barcode is fairly large
              float fMaxCount = 0.0f;
              for (int j = i + 1; j < aiCounts.Length; j++)
              {
                float fNextWidth, fNextCount;
                FindPeakWidth(j, ref aiWidths, ref aiCounts, out fNextWidth, out fNextCount);
                if (fNextWidth / fNarrowBarWidth > 1.5)
                {
                  if (fNextCount > fMaxCount)
                  {
                    fWiderBarWidth = fNextWidth;
                    fMaxCount = fNextCount;
                  }
                  else
                    break;
                }
              }
            }
          }
          break;
        }
      }
    }
  }

  static void FindPeakWidth(int i, ref int[] aiWidths, ref int[] aiCounts, out float fWidth, out float fCount)
  {
    fWidth = 0.0f;
    fCount = 0.0f;
    int iSamples = 0;
    for (int j = i - 1; j <= i + 1; j++)
    {
      if ((j >= 0) && (j < aiWidths.Length) && (Math.Abs(aiWidths[j] - aiWidths[i]) == Math.Abs(j - i)))
      {
        iSamples++;
        fCount += aiCounts[j];
        fWidth += aiWidths[j] * aiCounts[j];
      }
    }
    fWidth /= fCount;
    fCount /= iSamples;
  }

  /// <summary>
  /// FindBarcodeZones looks for barcode zones in the current band. 
  /// We look for white space that is more than GAPFACTOR * narrowbarwidth
  /// separating two zones. For narrowbarwidth we take the maximum of the 
  /// dark and light narrow bar width.
  /// </summary>
  /// <param name="hist">Data for current image band</param>
  private static void FindBarcodeZones(ref histogramResult hist)
  {
    if (!ValidBars(ref hist))
      hist.zones = null;
    else if (!UseBarcodeZones)
    {
      hist.zones = new BarcodeZone[1];
      hist.zones[0].Start = 0;
      hist.zones[0].End = hist.histogram.Length;
    }
    else
    {
      ArrayList alBarcodeZones = new ArrayList();
      bool bDarkBar = (hist.histogram[0] <= hist.threshold);
      int iBarStart = 0;
      int iZoneStart = -1;
      int iZoneEnd = -1;
      float fQuietZoneWidth = GAPFACTOR * (hist.darknarrowbarwidth + hist.lightnarrowbarwidth) / 2;
      float fMinZoneWidth = fQuietZoneWidth;

      for (int i = 1; i < hist.histogram.Length; i++)
      {
        bool bDark = (hist.histogram[i] <= hist.threshold);
        if (bDark != bDarkBar)
        {
          int iBarWidth = i - iBarStart;
          if (!bDarkBar) // This ends a light area
          {
            if ((iZoneStart == -1) || (iBarWidth > fQuietZoneWidth))
            {
              // the light area can be seen as a quiet zone
              iZoneEnd = i - (iBarWidth >> 1);

              // Check if the active zone is big enough to contain a barcode
              if ((iZoneStart >= 0) && (iZoneEnd > iZoneStart + fMinZoneWidth))
              {
                // record the barcode zone that ended in the detected quiet zone ...
                BarcodeZone bz = new BarcodeZone();
                bz.Start = iZoneStart;
                bz.End = iZoneEnd;
                alBarcodeZones.Add(bz);

                // .. and start a new barcode zone
                iZoneStart = iZoneEnd;
              }
              if (iZoneStart == -1)
                iZoneStart = iZoneEnd; // first zone starts here
            }
          }
          bDarkBar = bDark;
          iBarStart = i;
        }
      }
      if (iZoneStart >= 0)
      {
        BarcodeZone bz = new BarcodeZone();
        bz.Start = iZoneStart;
        bz.End = hist.histogram.Length;
        alBarcodeZones.Add(bz);
      }
      if (alBarcodeZones.Count > 0)
        hist.zones = (BarcodeZone[])alBarcodeZones.ToArray(typeof(BarcodeZone));
      else
        hist.zones = null;
    }
  }

  /// <summary>
  /// Checks if the supplied pixelFormat is supported, returns the default
  /// pixel format (PixelFormat.Format24bppRgb) if it isn't supported.
  /// </summary>
  /// <param name="pixelFormat">Input pixel format</param>
  /// <returns>Input pixel format if it is supported, else default.</returns>
  private static PixelFormat CheckSupportedPixelFormat(PixelFormat pixelFormat)
  {
    switch (pixelFormat)
    {
      case PixelFormat.Format1bppIndexed:
      case PixelFormat.Format32bppArgb:
      case PixelFormat.Format32bppRgb:
        return pixelFormat;
    }
    return PixelFormat.Format24bppRgb;
  }

  /// <summary>
  /// Calculates pixel brightness for specified pixel in byte array of locked bitmap rectangle.
  /// For RGB  : returns sum of the three color values.
  /// For 1bpp : returns 255 for a white pixel, 0 for a black pixel.
  /// </summary>
  /// <param name="p">Byte array containing pixel information</param>
  /// <param name="pf">Pixel format used in the byte array</param>
  /// <param name="stride">Byte offset between scan lines</param>
  /// <param name="x">Horizontal coordinate, relative to upper left corner of locked rectangle</param>
  /// <param name="y">Vertical coordinate, relative to upper left corner of locked rectangle</param>
  /// <returns></returns>
  private static unsafe ushort getpixelbrightness(byte* p, PixelFormat pf, int stride, int x, int y)
  {
    ushort uBrightness = 0;
    switch (pf)
    {
      case PixelFormat.Format1bppIndexed:
        byte b = p[(y * stride) + (x >> 3)];
        if (((b << (x % 8)) & 128) != 0)
          uBrightness = 255;
        break;

      default: // 24bpp RGB, 32bpp formats
        int iByte = (y * stride) + (x * (pf == PixelFormat.Format24bppRgb ? 3 : 4));
        for (int i = iByte; i < iByte + 3; i++)
          uBrightness += p[i];
        break;
    }
    return uBrightness;
  }
  #endregion

  #region Code39-specific
  /// <summary>
  /// Parses Code39 barcodes from the input pattern.
  /// </summary>
  /// <param name="sbPattern">Input pattern, should contain "n"-characters to
  /// indicate narrow bars and "w" to indicate wide bars.</param>
  /// <returns>Pipe-separated list of barcodes, empty string if none were detected</returns>
  private static string ParseCode39(StringBuilder sbPattern)
  {
    // Each pattern within code 39 is nine bars with one white bar between each pattern
    if (sbPattern.Length > 9)
    {
      StringBuilder sbBarcodes = new StringBuilder();
      string sPattern = sbPattern.ToString();
      int iStarPattern = sPattern.IndexOf(STARPATTERN); // index of first star barcode in pattern
      while ((iStarPattern >= 0) && (iStarPattern <= sbPattern.Length - 9))
      {
        int iPos = iStarPattern;
        int iNoise = 0;
        StringBuilder sbData = new StringBuilder((int)(sbPattern.Length / 10));
        while (iPos <= sbPattern.Length - 9)
        {
          // Test the next 9 characters from the pattern string
          string sData = ParseCode39Pattern(sbPattern.ToString(iPos, 9));

          if (sData == null) // no recognizeable data
          {
            iPos++;
            iNoise++;
          }
          else
          {
            // record if the data contained a lot of noise before the next valid data
            if (iNoise >= 2)
              sbData.Append("|");
            iNoise = 0; // reset noise counter
            sbData.Append(sData);
            iPos += 10;
          }
        }
        if (sbData.Length > 0)
        {
          // look for valid Code39 patterns in the data.
          // A valid Code39 pattern starts and ends with "*" and does not contain a noise character "|".
          // We return a pipe-separated list of these patterns.
          string[] asBarcodes = sbData.ToString().Split('|');
          foreach (string sBarcode in asBarcodes)
          {
            if (sBarcode.Length > 2)
            {
              int iFirstStar = sBarcode.IndexOf("*");
              if ((iFirstStar >= 0) && (iFirstStar < sBarcode.Length - 1))
              {
                int iSecondStar = sBarcode.IndexOf("*", iFirstStar + 1);
                if (iSecondStar > iFirstStar + 1)
                {
                  sbBarcodes.Append(sBarcode.Substring(iFirstStar + 1, (iSecondStar - iFirstStar - 1)) + "|");
                }
              }
            }
          }
        }
        iStarPattern = sPattern.IndexOf(STARPATTERN, iStarPattern + 5); // "nwnnwnwnn" pattern can not occur again before current index + 5
      }
      if (sbBarcodes.Length > 1)
        return sbBarcodes.ToString(0, sbBarcodes.Length - 1);
    }
    return string.Empty;
  }

  /// <summary>
  /// Parses bar pattern for one Code39 character.
  /// </summary>
  /// <param name="pattern">Pattern to be examined, should be 9 characters</param>
  /// <returns>Detected character or null</returns>
  private static string ParseCode39Pattern(string pattern)
  {
    switch (pattern)
    {
      case "wnnwnnnnw":
        return "1";
      case "nnwwnnnnw":
        return "2";
      case "wnwwnnnnn":
        return "3";
      case "nnnwwnnnw":
        return "4";
      case "wnnwwnnnn":
        return "5";
      case "nnwwwnnnn":
        return "6";
      case "nnnwnnwnw":
        return "7";
      case "wnnwnnwnn":
        return "8";
      case "nnwwnnwnn":
        return "9";
      case "nnnwwnwnn":
        return "0";
      case "wnnnnwnnw":
        return "A";
      case "nnwnnwnnw":
        return "B";
      case "wnwnnwnnn":
        return "C";
      case "nnnnwwnnw":
        return "D";
      case "wnnnwwnnn":
        return "E";
      case "nnwnwwnnn":
        return "F";
      case "nnnnnwwnw":
        return "G";
      case "wnnnnwwnn":
        return "H";
      case "nnwnnwwnn":
        return "I";
      case "nnnnwwwnn":
        return "J";
      case "wnnnnnnww":
        return "K";
      case "nnwnnnnww":
        return "L";
      case "wnwnnnnwn":
        return "M";
      case "nnnnwnnww":
        return "N";
      case "wnnnwnnwn":
        return "O";
      case "nnwnwnnwn":
        return "P";
      case "nnnnnnwww":
        return "Q";
      case "wnnnnnwwn":
        return "R";
      case "nnwnnnwwn":
        return "S";
      case "nnnnwnwwn":
        return "T";
      case "wwnnnnnnw":
        return "U";
      case "nwwnnnnnw":
        return "V";
      case "wwwnnnnnn":
        return "W";
      case "nwnnwnnnw":
        return "X";
      case "wwnnwnnnn":
        return "Y";
      case "nwwnwnnnn":
        return "Z";
      case "nwnnnnwnw":
        return "-";
      case "wwnnnnwnn":
        return ".";
      case "nwwnnnwnn":
        return " ";
      case STARPATTERN:
        return "*";
      case "nwnwnwnnn":
        return "$";
      case "nwnwnnnwn":
        return "/";
      case "nwnnnwnwn":
        return "+";
      case "nnnwnwnwn":
        return "%";
      default:
        return null;
    }
  }
  #endregion

  #region EAN-specific
  /// <summary>
  /// Parses EAN-barcodes from the input pattern.
  /// </summary>
  /// <param name="sbPattern">Input pattern, should contain characters
  /// "1" .. "4" to indicate valid EAN bar widths.</param>
  /// <returns>Pipe-separated list of barcodes, empty string if none were detected</returns>
  private static string ParseEAN(StringBuilder sbPattern)
  {
    StringBuilder sbEANData = new StringBuilder(32);
    int iEANSeparators = 0;
    string sEANCode = string.Empty;

    int iPos = 0;
    sbPattern.Append("|"); // append one extra "gap" character because separator has only 3 bands
    while (iPos <= sbPattern.Length - 4)
    {
      string sEANDigit = ParseEANPattern(sbPattern.ToString(iPos, 4), sEANCode, iEANSeparators);
      switch (sEANDigit)
      {
        case null:
          // reset on invalid code
          //iEANSeparators = 0;
          sEANCode = string.Empty;
          iPos++;
          break;
        case "|":
          // EAN separator found. Each EAN code contains three separators.
          if (iEANSeparators >= 3)
            iEANSeparators = 1;
          else
            iEANSeparators++;
          iPos += 3;
          if (iEANSeparators == 2)
          {
            iPos += 2; // middle separator has 5 bars
          }
          else if (iEANSeparators == 3) // end of EAN code detected
          {
            string sFirstDigit = GetEANFirstDigit(ref sEANCode);
            if ((sFirstDigit != null) && (sEANCode.Length == 12))
            {
              sEANCode = sFirstDigit + sEANCode;
              if (sbEANData.Length > 0)
                sbEANData.Append("|");
              sbEANData.Append(sEANCode);
            }
            // reset after end of code
            //iEANSeparators = 0;
            sEANCode = string.Empty;
          }
          break;
        case "S":
          // Start of supplemental code after EAN code
          iPos += 3;
          sEANCode = "S";
          iEANSeparators = 1;
          break;
        default:
          if (iEANSeparators > 0)
          {
            sEANCode += sEANDigit;
            iPos += 4;
            if (sEANCode.StartsWith("S"))
            {
              // Each digit of the supplemental code is followed by an additional "11"
              // We assume that the code ends if that is no longer the case.
              if ((sbPattern.Length > iPos + 2) && (sbPattern.ToString(iPos, 2) == "11"))
                iPos += 2;
              else
              {
                // Supplemental code ends. It must be either 2 or 5 digits.
                sEANCode = CheckEANSupplement(sEANCode);
                if (sEANCode.Length > 0)
                {
                  if (sbEANData.Length > 0)
                    sbEANData.Append("|");
                  sbEANData.Append(sEANCode);
                }
                // reset after end of code
                iEANSeparators = 0;
                sEANCode = string.Empty;
              }
            }
          }
          else
            iPos++; // no EAN digit expected before first separator
          break;
      }
    }
    return sbEANData.ToString();
  }

  /// <summary>
  /// Used by GetBarPatterns to derive bar character from bar width.
  /// </summary>
  /// <param name="sbEAN">Output pattern</param>
  /// <param name="nBarWidth">Measured bar width in pixels</param>
  /// <param name="fNarrowBarWidth">Narrow bar width in pixels</param>
  private static void AppendEAN(StringBuilder sbEAN, int nBarWidth, float fNarrowBarWidth)
  {
    int nEAN = (int)Math.Round((double)nBarWidth / fNarrowBarWidth);
    if (nEAN == 5) nEAN = 4; // bar width could be slightly off due to distortion
    if (nEAN < 10)
      sbEAN.Append(nEAN.ToString());
    else
      sbEAN.Append("|");
  }

  /// <summary>
  /// Parses the EAN pattern for one digit or separator
  /// </summary>
  /// <param name="sPattern">Pattern to be parsed</param>
  /// <param name="sEANCode">EAN code found so far</param>
  /// <param name="iEANSeparators">Number of separators found so far</param>
  /// <returns>Detected digit type (L/G/R) and digit, "|" for separator
  /// or null if the pattern was not recognized.</returns>
  private static string ParseEANPattern(string sPattern, string sEANCode, int iEANSeparators)
  {
    string[] LRCodes = 
        {"3211", "2221", "2122", "1411", "1132", 
         "1231", "1114", "1312", "1213", "3112"};
    string[] GCodes = 
        {"1123", "1222", "2212", "1141", "2311",
         "1321", "4111", "2131", "3121", "2113"};
    if ((sPattern != null) && (sPattern.Length >= 3))
    {
      if (sPattern.StartsWith("111") && ((iEANSeparators * 12) == sEANCode.Length))
        return "|";   // found separator
      if (sPattern.StartsWith("112") && (iEANSeparators == 3) && (sEANCode.Length == 0))
        return "S";   // found EAN supplemental code

      for (int i = 0; i < 10; i++)
      {
        if (sPattern.StartsWith(LRCodes[i]))
          return ((iEANSeparators == 2) ? "R" : "L") + i.ToString();
        if (sPattern.StartsWith(GCodes[i]))
          return "G" + i.ToString();
      }
    }
    return null;
  }

  /// <summary>
  /// Decodes the L/G-pattern for the left half of the EAN code 
  /// to derive the first digit. See table in
  /// http://en.wikipedia.org/wiki/European_Article_Number
  /// </summary>
  /// <param name="sEANPattern">
  /// IN: EAN pattern with digits and L/G/R codes.
  /// OUT: EAN digits only.
  /// </param>
  /// <returns>Detected first digit or null.</returns>
  private static string GetEANFirstDigit(ref string sEANPattern)
  {
    string[] LGPatterns = 
        {"LLLLLL", "LLGLGG", "LLGGLG", "LLGGGL", "LGLLGG",
         "LGGLLG", "LGGGLL", "LGLGLG", "LGLGGL", "LGGLGL"};
    string sLG = string.Empty;
    string sDigits = string.Empty;
    if ((sEANPattern != null) && (sEANPattern.Length >= 24))
    {
      for (int i = 0; i < 12; i++)
      {
        sLG += sEANPattern[2 * i];
        sDigits += sEANPattern[2 * i + 1];
      }
      for (int i = 0; i < 10; i++)
      {
        if (sLG.StartsWith(LGPatterns[i]))
        {
          sEANPattern = sDigits + sEANPattern.Substring(24);
          return i.ToString();
        }
      }
    }
    return null;
  }

  /// <summary>
  /// Checks if EAN supplemental code is valid.
  /// </summary>
  /// <param name="sEANPattern">Parse result</param>
  /// <returns>Supplemental code or empty string</returns>
  private static string CheckEANSupplement(string sEANPattern)
  {
    try
    {
      if (sEANPattern.StartsWith("S"))
      {
        string sDigits = string.Empty;
        string sLG = string.Empty;
        for (int i = 1; i < sEANPattern.Length - 1; i += 2)
        {
          sLG += sEANPattern[i];
          sDigits += sEANPattern[i + 1];
        }

        // Supplemental code must be either 2 or 5 digits.
        switch (sDigits.Length)
        {
          case 2:
            // Do EAN-2 parity check
            string[] EAN2Parity = { "LL", "LG", "GL", "GG" };
            int iParity = Convert.ToInt32(sDigits) % 4;
            if (sLG != EAN2Parity[iParity])
              return string.Empty; // parity check failed
            break;
          case 5:
            // Do EAN-5 checksum validation
            uint uCheckSum = 0;
            for (int i = 0; i < sDigits.Length; i++)
            {
              uCheckSum += (uint)(Convert.ToUInt32(sDigits.Substring(i, 1)) * (((i & 1) == 0) ? 3 : 9));
            }
            string[] EAN5CheckSumPattern = 
                {"GGLLL", "GLGLL", "GLLGL", "GLLLG", "LGGLL", 
                 "LLGGL", "LLLGG", "LGLGL", "LGLLG", "LLGLG"};
            if (sLG != EAN5CheckSumPattern[uCheckSum % 10])
              return string.Empty; // Checksum validation failed
            break;
          default:
            return string.Empty;
        }
        return "S" + sDigits;
      }
    }
    catch (Exception ex)
    {
      System.Diagnostics.Trace.Write(ex);
    }
    return string.Empty;
  }
  #endregion

  #region Code128-specific
  /// <summary>
  /// Parses Code128 barcodes.
  /// </summary>
  /// <param name="sbPattern">Input pattern, should contain characters
  /// "1" .. "4" to indicate valid bar widths.</param>
  /// <returns>Pipe-separated list of barcodes, empty string if none were detected</returns>
  private static string ParseCode128(StringBuilder sbPattern)
  {
    StringBuilder sbCode128Data = new StringBuilder(32);
    string sCode128Code = string.Empty;
    uint uCheckSum = 0;
    int iCodes = 0;
    int iPos = 0;
    char cCodePage = 'B';
    while (iPos <= sbPattern.Length - 6)
    {
      int iResult = ParseCode128Pattern(sbPattern.ToString(iPos, 6), ref sCode128Code, ref uCheckSum, ref cCodePage, ref iCodes);
      switch (iResult)
      {
        case -1: // unrecognized pattern
          iPos++;
          break;
        case -2: // stop condition, but failed to recognize barcode
          iPos += 7;
          break;
        case CODE128STOP:
          iPos += 7;
          if (sCode128Code.Length > 0)
          {
            if (sbCode128Data.Length > 0)
              sbCode128Data.Append("|");
            sbCode128Data.Append(sCode128Code);
          }
          break;
        default:
          iPos += 6;
          break;
      }
    }
    return sbCode128Data.ToString();
  }

  /// <summary>
  /// Parses the Code128 pattern for one barcode character.
  /// </summary>
  /// <param name="sPattern">Pattern to be parsed, should be 6 characters</param>
  /// <param name="sResult">Resulting barcode up to current character</param>
  /// <param name="uCheckSum">Checksum up to current character</param>
  /// <param name="cCodePage">Current code page</param>
  /// <param name="iCodes">Count of barcode characters already parsed (needed for checksum)</param>
  /// <returns>
  /// CODE128STOP: end of barcode detected, barcode recognized.
  ///          -2: end of barcode, recognition failed.
  ///          -1: unrecognized pattern.
  ///       other: code 128 character index
  /// </returns>
  private static int ParseCode128Pattern(string sPattern, ref string sResult, ref uint uCheckSum, ref char cCodePage, ref int iCodes)
  {
    string[] Code128 = 
        {"212222", "222122", "222221", "121223", "121322", "131222", 
         "122213", "122312", "132212", "221213", "221312", "231212", 
         "112232", "122132", "122231", "113222", "123122", "123221", 
         "223211", "221132", "221231", "213212", "223112", "312131", 
         "311222", "321122", "321221", "312212", "322112", "322211", 
         "212123", "212321", "232121", "111323", "131123", "131321", 
         "112313", "132113", "132311", "211313", "231113", "231311", 
         "112133", "112331", "132131", "113123", "113321", "133121", 
         "313121", "211331", "231131", "213113", "213311", "213131", 
         "311123", "311321", "331121", "312113", "312311", "332111", 
         "314111", "221411", "431111", "111224", "111422", "121124", 
         "121421", "141122", "141221", "112214", "112412", "122114", 
         "122411", "142112", "142211", "241211", "221114", "413111", 
         "241112", "134111", "111242", "121142", "121241", "114212", 
         "124112", "124211", "411212", "421112", "421211", "212141", 
         "214121", "412121", "111143", "111341", "131141", "114113", 
         "114311", "411113", "411311", "113141", "114131", "311141", 
         "411131", "211412", "211214", "211232", "233111"};

    if ((sPattern != null) && (sPattern.Length >= 6))
    {
      for (int i = 0; i < Code128.Length; i++)
      {
        if (sPattern.StartsWith(Code128[i]))
        {
          if (i == CODE128STOP)
          {
            try
            {
              int iLength = sResult.Length;
              if (iLength > 1)
              {
                char cCheckDigit;
                if (cCodePage == 'C')
                {
                  cCheckDigit = (char)(Convert.ToInt32(sResult.Substring(iLength - 2)) + 32);
                  sResult = sResult.Substring(0, iLength - 2);
                }
                else
                {
                  cCheckDigit = sResult[iLength - 1];
                  sResult = sResult.Substring(0, iLength - 1);
                }
                uint uCheckDigit = (uint)((int)(((int)cCheckDigit) - 32) * iCodes);
                if (uCheckSum > uCheckDigit)
                {
                  uCheckSum = (uCheckSum - uCheckDigit) % 103;
                  if (cCheckDigit == (char)((int)(uCheckSum + 32)))
                  {
                    return CODE128STOP;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              System.Diagnostics.Trace.Write(ex);
            }
            // If reach this point, some check failed.
            // Reset everything and return error.
            sResult = string.Empty;
            uCheckSum = 0;
            iCodes = 0;
            return -2;
          }
          else if (i >= CODE128START)
          {
            // Start new code 128 sequence
            sResult = string.Empty;
            uCheckSum = (uint)i;
            cCodePage = (char)('A' + (i - CODE128START));
          }
          else if (uCheckSum > 0)
          {
            bool bSkip = false;
            char cNewCodePage = cCodePage;
            switch (i)
            {
              case CODE128C:
                cNewCodePage = 'C';
                break;
              case CODE128B:
                cNewCodePage = 'B';
                break;
              case CODE128A:
                cNewCodePage = 'A';
                break;
            }
            if (cCodePage != cNewCodePage)
            {
              cCodePage = cNewCodePage;
              bSkip = true;
            }
            if (!bSkip)
            {
              switch (cCodePage)
              {
                case 'C':
                  sResult += i.ToString("00");
                  break;
                default:
                  // Regular character
                  char c = (char)(i + 32);
                  sResult += c;
                  break;
              }
            }
            iCodes++;
            uCheckSum += (uint)(i * iCodes);
          }
          return i;
        }
      }
    }
    sResult = string.Empty;
    uCheckSum = 0;
    iCodes = 0;
    return -1;
  }


  #endregion

  #endregion
}