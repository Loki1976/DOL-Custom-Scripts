using System;
using System.Collections.Generic;
using System.Text;
using DOL.GS;
using DOL.Database;
using DOL.Events;
using DOL.AI.Brain;
using DOL.AI;

namespace DOL.GS.Scripts
{
    /// <summary>
    /// This class is a gamenpc that runs when it is struck by melee combat.
    /// </summary>
    public class TomteHockeyPuck : GameNPC
    {
        #region Tomte Hockey Puck

        public TomteHockeyPuck(TomteHockeyGame game)
        {
            m_game = game; //assign a field to this puck.
            
            //We assign this handler to check whenever the puck stops moving whether it has scored.
            GameEventMgr.AddHandler(this, GameNPCEvent.ArriveAtTarget, new DOLEventHandler(ArriveAtTarget));

            //We also want to be void of a brain for this puck.
            BlankBrain brain = new BlankBrain();
            SetOwnBrain(brain);

            Model = 671;
            Size = 20;
            Name = "Puck";
            Flags = 0; //no flags.
            Realm = 0;

            //prevent nullrefException by assigning a value.
            m_hitLastByRedTeam = false;
        }

        private bool m_hitLastByRedTeam;

        /// <summary>
        /// Did the red team hit us last? Used for offside rules.
        /// </summary>
        public bool HitLastByRedTeam
        {
            get { return  m_hitLastByRedTeam; }
            set { m_hitLastByRedTeam = value; }
        }

        private int m_TeamHitLimiter;

        /// <summary>
        /// Limits which teams can hit the puck - set to zero after puck is hit.
        /// 0 = everyone
        /// 1 = Red Team Only
        /// 2 = Blue Team Only
        /// </summary>
        public int TeamHitLimiter
        {
            get { return m_TeamHitLimiter; }
            set { m_TeamHitLimiter = value; }
        }

        #region Game association

        private TomteHockeyGame m_game;

        public TomteHockeyGame Game
        { get { return m_game; } }

        #endregion

        public override void StartAttack(GameObject attackTarget)
        {
            //Do nothing - pucks can't attack!
        }

        public override eAttackResult CalculateEnemyAttackResult(AttackData ad, InventoryItem weapon)
        {
            return eAttackResult.HitUnstyled; //We ALWAYS hit the puck. This is to prevent misses.
        }

