using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace Editor.NodeEditor
{
    public class NodeEditorArchitecture : Architecture<NodeEditorArchitecture>, IArchitecture
    {
        protected override void Init()
        {
            RegisterSystem<IStyleFactorySystem>(new StyleFactorySystem());
            RegisterSystem<INodeFactorySystem>(new NodeFactorySystem());
            RegisterSystem<IMenuSystem>(new MenuSystem());
            RegisterSystem<IConnectSystem>(new ConnectSystem());
            RegisterSystem<IItemFactorySystem>(new ItemFactorySystem());

            RegisterModel<IZoneModel>(new ZoneModel());
            RegisterModel<INodeModel>(new NodeModel());
            RegisterModel<IConnectModel>(new ConnectModel());
            RegisterModel<IDiagramDirectoryModel>(new DiagramDirectoryModel());
            RegisterModel<IItemModel>(new ItemModel());

            RegisterUtility<IDirectoryUtility>(new DirectoryUtility());
        }
    }
}

