using System.Runtime.Serialization;

namespace GAAPICommon.Architecture
{
    [DataContract]
    public enum ServiceCode
    {
        [EnumMember]
        NoError = 0,

        [EnumMember]
        UnknownFailure = 1,

        [EnumMember]
        UnknownException = 2,

        [EnumMember]
        ServiceNotConfigured = 3,

        [EnumMember]
        ClientException = 4
    };

	[DataContract]
	public enum AgvMode
	{
		[EnumMember]
		Automatic = 1,

		[EnumMember]
		Manual = 0
	}

	[DataContract]
	public enum ExtendedDataFaultStatus : byte
	{
		[EnumMember]
		OK = 0,

		[EnumMember]
		Fault = 1
	}

	[DataContract]
	public enum MovementType
	{
		[EnumMember]
		Stationary = 0,

		[EnumMember]
		ClothoidForwards = 1,

		[EnumMember]
		ClothoidBackwards = 2,

		[EnumMember]
		ACRIP = 3, // Anti-clockwise rotate in place

		[EnumMember]
		CRIP = 4, // Clockwise rotate in place

		[EnumMember]
		StrafeLinear = 5, // WSAD

		[EnumMember]
		StrafeClothoidForwards = 6,

		[EnumMember]
		StrafeClothoidBackwards = 7,
	}

	[DataContract]
	public enum FrozenState
	{
		[EnumMember]
		Frozen = 0,

		[EnumMember]
		Unfrozen = 1
	}

	[DataContract]
	public enum NavigationStatus : ushort
	{
		[EnumMember]
		OK = 0,

		[EnumMember]
		Lost = 1,

		[EnumMember]
		AssociationFailure = 2,

		[EnumMember]
		HighUncertainty = 3,

		[EnumMember]
		PoorAssociaton = 4,

		[EnumMember]
		NoResponse = 5,

		[EnumMember]
		Unknown = 65535
	}

	[DataContract]
	public enum PositionControlStatus : ushort
	{
		[EnumMember]
		OK = 0,

		[EnumMember]
		Disabled = 1,

		[EnumMember]
		Disabling = 2,

		[EnumMember]
		NoWaypoints = 3,

		[EnumMember]
		OutOfPosition = 4,

		[EnumMember]
		WaypointDiscontinuity = 5,

		[EnumMember]
		Unknown = 65535
	}


	[DataContract]
	public enum DynamicLimiterStatus : ushort
	{
		[EnumMember]
		OK = 0,

		[EnumMember]
		SafetySensor = 1,

		[EnumMember]
		Warning_1 = 2,

		[EnumMember]
		Warning_2 = 3,

		[EnumMember]
		MotorFault = 4,

		[EnumMember]
		FastStop = 5,

		[EnumMember]
		GoSlow = 6,

		[EnumMember]
		Unknown = 65535
	}
}
