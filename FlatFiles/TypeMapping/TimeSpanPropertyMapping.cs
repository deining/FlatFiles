﻿using System;

namespace FlatFiles.TypeMapping
{
    /// <summary>
    /// Represents the mapping from a type property to a TimeSpan column.
    /// </summary>
    public interface ITimeSpanPropertyMapping
    {
        /// <summary>
        /// Sets the name of the column in the input or output file.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <returns>The property mapping for further configuration.</returns>
        ITimeSpanPropertyMapping ColumnName(string name);

        /// <summary>
        /// Sets the date/time format the input is expected to be in.
        /// </summary>
        /// <param name="format">The format to expect.</param>
        /// <returns>The property mapping for further configuration.</returns>
        ITimeSpanPropertyMapping InputFormat(string format);

        /// <summary>
        /// Sets the date/time format to use for output.
        /// </summary>
        /// <param name="format">The format to use.</param>
        /// <returns>The property mapping for further configuration.</returns>
        ITimeSpanPropertyMapping OutputFormat(string format);

        /// <summary>
        /// Sets the format provider to use when reading and writing date/times.
        /// </summary>
        /// <param name="provider">The provider to use.</param>
        /// <returns>The property mapping for further configuration.</returns>
        ITimeSpanPropertyMapping FormatProvider(IFormatProvider provider);

        /// <summary>
        /// Sets what value(s) are treated as null.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>The property mapping for further configuration.</returns>
        /// <remarks>Passing null will cause the default formatter to be used.</remarks>
        ITimeSpanPropertyMapping NullFormatter(INullFormatter formatter);

        /// <summary>
        /// Sets the default value to use when a null is encountered on a non-null property.
        /// </summary>
        /// <param name="defaultValue">The default value to use.</param>
        /// <returns>The property mapping for further configuration.</returns>
        /// <remarks>Passing null will cause an exception to be thrown for unexpected nulls.</remarks>
        ITimeSpanPropertyMapping DefaultValue(IDefaultValue defaultValue);

        /// <summary>
        /// Sets a function to preprocess in the input before parsing it.
        /// </summary>
        /// <param name="preprocessor">A preprocessor function.</param>
        /// <returns>The property mapping for further configuration.</returns>
        [Obsolete("This function has been superseded by the OnParsing function.")]
        ITimeSpanPropertyMapping Preprocessor(Func<string, string> preprocessor);

        /// <summary>
        /// Sets the function to run before the input is parsed.
        /// </summary>
        /// <param name="handler">A function to call before the textual value is parsed.</param>
        /// <returns>The property mapping for further configuration.</returns>
        ITimeSpanPropertyMapping OnParsing(Func<IColumnContext, String, String> handler);

        /// <summary>
        /// Sets the function to run after the input is parsed.
        /// </summary>
        /// <param name="handler">A function to call after the value is parsed.</param>
        /// <returns>The property mapping for further configuration.</returns>
        ITimeSpanPropertyMapping OnParsed(Func<IColumnContext, object, object> handler);

        /// <summary>
        /// Sets the function to run before the output is formatted as a string.
        /// </summary>1
        /// <param name="handler">A function to call before the value is formatted as a string.</param>
        /// <returns>The property mapping for further configuration.</returns>
        ITimeSpanPropertyMapping OnFormatting(Func<IColumnContext, object, object> handler);

        /// <summary>
        /// Sets the function to run after the output is formatted as a string.
        /// </summary>
        /// <param name="handler">A function to call after the value is formatted as a string.</param>
        /// <returns>The property mapping for further configuration.</returns>
        ITimeSpanPropertyMapping OnFormatted(Func<IColumnContext, string, string> handler);
    }

    internal sealed class TimeSpanPropertyMapping : ITimeSpanPropertyMapping, IMemberMapping
    {
        private readonly TimeSpanColumn column;

        public TimeSpanPropertyMapping(TimeSpanColumn column, IMemberAccessor member, int physicalIndex, int logicalIndex)
        {
            this.column = column;
            Member = member;
            PhysicalIndex = physicalIndex;
            LogicalIndex = logicalIndex;
        }

        public ITimeSpanPropertyMapping ColumnName(string name)
        {
            column.ColumnName = name;
            return this;
        }

        public ITimeSpanPropertyMapping InputFormat(string format)
        {
            column.InputFormat = format;
            return this;
        }

        public ITimeSpanPropertyMapping OutputFormat(string format)
        {
            column.OutputFormat = format;
            return this;
        }

        public ITimeSpanPropertyMapping FormatProvider(IFormatProvider provider)
        {
            column.FormatProvider = provider;
            return this;
        }

        public ITimeSpanPropertyMapping NullFormatter(INullFormatter formatter)
        {
            column.NullFormatter = formatter;
            return this;
        }

        public ITimeSpanPropertyMapping DefaultValue(IDefaultValue defaultValue)
        {
            column.DefaultValue = defaultValue;
            return this;
        }

        public ITimeSpanPropertyMapping Preprocessor(Func<string, string> preprocessor)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            column.Preprocessor = preprocessor;
#pragma warning restore CS0618 // Type or member is obsolete
            return this;
        }

        public ITimeSpanPropertyMapping OnParsing(Func<IColumnContext, String, String> handler)
        {
            column.OnParsing = handler;
            return this;
        }

        public ITimeSpanPropertyMapping OnParsed(Func<IColumnContext, object, object> handler)
        {
            column.OnParsed = handler;
            return this;
        }

        public ITimeSpanPropertyMapping OnFormatting(Func<IColumnContext, object, object> handler)
        {
            column.OnFormatting = handler;
            return this;
        }

        public ITimeSpanPropertyMapping OnFormatted(Func<IColumnContext, string, string> handler)
        {
            column.OnFormatted = handler;
            return this;
        }

        public IMemberAccessor Member { get; }

        public Action<IColumnContext, object, object> Reader => null;

        public Action<IColumnContext, object, object[]> Writer => null;

        public IColumnDefinition ColumnDefinition => column;

        public int PhysicalIndex { get; }

        public int LogicalIndex { get; }
    }
}
