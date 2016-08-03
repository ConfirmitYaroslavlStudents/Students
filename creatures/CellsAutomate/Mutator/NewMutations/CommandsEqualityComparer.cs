using System;
using System.Collections.Generic;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.NewMutations
{
    public class CommandsEqualityComparer : IEqualityComparer<ICommand>
    {
        public bool Equals(ICommand x, ICommand y)
        {
            if (x == null || y == null) return false;
            if (x.GetType() != y.GetType()) return false;

            if (x is NewInt) return EqualsNewInt((NewInt)x, (NewInt)y);
            if (x is CloneValue) return EqualsCloneValue((CloneValue)x, (CloneValue)y);
            if (x is Condition) return EqualsCondition((Condition)x, (Condition)y);
            if (x is GetRandom) return EqualsGetRandom((GetRandom)x, (GetRandom)y);
            if (x is GetState) return EqualsGetState((GetState)x, (GetState)y);
            if (x is Minus) return EqualsMinus((Minus)x, (Minus)y);
            if (x is Plus) return EqualsPlus((Plus)x, (Plus)y);
            if (x is Print) return EqualsPrint((Print)x, (Print)y);
            if (x is CloseCondition) return EqualsCloseCondition((CloseCondition)x, (CloseCondition)y);
            if (x is Stop) return EqualsStop((Stop)x, (Stop)y);
            if (x is SetValue) return EqualsSetValue((SetValue)x, (SetValue)y);

            return false;
        }

        private bool EqualsNewInt(NewInt x, NewInt y)
        {
            return x.Name == y.Name;
        }

        private bool EqualsCloneValue(CloneValue x, CloneValue y)
        {
            return x.SourceName == y.SourceName && x.TargetName == y.TargetName;
        }

        private bool EqualsCondition(Condition x, Condition y)
        {
            return x.ConditionName == y.ConditionName;
        }

        private bool EqualsGetRandom(GetRandom x, GetRandom y)
        {
            return x.TargetName == y.TargetName && x.MaxValueName == y.MaxValueName;
        }

        private bool EqualsGetState(GetState x, GetState y)
        {
            return x.Direction == y.Direction && x.TargetName == y.TargetName;
        }

        private bool EqualsMinus(Minus x, Minus y)
        {
            return x.FirstSource == y.FirstSource && x.SecondSource == y.SecondSource && x.TargetName == y.TargetName;
        }

        private bool EqualsPlus(Plus x, Plus y)
        {
            return x.FirstSource == y.FirstSource && x.SecondSource == y.SecondSource && x.TargetName == y.TargetName;
        }

        private bool EqualsPrint(Print x, Print y)
        {
            return x.Variable == y.Variable;
        }

        private bool EqualsSetValue(SetValue x, SetValue y)
        {
            return x.TargetName == y.TargetName && x.Value == y.Value;
        }

        private bool EqualsStop(Stop x, Stop y)
        {
            return true;
        }

        private bool EqualsCloseCondition(CloseCondition x, CloseCondition y)
        {
            return true;
        }

        public int GetHashCode(ICommand obj)
        {
            return obj.GetHashCode();
        }
    }
}