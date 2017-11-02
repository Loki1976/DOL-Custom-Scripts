/*
 * Originally created by Etaew.
 * Updated by Timx for performance optimisations.
 */
using System;
using System.Collections;
using DOL.GS.Commands;
using DOL.Database;
using DOL.GS.PacketHandler;

namespace DOL.GS.Scripts.Commands
{
	[CmdAttribute(
		"&train",
		ePrivLevel.Player,
		"Trains a line by the specified amount. /train is autotrain compliant.",
		"/train")]
	public class MultipleTrainCommandHandler : ICommandHandler
	{
		public void OnCommand(GameClient client, string[] args)
		{
            if (client.Player.TargetObject is GameTrainer == false)
			{
				client.Out.SendMessage("You have to be at your trainer to use this command.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				return;
			}
			if (args.Length != 3)
			{
				client.Out.SendMessage("Usage: e.g. /train Shields 40", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				return;
			}
			string line = args[1];
			int target = int.Parse(args[2]);
			if (target == -1)
			{
				client.Out.SendMessage("Invalid amount!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				return;
			}
			Specialization spec = client.Player.GetSpecialization(line);
			if (spec == null)
			{
				client.Out.SendMessage("Invalid line name, remember if there are 2 words in your line name enclose them with \"\"!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				return;
			}
			int current = spec.Level;
			if (current >= client.Player.Level)
			{
				client.Out.SendMessage("You can't train in this specialization again this level!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				return;
			}
			if (target <= current)
			{
				client.Out.SendMessage("You have already trained this amount!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
				return;
			}

            target = target - current;
            ushort skillspecialtypoints =0;
			int speclevel = 0;
			bool changed = false;
            for (int i = 0; i < target; i++)
			{
				if (spec.Level + speclevel >= client.Player.Level)
				{
					client.Out.SendMessage("You can't train in this specialization again this level!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
					break;
				}

                // graveen: /train now match 1.87 autotrain rules
				if ((client.Player.SkillSpecialtyPoints + client.Player.GetAutoTrainPoints(spec, 3)) - skillspecialtypoints >= (spec.Level + speclevel) + 1)
				{
					changed = true;
					skillspecialtypoints += (ushort)((spec.Level + speclevel) + 1);
                    if (spec.Level + speclevel < client.Player.Level / 4  && client.Player.GetAutoTrainPoints (spec,4) != 0 )
                            skillspecialtypoints -= (ushort)((spec.Level + speclevel) + 1);
					speclevel++;
                }
				else
				{
					client.Out.SendMessage("That specialization costs " + (spec.Level + 1) + " specialization points!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
					client.Out.SendMessage("You don't have that many specialization points left for this level.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
					break;
				}
			}
			if (changed)
			{
                client.Player.SkillSpecialtyPoints -= skillspecialtypoints;
				spec.Level += speclevel;
				client.Player.OnSkillTrained(spec);
				client.Out.SendUpdatePoints();
				client.Out.SendTrainerWindow();
				client.Out.SendMessage("Training complete!", eChatType.CT_System, eChatLoc.CL_SystemWindow);
			}
			return;
		}
	}
}