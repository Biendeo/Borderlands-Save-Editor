using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	public enum AmmoType {
		SMG,
		Grenade,
		Repeater,
		Revolver,
		Shotgun,
		CombatRifle,
		SniperRifle,
		RocketLauncher
	}

	public class AmmoPool {
		public string InternalName;
		public AmmoType Type {
			get { return AmmoDefinitions[InternalName]; }
			set { InternalName = AmmoDefinitions.FirstOrDefault(x => x.Value == value).Key; }
		}

		public string PoolInternalName { get { return "d_resourcepools.AmmoPools." + InternalName.Substring(0x1A) + "_Pool"; } }
		public float Ammo;
		public Int32 UpgradeLevel;

		public static readonly Dictionary<string, AmmoType> AmmoDefinitions = new Dictionary<string, AmmoType> {
			{ "d_resources.AmmoResources.Ammo_Patrol_SMG", AmmoType.SMG },
			{ "d_resources.AmmoResources.Ammo_Grenade_Protean", AmmoType.Grenade },
			{ "d_resources.AmmoResources.Ammo_Repeater_Pistol", AmmoType.Repeater },
			{ "d_resources.AmmoResources.Ammo_Revolver_Pistol", AmmoType.Revolver },
			{ "d_resources.AmmoResources.Ammo_Combat_Shotgun", AmmoType.Shotgun },
			{ "d_resources.AmmoResources.Ammo_Combat_Rifle", AmmoType.CombatRifle },
			{ "d_resources.AmmoResources.Ammo_Sniper_Rifle", AmmoType.SniperRifle },
			{ "d_resources.AmmoResources.Ammo_Rocket_Launcher", AmmoType.RocketLauncher }
		};

		public static readonly Dictionary<string, AmmoType> PoolDefinitions = new Dictionary<string, AmmoType> {
			{ "d_resourcepools.AmmoPool.Ammo_Patrol_SMG_Pool", AmmoType.SMG },
			{ "d_resourcepools.AmmoPool.Ammo_Grenade_Protean_Pool", AmmoType.Grenade },
			{ "d_resourcepools.AmmoPool.Ammo_Repeater_Pistol_Pool", AmmoType.Repeater },
			{ "d_resourcepools.AmmoPool.Ammo_Revolver_Pistol_Pool", AmmoType.Revolver },
			{ "d_resourcepools.AmmoPool.Ammo_Combat_Shotgun_Pool", AmmoType.Shotgun },
			{ "d_resourcepools.AmmoPool.Ammo_Combat_Rifle_Pool", AmmoType.CombatRifle },
			{ "d_resourcepools.AmmoPool.Ammo_Sniper_Rifle_Pool", AmmoType.SniperRifle },
			{ "d_resourcepools.AmmoPool.Ammo_Rocket_Launcher_Pool", AmmoType.RocketLauncher }
		};
	}
}
