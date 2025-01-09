using System.Collections.Generic;
using Characters;

namespace Grid
{
    public class GridObject
    {
        private GridSystem _gridSystem;
        private GridPosition _gridPosition;

        private List<Unit> _unitList;
        
        
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
        
        
        //*****************//
        //**** GETTERS ****//
        //*****************//
        
        public List<Unit> GetUnitList() =>
            _unitList;
        
        
        //*****************//
        //**** SETTERS ****//
        //*****************//

        public void AddUnit(Unit unit) { _unitList.Add(unit); }
        
        
        //*******************//
        //**** OVERRIDES ****//
        //*******************//
        
        public override string ToString()
        {
            string unitString = "";
            
            foreach (Unit unit in _unitList) { unitString += unit.ToString() + "\n"; }
            
            return (_gridPosition.ToString() + "\n" + unitString);
        }
    }
}
