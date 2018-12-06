using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Solvers
{
    public static partial class Solvers
    {
        public static class Day3
        {
            public static int Part1Solver(IEnumerable<string> rectDescriptions)
            {
                IEnumerable<Rect> rectangles = ParseRectangles(rectDescriptions).ToArray();
                int minLeftPoint = rectangles.Min(rect => rect.X);
                int minTopPoint = rectangles.Min(rect => rect.Y);
                int maxLeftPoint = rectangles.Max(rect => rect.X + rect.Width);
                int maxTopPoint = rectangles.Max(rect => rect.Y + rect.Height);

                int collisions = 0;

                for (int x = minLeftPoint; x <= maxLeftPoint; x++)
                {
                    for (int y = minTopPoint; y <= maxTopPoint; y++)
                    {
                        int occupiedFlatCount =
                            rectangles.Where(rect => DoesRectContainPoint(rect, x, y)).Take(2).Count();
                        if (occupiedFlatCount > 1)
                        {
                            collisions += 1;
                        }
                    }
                }

                return collisions;
            }

            public static int Part2Solver(IEnumerable<string> rectDescriptions)
            {
                IEnumerable<Rect> rectangles = ParseRectangles(rectDescriptions).ToArray();
                foreach (Rect rectangle in rectangles)
                {
                    bool hasCollisions = rectangles
                        .Except(Enumerable.Repeat(rectangle, 1))
                        .Any(b => DoesRectIntersectRect(rectangle, b));

                    if (!hasCollisions)
                    {
                        return rectangle.Id;
                    }
                }

                return -1;
            }

            private static IEnumerable<Rect> ParseRectangles(IEnumerable<string> rectDescriptions)
                => rectDescriptions.Select(ConvertStringToRect);

            private static Rect ConvertStringToRect(string description)
            {
                string[] idSplit = description.Split('@');
                int id = int.Parse(idSplit[0].Trim('#', ' '));
                string[] pointMeasureSplit = idSplit[1].Split(':');
                string[] pointSplit = pointMeasureSplit[0].Split(',');
                string[] measureSplit = pointMeasureSplit[1].Split('x');
                int x = int.Parse(pointSplit[0].Trim());
                int y = int.Parse(pointSplit[1].Trim());
                int width = int.Parse(measureSplit[0].Trim());
                int height = int.Parse(measureSplit[1].Trim());
                return new Rect(id, x, y, width, height);
            }

            private static bool DoesRectContainPoint(Rect rect, int x, int y)
            {
                return (x >= rect.X && x <= rect.X + (rect.Width - 1)) &&
                       (y >= rect.Y && y <= rect.Y + (rect.Height - 1));
            }

            private static bool DoesRectIntersectRect(Rect a, Rect b)
            {
                return (b.X >= a.X && b.X < a.X + a.Width || b.X + b.Width > a.X && b.X < a.X + a.Width)
                       && (b.Y >= a.Y && b.Y < a.Y + a.Height || b.Y + b.Height > a.Y && b.Y < a.Y + a.Height);
            }

            internal struct Rect
            {
                public int Id { get; }
                public int X { get; }
                public int Y { get; }
                public int Width { get; }
                public int Height { get; }

                public Rect(int id, int x, int y, int width, int height)
                {
                    this.Id = id;
                    this.X = x;
                    this.Y = y;
                    this.Width = width;
                    this.Height = height;
                }
            }
        }
    }
}