namespace GAAPICommon.Core.Dtos;

public class ComponentManagerStatusDto
{
    public string ComponentManagerState { get; set; }

    public bool IsFaulted { get; set; }

    public bool IsInEndState { get; set; }
}
