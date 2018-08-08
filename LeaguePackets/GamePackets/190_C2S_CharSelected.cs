﻿using LeaguePackets.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LeaguePackets.GamePackets
{
    public class C2S_CharSelected : GamePacket // 0xBE
    {
        public override GamePacketID ID => GamePacketID.C2S_CharSelected;
        public static C2S_CharSelected CreateBody(PacketReader reader, ChannelID channelID, NetID senderNetID) 
        {
            var result = new C2S_CharSelected();
            result.SenderNetID = senderNetID;
            result.ChannelID = channelID;

            return result;
        }
        public override void WriteBody(PacketWriter writer) {}
    }
}
