/*
 * DAWN OF LIGHT - The first free open source DAoC server emulator
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 *
 *
 * Made by Biceps. If you use it, modify it, tamper with it, look at it
 * or in any way interact with it, you MUST share if anyone asks you too.
 *
 * Oh and if you actually make changes to it, post the new script at 
 * http://doldata.fleetingclarity.com
 *
 */
/*
 * Created by VaNaTiC@gmx.net 
 * for Thidranki Clasic (http://www.thidrankiclassic.de)
 * 
 * Some notes:
 * - first I have to say sorry for my rusty english :D
 * - I removed the AddToWorld() so it's customizable by yourself with /mob ...
 * - its configurable with properties, dont edit code except who know what u do
 * - feel free to derive your own TransferNPC instead of rewrite some code,
 *   so its more comfortable for yourself if I had to release some new things
 * - let this section in the files if u install it on your server
 * - and let me know if u (dis)like something
 * 
 * Features:
 * - it interacts with the player, with examine-message
 * - gives info under what rules the transfer is done (automatical handled by the properties) * 
 * - it does a full RP/BP/money transfer
 * - as a security-feature who have to start this with the char from which u wanna transfer
 * - u can choose a possible char from a list
 * - possible to do it with /whisper CHARNAME
 * - no special handling for RP/BP-factor is needed, cause of full transfer and setting to zero
 * - if realm-transfer a realmrespec is done
 * - I integrated two logs, 1st is GM-Log, 2nd is Game-Log, its only written to one of them
 * 
 * ChangeLog:
 * 2007-03-21:
 * - added char list
 * - added money transfer
 * 2007-01-14:
 * - added logging
 * - added code-doc
 * - fixed removing from old char RealmPoints if ts odd-numbered
 * 2006-12:
 * - removed constructor
 * - created
 * 
 */
using System;
using System.Text;
using System.Collections;
using System.Reflection;
using log4net;
using DOL.Database;
using DOL.GS.PacketHandler;
using DOL.GS;


namespace DOL.GS.Scripts
{
	public class TransferNPC : GameNPC
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
		#region configuration flags
		
		/// <summary>
		/// Enable/Disable transfer whole realmpoints/realmlevel
		/// </summary>
		protected const bool TRANSFER_REALM = true;
		/// <summary>
		/// Enable/Disable transfer whole bountypoints
		/// </summary>
		protected const bool TRANSFER_BOUNTY = true;
		/// <summary>
		/// Enable/Disable transfer whole money
		/// </summary>
		protected const bool TRANSFER_MONEY = false;

		/// <summary>
		/// Allow/Disallow transfer crossrealm
		/// </summary>
		protected const bool TRANSFER_CROSSREALM = true;
		/// <summary>
		/// Allow/Disallow transfer to another account
		/// </summary>
		protected const bool TRANSFER_OTHERACCOUNT = false;
		/// <summary>
		/// Allow/Disallow transfer to logged in chars on another account
		/// ATTENTION: its not tested!!!
		/// </summary>
		protected const bool TRANSFER_LOGGEDIN = false;

		/// <summary>
		/// Enable/Disable log to GMLog
		/// if this is enabled, it only logs to GM-Log
		/// if both LOG_xxx are false, no logging
		/// </summary>
		protected const bool LOG_GM = true;
		/// <summary>
		/// Enable/Disable log to GameServer-Log
		/// if this is enabled AND LOG_GMLOG=false, it logs to GameServer-Log
		/// if both LOG_xxx are false, no logging
		/// </summary>
		protected const bool LOG_GAME = true;
		/// <summary>
		/// Shows the user a list of possible chars
		/// </summary>
		protected const bool CHAR_LIST = true;
		
		#endregion
		
		#region GetExamineMessages() and Interact(): begin of your smalltalk
		
		public override IList GetExamineMessages(GamePlayer player)
		{
			/*
			 * You examine NAME. He is friendly and is a special guy.
			 * [Talk to him, if you want to transfer some things to another char]
			 */
			IList list = new ArrayList();
			list.Add("You target [" + GetName(0, false) + "]");
			list.Add("You examine " + GetName(0, false) + "  " + GetPronoun(0, true) + " is " + GetAggroLevelString(player, false) + " and is a special guy.");
			list.Add("[Talk to him, if you want to transfer some things to another char]");
			return list;
		}