        public override void TakeDamage(GameObject source, eDamageType damageType, int damageAmount, int criticalAmount)
        {
            //We override take damage so that our puck will not be killed because of the fight.
            //          base.TakeDamage(source, damageType, damageAmount, criticalAmount);

            //having removed the base void that deals damage to the puck, lets test its damage type.
            //we do this because we don't for example want the puck to be moved by range attacks.

            if ((damageType != eDamageType.Crush && damageType != eDamageType.Slash && damageType != eDamageType.Thrust) && !Game.CanUseMagic)
                return;

            //If damagetype is not of a physical type, we will return and ignore the blow.


            //Next, check to see if the player is actually playing the game.
            //Any playing player remember will be holding a hockey stick. The 'source' dealt the damage.

            GamePlayer player = source as GamePlayer;
            if (player == null) return;

            //This removes both null sources (fall damage) and gamenpcs.

            InventoryItem item = player.Inventory.GetItem(eInventorySlot.TwoHandWeapon);
            if (item == null)
                return; //If not using a 2h weapon we can't possibly be playing!
            if (item.Id_nb != "hockeystick" || player.ActiveWeaponSlot != eActiveWeaponSlot.TwoHanded)
            {
                Say("I'm afraid you've got to hit me with the hockey stick, punk!");
                return; //The check for the hockey stick.
            }

            //The reason we check for the hockey stick is to prevent people just using the fastest
            //1h weapon possible to play. Everyone must use the same weapon at least for fairness sake!
            //There will obviously be affects from Qui, but this helps mostly.

            //Now, also check if the player is actually playing.
            if (!Game.PlayingPlayers.Contains(player))
                return; //return if not playing, simple eh?

            //Lastly, if the game uses the offside system, a check is needed to ensure our team has the right to hit the
            //ball.
            if (Game.UseOffsideRules && TeamHitLimiter != 0)
            {
                //what happens if we cannot hit it?
                if ((item.Color != 1 && TeamHitLimiter == 1) || (item.Color == 1 && TeamHitLimiter != 1))
                {
                    string team = (TeamHitLimiter == 1) ? "Reds" : "Blues";
                    player.Out.SendMessage("You cannot hit the puck - the shot can only be taken by a player from the " + team + "!", DOL.GS.PacketHandler.eChatType.CT_SpellResisted, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
                    return;
                }
                //Reset the Team Limiter, to prevent it becoming a one-sided game!
                TeamHitLimiter = 0;
            }
                    

            //With the relevant checks out of the way, we can now be moved.

            //First, we will check we are not actually already offside by checking bounds aswell.
            if (CheckBounds()) //This is now a bool so if relocated the puck now ignores any further movement from the player.
                return;

            //Regions are useful to help break up code into readable chunks...
            #region Movement

            //angle from player to puck.
            double angle = Math.Atan2(Y - player.Y, X - player.X);

            //degree to move using sin and cos.
            int xmove, ymove;
            xmove = (int)(200 * Math.Cos(angle));
            ymove = (int)(200 * Math.Sin(angle));

            //Now, call us to walk there....
            WalkTo(X + xmove, Y + ymove, Z, MaxSpeed);

            //Movement complete!

            #endregion

            //Send out a sound effect of the tomte wailing from the hit!
            if (Util.Chance(30))
                player.Out.SendSoundEffect(1210, (ushort)player.CurrentRegionID, (ushort)X, (ushort)Y, (ushort)Z, 300);

            //Lastly, as the hit has been successful, we will remember which team hit us last.
            m_hitLastByRedTeam = (item.Color == 1) ? true : false;
            //Basically, if the hockey stick is red, true - else false.

        }

        /// <summary>
        /// Checks the bounds of the game. Checks also scoring.
        /// </summary>
        /// <returns>True if the puck is relocated</returns>
        public bool CheckBounds()
        {
            /* The tomte hockey board is a rectangle. Two goals at each end, 1/2 the width of the board.
             
              *--| |--*   Blue
              *       * 
              *       *
              *       *   center
              *       *
              *       *
              *--| |--*   Red
              
             */

            //Lets see first if we are out of bounds.

            //First, are we off the sides of the court? This will always result in an out-of-bounds scenario.
            if (X < Game.X - Game.XSize/2 || X > Game.X + Game.XSize/2)
            {
                OutOfBounds();
                return true; //No further check is required!
            }

            //Next, check we are out of bounds Y-ways.
            if (Y < Game.Y - Game.YSize / 2 || Y > Game.Y + Game.YSize / 2)
            {
                //We are out of bounds y-ways. Did we miss the goal?
                if (X < Game.X - Game.XSize / 4 || X > Game.X + Game.XSize / 4)
                {
                    //we did, reposition as normal!
                    OutOfBounds();
                    return true;
                }
                else //else, which side scored?
                {
                    //It's easy to see which side scored. Which goal did it go into?
                    //This will be revealed due to the Y position of the puck. Is it < or > the centre of the court?

                    //For sake of consistency, we have said blue team is above the centre and red is below (see above).
                    //So, lets use that and see who scored!

                    if (Y > Game.Y)
                    {
                        OutOfBounds(1);
                        Game.RedScores(); //Red scores when the puck goes into blues goal. 
                    }
                    else
                    {
                        OutOfBounds(2);
                        Game.BlueScores(); //Blue scores when the puck goes into reds goal.
                    }

                    //Now reposition because we are still out of bounds and need to return!
                    //Before we do though, play a cheering noise.
                    foreach (GamePlayer pl in GetPlayersInRadius(WorldMgr.VISIBILITY_DISTANCE))
                        pl.Out.SendSoundEffect(218, (ushort)pl.CurrentRegionID, (ushort)X, (ushort)Y, (ushort)Z, (ushort)WorldMgr.VISIBILITY_DISTANCE);

                    return true;
                }

            }
            //Return false if we did not go offside.
            return false;
        }

        /// <summary>
        /// Repositions the puck to the center (from not scoring).
        /// </summary>
        public void OutOfBounds()
        {
            OutOfBounds(0);
        }

        /// <summary>
        /// Repositions the puck to the center, announces score.
        /// <param name="scored">1 for red, 2 for blue.</param>
        /// </summary>
        public void OutOfBounds(int scored)
        {
            MoveTo(CurrentRegionID, Game.X, Game.Y, Game.Z, Heading);
            //Cheer emote to taunt players to come and hit us!
            if (Util.Chance(50))
                Emote(DOL.GS.PacketHandler.eEmote.Cheer);
            else
                Emote(DOL.GS.PacketHandler.eEmote.Taunt);

            //No one scored, the ball went off the pitch.
            //if our game uses offside rules, we should activate these aswell!
            if (scored == 0)
            {
                string message = "The puck is knocked off-court";
                
                //Here's the part for the offside system!
                if (Game.UseOffsideRules)
                {
                    message += " by the ";
                    //Depending on who hit it last, assign a variable to the puck which allows only the other team to hit it.
                    if (m_hitLastByRedTeam)
                    {
                        message += " Red Team! Blues have the next shot.";
                        TeamHitLimiter = 2;
                    }
                    else
                    {
                        TeamHitLimiter = 1;
                        message += " Blue Team! Reds have the next shot.";
                    }
                }
                else
                    message+=".";

                //Tell players its gone offside!
                foreach (GamePlayer pl in GetPlayersInRadius(WorldMgr.VISIBILITY_DISTANCE))
                    pl.Out.SendMessage(message, DOL.GS.PacketHandler.eChatType.CT_Broadcast, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
            
            }
            //If we scored, announce it!
            else if (scored == 1)
                foreach (GamePlayer pl in GetPlayersInRadius(WorldMgr.VISIBILITY_DISTANCE))
                    pl.Out.SendMessage("Haha, Red team has scored!", DOL.GS.PacketHandler.eChatType.CT_Broadcast, DOL.GS.PacketHandler.eChatLoc.CL_ChatWindow);
            else if (scored == 2)
                foreach (GamePlayer pl in GetPlayersInRadius(WorldMgr.VISIBILITY_DISTANCE))
                    pl.Out.SendMessage("Blue team plays beautifully!", DOL.GS.PacketHandler.eChatType.CT_Broadcast, DOL.GS.PacketHandler.eChatLoc.CL_ChatWindow);

        }

        /// <summary>
        /// Checks if we are out of bounds when the puck stops moving.
        /// </summary>
        private void ArriveAtTarget(DOLEvent e, object sender, EventArgs arguments)
        {
            TomteHockeyPuck puck = sender as TomteHockeyPuck;

            if (puck != null)
                puck.CheckBounds();

        }

        public override bool RemoveFromWorld()
        {
            //We must remove the event handler aswell to prevent memory problems...
            GameEventMgr.RemoveHandler(this, GameNPCEvent.ArriveAtTarget, new DOLEventHandler(ArriveAtTarget));

            return base.RemoveFromWorld();
        }
    }
        #endregion

    #region Manager
    /// <summary>
    /// This class manages the tomte hockey system.
    /// </summary>
    public static class TomteHockeyGameManager
    {
        private static ItemTemplate hockeyStick;

        /// <summary>
        /// A template for hockey sticks...
        /// </summary>
        public static ItemTemplate HockeyStick
        { get { return hockeyStick; } }

        [ScriptLoadedEvent]
        public static void OnScriptCompiled(DOLEvent e, object sender, EventArgs args)
        {

            #region Hockeystick
            hockeyStick = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "hockeystick");
            if (hockeyStick == null)
            {
                hockeyStick = new ItemTemplate();
                hockeyStick.Name = "Hockey Stick";


                hockeyStick.Weight = 10;
                hockeyStick.Model = 19;
                hockeyStick.Hand = 1;
                hockeyStick.Item_Type = 12;
                hockeyStick.Object_Type = (int)eObjectType.GenericWeapon;
                hockeyStick.SPD_ABS = 30;
                hockeyStick.DPS_AF = 10;
                hockeyStick.Level = 1;
                hockeyStick.Quality = 100;
                hockeyStick.Durability = 100000;
                hockeyStick.MaxDurability = 100000;
                hockeyStick.Condition = 10000;
                hockeyStick.MaxCondition = 10000;
                hockeyStick.Type_Damage = 1; //crush damage. Essential for reasons stated above, 
                                             //so that magic damage does not scare the puck.
                
                hockeyStick.Id_nb = "hockeystick";
                hockeyStick.IsPickable = false;
                hockeyStick.IsDropable = true;
            }
            #endregion

        }



    }
    #endregion

