﻿using Borderlands_Save_Editor;
using Borderlands_Save_Editor.Player.Skill;
using Borderlands_Save_Editor.Save;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Save_Printer {
	class Program {
		static void Main(string[] args) {
			string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"my games\borderlands\savedata");

			Save save1 = Save.Read(Path.Combine(savePath, "save0001.sav"));
			Save save2 = Save.Read(Path.Combine(savePath, "save0002.sav"));
			Save save3 = Save.Read(Path.Combine(savePath, "save0003.sav"));
			Save save4 = Save.Read(Path.Combine(savePath, "save0004.sav"));
			Save save7 = Save.Read(Path.Combine(savePath, "save0007.sav"));
			Save save8 = Save.Read(Path.Combine(savePath, "save0008.sav"));
			Save save9 = Save.Read(Path.Combine(savePath, "save0009.sav"));
			Save save0A = Save.Read(Path.Combine(savePath, "save000A.sav"));
			Save save0B = Save.Read(Path.Combine(savePath, "save000B.sav"));
			Save save0C = Save.Read(Path.Combine(savePath, "save000C.sav"));
			// Try making a new save and read that?

			// This is just a gag.
			//save0B.Name = "Number 15: Burger king foot lettuce. The last thing you'd want in your Burger King burger is someone's foot fungus. But as it turns out, that might be what you get. A 4channer uploaded a photo anonymously to the site showcasing his feet in a plastic bin of lettuce. With the statement: \"This is the lettuce you eat at Burger King.\" Admittedly, he had shoes on. But that's even worse. The post went live at 11:38 PM on July 16, and a mere 20 minutes later, the Burger King in question was alerted to the rogue employee.At least, I hope he's rogue. How did it happen? Well, the BK employee hadn't removed the Exif data from the uploaded photo, which suggested the culprit was somewhere in Mayfleld Heights, Ohio. This was at 11:47.Three minutes later at 11:50, the Burger King branch address was posted with wishes of happy unemployment. 5 minutes later, the news station was contacted by another 4channer.And three minutes later, at 11:58, a link was posted: BK's \"Tell us about us\" online forum. The foot photo, otherwise known as exhibit A, was attached. Cleveland Scene Magazine contacted the BK in question the next day. When questioned, the breakfast shift manager said \"Oh, I know who that is. He's getting fired.\" Mystery solved, by 4chan. Now we can all go back to eating our fast food in peace.";
			save0B.SaveNumber = 0x0B;
			save0B.Color1 = Color.FromArgb(0, 255, 0, 255);
			save0B.Color2 = Color.FromArgb(0, 0, 255, 255);
			save0B.Color3 = Color.FromArgb(0, 255, 255, 0);
			save0B.Write(savePath);
			Save save0BTest = Save.Read(Path.Combine(savePath, "save000B.sav"));
			Console.WriteLine("hello");
		}
	}
}
