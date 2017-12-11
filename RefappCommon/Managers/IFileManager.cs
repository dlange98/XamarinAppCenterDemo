//  FILE: IFileManager.cs
//  AUTHOR: smorgan@cardinalsolutions.com
//  DATE: 12/6/2016
//  DESCRIPTION:
//  Interface for platform specific implementations for getting local file resources
//
//
using System;
using System.IO;
using SQLite.Net.Interop;

namespace Refapp.Managers
{
	public interface IFileManager
	{
		//* Get a Stream give a file path
		Stream GetStream(string fullPath);

		// Get the string file path wit the given filename (dbName)
		string GetDBPath(string dbName);

		// Pull out the filename given the path
		string GetFileNameFromPath(string path);

		// Return the platform specific SQLite implemenation
		ISQLitePlatform GetSQLPlatform();

	}
}
