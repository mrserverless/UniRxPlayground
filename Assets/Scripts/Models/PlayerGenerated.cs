#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;
    using global::ZeroFormatter.Comparers;

    public static partial class ZeroFormatterInitializer
    {
        static bool registered = false;

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Register()
        {
            if(registered) return;
            registered = true;

            // Enums
            // Objects
            ZeroFormatter.Formatters.Formatter<ZeroFormatter.Formatters.DefaultResolver, global::Player>.Register(new ZeroFormatter.DynamicObjectSegments.PlayerFormatter<ZeroFormatter.Formatters.DefaultResolver>());
            // Structs
            // Unions
            // Generics
            ZeroFormatter.Formatters.Formatter.RegisterList<ZeroFormatter.Formatters.DefaultResolver, int>();
        }
    }
}
#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter.DynamicObjectSegments
{
    using global::System;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;

    public class PlayerFormatter<TTypeResolver> : Formatter<TTypeResolver, global::Player>
        where TTypeResolver : ITypeResolver, new()
    {
        public override int? GetLength()
        {
            return null;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::Player value)
        {
            var segment = value as IZeroFormatterSegment;
            if (segment != null)
            {
                return segment.Serialize(ref bytes, offset);
            }
            else if (value == null)
            {
                BinaryUtil.WriteInt32(ref bytes, offset, -1);
                return 4;
            }
            else
            {
                var startOffset = offset;

                offset += (8 + 4 * (3 + 1));
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, int>(ref bytes, startOffset, offset, 0, value.Age);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, string>(ref bytes, startOffset, offset, 1, value.FirstName);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, string>(ref bytes, startOffset, offset, 2, value.LastName);
                offset += ObjectSegmentHelper.SerializeFromFormatter<TTypeResolver, global::System.Collections.Generic.IList<int>>(ref bytes, startOffset, offset, 3, value.List);

                return ObjectSegmentHelper.WriteSize(ref bytes, startOffset, offset, 3);
            }
        }

        public override global::Player Deserialize(ref byte[] bytes, int offset, DirtyTracker tracker, out int byteSize)
        {
            byteSize = BinaryUtil.ReadInt32(ref bytes, offset);
            if (byteSize == -1)
            {
                byteSize = 4;
                return null;
            }
            return new PlayerObjectSegment<TTypeResolver>(tracker, new ArraySegment<byte>(bytes, offset, byteSize));
        }
    }

    public class PlayerObjectSegment<TTypeResolver> : global::Player, IZeroFormatterSegment
        where TTypeResolver : ITypeResolver, new()
    {
        static readonly int[] __elementSizes = new int[]{ 4, 0, 0, 0 };

        readonly ArraySegment<byte> __originalBytes;
        readonly DirtyTracker __tracker;
        readonly int __binaryLastIndex;
        readonly byte[] __extraFixedBytes;

        CacheSegment<TTypeResolver, string> _FirstName;
        CacheSegment<TTypeResolver, string> _LastName;
        global::System.Collections.Generic.IList<int> _List;

        // 0
        public override int Age
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<TTypeResolver, int>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<TTypeResolver, int>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 1
        public override string FirstName
        {
            get
            {
                return _FirstName.Value;
            }
            set
            {
                _FirstName.Value = value;
            }
        }

        // 2
        public override string LastName
        {
            get
            {
                return _LastName.Value;
            }
            set
            {
                _LastName.Value = value;
            }
        }

        // 3
        public override global::System.Collections.Generic.IList<int> List
        {
            get
            {
                return _List;
            }
            set
            {
                __tracker.Dirty();
                _List = value;
            }
        }


        public PlayerObjectSegment(DirtyTracker dirtyTracker, ArraySegment<byte> originalBytes)
        {
            var __array = originalBytes.Array;

            this.__originalBytes = originalBytes;
            this.__tracker = dirtyTracker = dirtyTracker.CreateChild();
            this.__binaryLastIndex = BinaryUtil.ReadInt32(ref __array, originalBytes.Offset + 4);

            this.__extraFixedBytes = ObjectSegmentHelper.CreateExtraFixedBytes(this.__binaryLastIndex, 3, __elementSizes);

            _FirstName = new CacheSegment<TTypeResolver, string>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 1, __binaryLastIndex, __tracker));
            _LastName = new CacheSegment<TTypeResolver, string>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 2, __binaryLastIndex, __tracker));
            _List = ObjectSegmentHelper.DeserializeSegment<TTypeResolver, global::System.Collections.Generic.IList<int>>(originalBytes, 3, __binaryLastIndex, __tracker);
        }

        public bool CanDirectCopy()
        {
            return !__tracker.IsDirty;
        }

        public ArraySegment<byte> GetBufferReference()
        {
            return __originalBytes;
        }

        public int Serialize(ref byte[] targetBytes, int offset)
        {
            if (__extraFixedBytes != null || __tracker.IsDirty)
            {
                var startOffset = offset;
                offset += (8 + 4 * (3 + 1));

                offset += ObjectSegmentHelper.SerializeFixedLength<TTypeResolver, int>(ref targetBytes, startOffset, offset, 0, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeCacheSegment<TTypeResolver, string>(ref targetBytes, startOffset, offset, 1, ref _FirstName);
                offset += ObjectSegmentHelper.SerializeCacheSegment<TTypeResolver, string>(ref targetBytes, startOffset, offset, 2, ref _LastName);
                offset += ObjectSegmentHelper.SerializeSegment<TTypeResolver, global::System.Collections.Generic.IList<int>>(ref targetBytes, startOffset, offset, 3, _List);

                return ObjectSegmentHelper.WriteSize(ref targetBytes, startOffset, offset, 3);
            }
            else
            {
                return ObjectSegmentHelper.DirectCopyAll(__originalBytes, ref targetBytes, offset);
            }
        }
    }


}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
