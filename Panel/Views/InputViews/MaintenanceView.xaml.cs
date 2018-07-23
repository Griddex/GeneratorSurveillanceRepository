﻿using Panel.BusinessLogic.MaintenanceLogic;
using Panel.Interfaces;
using Panel.Repositories;
using Panel.ViewModels.InputViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Panel.Views.InputViews
{
    /// <summary>
    /// Interaction logic for Maintenance.xaml
    /// </summary>
    public partial class MaintenanceView : Page, IView
    {
        public MaintenanceView(MaintenanceViewModel maintenanceViewModel)
        {
            InitializeComponent();
            this.DataContext = maintenanceViewModel;
        }

        private void GroupbyGenerator_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView cvsGeneratorConsumption = CollectionViewSource
                                                      .GetDefaultView(this.dtgdGenScheduledRemindersTable
                                                                          .ItemsSource);
            if (cvsGeneratorConsumption != null && cvsGeneratorConsumption.CanGroup == true)
            {
                cvsGeneratorConsumption.GroupDescriptions.Clear();
                cvsGeneratorConsumption.GroupDescriptions.Add(new PropertyGroupDescription("Generator"));
            }
        }

        private void ClearGeneratorGrouping_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView cvsGeneratorConsumption = CollectionViewSource
                                                      .GetDefaultView(this.dtgdGenScheduledRemindersTable
                                                                          .ItemsSource);
            if (cvsGeneratorConsumption != null && cvsGeneratorConsumption.CanGroup == true)
            {
                cvsGeneratorConsumption.GroupDescriptions.Clear();
            }
        }

        private void expdrScheduledReminders_Expanded(object sender, RoutedEventArgs e)
        {
            this.dtgdGenScheduledRemindersTable.Items.Refresh();
            ICollectionView cvsGeneratorConsumption = CollectionViewSource
                                                    .GetDefaultView(this.dtgdGenScheduledRemindersTable
                                                                        .ItemsSource);
            cvsGeneratorConsumption.Refresh();
        }

        private void DeactivateGenerator_Click(object sender, RoutedEventArgs e)
        {
            var dataGridRowSelected = (dynamic)dtgdGenScheduledRemindersTable.SelectedItem;
            string GeneratorName = dataGridRowSelected.GeneratorName;

            MessageBoxResult result = MessageBox.Show($"Do you want to deactivate {GeneratorName}?",
                                                    "Confirmation", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.No:
                case MessageBoxResult.None:
                case MessageBoxResult.Cancel:
                    return;
                case MessageBoxResult.Yes:
                case MessageBoxResult.OK:
                    ScheduledRemindersMethods.DeactivateGenerator(GeneratorName);                    
                    break;                              
                default:
                    break;
            }

            this.dtgdGenScheduledRemindersTable.ItemsSource = (this.DataContext as MaintenanceViewModel).UnitOfWork
                                                            .GeneratorScheduler.GetActiveGeneratorSchedules();
            this.dtgdGenScheduledRemindersTable.Items.Refresh();
            ICollectionView cvsGeneratorReminders = CollectionViewSource
                                                    .GetDefaultView(this.dtgdGenScheduledRemindersTable
                                                                        .ItemsSource);
            cvsGeneratorReminders.Refresh();
        }
    }
}
