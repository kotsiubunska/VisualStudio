﻿using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Composition;
using GitHub.Services;
using GitHub.UI;
using GitHub.Extensions;
using NullGuard;
using GitHub.Api;

namespace GitHub.VisualStudio.Menus
{
    [Export(typeof(IMenuHandler))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AddConnection : MenuBase, IMenuHandler
    {
        [ImportingConstructor]
        public AddConnection([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider, ISimpleApiClientFactory apiFactory)
            : base(serviceProvider, apiFactory)
        {
        }

        public Guid Guid => GuidList.guidGitHubCmdSet;
        public int CmdId => PkgCmdIDList.addConnectionCommand;

        public void Activate()
        {
            StartFlow(UIControllerFlow.Authentication);
        }

        void StartFlow(UIControllerFlow controllerFlow)
        {
            var uiProvider = ServiceProvider.GetExportedValue<IUIProvider>();
            uiProvider.RunUI(controllerFlow, null);
        }
    }
}
