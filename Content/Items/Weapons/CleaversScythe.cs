using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System.Collections.Generic;
using Terraria.DataStructures;
using HackathonSkulduggeryMod.Content.Projectiles;


namespace HackathonSkulduggeryMod.Content.Items.Weapons
{ 
	
	
	public class CleaversScythe : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.WeaponTest.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 54;
			Item.useTime = 70;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.buyPrice(gold: 14);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.shoot = ModContent.ProjectileType<CleaversProjectile>();
			Item.shootSpeed = 8f;
			Item.holdStyle = ItemHoldStyleID.HoldGuitar;
			Item.ChangePlayerDirectionOnShoot = true;
			

		}

		
	}	

	
}


