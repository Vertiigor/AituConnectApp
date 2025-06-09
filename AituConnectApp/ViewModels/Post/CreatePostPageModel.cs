using AituConnectApp.Dto.Responses;
using AituConnectApp.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AituConnectApp.ViewModels
{
    public partial class CreatePostPageModel : ObservableObject, IQueryAttributable, INotifyPropertyChanged
    {
        [ObservableProperty]
        private ObservableCollection<SubjectResponseDto> allSubjects = new();

        [ObservableProperty]
        private ObservableCollection<SubjectResponseDto> selectedSubjects;

        private readonly ISubjectApiService _subjectApiService; // Assuming you have a service to fetch subjects

        public CreatePostPageModel(ISubjectApiService subjectApiService)
        {
            _subjectApiService = subjectApiService;
            SelectedSubjects = new ObservableCollection<SubjectResponseDto>();
        }

        public void AddSubject(SubjectResponseDto subject)
        {
            if (!SelectedSubjects.Contains(subject))
            {
                SelectedSubjects.Add(subject);
            }
        }

        public void RemoveSubject(SubjectResponseDto subject)
        {
            if (SelectedSubjects.Contains(subject))
                SelectedSubjects.Remove(subject);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            await LoadData(); // ✅ called after navigation
        }

        private async Task LoadData()
        {
            var subjects = await _subjectApiService.GetAllAsync(); // Assuming you have a service to fetch subjects

            AllSubjects.Clear();

            foreach (var subject in subjects)
                AllSubjects.Add(subject);
        }
    }
}
