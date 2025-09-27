using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HackathonSkulduggeryMod.Content.Items.Weapons
{
    internal class SkullRevolver : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Revolver);
            Item.knockBack = 20;

            Item.damage = 35;
            Item.shootSpeed = 20;
        }
    }
}
