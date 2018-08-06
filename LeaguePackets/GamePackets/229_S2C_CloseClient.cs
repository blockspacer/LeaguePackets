﻿using LeaguePackets.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LeaguePackets.GamePackets
{
    public class S2C_CloseClient : GamePacket // 0xE5
    {
        public override GamePacketID ID => GamePacketID.S2C_CloseClient;
        public static S2C_CloseClient CreateBody(PacketReader reader, NetID senderNetID) 
        {
            var result = new S2C_CloseClient();
            result.SenderNetID = senderNetID;
            return result;
        }
        public override void WriteBody(PacketWriter writer) {}
    }
}