    #region Tomte Marker
    public class TomteMarker : GameNPC
    {
        public TomteMarker()
            : base()
        {
            Name = "Tomte Adjudicator";
            GuildName = "";
            Model = 672;
            Size = 35;
            Level = 1;
            MaxSpeedBase = 0;
            Flags = (uint)eFlags.DONTSHOWNAME + (uint)eFlags.CANTTARGET + (uint)eFlags.PEACE;
            CheerBrain brain = new CheerBrain();
            SetOwnBrain(brain);
        }

        public override bool Interact(GamePlayer player)
        {
            Emote(DOL.GS.PacketHandler.eEmote.Clap);
            return base.Interact(player);
        }

    }

    /// <summary>
    /// A brain that mindlessly cheers!
    /// </summary>
    public class CheerBrain : ABrain
    {
        public override void Think()
        {
            if (Util.Chance(40))
            {
                Body.Emote(DOL.GS.PacketHandler.eEmote.Cheer);
            }
        }
    }
    #endregion

    #region Tomte Hockey Game
    /// <summary>
    /// A game of Tomte Hockey. This holds whether the game is being played, players, match x/y etc.
    /// </summary>
    public class TomteHockeyGame
    {
        /// <summary>
        /// Creates a tomte hockey game using the coordinates of the judge.
        /// </summary>
        /// <param name="judge"></param>
        public TomteHockeyGame(GameNPC judge)
        {
            //We used to use the same x-position as the judge.
            //However this meant he often 'got in the way' of the games, standing in the middle of the court. Nowadays, we
            //Use his basis for positioning, but as we can't move him lest the server reboot and reloads him in his altered
            //position, creating a moved game, we shall simply place him at the side to start.
            m_xsize = 700;
            m_ysize = 1150;
            
            m_x = judge.X + XSize/2 + 50;
            m_y = judge.Y;
            m_z = judge.Z;
            m_region = judge.CurrentRegionID;


            //Initialise rules stuff...
            m_playerCount = 4;
            m_canUseMagic = false;
            m_repositionplayersonGoal = false;

            //Set the match to 'awaiting players'.
            playState = ePlayState.Waiting;

            //We initialise these variables to prevent any nullref exceptions later on.
            m_playingPlayers = new List<GamePlayer>();
            m_blueTeam = new List<GamePlayer>();
            m_redTeam = new List<GamePlayer>();

            //This section constructs the hockey court. Feel free to use the basis for other interesting games!

            #region Build match...

            //Create marker npcs to illustrate the pitch size!
            //3 along the sides.
            for (int x = -1; x <2; x++)
                for (int y = 1; y < 4; y++)
                {
                    if (x==0) //to prevent single split down the middle.
                        continue;
                    int ybias = 2 - y;

                    TomteMarker npc = new TomteMarker();
                    npc.X = X + XSize * x / 2;
                    npc.Y = Y + YSize * (2-y) / 2;
                    npc.Z = Z;
                    npc.CurrentRegionID = Region;
                    npc.AddToWorld();
                    npc.TurnTo(X, Y, true);

                }

            //Create the goal posts.
            for (int x = -1; x<2;x++)
                for (int y = -1; y < 2; y++)
                {
                    if (y == 0 || x == 0)
                        continue;

                    GameStaticItem goalpost = new GameStaticItem();
                    goalpost.Model = 2963;
                    goalpost.X = X + XSize * x / 4;
                    goalpost.Y = Y + YSize * y / 2;
                    goalpost.Z = Z;
                    goalpost.CurrentRegionID = Region;
                    goalpost.Name = "Goalpost";

                    //Goalpost colour names...
                    goalpost.Name += (y > 0) ? " [Blue]" : " [Red]";

                    goalpost.AddToWorld();
                }

            //Create the colored goal markers...
            for (int y = -1; y < 2; y++)
            {
                if (y == 0)
                    continue;

                TomteMarker stick = new TomteMarker();
                if (y >0)
                    stick.Model = 907; //Blue goal orb.
                else
                    stick.Model= 908; //Red goal orb.
                stick.X = X;
                stick.Y = Y + YSize * y / 2;
                stick.Z = Z;
                stick.CurrentRegionID = Region;
                stick.Name = "Big Goal Thing";

                //Goalpost colour names...
                stick.Name += (y > 0) ? " [Blue]" : " [Red]";

                stick.AddToWorld();
            }

            #endregion

        }

