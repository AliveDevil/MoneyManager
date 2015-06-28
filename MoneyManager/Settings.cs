/*
Copyright (C) 2015  Jöran Malek

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;

namespace MoneyManager
{
	public class Settings
	{
		private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
		{
			ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
			DateFormatHandling = DateFormatHandling.IsoDateFormat,
			DateParseHandling = DateParseHandling.DateTime,
			DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
			FloatFormatHandling = FloatFormatHandling.DefaultValue,
			FloatParseHandling = FloatParseHandling.Double,
			Formatting = Formatting.Indented,
			MetadataPropertyHandling = MetadataPropertyHandling.Default,
			MissingMemberHandling = MissingMemberHandling.Ignore,
			NullValueHandling = NullValueHandling.Ignore,
			ObjectCreationHandling = ObjectCreationHandling.Auto,
			PreserveReferencesHandling = PreserveReferencesHandling.All,
			ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
			StringEscapeHandling = StringEscapeHandling.EscapeHtml,
			TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
			TypeNameHandling = TypeNameHandling.Auto
		};

		public ObservableCollection<SettingsDatabaseEntry> Databases { get; set; }

		public Settings()
		{
			Databases = new ObservableCollection<SettingsDatabaseEntry>();
		}

		public SettingsDatabaseEntry NewEntry(string name, string path)
		{
			SettingsDatabaseEntry newEntry = new SettingsDatabaseEntry() { Name = name, Path = path };
			Databases.Add(newEntry);
			return newEntry;
		}

		public static Settings Load(FileInfo file)
		{
			using (var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
			using (var textReader = new StreamReader(stream))
			using (var reader = new JsonTextReader(textReader))
			{
				JsonSerializer serializer = JsonSerializer.Create(serializerSettings);
				return serializer.Deserialize<Settings>(reader);
			}
		}

		public static void Save(Settings settings, FileInfo file)
		{
			using (var stream = file.Open(FileMode.Create, FileAccess.Write, FileShare.Read))
			using (var textWriter = new StreamWriter(stream))
			using (var writer = new JsonTextWriter(textWriter))
			{
				JsonSerializer serializer = JsonSerializer.Create(serializerSettings);
				serializer.Serialize(writer, settings, typeof(Settings));
			}
		}
	}

	public class SettingsDatabaseEntry
	{
		public string Name { get; set; }

		public string Path { get; set; }
	}
}
