﻿using BinarySerialization;

namespace XBee.Frames
{
    public class RxIndicatorFrame : FrameContent, IRxIndicatorDataFrame
    {
        [FieldOrder(0)]
        public LongAddress Source { get; set; }

        [FieldOrder(1)]
        public ReceivedSignalStrengthIndicator ReceivedSignalStrengthIndicator { get; set; }

        [FieldOrder(2)]
        public ReceiveOptions Options { get; set; }

        public NodeAddress GetAddress()
        {
            return new NodeAddress(Source);
        }

        [FieldOrder(3)]
        public byte[] Data { get; set; }
    }
}
