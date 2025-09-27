using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HackathonSkulduggeryMod.Content.Projectiles
{
    public class ShadowLashProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            
            Projectile.width = 62;
            Projectile.height = 50;
            



            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 25;
            Projectile.damage = 20;
            
            Projectile.timeLeft = 200;
            

            
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            //weirld this does nothing 
            
            


            Projectile.penetrate = 5;
            Projectile.timeLeft = 430;
            Projectile.light = 1f;

        }
      

    }
}
