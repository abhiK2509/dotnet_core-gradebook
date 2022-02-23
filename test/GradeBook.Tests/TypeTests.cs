using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTests
    {
        int count = 0;
        [Fact]
        public void WriteLogDelegateCanPointToMethod1()
        {
            WriteLogDelegate log = ReturnMessage1;
            log += ReturnMessage1;
            log += IncrementCount;

            var res = log("Hello!");
            Assert.Equal(3, count);
        }

        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }
        string ReturnMessage1(string message)
        {
            count++;
            return message;
        }

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log;
            log = ReturnMessage;

            var res = log("Hello!");
            Assert.Equal("Hello!", res);
        }

        string ReturnMessage(string message)
        {
            return message;
        }

        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "Abhijeet";
            var upper = MakeUppercase(name);
            Assert.Equal("ABHIJEET", upper);
        }
        private string MakeUppercase(string parameter)
        {
            return parameter.ToUpper();
        }

        //=============================================================================
        [Fact]
        public void ValueTypeAlsoPassByValue()
        {
            var x = GetInt();
            SetInt(ref x);
            Assert.Equal(42, x);
        }
        private int GetInt()
        {
            return 3;
        }
        private void SetInt(ref int z)
        {
            z = 42;
        }

        //=============================================================================
        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Grade Book 1");
            GetBookSetName(ref book1, "New Grade Book 1");
            Assert.Equal("New Grade Book 1", book1.Name);
        }

        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        //=============================================================================
        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Grade Book 1");
            GetBookSetName(book1, "New Grade Book 1");
            Assert.Equal("Grade Book 1", book1.Name);
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        //=============================================================================
        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Grade Book 1");
            SetName(book1, "New Grade Book 1");
            Assert.Equal("New Grade Book 1", book1.Name);
        }
        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        //=============================================================================
        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Grade Book 1");
            var book2 = GetBook("Grade Book 2");
            Assert.Equal("Grade Book 1", book1.Name);
            Assert.Equal("Grade Book 2", book2.Name);
            Assert.NotSame(book1, book2);
            Assert.False(Object.ReferenceEquals(book1, book2));
        }

        //=============================================================================
        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var book1 = GetBook("Grade Book 1");
            var book2 = book1;
            Assert.Equal("Grade Book 1", book1.Name);
            Assert.Equal("Grade Book 1", book2.Name);
            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}