using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Foundation.TinyGameplayFramework
{
    public class GameplayUnitsGroup<TUnit> : GameplayUnit, IEnumerable<TUnit>
        where TUnit : GameplayUnit
    {
        public IEnumerable<TUnit> Units => _units;

        private HashSet<TUnit> _units = new();

        public IEnumerator<TUnit> GetEnumerator()
        {
            return _units.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected void AddUnit(TUnit unit)
        {
            if (_units.Contains(unit))
            {
                LogError("Attempt to add a unit into a group that already contains it.");
                return;
            }

            unit.OnDestroyed += Unit_OnDestroyed;

            _units.Add(unit);
            HandleOnUnitAdded(unit);
        }

        protected void RemoveUnit(TUnit unit)
        {
            if (!_units.Contains(unit))
            {
                LogError("Attempt to remove a unit from a group that doesn't contain it.");
                return;
            }

            unit.OnDestroyed -= Unit_OnDestroyed;

            _units.Remove(unit);
            HandleOnUnitRemoved(unit);

            if (_units.Count < 1)
            {
                HandleOnLastUnitRemoved(unit);
            }
        }

        protected virtual void HandleOnUnitAdded(TUnit unit) { }
        protected virtual void HandleOnUnitRemoved(TUnit unit) { }
        protected virtual void HandleOnLastUnitRemoved(TUnit unit) { }

        private void LogError(string message)
        {
            Debug.LogError($"[{GetType().Name}] {message}");
        }

        private void Unit_OnDestroyed(object sender, EventArgs _)
        {
            RemoveUnit((TUnit)sender);
        }
    }
}