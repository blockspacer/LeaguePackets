﻿using LeaguePackets.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LeaguePackets.PayloadPackets
{
    public class UnknownPayloadPacket : PayloadPacket
    {
        private PayloadPacketID _id;
        public override PayloadPacketID ID => _id;
        public PayloadPacketID PayloadPacketIDRaw
        {
            get => _id;
            set => _id = value;
        }
        public UnknownPayloadPacket(PayloadPacketID id) => _id = id;
        public UnknownPayloadPacket(){}

        public static UnknownPayloadPacket CreateBody(PacketReader reader, PayloadPacketID id)
        {
            var result = new UnknownPayloadPacket(id);
            return result;
        }

        public override void WriteBody(PacketWriter writer)
        {
        }
    }
}
