using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AdventOfCode2018
{
    public static class Solvers
    {
        public static class Day1
        {
            public static int Part1Solver(IEnumerable<int> frequencyChanges, int initialFrequency = 0)
                => frequencyChanges.Prepend(initialFrequency).Sum();

            public static int Part2Solver(IEnumerable<int> frequencyChanges, int initialFrequency = 0)
            {
                IEnumerable<int> memorizedEnumerable = frequencyChanges as int[] ?? frequencyChanges.ToArray();
                var knownFrequencies = new HashSet<int>();
                int currentFrequency = initialFrequency;
                knownFrequencies.Add(currentFrequency);

                while (true)
                {
                    foreach (int frequencyChange in memorizedEnumerable)
                    {
                        currentFrequency += frequencyChange;
                        if (!knownFrequencies.Add(currentFrequency))
                            return currentFrequency;
                    }
                }
            }
        }

        public static class Day2
        {
            public static int Part1Solver(IEnumerable<string> boxIds)
            {
                var containsInfoEnumerable = boxIds.Select(id => GetContainsInfo(id.GroupBy(character => character))).ToArray();
                var containsTwoCount = containsInfoEnumerable.Count(o => o.ContainsTwo);
                var containsThreeCount = containsInfoEnumerable.Count(o => o.ContainsThree);

                return containsTwoCount * containsThreeCount;

                ContainsInfo GetContainsInfo(IEnumerable<IGrouping<char, char>> charcterGroups)
                {
                    var containsInfo = new ContainsInfo();
                    foreach (var group in charcterGroups)
                    {
                        var count = group.Count();
                        if (count == 2)
                        {
                            containsInfo.ContainsTwo = true;
                        }
                        else if (count == 3)
                        {
                            containsInfo.ContainsThree = true;
                        }
                    }
                    return containsInfo;
                }
            }

            public static string Part2Solver(IEnumerable<string> boxIds)
            {
                string result = null;

                var memorisedBoxIds = boxIds as string[] ?? boxIds.ToArray();
                foreach (string currentId in memorisedBoxIds)
                {
                    foreach (var comparisonId in memorisedBoxIds.Except(Enumerable.Repeat(currentId, 1)))
                    {
                        var length = currentId.Length;
                        var zipped = currentId.Zip(comparisonId,
                                (currentChar, comparisonChar) => new {Character = currentChar, Shared = currentChar == comparisonChar})
                            .Where(o => o.Shared).ToArray();
                        var sharedCount = zipped.Length;

                        if (length - sharedCount == 1)
                        {
                            result = String.Concat(zipped.Select(o => o.Character));
                            break;
                        }
                    }
                }

                return result;
            }

            internal struct ContainsInfo
            {
                public bool ContainsTwo { get; set; }
                public bool ContainsThree { get; set; }
            }
        }

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
                        int occupiedFlatCount = rectangles.Where(rect => DoesRectContainPoint(rect, x, y)).Take(2).Count();
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
                return (x >= rect.X && x <= rect.X + (rect.Width - 1)) && (y >= rect.Y && y <= rect.Y + (rect.Height - 1));
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
