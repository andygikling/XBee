using BinarySerialization;

namespace XBee.Frames.AtCommands
{
    internal class NetworkDiscoveryResponseData : AtCommandResponseFrameData
    {
        [FieldOrder(0)]
        public ShortAddress ShortAddress { get; set; }

        [FieldOrder(1)]
        public LongAddress LongAddress { get; set; }

        //Note sure when it happened but...
        //This works for XBee Pro (SB3) 900HP with Firmware version 8075 in DigiMesh mode.
        //Seems they have changed the response!  The signal strength is no longer found in this position!
        //Also, does the FieldOrder attribute number work correctly if this SerializeWhen decides the value should not be serialized?  Or is the FieldOrder off by 1?
        //Test this...
        //[FieldOrder(2)]
        [Ignore]
        //[SerializeWhen("Protocol", XBeeProtocol.Raw, RelativeSourceMode = RelativeSourceMode.SerializationContext)]
        public ReceivedSignalStrengthIndicator ReceivedSignalStrengthIndicator { get; set; }

        [FieldOrder(2)]
        public string Name { get; set; }

        [FieldOrder(3)]
        public NetworkDiscoveryResponseDataExtendedInfo ExtendedInfo { get; set; }

        [Ignore]
        public bool IsCoordinator
        {
            get
            {
                if (LongAddress.IsCoordinator)
                    return true;

                return ExtendedInfo != null && ExtendedInfo.DeviceType == DeviceType.Coordinator;
            }
        }

        public override string ToString()
        {
            return $"{Name}: {LongAddress}";
        }
    }
}
