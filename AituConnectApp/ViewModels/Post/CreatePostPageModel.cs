using AituConnectApp.Dto.Requests;
using AituConnectApp.Dto.Responses;
using AituConnectApp.Pages;
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

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string content;

        private readonly ISubjectApiService _subjectApiService;
        private readonly IPostApiService _postApiService;

        public CreatePostPageModel(ISubjectApiService subjectApiService, IPostApiService postApiService)
        {
            _subjectApiService = subjectApiService;
            _postApiService = postApiService;
            SelectedSubjects = new ObservableCollection<SubjectResponseDto>();
        }

        [RelayCommand]
        private async Task Create()
        {
            if (selectedSubjects == null || !selectedSubjects.Any())
            {
                await Shell.Current.DisplayAlert("Error", "Select at least on subject", "OK");
                return;
            }

            var dto = new CreatePostRequestDto
            {
                Title = title,
                Content = content,
                Subjects = selectedSubjects.Select(s => s.Id).ToList()
            };

            bool success = await _postApiService.CreateAsync(dto);

            if (success)
            {
#if DEBUG
                await Shell.Current.DisplayAlert("Success", "Post has been created!", "ОК");
#endif
                await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong...", "OK");
            }
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
            var subjects = await _subjectApiService.GetAllAsync();

            AllSubjects.Clear();

            foreach (var subject in subjects)
                AllSubjects.Add(subject);
        }
    }
}
