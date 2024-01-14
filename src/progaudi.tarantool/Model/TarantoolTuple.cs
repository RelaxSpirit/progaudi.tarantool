using System.Collections.Generic;

namespace ProGaudi.Tarantool.Client.Model
{
    public interface ITarantoolTuple
    {
    }

    public class TarantoolTuple<T1> : ITarantoolTuple
    {
        public TarantoolTuple(T1 item1)
        {

            Item1 = item1;
        }

        public T1 Item1 { get; }

        protected bool Equals(TarantoolTuple<T1> other)
        {
            return EqualityComparer<T1>.Default.Equals(Item1, other.Item1);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TarantoolTuple<T1>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = EqualityComparer<T1>.Default.GetHashCode(Item1);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{Item1}";
        }
    }

    public class TarantoolTuple<T1, T2> : ITarantoolTuple
    {
        public TarantoolTuple(T1 item1, T2 item2)
        {

            Item1 = item1;
            Item2 = item2;
        }

        public T1 Item1 { get; }
        public T2 Item2 { get; }

        protected bool Equals(TarantoolTuple<T1, T2> other)
        {
            return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) &&
                   EqualityComparer<T2>.Default.Equals(Item2, other.Item2);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TarantoolTuple<T1, T2>) obj);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Item1, Item2);
        }

        public override string ToString()
        {
            return $"{Item1}, {Item2}";
        }
    }

    public class TarantoolTuple<T1, T2, T3> : ITarantoolTuple
    {
        public TarantoolTuple(T1 item1, T2 item2, T3 item3)
        {

            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public T1 Item1 { get; }
        public T2 Item2 { get; }
        public T3 Item3 { get; }

        protected bool Equals(TarantoolTuple<T1, T2, T3> other)
        {
            return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) &&
                   EqualityComparer<T2>.Default.Equals(Item2, other.Item2) &&
                   EqualityComparer<T3>.Default.Equals(Item3, other.Item3);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TarantoolTuple<T1, T2, T3>) obj);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Item1, Item2, Item3);
        }

        public override string ToString()
        {
            return $"{Item1}, {Item2}, {Item3}";
        }
    }

    public class TarantoolTuple<T1, T2, T3, T4> : ITarantoolTuple
    {
        public TarantoolTuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {

            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        public T1 Item1 { get; }
        public T2 Item2 { get; }
        public T3 Item3 { get; }
        public T4 Item4 { get; }

        protected bool Equals(TarantoolTuple<T1, T2, T3, T4> other)
        {
            return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) &&
                   EqualityComparer<T2>.Default.Equals(Item2, other.Item2) &&
                   EqualityComparer<T3>.Default.Equals(Item3, other.Item3) &&
                   EqualityComparer<T4>.Default.Equals(Item4, other.Item4);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TarantoolTuple<T1, T2, T3, T4>) obj);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Item1, Item2, Item3, Item4);
        }

        public override string ToString()
        {
            return $"{Item1}, {Item2}, {Item3}, {Item4}";
        }
    }

    public class TarantoolTuple<T1, T2, T3, T4, T5> : ITarantoolTuple
    {
        public TarantoolTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {

            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
        }

        public T1 Item1 { get; }
        public T2 Item2 { get; }
        public T3 Item3 { get; }
        public T4 Item4 { get; }
        public T5 Item5 { get; }

        protected bool Equals(TarantoolTuple<T1, T2, T3, T4, T5> other)
        {
            return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) &&
                   EqualityComparer<T2>.Default.Equals(Item2, other.Item2) &&
                   EqualityComparer<T3>.Default.Equals(Item3, other.Item3) &&
                   EqualityComparer<T4>.Default.Equals(Item4, other.Item4) &&
                   EqualityComparer<T5>.Default.Equals(Item5, other.Item5);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TarantoolTuple<T1, T2, T3, T4, T5>) obj);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Item1, Item2, Item3, Item4, Item5);
        }

        public override string ToString()
        {
            return $"{Item1}, {Item2}, {Item3}, {Item4}, {Item5}";
        }
    }

    public class TarantoolTuple<T1, T2, T3, T4, T5, T6> : ITarantoolTuple
    {
        public TarantoolTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {

            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
        }

        public T1 Item1 { get; }
        public T2 Item2 { get; }
        public T3 Item3 { get; }
        public T4 Item4 { get; }
        public T5 Item5 { get; }
        public T6 Item6 { get; }

        protected bool Equals(TarantoolTuple<T1, T2, T3, T4, T5, T6> other)
        {
            return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) &&
                   EqualityComparer<T2>.Default.Equals(Item2, other.Item2) &&
                   EqualityComparer<T3>.Default.Equals(Item3, other.Item3) &&
                   EqualityComparer<T4>.Default.Equals(Item4, other.Item4) &&
                   EqualityComparer<T5>.Default.Equals(Item5, other.Item5) &&
                   EqualityComparer<T6>.Default.Equals(Item6, other.Item6);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TarantoolTuple<T1, T2, T3, T4, T5, T6>) obj);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Item1, Item2, Item3, Item4, Item5, Item6);
        }

        public override string ToString()
        {
            return $"{Item1}, {Item2}, {Item3}, {Item4}, {Item5}, {Item6}";
        }
    }

    public class TarantoolTuple<T1, T2, T3, T4, T5, T6, T7> : ITarantoolTuple
    {
        public TarantoolTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {

            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
        }

        public T1 Item1 { get; }
        public T2 Item2 { get; }
        public T3 Item3 { get; }
        public T4 Item4 { get; }
        public T5 Item5 { get; }
        public T6 Item6 { get; }
        public T7 Item7 { get; }

        protected bool Equals(TarantoolTuple<T1, T2, T3, T4, T5, T6, T7> other)
        {
            return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) &&
                   EqualityComparer<T2>.Default.Equals(Item2, other.Item2) &&
                   EqualityComparer<T3>.Default.Equals(Item3, other.Item3) &&
                   EqualityComparer<T4>.Default.Equals(Item4, other.Item4) &&
                   EqualityComparer<T5>.Default.Equals(Item5, other.Item5) &&
                   EqualityComparer<T6>.Default.Equals(Item6, other.Item6) &&
                   EqualityComparer<T7>.Default.Equals(Item7, other.Item7);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TarantoolTuple<T1, T2, T3, T4, T5, T6, T7>) obj);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Item1, Item2, Item3, Item4, Item5, Item6, Item7);
        }

        public override string ToString()
        {
            return $"{Item1}, {Item2}, {Item3}, {Item4}, {Item5}, {Item6}, {Item7}";
        }
    }

    public class TarantoolTuple<T1, T2, T3, T4, T5, T6, T7, T8> : ITarantoolTuple
    {
        public TarantoolTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
        {

            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
            Item8 = item8;
        }

        public T1 Item1 { get; }
        public T2 Item2 { get; }
        public T3 Item3 { get; }
        public T4 Item4 { get; }
        public T5 Item5 { get; }
        public T6 Item6 { get; }
        public T7 Item7 { get; }
        public T8 Item8 { get; }

        protected bool Equals(TarantoolTuple<T1, T2, T3, T4, T5, T6, T7, T8> other)
        {
            return EqualityComparer<T1>.Default.Equals(Item1, other.Item1) &&
                   EqualityComparer<T2>.Default.Equals(Item2, other.Item2) &&
                   EqualityComparer<T3>.Default.Equals(Item3, other.Item3) &&
                   EqualityComparer<T4>.Default.Equals(Item4, other.Item4) &&
                   EqualityComparer<T5>.Default.Equals(Item5, other.Item5) &&
                   EqualityComparer<T6>.Default.Equals(Item6, other.Item6) &&
                   EqualityComparer<T7>.Default.Equals(Item7, other.Item7) &&
                   EqualityComparer<T8>.Default.Equals(Item8, other.Item8);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TarantoolTuple<T1, T2, T3, T4, T5, T6, T7, T8>) obj);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8);
        }

        public override string ToString()
        {
            return $"{Item1}, {Item2}, {Item3}, {Item4}, {Item5}, {Item6}, {Item7}, {Item8}";
        }
    }


    public class TarantoolTuple : ITarantoolTuple
    {
        private TarantoolTuple()
        {
        }

        public static TarantoolTuple Empty { get; } = new TarantoolTuple();

        public static TarantoolTuple<T1>
            Create<T1>(T1 item1)
        {
            return new TarantoolTuple<T1>
                (item1);
        }


        public static TarantoolTuple<T1, T2>
            Create<T1, T2>(T1 item1, T2 item2)
        {
            return new TarantoolTuple<T1, T2>
                (item1, item2);
        }


        public static TarantoolTuple<T1, T2, T3>
            Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new TarantoolTuple<T1, T2, T3>
                (item1, item2, item3);
        }


        public static TarantoolTuple<T1, T2, T3, T4>
            Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return new TarantoolTuple<T1, T2, T3, T4>
                (item1, item2, item3, item4);
        }


        public static TarantoolTuple<T1, T2, T3, T4, T5>
            Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            return new TarantoolTuple<T1, T2, T3, T4, T5>
                (item1, item2, item3, item4, item5);
        }


        public static TarantoolTuple<T1, T2, T3, T4, T5, T6>
            Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            return new TarantoolTuple<T1, T2, T3, T4, T5, T6>
                (item1, item2, item3, item4, item5, item6);
        }


        public static TarantoolTuple<T1, T2, T3, T4, T5, T6, T7>
            Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            return new TarantoolTuple<T1, T2, T3, T4, T5, T6, T7>
                (item1, item2, item3, item4, item5, item6, item7);
        }


        public static TarantoolTuple<T1, T2, T3, T4, T5, T6, T7, T8>
            Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7,
                T8 item8)
        {
            return new TarantoolTuple<T1, T2, T3, T4, T5, T6, T7, T8>
                (item1, item2, item3, item4, item5, item6, item7, item8);
        }
    }
}