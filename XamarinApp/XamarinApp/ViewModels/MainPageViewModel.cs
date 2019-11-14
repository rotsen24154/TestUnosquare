using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using XamarinApp.Models;

namespace XamarinApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Properties
        private string marca;
        public string Marca
        {
            get { return marca; }
            set { SetProperty(ref marca, value); }
        }

        private string modelo;
        public string Modelo
        {
            get { return modelo; }
            set { SetProperty(ref modelo, value); }
        }

        private string anio;
        public string Anio
        {
            get { return anio; }
            set { SetProperty(ref anio, value); }
        }

        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
        #endregion

        #region Commands
        private DelegateCommand addCarCommand;
        public DelegateCommand AddCarCommand =>
            addCarCommand ?? (addCarCommand = new DelegateCommand(ExecuteAddCarCommand, CanExecuteAddCarCommand))
                            .ObservesProperty(() => Modelo)
                            .ObservesProperty(() => Anio)
                            .ObservesProperty(() => Marca);
        #endregion

        #region Constructor
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";

            Cars.Add(new Car
            {
                Anio = "2010",
                Marca = "Audi",
                Modelo = "A4"
            });

            Cars.Add(new Car
            {
                Anio = "2019",
                Marca = "Mercedez",
                Modelo = "CLI 600"
            });
        }
        #endregion

        #region Methods
        private void ExecuteAddCarCommand()
        {
            var newCar = new Car
            {
                Anio = anio,
                Marca = marca, 
                Modelo = modelo
            };

            Cars.Add(newCar);
        }

        private bool CanExecuteAddCarCommand()
        {
            var isOnList = Cars.ToList().Find(x => x.Anio.Equals(Anio) && x.Marca.Equals(Marca) && x.Modelo.Equals(Modelo));
            return isOnList == null;
        }
        #endregion
    }
}
