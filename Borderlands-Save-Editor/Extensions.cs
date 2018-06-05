using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	public static class Extensions {
		public static Dictionary<V, K> Reverse<K, V>(this Dictionary<K, V> d) {
			var dictionary = new Dictionary<V, K>();
			foreach (var entry in d) {
				if (!dictionary.ContainsKey(entry.Value)) {
					dictionary.Add(entry.Value, entry.Key);
				}
			}
			return dictionary;
		}
	}
}
