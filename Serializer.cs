/**  © 2014, Olga K. All Rights Reserved.
  *
  *  This file is part of the ASPUtilities Library.
  *  ASPUtilities is free software: you can redistribute it and/or modify
  *  it under the terms of the GNU General Public License as published by
  *  the Free Software Foundation, either version 3 of the License, or
  *  (at your option) any later version.
  *
  *  ASPUtilities is distributed in the hope that it will be useful,
  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  *  GNU General Public License for more details.
  *
  *  You should have received a copy of the GNU General Public License
  *  along with ASPUtilities.  If not, see <http://www.gnu.org/licenses/>.
  */

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ASPUtilities
{
	public class Serializer<T>
	{
		T GenericData;
		byte[] DataInfo = null;
		MemoryStream Data = null;
		IFormatter Formatter = null;

		public Serializer(T DataInfo)
		{
			GenericData = DataInfo;
			Data = new MemoryStream();
			Formatter = new BinaryFormatter();
		}

		~Serializer()
		{
			if (Data != null)
			{
				Data.Close();
				Data.Dispose();
			}
		}

		public void Clear()
		{
			DataInfo = null;
			GenericData = default(T);
			if (Data != null) Data.SetLength(0);
		}

		private void Serialize()
		{
			try
			{
				Formatter.Serialize(Data, GenericData);
				Data.Position = 0;
			}
			catch (SerializationException Ex)
			{
				throw new SerializationException("Error In Serializing Data To The Stream:\t" + Ex.ToString());
			}
		}

		public byte[] SerializedData()
		{
			if (Data == null || Data.Length == 0)
			{
				if (GenericData == null)
					return null;

				Serialize();
			}

			return (DataInfo == null || Data.Length == 0) ? (DataInfo = this.Data.ToArray()) : DataInfo;
		}

		public byte[] SerializedData(T DataInfo)
		{
			GenericData = DataInfo;
			Serialize();
			return (this.DataInfo = this.Data.ToArray());
		}

		override public string ToString()
		{
			return Convert.ToBase64String(this.Data.ToArray());
		}
	}

	public class DeSerializer<T>
	{
		T GenericData;
		byte[] DataInfo = null;
		MemoryStream Stream = null;

		public DeSerializer(byte[] DataInfo)
		{
			this.DataInfo = DataInfo;
		}

		public DeSerializer(string DataInfo)
		{
			this.DataInfo = Convert.FromBase64String(DataInfo);
		}

		~DeSerializer()
		{
			if (Stream != null)
			{
				Stream.Close();
				Stream.Dispose();
			}
		}

		public void Clear()
		{
			DataInfo = null;
			GenericData = default(T);
			if (Stream != null) Stream.SetLength(0);
		}

		private T DeSerialize()
		{
			try
			{
				if (Stream == null)
					Stream = new MemoryStream(DataInfo);

				IFormatter Formatter = new BinaryFormatter();
				return (T)Formatter.Deserialize(Stream);
			}
			catch (SerializationException Ex)
			{
				throw new SerializationException("Error In De-Serializing Data From The Stream:\t" + Ex.ToString());
			}
		}

		private T DeSerialize(string DataInfo)
		{
			if (string.IsNullOrEmpty(DataInfo)) return default(T);
			if (Stream != null) Stream.Close();
			Stream = null;
			this.DataInfo = Convert.FromBase64String(DataInfo);
			return DeSerialize();
		}

		private T DeSerialize(byte[] DataInfo)
		{
			if (Stream != null) Stream.Close();
			Stream = null;
			this.DataInfo = DataInfo;
			return DeSerialize();
		}

		public T DeSerializedData()
		{
			if (DataInfo == null || DataInfo.Length == 0)
				return default(T);

			return (GenericData == null ? (GenericData = DeSerialize()) : GenericData);
		}

		public T DeSerializedData(string DataInfo)
		{
			if (string.IsNullOrEmpty(DataInfo)) return default(T);
			return (GenericData = DeSerialize(DataInfo));
		}

		public T DeSerializedData(byte[] DataInfo)
		{
			if (DataInfo == null || DataInfo.Length == 0) return default(T);
			return (GenericData = DeSerialize(DataInfo));
		}
	}
}
