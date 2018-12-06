using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdventOfCode2018.Solvers
{
    public static partial class Solvers
    {
        public static class Day4
        {
            public static (int, int) Solver(IEnumerable<string> guardEventStrings)
            {
                IEnumerable<GuardEvent> guardEvents = ParseGuardEvents(guardEventStrings)
                    .OrderBy(e => e.EventTime)
                    .ToArray();

                var guardMidnightSleepDictionary = new Dictionary<int, Dictionary<DateTime, List<int>>>();

                int? currentGuardId = null;
                GuardEvent previousGuardEvent = null;
                foreach (GuardEvent guardEvent in guardEvents)
                {
                    switch (guardEvent.EventType)
                    {
                        case GuardEventType.Unknown:
                            throw new ArgumentOutOfRangeException();
                        case GuardEventType.ShiftBegins:
                            LogMinutesAsleep(currentGuardId, previousGuardEvent, guardEvent);
                            currentGuardId = GetGuardIdFromEventDescription(guardEvent.EventDescription);
                            break;
                        case GuardEventType.FallsAsleep:
                            break;
                        case GuardEventType.WakesUp:
                            LogMinutesAsleep(currentGuardId, previousGuardEvent, guardEvent);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    previousGuardEvent = guardEvent;
                }

                int mostSleepyGuardId =
                    guardMidnightSleepDictionary
                        .OrderByDescending(o => o.Value.SelectMany(dml => dml.Value).Count())
                        .Select(o => o.Key)
                        .First();

                int mostSleepyGuardsFavouriteMinute =
                    guardMidnightSleepDictionary[mostSleepyGuardId]
                        .SelectMany(dml => dml.Value)
                        .GroupBy(m => m)
                        .OrderByDescending(g => g.Count())
                        .First()
                        .Key;

                int mostSameMinuteAsleepGuardId =
                    guardMidnightSleepDictionary
                        .OrderByDescending(o => o.Value.SelectMany(dml => dml.Value)
                            .GroupBy(m => m)
                            .OrderByDescending(g => g.Count())
                            .First()
                            .Count())
                        .First()
                        .Key;

                int mostSameMinuteAsleepMinute = 
                    guardMidnightSleepDictionary[mostSameMinuteAsleepGuardId]
                        .SelectMany(dml => dml.Value)
                        .GroupBy(m => m)
                        .OrderByDescending(g => g.Count())
                        .First()
                        .Key;

                return (mostSleepyGuardId * mostSleepyGuardsFavouriteMinute, mostSameMinuteAsleepGuardId * mostSameMinuteAsleepMinute);

                void LogMinutesAsleep(int? guardId, GuardEvent previous, GuardEvent current)
                {
                    if (guardId == null || previous == null || current == null
                        || previous.EventType != GuardEventType.FallsAsleep)
                    {
                        return;
                    }

                    DateTime sleepStart = previous.EventTime > current.EventTime.Date
                        ? previous.EventTime
                        : current.EventTime.Date;

                    List<int> guardLog = guardMidnightSleepDictionary
                        .GetOrCreate(guardId.Value)
                        .GetOrCreate(sleepStart.Date);

                    guardLog.AddRange(Enumerable.Range(sleepStart.Minute,
                        current.EventTime.Minute - sleepStart.Minute));
                }
            }

            private static int GetGuardIdFromEventDescription(string guardEventEventDescription)
            {
                return int.Parse(guardEventEventDescription.Split('#')[1].Split(' ')[0]);
            }

            internal static IEnumerable<GuardEvent> ParseGuardEvents(IEnumerable<string> guardEventStrings)
                => guardEventStrings.Select(ParseGuardEvent);

            internal static GuardEvent ParseGuardEvent(string eventString)
            {
                DateTime eventTime = DateTime.ParseExact(eventString.Substring(0, 18), "[yyyy-MM-dd HH:mm]",
                    CultureInfo.InvariantCulture);
                string eventDescription = eventString.Substring(19);

                return new GuardEvent(eventTime, eventDescription);
            }

            internal class GuardEvent
            {
                public DateTime EventTime { get; }
                public GuardEventType EventType { get; }
                public string EventDescription { get; }

                public GuardEvent(DateTime eventTime, string eventDescription)
                {
                    this.EventTime = eventTime;
                    this.EventDescription = eventDescription;

                    if (eventDescription.Contains("asleep"))
                    {
                        this.EventType = GuardEventType.FallsAsleep;
                    }
                    else if (eventDescription.Contains("wakes"))
                    {
                        this.EventType = GuardEventType.WakesUp;
                    }
                    else if (eventDescription.Contains(" begins"))
                    {
                        this.EventType = GuardEventType.ShiftBegins;
                    }
                    else
                    {
                        this.EventType = GuardEventType.Unknown;
                    }
                }
            }

            internal enum GuardEventType
            {
                Unknown,
                ShiftBegins,
                FallsAsleep,
                WakesUp
            }
        }
    }
}