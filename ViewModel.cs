using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LAB_3.Models;
using LAB_3.Services;

namespace LAB_3.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Postgraduate> _allData = new();
        private ObservableCollection<Postgraduate> _filteredData = new();
        private string _searchText = string.Empty;

        public ObservableCollection<Postgraduate> FilteredData
        {
            get => _filteredData;
            set { _filteredData = value; OnPropertyChanged(); }
        }

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; ApplyFilters(); OnPropertyChanged(); }
        }

        public ICommand OpenFileCommand => new Command(async () => await OpenFile());
        public ICommand SaveFileCommand => new Command(async () => await SaveFile());
        public ICommand AddCommand => new Command(AddItem);
        public ICommand ShowAboutCommand => new Command(async () =>
            await App.Current.MainPage.DisplayAlert("Про програму",
            "Виконав: Прізвище Ім'я\nГрупа: КН-21\nРік: 2025\nОпис: Диспетчер аспірантури", "ОК"));

        private async Task OpenFile()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync();
                if (result != null)
                {
                    var list = await JsonService.LoadFromFileAsync(result.FullPath);
                    _allData = new ObservableCollection<Postgraduate>(list);
                    ApplyFilters();
                }
            }
            catch { }
        }

        private async Task SaveFile()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync();
                if (result != null)
                    await JsonService.SaveToFileAsync(result.FullPath, _allData.ToList());
            }
            catch { }
        }

        private void AddItem()
        {
            _allData.Add(new Postgraduate { FullName = "Новий запис", Department = "Кафедра" });
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filtered = _allData.Where(p =>
                p.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.Department.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
            ).ToList();
            FilteredData = new ObservableCollection<Postgraduate>(filtered);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
