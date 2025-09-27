using HackathonSkulduggeryMod.Content.Bosses;
using HackathonSkulduggeryMod.Content.Items.Armor;
using HackathonSkulduggeryMod.Content.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace HackathonSkulduggeryMod.Content.Items.Consumables
{
    internal class VileBossBag : ModItem
    {
        public override void SetStaticDefaults() 
        {
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;

            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;

            Item.rare = ItemRarityID.Purple;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<LordVileBreast>(), 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SolomonsCane>(), 1));

            //when hardmode
            if (Main.hardMode)
            {
                itemLoot.Add(ItemDropRule.Common(ItemID.NecromancerBanner, 10, 1, 2));
            }

            // add expert only items
            if (Main.hardMode)
            {
                itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.CrownosBreastplate, 10));
                itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.CrownosMask, 10));
                itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.CrownosLeggings, 10));
                itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.CrownosWings, 10));
            }

            //Money
            itemLoot.Add(ItemDropRule.Coins(2611112, true));
        }
    }
}
