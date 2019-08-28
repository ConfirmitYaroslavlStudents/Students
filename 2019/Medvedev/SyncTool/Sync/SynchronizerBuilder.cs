﻿using System;
using System.Collections.Generic;
using Sync.ConflictDetectionPolicies;
using Sync.Interactors;
using Sync.Providers;
using Sync.ResolvingPolicies;
using Sync.Visitors;

namespace Sync
{
    public class SynchronizerBuilder
    {
        private IProvider _fileSystemProvider;
        private IInteractor _fileSystemInteractor;

        private string _masterPath;
        private Dictionary<string, ResolverOptions> _slaves = new Dictionary<string, ResolverOptions>();

        private IConflictDetectionPolicy _conflictDetectionPolicy;

        private ILoggerVisitor _logger;

        public SynchronizerBuilder SetProvider(IProvider provider)
        {
            _fileSystemProvider = provider;
            return this;
        }

        public SynchronizerBuilder SetInteractor(IInteractor interactor)
        {
            _fileSystemInteractor = interactor;
            return this;
        }

        public SynchronizerBuilder SetMaster(string path)
        {
            _masterPath = path;
            return this;
        }

        public SynchronizerBuilder AddSlave(string path, ResolverOptions option = ResolverOptions.None)
        {
            _slaves.Add(path, option);
            return this;
        }

        public SynchronizerBuilder SetConflictDetectionPolicy(IConflictDetectionPolicy policy)
        {
            _conflictDetectionPolicy = policy;
            return this;
        }

        public SynchronizerBuilder SetLogger(ILoggerVisitor logger)
        {
            _logger = logger;
            return this;
        }

        public void Synchronize()
        {
            SyncMasterWithSlaves();
            SyncSlavesWithMaster();
        }

        private void SyncSlavesWithMaster()
        {
            foreach (var kvp in _slaves)
                Synchronize(_masterPath, kvp.Key, kvp.Value);
        }

        private void SyncMasterWithSlaves()
        {
            foreach (var kvp in _slaves)
                Synchronize(_masterPath, kvp.Key, kvp.Value);
        }

        private void Synchronize(string pathToMaster, string pathToSlave, ResolverOptions option)
        {
            var master = _fileSystemProvider.LoadDirectory(pathToMaster);
            var slave = _fileSystemProvider.LoadDirectory(pathToSlave);

            var conflicts = new ConflictsCollector(
                    master,
                    slave,
                    _conflictDetectionPolicy)
                .GetConflicts();

            IResolvingPolicy resolvingPolicy = null;

            if (option == ResolverOptions.NoDelete)
                resolvingPolicy = new NoDeleteResolvingPolicy(master, slave);
            else
                resolvingPolicy = new DefaultResolvingPolicy(master, slave);

            var resolutions = new Resolver(resolvingPolicy)
                .GetConflictsResolutions(conflicts);

            var commiter = new Commiter(_fileSystemInteractor, _logger);

            commiter.Commit(resolutions);

            _logger.WriteLog(Console.Out);
        }
    }
}