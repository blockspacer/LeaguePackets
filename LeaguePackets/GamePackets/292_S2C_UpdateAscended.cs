﻿using LeaguePackets.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LeaguePackets.GamePackets
{
    public class S2C_UpdateAscended : GamePacket // 0x124
    {
        public override GamePacketID ID => GamePacketID.S2C_UpdateAscended;
        public NetID AscendedNetID { get; set; }
        public static S2C_UpdateAscended CreateBody(PacketReader reader, ChannelID channelID, NetID senderNetID)
        {
            var result = new S2C_UpdateAscended();
            result.SenderNetID = senderNetID;
            result.ChannelID = channelID;

            result.AscendedNetID = reader.ReadNetID();
        
            return result;
        }
        public override void WriteBody(PacketWriter writer)
        {
            writer.WriteNetID(AscendedNetID);
        }
    }
}
