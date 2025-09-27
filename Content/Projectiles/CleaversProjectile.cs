using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HackathonSkulduggeryMod.Content.Projectiles
{
    public class CleaversProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            
            Projectile.width = 40;
            Projectile.height = 38;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            
            Projectile.damage = 25;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 700;
            Projectile.light = 0.3f;
            Projectile.alpha = 150;
           
           
        }
      

    }
}
