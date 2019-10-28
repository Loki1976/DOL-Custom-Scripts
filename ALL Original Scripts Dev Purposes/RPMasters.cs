// I cant take credit for this script, i only say this cause i dont want people mad at me if i used part of theres
// Creation in game : /mob create DOL.GS.Scripts.RPMaster

using System;
using System.Collections;
using System.Timers;
using DOL;
using DOL.GS;
using DOL.Database;
using DOL.GS.Scripts;
using DOL.Events;
using DOL.GS.GameEvents;
using DOL.GS.PacketHandler;
using DOL.GS.Quests;

namespace DOL.GS.Scripts
{
	public class RPMasters: GameNPC
	{
		private long Price = Money.GetMoney(0,0,0,0,0);//M/P/G/S/C Amount of gold used for RemoveMoney
		private eEmote Salute = eEmote.Ponder;
		private eEmote Laugh = eEmote.Laugh;

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
            Realm = eRealm.Midgard;
            Flags = 16;	// Peace flag.
            return base.AddToWorld();
        }

		public override bool Interact(GamePlayer player)
		{
			if (!base.Interact(player)) return false;
			TurnTo(player, 100);
			foreach(GamePlayer emoteplayer in 
this.GetPlayersInRadius(WorldMgr.VISIBILITY_DISTANCE))
			{
					emoteplayer.Out.SendEmoteAnimation(this, Salute);
			}
            player.Out.SendMessage(" Isus has asked me to give you some Realm Points and some Bounty Points to get you started, just click [HERE]!\n\n", eChatType.CT_System, eChatLoc.CL_PopupWindow);

			return true;
		}
		public void LaunchSpell(string linename, int spellLevel)
		{
			Spell castSpell = null;
			SpellLine castLine = null;
			castLine = (SpellLine)SkillBase.GetSpellLine(linename);
			IList spells = SkillBase.GetSpellList(castLine.KeyName);
			foreach (Spell spell in spells)
			{
				if (spell.Level == spellLevel)
				{
					castSpell = spell;
					break;
				}
			}
			CastSpell(castSpell, castLine);
		}
		public override bool WhisperReceive(GameLiving source, string str)
		{
			if(!base.WhisperReceive(source,str)) return false;
		  	if(!(source is GamePlayer)) return false;
			GamePlayer t = (GamePlayer) source;
			switch(str)
			{
				case "HERE":


                    if (t.RealmPoints >= 61750) //Max RPs before the NPC doesnt give you any
			{
				t.Out.SendMessage("You are already have more RP's and BP's then i want to give you!", eChatType.CT_System, eChatLoc.CL_SystemWindow);

			}
            if (t.RealmPoints < 61750) //Number of RPs tht will be given
			{
          //      if (GamePlayer.Level = 20)
				{
                    
							//long amount = 13334; //Change amount of RPs given
                            t.GainRealmPoints(67375);
                            t.GainBountyPoints(4000);
                            t.Out.SendMessage("Realm Points Master has given you " + 67375 * 15 + " Realm Points and " + 4000 * 50 + "Bounty Points!", eChatType.CT_Important, eChatLoc.CL_SystemWindow);
							t.SaveIntoDatabase();
							t.Out.SendUpdatePlayer();

				}
 //   	    if(player.Level <= 19)
//				{
//				t.Out.SendMessage("You need to be Level 50!", eChatType.CT_System, 
// eChatLoc.CL_SystemWindow);
		//		}


			}
					break;

				default: break;
			}
			return true;
		}
		private void SendReply(GamePlayer target, string msg)
		{
			target.Out.SendMessage(msg,eChatType.CT_System, eChatLoc.CL_PopupWindow);
		}
	}
}

