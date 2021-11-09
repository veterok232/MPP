using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowserView.Commands;
using Microsoft.Win32;

namespace AssemblyBrowserView.ViewModel
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ApplicationCommands openAssemblyCommand;

        public ApplicationCommands OpenAssemblyCommand
        {
            get
            {
                return openAssemblyCommand ??
                    (openAssemblyCommand = new ApplicationCommands(obj =>
                    {
                        try
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog();

                            if (openFileDialog.ShowDialog() == true)
                            {
                                NamespaceWrapper = AssemblyBrowser.GetAssemblyData(openFileDialog.FileName);
                                OnPropertyChanged(nameof(NamespaceWrapper));
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }));
            }
        }
    }
}
