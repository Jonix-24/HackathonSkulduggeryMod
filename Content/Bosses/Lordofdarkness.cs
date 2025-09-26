using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace HackathonSkulduggeryMod.Content.Bosses
{
    [AutoloadBossHead]
    internal class Lordofdarkness : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 8;

            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers drawMods = new()
            {
                PortraitScale = 0.6f,
                PortraitPositionYOverride = 0f
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawMods);
        }

        public override void SetDefaults()
        {
            //Hitbox
            NPC.width = 64;
            NPC.height = 128;

            // Damage and Defense
            NPC.damage = 32;
            NPC.defense = 15;

            //Max HP
            NPC.lifeMax = 7500;

            //knockback resistance
            NPC.knockBackResist = 0f;

            //Souds
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath8;

            //Collision
            NPC.noGravity = true;
            NPC.noTileCollide = true;

            // Boss settings
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 10f;

            //AI
            NPC.aiStyle = -1;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = ImmunityCooldownID.Bosses;
            return true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.TaxCollectorHat, 1));

            //when hardmode
            if (Main.hardMode)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.Zenith, 1, 1, 100));
            }
        }
    }
}
