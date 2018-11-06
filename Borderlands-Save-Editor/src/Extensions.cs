﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {
	/// <summary>
	/// Helpful extension methods that are used across the library.
	/// </summary>
	public static class Extensions {
		/// <summary>
		/// Returns a reversed dictionary where all the key value pairs are swapped. It assumes that
		/// the values are all unique.
		/// </summary>
		/// <typeparam name="K"></typeparam>
		/// <typeparam name="V"></typeparam>
		/// <param name="d"></param>
		/// <returns></returns>
		public static Dictionary<V, K> Reverse<K, V>(this Dictionary<K, V> d) {
			var dictionary = new Dictionary<V, K>();
			foreach (var entry in d) {
				if (!dictionary.ContainsKey(entry.Value)) {
					dictionary.Add(entry.Value, entry.Key);
				}
			}
			return dictionary;
		}

		/// <summary>
		/// Returns a subarray of the given array from a starting index and a length.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="index"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static T[] SubArray<T>(this T[] data, int index, int length) {
			T[] result = new T[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}

		/// <summary>
		/// Reads a string from a <see cref="BinaryReader"/> in the Borderlands format. This
		/// involves reading a <see cref="Int32"/>, and then reading in that number of characters
		/// afterwards.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static string BL_ReadString(this BinaryReader reader) {
			Int32 length = reader.ReadInt32();
			return Encoding.ASCII.GetString(reader.ReadBytes(length)).TrimEnd('\0');
		}

		/// <summary>
		/// Writes a string to a <see cref="BinaryWriter"/> in the Borderlands format. This involves
		/// writing an <see cref="Int32"/> of the string's length, then writing the string's values
		/// afterwards.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="str"></param>
		public static void BL_WriteString(this BinaryWriter writer, string str) {
			writer.Write(str.Length + 1);
			writer.Write(Encoding.ASCII.GetBytes(str));
			// The terminating 0 was removed when reading, so we write it.
			writer.Write((byte)'\0');
		}
	}
}
