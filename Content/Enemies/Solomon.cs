using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader.Utilities;

using Terraria.WorldBuilding;

namespace HackathonSkulduggeryMod.Content.Enemies
{
    internal class Solomon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 3;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPC.damage = 52;
            NPC.defense = 20;
            
            //Max HP
            NPC.lifeMax = 400;
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Necromancer);
            if (NPC.onFire)
            {
                NPC.defense = 70;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
           

            
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.Weapons.SolomonsCane>()));
            
        }
    }
}
