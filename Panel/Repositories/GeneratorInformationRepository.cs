﻿using Panel.Interfaces;
using Panel.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Panel.Repositories
{
    public class GeneratorInformationRepository : Repository<GeneratorUsage>, 
        IGeneratorInformationRepository
    {
        public GeneratorInformationRepository(GeneratorSurveillanceDBEntities context) 
            : base(context)
        {
            
        }
                
        public GeneratorSurveillanceDBEntities GeneratorSurveillanceDBContext
        {
            get { return Context as GeneratorSurveillanceDBEntities; }
        }

        public (bool IsNull, string ActiveGenName, DateTime? ActiveGenStartedDate,
            DateTime? ActiveGenStartedTime, int ActiveGenID) GeneratorStoppedIsNull()
        {
            bool isNull;
            string activeGenName = null;
            DateTime? activeGenStartedDate = null;
            DateTime? activeGenStartedTime = null;
            int activeGenID = 0;

            int NoOfRecords = GeneratorSurveillanceDBContext.GeneratorUsages.Count();
            var ActiveGenerators = GeneratorSurveillanceDBContext.GeneratorUsages
                                    .Where(x => 
                                    x.GeneratorStopped == 
                                    new DateTime(0001, 01, 01, 00, 00, 00) || 
                                    x.GeneratorStopped == 
                                    new DateTime(1899, 12, 30, 00, 00, 00) ||
                                    x.GeneratorStopped ==
                                    new DateTime(1900, 01, 01, 00, 00, 00));
            if (ActiveGenerators.ToList().Count == 0)
            {
                isNull = false;
            }
            else
            {
                isNull = true;
                var ActiveGeneratorsList = ActiveGenerators.ToList();
                var LastActiveGenIndex = ActiveGenerators.ToList().Count - 1;
                activeGenName = ActiveGeneratorsList[LastActiveGenIndex].GeneratorName;
                activeGenStartedDate = ActiveGeneratorsList[LastActiveGenIndex].Date;
                activeGenStartedTime = ActiveGeneratorsList[LastActiveGenIndex].GeneratorStarted;
                activeGenID = ActiveGeneratorsList[LastActiveGenIndex].Id;

                return (isNull, activeGenName, activeGenStartedDate, 
                    activeGenStartedTime, activeGenID);
            }
            return (isNull, activeGenName, activeGenStartedDate, 
                activeGenStartedTime, activeGenID);
        }
        

        public ObservableCollection<GeneratorNameModel> GetUniqueGeneratorNames()
        {
            return new ObservableCollection<GeneratorNameModel>
                (
                      GeneratorSurveillanceDBContext.GeneratorUsages
                     .Where(x => x.Id >= 1 && x.IsArchived != "Yes")
                     .OrderBy(x => x.GeneratorName)
                     .Select(x => new GeneratorNameModel
                        {
                            GeneratorName = x.GeneratorName
                        })
                     .Distinct()
                 );
        }

        public void AddGeneratorName(string GeneratorName, 
            ObservableCollection<GeneratorNameModel> uniqueGeneratorNames,
            ComboBox cmbxGenInfo)
        {
            if (!uniqueGeneratorNames.Where(x => x.GeneratorName == GeneratorName).Any())
            {
                uniqueGeneratorNames.Add(
                    new GeneratorNameModel { GeneratorName = GeneratorName });
                cmbxGenInfo.ItemsSource = uniqueGeneratorNames;
                cmbxGenInfo.SelectedIndex = cmbxGenInfo.Items.Count;
                MessageBox.Show($"{GeneratorName} added!", 
                    "Success", 
                    MessageBoxButton.OKCancel, 
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"{GeneratorName} already exists", 
                    "Error", 
                    MessageBoxButton.OKCancel, 
                    MessageBoxImage.Error);
                return;
            }                          
        }

        public void ArchiveGeneratorName(string GeneratorName, 
            ObservableCollection<GeneratorNameModel> uniqueGeneratorNames, 
            ComboBox cmbxGenInfo)
        {
            foreach (GeneratorUsage gu in GeneratorSurveillanceDBContext
                                                .GeneratorUsages)
            {
                if (gu.GeneratorName == GeneratorName)
                    gu.IsArchived = "Yes";
            }
            uniqueGeneratorNames.Remove((GeneratorNameModel)cmbxGenInfo.SelectedItem);
            cmbxGenInfo.ItemsSource = uniqueGeneratorNames;
        }
    }
}
