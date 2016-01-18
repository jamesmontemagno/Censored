using NUnit.Framework;
using System;
namespace Censored.Tests
{
    [TestFixture]
    public class AllTests
    {
        [Test]
        public void HasDirtyWords()
        {
            var censor = new Censor(new []{"helloworld"});

            var test = "The dirty word helloworld";
            var correct = "The dirty word **********";

            var result = censor.CensorText(test);

            Assert.IsTrue(censor.HasCensoredWord(test), "The text is dirty, but returned false");
            Assert.AreEqual(result, correct, "All words were not censored correctly");
        }

        [Test]
        public void NoDirtyWords()
        {
            var censor = new Censor(new []{"helloworld"});
            var test = "The dirty word is not here";
            var correct = "The dirty word is not here";

            var result = censor.CensorText(test);

            Assert.IsFalse(censor.HasCensoredWord(test), "The text is not dirty, but returned true");
            Assert.AreEqual(result, correct, "All words were not censored correctly");
        }

        [Test]
        public void MultipleDirtyWords()
        {
            var test = "The dirty word hello and world";
            var censor = new Censor(new []{"hello", "world"});
            var correct = "The dirty word ***** and *****";

            var result = censor.CensorText(test);

            Assert.IsTrue(censor.HasCensoredWord(test), "The text is dirty, but returned false");
            Assert.AreEqual(result, correct, "All words were not filtered");

        }

        [Test]
        public void CasingDirtyWords()
        {
            var test = "The dirty word helloworld and HelloWorld";
            var censor = new Censor(new []{"helloworld"});
            var correct = "The dirty word ********** and **********";

            var result = censor.CensorText(test);

            Assert.IsTrue(censor.HasCensoredWord(test), "The text is dirty, but returned false");
            Assert.AreEqual(result, correct, "All words were not filtered");

        }

        [Test]
        public void DirtyWordsWithSpaces()
        {
            var test = "The dirty word hello world and Hello World";
            var censor = new Censor(new []{"hello world"});
            var correct = "The dirty word *********** and ***********";

            var result = censor.CensorText(test);

            Assert.IsTrue(censor.HasCensoredWord(test), "The text is dirty, but returned false");
            Assert.AreEqual(result, correct, "All words were not filtered");

        }

        [Test]//Original from post
        public void WildcardDirty()
        {
            var censor = new Censor(new []
                {
                    "gosh",
                    "drat",
                    "darn*",
                });

            string result;

            result = censor.CensorText("I stubbed my toe. Gosh it hurts!");
            Assert.AreEqual(result, "I stubbed my toe. **** it hurts!");

            result = censor.CensorText("The midrate on the USD -> EUR forex trade has soured my day. Drat!");
            Assert.AreEqual(result, "The midrate on the USD -> EUR forex trade has soured my day. ****!");

            result = censor.CensorText("Gosh darnit, my shoe laces are undone.");
            Assert.AreEqual(result, "**** ******, my shoe laces are undone.");
        }


    }
}

