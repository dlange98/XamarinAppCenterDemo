// BaseDataManager.cs
// MyWork Mobile
//
// Created on 5/22/2017 by Scott Morgan
//
// Description:
//
using System;
using Refapp.Configuration;
using Refapp.Managers;
using Refapp.Models;
using SQLite.Net;
using Xamarin.Forms;

namespace Refapp.DAO
{
	public abstract class BaseDAO 
	{
		private IFileManager FileManager;
		public BaseDAO(IFileManager fileManger)
		{
			FileManager = fileManger;
			CreateTables();
		}

		protected SQLiteConnection getDBConnection()
		{
			var path = FileManager.GetDBPath(Settings.DBName);
			var conn = new SQLiteConnection(FileManager.GetSQLPlatform(), path);
			return conn;
		}

		private void CreateTables()
		{
			using (var db = getDBConnection())
			{
				db.CreateTable<AccessToken>();
			}
		}
	}
}
