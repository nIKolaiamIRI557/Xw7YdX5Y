// 代码生成时间: 2025-09-08 22:00:50
// <copyright file="BulkFileRenamer.cs" company="YourCompany">
//   Copyright (c) YourCompany. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Text.RegularExpressions;

namespace YourCompany.Tools
{
    /// <summary>
    /// Class responsible for bulk renaming files in a specified directory.
    /// </summary>
    public class BulkFileRenamer
    {
        private readonly string _directoryPath;
        private readonly Regex _searchPattern;
        private readonly string _replacementPattern;
        private readonly string _fileExtension;

        /// <summary>
        /// Initializes a new instance of the BulkFileRenamer class.
        /// </summary>
        /// <param name="directoryPath">The directory path where files are located.</param>
        /// <param name="searchPattern">The regex pattern to search for in the file names.</param>
        /// <param name="replacementPattern">The replacement string for the search pattern.</param>
        /// <param name="fileExtension">The file extension to consider for renaming.</param>
        public BulkFileRenamer(string directoryPath, string searchPattern, string replacementPattern, string fileExtension)
        {
            _directoryPath = directoryPath ?? throw new ArgumentNullException(nameof(directoryPath));
            _searchPattern = new Regex(searchPattern ?? throw new ArgumentNullException(nameof(searchPattern)));
            _replacementPattern = replacementPattern ?? throw new ArgumentNullException(nameof(replacementPattern));
            _fileExtension = fileExtension ?? throw new ArgumentNullException(nameof(fileExtension));
        }

        /// <summary>
        /// Renames all files in the specified directory matching the search pattern.
        /// </summary>
        public void RenameFiles()
        {
            if (!Directory.Exists(_directoryPath))
            {
                throw new DirectoryNotFoundException($"The directory '{_directoryPath}' was not found.");
            }

            string[] files = Directory.GetFiles(_directoryPath, $"*.{_fileExtension}");
            foreach (var file in files)
            {
                try
                {
                    string fileName = Path.GetFileName(file);
                    string newFileName = _searchPattern.Replace(fileName, _replacementPattern);
                    string newFilePath = Path.Combine(_directoryPath, newFileName);
                    File.Move(file, newFilePath);
                    Console.WriteLine($"Renamed '{file}' to '{newFilePath}'");
                }
                catch (Exception ex)
                {
                    // Log or handle the error appropriately.
                    Console.WriteLine($"Error renaming file '{file}': {ex.Message}");
                }
            }
        }
    }
}
