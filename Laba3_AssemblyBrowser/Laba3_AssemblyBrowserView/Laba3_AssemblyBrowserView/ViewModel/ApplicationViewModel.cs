using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AssemblyBrowserLibrary;
using AssemblyBrowserLibrary.Model;
using AssemblyBrowserView.Commands;
using Microsoft.Win32;

namespace AssemblyBrowserView.ViewModel
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {

        public List<NamespaceDescription> Namespaces { get; private set; }

        public AssemblyBrowser AssemblyBrowser { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ApplicationCommands openAssemblyCommand;

        public ApplicationViewModel()
        {
            Namespaces = new List<NamespaceDescription>();
            AssemblyBrowser = new AssemblyBrowser();
        }

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
                                Namespaces = AssemblyBrowser.GetAssemblyData(openFileDialog.FileName);
                                OnPropertyChanged(nameof(Namespaces));
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
