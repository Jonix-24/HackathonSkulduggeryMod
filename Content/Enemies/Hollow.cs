using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Bestary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader.Utilities

using Terraria.WorldBuilding;

namespace HackathonSkulduggeryMod.Content.Enemies
{
    internal class Hollow : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Zombie);
            if (NPC.onFire)
            {
                NPC.defense = 1;
            }
        }
    }
}
