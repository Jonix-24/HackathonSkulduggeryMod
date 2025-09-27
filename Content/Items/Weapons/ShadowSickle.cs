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
	
	
	public class ShadowSickle : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.WeaponTest.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Summon;
			Item.width = 25;
			Item.height = 28;
			Item.crit = 7;
			Item.useAnimation = 18;
			Item.useTime = 50;
            Item.useAnimation = 50;
            Item.noUseGraphic = true;
            Item.useStyle = 1;
            Item.knockBack = 3;
            Item.value = 8;
            Item.rare = 6;
            Item.shootSpeed = 15f;
            Item.shoot =ModContent.ProjectileType<ShadowLashProjectile>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
			
			Item.ChangePlayerDirectionOnShoot = true;

		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TungstenBar, 12);
			recipe.AddIngredient(ModContent.ItemType<Content.Items.Weapons.CleaversScythe>(),1);
			recipe.AddIngredient(ItemID.FallenStar, 2);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();
		}

		
	}	

	
}


