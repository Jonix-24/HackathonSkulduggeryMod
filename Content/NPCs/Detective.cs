using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HackathonSkulduggeryMod.Common.Systems;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.UI;

using Terraria.Localization;

using Terraria.ModLoader.IO;
using Terraria.Utilities;
//using HackathonSkulduggeryMod.Content.NPCs;
using HackathonSkulduggeryMod.Content.Items.Consumables;
using HackathonSkulduggeryMod.Content.Items.Weapons;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using System.Media;

namespace HackathonSkulduggeryMod.Content.NPCs
{
    [AutoloadHead]
    public class Detective : ModNPC
    {
        public bool sold = BossDownedSystem.downedVile;
        public const string ShopName = "Shop";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skulduggery");
            
        }
        public override void SetDefaults()
        {
            
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 20;
            NPC.height = 20;
            
            NPC.aiStyle = 7;
            NPC.defense = 35;
            
            NPC.lifeMax = 500;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 0;
            NPCID.Sets.AttackFrameCount[NPC.type] = 1;
            NPCID.Sets.DangerDetectRange[NPC.type] = 500;
            NPCID.Sets.AttackType[NPC.type] = 1;
            NPCID.Sets.AttackTime[NPC.type] = 30;
            NPCID.Sets.AttackAverageChance[NPC.type] = 15;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;
            AnimationType = 22;
            NPC.Happiness
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Like)
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Like)
                .SetNPCAffection(NPCID.SkeletonMerchant, AffectionLevel.Like)
                .SetNPCAffection(NPCID.GoblinTinkerer, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Clothier, AffectionLevel.Dislike);



        }

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            for (var i = 0; i < 255; i++)
            {
                Player player = Main.player[i];
                
                
                    
               
                foreach (Item item in player.inventory)
                {
                    if (NPC.downedBoss3)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                "Mr. Pleasant",
                "Skulduggery Pleasant",
                "Inspector Me",
                "Skulduggery",
                "Cadaver Cain"
            };
        }

       public override void SetChatButtons(ref string button, ref string button2) { // What the chat buttons are when you open up the chat UI
			button = Language.GetTextValue("LegacyInterface.28");
			
            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Content.Items.Armor.LordVileBreast>()))
            { button2 = "Vile?"; }

            else if (Main.LocalPlayer.HasItem(ModContent.ItemType<Content.Items.Consumables.SkulduggerysSuitcase>()))
            {
                button2 = "Vile?";
            }
            else if (Main.LocalPlayer.HasItem(ModContent.ItemType<Content.Items.Weapons.CleaversScythe>()))
            {
                button2 = "Cleavers?";
            }
            else { button2 = "Sparrows?"; }
        }

		public override void OnChatButtonClicked(bool firstButton, ref string shop) {
			if (firstButton) {
				// We want 3 different functionalities for chat buttons, so we use HasItem to change button 1 between a shop and upgrade action.

                Main.npcChatText = "The Sparrow flies south for Winter";

				shop = ShopName; // Name of the shop tab we want to open.
			}
            else
            {
                if (Main.LocalPlayer.HasItem(ModContent.ItemType <Content.Items.Armor.LordVileBreast>()))
                {Main.npcChatText = "You defeated Vile! Well atleast you measured up to my unconscious"}
                else if (Main.LocalPlayer.HasItem(ModContent.ItemType<Content.Items.Consumables.SkulduggerysSuitcase>()))
                { Main.npcChatText = "Oh you found my suitcase. You want to know my nature? Look inside! It is a dark and twisted thing"; }
                else if (Main.LocalPlayer.HasItem(ModContent.ItemType<Content.Items.Weapons.CleaversScythe>()))
                {

                    Main.npcChatText = "You have met a Cleaver? They were on out side ages ago. We should have appreciated them more";

                }
                else
                {
                    Main.npcChatText = "The Sparrow flies south for Winter";
                }
                

            }
		}

        // Not completely finished, but below is what the NPC will sell
        public override void AddShops() {
            NPCShop npcShop = new NPCShop(Type, ShopName)


                .Add(new Item(ItemID.Revolver))
                .Add(new Item(ItemID.Boulder))
                .Add(new Item(ModContent.ItemType<Content.Items.Weapons.CleaversScythe>()))
                .Add(new Item(ModContent.ItemType<Content.Items.Weapons.SkullRevolver>()));

            if (BossDownedSystem.downedVile) //add a bool
                    {npcShop.Add(new Item(ModContent.ItemType<Content.Items.Consumables.SkulduggerysSuitcase>())); };

            //.Add(new Item(ModContent.ItemType<Content.Items.CleaversScythe>(Condition.DownedEyeOfCthulhu)) { shopCustomPrice = Item.buyPrice(platinum: 5) });

            npcShop.Register(); // Name of this shop tab
		}

		public override void ModifyActiveShop(string shopName, Item[] items) {
			foreach (Item item in items) {
				// Skip 'air' items and null items.
				if (item == null || item.type == ItemID.None) {
					continue;
				}

				
		    }
        }
    	
       
       

        

        public override string GetChat()
        {
            NPC.FindFirstNPC(ModContent.NPCType<Detective>());
            switch (Main.rand.Next(6))
            {
                case 0:
                    return "Doors are for people with no imagination.";
                case 1:
                    return "I'm sophisticated, charming, suaive and debonair, but I have never claimed to civilized";
                case 2:
                    return "Embrace your inner lunatic. Fun times guaranteed.";
                case 3:  
                    return "I’m Detective Inspector Me. Unusual name, I know. My family were incredibly narcissistic. I’m lucky I escaped with any degree of humility at all, to be honest, but then I’ve always managed to exceed expectations. ";
                case 4:
                    return "There’s no such thing as winning or losing. There is won and there is lost, there is victory and defeat. There are absolutes. Everything in between is still left to fight for. Until then, there is only the struggle, because tides do what tides do–they turn.";
                default:
                    return "I woke up, a bag of bones. Literally. They had gathered up my bones and put them in a bag and thrown the bag into a river.";  
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 15;
            knockback = 2f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 5;
            randExtraCooldown = 10;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.CursedFlameFriendly;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 7f;
        }

        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ItemID.BallaHat, 1, false, 0, false, false);
        }
        
    }
}
