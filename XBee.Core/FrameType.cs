﻿using JetBrains.Annotations;

namespace XBee
{
    [PublicAPI]
    internal enum FrameType : byte
    {
        TxRequest = 0x00,
        TxRequest16 = 0x01,
        AtCommand = 0x08,
        AtQueuedCommand = 0x09,
        TxRequestExt = 0x10,
        TxRequestExplicit = 0x11,
        RemoteAtCommand = 0x17,
        TxSms = 0x1f,
        TxIPv4 = 0x20,
        CreateSourceRoute = 0x21,
        RxIndicator = 0x80,
        RxIndicator16 = 0x81,
        RxIndicatorSample = 0x82,
        RxIndicator16Sample = 0x83,
        AtCommandResponse = 0x88,
        TxStatus = 0x89,
        ModemStatus = 0x8a,
        TxStatusExt = 0x8b,
        RouteInformation = 0x8d,
        AggregateAddressingUpdate = 0x8e,
        RxIndicatorExt = 0x90,
        RxIndicatorExplicitExt = 0x91,
        RxIndicatorSampleExt = 0x92,
        SensorReadIndicator = 0x94,
        NodeIdentificationIndicator = 0x95,
        RemoteAtCommandResponse = 0x97,
        ModemStatusExt = 0x98,
        RxSms = 0x9f,
        FirmwareUpdateStatus = 0xa0,
        RouteRecordIndicator = 0xa1,
        ManyToOneRouteRequestIndicator = 0xa3,
        RxIPv4 = 0xb0,
        Unknown = 0xff,
    }
}
