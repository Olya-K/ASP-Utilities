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
using System.IO.Compression;

namespace ASPUtilities
{
	public class Archive
	{
		private ZipType TypeOfZip;
		public enum ZipType { WinZipArchive, GZipArchive };

        /** @brief Constructor for the zip archive type.
         *
         * @param TypeOfArchive ZipType Determines whether or not the zip to be created or extract is a Windows Zip archive or a GZip archive.
         *
         */
		public Archive(ZipType TypeOfArchive)
		{
			this.TypeOfZip = TypeOfArchive;
		}


        /** @brief Zips/Archives a directory or file.
         *
         * @param SourcePath string Location of the file or directory to zip/archive.
         * @param DestinationPath string Path to the directory where the zipped/archived file will be stored.
         * @return void
         *
         */
		public void ZipDirectory(string SourcePath, string DestinationPath)
		{
			ZipFile.CreateFromDirectory(SourcePath, DestinationPath + ".zip");
		}


        /** @brief Unzips a zip/archive into a specified directory.
         *
         * @param SourceFile string Location of the file/archive to be extracted.
         * @param DestinationPath string Path to the directory where the file/archive will be extracted.
         * @return void
         *
         */
		public void UnZipFile(string SourceFile, string DestinationPath)
		{
			ZipFile.ExtractToDirectory(SourceFile, DestinationPath);
		}


        /** @brief Extracts specified files from an zip/archive.
         *
         * @param FilesToExtract string[] An array of file names of the files to be extracted from the specified zip/archive.
         * @param ZipPath string Location of the zip file containing the files to be extracted.
         * @param DestinationPath string Path to the directory where the files will be extracted.
         * @return void
         *
         */
		public void ExtractFiles(string[] FilesToExtract, string ZipPath, string DestinationPath)
		{
			using (ZipArchive Archive = ZipFile.OpenRead(ZipPath))
			{
				int I = 0;
				foreach (ZipArchiveEntry ZippedFile in Archive.Entries)
				{
					//FilesToExtract.
				}
			}
		}
	}
}
