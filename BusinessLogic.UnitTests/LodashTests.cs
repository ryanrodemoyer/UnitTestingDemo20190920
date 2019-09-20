using NUnit.Framework;
using System;
using System.Linq;

namespace BusinessLogic.UnitTests
{
    /*
     * Three basic patterns to write multiple tests for the same code/method.
     * 1. One method per set of inputs. 
     * 2. One method that tests multiple inputs and asserts multiple inputs.
     * 3. One (or a few more) parameterized tests that test a range of inputs.
     */

    [TestFixture]
    public class LodashTests
    {
        [Test]
        public void intersect__two_item_and_three_item__is_successful()
        {
            // arrange
            int[] a = {1, 2, 3};
            int[] b = {1, 3};

            // act
            var intersection = _.Intersect(a, b);

            // assert
            CollectionAssert.AreEqual(new [] {1, 3}, intersection);
        }

        [Test]
        public void intersect__with_duplicates__nine_item_and_five_item__is_successful()
        {
            // arrange
            int[] a = {1, 2, 3, 4, -1, 4, 2, 1, 5};
            int[] b = {1, 3, 5, 1, 9};

            // act
            var intersection = _.Intersect(a, b);

            // assert
            foreach (var i in intersection)
            {
                Console.WriteLine(i);
            }

            Assert.AreEqual(3, intersection.Length);

            // what could be "wrong" with this way of asserting the items in the collection?
            Assert.IsTrue(intersection.Contains(1));
            Assert.IsTrue(intersection.Contains(3));
            Assert.IsTrue(intersection.Contains(5));
        }

        [TestCase("1,2,3", "1,3", "1,3")]
        [TestCase("1,2,3,4,-1,4,2,1,5", "1,3,5,1,9", "1,3,5")]
        public void intersect_integer_tests(string inputA, string inputB, string inputExpected)
        {
            // arrange
            int[] a = inputA.Split(',').Select(i => Convert.ToInt32(i)).ToArray();
            int[] b = inputB.Split(',').Select(i => Convert.ToInt32(i)).ToArray();
            int[] expected = inputExpected.Split(',').Select(i => Convert.ToInt32(i)).ToArray();

            // act
            var intersection = _.Intersect(a, b);

            // assert
            //Assert.AreEqual(expected.Length, intersection.Length);
            CollectionAssert.AreEqual(expected, intersection);
        }

        [Test]
        public void tail__three_items__is_successful()
        {
            // arrange
            int[] items = {5, 10, 100, 15, 25};

            // act
            var tail = _.Tail(items);

            // assert
            Assert.AreEqual(4, tail.Length);
            Assert.AreEqual(10, tail[0]);
            Assert.AreEqual(100, tail[1]);
            Assert.AreEqual(15, tail[2]);
            Assert.AreEqual(25, tail[3]);
        }
    }
}
