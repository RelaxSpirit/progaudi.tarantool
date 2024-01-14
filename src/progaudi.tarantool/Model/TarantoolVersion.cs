using System;
using System.Collections.Generic;
using ProGaudi.Tarantool.Client.Utils;

namespace ProGaudi.Tarantool.Client.Model
{
    public class TarantoolVersion : IEquatable<TarantoolVersion>, IComparable<TarantoolVersion>, IComparable
    {
        public TarantoolVersion((int, int) major, int minor, int build, string commitHash)
        {
            Major = major;
            Minor = minor;
            Build = build;
            CommitHash = commitHash;
        }

        public (int, int) Major { get; }
        public int Minor { get; }
        public int Build { get; }
        public string CommitHash { get; }

        public static TarantoolVersion Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw ExceptionHelper.VersionCantBeEmpty();

            // 1.7.6-7-gce1a37741
            var parts = s.Split(new[] { '.', '-', 'g' }, StringSplitOptions.RemoveEmptyEntries);
            return new TarantoolVersion((int.Parse(parts[0]), int.Parse(parts[1])), int.Parse(parts[2]), int.Parse(parts[3]), parts[4]);
        }

        public override string ToString() => $"{Major.Item1}.{Major.Item2}.{Minor}-{Build}-g{CommitHash}";

        public bool Equals(TarantoolVersion other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Major.Equals(other.Major) && Minor == other.Minor && Build == other.Build && string.Equals(CommitHash, other.CommitHash);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TarantoolVersion) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Major, Minor, Build, CommitHash);
        }

        public int CompareTo(TarantoolVersion other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (other is null) return 1;
            var majorComparison = Major.CompareTo(other.Major);
            if (majorComparison != 0) return majorComparison;
            var minorComparison = Minor.CompareTo(other.Minor);
            if (minorComparison != 0) return minorComparison;
            var buildComparison = Build.CompareTo(other.Build);
            if (buildComparison != 0) return buildComparison;

            if (CommitHash == null || other.CommitHash == null) return 0;

            var hashComparison = string.Compare(CommitHash, other.CommitHash, StringComparison.Ordinal);
            if (hashComparison != 0) throw ExceptionHelper.CantCompareBuilds(this, other);
            return 0;
        }

        public int CompareTo(object obj)
        {
            if (obj is null) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is TarantoolVersion version
                ? CompareTo(version)
                : throw new ArgumentException($"Object must be of type {nameof(TarantoolVersion)}");
        }

        public static bool operator <(TarantoolVersion left, TarantoolVersion right) => Comparer<TarantoolVersion>.Default.Compare(left, right) < 0;

        public static bool operator >(TarantoolVersion left, TarantoolVersion right) => Comparer<TarantoolVersion>.Default.Compare(left, right) > 0;

        public static bool operator <=(TarantoolVersion left, TarantoolVersion right) => Comparer<TarantoolVersion>.Default.Compare(left, right) <= 0;

        public static bool operator >=(TarantoolVersion left, TarantoolVersion right) => Comparer<TarantoolVersion>.Default.Compare(left, right) >= 0;

        public static bool operator ==(TarantoolVersion left, TarantoolVersion right) => Equals(left, right);

        public static bool operator !=(TarantoolVersion left, TarantoolVersion right) => !Equals(left, right);

        public static implicit operator TarantoolVersion((int, int) major)
        {
            return new TarantoolVersion(major, 0, 0, null);
        }
    }
}