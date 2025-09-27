using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader.Utilities;

using Terraria.WorldBuilding;

namespace HackathonSkulduggeryMod.Content.Enemies
{
    internal class Hollow : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            AnimationType = 3;
            if (NPC.onFire)
            {
                NPC.defense = 1;
            }
        }
    }
}
