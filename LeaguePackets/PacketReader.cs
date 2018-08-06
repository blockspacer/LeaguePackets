﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace LeaguePackets
{
    public class PacketReader : IDisposable
    {
        private BinaryReader _reader;
        private byte _bits = 0;
        private byte _bitsLeft = 0;
        public Stream Stream => _reader.BaseStream;

        public PacketReader(Stream stream, bool leaveOpen = false) 
        {
            _reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen);
        }

        public PacketReader(byte[] data) : this(new MemoryStream(data), false) {}

        public void Dispose()
        {
            ((IDisposable)_reader).Dispose();
        }

        public bool ReadBool() => _reader.ReadBoolean();
        public SByte ReadSByte() => _reader.ReadSByte();
        public byte ReadByte() => _reader.ReadByte();
        public short ReadInt16() => _reader.ReadInt16();
        public ushort ReadUInt16() => _reader.ReadUInt16();
        public int ReadInt32() => _reader.ReadInt32();
        public uint ReadUInt32() => _reader.ReadUInt32();
        public long ReadInt64() => _reader.ReadInt64();
        public ulong ReadUInt64() => _reader.ReadUInt64();
        public float ReadFloat() => _reader.ReadSingle();
        public double ReadDouble() => _reader.ReadDouble();
        public byte[] ReadBytes(int count) => _reader.ReadBytes(count);
        public void ReadPad(int count) => ReadBytes(count);


        public byte[] ReadLeft()
        {
            return ReadBytes((int)(Stream.Length - Stream.Position));
        }

        public Vector2 ReadVector2()
        {
            var x = ReadFloat();
            var y = ReadFloat();
            return new Vector2(x, y);
        }

        public Vector3 ReadVector3()
        {
            var x = ReadFloat();
            var y = ReadFloat();
            var z = ReadFloat();
            return new Vector3(x, y, z);
        }

        public Vector4 ReadVector4()
        {
            var x = ReadFloat();
            var y = ReadFloat();
            var z = ReadFloat();
            var w = ReadFloat();
            return new Vector4(x, y, z, w);
        }

        public string ReadFixedString(int maxLength)
        {
            var data = ReadBytes(maxLength).TakeWhile(c => c != 0).ToArray();
            return Encoding.ASCII.GetString(data);
        }

        public string ReadSizedString()
        {
            var count = ReadInt32();
            if (count <= 0)
            {
                return "";
            }
            var data = ReadBytes(count);
            return Encoding.ASCII.GetString(data);
        }

        public string ReadSizedFixedString(int maxLength)
        {
            var count = ReadInt32();
            if (count >= (maxLength - 1))
            {
                throw new IOException("Data count too big!");
            }
            var data = ReadBytes(count);
            ReadPad(maxLength - count);
            return Encoding.ASCII.GetString(data);
        }

        public string ReadZeroTerminatedString()
        {
            var data = new List<byte>();
            while (true)
            {
                byte c = ReadByte();
                if (c == 0)
                {
                    break;
                }
                data.Add(c);
            }
            return Encoding.ASCII.GetString(data.ToArray());
        }

        public uint ReadPackedUInt32()
        {
            uint result = 0;
            byte shift = 0;
            byte data = 0;
            do
            {
                if(shift > 28)
                {
                    throw new IOException("Too much data in 7bit encoded int!");
                }
                data = _reader.ReadByte();
                result |= (uint)(data & 0x7Fu) << shift;
                shift += 7;
            } while ((data & 0x80) != 0);
            return result;
        }

        public int ReadPackedInt32()
        {
            return (int)ReadPackedUInt32();
        }

        public float ReadPackedFloat()
        {
            var firstByte = ReadByte();
            if(firstByte == 0xFF)
            {
                return 0.0f;
            }
            else if(firstByte == 0xFE)
            {
                return ReadFloat();
            }
            else
            {
                byte[] newBytes = new byte[4];
                newBytes[0] = firstByte;
                Buffer.BlockCopy(ReadBytes(3), 0, newBytes, 1, 3);
                if(!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(newBytes);
                }
                return BitConverter.ToSingle(newBytes, 0);
            }
        }
    }
}
