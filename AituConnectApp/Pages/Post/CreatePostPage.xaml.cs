using AituConnectApp.Dto.Responses;
using AituConnectApp.ViewModels;

namespace AituConnectApp.Pages.Post
{

    public partial class CreatePostPage : ContentPage
    {
        public CreatePostPageModel ViewModel => BindingContext as CreatePostPageModel;

        public CreatePostPage(CreatePostPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }

        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var previous = e.PreviousSelection;
            var current = e.CurrentSelection;
        }

        private void OnSubjectSelected(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if (picker.SelectedItem is SubjectResponseDto selectedSubject)
            {
                if (!ViewModel.SelectedSubjects.Any(s => s.Id == selectedSubject.Id))
                    ViewModel.AddSubject(selectedSubject);

                picker.SelectedIndex = -1;
            }
        }


        private void OnRemoveSubjectClicked(object sender, EventArgs e)
        {
            if ((sender as ImageButton)?.CommandParameter is SubjectResponseDto subject)
            {
                ViewModel.RemoveSubject(subject);
            }
        }

    }
}