using System.Globalization;
using FluentAssertions;
using Humanizer;
using RussianHumanizer.Extensions;

namespace RussianHumanizerTests
{
    public class DecimalExtensionsTests
    {
        [TestCase(0, 1, 1, 0, 0, "0 ������ 0 ������")]
        [TestCase(1, 1, 1, 0, 0, "1 ����� 0 ������")]
        [TestCase(2, 1, 1, 0, 0, "2 ����� 0 ������")]
        [TestCase(3, 1, 1, 0, 0, "3 ����� 0 ������")]
        [TestCase(4, 1, 1, 0, 0, "4 ����� 0 ������")]
        [TestCase(5, 1, 1, 0, 0, "5 ������ 0 ������")]
        [TestCase(6, 1, 1, 0, 0, "6 ������ 0 ������")]
        [TestCase(7, 1, 1, 0, 0, "7 ������ 0 ������")]
        [TestCase(8, 1, 1, 0, 0, "8 ������ 0 ������")]
        [TestCase(9, 1, 1, 0, 0, "9 ������ 0 ������")]
        [TestCase(10, 1, 1, 0, 0, "10 ������ 0 ������")]
        [TestCase(11, 1, 1, 0, 0, "11 ������ 0 ������")]
        [TestCase(12, 1, 1, 0, 0, "12 ������ 0 ������")]
        [TestCase(13, 1, 1, 0, 0, "13 ������ 0 ������")]
        [TestCase(14, 1, 1, 0, 0, "14 ������ 0 ������")]
        [TestCase(20, 1, 1, 0, 0, "20 ������ 0 ������")]
        [TestCase(21, 1, 1, 0, 0, "21 ����� 0 ������")]
        [TestCase(22, 1, 1, 0, 0, "22 ����� 0 ������")]
        [TestCase(23, 1, 1, 0, 0, "23 ����� 0 ������")]
        [TestCase(24, 1, 1, 0, 0, "24 ����� 0 ������")]
        [TestCase(25, 1, 1, 0, 0, "25 ������ 0 ������")]
        [TestCase(1125, 2, 1, 0, 0, "���� ������ ��� �������� ���� ������ 0 ������")]
        [TestCase(1125.37, 2, 2, 0, 0, "���� ������ ��� �������� ���� ������ �������� ���� ������")]
        [TestCase(25.1, 1, 1, 1, 1, "25 ���. 10 ���.")]
        public void ToRussianMoney_Ok(decimal value, ShowQuantityAs rubleShowAs,
            ShowQuantityAs pennyQuantityAs, WordForm rubleWordForm, WordForm pennyWordForm, string expected)
        {
            // Arrange

            // Act
            var actual = value.ToRussianMoney(rubleShowAs, pennyQuantityAs, rubleWordForm, pennyWordForm);

            // Assert
            actual.Should().Be(expected);
        }

        [TestCase(1357468.02, "1,357,468 ������ 02 �������")]
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