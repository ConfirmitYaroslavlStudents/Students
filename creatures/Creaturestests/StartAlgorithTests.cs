using CellsAutomate;
using Creatures.Language.Executors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creaturestests
{
    public class TestExecutorToolset : IExecutorToolset
    {
        Tuple<int, int>[] _randoms;
        IDictionary<int, int> _state;
        int _randomCounter = 0;

        public TestExecutorToolset(Tuple<int, int>[] randoms, IDictionary<int, int> state)
        {
            _randoms = randoms;
            _state = state;
        }

        public int GetRandom(int maxValue)
        {
            Assert.AreEqual(maxValue, _randoms[_randomCounter].Item1);
            var result =  _randoms[_randomCounter].Item2;
            _randomCounter++;
            return result;
        }

        public int GetState(int direction)
        {
            return _state[direction];
        }
    }

    [TestClass]
    public class StartAlgorithTests : TestsBase
    {
        [TestMethod]
        public void AllCellsAreOpen()
        {
            var state = new Dictionary<int, int>() {
                { 0, 4 },
                { 1, 4 },
                { 2, 4 },
                { 3, 4 } };

            foreach (var i in new []{1, 2, 3, 4})
            {
                var randomChoice = new[] { Tuple.Create(4, i) };

                Check(GetAlgorithm().ToString(), new TestExecutorToolset(randomChoice, state), i);
            }
        }

        [TestMethod]
        public void AllCellsAreClosed()
        {
            var state = new Dictionary<int, int>() {
                { 0, 3 },
                { 1, 3 },
                { 2, 3 },
                { 3, 3 } };

            var randomChoice = new Tuple<int, int> [] { };

            Check(GetAlgorithm().ToString(), new TestExecutorToolset(randomChoice, state), 0);
        }

        [TestMethod]
        public void OneCellIsOpen()
        {
            foreach (var i in new[] { 1, 2, 3, 4 })
            {
                var state = new[] { 0, 1, 2, 3 }.ToDictionary(x => x, x => x == (i - 1) ? 4 : 3);

                var randomChoice = new[] { Tuple.Create(1, 1) };

                Check(GetAlgorithm().ToString(), new TestExecutorToolset(randomChoice, state), i);
            }
        }

        private string GetAlgorithm()
        {
            return new SeedGenerator().GetAlgorithm();
        }
    }
}