        #region Variables

        #region Matchsize & location

        private int m_x;
        private int m_y;
        private int m_z;
        private ushort m_region;

        public ushort Region
        { get { return m_region; } }

        public int X
        { get { return m_x; } }

        public int Y
        { get { return m_y; } }

        public int Z
        { get { return m_z; } }

        private int m_xsize, m_ysize;

        public int XSize
        { get { return m_xsize; } set { m_xsize = value; } }

        public int YSize
        { get { return m_ysize; } set { m_ysize = value; } }
        
        #endregion

        #region Players

        private List<GamePlayer> m_playingPlayers;

        public List<GamePlayer> PlayingPlayers
        {
            get { return m_playingPlayers; }
            //no ability to set - this is to ensure players are added through the right command,
            //and the list is flushed only at the end of the game.
        }

        private List<GamePlayer> m_redTeam;
        private List<GamePlayer> m_blueTeam;

        private int m_playerCount;

        /// <summary>
        /// The max number of players that can play.
        /// </summary>
        public int MaxPlayerCount
        {
            get { return m_playerCount; }
            set { m_playerCount = value; }
        }

        #endregion

        #region Scores

        int m_blueScore, m_redScore;

        public int BlueScore
        { get { return m_blueScore; } }

        public int RedScore
        { get { return m_redScore; } }

