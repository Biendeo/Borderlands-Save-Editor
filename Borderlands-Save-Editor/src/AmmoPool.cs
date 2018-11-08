using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// Represents the type of ammo used.
	/// </summary>
	public enum AmmoType {
		/// <summary>
		/// SMG ammo.
		/// </summary>
		SMG,
		/// <summary>
		/// Grenade quantity.
		/// </summary>
		Grenade,
		/// <summary>
		/// Repeater pistol ammo.
		/// </summary>
		Repeater,
		/// <summary>
		/// Revolver pistol ammo.
		/// </summary>
		Revolver,
		/// <summary>
		/// Shotgun ammo.
		/// </summary>
		Shotgun,
		/// <summary>
		/// Combat rifle ammo.
		/// </summary>
		CombatRifle,
		/// <summary>
		/// Sniper rifle ammo.
		/// </summary>
		SniperRifle,
		/// <summary>
		/// Rocket launcher ammo.
		/// </summary>
		RocketLauncher
	}

	/// <summary>
	/// Stores properties about a pool of ammo that a player has.
	/// </summary>
	public class AmmoPool {
		/// <summary>
		/// Writes this ammo pool to a given writer in the Borderlands save format.
		/// </summary>
		/// <param name="writer"></param>
		public void Write(BinaryWriter writer) {
			writer.Write(InternalName);
			writer.Write(PoolInternalName);
			writer.Write(Ammo);
			writer.Write(UpgradeLevel);
		}

		/// <summary>
		/// The internal name of the ammo in this pool used by a save file.
		/// </summary>
		public string InternalName {
			get { return AmmoDefinitions.FirstOrDefault(x => x.Value == Type).Key; }
			set { Type = AmmoDefinitions[value]; }
		}

		/// <summary>
		/// The ammo type used by this pool.
		/// </summary>
		public AmmoType Type;

		/// <summary>
		/// The internal name of the pool itself used by the save file.
		/// </summary>
		public string PoolInternalName { get { return "d_resourcepools.AmmoPools." + InternalName.Substring(0x1A) + "_Pool"; } }

		/// <summary>
		/// The amount of ammo stored by this pool. It is interestingly stored as a float, maybe for
		/// regenerating ammo weapons?
		/// </summary>
		public float Ammo;

		/// <summary>
		/// Indicates what level the pool has been upgraded too. This dictates the maximum capacity
		/// of ammo that can be stored. Different weapon types will sit at different amounts.
		/// 
		/// TODO: Note how much ammo is stored in each level.
		/// </summary>
		public Int32 UpgradeLevel;

		/// <summary>
		/// Maps internal names to types.
		/// </summary>
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

		/// <summary>
		/// Maps internal pool names to types.
		/// </summary>
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
