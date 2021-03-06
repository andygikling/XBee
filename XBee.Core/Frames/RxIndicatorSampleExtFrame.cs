﻿using System.Collections.Generic;
using System.Linq;
using BinarySerialization;
using JetBrains.Annotations;
using XBee.Converters;

namespace XBee.Frames
{
    internal class RxIndicatorSampleExtFrame : FrameContent, IRxIndicatorSampleFrame
    {
        private const AnalogSampleChannels AllAnalogSampleChannels =
            AnalogSampleChannels.Input0 |
            AnalogSampleChannels.Input1 |
            AnalogSampleChannels.Input2 |
            AnalogSampleChannels.Input3;

        [FieldOrder(0)]
        public LongAddress Source { get; set; }

        [FieldOrder(1)]
        public ShortAddress ShortAddress { get; set; }

        [FieldOrder(2)] [UsedImplicitly] public ReceiveOptionsExt ReceiveOptions { get; set; }

        [FieldOrder(3)] [UsedImplicitly] public byte SampleCount { get; set; }

        [FieldOrder(4)]
        public DigitalSampleChannels DigitalChannels { get; set; }

        [FieldOrder(5)]
        public AnalogSampleChannels AnalogChannels { get; set; }

        [FieldOrder(6)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input0,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input0)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input1,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input1)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input2,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input2)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input3,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input3)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input4,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input4)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input5,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input5)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input6,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input6)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input7,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input7)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input8,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input8)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input9,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input9)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input10,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input10)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input11,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input11)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input12,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input12)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input13,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input13)]
        [SerializeWhen(nameof(DigitalChannels), DigitalSampleChannels.Input14,
            ConverterType = typeof(BitwiseAndConverter), ConverterParameter = DigitalSampleChannels.Input14)]
        public DigitalSampleState DigitalSampleState { get; set; }

        [FieldOrder(7)]
        [FieldCount(nameof(AnalogChannels),
            ConverterType = typeof(BitCountingConverter), ConverterParameter = AllAnalogSampleChannels)]
        public List<ushort> AnalogSamples { get; set; }


        public Sample GetSample()
        {
            return new Sample(DigitalChannels, DigitalSampleState, AnalogChannels, GetAnalogSamples());
        }

        public NodeAddress GetAddress()
        {
            return new NodeAddress(Source, ShortAddress);
        }

        private IEnumerable<AnalogSample> GetAnalogSamples()
        {
            if (AnalogSamples == null)
                return Enumerable.Empty<AnalogSample>();

            IEnumerable<AnalogSampleChannels> analogChannels =
                (AnalogChannels & AllAnalogSampleChannels).GetFlagValues();
            return AnalogSamples.Zip(analogChannels, (sample, channel) => new AnalogSample(channel, sample));
        }
    }
}