        #endregion

        #region Playstate

        /// <summary>
        /// We use a playstate to describe the current state of the tomte hockey game.
        /// Awaiting players or playing?
        /// </summary>
        public enum ePlayState
        {
            Waiting,
            Playing
        }

        private ePlayState playState;

        /// <summary>
        /// The Playstate of tomte hockey.
        /// </summary>
        public ePlayState PlayState
        {
            get { return playState; }
            set { playState = value; }
        }

        #endregion

        #region Puck and Posts...

        private TomteHockeyPuck m_puck;

        /// <summary>
        /// The puck relating to this Tomte Hockey Match.
        /// </summary>
        public TomteHockeyPuck Puck
        {
            get { return m_puck; }
            set { m_puck = value; }
        }

        #endregion

        #region Rules

        bool m_repositionplayersonGoal = false;

        /// <summary>
        /// Do we move players after a goal is scored?
        /// </summary>
        public bool RepositionPlayersOnGoal
        {
            get { return m_repositionplayersonGoal; }
            set { m_repositionplayersonGoal = value; }
        }

        bool m_canUseMagic = false;

        /// <summary>
        /// Can magic be used to move the puck?
        /// </summary>
        public bool CanUseMagic
        {
            get { return m_canUseMagic; }
            set { m_canUseMagic = value; }
        }

        private bool m_useOffsideRules = true;

        /// <summary>
        /// Should we use the new offside rules, that allows only a member off the opposite team to take the 'throw in'.
        /// </summary>
        public bool UseOffsideRules
        {
            get { return m_useOffsideRules; }
            set { m_useOffsideRules = value; }
        }

        #endregion

        #endregion

        #region Adding Players
        /// <summary>
        /// Adds a player to the game, and moves him to his teams goal.
        /// </summary>
        /// <param name="player"></param>
        private void AddPlayerToGame(GamePlayer player)
        {
            //To add a player to the game, we must check which team he will go to.
            bool redTeam = m_redTeam.Count < m_blueTeam.Count;
            //This bool will be true if players are to be added to the read team.

            //Whatever happens, the player will be added to the playing player list.
            m_playingPlayers.Add(player);

            //Depending on that bool, assign him to the right team.
            if (redTeam)
                m_redTeam.Add(player);
            else
                m_blueTeam.Add(player);

            //We have assigned him to the team. Now, we need to give him a hockey stick!

            //HOCKEY STICK HERE (COLOR ETC).
            InventoryItem stick = new InventoryItem(TomteHockeyGameManager.HockeyStick);

            if (redTeam)
                stick.Color = 1;
            else
                stick.Color = 3;

            //Give the player the hockeystick.
            player.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, stick);

            //Brilliant. Now all that remains is to position him at his goal.
            int yp;
            if (redTeam)
                yp = Y - YSize / 2;
            else
                yp = Y + YSize / 2;

            player.X = X;
            player.Y = yp;

            //Alert the player of his team.
            string teamname = (redTeam) ? "Red" : "Blue";
            player.Out.SendMessage("You join Team " + teamname + ".", DOL.GS.PacketHandler.eChatType.CT_ScreenCenterSmaller, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
            player.Out.SendMessage("You join Team " + teamname + ".", DOL.GS.PacketHandler.eChatType.CT_SpellResisted, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);


            //Why not use MoveTo? Because we also need to find the heading!

            player.Heading = player.GetHeadingToSpot(X, Y);

            //Now we send the jump packet, so it sends the heading aswell.
            player.MoveTo(player.CurrentRegionID, player.X, player.Y, player.Z, player.Heading);

            //Check to see if we have filled the amount of players to start the game.
            if (m_playingPlayers.Count >= MaxPlayerCount)
                StartGame();

        }

