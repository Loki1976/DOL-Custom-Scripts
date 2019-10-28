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
 */
using System;
using System.Collections.Generic;
using System.Text;
using DOL.GS.PacketHandler;
using DOL.GS;
using DOL.Database;
using System.Collections;
using DOL.GS.Spells;
using log4net;
using System.Reflection;
using DOL.GS.Quests.Catacombs.Obelisks;
using DOL.GS.Housing;
using DOL.GS.ServerRules;

namespace DOL.GS.Scripts
{
    /// <summary>
    /// Albion teleporter.
    /// </summary>
    /// <author>created by Blues, based on Teleporters made by Aredhel</author>
    public class PvPTeleporter : GameTeleporter
    {
        /// <summary>
        /// Defines a logger for this class.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        ServerProperty curMap = GameServer.Database.SelectObject(typeof(ServerProperty), "`Key` = 'current_map'") as ServerProperty;

        /// <summary>
        /// Add equipment to the teleporter.
        /// </summary>
        /// <returns></returns>
        public override bool AddToWorld()
        {
            Model = 2026;
            Name = "PvP TELEPORTER";
            GuildName = "PvP Teleporter";
            Level = 50;
            Size = 60;
            Flags = 16;	// Peace flag.
            return base.AddToWorld();
        }

        /// <summary>
        /// Player right-clicked the teleporter.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player)) return false;
            TurnTo(player.X, player.Y);
            player.Out.SendCustomDialog("Do you want to be teleported to the current PvP Zone?", new CustomDialogResponse(TeleportToPvP));
            return true;
        }
            
        public static void TeleportToPvP(GamePlayer player, byte response)
		{
			//If your response is NOT "ok" we just return and don't do anything
			if (response != 0x01)
				return;
			//The player clicked on "OK" so we teleport him!
            ServerProperty curMap = GameServer.Database.SelectObject(typeof(ServerProperty), "`Key` = 'current_map'") as ServerProperty;
            String intro = "";
            
            switch (curMap.Value)
            {
                case "Thidranki PvP":
                    {
                        intro = String.Format("I'm now teleporting you to the current PvP Setup area");
                        player.MoveTo(238, 550627, 553876, 4424, 2921);
                        player.Bind(true);
                    }
                    break;

                case "Knarr PvP":
                    {
                        intro = String.Format("I'm now teleporting you to the current PvP Setup area");
                        player.MoveTo(151, 348551, 433572, 3712, 3338);
                        player.Bind(true);
                    }
                    break;

                case "Gothwaite PvP":
                    {
                        intro = String.Format("I'm now teleporting you to the current PvP Setup area");
                        player.MoveTo(51, 526034, 505253, 3424, 1549);
                        player.Bind(true);
                    }
                    break;


                case "Mag Mell PvP":
                    {
                        intro = String.Format("I'm now teleporting you to the current PvP Setup area");
                        player.MoveTo(200, 296554, 454088, 7139, 1101);
                        player.Bind(true);
                    }
                    break;
            }
        }


        /// <summary>
        /// Teleport the player to the designated coordinates.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="destination"></param>
        protected override void OnTeleport(GamePlayer player, Teleport destination)
        {
            OnTeleportSpell(player, destination);            
        }
    }
}