		public override bool Interact(GamePlayer player)
		{
			if (!base.Interact(player))
				return false;

			TurnTo(player.X, player.Y);
			StringBuilder sb = new StringBuilder();
			sb.Append("Hello ").Append(player.Name).Append(",\n");
			sb.Append("I can transfer some things to another char.\n");
			sb.Append("Howto:\n");
			if ( CHAR_LIST )
				sb.Append("- choose one from the list or\n");
			sb.Append("- whisper me the char name with /whisper CHARNAME\n");
			sb.Append("- hit YES if you are willing to do this\n");
			sb.Append("Whats transfered:\n");
			if ( TRANSFER_REALM )
				sb.Append("- your whole amout of realmpoints and realmlevel\n");
			else
				sb.Append("- no realmpoints and no realmlevel\n");
			if ( TRANSFER_BOUNTY )
				sb.Append("- your whole amout of bountypoints\n");
			else
				sb.Append("- no bountypoints\n");
			if ( TRANSFER_MONEY )
				sb.Append("- your whole amout of money\n");
			else
				sb.Append("- no money\n");
			sb.Append("Rules:\n");
			sb.Append("- the char receiving the stuff loose all RP/BP/money\n");
			sb.Append("- thatswhy its tested for empty RP/BP, money doesnt matter\n");			
			if ( TRANSFER_OTHERACCOUNT )
				sb.Append("- its allowed to transfer to another account\n");
			else
				sb.Append("- the both characters have to be on the same account\n");
			if ( TRANSFER_CROSSREALM )
				sb.Append("- its allowed to transfer to another realm\n");
			else
				sb.Append("- the both characters have to be on the same realm\n");
			if ( TRANSFER_LOGGEDIN )
				sb.Append("- its allowed to transfer to another realm\n");
			else
				sb.Append("- the receiving char have to be logged out!\n");
			if ( CHAR_LIST )
			{
				sb.Append("Possible Chars on your account:\n");				
				if ( TRANSFER_OTHERACCOUNT )
					sb.Append("(For chars on another account use /whisper CHARNAME)\n");
				foreach ( Character character in player.Client.Account.Characters )
				{
					if (( character.Name == player.Name ) && (player.RealmPoints > 61750))
						continue;
					
					// not nice, but very readable :D
					bool ok1 = TRANSFER_CROSSREALM || (character.Realm == (int)player.Realm);
					bool ok2 = !TRANSFER_BOUNTY || (character.BountyPoints == 0);
					bool ok3 = !TRANSFER_REALM || (character.RealmPoints == 0);
					if ( ok1 && ok2 && ok3 )
						sb.Append("- [").Append(character.Name).Append("]");
					else
						sb.Append("- ").Append(character.Name);
					
					sb.Append(" L").Append(character.Level).Append(", RR");
					sb.Append((int)(character.RealmLevel+10)/10).Append("L");
					sb.Append((character.RealmLevel+10)%10).Append("\n");
				}
			}// if CHAR_LIST
			return Returning(player, sb.ToString(), true);
		}

		#endregion

		#region !!! ATTENTION: dont edit code under this line, except if u know what u do !!!
		
			#region WhisperReceive(): handles all the stuff :)
			
			private Hashtable m_chars = new Hashtable();

