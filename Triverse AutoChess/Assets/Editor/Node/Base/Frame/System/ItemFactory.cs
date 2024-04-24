using Editor.NodeEditor.Items;
using QFramework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Editor.NodeEditor
{
    public interface IItemFactorySystem:ISystem
    {
        IItem GetSimpleItem(E_NodeData dataType,string title);
        IItem GetRepeaterItem(E_NodeData dataType,string title);
        IItem GetLabelItem(string title);
    }

    public class ItemFactorySystem : AbstractSystem, IItemFactorySystem
    {
        Dictionary<E_NodeData, Func<string,IItem>> factories;
        Dictionary<E_NodeData, Func<string, IItem>> repeaterFactories;

        protected override void OnInit()
        {
            factories = new Dictionary<E_NodeData, Func<string, IItem>>();
            repeaterFactories = new Dictionary<E_NodeData, Func<string, IItem>>();

            #region Simple
            RegisterItem(E_NodeData.Int, (title) => new ItemInt(title), factories);
            RegisterItem(E_NodeData.Float, (title) => new ItemFloat(title), factories);
            RegisterItem(E_NodeData.Bool, (title) => new ItemBool(title), factories);
            RegisterItem(E_NodeData.MapPoint, (title) => new ItemMapPoint(title), factories);
            RegisterItem(E_NodeData.MapArea, (title) => new ItemMapArea(title), factories);
            RegisterItem(E_NodeData.Projecticle, (title) => new ItemProjecticle(title), factories);
            RegisterItem(E_NodeData.Chess, (title) => new ItemChess(title), factories);
            RegisterItem(E_NodeData.IndividualData, (title) => new ItemIndividualData(title), factories);
            RegisterItem(E_NodeData.String, (title) => new ItemString(title), factories);
            RegisterItem(E_NodeData.Control, (title) => new ItemControl(title), factories);
            #endregion

            #region Repeater
            RegisterItem(E_NodeData.Int, (title) => new ItemIntRepeater(title), repeaterFactories);
            RegisterItem(E_NodeData.Float, (title) => new ItemFloatRepeater(title), repeaterFactories);
            RegisterItem(E_NodeData.Bool, (title) => new ItemBoolRepeater(title), repeaterFactories);
            RegisterItem(E_NodeData.MapPoint, (title) => new ItemMapPointRepeater(title), repeaterFactories);
            RegisterItem(E_NodeData.MapArea, (title) => new ItemMapAreaRepeater(title), repeaterFactories);
            RegisterItem(E_NodeData.Projecticle, (title) => new ItemProjecticleRepeater(title), repeaterFactories);
            RegisterItem(E_NodeData.Chess, (title) => new ItemChessRepeater(title), repeaterFactories);
            RegisterItem(E_NodeData.IndividualData, (title) => new ItemIndividualDataRepeater(title), repeaterFactories);
            RegisterItem(E_NodeData.String, (title) => new ItemStringRepeater(title), repeaterFactories);
            RegisterItem(E_NodeData.Control, (title) => new ItemControlRepeater(title), repeaterFactories);
            #endregion
        }
        
        #region ½Ó¿Ú
        public IItem GetRepeaterItem(E_NodeData dataType, string title)
        {
            return repeaterFactories[dataType].Invoke(title).Add2Model();
        }

        public IItem GetSimpleItem(E_NodeData dataType, string title)
        {
            return factories[dataType].Invoke(title).Add2Model();
        }

        public IItem GetLabelItem(string title)
        { 
            return new LabelItem(title).Add2Model();
        }
        #endregion

        private void RegisterItem(E_NodeData dataType,Func<string, IItem> factory, Dictionary<E_NodeData, Func<string,IItem>> factories)
        {
            if (factories.ContainsKey(dataType))
                Debug.LogError("ItemÖØ¸´×¢²á");
            else
                factories.Add(dataType, factory);
        }
    }

    public static class ItemFactoryExtension
    {
        public static IItem Add2Model(this IItem self)
        { 
            (self as IController).GetModel<IItemModel>().Items.Add(self);
            return self;
        }
    }
}
