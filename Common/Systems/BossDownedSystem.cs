using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace HackathonSkulduggeryMod.Common.Systems
{
    public class BossDownedSystem : ModSystem
    {
        public static bool downedVile = false;

        public override void ClearWorld()
        {
            downedVile = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["downedTutorialBoss"] = downedVile;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedVile = tag.GetBool("downedTutorialBoss");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedVile;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedVile = flags[0];
        }
    }
}
