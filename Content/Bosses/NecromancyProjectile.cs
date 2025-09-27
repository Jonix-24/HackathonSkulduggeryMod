using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace HackathonSkulduggeryMod.Content.Bosses
{
    internal class NecromancyProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;

            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.hostile = true;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

            Projectile.timeLeft = 300;
            Projectile.penetrate = 1;
        }

        public override void AI()
        {
            Player player = Main.player[(int)Projectile.ai[0]];
            if(player.dead || !player.active)
            {
                return;
            }

            Projectile.velocity = (player.Center - Projectile.Center).SafeNormalize(Microsoft.Xna.Framework.Vector2.Zero) * 6f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
    }
}
