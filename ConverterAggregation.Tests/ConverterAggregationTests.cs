using System;
using System.Collections.Generic;
using ConverterDictionaryAggregation;
using NUnit.Framework;

namespace ConverterCompositionAndAggregation.Tests.TransformerDictionaryAggregation
{
    [TestFixture]
    public class ConverterAggregationTests
    {
        private CharsDictionary englishDictionary;
        private CharsDictionary russianDictionary;
        private CharsDictionary germanDictionary;

        [OneTimeSetUp]
        public void SetUp() =>
            (this.germanDictionary, this.englishDictionary, this.russianDictionary) =
            (CreateGermanDictionary(), CreateEnglishDictionary(), CreateRussianDictionary());

        [TestCase(123.78, ExpectedResult = "один два три запятая семь восемь")]
        [TestCase(-12.78, ExpectedResult = "минус один два запятая семь восемь")]
        [TestCase(-0.78, ExpectedResult = "минус ноль запятая семь восемь")]
        [TestCase(double.PositiveInfinity, ExpectedResult = "положительная бесконечность")]
        [TestCase(double.NegativeInfinity, ExpectedResult = "отрицательная бесконечность")]
        [TestCase(double.NaN, ExpectedResult = "не число")]
        [TestCase(double.Epsilon, ExpectedResult = "эпсилон")]
        [TestCase(double.MinValue,
            ExpectedResult =
                "минус один запятая семь девять семь шесть девять три один три четыре восемь шесть два три один пять семь экспонента плюс три ноль восемь")]
        [TestCase(double.MaxValue,
            ExpectedResult =
                "один запятая семь девять семь шесть девять три один три четыре восемь шесть два три один пять семь экспонента плюс три ноль восемь")]
        [TestCase(6.67300E-11, ExpectedResult = "шесть запятая шесть семь три экспонента минус один один")]
        [TestCase(3.302e+23, ExpectedResult = "три запятая три ноль два экспонента плюс два три")]
        [TestCase(1234567890, ExpectedResult = "один два три четыре пять шесть семь восемь девять ноль")]
        public string Convert_Russian(double number)
        {
            var transformer = new Converter(this.russianDictionary);
            return transformer.Convert(number);
        }

        [TestCase(123.78, ExpectedResult = "one two three point seven eight")]
        [TestCase(-12.78, ExpectedResult = "minus one two point seven eight")]
        [TestCase(-0.78, ExpectedResult = "minus zero point seven eight")]
        [TestCase(double.PositiveInfinity, ExpectedResult = "positive infinity")]
        [TestCase(double.NegativeInfinity, ExpectedResult = "negative infinity")]
        [TestCase(double.NaN, ExpectedResult = "not a number")]
        [TestCase(double.Epsilon, ExpectedResult = "epsilon")]
        [TestCase(double.MinValue,
            ExpectedResult =
                "minus one point seven nine seven six nine three one three four eight six two three one five seven exponent plus three zero eight")]
        [TestCase(double.MaxValue,
            ExpectedResult =
                "one point seven nine seven six nine three one three four eight six two three one five seven exponent plus three zero eight")]
        [TestCase(6.67300E-11, ExpectedResult = "six point six seven three exponent minus one one")]
        [TestCase(3.302e+23, ExpectedResult = "three point three zero two exponent plus two three")]
        [TestCase(1234567890, ExpectedResult = "one two three four five six seven eight nine zero")]
        public string Convert_English(double number)
        {
            var transformer = new Converter(this.englishDictionary);
            return transformer.Convert(number);
        }

