using System;
using System.Collections;
using DOL.GS.PacketHandler;
using DOL.Database;

namespace DOL.GS.Scripts
{
    
    [CmdAttribute(
       "&toprps",
       (uint)ePrivLevel.Player,
       "Displays the Top 20 Players with the most Realm Points.",
       "/toprps")]
    public class TopHeroCommandHandler : ICommandHandler
    {
        private static bool hasbeenrun = false; //Keep track of if it's the first time it's been called
        private static ArrayList list = new ArrayList();
        private static long LastUpdatedTime = 0; //When was the list last updated?
        private static long TimeToChange = 0;  //What region time will it be when it's time to refresh list?
        public int OnCommand(GameClient client, string[] args)
        {
            if (LastUpdatedTime == 0) { LastUpdatedTime = client.Player.CurrentRegion.Time; }
            TimeToChange = (LastUpdatedTime + 900000); //15 minutes between list refreshing
            if (((client.Player.CurrentRegion.Time) <= (TimeToChange)) && (hasbeenrun == true))
            {
                client.Player.Out.SendMessage("List is less then 15 minutes old, showing cached information.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                client.Player.Out.SendMessage("Last updated time: " + LastUpdatedTime + " Current Region Time: " + client.Player.CurrentRegion.Time + " TimeToChange: " + TimeToChange, eChatType.CT_System, eChatLoc.CL_SystemWindow);
                client.Out.SendCustomTextWindow("Top 20 Players", list);
                return 1;
            }

            if (((client.Player.CurrentRegion.Time) >= (TimeToChange)) || (hasbeenrun == false))
            {
                client.Player.Out.SendMessage("List is more then 15 minutes old, refreshing the list with updated information!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                client.Player.Out.SendMessage("Last updated time: " + LastUpdatedTime + " Current Region Time: " + client.Player.CurrentRegion.Time + " TimeToChange: " + TimeToChange, eChatType.CT_System, eChatLoc.CL_SystemWindow);
                LastUpdatedTime = client.Player.CurrentRegion.Time;
                Character[] chars = (Character[])GameServer.Database.SelectObjects(typeof(Character), "RealmPoints > 0 ORDER BY RealmPoints DESC LIMIT 20");
                list.Clear(); //Purge the list
                //LastUpdatedTime = 0; TimeToChange = 0; //reset timers.
                list.Add("Top 20 Highest Realm Points:\n\n");
                int count = 1;
                foreach (Character chr in chars)
                {
                    //Don't add people to the list if they are in my staff guild or have less then RR 4L5.
                    if ((chr.GuildID != "590ac9d8-03c6-403e-9389-fd173fa92149") && (chr.RealmPoints > 342131))
                    {
                        string str = "Rank #" + count.ToString() + ": " + chr.Name + " with " + chr.RealmPoints.ToString() + " realm points\n";
                        count++;
                        list.Add(str);
                    }
                }
                hasbeenrun = true;
                client.Out.SendCustomTextWindow("Top 20 Players", list);
                return 1;
            }
            return 1;

        }
    }
}
