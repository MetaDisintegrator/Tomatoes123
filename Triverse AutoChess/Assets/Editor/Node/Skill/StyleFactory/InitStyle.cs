using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Editor.NodeEditor
{
    public partial class SkillEditorWindow : Window<SkillEditorWindow>
    {
        protected override void InitStyle(IStyleFactorySystemInit system)
        {
            #region NodeºÍZone
            system.RegisterBoxStyle("NodeNormal", "NodeNormal");
            system.RegisterBoxStyle("NodeHighlight", "NodeHighlight");
            system.RegisterBoxStyle("ZoneNormal", "ZoneBoarder");
            system.RegisterBoxStyle("ZoneHighlight", "ZoneBoarderHighlight");
            #endregion

            #region Point
            system.RegisterPointStyle(E_NodeData.Int, E_NodeDataScale.Single);
            system.RegisterPointStyle(E_NodeData.Float, E_NodeDataScale.Single);
            system.RegisterPointStyle(E_NodeData.Bool, E_NodeDataScale.Single);
            system.RegisterPointStyle(E_NodeData.MapPoint, E_NodeDataScale.Single);
            system.RegisterPointStyle(E_NodeData.MapArea, E_NodeDataScale.Single);
            system.RegisterPointStyle(E_NodeData.Chess, E_NodeDataScale.Single);
            system.RegisterPointStyle(E_NodeData.Projecticle, E_NodeDataScale.Single);
            system.RegisterPointStyle(E_NodeData.IndividualData, E_NodeDataScale.Single);
            system.RegisterPointStyle(E_NodeData.UnKnown, E_NodeDataScale.Single);

            system.RegisterPointStyle(E_NodeData.Bool, E_NodeDataScale.Multiple);

            #endregion

            #region Connect
            //Int
            system.RegisterColor(E_NodeData.Int, "#088900");
            //Float
            system.RegisterColor(E_NodeData.Float, "#04FF00");
            //Bool
            system.RegisterColor(E_NodeData.Bool, "#FF20D1");
            //MapPoint
            system.RegisterColor(E_NodeData.MapPoint, "#FFBE00");
            //MapArea
            system.RegisterColor(E_NodeData.MapArea, "#FF8600");
            //Projecticle
            system.RegisterColor(E_NodeData.Projecticle, "#9A00FF");
            //Chess
            system.RegisterColor(E_NodeData.Chess, "#FF0010");
            //IndividualData
            system.RegisterColor(E_NodeData.IndividualData, "#F7F7F7");
            //Control
            system.RegisterColor(E_NodeData.Control, "#04004A");
            #endregion
        }
    }
}