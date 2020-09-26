﻿using System;
using System.Text;

namespace FlatFiles.TypeMapping
{
    /// <summary>
    /// Represents the mapping from a type property to a byte array column.
    /// </summary>
    public interface IByteArrayPropertyMapping
    {
        /// <summary>
        /// Sets the name of the column in the input or output file.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <returns>The property mapping for further configuration.</returns>
        IByteArrayPropertyMapping ColumnName(string name);

        /// <summary>
        /// Sets the encoding to use to read and write the column.
        /// </summary>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>The property mapping for further configuration.</returns>
        IByteArrayPropertyMapping Encoding(Encoding encoding);

        /// <summary>
        /// Sets what value(s) are treated as null.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>The property mapping for further configuration.</returns>
        /// <remarks>Passing null will cause the default formatter to be used.</remarks>
        IByteArrayPropertyMapping NullFormatter(INullFormatter formatter);

        /// <summary>
        /// Sets the default value to use when a null is encountered on a non-null property.
        /// </summary>
        /// <param name="defaultValue">The default value to use.</param>
        /// <returns>The property mapping for further configuration.</returns>
        /// <remarks>Passing null will cause an exception to be thrown for unexpected nulls.</remarks>
        IByteArrayPropertyMapping DefaultValue(IDefaultValue defaultValue);

        /// <summary>
        /// Sets a function to preprocess in the input before parsing it.
        /// </summary>
        /// <param name="preprocessor">A preprocessor function.</param>
        /// <returns>The property mapping for further configuration.</returns>
        [Obsolete("This function has been superseded by the OnParsing function.")]
        IByteArrayPropertyMapping Preprocessor(Func<string, string> preprocessor);

        /// <summary>
        /// Sets the function to run before the input is parsed.
        /// </summary>
        /// <param name="handler">A function to call before the textual value is parsed.</param>
        /// <returns>The property mapping for further configuration.</returns>
        IByteArrayPropertyMapping OnParsing(Func<IColumnContext, String, String> handler);

        /// <summary>
        /// Sets the function to run after the input is parsed.
        /// </summary>
        /// <param name="handler">A function to call after the value is parsed.</param>
        /// <returns>The property mapping for further configuration.</returns>
        IByteArrayPropertyMapping OnParsed(Func<IColumnContext, object, object> handler);

        /// <summary>
        /// Sets the function to run before the output is formatted as a string.
        /// </summary>1
        /// <param name="handler">A function to call before the value is formatted as a string.</param>
        /// <returns>The property mapping for further configuration.</returns>
        IByteArrayPropertyMapping OnFormatting(Func<IColumnContext, object, object> handler);

        /// <summary>
        /// Sets the function to run after the output is formatted as a string.
        /// </summary>
        /// <param name="handler">A function to call after the value is formatted as a string.</param>
        /// <returns>The property mapping for further configuration.</returns>
        IByteArrayPropertyMapping OnFormatted(Func<IColumnContext, string, string> handler);
    }

    internal sealed class ByteArrayPropertyMapping : IByteArrayPropertyMapping, IMemberMapping
    {
        private readonly ByteArrayColumn column;

        public ByteArrayPropertyMapping(ByteArrayColumn column, IMemberAccessor member, int physicalIndex, int logicalIndex)
        {
            this.column = column;
            Member = member;
            PhysicalIndex = physicalIndex;
            LogicalIndex = logicalIndex;
        }

        public IByteArrayPropertyMapping ColumnName(string name)
        {
            column.ColumnName = name;
            return this;
        }

        public IByteArrayPropertyMapping Encoding(Encoding encoding)
        {
            column.Encoding = encoding;
            return this;
        }

        public IByteArrayPropertyMapping NullFormatter(INullFormatter formatter)
        {
            column.NullFormatter = formatter;
            return this;
        }

        public IByteArrayPropertyMapping DefaultValue(IDefaultValue defaultValue)
        {
            column.DefaultValue = defaultValue;
            return this;
        }

        public IByteArrayPropertyMapping Preprocessor(Func<string, string> preprocessor)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            column.Preprocessor = preprocessor;
#pragma warning restore CS0618 // Type or member is obsolete
            return this;
        }

        public IByteArrayPropertyMapping OnParsing(Func<IColumnContext, String, String> handler)
        {
            column.OnParsing = handler;
            return this;
        }

        public IByteArrayPropertyMapping OnParsed(Func<IColumnContext, object, object> handler)
        {
            column.OnParsed = handler;
            return this;
        }

        public IByteArrayPropertyMapping OnFormatting(Func<IColumnContext, object, object> handler)
        {
            column.OnFormatting = handler;
            return this;
        }

        public IByteArrayPropertyMapping OnFormatted(Func<IColumnContext, string, string> handler)
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
