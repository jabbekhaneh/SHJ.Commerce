﻿namespace SHJ.Commerce.Domain.Aggregates.Dynamic;

public class LogHistory : BaseEntity
{
    public LogHistory()
    {
        
    }
    public LogHistoryType Type { get; set; }
    public virtual string? IP { get; set; } = string.Empty;
    public virtual string? OS { get; set; } = String.Empty;
}
public enum LogHistoryType : int
{

}