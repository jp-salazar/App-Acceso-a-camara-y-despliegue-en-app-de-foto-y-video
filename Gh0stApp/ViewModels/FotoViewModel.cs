using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using Gh0stApp.Services;


namespace Gh0stApp.ViewModels
{
    public class FotoViewModel : BaseViewModel
    {
        CameraService cameraService;
        private ImageSource _foto;

        public ImageSource Foto
        {
            get { return _foto; }
            set { _foto = value; OnPropertyChanged(); }
        }

        public ICommand TakePhotoCommand { get; private set; }
        public ICommand ChoosePhotoCommand { get; private set; }

        public FotoViewModel()
        {
            cameraService = new CameraService();
            Task.Run(async () => await cameraService.Init());

            TakePhotoCommand = new Command(async () => await TakePhoto());
            ChoosePhotoCommand = new Command(async () => await ChoosePhoto());
        }

        private async Task TakePhoto()
        {
            var file = await cameraService.TakePhoto();

            if (file != null)
                Foto = ImageSource.FromStream(() => file.GetStream());
        }

        private async Task ChoosePhoto()
        {
            var file = await cameraService.ChoosePhoto();

            if (file != null)
                Foto = ImageSource.FromStream(() => file.GetStream());
        }
    }
}