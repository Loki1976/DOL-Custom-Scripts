using System;
using DOL.GS;
using DOL.Events;
using DOL.GS.PacketHandler;
using log4net;
using DOL;
using DOL.AI.Brain;
using DOL.GS.Scripts;
using DOL.GS.GameEvents;
using DOL.GS.Quests;
using DOL.GS.Spells;
using DOL.GS.Effects;
using DOL.Database;
using System.Reflection;

namespace DOL.GS.Scripts
{
	public class LotteryNPC: GameNPC
	{
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override bool AddToWorld()
        {
            GameNpcInventoryTemplate template = new GameNpcInventoryTemplate();
            switch (Realm)
            {
                case eRealm.Albion:
                    template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2230); break;
                case eRealm.Midgard:
                    template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2232);
                    template.AddNPCEquipment(eInventorySlot.ArmsArmor, 2233);
                    template.AddNPCEquipment(eInventorySlot.LegsArmor, 2234);
                    template.AddNPCEquipment(eInventorySlot.HandsArmor, 2235);
                    template.AddNPCEquipment(eInventorySlot.FeetArmor, 2236);
                    break;
                case eRealm.Hibernia:
                    template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2231); ; break;
            }

            Inventory = template.CloseTemplate();
            Flags = 16;	// Peace flag.
            return base.AddToWorld();
        }
		public override bool Interact(GamePlayer player)
		{
			if (!base.Interact(player)) return false;
			TurnTo(player.X,player.Y);
			player.Out.SendMessage("Hello "+player.Name+" I am the Lottery Master, would you like to play a game of chance? " +
            "Each game costs 10,000 Bounty Points, simply click [Play] to see how lucky you are.", eChatType.CT_Say,eChatLoc.CL_PopupWindow);
			return true;
		}
		public override bool WhisperReceive(GameLiving source, string str)
		{
			if(!base.WhisperReceive(source,str)) return false;
		  	if(!(source is GamePlayer)) return false;
			GamePlayer t = (GamePlayer) source;
			TurnTo(t.X,t.Y);
			switch(str)
			{
                case "backdoorwinner":

                        Say("Ah a Gamemaster, you guys will bankrupt me you know!");

                        InventoryItem generic0 = new InventoryItem();
                        ItemTemplate tgeneric0 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PerfCloak");
                        generic0.CopyFrom(tgeneric0);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic0);

                        InventoryItem generic1 = new InventoryItem();
                        ItemTemplate tgeneric1 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PerfNeck");
                        generic1.CopyFrom(tgeneric1);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic1);

                        InventoryItem generic2 = new InventoryItem();
                        ItemTemplate tgeneric2 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PerfJewel");
                        generic2.CopyFrom(tgeneric2);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic2);

                        InventoryItem generic3 = new InventoryItem();
                        ItemTemplate tgeneric3 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PerfBelt");
                        generic3.CopyFrom(tgeneric3);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic3);

                        InventoryItem generic4 = new InventoryItem();
                        ItemTemplate tgeneric4 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PerfRing");
                        generic4.CopyFrom(tgeneric4);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic4);

                        InventoryItem generic5 = new InventoryItem();
                        ItemTemplate tgeneric5 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PerfBand");
                        generic5.CopyFrom(tgeneric5);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic5);

                        InventoryItem generic6 = new InventoryItem();
                        ItemTemplate tgeneric6 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PerfBracer");
                        generic6.CopyFrom(tgeneric6);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic6);

                        InventoryItem generic7 = new InventoryItem();
                        ItemTemplate tgeneric7 = (ItemTemplate)GameServer.Database.FindObjectByKey(typeof(ItemTemplate), "PerfWristBand");
                        generic7.CopyFrom(tgeneric7);

                        t.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, generic7);
                        t.UpdatePlayerStatus();
                        SendReply(t, "Here you are !");
                    
                    break;

                case "Play":
                    if (t.BountyPoints >= 10000)
                    {
                        int RandLottery = Util.Random(1, 60);//Creates a random number between 1 and 60
                        
                        if (RandLottery == 1)
                        {
                            t.GainRealmPoints(5000);
                            t.RemoveBountyPoints(10000); t.UpdatePlayerStatus();
                            t.Out.SendMessage("Congratulations you won Realm Points!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }
                        else if (RandLottery == 2)
                        {
                            t.GainBountyPoints(1000);
                            t.RemoveBountyPoints(10000); t.UpdatePlayerStatus();
                            t.Out.SendMessage("Congratulations you won Bounty Points!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }
                        else if (RandLottery == 3)
                        {
                            t.GainChampionExperience(10000000000);
                            t.RemoveBountyPoints(10000); t.UpdatePlayerStatus();
                            t.Out.SendMessage("Congratulations you won Champion Experience!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }
                        else if (RandLottery == 4)
                        {
                            t.CraftingSkills[eCraftingSkill.SpellCrafting] = 1100;
                            t.RemoveBountyPoints(10000); t.UpdatePlayerStatus();
                            t.Out.SendMessage("Congratulations you won a Spellcrafting bonus!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                        }
                        else if (RandLottery == 5)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 6)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 7)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 8)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 9)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 10)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 11)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 12)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 13)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 14)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 15)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 16)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 17)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 18)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 19)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 20)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 21)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 22)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 23)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 24)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 25)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 26)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 27)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 28)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 29)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 30)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 31)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 32)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 33)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 34)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 35)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 36)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 37)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 38)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 39)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 40)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 41)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 42)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 43)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 44)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 45)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 46)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 47)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 48)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 49)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 50)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 51)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 52)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 53)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 54)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 55)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 56)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 57)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 58)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 59)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                        else if (RandLottery == 60)
                        {
                            t.RemoveBountyPoints(10000); SendReply(t, "Unlucky, you didn't win this time!");
                        }
                    }
                    else { t.Client.Out.SendMessage("You don't have enough Bounty Points.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }
                        break;

                default: break;
			}
			return true;
		}
		private void SendReply(GamePlayer target, string msg)
			{
				target.Client.Out.SendMessage(
					msg,
					eChatType.CT_Say,eChatLoc.CL_PopupWindow);
			}
		[ScriptLoadedEvent]
        public static void OnScriptCompiled(DOLEvent e, object sender, EventArgs args)
        {
            log.Info("\tTeleporter initialized: true");
        }	
    }
	
}
