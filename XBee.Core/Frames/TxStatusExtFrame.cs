﻿using BinarySerialization;
using JetBrains.Annotations;

namespace XBee.Frames
{
    internal class TxStatusExtFrame : CommandResponseFrameContent
    {
        public TxStatusExtFrame()
        {
            ShortAddress = ShortAddress.Broadcast;
        }

        [FieldOrder(0)]
        public ShortAddress ShortAddress { get; set; }

        [FieldOrder(1)] [UsedImplicitly] public byte RetryCount { get; set; }

        [FieldOrder(2)]
        public DeliveryStatusExt DeliveryStatus { get; set; }

        [FieldOrder(3)] [UsedImplicitly] public DiscoveryStatus DiscoveryStatus { get; set; }
    }
}
