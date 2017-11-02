/* 04/02/08 
 * Merec: On live servers there were 4 Rows with 5 trees in it.
 * seedsman do not stop planting if somone killed a tree.
 * replaced switch-case sections.
 * tree-locations are now /loc instead of /gloc coordinates
 * 
 * 03/03/08
 * BluRaven: This is the undead seedsman mob for Albion SI, Avalon Isle.
 * Located at: X: 484850  Y: 487251   Z: 3098   Region: 51, Avalon Isle.
 * There are a total of 24 tree's, in 3 rows.  Once *ALL* tree's are
 * killed by players, he will replant them all.  If a player kills a tree
 * while he is replanting, he will abort replanting and wait again for
 * *ALL* tree's to be killed before he tries to plant them again.
 * Freshly planted trees will start as level 1 seedlings, then grow to
 * their adult size and level at a random interval. (Live like!)
 * Thanks to Graveen & Dinberg for helping me with this script
 * and to the DOL staff and community that contributes to DOL! =)
 * PS: I've done my best to bugtest this script, but if you find any bugs
 * besure to send me a PM.  Feel free to make changes to
 * it but if you do, please share it with the community.
 * Thanks to Merec for his nice changes.
 * 
 * TO USE:
 * Place this script inside your release/scripts folder and restart
 * your server.  The seedsman is automatically started/created when
 * the server starts up.  He is not saved in the database, and the
 * tree's are not saved either.  They are made every time by this script.
 * 
 * Optional:
 * You can change the walk speed to make planting/moving slower/faster.
 * Just edit the WalkSpeed in this script.  Also you can change the
 * locations of the tree's or his 5 random walk spots (by editing).
 * 
 * TODO:
 * 
 * Use exact locations from live. [done by Merec]
 * 
 */

using System;
using System.Collections;
using DOL.Database;
using DOL.Events;
using DOL.GS;
using DOL.GS.PacketHandler;
using DOL.GS.Scripts;
using System.Threading;
using log4net;
using DOL.AI.Brain;

namespace DOL.GS.Scripts
{
    //Make these haunted tree's not respawn and not save in database.
    class GameHauntedTree : GameNPC
    {
        public override bool AddToWorld()
        {
             new RegionTimer(this, new RegionTimerCallback(TreeGrow), (10000 * Util.Random(3, 6)));
             return base.AddToWorld();
        }

        /// <summary>
        /// Blank for spawned mobs, which dont REspawn!
        /// </summary>
        public override void StartRespawn()
        {
        }

        public override void SaveIntoDatabase()
        {
        }

        public int TreeGrow(RegionTimer timer)
        {
            //It's been some time now, lets change the tree's name, size, and level.
            this.Size = 61;
            this.Name = "haunted appletree"; //same as live
            this.Level = (byte)Util.Random(18, 23); //from live, they range from level 18 to 23.
            return 0;
        }
    
    }


	public class UndeadSeedsman : GameNPC
	{        
        public override void SaveIntoDatabase()
        {
        }

        public static bool IsPlanting;

        private static int _TreeCounter;
        private static int _TreeCounterWhilePlanting;

        public static int WalkSpeed
        {
            get { return 500; } //The walking speed of the seedsman when planting tree's.
            set { }
        }

        //Holds the current number of trees, be sure to TreeNumber--; when a tree dies.
        public static int TreeNumber
        {

            get { return _TreeCounter; }
            set { _TreeCounter = value; }
        }
        public static int TreenumberWhilePlanting
        {
            get { return _TreeCounterWhilePlanting; }
            set { _TreeCounterWhilePlanting = value; }
        }

        //Array of Coordinates (locs) to plant tree's at
        public static int[,] Tree = {
            //  0       1      2    3
            {56958, 46853, 3355}, //0 --Row 1 Tree 1
            {57791, 46853, 3246}, //1 --Row 1 Tree 2
            {58560, 46853, 3183}, //2 --Row 1 Tree 3
            {59329, 46853, 3118}, //3 --Row 1 Tree 4
            {60114, 46853, 3061}, //4 --Row 1 Tree 5
            
            {60114, 46087, 3036}, //4 --Row 2 Tree 1
            {59329, 46087, 3129}, //5 --Row 2 Tree 2
            {58560, 46087, 3193}, //6 --Row 2 Tree 3
            {57791, 46087, 3253}, //7 --Row 2 Tree 4
            {56958, 46087, 3320}, //8 --Row 1 Tree 5
            
            {56958, 45445, 3264}, //9 --Row 3 Tree 1
            {57791, 45445, 3191}, //10--Row 3 Tree 2
            {58560, 45445, 3152}, //11--Row 3 Tree 3
            {59329, 45445, 3094}, //12--Row 3 Tree 4
            {60114, 45445, 3063}, //13--Row 3 Tree 5

            {60114, 44804, 3064}, //14--Row 4 Tree 1
            {59329, 44804, 3088}, //15--Row 4 Tree 2
            {58560, 44804, 3121}, //16--Row 4 Tree 3
            {57791, 44804, 3153}, //17--Row 4 Tree 4
            {56958, 44804, 3185}  //18--Row 4 Tree 5
        };

