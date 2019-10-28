/*
 * Very simple free respec NPC who does not use items, he just directly gives the respec.
 * He gives Full, Single, and Realm respecs.  He'll stop giving that type of respec if
 * the player has more then 10 of them.  It plays a sound and screen center message
 * when the respec is granted to the player.
 * 
 * Written by BluRaven 5-22-07.
 * command to create it in game: /mob create DOL.GS.Scripts.RespecMaster
 */

using System;
using System.Collections;
using DOL;
using DOL.GS;
using DOL.Database;
using DOL.GS.Scripts;
using DOL.Events;
using DOL.GS.GameEvents;
using DOL.GS.PacketHandler;

namespace DOL.GS.Scripts
{
	public class RespecMaster: GameNPC
	{
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
			TurnTo(player, 100);
			
            player.Out.SendMessage("Hail!  I can give you give you free Respec's.  To use them, stand infront of the Trainer and type either /resec all, /respec realm, or /respec <skill name>.  Once you have obtained 10 respecs from me, I will not give you any more untill you use some of them.  Well then, if you're ready, What will it be: a [Full], [Single Line], or a [Realm] Respec?\n\n", eChatType.CT_System, eChatLoc.CL_PopupWindow);

			return true;
		}

		public override bool WhisperReceive(GameLiving source, string str)
		{
			if(!base.WhisperReceive(source,str)) return false;
		  	if(!(source is GamePlayer)) return false;
			GamePlayer t = (GamePlayer) source;
			switch(str)
			{
				case "Full":


                    if (t.RespecAmountAllSkill >= 10) //Max Full line Respec before the NPC doesnt give you any
			{
				t.Out.SendMessage("You have more then 10 full line respecs and I will not give you any more unless you use some of them first.  Try using them at the trainer.  Just type /respec all", eChatType.CT_System, eChatLoc.CL_SystemWindow);

			}
            if (t.RespecAmountAllSkill < 10)
			{
         
				{

             
                            t.RespecAmountAllSkill++;
                        
                            t.Out.SendMessage("I just added one full respec to your account.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            t.Out.SendMessage("All Skills Respec Granted!", eChatType.CT_ScreenCenter, eChatLoc.CL_SystemWindow);
                            t.Out.SendPlaySound(eSoundType.Craft, 0x04);		
                            t.SaveIntoDatabase();
							t.Out.SendUpdatePlayer();

				}


			}
					break;
                case "Single Line":


                    if (t.RespecAmountSingleSkill >= 10) //Max Full line Respec before the NPC doesnt give you any
                    {
                        t.Out.SendMessage("You have more then 10 single line respecs and I will not give you any more unless you use some of them first.  Try using them at the trainer.  Just type /respec <skill name>!", eChatType.CT_System, eChatLoc.CL_SystemWindow);

                    }
                    if (t.RespecAmountSingleSkill < 10)
                    {
                    
                        {

                            t.RespecAmountSingleSkill++;
                          
                            t.Out.SendMessage("I just added one Single Line respec to your account.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            t.Out.SendMessage("Single Line Respec Granted!", eChatType.CT_ScreenCenter, eChatLoc.CL_SystemWindow);
                            t.Out.SendPlaySound(eSoundType.Craft, 0x04);
                            t.SaveIntoDatabase();
                            t.Out.SendUpdatePlayer();

                        }


                    }
                    break;
                case "Realm":


                    if (t.RespecAmountRealmSkill >= 10) //Max Full line Respec before the NPC doesnt give you any
                    {
                        t.Out.SendMessage("You have more then 10 realm respecs and I will not give you any more unless you use some of them first.  Try using them at the trainer.  Just type /respec realm", eChatType.CT_System, eChatLoc.CL_SystemWindow);

                    }
                    if (t.RespecAmountRealmSkill < 10)
                    {
                      
                        {

                        
                            t.RespecAmountRealmSkill++;
                            t.Out.SendMessage("I just added one realm respec to your account.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                            t.Out.SendMessage("Realm skill Respec Granted!", eChatType.CT_ScreenCenter, eChatLoc.CL_SystemWindow);
                            t.Out.SendPlaySound(eSoundType.Craft, 0x04);
                            t.SaveIntoDatabase();
                            t.Out.SendUpdatePlayer();

                        }


                    }
                    break;

				default: break;
			}
			return true;
		}
		
	}
}

