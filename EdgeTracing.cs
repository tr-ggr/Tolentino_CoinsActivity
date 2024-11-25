using System;
using System.Collections.Generic;
using System.Drawing;

public class EdgeTracing
{
    public EdgeTracing()
    {
    }

    private static readonly Point[] Directions =
    {
        new Point(0, -1),  // North
        new Point(1, -1),  // North-East
        new Point(1, 0),   // East
        new Point(1, 1),   // South-East
        new Point(0, 1),   // South
        new Point(-1, 1),  // South-West
        new Point(-1, 0),  // West
        new Point(-1, -1)  // North-West
    };

    public static List<List<Point>> ExtractContours(Bitmap binaryImage)
    {
        int width = binaryImage.Width;
        int height = binaryImage.Height;
        bool[,] visited = new bool[width, height];
        List<List<Point>> contours = new List<List<Point>>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (binaryImage.GetPixel(x, y).R == 255 && !visited[x, y])
                {
                    List<Point> contour = TraceSingleContour(binaryImage, visited, x, y);
                    if (contour.Count > 0)
                        contours.Add(contour);
                }
            }
        }

        return contours;
    }

    private static List<Point> TraceSingleContour(Bitmap binaryImage, bool[,] visited, int startX, int startY)
    {
        List<Point> contour = new List<Point>();
        Point current = new Point(startX, startY);
        Point previous = new Point(startX, startY - 1);

        do
        {
            contour.Add(current);
            visited[current.X, current.Y] = true;

            Point next = FindNextContourPoint(binaryImage, current, previous);
            previous = current;
            current = next;

            if (current == Point.Empty)
                break;

        } while (current != new Point(startX, startY));

        return contour;
    }

    private static Point FindNextContourPoint(Bitmap binaryImage, Point current, Point previous)
    {
        int width = binaryImage.Width;
        int height = binaryImage.Height;

        int previousIndex = Array.IndexOf(Directions, new Point(previous.X - current.X, previous.Y - current.Y));
        int startIndex = (previousIndex + 1) % 8;

        for (int i = 0; i < 8; i++)
        {
            int neighborIndex = (startIndex + i) % 8;
            Point offset = Directions[neighborIndex];
            Point neighbor = new Point(current.X + offset.X, current.Y + offset.Y);

            if (neighbor.X >= 0 && neighbor.X < width && neighbor.Y >= 0 && neighbor.Y < height &&
                binaryImage.GetPixel(neighbor.X, neighbor.Y).R == 255)
            {
                return neighbor;
            }
        }

        return Point.Empty; 
    }

    public static Bitmap HighlightContour(Bitmap bitmap, List<Point> contour)
    {
        Bitmap resultBitmap = new Bitmap(bitmap);

        int contourThickness = 3;

        foreach (var point in contour)
        {
            resultBitmap.SetPixel(point.X, point.Y, Color.Green);

            for (int dx = -contourThickness; dx <= contourThickness; dx++)
            {
                for (int dy = -contourThickness; dy <= contourThickness; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    int newX = point.X + dx;
                    int newY = point.Y + dy;

                    if (newX >= 0 && newX < bitmap.Width && newY >= 0 && newY < bitmap.Height)
                    {
                        resultBitmap.SetPixel(newX, newY, Color.Green);
                    }
                }
            }
        }

        return resultBitmap;
    }
}