        public static int[,] UndeadSeedsmanWalks = {
            {484138, 486809, 3213}, // Random Walk Spot 1
            {483924, 487861, 3184}, // Random Walk Spot 2
            {483567, 488859, 3275}, // Random Walk Spot 3
            {482823, 487321, 3203}, // Random Walk Spot 4
            {482734, 488722, 3378}  // Random Walk Spot 5
        };

        //ZoneId of Avalon Isle
        private static ushort tempZoneID = 52;

        [ScriptLoadedEvent]
        public static void ScriptLoaded(DOLEvent e, object sender, EventArgs args)
        {
            UndeadSeedsman seedsman = new UndeadSeedsman();
            seedsman.Name = "undead seedsman";//from live
            seedsman.GuildName = "";
            seedsman.Model = 921;
            seedsman.Realm = 0;
            seedsman.CurrentRegionID = 51;
            seedsman.Size = 35;
            seedsman.Level = (byte)Util.Random(19, 20);//from live, he ranges in level from 19 to 20.
            seedsman.X = UndeadSeedsmanWalks[0, 0];
            seedsman.Y = UndeadSeedsmanWalks[0, 1];
            seedsman.Z = UndeadSeedsmanWalks[0, 2];
            seedsman.Heading = 880;
            seedsman.RoamingRange = 0;
            seedsman.Flags ^= (uint)GameNPC.eFlags.TRANSPARENT;
            seedsman.CurrentSpeed = 0;
            seedsman.MaxSpeedBase = 191;
            UndeadSeedsmanBrain ubrain = new UndeadSeedsmanBrain();
            ubrain.AggroLevel = 100;
            ubrain.AggroRange = 500;
            seedsman.SetOwnBrain(ubrain);
            seedsman.AddToWorld();
            seedsman.Brain.Start();
            seedsman.StartPlanting();
        }

        #region PlantTree

         //This will plant a tree at x y and z and increase the TreeNumber.
        private int PlantTree(int treeX, int treeY, int treeZ)
        {
            GameHauntedTree hauntedTree = new GameHauntedTree();
            hauntedTree.Model = 948;
            hauntedTree.Size = 25;
            hauntedTree.Level = (byte)Util.Random(6, 7); //from live, they range in level from 6 to 7.
            hauntedTree.Name = "haunted appletree seedling"; //from live
            hauntedTree.CurrentRegionID = 51;
            hauntedTree.Heading = 3195;
            hauntedTree.Realm = 0;
            hauntedTree.CurrentSpeed = 0;
            hauntedTree.MaxSpeedBase = 191;
            hauntedTree.GuildName = "";
            hauntedTree.X = treeX; //These have to vary depending on which
            hauntedTree.Y = treeY; //spot we are planting a tree at.
            hauntedTree.Z = treeZ; //Z is very important.  Strange behavior if Z is zero.
            hauntedTree.RoamingRange = 0;
            hauntedTree.RespawnInterval = -1;
            hauntedTree.BodyType = 0;

            StandardMobBrain brain = new StandardMobBrain();
            brain.AggroLevel = 100;
            brain.AggroRange = 500;
            hauntedTree.SetOwnBrain(brain);

            hauntedTree.AddToWorld();
            TreenumberWhilePlanting++;
            TreeNumber++;
            //Tell me when you die so I can TreeNumber--;
            GameEventMgr.AddHandler(hauntedTree, GameNPCEvent.Dying, new DOLEventHandler(TreeHasDied));

            return 0;
        }

        #endregion PlantTree

