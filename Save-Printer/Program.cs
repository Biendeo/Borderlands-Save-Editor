using Borderlands_Save_Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Save_Printer {
	class Program {
		static void Main(string[] args) {
			Save save1 = Save.Read("D:/Documents/my games/borderlands/savedata/save0001.sav");
			Save save2 = Save.Read("D:/Documents/my games/borderlands/savedata/save0002.sav");
			Save save3 = Save.Read("D:/Documents/my games/borderlands/savedata/save0003.sav");
			Save save4 = Save.Read("D:/Documents/my games/borderlands/savedata/save0004.sav");
			Save save7 = Save.Read("D:/Documents/my games/borderlands/savedata/save0007.sav");
			Save save8 = Save.Read("D:/Documents/my games/borderlands/savedata/save0008.sav");
			Save save9 = Save.Read("D:/Documents/my games/borderlands/savedata/save0009.sav");
			// Try making a new save and read that?

			// This is just a gag.
			save9.Name = "Number 15: Burger king foot lettuce. The last thing you'd want in your Burger King burger is someone's foot fungus. But as it turns out, that might be what you get. A 4channer uploaded a photo anonymously to the site showcasing his feet in a plastic bin of lettuce. With the statement: \"This is the lettuce you eat at Burger King.\" Admittedly, he had shoes on. But that's even worse. The post went live at 11:38 PM on July 16, and a mere 20 minutes later, the Burger King in question was alerted to the rogue employee.At least, I hope he's rogue. How did it happen? Well, the BK employee hadn't removed the Exif data from the uploaded photo, which suggested the culprit was somewhere in Mayfleld Heights, Ohio. This was at 11:47.Three minutes later at 11:50, the Burger King branch address was posted with wishes of happy unemployment. 5 minutes later, the news station was contacted by another 4channer.And three minutes later, at 11:58, a link was posted: BK's \"Tell us about us\" online forum. The foot photo, otherwise known as exhibit A, was attached. Cleveland Scene Magazine contacted the BK in question the next day. When questioned, the breakfast shift manager said \"Oh, I know who that is. He's getting fired.\" Mystery solved, by 4chan. Now we can all go back to eating our fast food in peace.";
			save9.Level = 69;
			save9.Experience = Int32.MaxValue;
			save9.UnknownVariable11 = 10;
			save9.SkillPoints = 0;
			save9.SaveTimeString = "20200420060901";
			save9.Skills.Clear();
			foreach (SkillType type in Enum.GetValues(typeof(SkillType))) {
				Skill skill = new Skill();
				skill.Level = 6;
				skill.Type = type;
				skill.EquippedElemental = 1;
				if (skill.InternalName.StartsWith("gd_skills2_Mordecai")) {
					save9.Skills.Add(skill);
				}
			}
			save9.Write("D:/Documents/my games/borderlands/savedata/save0011.sav");
			Save save9Test = Save.Read("D:/Documents/my games/borderlands/savedata/save0011.sav");
			Console.WriteLine("hello");
		}
	}
}
