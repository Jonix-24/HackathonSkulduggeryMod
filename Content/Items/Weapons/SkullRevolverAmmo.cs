using HackathonSkulduggeryMod.Content.Projectiles;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HackathonSkulduggeryMod.Content.Items.Weapons
{
    internal class SkullRevolverAmmo : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.HighVelocityBullet);
            Item.damage = 18;
            Item.knockBack = 20;

            Item.shoot = ModContent.ProjectileType<SkullRevolverAmmoProjectile>();

            
        }

    }
}