        public static void TreeHasDied(DOLEvent e, object sender, EventArgs args)
        {
            //if (IsPlanting) { IsPlanting = false; }
            TreeNumber--;
            //Remove the handler so they don't pile up.
            GameEventMgr.RemoveHandler(sender, GameNPCEvent.Dying, new DOLEventHandler(TreeHasDied));
            return;
        }

        #region Notify/Pathing

        public override void Notify(DOLEvent e, object sender)
        {
            base.Notify(e, sender);

            // When seedsman gets to a plant spot, plant a tree.

            if (e == GameNPCEvent.ArriveAtTarget)
            {
                if ((!IsPlanting) || (InCombat) || (IsStunned) || (IsMezzed) || (AttackState)) { return; } //disregard if not planting or in combat

                int x, y, z;
                //Treemnumber is the current index
                if (TreenumberWhilePlanting < Tree.GetLength(0))
                {
                    x = GameLocation.ConvertLocalXToGlobalX(Tree[TreenumberWhilePlanting, 0], tempZoneID);
                    y = GameLocation.ConvertLocalYToGlobalY(Tree[TreenumberWhilePlanting, 1], tempZoneID);
                    z = Tree[TreenumberWhilePlanting, 2];

                    //Increases Treenumber by 1
                    PlantTree(x, y, z);
                }

                //Treenumber was increased
                if (TreenumberWhilePlanting < Tree.GetLength(0))
                {
                    x = GameLocation.ConvertLocalXToGlobalX(Tree[TreenumberWhilePlanting, 0], tempZoneID);
                    y = GameLocation.ConvertLocalYToGlobalY(Tree[TreenumberWhilePlanting, 1], tempZoneID);
                    z = Tree[TreenumberWhilePlanting, 2];

                    WalkTo(x, y, z, WalkSpeed); //walk to next tree
                }
                else
                {
                    //Last Tree was planted, so stop Planting
                    IsPlanting = false;
                    if (!IsPlanting && !InCombat) //back at spawn loc, turn toward field of trees.
                    {
                        TurnTo(UndeadSeedsmanWalks[0, 0], UndeadSeedsmanWalks[0, 1]);
                    }
                }
            }
            return;
        }

        #endregion Notify/Pathing

        public void StartPlanting()
        {
            IsPlanting = true;

            //To start the chain we simply walk the seedsman to the first x y z.
            if (TreeNumber < 1)
            {
                TreenumberWhilePlanting = TreeNumber;
                int x = GameLocation.ConvertLocalXToGlobalX(Tree[0, 0], tempZoneID);
                int y = GameLocation.ConvertLocalYToGlobalY(Tree[0, 1], tempZoneID);
                int z = Tree[0, 2];
                
                WalkTo(x, y, z, (WalkSpeed + 50)); //x y z speed
            }
        }
	}
}

namespace DOL.AI.Brain
{
    public class UndeadSeedsmanBrain : StandardMobBrain
	{
		public UndeadSeedsmanBrain()
			: base()
		{
			AggroLevel = 100;
			AggroRange = 500;
			ThinkInterval = 30000;
		}
		public override void Think()
		{
            UndeadSeedsman seedsman = Body as UndeadSeedsman;
            // Check if seedsman is dead, returning home, stunned, or mezzed, if yes then don't think.
            if (!seedsman.IsAlive) { return; }
            if (seedsman.IsReturningHome) { return; }
            if (seedsman.AttackState) { return; }
           // if ((seedsman.IsStunned) || (seedsman.IsMezzed)) { return; }
            
            // Check if seedsman should start planting tree's
            if (UndeadSeedsman.TreeNumber < 1 && !UndeadSeedsman.IsPlanting && !seedsman.InCombat && !seedsman.IsMoving) { seedsman.StartPlanting(); }
            // Check if seedsman is idle and should random walk.
            if (UndeadSeedsman.TreeNumber >= 1 && !UndeadSeedsman.IsPlanting && !seedsman.InCombat && !seedsman.IsMoving)
            {
                //Seedsman is Idle with nothing to do.  Random Walk near spawn.
                int chance = Util.Random(1, 3);
                if (chance == 3)
                {
                    int spot = Util.Random(0, UndeadSeedsman.UndeadSeedsmanWalks.GetLength(0)-1);
                    seedsman.WalkTo(UndeadSeedsman.UndeadSeedsmanWalks[spot, 0], UndeadSeedsman.UndeadSeedsmanWalks[spot, 1], UndeadSeedsman.UndeadSeedsmanWalks[spot, 2], 80);
                }
            }
            base.Think();
		}
    }
}
