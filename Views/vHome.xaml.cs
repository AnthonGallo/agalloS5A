using System.Threading.Tasks;
using agalloS5A.Model;

namespace agalloS5A.Views;

public partial class vHome : ContentPage
{
	public vHome()
	{
		InitializeComponent();
	}

    private void btnInsertarDato_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        App.personRepo.AddNewPerson(txtNombre.Text);

        statusMessage.Text = App.personRepo.statusMessage;
    }

    private void btnListarPersonas_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        List<Persona> lista = App.personRepo.GetAllPerson();
        listPersons.ItemsSource = lista;
    }

    private async void btnActualizarPersona_Clicked(object sender, EventArgs e)
    {
        if (listPersons.SelectedItem is Persona selectedPerson)
        {
            bool confirm = await DisplayAlert("Aviso", "¿Deseas actualizar este registro?", "Si", "No");
            if (confirm)
            {
                string newName = txtNombre.Text;
                if (string.IsNullOrWhiteSpace(newName))
                {
                    statusMessage.Text = "Ingrese un nombre válido.";
                    return;
                }
                App.personRepo.UpdatePerson(selectedPerson.Id, newName);
                statusMessage.Text = App.personRepo.statusMessage;
                btnListarPersonas_Clicked(sender, e);
            }
        }
        else
        {
            statusMessage.Text = "Seleccione una persona para actualizar, por favor.";
        }
    }

    private async void btnEliminarPersona_Clicked(object sender, EventArgs e)
    {
        if(listPersons.SelectedItem is Persona selectedPerson)
        {
            bool confirm = await DisplayAlert("Alerta", "¿Deseas eliminar este registro?", "Si", "No");
            if(confirm)
            {
                App.personRepo.DeletePerson(selectedPerson.Id);
                statusMessage.Text = App.personRepo.statusMessage;
                btnListarPersonas_Clicked(sender, e);
            }
        }
        else
        {
            statusMessage.Text = "Seleccione una persona para eliminar, por favor.";
        }
    }

    private void listPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (listPersons.SelectedItem is Persona selectedPersona)
        {
            txtNombre.Text = selectedPersona.Name;
        }
        else
        {
            txtNombre.Text = string.Empty;
        }
    }
}