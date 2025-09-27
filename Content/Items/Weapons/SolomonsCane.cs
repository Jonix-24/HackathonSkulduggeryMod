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
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	
	public class SolomonsCane : ModItem
	{
		

		public override void SetDefaults()
		{
			
			Item.DefaultToWhip(ModContent.ProjectileType<SolomonsProjectile>(), 40, 2, 4);
			Item.shootSpeed = 4;
			Item.rare = ItemRarityID.Yellow;
			Item.channel = true;
			
			Item.buffType = 78;
			Item.buffTime = 180;
			
			Item.width = 5;
			Item.height = 16;
	
		}

		// Makes the whip receive melee prefixes
		public override bool MeleePrefix()
		{
			return true;
		}
	}
}