			public override bool WhisperReceive(GameLiving source, string text)
			{
				GamePlayer player = source as GamePlayer;
				if ( player == null )
					return false;
	
				// check if already got an /whisper CHARNAME, this works also for charname='Yes'
				if ( !m_chars.ContainsKey(player) )
				{
					Character character = GetCharacter(text);
					if ( character != null ) // found that char
					{
						m_chars.Add(player, character);
						SayTo(player,
						      "\nYou choose " + character.Name + " as target.\n"+
						      "Hit [YES] if you are ready to transfer.\n"+
						      "And [NO] if you will come back another time.\n");
						return true;
					}
					else
					{
						if ( IsLogging ) Log(player, "ERR: Cannot find the character '" + text + "'");
						Returning(player, "Cannot find the character '" + text + "'!", true);
					}
				}
				else if ( text == "YES" )
				{
					Character fromchar = player.PlayerCharacter;
					Character tochar = (Character)m_chars[player];
					
					#region some checks before
					// NULL check
					if ( fromchar == null || tochar == null )
					{
						if ( IsLogging ) Log(player, "ERR: fromchar="+fromchar+" tochar="+tochar);
						return Returning(player, "Something went wrong with your characters.\nPlease try later!\n", true);
					}
					// Same Account?
					if ( !TRANSFER_OTHERACCOUNT && (fromchar.AccountName != tochar.AccountName) )
					{
						if ( IsLogging ) Log(player, "ABORT: fromchar="+fromchar.Name+" tochar="+tochar.Name+" Reason=Accounts not same");
						return Returning(player, "Both characters dont belong to the same account.\nNext time try better!\n", true);
					}
					// Same Realm?
					if ( !TRANSFER_CROSSREALM && (fromchar.Realm != tochar.Realm) )
					{
						if ( IsLogging ) Log(player, "ABORT: fromchar="+fromchar.Name+" tochar="+tochar.Name+" Reason=Realms not same");
						return Returning(player, "Both characters dont belong to the same realm.\nNext time try better!\n", true);
					}
					// Legal RPs ToChar?
					if ( ( TRANSFER_REALM && (tochar.RealmPoints != 0) ) ||
					     ( TRANSFER_BOUNTY && (tochar.BountyPoints != 0) ) )
					{
						if ( IsLogging ) Log(player, "ABORT: fromchar="+fromchar.Name+" tochar="+tochar.Name+" Reason=ToChar already have RP/BP");
						return Returning(player, "The receiving character already have RP/BP!\nTry another character\n", true);
					}
					#endregion
	
					#region the RP-Transfer/Respec
					if ( TRANSFER_REALM )
					{
						tochar.RealmLevel = fromchar.RealmLevel;
						tochar.RealmSpecialtyPoints = fromchar.RealmLevel;
						tochar.RealmPoints = fromchar.RealmPoints;
						player.RealmPoints = 0;
						player.RealmLevel = 0;
						player.RealmSpecialtyPoints = 0;
						player.RespecRealm();
					}
					#endregion
	
					#region the BP-Transfer
					if ( TRANSFER_BOUNTY )
					{
						tochar.BountyPoints = fromchar.BountyPoints;
						player.BountyPoints = 0;
					}
					#endregion
					
					#region the Money-transfer
					if ( TRANSFER_MONEY )
					{
						tochar.Mithril = fromchar.Mithril; fromchar.Mithril = 0;
						tochar.Platinum = fromchar.Platinum; fromchar.Platinum = 0;
						tochar.Gold = fromchar.Gold; fromchar.Gold = 0;
						tochar.Silver = fromchar.Silver; fromchar.Silver = 0;
						tochar.Copper = fromchar.Copper; fromchar.Copper = 0;
					}
					#endregion
					
					#region update current player to the Database
					player.SaveIntoDatabase();
					player.Out.SendUpdatePlayer();
					player.Out.SendUpdatePlayerSkills();
					player.Out.SendUpdatePoints();
					// after this, update new char to Database
					GameServer.Database.SaveObject(tochar);
					#endregion

					if ( IsLogging )
					{
						StringBuilder sb = new StringBuilder("OK: from '"+fromchar.Name+"' to '"+tochar.Name+"'");
						if ( TRANSFER_REALM )
							sb.Append(" RP="+Convert.ToString(tochar.RealmPoints));
						if ( TRANSFER_BOUNTY )
							sb.Append(" BP="+Convert.ToString(tochar.BountyPoints));
						Log(player, sb.ToString());
					}
					return Returning(player, "Ready...\nAnd now go and fight on Atlantis PvP.", true);
				}
				else if ( text == "NO" )
				{
					if ( IsLogging ) Log(player, "CANCEL: from '"+player.Name+"' to '"+((Character)m_chars[player]).Name+"'");
					Returning(player, "Go away and dont disturb me!\n", true);
				}
				return false;
			}
			
			#endregion

			#region private utility methods
			
			/// <summary>
			/// Returns if one of the Logging-Flags are enabled
			/// </summary>
			private bool IsLogging
			{
				get { return LOG_GAME || LOG_GM; }
			}
			
			public static readonly ILog LOG = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

			private void Log(GamePlayer player, string text)
			{
				if ( player != null )
					text = "("+player.Name+"): "+text;
				if ( LOG_GM )
					GameServer.Instance.LogGMAction("[TransferNPC] "+text);
				else if ( LOG_GAME )
					LOG.Info(text);
			}
			
			/// <summary>
			/// Quickly sends an answer and returns while removing the chars from the internal map
			/// </summary>
			/// <param name="player">the player</param>
			/// <param name="msg">the message</param>
			/// <param name="b">the result which is returned</param>
			/// <returns>returns the param b</returns>
			private bool Returning(GamePlayer player, string msg, bool b)
			{
				m_chars.Remove(player);
				SayTo(player, msg);
				return b;
			}
			
			/// <summary>
			/// Format "say" message and send it to target in popup window
			/// </summary>
			/// <param name="target"></param>
			/// <param name="message"></param>
			public virtual void SayTo(GamePlayer target, string message)
			{
				// didnt like that ... speaks to ... and so on
				target.Out.SendMessage(message, eChatType.CT_System, eChatLoc.CL_PopupWindow);
			}

			/// <summary>
			/// Returns TRUE if the character with the given name is logged in
			/// </summary>
			/// <param name="charname">the charactername</param>
			/// <returns>true if the char is logged in</returns>
			private bool CharacterIsOnline(string charname)
			{
				return ( WorldMgr.GetClientByPlayerName(charname, true, false) != null );
			}
			
			/// <summary>
			/// Returns a given character logged in or logged of
			/// </summary>
			/// <param name="charname">the charactername</param>
			/// <returns>the found character or null</returns>
			private Character GetCharacter(string charname)
			{
				//Seek players ingame first
				GameClient client = WorldMgr.GetClientByPlayerName(charname, true, false);
				if (client != null)
					return client.Player.PlayerCharacter;
	
				//Return database object
				return (Character) GameServer.Database.SelectObject(typeof (Character), "Name='" + GameServer.Database.Escape(charname) + "'");
			}
			
			#endregion
	
		#endregion
	}
}
