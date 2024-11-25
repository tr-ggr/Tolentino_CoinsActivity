using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class CoinDetect
{
    public static double ComputePerimeter(List<Point> contour)
    {
        double perimeter = 0;
        for (int i = 0; i < contour.Count; i++)
        {
            Point current = contour[i];
            Point next = contour[(i + 1) % contour.Count];
            double distance = Math.Sqrt(Math.Pow(next.X - current.X, 2) + Math.Pow(next.Y - current.Y, 2));
            perimeter += distance;
        }
        return perimeter;
    }

    public static double ComputeArea(List<Point> contour)
    {
        double area = 0;
        for (int i = 0; i < contour.Count; i++)
        {
            Point current = contour[i];
            Point next = contour[(i + 1) % contour.Count];
            area += current.X * next.Y - current.Y * next.X;
        }
        return Math.Abs(area) / 2.0;
    }

    public static Rectangle CalculateBoundingBox(List<Point> contour)
    {
        int minX = contour.Min(p => p.X);
        int maxX = contour.Max(p => p.X);
        int minY = contour.Min(p => p.Y);
        int maxY = contour.Max(p => p.Y);

        return new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
    }

    public static bool IsInside(List<Point> inner, List<Point> outer)
    {
        Rectangle bboxInner = CalculateBoundingBox(inner);
        Rectangle bboxOuter = CalculateBoundingBox(outer);
        return bboxInner.Left >= bboxOuter.Left && bboxInner.Right <= bboxOuter.Right && bboxInner.Top >= bboxOuter.Top && bboxInner.Bottom <= bboxOuter.Bottom;
    }

    public static List<List<Point>> RemoveNestedContours(List<List<Point>> contours)
    {
        List<List<Point>> filteredContours = new List<List<Point>>();

        for (int i = 0; i < contours.Count; i++)
        {
            bool isNested = false;
            double area1 = ComputeArea(contours[i]);

            for (int j = 0; j < contours.Count; j++)
            {
                if (i == j) continue;

                double area2 = ComputeArea(contours[j]);

                if (IsInside(contours[i], contours[j]) && area2 < area1 * 4)
                {
                    isNested = true;
                    break;
                }
            }

            if (!isNested)
            {
                filteredContours.Add(contours[i]);
            }
        }

        return filteredContours;
    }

    public static Tuple<List<List<Point>>, List<int>, List<double>> IdentifyCoins(List<List<Point>> contours, double circularityThreshold = 0.75, int areaThreshold = 50)
    {
        List<List<Point>> potentialCoins = new List<List<Point>>();

        foreach (var contour in contours)
        {
            double perimeter = ComputePerimeter(contour);
            double area = ComputeArea(contour);

            if (perimeter == 0) continue;

            double circularity = (4 * Math.PI * area) / (perimeter * perimeter);

            if (circularity >= circularityThreshold && area > areaThreshold)
            {
                potentialCoins.Add(contour);
            }
        }

        potentialCoins = RemoveNestedContours(potentialCoins);

        List<List<Point>> finalCoins = new List<List<Point>>();
        List<int> coinValues = new List<int>();
        List<double> coinSizes = new List<double>();

        for (int i = 0; i < potentialCoins.Count; i++)
        {
            if (ComputeArea(potentialCoins[i]) < 500) continue;

            double size = ComputeArea(potentialCoins[i]);
            bool isCent = false;

            for (int j = 0; j < potentialCoins.Count; j++)
            {
                if (i == j) continue;

                if (IsInside(potentialCoins[j], potentialCoins[i]))
                {
                    finalCoins.Add(potentialCoins[i].Union(potentialCoins[j]).ToList());
                    coinValues.Add(5);
                    coinSizes.Add(ComputeArea(potentialCoins[i]));
                    isCent = true;
                    break;
                }
            }

            if (!isCent)
            {
                finalCoins.Add(potentialCoins[i]);
                coinSizes.Add(size);

                coinValues.Add(size < 4000 ? 10 : size < 5000 ? 25 : size < 8000 ? 100 : 500);
            }
        }

        return new Tuple<List<List<Point>>, List<int>, List<double>>(finalCoins, coinValues, coinSizes);
    }
}
