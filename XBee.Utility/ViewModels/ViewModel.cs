﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;

namespace XBee.Utility.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private SerialDeviceViewModel _selectedSerialDevice;
        private IHardware _selectedHardware;
        public ObservableCollection<SerialDeviceViewModel> SerialDevices { get; } = new ObservableCollection<SerialDeviceViewModel>();

        public ViewModel()
        {
            ConnectCommand = new RelayCommand(ConnectAsync);
        }

        public SerialDeviceViewModel SelectedSerialDevice
        {
            get => _selectedSerialDevice;

            set
            {
                if (Equals(value, _selectedSerialDevice)) return;
                _selectedSerialDevice = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<XBeeControllerViewModel> DiscoveredControllers { get; } = new ObservableCollection<XBeeControllerViewModel>();

        public IHardware SelectedHardware
        {
            get => _selectedHardware;

            set
            {
                if (Equals(value, _selectedHardware)) return;
                _selectedHardware = value;
                OnPropertyChanged();
            }
        }

        public ICommand ConnectCommand { get; }

        public async Task RefreshDevicesAsync()
        {
            var devices = await DeviceInformation.FindAllAsync(SerialDevice.GetDeviceSelector());
            var serialDeviceTasks = devices.Select(device => SerialDevice.FromIdAsync(device.Id).AsTask());
            var serialDevices = await Task.WhenAll(serialDeviceTasks);
            var serialDeviceViewModels = serialDevices.Select(serialDevice => new SerialDeviceViewModel(serialDevice))
                .ToList();

            var newDevices = serialDeviceViewModels.Except(SerialDevices).ToList();
            var missingDevices = SerialDevices.Except(serialDeviceViewModels).ToList();
            
            foreach (var serialDevice in newDevices)
            {
                SerialDevices.Add(serialDevice);
                //serialDevice.SerialDevice.ReadTimeout = TimeSpan.MaxValue;
                //serialDevice.SerialDevice.WriteTimeout = TimeSpan.FromSeconds(60);
            }

            foreach (var missingDevice in missingDevices)
            {
                SerialDevices.Remove(missingDevice);
            }
        }

        private async void ConnectAsync()
        {
            var controller = new Universal.XBeeController(SelectedSerialDevice.SerialDevice);

            try
            {
                var hardwareVersion = await controller.GetHardwareVersionAsync();
                DiscoveredControllers.Add(new XBeeControllerViewModel(controller, hardwareVersion));
            }
            catch (TimeoutException)
            {
            }
        }
    }
}