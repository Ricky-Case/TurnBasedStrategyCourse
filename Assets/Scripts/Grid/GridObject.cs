using System.Collections.Generic;
using StringLibrary;
using Units;

namespace Grid
{
    public class GridObject
    {
        private GridSystem _gridSystem;
        private readonly GridPosition _gridPosition;

        private readonly List<Unit> _unitList;
        
        
        //**********************//
        //**** CONSTRUCTORS ****//
        //**********************//
        
        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            _gridSystem = gridSystem;
            _gridPosition = gridPosition;
            _unitList = new List<Unit>();
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        public void RemoveUnit(Unit unit) { _unitList.Remove(unit); }

        public bool HasAnyUnit() =>
            _unitList.Count > 0;
        
        
        //*****************//
        //**** GETTERS ****//
        //*****************//
        
        public List<Unit> GetUnitList() =>
            _unitList;

        public Unit GetUnit() =>
            HasAnyUnit() ? _unitList[0] : null;
        
        
        //*****************//
        //**** SETTERS ****//
        //*****************//

        public void AddUnit(Unit unit) { _unitList.Add(unit); }


        //*******************//
        //**** OVERRIDES ****//
        //*******************//
        
        public override string ToString()
        {
            string unitID = GeneralStrings.Empty;
            
            foreach (Unit unit in _unitList) { unitID += unit + GeneralStrings.LineEnd; }
            
            return (_gridPosition.ToString() + GeneralStrings.LineEnd + unitID);
        }
    }
}
