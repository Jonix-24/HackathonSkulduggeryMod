using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using HackathonSkulduggeryMod.Content.Projectiles;

namespace HackathonSkulduggeryMod.Content.Buffs
{
    public class GistBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;  
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Gist>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
                return;
            }
            player.DelBuff(buffIndex);
            buffIndex--;
        }

        
    }
}
