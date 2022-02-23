using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        // [Fact] is the attribute
        [Fact]
        public void BookCalculateAnAverageGrade()
        {
            /*
                Entire Test is basically categrorized into 3 sections
                - arrange
                - act
                - assert
            */
            var book = new InMemoryBook("Grade Book");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.5);
            var result = book.GetStatistics();
            Assert.Equal(85.7, result.Average, 1);
            Assert.Equal(90.5, result.High, 1);
            Assert.Equal(77.5, result.Low, 1);
            Assert.Equal('B', result.Letter);
        }
    }
}