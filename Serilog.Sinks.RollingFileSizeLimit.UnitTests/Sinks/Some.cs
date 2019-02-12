﻿using System;
using System.IO;
using System.Linq;
using System.Threading;

using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Sinks.RollingFileSizeLimit.UnitTests.Sinks
{
    internal static class Some
    {
        private static int Counter;

        public static int Int()
        {
            return Interlocked.Increment(ref Counter);
        }

        public static decimal Decimal()
        {
            return Int() + 0.123m;
        }

        public static string String(string tag = null)
        {
            return (tag ?? "") + "__" + Int();
        }

        public static TimeSpan TimeSpan()
        {
            return System.TimeSpan.FromMinutes(Int());
        }

        public static DateTime Instant()
        {
            return new DateTime(2012, 10, 28) + TimeSpan();
        }

        public static DateTimeOffset OffsetInstant()
        {
            return new DateTimeOffset(Instant());
        }

        public static LogEvent InformationEvent(DateTimeOffset? timestamp = null, string message = null)
        {
            return new LogEvent(timestamp ?? OffsetInstant(), LogEventLevel.Information,
                null, MessageTemplate(message), Enumerable.Empty<LogEventProperty>());
        }

        public static LogEventProperty LogEventProperty()
        {
            return new LogEventProperty(String(), new ScalarValue(Int()));
        }

        public static string NonexistentTempFilePath()
        {
            return Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");
        }

        public static string TempFilePath()
        {
            return Path.GetTempFileName();
        }

        public static string TempFolderPath()
        {
            var dir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(dir);
            return dir;
        }

        public static MessageTemplate MessageTemplate(string message = null)
        {
            return new MessageTemplateParser().Parse(message ?? String());
        }
    }
}