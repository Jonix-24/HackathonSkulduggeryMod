using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HackathonSkulduggeryMod.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting a X_Body.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class LordVileBreast : ModItem
	{
		public static readonly int MaxManaIncrease = 30;
		public static readonly double MaxMinionIncrease = 1.5;
        public static readonly double MaxHealthIncrease = 0.5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaIncrease, MaxMinionIncrease);

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Purple; // The rarity of the item
			Item.defense = 40; // The amount of defense the item will give when equipped
			
		}

		public override void UpdateEquip(Player player) {
			player.buffImmune[BuffID.OnFire] = true;// Make the player immune to Fire
			
			player.statManaMax2 += MaxManaIncrease; // Increase how many mana points the player can have by 20
			

			int j = player.maxMinions; //cut minionslots by maxminionincrease
			double k = j * MaxMinionIncrease;
			player.maxMinions = (int)k;
			
			
			int i = player.statLifeMax2; //typecasting to decrease heath by maxhealthincrease
			double l = i * MaxHealthIncrease;
			player.statLifeMax2 = (int)l;
			
		}

		
	}
}
