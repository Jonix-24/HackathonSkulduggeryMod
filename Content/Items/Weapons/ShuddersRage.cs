using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using HackathonSkulduggeryMod.Content.Projectiles;
using HackathonSkulduggeryMod.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace HackathonSkulduggeryMod.Content.Items.Weapons
{
    public class ShuddersRage : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.GamepadWholeScreenUseRange[Type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;
            Item.UseSound = SoundID.Item71;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 44;
            Item.UseSound = SoundID.Item1;

            Item.DamageType = DamageClass.Summon;
            Item.damage = 29;
            Item.knockBack = 2f;
            Item.mana = 10;
            Item.noMelee = true;

            Item.value = Item.buyPrice(gold: 1);
            Item.shoot = ModContent.ProjectileType<Gist>();
            Item.buffType = ModContent.BuffType<GistBuff>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);

            int projectile = Projectile.NewProjectile(source, position ,velocity ,type ,damage, knockback, player.whoAmI);

            Main.projectile[projectile].originalDamage = Item.damage;
            return false;
        }
        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            
        }

       
    }
}