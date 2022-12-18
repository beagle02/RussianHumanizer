using System.Globalization;
using FluentAssertions;
using Humanizer;
using RussianHumanizer.Extensions;

namespace RussianHumanizerTests
{
    public class DecimalExtensionsTests
    {
        [TestCase(0, 1, 1, 0, 0, "0 рублей 0 копеек")]
        [TestCase(1, 1, 1, 0, 0, "1 рубль 0 копеек")]
        [TestCase(2, 1, 1, 0, 0, "2 рубля 0 копеек")]
        [TestCase(3, 1, 1, 0, 0, "3 рубля 0 копеек")]
        [TestCase(4, 1, 1, 0, 0, "4 рубля 0 копеек")]
        [TestCase(5, 1, 1, 0, 0, "5 рублей 0 копеек")]
        [TestCase(6, 1, 1, 0, 0, "6 рублей 0 копеек")]
        [TestCase(7, 1, 1, 0, 0, "7 рублей 0 копеек")]
        [TestCase(8, 1, 1, 0, 0, "8 рублей 0 копеек")]
        [TestCase(9, 1, 1, 0, 0, "9 рублей 0 копеек")]
        [TestCase(10, 1, 1, 0, 0, "10 рублей 0 копеек")]
        [TestCase(11, 1, 1, 0, 0, "11 рублей 0 копеек")]
        [TestCase(12, 1, 1, 0, 0, "12 рублей 0 копеек")]
        [TestCase(13, 1, 1, 0, 0, "13 рублей 0 копеек")]
        [TestCase(14, 1, 1, 0, 0, "14 рублей 0 копеек")]
        [TestCase(20, 1, 1, 0, 0, "20 рублей 0 копеек")]
        [TestCase(21, 1, 1, 0, 0, "21 рубль 0 копеек")]
        [TestCase(22, 1, 1, 0, 0, "22 рубля 0 копеек")]
        [TestCase(23, 1, 1, 0, 0, "23 рубля 0 копеек")]
        [TestCase(24, 1, 1, 0, 0, "24 рубля 0 копеек")]
        [TestCase(25, 1, 1, 0, 0, "25 рублей 0 копеек")]
        [TestCase(1125, 2, 1, 0, 0, "одна тысяча сто двадцать пять рублей 0 копеек")]
        [TestCase(1125.37, 2, 2, 0, 0, "одна тысяча сто двадцать пять рублей тридцать семь копеек")]
        [TestCase(25.1, 1, 1, 1, 1, "25 руб. 10 коп.")]
        public void ToRussianMoney_Ok(decimal value, ShowQuantityAs rubleShowAs,
            ShowQuantityAs pennyQuantityAs, WordForm rubleWordForm, WordForm pennyWordForm, string expected)
        {
            // Arrange

            // Act
            var actual = value.ToRussianMoney(rubleShowAs, pennyQuantityAs, rubleWordForm, pennyWordForm);

            // Assert
            actual.Should().Be(expected);
        }

        [TestCase(1357468.02, "1,357,468 рублей 02 копейки")]
        public void ToRussianMoney_Format_Ok(decimal value, string expected)
        {
            // Arrange

            // Act
            var actual = value.ToRussianMoney(rubleFormat:"#,#", pennyFormat:"0#", formatProvider:CultureInfo.InvariantCulture);

            // Assert
            actual.Should().Be(expected);
        }
    }
}