        [TestCase(123.78, ExpectedResult = "eins zwei drei komma sieben acht")]
        [TestCase(-12.78, ExpectedResult = "minus eins zwei komma sieben acht")]
        [TestCase(-0.78, ExpectedResult = "minus null komma sieben acht")]
        [TestCase(double.PositiveInfinity, ExpectedResult = "positive unendlichkeit")]
        [TestCase(double.NegativeInfinity, ExpectedResult = "negative unendlichkeit")]
        [TestCase(double.NaN, ExpectedResult = "keine zahl")]
        [TestCase(double.Epsilon, ExpectedResult = "epsilon")]
        [TestCase(double.MinValue,
            ExpectedResult =
                "minus eins komma sieben neun sieben sechs neun drei eins drei vier acht sechs zwei drei eins fünf sieben exponent plus drei null acht")]
        [TestCase(double.MaxValue,
            ExpectedResult =
                "eins komma sieben neun sieben sechs neun drei eins drei vier acht sechs zwei drei eins fünf sieben exponent plus drei null acht")]
        [TestCase(6.67300E-11, ExpectedResult = "sechs komma sechs sieben drei exponent minus eins eins")]
        [TestCase(3.302e+23, ExpectedResult = "drei komma drei null zwei exponent plus zwei drei")]
        [TestCase(1234567890, ExpectedResult = "eins zwei drei vier fünf sechs sieben acht neun null")]
        public string Convert_German(double number)
        {
            var transformer = new Converter(this.germanDictionary);
            return transformer.Convert(number);
        }

        [Test]
        public void Convert_DictionaryIsEmpty_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(
                () => new Converter(new CharsDictionary()
                {
                    Dictionary = new Dictionary<Character, string>(),
                    CultureName = null,
                }), "Dictionary cannot be empty.");
        }

        [Test]
        public void TransformToWords_DictionaryIsNull_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => new Converter(null), "Dictionary cannot be null.");
        }

        private static CharsDictionary CreateEnglishDictionary() => new()
        {
            Dictionary = new Dictionary<Character, string>
            {
                [Character.Zero] = "zero",
                [Character.One] = "one",
                [Character.Two] = "two",
                [Character.Three] = "three",
                [Character.Four] = "four",
                [Character.Five] = "five",
                [Character.Six] = "six",
                [Character.Seven] = "seven",
                [Character.Eight] = "eight",
                [Character.Nine] = "nine",
                [Character.Minus] = "minus",
                [Character.Plus] = "plus",
                [Character.Point] = "point",
                [Character.Comma] = "comma",
                [Character.Exponent] = "exponent",
                [Character.Epsilon] = "epsilon",
                [Character.NegativeInfinity] = "negative infinity",
                [Character.PositiveInfinity] = "positive infinity",
                [Character.NaN] = "not a number",
            },
            CultureName = "en-us",
        };

        private static CharsDictionary CreateRussianDictionary() => new()
        {
            Dictionary = new Dictionary<Character, string>
            {
                [Character.Zero] = "ноль",
                [Character.One] = "один",
                [Character.Two] = "два",
                [Character.Three] = "три",
                [Character.Four] = "четыре",
                [Character.Five] = "пять",
                [Character.Six] = "шесть",
                [Character.Seven] = "семь",
                [Character.Eight] = "восемь",
                [Character.Nine] = "девять",
                [Character.Minus] = "минус",
                [Character.Plus] = "плюс",
                [Character.Point] = "точка",
                [Character.Comma] = "запятая",
                [Character.Exponent] = "экспонента",
                [Character.Epsilon] = "эпсилон",
                [Character.NegativeInfinity] = "отрицательная бесконечность",
                [Character.PositiveInfinity] = "положительная бесконечность",
                [Character.NaN] = "не число",
            },
            CultureName = "ru-ru",
        };

        private static CharsDictionary CreateGermanDictionary() => new()
        {
            Dictionary = new Dictionary<Character, string>
            {
                [Character.Zero] = "null",
                [Character.One] = "eins",
                [Character.Two] = "zwei",
                [Character.Three] = "drei",
                [Character.Four] = "vier",
                [Character.Five] = "fünf",
                [Character.Six] = "sechs",
                [Character.Seven] = "sieben",
                [Character.Eight] = "acht",
                [Character.Nine] = "neun",
                [Character.Minus] = "minus",
                [Character.Plus] = "plus",
                [Character.Point] = "punkt",
                [Character.Comma] = "komma",
                [Character.Exponent] = "exponent",
                [Character.Epsilon] = "epsilon",
                [Character.NegativeInfinity] = "negative unendlichkeit",
                [Character.PositiveInfinity] = "positive unendlichkeit",
                [Character.NaN] = "keine zahl",
            },
            CultureName = "de-de",
        };
    }
}