        /// <summary>
        /// Checks if the player can be added.
        /// </summary>
        /// <param name="player"></param>
        /// <returns>The error message, or ""</returns>
        private string CanAddPlayer(GamePlayer player)
        {
            if (m_playingPlayers.Count > MaxPlayerCount)
                return "The game is already full!";

            //inventory check.
            //Do we have room for a staff?
            if (!player.Inventory.IsSlotsFree(1, eInventorySlot.FirstBackpack, eInventorySlot.LastBackpack))
                return "There is not enough room in your inventory for the hockey stick!";

            if (playState == ePlayState.Playing)
                return "There is already a game ongoing!";

            return "";
        }

        public bool TryAddPlayer(GamePlayer player)
        {
            string str = CanAddPlayer(player);

            if (str == "")
            {
                AddPlayerToGame(player);
                return true;
            }
            else
            {
                player.Out.SendMessage(str, DOL.GS.PacketHandler.eChatType.CT_Broadcast, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                return false;
            }
        }

        public void ResetPlayersAtStart(GamePlayer refresher)
        {
            foreach (GamePlayer player in PlayingPlayers)
            {
                player.Out.SendMessage(refresher.Name + " requests that the games players be reset.", DOL.GS.PacketHandler.eChatType.CT_ScreenCenterSmaller, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
                player.Out.SendMessage(refresher.Name + " requests that the games players be reset.", DOL.GS.PacketHandler.eChatType.CT_SpellResisted, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);

                RemoveStick(player);
            }

            m_playingPlayers.Clear();
            m_redTeam.Clear();
            m_blueTeam.Clear();


        }

        public void RemoveStick(GamePlayer player)
        {
            //Count how many sticks in the players inventory - a player may have picked a team twice!
            int stickcount = player.Inventory.CountItemTemplate(TomteHockeyGameManager.HockeyStick.Id_nb, eInventorySlot.MinEquipable, eInventorySlot.LastBackpack);
            while (stickcount > 0)
            {
                InventoryItem i = player.Inventory.GetFirstItemByName(TomteHockeyGameManager.HockeyStick.Name, eInventorySlot.MinEquipable, eInventorySlot.LastBackpack);
                if (i != null)
                    player.Inventory.RemoveItem(i);
                //decrease the number of sticks remaining!
                stickcount--;
            }
        }

        #endregion

        #region Scoring voids...
        /// <summary>
        /// Increases blue score and checks for victory.
        /// </summary>
        public void BlueScores()
        {
            m_blueScore++;
            foreach (GamePlayer pl in m_blueTeam)
            {
                pl.Out.SendMessage("Your team scores!", DOL.GS.PacketHandler.eChatType.CT_ScreenCenterSmaller, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
                pl.Out.SendMessage("Your team scores!", DOL.GS.PacketHandler.eChatType.CT_SpellResisted, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
            }
            Score();
        }

        /// <summary>
        /// Increases red score and checks for victory.
        /// </summary>
        public void RedScores()
        {
            m_redScore++;
            foreach (GamePlayer pl in m_redTeam)
            {
                pl.Out.SendMessage("Your team scores!", DOL.GS.PacketHandler.eChatType.CT_ScreenCenterSmaller, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
                pl.Out.SendMessage("Your team scores!", DOL.GS.PacketHandler.eChatType.CT_SpellResisted, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
            }
            Score();
        }

        /// <summary>
        /// Called on every goal scored - checks for victory to end the game.
        /// </summary>
        public void Score()
        {
            //If we are set to reposition players, do it!
            if (RepositionPlayersOnGoal)
                RepositionPlayers();

            if (m_redScore < 3 && m_blueScore < 3)
                return; //no worries! No one has one yet.

            EndGame();
        }

        #endregion

        /// <summary>
        /// This void repositions all playing players to their respective goals. It is used at the start, and possibly on goals.
        /// </summary>
        public void RepositionPlayers()
        {
            //Move Red Team!
            foreach (GamePlayer player in m_redTeam)
            {
                player.X = X;
                player.Y = Y - YSize / 2;

                player.MoveTo(Region, player.X, player.Y, player.Z, player.GetHeadingToSpot(X, Y));
            }

            //Move Blue Team!
            foreach (GamePlayer player in m_blueTeam)
            {
                player.X = X;
                player.Y = Y + YSize / 2;

                player.MoveTo(Region, player.X, player.Y, player.Z, player.GetHeadingToSpot(X, Y));
            }

        }

        /// <summary>
        /// Clears up the match - removes goalposts and puck, ad decides the winning team.
        /// </summary>
        public void EndGame()
        {
            bool redWon = false;

            if (m_redScore >= 3)
                redWon = true;

            //By default, blue has won, unless red has.

            string winner = (redWon) ? "Reds" : "Blues";

            foreach (GamePlayer pl in PlayingPlayers)
            {
                if (pl == null)
                    continue;
                if (pl.ObjectState != GameObject.eObjectState.Active)
                    continue;

                //Inform the players of the winner!
                pl.Out.SendMessage("The game is over! Congratulations to the winning team, the " + winner + "!", DOL.GS.PacketHandler.eChatType.CT_Broadcast, DOL.GS.PacketHandler.eChatLoc.CL_ChatWindow);

                //Remove the stick from the player.
                RemoveStick(pl);
            }

            if (Puck != null)
            {
                Puck.RemoveFromWorld();
                Puck.Delete();
            }

            playState = ePlayState.Waiting;

            //Flush player lists...
            m_redTeam = new List<GamePlayer>();
            m_blueTeam = new List<GamePlayer>();
            m_playingPlayers = new List<GamePlayer>();

        }

        #region New game

        /// <summary>
        /// Starts the game playing...
        /// </summary>
        public void StartGame()
        {
            playState = ePlayState.Playing;

            //Reset scores...
            m_redScore = 0;
            m_blueScore = 0;

            //Send out a warning to players to equip and get ready for the battle!
            foreach (GamePlayer pl in m_playingPlayers)
            {
                if (pl == null || pl.ObjectState != GameObject.eObjectState.Active) //incase of linkdead players
                    continue;

                pl.Out.SendMessage("The game has now begun! Equip the hockey sticks in your inventory and swing at the puck to move it to your opponents goal. First to the score limit wins!", DOL.GS.PacketHandler.eChatType.CT_Broadcast, DOL.GS.PacketHandler.eChatLoc.CL_ChatWindow);

                pl.Out.SendMessage("The game has begun! Adorn your hockey stick!", DOL.GS.PacketHandler.eChatType.CT_ScreenCenterSmaller, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
                pl.Out.SendMessage("The game has begun! Adorn your hockey stick!", DOL.GS.PacketHandler.eChatType.CT_SpellResisted, DOL.GS.PacketHandler.eChatLoc.CL_SystemWindow);
            
            }

            //Create the puck...
            TomteHockeyPuck puck = new TomteHockeyPuck(this);
            puck.CurrentRegionID = Region;
            puck.X = X;
            puck.Y = Y;
            puck.Z = Z;
            puck.AddToWorld();

            //Assign the puck to this game.
            Puck = puck;

        }


        #endregion
    }

    #endregion

    public class TomteHockeyJudge : GameNPC
    {
        public TomteHockeyJudge()
            : base()
        {
            Name = "Master Dinberg";
            GuildName = "Tomte Hockey";
            Model = 672;
            Size = 80;
            Level = 60;
            Realm = eRealm.None;
            Flags = (uint)eFlags.PEACE;
            CheerBrain brain = new CheerBrain();
            SetOwnBrain(brain);
            MaxSpeedBase = 191;
        }

        TomteHockeyGame m_game;

        public override bool AddToWorld()
        {
            //Create a new game when added - have to do it here rather than constructor so game takes correct coords.
            TomteHockeyGame game = new TomteHockeyGame(this);
            m_game = game;

            return base.AddToWorld();
        }

        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player))
                return false;

            if (m_game.PlayState == TomteHockeyGame.ePlayState.Playing)
            {
                //The code below checks to see if the players playing are actually realistically playing the game.
                //if no one is, it stops the game so another can be played.
                bool play = false;
                foreach (GamePlayer pl in m_game.PlayingPlayers)
                    if (pl != null && pl.ObjectState == eObjectState.Active)
                        if (pl.CurrentRegionID == m_game.Region)
                            play = true;

                //play will be true if someone is playing...
                if (!play)
                {
                    m_game.EndGame();
                    //Inform the player of our action!
                    player.Out.SendMessage("Ahh it seems everyone had left the old game. I've stopped it so you can have a turn!", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                }
                else
                {
                    player.Out.SendMessage("Sorry, but if you aren't already playing you'll have to wait for the current game to finish!", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                }
            }
            else
            {
                player.Out.SendMessage("Ahh, it seems a game hasn't started yet! We need " + m_game.MaxPlayerCount + " players, and currently there are " + m_game.PlayingPlayers.Count + " waiting. Care to [join]?\nAlternatively, you can look at the [options] for a game. Perhaps you are unhappy with the current teams so far and wish to [restart] the joining process.\nThen again, if you are completely new, you probably want the [rules], eh?", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
            }

            return true;
        }

        public override bool WhisperReceive(GameLiving source, string text)
        {
            if (!base.WhisperReceive(source, text))
                return false;

            GamePlayer player = source as GamePlayer;
            if (player == null) return false;

            switch (text)
            {
                case "join":
                    //We will check if the player has already been added - if he has, we should probably warn him!
                    if (m_game.PlayingPlayers.Contains(player))
                    {
                        player.Out.SendMessage("You are already playing for a team at present. Are you [sure] you want to join again? This option is suggested for champion matches or for practice against yourself.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    }
                    else
                    m_game.TryAddPlayer(player);
                    break;
                case "sure":
                    //The player is sure, lets add them!
                    m_game.TryAddPlayer(player);
                    break;
                case "restart":
                    m_game.ResetPlayersAtStart(player);
                    break;
                    //Numbers of players...
                case "options":
                    player.Out.SendMessage("Ahh yes, you can alter the number of players required for a game - [two], [four], [six] or even [ten] - though that is hectic! Or perhaps you wish the game to [reposition] players after every goal, or [leave] them alone where they stand? Also, I can offer you the ability to turn on [magic], though by default [magic is off].\nYou can also toggle the offside rules [on] or [off], off giving a faster and more rushed game.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    break;
                case "two":
                    m_game.MaxPlayerCount = 2;
                    player.Out.SendMessage("There, it is done! I've removed any players currently joined to the game to reflect the changes.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    m_game.ResetPlayersAtStart(player);
                    break;
                case "four":
                    m_game.MaxPlayerCount = 4;
                    player.Out.SendMessage("There, it is done! I've removed any players currently joined to the game to reflect the changes.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    m_game.ResetPlayersAtStart(player);
                    break;
                case "six":
                    m_game.MaxPlayerCount = 6; 
                    player.Out.SendMessage("There, it is done! I've removed any players currently joined to the game to reflect the changes.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    m_game.ResetPlayersAtStart(player);
                    break;
                case "ten":
                    m_game.MaxPlayerCount = 10;
                    player.Out.SendMessage("There, it is done! I've removed any players currently joined to the game to reflect the changes.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    m_game.ResetPlayersAtStart(player);
                    break;
                case "magic":
                    m_game.CanUseMagic = true;
                    player.Out.SendMessage("Done, players can now move the puck with magical damage aswell as the hockey stick.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    break;
                case "magic is off":
                    m_game.CanUseMagic = false;
                    player.Out.SendMessage("I've put it back to default, magic damage will no longer move the puck.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    break;
                case "reposition":
                    m_game.RepositionPlayersOnGoal = true;
                    player.Out.SendMessage("There, players will now be moved on goal scores. This is advised for duels as it means a goal isn't left unfairly open due to one player scoring.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    break;
                case "leave":
                    m_game.RepositionPlayersOnGoal = false;
                    player.Out.SendMessage("Players will no longer be moved on a successful goal.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    break;
                case "rules":
                    player.Out.SendMessage("The aim of the game is to chase the puck through the other team's goal, while defending your own. When you join the game, you will be given a hockey stick, and can drive the puck by simply striking it in melee with this hockeystick. The first team to score three points is the winner! Best of luck, lets see if we can get a game going.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    break;
                case "on":
                    player.Out.SendMessage("Offside rules enabled! Now the team that takes it off the pitch is penalised by allowing the other team only to take the 'throw in'.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    break;
                case "off":
                    player.Out.SendMessage("There, offside rules are now off - any team may take the 'throw in' when the ball goes offside.", DOL.GS.PacketHandler.eChatType.CT_Say, DOL.GS.PacketHandler.eChatLoc.CL_PopupWindow);
                    break;
            }
            return true;
        }

    }
}
