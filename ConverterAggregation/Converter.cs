using System;

namespace ConverterDictionaryAggregation
{
    /// <summary>
    /// Converts a real number to string.
    /// </summary>
    public class Converter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Converter"/> class.
        /// </summary>
        /// <param name="charsDictionary">The dictionary with rules of converting.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when dictionary is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when charsDictionary.Dictionary is empty.</exception>
        public Converter(CharsDictionary? charsDictionary)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts double number into string.
        /// </summary>
        /// <param name="number">Double number to convert.</param>
        /// <returns>A number string representation.</returns>
        public string Convert(double number)
        {
            throw new NotImplementedException();
        }
    }
}
