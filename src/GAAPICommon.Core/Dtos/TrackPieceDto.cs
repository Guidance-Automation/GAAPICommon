﻿using GAAPICommon.Architecture;

namespace GAAPICommon.Core.Dtos;

public class TrackPieceDto
{
    public int DeltaX { get; set; } = 0;

    public int DeltaY { get; set; } = 0;

    public double DeltaHeading { get; set; } = 0;

    public double SpeedLimit { get; set; } = 0;

    public MovementType MovementType { get; set; } = MovementType.Stationary;

}