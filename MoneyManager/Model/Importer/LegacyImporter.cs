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

//using GalaSoft.MvvmLight.CommandWpf;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Win32;
//using System.Windows;
//using System.IO;
//using MoneyManager.ViewModel;
//using Microsoft.Practices.ServiceLocation;
//using Newtonsoft.Json;
//using System.IO.Compression;
//using System.Xml.Serialization;
//using System.Data.SqlTypes;

namespace MoneyManager.Model.Importer
{
	//public static class LegacyImporter
	//{
	//	public class MoneyData
	//	{
	//		public Decimal StartAmount { get; set; }
	//		public MoneyRecord[] Records { get; set; }
	//	}
	//	public class MoneyRecord
	//	{
	//		public Guid Id { get; set; }
	//		public DateTime Date { get; set; }
	//		public string Description { get; set; }
	//		public Decimal Amount { get; set; }
	//	}

	// private static RelayCommand importCommand;

	// public static RelayCommand ImportCommand { get { return importCommand ?? (importCommand = new
	// RelayCommand(async () => { Account account = Account; if (account == null) {
	// MessageBox.Show("No account selected", "", MessageBoxButton.OK, MessageBoxImage.Error);
	// return; }

	// OpenFileDialog openFileDialog = new OpenFileDialog()._(_ => { _.CheckFileExists = true;
	// _.CheckPathExists = true; _.Filter = "MoneyManager Legacy File|*.mmgr"; _.InitialDirectory =
	// Path.GetDirectoryName(Application.ResourceAssembly.Location); _.Multiselect = false;
	// _.ValidateNames = true; }); bool? result = openFileDialog.ShowDialog(); if (result.HasValue
	// && result.Value) { MoneyData data; using (FileStream fileStream = new
	// FileStream(openFileDialog.FileName, FileMode.Open)) using (GZipStream gzipStream = new
	// GZipStream(fileStream, CompressionMode.Decompress)) { XmlSerializer serializer = new
	// XmlSerializer(typeof(MoneyData)); data = (MoneyData)serializer.Deserialize(gzipStream); }
	// await LoadRecords(account, data); data.Records = null; data = null; } })); } }

	// private static async Task LoadRecords(Account account, MoneyData data) { foreach (var item in
	// data.Records) { await Task.Yield();

	// Record record = DatabaseContext.Instance.RecordSet.Create(); record.Account = account;
	// record.Tag = DatabaseContext.Instance.DefaultTag; record.Description = item.Description; if
	// (item.Date < SqlDateTime.MinValue.Value) record.Timestamp = SqlDateTime.MinValue.Value; else
	// record.Timestamp = item.Date; record.Value = (float)item.Amount;
	// DatabaseContext.Instance.RecordSet.Add(record); } await
	// DatabaseContext.Instance.SaveChangesAsync(); }

	//	private static Account Account
	//	{
	//		get
	//		{
	//			var listModel = ServiceLocator.Current.GetInstance<AccountListViewModel>();
	//			if (listModel == null)
	//				return null;
	//			var viewModel = listModel.Account;
	//			if (viewModel == null)
	//				return null;
	//			return (Account)viewModel;
	//		}
	//	}
	//}
}
