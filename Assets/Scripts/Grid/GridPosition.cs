using System;

namespace Grid
{
    public struct GridPosition : IEquatable<GridPosition>
    {
        public int X;
        public int Z;

        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        public bool Equals(GridPosition other) =>
            this == other;
        
        
        //**********************//
        //**** CONSTRUCTORS ****//
        //**********************//
        
        public GridPosition(int x, int z)
        {
            X = x;
            Z = z;
        }

        
        //*******************//
        //**** OVERRIDES ****//
        //*******************//
        
        public override string ToString() =>
            $"{X} X {Z} ";

        public static bool operator ==(GridPosition gridPositionA, GridPosition gridPositionB) =>
            gridPositionA.X == gridPositionB.X && gridPositionA.Z == gridPositionB.Z;

        public static bool operator !=(GridPosition gridPositionA, GridPosition gridPositionB) => 
            !(gridPositionA == gridPositionB);

        public static GridPosition operator +(GridPosition gridPositionA, GridPosition gridPositionB) =>
            new GridPosition(gridPositionA.X + gridPositionB.X, gridPositionA.Z + gridPositionB.Z);
        
        public static GridPosition operator -(GridPosition gridPositionA, GridPosition gridPositionB) =>
            new GridPosition(gridPositionA.X - gridPositionB.X, gridPositionA.Z - gridPositionB.Z);

        public override bool Equals(object obj) =>
            obj is GridPosition position && X == position.X && Z == position.Z;

        public override int GetHashCode() =>
            HashCode.Combine(X, Z);
    }
}