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
 * for Thidranki Classic (http://www.thidrankiclassic.de)
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
 * - you could enable/disable realmlevel-based bountypoint costs for usage of transfer
 * - additional you can give a realmlevel-based costfree transfer each x-days
 * 
 * ChangeLog:
 * 2007-03-27:
 * - fixed a bug on handling LastFreeTransfer (LastFreeLeveled)
 * 2007-03-26:
 * - added costs for usages (enable/disable by property)
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
	public class RPBPTransferNPC : GameNPC
	{
		#region configuration flags
		
		/// <summary>
		/// Enable/Disable transfer whole realmpoints/realmlevel
		/// </summary>
		protected readonly bool TRANSFER_REALM = true;
		/// <summary>
		/// Enable/Disable transfer whole bountypoints
		/// </summary>
		protected readonly bool TRANSFER_BOUNTY = true;
		/// <summary>
		/// Enable/Disable transfer whole money
		/// </summary>
		protected readonly bool TRANSFER_MONEY = false;

		/// <summary>
		/// Allow/Disallow transfer crossrealm
		/// </summary>
		protected readonly bool TRANSFER_CROSSREALM = true;
		/// <summary>
		/// Allow/Disallow transfer to another account
		/// </summary>
		protected readonly bool TRANSFER_OTHERACCOUNT = false;
		/// <summary>
		/// Allow/Disallow transfer to logged in chars on another account
		/// ATTENTION: its not tested!!!
		/// </summary>
		protected readonly bool TRANSFER_LOGGEDIN = false;

		/// <summary>
		/// Enable/Disable log to GMLog
		/// if this is enabled, it only logs to GM-Log
		/// if both LOG_xxx are false, no logging
		/// </summary>
		protected readonly bool LOG_GM = true;
		/// <summary>
		/// Enable/Disable log to GameServer-Log
		/// if this is enabled AND LOG_GMLOG=false, it logs to GameServer-Log
		/// if both LOG_xxx are false, no logging
		/// </summary>
		protected readonly bool LOG_GAME = true;
		/// <summary>
		/// Shows the user a list of possible chars
		/// </summary>
		protected readonly bool CHAR_LIST = true;
		/// <summary>
		/// enable/disable bounty-point costs for usage
		/// </summary>
		protected readonly bool COSTS_BOUNTY = true;
		/// <summary>
		/// array for usage-costs in bountypoints per RR
		/// hint: I used an array (no formula), cause its better editable for a non-dev-skilled gm/admin
		/// </summary>
		protected readonly long[] BP_COSTS = {
			0,     // RR1
			500,    // RR2
			1250,   // RR3
			2500,   // RR4
			5000,   // RR5
			7500,  // RR6
			10000,  // RR7
			20000,  // RR8
			35000,  // RR9
			50000, // RR10
			75000, // RR11
			100000, // RR12
			200000 // RR13		
		};
		/// <summary>
		/// enables/disables the handling for one free transfer if COSTS_xxx is enabled
		/// ATTENTION: Use this only, if you have an instant-50-server, cause I use the
		/// db-fields "LastFreeLeveled" for saving the last transfer timestamp !!!
		/// </summary>
		protected readonly bool FREE_COSTS = false;
		/// <summary>
		/// array for free usage based on RR
		/// 0  ... never a free usage
		/// >0 ... after x days one usage is for free
		/// hint: I used an array (no formula), cause its better editable for a non-dev-skilled gm/admin
		/// </summary>
		protected readonly double[] FREE_USAGE_AFTER_DAYS = {
			1,  // RR1
			1,  // RR2
			1,  // RR3
			1,  // RR4
			1,  // RR5
			2,  // RR6
			4,  // RR7
			8,  // RR8
			16, // RR9
			30, // RR10
			90, // RR11
			0,  // RR12
			0   // RR13
		};		
		
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
            if (player.KillsLegion == 100)
            {
                if (player.RealmLevel > 90)
                {
                    player.RespecRealm();
                    player.RealmPoints = 0;
                    player.RealmSpecialtyPoints = 0;
                    player.RealmLevel = 0;
                    player.GainRealmPoints(5974125, false);
                    /*
                    if (player.RealmSpecialtyPoints < 10)
                    {
                        player.RealmSpecialtyPoints = 90;
                    }
                    if (player.RealmLevel < 10)
                    {
                        player.RealmLevel = 90;
                    }
                     */
                    player.Out.SendMessage("Greetings, we have reset everyones realm rank to RR10 if you was above realm rank 10, this should not happen again as the RP rate has been lowered drastically", eChatType.CT_Spell, eChatLoc.CL_ChatWindow);
                    player.KillsLegion = 0;
                    player.SaveIntoDatabase();
                }
            }
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
			if ( COSTS_BOUNTY )
			{
				sb.Append("- it costs an amount of bounty-points or sometimes free (both RR-based)\n");
				if ( IsFreeUsageReady(player) )
					sb.Append("-> this transfer is for free\n");
				else
					sb.Append("-> this transfer will cost ").Append((BP_COSTS[player.RealmLevel/10])).Append(" bountypoints\n");
			}
			sb.Append("- the char receiving the stuff loose all RP/BP/money\n");
			sb.Append("- thatswhy its tested if target has no RP/BP, money doesnt matter\n");			
			if ( TRANSFER_OTHERACCOUNT )
				sb.Append("- its allowed to transfer to another account\n");
			else
				sb.Append("- the both characters have to be on the same account\n");
			if ( TRANSFER_CROSSREALM )
				sb.Append("- its allowed to transfer to another realm\n");
			else
				sb.Append("- the both characters have to be on the same realm\n");
			if ( TRANSFER_LOGGEDIN )
				sb.Append("- its allowed to transfer to a logged in char\n");
			else
				sb.Append("- the receiving char have to be logged out!\n");
			if ( CHAR_LIST )
			{
				sb.Append("Possible Chars on your account:\n");				
				if ( TRANSFER_OTHERACCOUNT )
					sb.Append("(For chars on another account use /whisper CHARNAME)\n");
                foreach (DOLCharacters character in player.Client.Account.Characters)
				{
					if ( character.Name == player.Name )
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

		#region !!! ATTENTION: dont edit code below this line, except if u know what u do !!!
		
			#region WhisperReceive(): handles all the stuff :)
			
			private Hashtable m_chars = new Hashtable();

			public override bool WhisperReceive(GameLiving source, string text)
			{
				GamePlayer player = source as GamePlayer;
				if ( player == null )
					return false;
                if (player.KillsLegion == 100)
                {
                    if (player.RealmLevel > 90)
                    {
                        player.RespecRealm();
                        player.RealmPoints = 0;
                        player.RealmSpecialtyPoints = 0;
                        player.RealmLevel = 0;
                        player.GainRealmPoints(5974125, false);
                        /*
                        if (player.RealmSpecialtyPoints < 10)
                        {
                            player.RealmSpecialtyPoints = 90;
                        }
                        if (player.RealmLevel < 10)
                        {
                            player.RealmLevel = 90;
                        }
                         */
                        player.Out.SendMessage("Greetings, we have reset everyones realm rank to RR10 if you was above realm rank 10, this should not happen again as the RP rate has been lowered drastically", eChatType.CT_Spell, eChatLoc.CL_ChatWindow);
                        player.KillsLegion = 0;
                        player.SaveIntoDatabase();
                    }
                }
				long costs = BP_COSTS[player.RealmLevel/10];
	
				// check if already got an /whisper CHARNAME, this works also for charname='Yes'
				if ( !m_chars.ContainsKey(player) )
				{
					DOLCharacters character = GetCharacter(text);
					if ( character != null ) // found that char
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("\nYou choose ").Append(character.Name).Append(" as target.\n");
						if ( COSTS_BOUNTY )
						{
							if ( IsFreeUsageReady(player) )
								sb.Append("This transfer is for free.\n");
							else
							{
								sb.Append("It costs ").Append(costs).Append(" bountypoints\n");
								if ( player.BountyPoints < costs )
								{
									if ( IsLogging ) Log(player, "ABORT: fromchar="+player.Name+" tochar="+character.Name+" Reason=Not enough BP");
									return Returning(player, sb.Append("But you dont have enough bountypoints").ToString(), false);
								}
							}
						}								
						m_chars.Add(player, character);
						sb.Append("Hit [YES] if you are ready to transfer.\n");
						sb.Append("And [NO] if you will come back another time.\n");
						SayTo(player, sb.ToString());
							return true;
					}
					else
					{
						if ( IsLogging ) Log(player, "ERR: Cannot find the character '" + text + "'");
						return Returning(player, "Cannot find the character '" + text + "'!", true);
					}
				}
				else if ( text == "YES" )
				{
                    DOLCharacters fromchar = player.DBCharacter;
                    DOLCharacters tochar = (DOLCharacters)m_chars[player];
					
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
					// Costs?
					if ( COSTS_BOUNTY && (costs > player.BountyPoints) && !IsFreeUsageReady(player) )
					{
						if ( IsLogging ) Log(player, "ABORT: fromchar="+fromchar.Name+" tochar="+tochar.Name+" Reason=Not enough BP");
						return Returning(player, "But you dont have enough bountypoints and for now no free transfer ready.\nCome back if you have enough!\n", true);
					}
					#endregion
	
					#region the handling for the costs
					if ( COSTS_BOUNTY )
					{
						if ( IsFreeUsageReady(player) )
							player.DBCharacter.LastFreeLeveled = DateTime.Now;
						else
							player.BountyPoints -= costs;
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

                        fromchar.RealmPoints = 0;
                        fromchar.RealmLevel = 0;
                        fromchar.RealmSpecialtyPoints = 0;
					}
					#endregion
	
					#region the BP-Transfer
					if ( TRANSFER_BOUNTY )
					{
						tochar.BountyPoints = fromchar.BountyPoints;
						player.BountyPoints = 0;
                        fromchar.BountyPoints = 0;
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
					tochar.LastFreeLeveled = fromchar.LastFreeLeveled;
					player.SaveIntoDatabase();
					player.Out.SendUpdatePlayer();
					player.Out.SendUpdatePlayerSkills();
					player.Out.SendUpdatePoints();
					// after this, update new and old char to Database
                    GameServer.Database.SaveObject(tochar);
					GameServer.Database.SaveObject(fromchar);
                    foreach (DOLCharacters character in player.Client.Account.Characters)
                    {
                        if (character.Name == tochar.Name)
                        {
                            if (TRANSFER_BOUNTY)
                            {
                                character.BountyPoints = tochar.BountyPoints;
                            }
                            if (TRANSFER_REALM)
                            {
                                character.RealmLevel = tochar.RealmLevel;
                                character.RealmSpecialtyPoints = tochar.RealmSpecialtyPoints;
                                character.RealmPoints = tochar.RealmPoints;
                            }
                        }
                        if (character.Name == player.Name)
                        {
                            if (TRANSFER_BOUNTY)
                            {
                                character.BountyPoints = player.BountyPoints;
                            }
                            if (TRANSFER_REALM)
                            {
                                character.RealmLevel = player.RealmLevel;
                                character.RealmSpecialtyPoints = player.RealmLevel;
                                character.RealmPoints = player.RealmPoints;
                            }
                        }
                    }
					#endregion

					#region Logging the transfer
					if ( IsLogging )
					{
						StringBuilder sb = new StringBuilder("OK: from '"+fromchar.Name+"' to '"+tochar.Name+"'");
						if ( TRANSFER_REALM )
							sb.Append(" RP="+Convert.ToString(tochar.RealmPoints));
						if ( TRANSFER_BOUNTY )
							sb.Append(" BP="+Convert.ToString(tochar.BountyPoints));
						Log(player, sb.ToString());
					}
					#endregion

					return Returning(player, "Ready...\nAnd now go and fight on Neon PvP.", true);
				}
				else if ( text == "NO" )
				{
					if ( IsLogging ) Log(player, "CANCEL: from '"+player.Name+"' to '"+((DOLCharacters)m_chars[player]).Name+"'");
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
			/// Returns a given character (doesnt matter if logged in off)
			/// </summary>
			/// <param name="charname">the charactername</param>
			/// <returns>the found character or null</returns>
			private DOLCharacters GetCharacter(string charname)
			{
				//Seek players ingame first
				GameClient client = WorldMgr.GetClientByPlayerName(charname, true, false);
				if (client != null)
                    return client.Player.DBCharacter;
	
				//Return database object
                return GameServer.Database.SelectObject<DOLCharacters>("Name='" + GameServer.Database.Escape(charname) + "'");
			}
			
			/// <summary>
			/// Checks if the player has now a costfree transfer ready, if the last costfree
			/// transfer is older then the configured days in the array FREE_USAGE_AFTER_DAYS
			/// </summary>
			/// <param name="player">the player</param>
			/// <returns>true if a costfree transfer is allowed</returns>
			private bool IsFreeUsageReady(GamePlayer player)
			{
				// quick check if its enabled or player is null
				if ( !FREE_COSTS || player == null || FREE_USAGE_AFTER_DAYS[player.RealmLevel/10] == 0 )
					return false;

                DateTime dt = player.DBCharacter.LastFreeLeveled;
				dt = dt.AddDays(FREE_USAGE_AFTER_DAYS[player.RealmLevel/10]);
				return ( DateTime.Now.CompareTo(dt) >= 0 );
			}
			
			#endregion
	
		#endregion
	}
}
