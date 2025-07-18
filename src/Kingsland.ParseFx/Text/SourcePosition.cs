﻿namespace Kingsland.ParseFx.Text;

public sealed class SourcePosition
{

    #region Constructor

    public SourcePosition(int position, int lineNumber, int columnNumber)
    {
        this.Position = position;
        this.LineNumber = lineNumber;
        this.ColumnNumber = columnNumber;
    }

    #endregion

    #region Properties

    [PublicAPI]
    public int Position
    {
        get;
    }

    [PublicAPI]
    public int LineNumber
    {
        get;
    }

    [PublicAPI]
    public int ColumnNumber
    {
        get;
    }

    #endregion

    #region Methods

    [PublicAPI]
    public bool IsEqualTo(SourcePosition obj)
    {
        return object.ReferenceEquals(obj, this) ||
               ((obj is not null) &&
                (obj.Position == this.Position) &&
                (obj.LineNumber == this.LineNumber) &&
                (obj.ColumnNumber == this.ColumnNumber));
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        return $"Position={this.Position}," +
               $"LineNumber={this.LineNumber}," +
               $"ColumnNumber={this.ColumnNumber}";
    }

    #endregion

}
