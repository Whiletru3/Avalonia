﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Avalonia.Storage.FileIO
{
    public class BclStorageFolder : IStorageBookmarkFolder
    {
        private readonly DirectoryInfo _directoryInfo;

        public BclStorageFolder(DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo ?? throw new ArgumentNullException(nameof(directoryInfo));
            if (!_directoryInfo.Exists)
            {
                throw new ArgumentException("Directory must exist", nameof(directoryInfo));
            }
        }

        public string Name => _directoryInfo.Name;

        public bool CanBookmark => true;

        public Task<StorageItemProperties> GetBasicPropertiesAsync()
        {
            var props = new StorageItemProperties
            {
                DateModified = _directoryInfo.LastAccessTimeUtc,
                ItemDate = _directoryInfo.CreationTimeUtc
            };
            return Task.FromResult(props);
        }

        public Task<IStorageFolder?> GetParentAsync()
        {
            if (_directoryInfo.Parent is { } directory)
            {
                return Task.FromResult<IStorageFolder?>(new BclStorageFolder(directory));
            }
            return Task.FromResult<IStorageFolder?>(null);
        }

        public Task Release()
        {
            return Task.CompletedTask;
        }

        public Task<bool> RequestPermissions()
        {
            return Task.FromResult(true);
        }

        public Task<string?> SaveBookmark()
        {
            return Task.FromResult<string?>(_directoryInfo.FullName);
        }

        public bool TryGetFullPath([NotNullWhen(true)] out string? path)
        {
            path = _directoryInfo.FullName;
            return path != null;
        }
    }
}
