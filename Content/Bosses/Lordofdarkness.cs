using HackathonSkulduggeryMod.Common.Systems;
using HackathonSkulduggeryMod.Content.Items.Armor;
using HackathonSkulduggeryMod.Content.Items.Consumables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace HackathonSkulduggeryMod.Content.Bosses
{
    [AutoloadBossHead]
    internal class Lordofdarkness : ModNPC
    {
        private int state
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;   
        }

        private int subState
        {
            get => (int)NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        private float stateTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        private float stateTimer2
        {
            get => NPC.ai[3]; 
            set => NPC.ai[3] = value;
        }

        private bool secondPhase => state == 1;


        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 8;

            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers drawMods = new()
            {
                PortraitScale = 0.6f,
                PortraitPositionYOverride = 0f
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawMods);
        }

        public override void SetDefaults()
        {
            //Hitbox
            NPC.width = 64;
            NPC.height = 128;

            // Damage and Defense
            NPC.damage = 100;
            NPC.defense = 50;

            //Max HP
            NPC.lifeMax = 7500;
            if (Main.hardMode)
            {
                NPC.lifeMax = 9000;
            }

            //knockback resistance
            NPC.knockBackResist = 0f;

            //Souds
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath8;

            //Collision
            NPC.noGravity = true;
            NPC.noTileCollide = true;

            // Boss settings
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 10f;

            //AI
            NPC.aiStyle = -1;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = ImmunityCooldownID.Bosses;
            return true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VileBossBag>(), 1));

            //when hardmode
            if (Main.hardMode)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.Zenith, 1, 1, 100));
            }

            if(Main.expertMode)
            {
                npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<VileSummonItem>()));
            }
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref BossDownedSystem.downedVile, -1);
        }

        public override void AI()
        {
            //handle Targeting
            if(NPC.target == 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active) 
            {
                NPC.TargetClosest();
            }

            //Set Player Target
            Player player = Main.player[NPC.target];

            // Despawnbehaviour
            if(player.dead || !player.active)
            {
                NPC.velocity.Y -= 0.04f;
                NPC.EncourageDespawn(10);
                return;
            }

            //handle state machine
            switch (state)
            {
                case 0:
                    HandleFirstState(player);
                    break;
                case 1:
                    HandleSecondState(player);
                    break;
            }
        }

        private void HandleFirstState(Player player)
        {
            //Move Toards Player
            if(subState == 0)
            {
                float baseMoveSpeed = 8f;
                float accelerationSpeed = 0.04f;

                // Expert Mode Adjustment
                if(Main.expertMode)
                {
                    baseMoveSpeed = 10f;
                    accelerationSpeed = 0.15f;
                }

                //Move Towards Target
                MoveToTarget(player, baseMoveSpeed, accelerationSpeed, out float distanceToPlayer);

                //increase state timer
                stateTimer += 1f;

                //check if change substate

                float threshold = 300f;

                if (Main.expertMode)
                {
                    threshold *= 0.3f;
                }

                // change substate if conditions met
                if(stateTimer >= threshold)
                {
                    subState = 1;
                    stateTimer = 0;
                    stateTimer2 = 0;
                    NPC.netUpdate = true;
                    return;
                }

                if (NPC.position.Y + NPC.height < player.Center.Y && distanceToPlayer < 500f)
                {
                    // increase secondary timer
                    if (!player.dead)
                    {
                        stateTimer2 += 1f;
                    }

                    // handle projectile threshold
                    float projThreshold = 50f;
                    if (Main.expertMode)
                    {
                        projThreshold += 0.4f;
                    }

                    // handle shooting projectile
                    if (stateTimer2 >= projThreshold)
                    {
                        //reset timer
                        stateTimer2 = 0;

                        //set move speed
                        float projSpeed = 13f;
                        if (Main.expertMode)
                        {
                            projSpeed = 17f;
                        }

                        //calculate damage
                        int projDmg = (int)(NPC.damage * 0.5f);
                        float projKnockback = 3f;
                        if (Main.expertMode)
                        {
                            projDmg += (int)(NPC.damage * .15);
                            projKnockback += .5f;
                        }

                        //shoot projectile
                        ShootProjectile(player, ModContent.ProjectileType<NecromancyProjectile>(), projSpeed, projDmg, projKnockback, 1);
                    }
                }
            }
            // charge at player
            else if (subState == 1)
            {
                //setub speed
                float basespeed = 10f;
                if (Main.expertMode)
                {
                    basespeed = 12f;
                }

                //hanlde charge velocity
                float deltaX = (player.Center.X - NPC.Center.X);
                float deltaY = (player.Center.Y - NPC.Center.Y);

                //Get distance
                float distanceToP = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                // Calculate Velocity 
                float movementSpeed = basespeed / distanceToP;
                Microsoft.Xna.Framework.Vector2 velocity = new Microsoft.Xna.Framework.Vector2(deltaX, deltaY) * movementSpeed;

                // apply velocity to npc
                NPC.velocity = velocity * 2;

                //move to post charge state
                subState = 2;

                //update network
                NPC.netUpdate = true;
                if (NPC.netSpam > 10)
                {
                    NPC.netSpam = 10;
                }
            }

            //post charge state
            else if (subState == 2)
            {
                // increase timer
                stateTimer += 1f;

                //slowdown charge
                if (stateTimer >= 48f)
                {
                    //slow velocity
                    NPC.velocity *= 0.98f;

                    //adjust based on difficulty
                    if (Main.expertMode)
                    {
                        NPC.velocity *= 0.985f;
                    }

                    //if velocity close to 0
                    if (Math.Abs(NPC.velocity.X) < 0.05) NPC.velocity.X = 0f;
                    if (Math.Abs(NPC.velocity.Y) < 0.05) NPC.velocity.Y = 0f;
                }

                //Handle state Changing
                int stateThreshold = 150;
                if (Main.expertMode)
                {
                    stateThreshold = 100;
                }

                //handle state change 
                if (stateTimer >= stateThreshold)
                {
                    //inceremet 2nd timer
                    stateTimer2 += 1f;

                    //reset main timer
                    stateTimer = 0f;

                    //reset target

                    NPC.target = 255;

                    //change substate
                    if (stateTimer2 >= 5f)
                    {
                        subState = 0;
                        stateTimer2 = 0f;
                    }
                    else
                    {
                        subState = 1;
                    }
                }
            }

            //check 2nd state switch
            float lowHealthThreshold = 0.5f;
            if((float)NPC.life < (float)NPC.lifeMax * lowHealthThreshold)
            {
                //go to 2nd state
                state = 1;

                subState = 0;
                stateTimer = 0;
                stateTimer2 = 0;

                // handle´network
                NPC.netUpdate = true;
                if(NPC.netSpam > 10)
                {
                    NPC.netSpam = 10;
                }
            }
        }

        private void HandleSecondState(Player player)
        {
            //Move Toards Player
            if (subState == 0)
            {
                float baseMoveSpeed = 10f;
                float accelerationSpeed = 0.2f;

                // Expert Mode Adjustment
                if (Main.expertMode)
                {
                    baseMoveSpeed = 12f;
                    accelerationSpeed = 0.35f;
                }

                //Move Towards Target
                MoveToTarget(player, baseMoveSpeed, accelerationSpeed, out float distanceToPlayer);
                NPC.spriteDirection = NPC.direction;

                //increase state timer
                stateTimer += 1f;

                //check if change substate

                float threshold = 200f;

                if (Main.expertMode)
                {
                    threshold *= 0.3f;
                }

                // change substate if conditions met
                if (stateTimer >= threshold)
                {
                    subState = 1;
                    stateTimer = 0;
                    stateTimer2 = 0;
                    NPC.netUpdate = true;
                    return;
                }

                // handle secondary action
                if(NPC.position.Y + NPC.height < player.Center.Y && distanceToPlayer < 500f)
                {
                    // increase secondary timer
                    if(!player.dead)
                    {
                        stateTimer2 += 1f;
                    }

                    // handle projectile threshold
                    float projThreshold = 30f;
                    if (Main.expertMode)
                    {
                        projThreshold += 0.4f;
                    }

                    // handle shooting projectile
                    if(stateTimer2 >= projThreshold)
                    {
                        //reset timer
                        stateTimer2 = 0;

                        //set move speed
                        float projSpeed = 20f;
                        if (Main.expertMode) 
                        {
                            projSpeed = 25f;
                        }

                        //calculate damage
                        int projDmg = (int)(NPC.damage * 0.8f);
                        float projKnockback = 3f;
                        if (Main.expertMode)
                        {
                            projDmg += (int)(NPC.damage * .15);
                            projKnockback += .5f;
                        }

                        //shoot projectile
                        ShootProjectile(player, ModContent.ProjectileType<NecromancyProjectile>(), projSpeed, projDmg, projKnockback, 3);
                    }
                }
            }
            // charge at player
            else if (subState == 1)
            {
                //setub speed
                float basespeed = 6f;
                if (Main.expertMode)
                {
                    basespeed = 7f;
                }

                //hanlde charge velocity
                float deltaX = (player.Center.X - NPC.Center.X);
                float deltaY = (player.Center.Y - NPC.Center.Y);

                //Get distance
                float distanceToP = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                // Calculate Velocity 
                float movementSpeed = basespeed / distanceToP;
                Microsoft.Xna.Framework.Vector2 velocity = new Microsoft.Xna.Framework.Vector2(deltaX, deltaY) * movementSpeed;

                // apply velocity to npc
                NPC.velocity = velocity * 2;

                //move to post charge state
                subState = 2;

                //update network
                NPC.netUpdate = true;
                if (NPC.netSpam > 10)
                {
                    NPC.netSpam = 10;
                }
            }

            //post charge state
            else if (subState == 2)
            {
                // increase timer
                stateTimer += 1f;

                //slowdown charge
                if (stateTimer >= 40f)
                {
                    //slow velocity
                    NPC.velocity *= 0.98f;

                    //adjust based on difficulty
                    if (Main.expertMode)
                    {
                        NPC.velocity *= 0.985f;
                    }

                    //if velocity close to 0
                    if (Math.Abs(NPC.velocity.X) < 0.05) NPC.velocity.X = 0f;
                    if (Math.Abs(NPC.velocity.Y) < 0.05) NPC.velocity.Y = 0f;
                }

                //Handle state Changing
                int stateThreshold = 120;
                if (Main.expertMode)
                {
                    stateThreshold = 85;
                }

                //handle state change 
                if (stateTimer >= stateThreshold)
                {
                    //inceremet 2nd timer
                    stateTimer2 += 1f;

                    //reset main timer
                    stateTimer = 0f;

                    //reset target

                    NPC.target = 255;

                    //change substate
                    if (stateTimer2 >= 8f)
                    {
                        subState = 0;
                        stateTimer2 = 0f;
                    }
                    else
                    {
                        subState = 1;
                    }
                }
            }
        }

        private void MoveToTarget(Player player, float moveSpeed, float accelerationRate, out float distanceToPlayer)
        {
            // set distance to player
            distanceToPlayer = Microsoft.Xna.Framework.Vector2.Distance(NPC.Center, player.Center);

            // set Move speeds
            float movementSpeed = moveSpeed / distanceToPlayer;

            float targetVelocityX = (player.Center.X - NPC.Center.X) * movementSpeed;
            float targetVelocityY = (player.Center.Y - NPC.Center.Y) * movementSpeed;

            //Apply acceleration
            if(NPC.velocity.X < targetVelocityX)
            {
                //increase velocity by acceleration
                NPC.velocity.X += accelerationRate;
                // further incrase velocity
                if(NPC.velocity.X < 0f && targetVelocityX > 0f)
                {
                    NPC.velocity.X += accelerationRate;
                }
            }

            if (NPC.velocity.X > targetVelocityX)
            {
                //increase velocity by acceleration
                NPC.velocity.X -= accelerationRate;
                // further incrase velocity
                if (NPC.velocity.X > 0f && targetVelocityX < 0f)
                {
                    NPC.velocity.X -= accelerationRate;
                }
            }

            if (NPC.velocity.Y < targetVelocityY)
            {
                //increase velocity by acceleration
                NPC.velocity.Y += accelerationRate;
                // further incrase velocity
                if (NPC.velocity.Y < 0f && targetVelocityY > 0f)
                {
                    NPC.velocity.Y += accelerationRate;
                }
            }

            if (NPC.velocity.Y > targetVelocityY)
            {
                //increase velocity by acceleration
                NPC.velocity.Y -= accelerationRate;
                // further incrase velocity
                if (NPC.velocity.Y > 0f && targetVelocityY < 0f)
                {
                    NPC.velocity.Y -= accelerationRate;
                }
            }
        }

        private void ShootProjectile(Player player, int type, float speed, int damage, float knockback, int number)
        {
            for (int i = 0; i < type; i++)
            {
                float randomize = Main.rand.Next(1, 10);

                //get Target position
                Microsoft.Xna.Framework.Vector2 projTarget = new(player.Center.X - NPC.Center.X, player.Center.Y - NPC.Center.Y);
                float projDistance = (float)(projTarget.X * projTarget.X - projTarget.Y * projTarget.Y);
                float projTargetDistance = speed / projDistance;

                //set velocity
                Microsoft.Xna.Framework.Vector2 projVelocity = (projTarget * projTargetDistance);

                // get spawn position
                Microsoft.Xna.Framework.Vector2 projSpawn = NPC.Center + projVelocity * 10f;

                // handle network
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int projectileID = Projectile.NewProjectile(NPC.GetSource_FromAI(), projSpawn, projVelocity, type, damage, knockback);

                    if (Main.netMode == NetmodeID.Server && projectileID < 200)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, projectileID);
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            //first phase
            int startframe = 0;
            int endframe = 3;

            //adjust for 2nd phase
            if(secondPhase)
            {
                startframe = 4;
                endframe = 7;

                //ensure frame is in range
                if(NPC.frame.Y < startframe * frameHeight)
                {
                    NPC.frame.Y = startframe * frameHeight;
                }
            }

            //handle animation
            int frameSpeed = 5;

            //increment frame counters
            NPC.frameCounter += 0.5f;
            NPC.frameCounter += NPC.velocity.Length() / 10f;

            //adjust Frame
            if(NPC.frameCounter >= frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                //loop back to start
                if(NPC.frame.Y > endframe * frameHeight)
                {
                    NPC.frame.Y = startframe * frameHeight;
                }
            }
        }
    }
}
