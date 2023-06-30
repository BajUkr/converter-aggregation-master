using System;
using System.Globalization;
using System.Text;

namespace ConverterDictionaryAggregation
{
    /// <summary>
    /// Converts a real number to string.
    /// </summary>
    public class Converter
    {
        private readonly CharsDictionary charsDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="Converter"/> class.
        /// </summary>
        /// <param name="charsDictionary">The dictionary with rules of converting.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when dictionary is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when charsDictionary.Dictionary is empty.</exception>
        public Converter(CharsDictionary? charsDictionary)
        {
            this.charsDictionary = charsDictionary ?? throw new ArgumentNullException(nameof(charsDictionary));

            if (charsDictionary.Dictionary == null || charsDictionary.Dictionary.Count == 0)
            {
                throw new ArgumentException("Dictionary cannot be empty.", nameof(charsDictionary));
            }
        }

        /// <summary>
        /// Converts double number into string.
        /// </summary>
        /// <param name="number">Double number to convert.</param>
        /// <returns>A number string representation.</returns>
        public string Convert(double number)
        {
            if (double.IsNaN(number))
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                return this.charsDictionary.Dictionary[Character.NaN];
            }

            if (double.IsPositiveInfinity(number))
            {
                return this.charsDictionary.Dictionary[Character.PositiveInfinity];
            }

            if (double.IsNegativeInfinity(number))
            {
                return this.charsDictionary.Dictionary[Character.NegativeInfinity];
            }

            if (number == double.Epsilon)
            {
                return this.charsDictionary.Dictionary[Character.Epsilon];
            }

            CultureInfo culture = this.charsDictionary.CultureName != null ? new CultureInfo(this.charsDictionary.CultureName) : CultureInfo.InvariantCulture;
            var numberString = number.ToString(culture);
            var stringBuilder = new StringBuilder();

            foreach (var ch in numberString)
            {
                if (this.charsDictionary.Dictionary.TryGetValue(GetCharacter(ch), out var word))
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append(' ');
                    }

                    stringBuilder.Append(word);
                }
                else
                {
                    throw new InvalidOperationException($"Character '{ch}' is not supported by the provided CharsDictionary.");
                }
            }

            return stringBuilder.ToString();
        }

        private static Character GetCharacter(char ch)
        {
            return ch switch
            {
                '0' => Character.Zero,
                '1' => Character.One,
                '2' => Character.Two,
                '3' => Character.Three,
                '4' => Character.Four,
                '5' => Character.Five,
                '6' => Character.Six,
                '7' => Character.Seven,
                '8' => Character.Eight,
                '9' => Character.Nine,
                '+' => Character.Plus,
                '-' => Character.Minus,
                '.' => Character.Point,
                ',' => Character.Comma,
                'E' => Character.Exponent,
                'e' => Character.Exponent,
                _ => throw new ArgumentException($"Unsupported character '{ch}'.")
            };
        }
    }
}
