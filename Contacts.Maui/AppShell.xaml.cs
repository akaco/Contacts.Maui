using Contacts.Maui.Views;

namespace Contacts.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(ContactsPage), typeof(ContactsPage));
		Routing.RegisterRoute("addcontact", typeof(AddContactPage));
		Routing.RegisterRoute("editcontact", typeof(EditContactPage));
	}
}
